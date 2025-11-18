using System;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyDoAn.Utils
{
    public static class HashHelper
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}