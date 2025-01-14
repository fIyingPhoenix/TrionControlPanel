using System.Security.Cryptography;
using System.Text;

namespace TrionControlPanel.Desktop.Extensions.Cryptography
{
    public class AscEmuSHA1
    {
        public static string GetPasswordHash(string username, string password)
        {
            // AscEmu uses SHA1 hashing: SHA1(username:password)
            using (var sha1 = SHA1.Create())
            {
                string input = username.ToUpper() + ":" + password.ToUpper();
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                // Convert the hash bytes to a hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
