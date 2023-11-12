using System.Security.Cryptography;
using System.Text;
using TrionControlPanel.Encrypting;

namespace TrionDiscordBot.Encrypting
{
    public class Password
    {
        public static string CalculatePassHashBnet(string name, string password)
        {
            //Hash Calculate system (Bnet Users)
            SHA256 sha256 = SHA256.Create();
            var nameHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(name));
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(nameHash.ToHexString() + ":" + password)).ToHexString(true);
        }
    }
}
