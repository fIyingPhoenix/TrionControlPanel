using System.Security.Cryptography;
using System.Text;

namespace TrionControlPanel.Desktop.Extensions.Cryptography
{
    public class AscEmuSHA1
    {
        // This method computes the SHA1 hash of a concatenation of username and password
        public static string GetPasswordHash(string username, string password)
        {
            // AscEmu uses SHA1 hashing for the combination of the username and password
            using (var sha1 = SHA1.Create())  // Create an instance of SHA1 for hashing
            {
                // Prepare the input string: concatenate the username and password (both uppercased) with a colon in between
                string input = username.ToUpper() + ":" + password.ToUpper();

                // Convert the input string to a byte array using ASCII encoding
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);

                // Compute the SHA1 hash of the input byte array
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                // Convert the resulting hash bytes into a hexadecimal string and return it
                // The Replace("-", "") removes dashes between byte pairs, and ToLower() makes the string lowercase
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
