using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BeatLab
{
    public static class SaltedPasswordGenerator
    {
        public static void GenerateHashAndSalt(string password,
            out byte[] saltBytes,
            out byte[] encryptedPasswordAndSalt,
            byte[] salt = null)
        {
            byte[] passwordBytes = Encoding.UTF8
                .GetBytes(password);
            if (salt == null)
            {
                salt = Encoding.UTF8
                    .GetBytes(
                        Guid.NewGuid()
                            .ToString()
                            .Substring(0, 4));
            }
            byte[] passwordAndSalt = passwordBytes
                .Concat(salt)
                .ToArray();
            encryptedPasswordAndSalt = SHA256
                .Create()
                .ComputeHash(passwordAndSalt);
            saltBytes = salt;
        }
    }
}