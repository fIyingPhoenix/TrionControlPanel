using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
namespace TrionControlPanel.API.Classes.Cryptography
{
    public class EncryptManager
    {
       public static string GetMd5HashFromFile(string filePath)
       {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
        public static string GetMd5HashFromStream(Stream stream)
        {
            using var md5 = MD5.Create(); // Create an MD5 hash instance
            var hashBytes = md5.ComputeHash(stream); // Compute the hash of the stream
            return BitConverter.ToString(hashBytes).Replace("-", "").ToUpperInvariant(); // Convert to hex string
        }
    }
    
}
