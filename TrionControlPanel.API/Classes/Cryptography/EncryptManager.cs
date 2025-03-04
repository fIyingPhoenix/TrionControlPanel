using System.Security.Cryptography;

namespace TrionControlPanel.API.Classes.Cryptography
{
    public class EncryptManager
    {
        // Synchronously computes the MD5 hash of a file from its file path
        public static string GetMd5HashFromFile(string filePath)
        {
            // Use MD5 to compute the hash
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                // Compute the hash and convert it to a string (hexadecimal format)
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        // Synchronously computes the MD5 hash of a stream
        public static string GetMd5HashFromStream(Stream stream)
        {
            // Create MD5 hash instance and compute the hash of the provided stream
            using var md5 = MD5.Create();
            var hashBytes = md5.ComputeHash(stream);

            // Convert the computed hash bytes to a hexadecimal string (uppercase)
            return BitConverter.ToString(hashBytes).Replace("-", "").ToUpperInvariant();
        }

        // Asynchronously computes the MD5 hash of a file from its file path
        public static async Task<string> GetMd5HashFromFileAsync(string filePath)
        {
            // Open the file stream asynchronously with a buffer size of 4096
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                using (var md5 = MD5.Create())
                {
                    // Compute the hash asynchronously and return the result as a hex string
                    var hash = await md5.ComputeHashAsync(fileStream);
                    return BitConverter.ToString(hash).Replace("-", "").ToUpper();
                }
            }
        }
    }
}
