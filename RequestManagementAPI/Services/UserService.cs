using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using RequestManagementAPI.Models;
using RequestManagementWeb.Helper;
using System.Reflection.Metadata;


namespace RequestManagementAPI.Services
{
    public class UserService
    {
        private readonly DbContextAssesment _context;

        public UserService(DbContextAssesment context)
        {
            _context = context;
        }


        public List<VwUser> GetVwUser(int userId, int page = 1, int pageSize = 10)
        {
            PermissionHelper.CheckPermissionRead(userId, PermissionHelper.MenuID.User, _context);

            return _context.VwUsers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public List<MstOrganization> GetAllMstOrganization()
        {
            return _context.MstOrganizations.ToList();
        }
        public List<MstDivision> GetAllMstDivision()
        {
            return _context.MstDivisions.ToList();
        }
        public List<MstRole> GetMstRoleByDivisionId(int divisionId)
        {
            return _context.MstRoles
                .Where(r => r.DivisionId == divisionId)
                .ToList();
        }

        // CREATE
        public MstUser CreateUser(MstUser user)
        {
            PermissionHelper.CheckPermissionCreate(user.CreatedBy, PermissionHelper.MenuID.User, _context);

            string encryptedPassword = EncryptionHelper.EncryptString(user.Password);
            user.Password = encryptedPassword;
            user.CreatedDate = DateTime.Now;
            _context.MstUsers.Add(user);
            _context.SaveChanges();
            return user;
        }
        // UPDATE
        public MstUser GetUserById(int id)
        {
            var user = _context.MstUsers.Find(id);
            user.Password = EncryptionHelper.DecryptString(user.Password);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return user;
        }
        public MstUser UpdateUser(int id, MstUser updatedUser)
        {
            PermissionHelper.CheckPermissionUpdate(updatedUser.UpdatedBy.Value, PermissionHelper.MenuID.User, _context);

            var existingUser = _context.MstUsers.Find(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            existingUser.Password = EncryptionHelper.EncryptString(updatedUser.Password);
            existingUser.UpdatedBy = updatedUser.UpdatedBy;
            existingUser.UpdatedDate = DateTime.Now;
            existingUser.OrganizationId = updatedUser.OrganizationId;
            existingUser.DivisionId = updatedUser.DivisionId;
            existingUser.RoleId = updatedUser.RoleId;

            _context.SaveChanges();
            return existingUser;
        }
        //DELETE
        public MstUser DeleteUser(int id, MstUser deletedUser)
        {
            PermissionHelper.CheckPermissionDelete(deletedUser.UpdatedBy.Value, PermissionHelper.MenuID.User, _context);

            var existingUser = _context.MstUsers.Find(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            existingUser.UpdatedBy = deletedUser.UpdatedBy;
            existingUser.UpdatedDate = DateTime.Now;
            existingUser.IsDeleted = true;

            _context.SaveChanges();
            return existingUser;
        }

        #region Helper
        //Helper function to get total count for paging 
        public int GetVwUserTotalCountAll()
        {
            return _context.VwUsers.Count();
        }
        #endregion


    }
}
