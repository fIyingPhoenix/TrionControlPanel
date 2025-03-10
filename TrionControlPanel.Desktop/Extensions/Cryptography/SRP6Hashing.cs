using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace TrionControlPanel.Desktop.Extensions.Cryptography
{
    public static class SRP6
    {
        public class V2SHA256
        {
            private const int g = 2;
            private static readonly BigInteger N = new(Convert.FromHexString("AC6BDB41324A9A9BF166DE5E1389582FAF72B6651987EE07FC3192943DB56050A37329CBB4A099ED8193E0757767A13DD52312AB4B03310DCD7F48A9DA04FD50E8083969EDB767B0CF6095179A163AB3661A05FBD5FAAAE82918A9962F0B93B855F97993EC975EEAA80D740ADBF4FF747359D041D5C33EA71D281E446B14773BCA97B43A23FB801676BD207A436C6481F1D2B9078717461A5B9D32E688F87748544523B524B0D57D5EA77A2775D2ECFA032CFBDBF52FB3786160279004E57AE6AF874E7303CE53299CCC041C7BC308D82A5698F3A8D0C38271AE35F8E9DBFBB694B5C803D89F7AE435DE236D525F54759B65E372FCD68EF20FA7111F9E4AFF73"), true, true);
            public static byte[] CreateVerifier(string username, string password, byte[] salt)
            {
                // H(I | ":" | P) with SHA-256
                byte[] h = SHA256.HashData(Encoding.UTF8.GetBytes($"{username}:{password}".ToUpper()));

                // x = H(s | H(I | ":" | P)) with SHA-256
                BigInteger x = new(SHA256.HashData(salt.Concat(h).ToArray()), true);

                // g^x mod N
                byte[] verifier = BigInteger.ModPow(g, x, N).ToByteArray();

                // Pad to 32 bytes
                if (verifier.Length < 32)
                {
                    Array.Resize(ref verifier, 32);
                }
                return verifier;
            }
            public static bool VerifyPassword(string username, string password, byte[] salt, byte[] verifier)
            {
                return verifier.SequenceEqual(CreateVerifier(username, password, salt));
            }

        }
        public class LegecySHA1
        {
            private const int g = 7;
            private static readonly BigInteger N = new(Convert.FromHexString("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7"), true, true);
            public static byte[] CreateVerifier(string username, string password, byte[] salt)
            {
                // H(I | ":" | P)
                byte[] h = SHA1.HashData(Encoding.UTF8.GetBytes($"{username}:{password}".ToUpper(CultureInfo.InvariantCulture)));

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
            public static bool VerifyPassword(string username, string password, byte[] salt, byte[] verifier)
            {
                return verifier.SequenceEqual(CreateVerifier(username, password, salt));
            }          
        }
        public static string GetSrpUsername(string name, bool reverse = false)
        {
            return ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(name)), reverse);
        }
        public static string ToHexString(this byte[] byteArray, bool reverse = false)
        {
            if (reverse)
                return byteArray.Reverse().Aggregate("", (current, b) => current + b.ToString("X2"));
            else
                return byteArray.Aggregate("", (current, b) => current + b.ToString("X2"));
        }
        public static byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(32);
        }

    }
}
