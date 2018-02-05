using System;
using System.Security.Cryptography;
using System.Text;

namespace NikiCars.Services
{
    public class CryptographyService : ICryptographyService
    {
        private const int ITERATIONS = 10000;
        private const int SALTLENGTH = 16;
        private const int PASSWORDLENGTH = 32;

        private byte[] GenerateSalt()
        {
            var bytes = new byte[SALTLENGTH];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        private byte[] GenerateHash(byte[] password, byte[] salt)
        {
            byte[] encrypted = HashSHA256Cng(password);

            using (var deriveBytes = new Rfc2898DeriveBytes(encrypted, salt, ITERATIONS))
            {
                var hash = deriveBytes.GetBytes(PASSWORDLENGTH);
                byte[] hashBytes = new byte[SALTLENGTH + PASSWORDLENGTH];
                Array.Copy(hash, 0, hashBytes, 0, PASSWORDLENGTH);
                Array.Copy(salt, 0, hashBytes, PASSWORDLENGTH, SALTLENGTH);

                return hashBytes;
            }
        }

        private byte[] HashSHA256Cng(byte[] password)
        {
            byte[] encrypted;
            
            using (SHA256Cng sHA256Cng = new SHA256Cng())
            {
                encrypted = sHA256Cng.ComputeHash(password);
            }

            return encrypted;
        }


        public string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] pass = Encoding.UTF8.GetBytes(password);
            byte[] hash = GenerateHash(pass, salt);

            string hashPassword = Convert.ToBase64String(hash);


            return hashPassword;
        }

        public bool ValidatePassword(string password, string hashedPassword)
        {
            byte[] storedPassword = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SALTLENGTH];
            Array.Copy(storedPassword, PASSWORDLENGTH, salt, 0, SALTLENGTH);
            byte[] pass = Encoding.UTF8.GetBytes(password);

            string newhashedPass = Convert.ToBase64String(GenerateHash(pass, salt));

            if (newhashedPass == hashedPassword)
            {
                return true;
            }

            return false;
        }
    }
}
