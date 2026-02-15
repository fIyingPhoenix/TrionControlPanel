// =============================================================================
// RealmListManager.cs
// Purpose: Manages realm list CRUD operations in the authentication database
// Dependencies: AccessManager, SqlQueryManager, TrionLogger
// Step 9 of IMPROVEMENTS.md - Region-based file organization
// =============================================================================

using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    /// <summary>
    /// Manages realm list CRUD operations in the authentication database.
    /// Provides methods to read, create, update, and delete realm entries.
    /// </summary>
    public static class RealmListManager
    {
        #region Public Methods - Read Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Retrieves all realm list entries from the database.
        /// </summary>
        /// <typeparam name="T">The type to deserialize realm data into.</typeparam>
        /// <param name="settings">Application settings containing database configuration.</param>
        /// <returns>A list of realm entries.</returns>
        public static Task<List<T>> GetRealmListsAsync<T>(AppSettings settings)
            => AccessManager.LodaDataList<T, dynamic>(
                SqlQueryManager.GetRealmList(settings.SelectedCore),
                new { },
                AccessManager.ConnectionString(settings, settings.AuthDatabase));

        #endregion

        #region Public Methods - Update Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Updates the address of an existing realm entry.
        /// </summary>
        /// <param name="id">The realm ID to update.</param>
        /// <param name="address">The new address value.</param>
        /// <param name="settings">Application settings containing database configuration.</param>
        /// <returns>
        /// <see cref="RealmListOpResult.Ok"/> if successful,
        /// <see cref="RealmListOpResult.DBInternalError"/> if the operation failed.
        /// </returns>
        public static Task<RealmListOpResult> UpdateRealmListAddressAsync(
            int id, string address, AppSettings settings)
            => ExecuteAsync(
                SqlQueryManager.UpdateRealmListAddress(settings.SelectedCore),
                new { ID = id, Address = address },
                settings,
                $"Address updated  ID:{id}  Core:{settings.SelectedCore}  Address:{address}");

        #endregion

        #region Public Methods - Create Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Creates a new realm entry in the database.
        /// </summary>
        /// <param name="settings">Application settings containing database configuration.</param>
        /// <param name="name">The display name for the realm.</param>
        /// <param name="address">The public IP address or hostname of the realm.</param>
        /// <param name="localAddress">The local/internal IP address of the realm.</param>
        /// <param name="subnetMask">The subnet mask for local address matching.</param>
        /// <param name="port">The port number the world server listens on.</param>
        /// <param name="gameBuild">The game client build number required for this realm.</param>
        /// <returns>
        /// <see cref="RealmListOpResult.Ok"/> if successful,
        /// <see cref="RealmListOpResult.DBInternalError"/> if the operation failed.
        /// </returns>
        public static Task<RealmListOpResult> CreateRealmListAsync(
            AppSettings settings,
            string name,
            string address,
            string localAddress,
            string subnetMask,
            int port,
            int gameBuild)
            => ExecuteAsync(
                SqlQueryManager.CreateRealmList(settings.SelectedCore),
                new
                {
                    Name = name,
                    Address = address,
                    LocalAddress = localAddress,
                    LocalSubnetMask = subnetMask,
                    Port = port,
                    GameBuild = gameBuild
                },
                settings,
                $"Realm created  Name:{name}  Core:{settings.SelectedCore}  Address:{address}");

        #endregion

        #region Public Methods - Delete Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Deletes a realm entry from the database.
        /// </summary>
        /// <param name="settings">Application settings containing database configuration.</param>
        /// <param name="id">The realm ID to delete.</param>
        /// <returns>
        /// <see cref="RealmListOpResult.Ok"/> if successful,
        /// <see cref="RealmListOpResult.DBInternalError"/> if the operation failed.
        /// </returns>
        public static Task<RealmListOpResult> DeleteRealmListAsync(
            AppSettings settings, int id)
            => ExecuteAsync(
                SqlQueryManager.DeleteRealmList(settings.SelectedCore),
                new { ID = id },
                settings,
                $"Realm deleted  ID:{id}  Core:{settings.SelectedCore}");

        #endregion

        #region Private Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Executes a database operation and handles errors consistently.
        /// </summary>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="parameters">The parameters for the query.</param>
        /// <param name="settings">Application settings containing database configuration.</param>
        /// <param name="successMessage">The message to log on successful execution.</param>
        /// <returns>
        /// <see cref="RealmListOpResult.Ok"/> if successful,
        /// <see cref="RealmListOpResult.DBInternalError"/> if an exception occurred.
        /// </returns>
        private static async Task<RealmListOpResult> ExecuteAsync(
            string sql,
            object parameters,
            AppSettings settings,
            string successMessage)
        {
            try
            {
                await AccessManager.SaveData(
                    sql,
                    parameters,
                    AccessManager.ConnectionString(settings, settings.AuthDatabase)).ConfigureAwait(false);

                TrionLogger.LogDatabaseOperation("Execute", "realmlist", true, additionalInfo: successMessage);
                return RealmListOpResult.Ok;
            }
            catch (Exception ex)
            {
                TrionLogger.LogDatabaseOperation("Execute", "realmlist", false, additionalInfo: ex.Message);
                TrionLogger.LogException(ex, "RealmListManager");
                return RealmListOpResult.DBInternalError;
            }
        }

        #endregion
    }
}
