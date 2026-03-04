using System.Text;
using Konscious.Security.Cryptography;

namespace TryOnVerse.API.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Convert password to bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Salt - in production, generate a random salt per user
            byte[] salt = Encoding.UTF8.GetBytes("TryOnVerseSalt123"); // demo only

            var argon2 = new Argon2id(passwordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 16
            };

            byte[] hashBytes = argon2.GetBytes(32);
            return Convert.ToBase64String(hashBytes);
        }
    }
}