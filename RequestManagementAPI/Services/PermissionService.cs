using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using RequestManagementAPI.Models;
using RequestManagementWeb.Helper;
using System.Reflection.Metadata;
using System.Security;


namespace RequestManagementAPI.Services
{
    public class PermissionService
    {
        private readonly DbContextAssesment _context;

        public PermissionService(DbContextAssesment context)
        {
            _context = context;
        }


        #region Data
        public List<VwRoleMenuPermission> GetVwRoleMenuPermission(int userId, int page = 1, int pageSize = 10)
        {
            PermissionHelper.CheckPermissionRead(userId, PermissionHelper.MenuID.Permission, _context);

            return _context.VwRoleMenuPermissions
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public List<MstRole> GetMstRole()
        {
            return _context.MstRoles.ToList();
        }
        public List<MstMenu> GetMstMenu()
        {
            return _context.MstMenus.ToList();
        }

        // CREATE
        public MstRoleMenuPermission CreatePermission(MstRoleMenuPermission permission)
        {
            PermissionHelper.CheckPermissionCreate(permission.CreatedBy, PermissionHelper.MenuID.Permission, _context);

            var existingPermission = _context.MstRoleMenuPermissions
                .Where(r => r.RoleId == permission.RoleId && r.MenuId == permission.MenuId)
                .FirstOrDefault();

            if (existingPermission != null)
            {
                throw new KeyNotFoundException($"Permission already exist.");
            }

            permission.CreatedDate = DateTime.Now;
            _context.MstRoleMenuPermissions.Add(permission);
            _context.SaveChanges();

            return permission;
        }

        public MstRoleMenuPermission GetPermissionById(int id)
        {
            var permission = _context.MstRoleMenuPermissions.Find(id);
            if (permission == null)
            {
                throw new KeyNotFoundException($"Permission with ID {id} not found.");
            }
            return permission;
        }

        public MstRoleMenuPermission UpdatePermission(int id, MstRoleMenuPermission updatedPermission)
        {
            PermissionHelper.CheckPermissionUpdate(updatedPermission.UpdatedBy.Value, PermissionHelper.MenuID.Permission, _context);

            var existingPermission = _context.MstRoleMenuPermissions.Find(id);
            if (existingPermission == null)
            {
                throw new KeyNotFoundException($"Permission with ID {id} not found.");
            }

            existingPermission.UpdatedBy = updatedPermission.UpdatedBy;
            existingPermission.UpdatedDate = DateTime.Now;
            existingPermission.RoleId = updatedPermission.RoleId;
            existingPermission.MenuId = updatedPermission.MenuId;
            existingPermission.IsCreate = updatedPermission.IsCreate;
            existingPermission.IsRead = updatedPermission.IsRead;
            existingPermission.IsUpdate = updatedPermission.IsUpdate;
            existingPermission.IsDelete = updatedPermission.IsDelete;

            _context.SaveChanges();
            return existingPermission;
        }
        #endregion



        #region Helper
        //Helper function to get total count for paging 
        public int GetVwRoleMenuPermissionTotalCountAll()
        {
            return _context.VwRoleMenuPermissions.Count();
        }
        #endregion


    }
}
