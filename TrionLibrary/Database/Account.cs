using System;
using System.Threading.Tasks;
using TrionLibrary.Crypto.SRP6;
using TrionLibrary.Database;
using TrionLibrary.Extensions;
using TrionLibrary.Models;
using TrionLibrary.Setting;
using static TrionLibrary.Models.Enums;

namespace TrionDatabase
{
    public class AccountCreate
    {
        const int MaxAccountLength = 16;
        const int MaxEmailLength = 64;
        const int MaxPasswordLength = 16;
        const int MaxBnetEmailLength = 320;
        const int MaxBnetPassLength = 128;
        public static string Message { get; set; }
        public AccountOpResult CreateBattlenet(string username, string email, string password, bool withGameAccount)
        {
            if (email.IsEmpty() || email.Length > 320)
                return AccountOpResult.NameTooLong;

            if (username.IsEmpty() || username.Length > MaxAccountLength)
                return AccountOpResult.NameTooLong;

            if (password.IsEmpty() || password.Length > MaxPasswordLength)
                return AccountOpResult.PassTooLong;

            if (GetUser(username) != 0 || GetEmail(email) != 0)
                return AccountOpResult.NameAlreadyExist;

            return AccountOpResult.Ok;

        }
        public static async Task<AccountOpResult> CreateAuth(string username, string password, string email, string datanase, Cores Core)
        {
            if (username.Length > MaxAccountLength)
                return AccountOpResult.NameTooLong;

            if (password.Length > MaxPasswordLength)
                return AccountOpResult.PassTooLong;

            if (email.IsEmpty() || email.Length > 320)
                return AccountOpResult.NameTooLong;

            if (GetUser(username) != 0 || GetEmail(email) != 0)
                return AccountOpResult.NameAlreadyExist;

            byte[] salt = AzerothCore.GenerateSalt();
            byte[] verifier = AzerothCore.CreateVerifier(username, password, salt);

            if(Core == Cores.AzerothCore)
            {
                try
                {
                    await Task.Run(() => Access.SaveData(SQLQuery.AccountCreate(), new
                    {
                        Username = username,
                        Salt = salt,
                        Verifier = verifier,
                        Email = email,
                        RegMail = email,
                        JoinDate = DateTime.Now,
                    }, Connect.String(datanase)));
                }
                catch (Exception ex) 
                {
                
                }

            }
           
            return AccountOpResult.Ok;
        }
        private static int GetUser(string username)
        {
            return 0;
        }
        public static int GetEmail(string Email)
        {
            return 0;
        }
    }
}
