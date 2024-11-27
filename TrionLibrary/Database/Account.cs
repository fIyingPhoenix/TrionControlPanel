using Org.BouncyCastle.Crypto;
using System;
using System.Threading.Tasks;
using TrionLibrary.Crypto;
using TrionLibrary.Database;
using TrionLibrary.Extensions;
using TrionLibrary.Sys;
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
            if (email.IsEmpty() || email.Length > MaxEmailLength)
                return AccountOpResult.NameTooLong;

            if (username.IsEmpty() || username.Length > MaxBnetEmailLength)
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

            if (email.IsEmpty() || email.Length > MaxEmailLength)
                return AccountOpResult.NameTooLong;

            if (GetUser(username) != 0 || GetEmail(email) != 0)
                return AccountOpResult.NameAlreadyExist;

            if (Core == Cores.AzerothCore)
            {
                byte[] salt = SRP6Hashing.GenerateSalt();
                byte[] verifier = SRP6Hashing.CreateVerifier(username, password, salt);
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
                    return AccountOpResult.Ok;
                }
                catch (Exception ex)
                {
                    Infos.Message = ex.Message;
                    return AccountOpResult.DBInternalError;
                }

            }
            if (Core == Cores.AscEmu)
            {
                try
                {
                   var passhash = SHA1Hashing.AscEmuHashPassword(username, password);
                    await Task.Run(() => Access.SaveData(SQLQuery.AccountCreate(), new
                    {
                        Username = username,
                        EncryptedPassword = passhash,
                        Email = email,
                        JoinDate = DateTime.Now,
                    }, Connect.String(datanase)));
                    return AccountOpResult.Ok;
                }
                catch (Exception ex)
                {
                    Infos.Message = ex.Message;
                    return AccountOpResult.DBInternalError;
                }
            }
            if (Core == Cores.TrinityCore335)
            {
                byte[] salt = SRP6Hashing.GenerateSalt();
                byte[] verifier = SRP6Hashing.CreateVerifier(username, password, salt);
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
                    return AccountOpResult.Ok;
                }
                catch (Exception ex)
                {
                    Infos.Message = ex.Message; 
                    return AccountOpResult.DBInternalError;
                }
            }
            if (Core == Cores.TrinityCore)
            {
                try
                {

                }
                catch (Exception ex)
                {
                    Infos.Message = ex.Message;
                    return AccountOpResult.DBInternalError;
                }
            }
            if (Core == Cores.CMaNGOS || Core == Cores.VMaNGOS)
            {
                try
                {

                }
                catch (Exception ex)
                {
                    Infos.Message = ex.Message;
                    return AccountOpResult.DBInternalError;
                }
            }
            return AccountOpResult.BadLink;
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
