// =============================================================================
// SRP6Hashing.cs
// Purpose: Implements Secure Remote Password (SRP6) protocol for password verification
// Used by: AccountManager for creating secure password verifiers
// Step 11 of IMPROVEMENTS.md - Add Inline Comments for Complex Logic
// =============================================================================
//
// SRP6 Protocol Overview:
// -----------------------
// SRP (Secure Remote Password) is a cryptographic protocol that allows password
// verification without transmitting the actual password. Instead, a "verifier" is
// stored in the database, which can prove the user knows the password without
// revealing it.
//
// Key concepts:
// - Salt: Random data combined with the password to prevent rainbow table attacks
// - Verifier: A value derived from the password that can verify correctness
// - N: A large safe prime number (part of the protocol's security)
// - g: A generator value (typically 2 or 7)
//
// Mathematical basis: v = g^x mod N, where x = H(salt | H(username:password))
// =============================================================================

using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace TrionControlPanel.Desktop.Extensions.Cryptography
{
    /// <summary>
    /// Implements SRP6 (Secure Remote Password) protocol variants for different WoW emulators.
    /// </summary>
    public static class SRP6
    {
        #region V2SHA256 - Modern TrinityCore/CypherCore Implementation
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// SRP6 v2 implementation using SHA-256, used by modern TrinityCore and CypherCore.
        /// </summary>
        public class V2SHA256
        {
            // Generator value - a small number used as the base for modular exponentiation
            private const int g = 2;

            // Large safe prime N - this 256-byte prime ensures cryptographic security
            // Both client and server must use the same N value
            // The prime is stored as a hex string and converted to BigInteger
            private static readonly BigInteger N = new(Convert.FromHexString("AC6BDB41324A9A9BF166DE5E1389582FAF72B6651987EE07FC3192943DB56050A37329CBB4A099ED8193E0757767A13DD52312AB4B03310DCD7F48A9DA04FD50E8083969EDB767B0CF6095179A163AB3661A05FBD5FAAAE82918A9962F0B93B855F97993EC975EEAA80D740ADBF4FF747359D041D5C33EA71D281E446B14773BCA97B43A23FB801676BD207A436C6481F1D2B9078717461A5B9D32E688F87748544523B524B0D57D5EA77A2775D2ECFA032CFBDBF52FB3786160279004E57AE6AF874E7303CE53299CCC041C7BC308D82A5698F3A8D0C38271AE35F8E9DBFBB694B5C803D89F7AE435DE236D525F54759B65E372FCD68EF20FA7111F9E4AFF73"), true, true);

            /// <summary>
            /// Creates a password verifier using SRP6 v2 with SHA-256.
            /// </summary>
            /// <param name="username">Account username.</param>
            /// <param name="password">Account password.</param>
            /// <param name="salt">32-byte random salt value.</param>
            /// <returns>32-byte verifier that can prove password knowledge.</returns>
            public static byte[] CreateVerifier(string username, string password, byte[] salt)
            {
                // Step 1: Create the identity hash H(I | ":" | P)
                // Concatenate username:password in uppercase, then hash with SHA-256
                // This creates a unique identifier for this username/password combo
                byte[] h = SHA256.HashData(Encoding.UTF8.GetBytes($"{username}:{password}".ToUpper()));

                // Step 2: Calculate x = H(s | H(I | ":" | P))
                // Combine the salt with the identity hash to create the private key 'x'
                // This prevents rainbow table attacks since each account has unique salt
                BigInteger x = new(SHA256.HashData(salt.Concat(h).ToArray()), true);

                // Step 3: Calculate verifier v = g^x mod N
                // This is the core SRP6 calculation using modular exponentiation
                // The verifier can prove password knowledge without revealing password
                byte[] verifier = BigInteger.ModPow(g, x, N).ToByteArray();

                // Step 4: Ensure verifier is exactly 32 bytes (pad with zeros if needed)
                // The database expects a fixed-size field
                if (verifier.Length < 32)
                {
                    Array.Resize(ref verifier, 32);
                }
                return verifier;
            }

            /// <summary>
            /// Verifies a password against a stored verifier.
            /// </summary>
            public static bool VerifyPassword(string username, string password, byte[] salt, byte[] verifier)
            {
                // Recreate the verifier and compare - if they match, password is correct
                return verifier.SequenceEqual(CreateVerifier(username, password, salt));
            }
        }

        #endregion

        #region LegacySHA1 - AzerothCore/TrinityCore 3.3.5 Implementation
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Legacy SRP6 implementation using SHA-1, used by AzerothCore and TrinityCore 3.3.5.
        /// </summary>
        /// <remarks>
        /// This is the original WoW authentication hash format from the 3.3.5 client era.
        /// Uses a smaller prime N and generator g=7 instead of g=2.
        /// </remarks>
        public class LegecySHA1
        {
            // Generator value - legacy uses 7 instead of 2
            private const int g = 7;

            // Smaller 32-byte safe prime used by legacy WoW clients
            // This was sufficient security for the original game's requirements
            private static readonly BigInteger N = new(Convert.FromHexString("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7"), true, true);

            /// <summary>
            /// Creates a password verifier using legacy SRP6 with SHA-1.
            /// </summary>
            public static byte[] CreateVerifier(string username, string password, byte[] salt)
            {
                // Step 1: H(I | ":" | P) - identity hash using SHA-1
                // Note: Uses InvariantCulture for consistent uppercase conversion
                byte[] h = SHA1.HashData(Encoding.UTF8.GetBytes($"{username}:{password}".ToUpper(CultureInfo.InvariantCulture)));

                // Step 2: x = H(s | H(I | ":" | P)) - combine salt with identity hash
                BigInteger x = new(SHA1.HashData(salt.Concat(h).ToArray()), true);

                // Step 3: v = g^x mod N - modular exponentiation to create verifier
                byte[] verifier = BigInteger.ModPow(g, x, N).ToByteArray();

                // Step 4: Pad to 32 bytes if necessary
                if (verifier.Length < 32)
                {
                    Array.Resize(ref verifier, 32);
                }
                return verifier;
            }

            /// <summary>
            /// Verifies a password against a stored verifier.
            /// </summary>
            public static bool VerifyPassword(string username, string password, byte[] salt, byte[] verifier)
            {
                return verifier.SequenceEqual(CreateVerifier(username, password, salt));
            }
        }

        #endregion

        #region CMaNGOS SRP6 Implementation
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// SRP6 implementation for CMaNGOS and VMaNGOS emulators.
        /// </summary>
        /// <remarks>
        /// CMaNGOS uses a different byte ordering (little-endian) and hex string storage
        /// compared to TrinityCore-based emulators. This implementation matches the
        /// CMaNGOS server's expectations for salt and verifier format.
        /// </remarks>
        public class CMaNGosSRP6
        {
            // SRP6 constants matching CMaNGOS implementation
            private const int GENERATOR = 7;
            private const int SALT_BYTE_SIZE = 32;
            private const int VERIFIER_BYTE_SIZE = 32;

            // Large safe prime N - same as legacy but parsed differently
            private static readonly BigInteger N = BigInteger.Parse("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7", NumberStyles.HexNumber);

            /// <summary>
            /// Generates a cryptographically secure random salt.
            /// </summary>
            public static byte[] GenerateSalt()
            {
                return RandomNumberGenerator.GetBytes(SALT_BYTE_SIZE);
            }

            /// <summary>
            /// Creates a password verifier using CMaNGOS-compatible SRP6.
            /// </summary>
            /// <remarks>
            /// CMaNGOS has specific byte ordering requirements:
            /// - Salt and credential hashes are reversed (little-endian)
            /// - Verifier is stored in little-endian format
            /// This differs from TrinityCore which uses big-endian storage.
            /// </remarks>
            public static byte[] CreateVerifier(string username, string password, byte[] salt)
            {
                if (salt == null || salt.Length != SALT_BYTE_SIZE)
                    throw new ArgumentException($"Salt must be exactly {SALT_BYTE_SIZE} bytes", nameof(salt));

                // Step 1: Calculate SHA1 hash of "USERNAME:PASSWORD" (CalculateShaPassHash)
                // CMaNGOS stores credentials in uppercase
                string credentials = $"{username.ToUpper()}:{password.ToUpper()}";
                byte[] credentialsHash = SHA1.HashData(Encoding.UTF8.GetBytes(credentials));

                // Step 2: Reverse the salt for little-endian byte order
                // CMaNGOS server expects little-endian format for SRP calculations
                byte[] reversedSalt = new byte[salt.Length];
                Array.Copy(salt, reversedSalt, salt.Length);
                Array.Reverse(reversedSalt);

                // Step 3: Reverse the credentials hash for little-endian byte order
                byte[] reversedCredsHash = new byte[credentialsHash.Length];
                Array.Copy(credentialsHash, reversedCredsHash, credentialsHash.Length);
                Array.Reverse(reversedCredsHash);

                // Step 4: Calculate x = SHA1(salt | credentials_hash)
                // Concatenate reversed salt and reversed credentials hash
                byte[] combined = new byte[reversedSalt.Length + reversedCredsHash.Length];
                Array.Copy(reversedSalt, 0, combined, 0, reversedSalt.Length);
                Array.Copy(reversedCredsHash, 0, combined, reversedSalt.Length, reversedCredsHash.Length);
                byte[] xBytes = SHA1.HashData(combined);

                // Step 5: Convert x to BigInteger (little-endian, unsigned)
                // This matches how CMaNGOS interprets the hash as a number
                BigInteger x = new BigInteger(xBytes, isUnsigned: true, isBigEndian: false);

                // Step 6: Calculate verifier v = g^x mod N
                // Standard SRP6 modular exponentiation
                BigInteger verifier = BigInteger.ModPow(GENERATOR, x, N);

                // Step 7: Convert verifier to byte array (little-endian for CMaNGOS storage)
                byte[] verifierBytes = verifier.ToByteArray(isUnsigned: true, isBigEndian: false);

                // Step 8: Ensure verifier is exactly 32 bytes
                // Pad with leading zeros or trim trailing zeros as needed
                if (verifierBytes.Length < VERIFIER_BYTE_SIZE)
                {
                    byte[] padded = new byte[VERIFIER_BYTE_SIZE];
                    Array.Copy(verifierBytes, 0, padded, VERIFIER_BYTE_SIZE - verifierBytes.Length, verifierBytes.Length);
                    return padded;
                }
                else if (verifierBytes.Length > VERIFIER_BYTE_SIZE)
                {
                    // Take the last 32 bytes (BigInteger may have extra byte for sign)
                    byte[] trimmed = new byte[VERIFIER_BYTE_SIZE];
                    Array.Copy(verifierBytes, verifierBytes.Length - VERIFIER_BYTE_SIZE, trimmed, 0, VERIFIER_BYTE_SIZE);
                    return trimmed;
                }

                return verifierBytes;
            }

            /// <summary>
            /// Verifies a password against a stored verifier.
            /// </summary>
            public static bool VerifyPassword(string username, string password, byte[] salt, byte[] storedVerifier)
            {
                if (salt == null || salt.Length != SALT_BYTE_SIZE)
                    return false;

                if (storedVerifier == null || storedVerifier.Length != VERIFIER_BYTE_SIZE)
                    return false;

                byte[] calculatedVerifier = CreateVerifier(username, password, salt);

                return calculatedVerifier.SequenceEqual(storedVerifier);
            }

            /// <summary>
            /// Converts a byte array to its hexadecimal string representation.
            /// </summary>
            public static string BytesToHexString(byte[] bytes)
            {
                if (bytes == null)
                    throw new ArgumentNullException(nameof(bytes));

                // Convert each byte to 2 hex characters, no separator
                return BitConverter.ToString(bytes).Replace("-", "");
            }

            /// <summary>
            /// Converts a hexadecimal string to a byte array.
            /// </summary>
            public static byte[] HexStringToBytes(string hex)
            {
                if (string.IsNullOrEmpty(hex))
                    throw new ArgumentException("Hex string cannot be null or empty", nameof(hex));

                if (hex.Length % 2 != 0)
                    throw new ArgumentException("Hex string must have an even number of characters", nameof(hex));

                // Parse each pair of hex characters into a byte
                byte[] bytes = new byte[hex.Length / 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
                return bytes;
            }

            /// <summary>
            /// Creates complete account credentials (salt and verifier) for CMaNGOS.
            /// </summary>
            /// <returns>A tuple containing hex-encoded salt and verifier strings.</returns>
            public static (string SaltHex, string VerifierHex) CreateAccountCredentials(string username, string password)
            {
                byte[] salt = GenerateSalt();
                byte[] verifier = CreateVerifier(username, password, salt);

                // CMaNGOS stores salt and verifier as hex strings in the database
                return (BytesToHexString(salt), BytesToHexString(verifier));
            }
        }

        #endregion

        #region Shared Utility Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets the SRP username hash for Battle.net accounts.
        /// </summary>
        /// <param name="name">The username (typically email) to hash.</param>
        /// <param name="reverse">If true, reverses the byte order of the result.</param>
        /// <returns>Hex string representation of the SHA-256 hash.</returns>
        /// <remarks>
        /// Battle.net accounts use a hashed version of the email as the SRP username
        /// instead of the plaintext email. This provides additional privacy.
        /// </remarks>
        public static string GetSrpUsername(string name, bool reverse = false)
        {
            return ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(name)), reverse);
        }

        /// <summary>
        /// Converts a byte array to a hexadecimal string.
        /// </summary>
        /// <param name="byteArray">The bytes to convert.</param>
        /// <param name="reverse">If true, reverses byte order before conversion.</param>
        /// <returns>Uppercase hex string representation.</returns>
        public static string ToHexString(this byte[] byteArray, bool reverse = false)
        {
            // Build hex string by converting each byte to 2 hex characters
            if (reverse)
                return byteArray.Reverse().Aggregate("", (current, b) => current + b.ToString("X2"));
            else
                return byteArray.Aggregate("", (current, b) => current + b.ToString("X2"));
        }

        /// <summary>
        /// Generates a cryptographically secure 32-byte random salt.
        /// </summary>
        /// <remarks>
        /// Uses RandomNumberGenerator for cryptographic quality randomness.
        /// Each account should have a unique salt to prevent rainbow table attacks.
        /// </remarks>
        public static byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(32);
        }

        #endregion
    }
}
