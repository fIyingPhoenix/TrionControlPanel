using System.Text;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Cryptography;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    public class AccountManager
    {
        const int MaxAccountLength = 16;
        const int MaxEmailLength = 64;
        const int MaxPasswordLength = 16;
        const int MaxBnetEmailLength = 320;
        const int MaxBnetPassLength = 128;
        public static string Message { get; set; }

        public async static Task<AccountOpResult> CreateBattlenetAccount(string username, string email, string password, AppSettings Settings)
        {
            if (string.IsNullOrEmpty(email) || email.Length > MaxEmailLength)
                return AccountOpResult.NameTooLong;

            if (string.IsNullOrEmpty(username) || username.Length > MaxBnetEmailLength)
                return AccountOpResult.NameTooLong;

            if (string.IsNullOrEmpty(password) || password.Length > MaxBnetPassLength)
                return AccountOpResult.PassTooLong;

            var srpUsername = SRP6.GetSrpUsername(email.ToUpper());
            byte[] salt = SRP6.GenerateSalt();
            byte[] verifier = SRP6.V2SHA256.CreateVerifier(srpUsername, password, salt);

            try
            {

            }
            catch (Exception ex) 
            {
                TrionLogger.Log($"Error creating BnetAccount Messsage :{ex.Message}", "ERROR");
            }
            return AccountOpResult.Ok;

        }
        public static async Task<AccountOpResult> CreateAccount(string username, string password, string email, AppSettings Settings)
        {
            if (string.IsNullOrEmpty(email) || username.Length > MaxAccountLength)
                return AccountOpResult.NameTooLong;

            if (string.IsNullOrEmpty(password) || password.Length > MaxPasswordLength)
                return AccountOpResult.PassTooLong;

            if (string.IsNullOrEmpty(email) || email.Length > MaxEmailLength)
                return AccountOpResult.NameTooLong;

            if (await GetUser(username, Settings) > 0)
                return AccountOpResult.NameAlreadyExist;

            if (await GetEmail(email, Settings) > 0)
                return AccountOpResult.EmailAlreadyExist;

            if (Settings.SelectedCore == Cores.AzerothCore || Settings.SelectedCore == Cores.TrinityCore335 || Settings.SelectedCore == Cores.CMaNGOS || Settings.SelectedCore == Cores.VMaNGOS)
            {
                byte[] salt = SRP6.GenerateSalt();
                byte[] verifier = SRP6.LegecySHA1.CreateVerifier(username, password, salt);
                try
                {
                    await AccessManager.SaveData(SqlQueryManager.CreateAccount(Settings.SelectedCore), new
                    {
                        Username = username,
                        Salt = salt,
                        Verifier = verifier,
                        Email = email,
                        RegMail = email,
                        JoinDate = DateTime.Now,
                    }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                    return AccountOpResult.Ok;
                }
                catch (Exception ex)
                {
                    return AccountOpResult.DBInternalError;
                }
            }
            if (Settings.SelectedCore == Cores.AscEmu)
            {
                try
                {
                    var passhash = AscEmuSHA1.GetPasswordHash(username, password);
                    await AccessManager.SaveData(SqlQueryManager.CreateAccount(Settings.SelectedCore), new
                    {
                        Username = username,
                        EncryptedPassword = passhash,
                        Email = email,
                        JoinDate = DateTime.Now,
                    }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                    return AccountOpResult.Ok;
                }
                catch (Exception ex)
                {

                    return AccountOpResult.DBInternalError;
                }
            }
            if (Settings.SelectedCore == Cores.TrinityCore || Settings.SelectedCore == Cores.CypherCore)
            {
                byte[] salt = SRP6.GenerateSalt();
                byte[] verifier = SRP6.V2SHA256.CreateVerifier(username, password, salt);
                try
                {
                    await AccessManager.SaveData(SqlQueryManager.CreateAccount(Settings.SelectedCore), new
                    {
                        Username = username,
                        Salt = salt,
                        Verifier = verifier,
                        Email = email,
                        RegMail = email,
                        JoinDate = DateTime.Now,
                    }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                    return AccountOpResult.Ok;
                }
                catch (Exception ex)
                {
                    return AccountOpResult.DBInternalError;
                }
            }
            return AccountOpResult.BadLink;
        }
        private static async Task<int> GetUser(string Username, AppSettings Settings)
        {
            return await AccessManager.LoadDataType<int ,dynamic>(SqlQueryManager.GetUserByUsername(Settings.SelectedCore), new { 
                Username 
            },AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
        }
        public static async Task<int> GetEmail(string Email, AppSettings Settings)
        {
            return await AccessManager.LoadDataType<int, dynamic>(SqlQueryManager.GetEmailByEmail(Settings.SelectedCore), new
            {
                Email
            }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
        }
        public static async Task<int> GetUserID(string Username, AppSettings Settings)
        {
            return await AccessManager.LoadDataType<int, dynamic>(SqlQueryManager.GetUserID(Settings.SelectedCore), new
            {
                Username
            }, AccessManager.ConnectionString(Settings , Settings.AuthDatabase));
        }

    }
}
