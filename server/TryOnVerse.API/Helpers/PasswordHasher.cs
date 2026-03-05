using System.Text;
using Konscious.Security.Cryptography;

namespace TryOnVerse.API.Helpers
{
    public static class PasswordHasher
    {
        private static readonly byte[] Salt = Encoding.UTF8.GetBytes("TryOnVerseSalt123"); // demo only, ideally per-user

        public static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            var argon2 = new Argon2id(passwordBytes)
            {
                Salt = Salt,
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 16
            };

            byte[] hashBytes = argon2.GetBytes(32);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            string hashOfInput = HashPassword(password);
            return hashOfInput == storedHash;
        }
    }
}