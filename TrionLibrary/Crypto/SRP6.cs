using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace TrionLibrary.Crypto
{
    internal class SRP6Hashing
    {
        private const int g = 7;
        private static readonly BigInteger N = new(Convert.FromHexString("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7"), true, true);
        public static byte[] CreateVerifier(string username, string password, byte[] salt)
        {
            // H(I | ":" | P)
            byte[] h = SHA1.HashData(Encoding.UTF8.GetBytes($"{username}:{password}".ToUpper()));

            // x = H(s | H(I | ":" | P))
            BigInteger x = new(SHA1.HashData(salt.Concat(h).ToArray()), true);

            // g^x mod N
            byte[] verifier = BigInteger.ModPow(g, x, N).ToByteArray();

            // Pad to 32 bytes
            if (verifier.Length < 32)
            {
                Array.Resize(ref verifier, 32);
            }
            return verifier;
        }
        public static byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(32);
        }
        public static bool VerifyPassword(string username, string password, byte[] salt, byte[] verifier)
        {
            return verifier.SequenceEqual(CreateVerifier(username, password, salt));
        }

    }

}
