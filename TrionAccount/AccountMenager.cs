using System.Text;
using TrionControlPanel.Classes;
using System.Security.Cryptography;

namespace TrionControlPanel.Database
{
    internal class AccountMenager
    {
        public static bool CheckForUsername(string username)
        {
            return true;
        }
        // CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP)
        public static string CalculatePassHashBnet(string name, string password)
        {
            //Hash Calculate system (Bnet Users)
            SHA256 sha256 = SHA256.Create();
            var nameHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(name));
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(nameHash.ToHexString() + ":" + password)).ToHexString(true);
        }

        public static string CalculatePasswordHashAscEmu(string name, string password)
        {
            using (SHA1Managed sha1 = new())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes($"{name.ToUpper()}:{password.ToUpper()}"));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    // can be "x2" if you want LowerCase - can be "X2" if you want UpperCase
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
