using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace AirplaneTickets.Helpers
{
    public static class PasswordHasher
    {
        public static string GetHashString(string inputString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte hashByte in GetHash(inputString))
                stringBuilder.Append(hashByte.ToString("X2"));

            return stringBuilder.ToString();
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}
