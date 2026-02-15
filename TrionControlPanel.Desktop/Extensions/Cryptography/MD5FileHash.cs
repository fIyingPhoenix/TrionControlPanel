
using System.Security.Cryptography;

namespace TrionControlPanel.Desktop.Extensions.Cryptography
{
    public class MD5FileHash
    {
        public static string GetMd5HashFromFile(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
            }
        }
        public static string GetMd5HashFromStream(Stream stream)
        {
            using var md5 = MD5.Create();
            var hashBytes = md5.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToUpperInvariant();
        }

        public static async Task<string> GetMd5HashFromFileAsync(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                using (var md5 = MD5.Create())
                {
                    var hash = await md5.ComputeHashAsync(fileStream);
                    return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
                }
            }
        }
    }
}
