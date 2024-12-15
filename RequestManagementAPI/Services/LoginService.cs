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
        
        public MstUser CheckLogin(string username, string password)
        {
            string encryptedPassword = EncryptionHelper.EncryptString(password);
            var user = _context.MstUsers.FirstOrDefault(u => u.UserName == username && u.Password == encryptedPassword); 
            if (user == null || user.IsDeleted)
            {
                throw new KeyNotFoundException($"Invalid username or password.");
            }
            
            return user;
        }
        #endregion


    }
}
