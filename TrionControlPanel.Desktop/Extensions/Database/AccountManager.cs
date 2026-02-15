// =============================================================================
// AccountManager.cs
// Purpose: Manages user and battlenet account creation and queries
// Dependencies: AccessManager, SRP6, SqlQueryManager, TrionLogger
// Step 9 of IMPROVEMENTS.md - Region-based file organization
// =============================================================================

using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Cryptography;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using static TrionControlPanel.Desktop.Extensions.Cryptography.SRP6;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    /// <summary>
    /// Manages user and Battle.net account creation, validation, and queries.
    /// Supports multiple emulator cores with appropriate password hashing algorithms.
    /// </summary>
    public class AccountManager
    {
        #region Constants
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Maximum length for account usernames.
        /// </summary>
        private const int MaxAccountLength = 16;

        /// <summary>
        /// Maximum length for email addresses (standard accounts).
        /// </summary>
        private const int MaxEmailLength = 64;

        /// <summary>
        /// Maximum length for account passwords.
        /// </summary>
        private const int MaxPasswordLength = 16;

        /// <summary>
        /// Maximum length for Battle.net email addresses.
        /// </summary>
        private const int MaxBnetEmailLength = 320;

        /// <summary>
        /// Maximum length for Battle.net passwords.
        /// </summary>
        private const int MaxBnetPassLength = 128;

        #endregion

        #region Public Methods - Account Creation
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Creates a new Battle.net account using SRP6 v2 (SHA256).
        /// </summary>
        /// <param name="username">The account username.</param>
        /// <param name="email">The account email address.</param>
        /// <param name="password">The account password.</param>
        /// <param name="settings">Application settings containing database configuration.</param>
        /// <returns>
        /// <see cref="AccountOpResult.Ok"/> if successful, or an appropriate error code.
        /// </returns>
        /// <remarks>
        /// Battle.net accounts use the email as the SRP6 username (uppercased).
        /// </remarks>
        public static async Task<AccountOpResult> CreateBattlenetAccount(string username, string email, string password, AppSettings settings)
        {
            // --- Validation ---
            if (string.IsNullOrEmpty(email) || email.Length > MaxEmailLength)
                return AccountOpResult.EmailTooLong;

            if (string.IsNullOrEmpty(username) || username.Length > MaxBnetEmailLength)
                return AccountOpResult.NameTooLong;

            if (string.IsNullOrEmpty(password) || password.Length > MaxBnetPassLength)
                return AccountOpResult.PassTooLong;

            // --- Business Logic ---
            try
            {
                // Note: The username for SRP6 should be the email in bnet's case
                var srpUsername = SRP6.GetSrpUsername(email.ToUpper());
                byte[] salt = SRP6.GenerateSalt();
                byte[] verifier = SRP6.V2SHA256.CreateVerifier(srpUsername, password, salt);

                string sql = SqlQueryManager.CreateAccount(settings.SelectedCore);
                var parameters = new
                {
                    Email = email,
                    Username = srpUsername, // The upper-cased email
                    Salt = salt,
                    Verifier = verifier,
                    JoinDate = DateTime.Now
                };

                await AccessManager.SaveData(sql, parameters, AccessManager.ConnectionString(settings, settings.AuthDatabase)).ConfigureAwait(false);

                TrionLogger.Log($"Successfully created Battle.net account for email: {email}", "INFO");
                return AccountOpResult.Ok;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error creating Battle.net account for {email}. Exception: {ex.Message}", "ERROR");
                return AccountOpResult.DBInternalError;
            }
        }

        /// <summary>
        /// Creates a new game account with the appropriate hashing for the selected core.
        /// </summary>
        /// <param name="username">The account username.</param>
        /// <param name="password">The account password.</param>
        /// <param name="email">The account email address.</param>
        /// <param name="settings">Application settings containing database and core configuration.</param>
        /// <returns>
        /// <see cref="AccountOpResult.Ok"/> if successful, or an appropriate error code.
        /// </returns>
        /// <remarks>
        /// Different emulator cores use different password hashing algorithms:
        /// - AzerothCore, TrinityCore335: Legacy SHA1 SRP6
        /// - CMaNGOS, VMaNGOS: CMaNGOS-specific SRP6
        /// - TrinityCore, CypherCore: SRP6 v2 (SHA256)
        /// </remarks>
        public static async Task<AccountOpResult> CreateAccount(string username, string password, string email, AppSettings settings)
        {
            // --- Validation ---
            if (string.IsNullOrEmpty(username) || username.Length > MaxAccountLength)
                return AccountOpResult.NameTooLong;
            if (string.IsNullOrEmpty(password) || password.Length > MaxPasswordLength)
                return AccountOpResult.PassTooLong;
            if (string.IsNullOrEmpty(email) || email.Length > MaxEmailLength)
                return AccountOpResult.EmailTooLong;
            if (await GetUser(username, settings).ConfigureAwait(false) > 0)
                return AccountOpResult.NameAlreadyExist;
            if (await GetEmail(email, settings).ConfigureAwait(false) > 0)
                return AccountOpResult.EmailAlreadyExist;

            // --- Logic and Parameter Preparation ---
            string sql;
            object parameters;

            // Use a switch statement for clarity and better organization
            switch (settings.SelectedCore)
            {
                case Cores.AzerothCore:
                case Cores.TrinityCore335:
                    byte[] legacySalt = SRP6.GenerateSalt();
                    byte[] legacyVerifier = SRP6.LegecySHA1.CreateVerifier(username, password, legacySalt);
                    sql = SqlQueryManager.CreateAccount(settings.SelectedCore);
                    parameters = new
                    {
                        Username = username,
                        Salt = legacySalt,
                        Verifier = legacyVerifier,
                        Email = email,
                        RegMail = email,
                        JoinDate = DateTime.Now,
                    };
                    break;

                case Cores.CMaNGOS:
                case Cores.VMaNGOS:
                    // Use CMaNGOS-specific SRP6 implementation
                    var (saltHex, verifierHex) = CMaNGosSRP6.CreateAccountCredentials(username, password);

                    sql = SqlQueryManager.CreateAccount(settings.SelectedCore);
                    parameters = new
                    {
                        Username = username.ToUpper(), // CMaNGOS stores username in uppercase
                        Salt = saltHex,
                        Verifier = verifierHex,
                        Email = email,
                        RegMail = email,
                        JoinDate = DateTime.Now,
                    };

                    TrionLogger.Log($"CMaNGOS account credentials generated - Salt: {saltHex.Substring(0, 8)}..., Verifier: {verifierHex.Substring(0, 8)}...", "DEBUG");
                    break;

                case Cores.TrinityCore:
                case Cores.CypherCore:
                    byte[] srp6v2Salt = SRP6.GenerateSalt();
                    byte[] srp6v2Verifier = SRP6.V2SHA256.CreateVerifier(username, password, srp6v2Salt);
                    sql = SqlQueryManager.CreateAccount(settings.SelectedCore);
                    parameters = new
                    {
                        Username = username,
                        Salt = srp6v2Salt,
                        Verifier = srp6v2Verifier,
                        Email = email,
                        RegMail = email,
                        JoinDate = DateTime.Now,
                    };
                    break;

                default:
                    // Handle unknown or unsupported cores
                    TrionLogger.Log($"Account creation failed: Unsupported core type '{settings.SelectedCore}'.", "WARNING");
                    return AccountOpResult.BadLink;
            }

            // --- Database Execution (Single point of execution) ---
            try
            {
                await AccessManager.SaveData(sql, parameters, AccessManager.ConnectionString(settings, settings.AuthDatabase)).ConfigureAwait(false);
                TrionLogger.Log($"Successfully created game account '{username}' for core '{settings.SelectedCore}'.", "INFO");
                return AccountOpResult.Ok;
            }
            catch (Exception ex)
            {
                // The most important change: Log the actual error!
                TrionLogger.Log($"Database error creating account '{username}'. Core: {settings.SelectedCore}. Exception: {ex.Message}", "ERROR");
                return AccountOpResult.DBInternalError;
            }
        }

        #endregion

        #region Public Methods - Account Queries
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks if an email already exists in the database.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <param name="settings">Application settings containing database configuration.</param>
        /// <returns>
        /// The count of matching emails (0 if not found, -1 if error occurred).
        /// </returns>
        public static async Task<int> GetEmail(string email, AppSettings settings)
        {
            try
            {
                return await AccessManager.LoadDataType<int, dynamic>(
                    SqlQueryManager.GetEmailByEmail(settings.SelectedCore),
                    new { Email = email },
                    AccessManager.ConnectionString(settings, settings.AuthDatabase)
                ).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"DB check for email '{email}' failed. Exception: {ex.Message}", "ERROR");
                return -1; // Return -1 to indicate an error occurred
            }
        }

        /// <summary>
        /// Gets the ID of a user by their username.
        /// </summary>
        /// <param name="username">The username to look up.</param>
        /// <param name="settings">Application settings containing database configuration.</param>
        /// <returns>
        /// The user ID if found, 0 if not found or error occurred.
        /// </returns>
        public static async Task<int> GetUserID(string username, AppSettings settings)
        {
            try
            {
                return await AccessManager.LoadDataType<int, dynamic>(
                    SqlQueryManager.GetUserID(settings.SelectedCore),
                    new { Username = username },
                    AccessManager.ConnectionString(settings, settings.AuthDatabase)
                ).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"DB lookup for user ID of '{username}' failed. Exception: {ex.Message}", "ERROR");
                return 0; // Return 0 or -1 to indicate failure
            }
        }

        #endregion

        #region Public Methods - Account Management
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Sets the Game Master level for an account.
        /// </summary>
        /// <param name="AccountId">The account ID to modify.</param>
        /// <param name="GmLevel">The GM level to set (0 = player, higher = more privileges).</param>
        /// <param name="RealmID">The realm ID to apply the GM level to (-1 for all realms).</param>
        /// <param name="settings">Application settings containing database configuration.</param>
        /// <returns>
        /// <see cref="AccountOpResult.GMSet"/> if successful, <see cref="AccountOpResult.Faild"/> otherwise.
        /// </returns>
        public static async Task<AccountOpResult> SetGMLevel(int AccountId, int GmLevel, int RealmID, AppSettings settings)
        {
            try
            {
                object parameters = new
                {
                    AccountId,
                    GmLevel,
                    RealmID,
                };
                await AccessManager.SaveData(SqlQueryManager.GrantGmLevel(settings.SelectedCore), parameters, AccessManager.ConnectionString(settings, settings.AuthDatabase)).ConfigureAwait(false);
                return AccountOpResult.GMSet;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Failed to set GM level for account {AccountId}. Exception: {ex.Message}", "ERROR");
                return AccountOpResult.Faild;
            }
        }

        #endregion

        #region Private Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks if a username already exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <param name="settings">Application settings containing database configuration.</param>
        /// <returns>
        /// The count of matching usernames (0 if not found, -1 if error occurred).
        /// </returns>
        private static async Task<int> GetUser(string username, AppSettings settings)
        {
            try
            {
                return await AccessManager.LoadDataType<int, dynamic>(
                    SqlQueryManager.GetUserByUsername(settings.SelectedCore),
                    new { Username = username },
                    AccessManager.ConnectionString(settings, settings.AuthDatabase)
                ).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"DB check for username '{username}' failed. Exception: {ex.Message}", "ERROR");
                return -1; // Return -1 to indicate an error occurred
            }
        }

        #endregion
    }
}
