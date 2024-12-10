using Microsoft.EntityFrameworkCore;
using RequestManagementAPI.Models;
using RequestManagementWeb.Helper;
using System.Reflection.Metadata;


namespace RequestManagementAPI.Services
{
    public class LoginService
    {
        private readonly DbContextAssesment _context;

        public LoginService(DbContextAssesment context)
        {
            _context = context;
        }

        
        #region Auth
        /*public List<MstUser> GetAllMstUser()
        {
            return _context.MstUsers.ToList();
        }

        public MstUser GetMstUserById(int id)
        {
            var user = _context.MstUsers.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username {user.UserName} not found.");
            }
            return user;
        }*/

        public MstUser CheckLogin(string username, string password)
        {
            string encryptedPassword = EncryptionHelper.EncryptString(password);
            var user = _context.MstUsers.FirstOrDefault(u => u.UserName == username && u.Password == encryptedPassword); 
            if (user == null)
            {
                throw new KeyNotFoundException($"Invalid username or password.");
            }
            
            return user;
        }
        #endregion


    }
}
