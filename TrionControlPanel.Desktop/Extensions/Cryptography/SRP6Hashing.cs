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
        public class CMaNGosSRP6
        {
            // SRP6 constants from CMaNGOS
            private const int GENERATOR = 7;
            private const int SALT_BYTE_SIZE = 32;
            private const int VERIFIER_BYTE_SIZE = 32;

            // Large safe prime N (from CMaNGOS SRP6.cpp)
            private static readonly BigInteger N = BigInteger.Parse("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7", NumberStyles.HexNumber);

            public static byte[] GenerateSalt()
            {
                return RandomNumberGenerator.GetBytes(SALT_BYTE_SIZE);
            }


            public static byte[] CreateVerifier(string username, string password, byte[] salt)
            {
                if (salt == null || salt.Length != SALT_BYTE_SIZE)
                    throw new ArgumentException($"Salt must be exactly {SALT_BYTE_SIZE} bytes", nameof(salt));

                //Calculate SHA1 hash of "USERNAME:PASSWORD" (CalculateShaPassHash)
                string credentials = $"{username.ToUpper()}:{password.ToUpper()}";
                byte[] credentialsHash = SHA1.HashData(Encoding.UTF8.GetBytes(credentials));

                //Reverse the salt (CMaNGOS uses little-endian byte order)
                byte[] reversedSalt = new byte[salt.Length];
                Array.Copy(salt, reversedSalt, salt.Length);
                Array.Reverse(reversedSalt);

                //Reverse the credentials hash
                byte[] reversedCredsHash = new byte[credentialsHash.Length];
                Array.Copy(credentialsHash, reversedCredsHash, credentialsHash.Length);
                Array.Reverse(reversedCredsHash);

                //Calculate x = SHA1(salt | credentials_hash)
                byte[] combined = new byte[reversedSalt.Length + reversedCredsHash.Length];
                Array.Copy(reversedSalt, 0, combined, 0, reversedSalt.Length);
                Array.Copy(reversedCredsHash, 0, combined, reversedSalt.Length, reversedCredsHash.Length);

                byte[] xBytes = SHA1.HashData(combined);

                //Convert x to BigInteger (little-endian, unsigned)
                BigInteger x = new BigInteger(xBytes, isUnsigned: true, isBigEndian: false);

                // Calculate verifier v = g^x mod N
                BigInteger verifier = BigInteger.ModPow(GENERATOR, x, N);

                // Convert verifier to byte array (32 bytes, big-endian for storage)
                byte[] verifierBytes = verifier.ToByteArray(isUnsigned: true, isBigEndian: false);

                // Pad to 32 bytes if necessary
                if (verifierBytes.Length < VERIFIER_BYTE_SIZE)
                {
                    byte[] padded = new byte[VERIFIER_BYTE_SIZE];
                    Array.Copy(verifierBytes, 0, padded, VERIFIER_BYTE_SIZE - verifierBytes.Length, verifierBytes.Length);
                    return padded;
                }
                else if (verifierBytes.Length > VERIFIER_BYTE_SIZE)
                {
                    // Take the last 32 bytes
                    byte[] trimmed = new byte[VERIFIER_BYTE_SIZE];
                    Array.Copy(verifierBytes, verifierBytes.Length - VERIFIER_BYTE_SIZE, trimmed, 0, VERIFIER_BYTE_SIZE);
                    return trimmed;
                }

                return verifierBytes;
            }

            public static bool VerifyPassword(string username, string password, byte[] salt, byte[] storedVerifier)
            {
                if (salt == null || salt.Length != SALT_BYTE_SIZE)
                    return false;

                if (storedVerifier == null || storedVerifier.Length != VERIFIER_BYTE_SIZE)
                    return false;

                byte[] calculatedVerifier = CreateVerifier(username, password, salt);

                return calculatedVerifier.SequenceEqual(storedVerifier);
            }

            public static string BytesToHexString(byte[] bytes)
            {
                if (bytes == null)
                    throw new ArgumentNullException(nameof(bytes));

                return BitConverter.ToString(bytes).Replace("-", "");
            }

            public static byte[] HexStringToBytes(string hex)
            {
                if (string.IsNullOrEmpty(hex))
                    throw new ArgumentException("Hex string cannot be null or empty", nameof(hex));

                if (hex.Length % 2 != 0)
                    throw new ArgumentException("Hex string must have an even number of characters", nameof(hex));

                byte[] bytes = new byte[hex.Length / 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
                return bytes;
            }


            public static (string SaltHex, string VerifierHex) CreateAccountCredentials(string username, string password)
            {
                byte[] salt = GenerateSalt();
                byte[] verifier = CreateVerifier(username, password, salt);

                return (BytesToHexString(salt), BytesToHexString(verifier));
            }
        }

    }
}
