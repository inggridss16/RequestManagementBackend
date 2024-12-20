﻿namespace RequestManagementWeb.Helper
{
    using Org.BouncyCastle.Asn1.Ocsp;
    using RequestManagementAPI.Models;
    using System.Security.Cryptography;
    using System.Text;

    public static class EncryptionHelper
    {
        private static readonly byte[] _key = Encoding.UTF8.GetBytes("A1B2C3D4E5F6G7H8I9J0K1L2M3N4O5P6"); // Replace with a strong, randomly generated key
        private static readonly byte[] _iv = Encoding.UTF8.GetBytes("0123456789ABCDEF"); // Replace with a strong, randomly generated IV (Initialization Vector)


        public static string EncryptString(string plainText)
        {
            using Aes aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
            using (StreamWriter streamWriter = new(cryptoStream, Encoding.UTF8))
            {
                streamWriter.Write(plainText);
            }

            byte[] encryptedBytes = memoryStream.ToArray();
            return Convert.ToBase64String(encryptedBytes); // Or another encoding like hex
        }

        public static string DecryptString(string cipherText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(cipherText); // Or other encoding if used during encryption

            using Aes aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;


            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new(encryptedBytes);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream, Encoding.UTF8);
            return streamReader.ReadToEnd();

        }
    }

    public static class PermissionHelper
    {
        public static void CheckPermissionCreate(int userId, int menuId, DbContextAssesment context)
        {
            var existingUser = context.MstUsers.Find(userId);
            var existingRoleMenuPermission = context.MstRoleMenuPermissions
                .Where(r => r.RoleId == existingUser.RoleId && r.MenuId == menuId)
                .FirstOrDefault();

            if (existingRoleMenuPermission != null && !existingRoleMenuPermission.IsCreate)
                throw new UnauthorizedAccessException("You have no access.");
            else if (existingRoleMenuPermission == null)
                throw new UnauthorizedAccessException("You have no access."); 
        }
        public static void CheckPermissionRead(int userId, int menuId, DbContextAssesment context)
        {
            var existingUser = context.MstUsers.Find(userId);
            var existingRoleMenuPermission = context.MstRoleMenuPermissions
                .Where(r => r.RoleId == existingUser.RoleId && r.MenuId == menuId)
                .FirstOrDefault();

            if (existingRoleMenuPermission != null && !existingRoleMenuPermission.IsRead)
                throw new UnauthorizedAccessException("You have no access.");
            else if (existingRoleMenuPermission == null)
                throw new UnauthorizedAccessException("You have no access.");
        }
        public static void CheckPermissionUpdate(int userId, int menuId, DbContextAssesment context)
        {
            var existingUser = context.MstUsers.Find(userId);
            var existingRoleMenuPermission = context.MstRoleMenuPermissions
                .Where(r => r.RoleId == existingUser.RoleId && r.MenuId == menuId)
                .FirstOrDefault();

            if (existingRoleMenuPermission != null && !existingRoleMenuPermission.IsUpdate)
                throw new UnauthorizedAccessException("You have no access.");
            else if (existingRoleMenuPermission == null)
                throw new UnauthorizedAccessException("You have no access.");
        }
        public static void CheckPermissionDelete(int userId, int menuId, DbContextAssesment context)
        {
            var existingUser = context.MstUsers.Find(userId);
            var existingRoleMenuPermission = context.MstRoleMenuPermissions
                .Where(r => r.RoleId == existingUser.RoleId && r.MenuId == menuId)
                .FirstOrDefault();

            if (existingRoleMenuPermission != null && !existingRoleMenuPermission.IsDelete)
                throw new UnauthorizedAccessException("You have no access.");
            else if (existingRoleMenuPermission == null)
                throw new UnauthorizedAccessException("You have no access.");
        }

        public static class MenuID
        {
            public static int Request { get; } = 1;
            public static int Expense { get; } = 2;
            public static int User { get; } = 3;
            public static int Permission { get; } = 4;
        }
    }
    // Example Usage:
    /*public class Example
    {
        public static void Main(string[] args)
        {
            string originalText = "This is a secret message.";

            string encryptedText = EncryptionHelper.EncryptString(originalText);
            Console.WriteLine($"Encrypted: {encryptedText}");

            string decryptedText = EncryptionHelper.DecryptString(encryptedText);
            Console.WriteLine($"Decrypted: {decryptedText}");
        }
    }*/
}
