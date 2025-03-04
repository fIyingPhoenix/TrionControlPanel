using System.Security.Cryptography;
using System.IO;

namespace TrionControlPanel.Desktop.Extensions.Cryptography
{
    public class MD5FileHash  // Fixed the typo in the class name from "Hasah" to "Hash"
    {
        // Computes the MD5 hash of a file given its file path (synchronous method)
        public static string GetMd5HashFromFile(string filePath)
        {
            using (var md5 = MD5.Create()) // Create an MD5 hash instance
            using (var stream = File.OpenRead(filePath)) // Open the file as a stream
            {
                var hash = md5.ComputeHash(stream); // Compute the hash of the file stream
                return BitConverter.ToString(hash) // Convert the hash to a hex string
                    .Replace("-", "") // Remove the hyphens from the hex string
                    .ToLowerInvariant(); // Convert the hex string to lowercase
            }
        }

        // Computes the MD5 hash from a given stream (synchronous method)
        public static string GetMd5HashFromStream(Stream stream)
        {
            using var md5 = MD5.Create(); // Create an MD5 hash instance
            var hashBytes = md5.ComputeHash(stream); // Compute the hash of the stream
            return BitConverter.ToString(hashBytes) // Convert the hash to a hex string
                .Replace("-", "") // Remove the hyphens
                .ToUpperInvariant(); // Convert to uppercase for consistency
        }

        // Computes the MD5 hash of a file asynchronously
        public static async Task<string> GetMd5HashFromFileAsync(string filePath)
        {
            // Open the file stream asynchronously with a buffer size of 4096
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            using (var md5 = MD5.Create()) // Create an MD5 hash instance
            {
                var hash = await md5.ComputeHashAsync(fileStream); // Asynchronously compute the hash
                return BitConverter.ToString(hash) // Convert the hash to a hex string
                    .Replace("-", "") // Remove hyphens
                    .ToUpper(); // Convert to uppercase for consistency
            }
        }
    }
}
