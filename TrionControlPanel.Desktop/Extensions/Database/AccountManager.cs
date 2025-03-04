using System.Text;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Cryptography;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    public class AccountManager
    {
        // Constants defining maximum lengths for account fields
        const int MaxAccountLength = 16;
        const int MaxEmailLength = 64;
        const int MaxPasswordLength = 16;
        const int MaxBnetEmailLength = 320;
        const int MaxBnetPassLength = 128;

        // This property stores the result message for account operations
        public static string Message { get; set; }

        // Create a new BattleNet account
        public async static Task<AccountOpResult> CreateBattlenetAccount(string username, string email, string password, AppSettings Settings)
        {
            // Validate email length
            if (string.IsNullOrEmpty(email) || email.Length > MaxEmailLength)
                return AccountOpResult.NameTooLong;

            // Validate username length
            if (string.IsNullOrEmpty(username) || username.Length > MaxBnetEmailLength)
                return AccountOpResult.NameTooLong;

            // Validate password length
            if (string.IsNullOrEmpty(password) || password.Length > MaxBnetPassLength)
                return AccountOpResult.PassTooLong;

            // Generate SRP Username and Verifier
            var srpUsername = SRP6.GetSrpUsername(email.ToUpper());
            byte[] salt = SRP6.GenerateSalt();
            byte[] verifier = SRP6.V2SHA256.CreateVerifier(srpUsername, password, salt);

            try
            {
                // TODO: Add functionality for BattleNet account creation in the database
                return AccountOpResult.Success;
            }
            catch (Exception ex)
            {
                // Log error for debugging
                TrionLogger.Log($"Error creating BnetAccount Message: {ex.Message}", "ERROR");
                return AccountOpResult.DatabaseInternalError;  // Return internal error result
            }
        }

        // Create a new standard account
        public static async Task<AccountOpResult> CreateAccount(string username, string password, string email, AppSettings Settings)
        {
            // Validate account name length
            if (string.IsNullOrEmpty(username) || username.Length > MaxAccountLength)
                return AccountOpResult.NameTooLong;

            // Validate email length
            if (string.IsNullOrEmpty(email) || email.Length > MaxEmailLength)
                return AccountOpResult.NameTooLong;

            // Validate password length
            if (string.IsNullOrEmpty(password) || password.Length > MaxPasswordLength)
                return AccountOpResult.PassTooLong;

            // Check if username already exists
            if (await GetUser(username, Settings) > 0)
                return AccountOpResult.NameAlreadyExists;

            // Check if email already exists
            if (await GetEmail(email, Settings) > 0)
                return AccountOpResult.EmailAlreadyExists;

            // Handle different core types for account creation
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
                    return AccountOpResult.Success;
                }
                catch (Exception ex)
                {
                    // Log exception for troubleshooting
                    TrionLogger.Log($"Error creating account (core type handling): {ex.Message}", "ERROR");
                    return AccountOpResult.DatabaseInternalError;
                }
            }
            else if (Settings.SelectedCore == Cores.AscEmu)
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
                    return AccountOpResult.Success;
                }
                catch (Exception ex)
                {
                    TrionLogger.Log($"Error creating AscEmu account: {ex.Message}", "ERROR");
                    return AccountOpResult.DatabaseInternalError;
                }
            }
            else if (Settings.SelectedCore == Cores.TrinityCore || Settings.SelectedCore == Cores.CypherCore)
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
                    return AccountOpResult.Success;
                }
                catch (Exception ex)
                {
                    TrionLogger.Log($"Error creating TrinityCore/CypherCore account: {ex.Message}", "ERROR");
                    return AccountOpResult.DatabaseInternalError;
                }
            }

            // Return error if no valid core type is found
            return AccountOpResult.BadLink;
        }

        // Helper function to check if a username already exists
        private static async Task<int> GetUser(string Username, AppSettings Settings)
        {
            return await AccessManager.LoadDataType<int, dynamic>(SqlQueryManager.GetUserByUsername(Settings.SelectedCore), new
            {
                Username
            }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
        }

        // Helper function to check if an email already exists
        public static async Task<int> GetEmail(string Email, AppSettings Settings)
        {
            return await AccessManager.LoadDataType<int, dynamic>(SqlQueryManager.GetEmailByEmail(Settings.SelectedCore), new
            {
                Email
            }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
        }

        // Helper function to get a user ID by username
        public static async Task<int> GetUserID(string Username, AppSettings Settings)
        {
            return await AccessManager.LoadDataType<int, dynamic>(SqlQueryManager.GetUserID(Settings.SelectedCore), new
            {
                Username
            }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
        }
    }
}
