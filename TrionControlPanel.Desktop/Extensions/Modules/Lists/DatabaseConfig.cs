// =============================================================================
// DatabaseConfig.cs
// Purpose: Configuration model for MySQL database server settings
// Used by: AppSettings, AccessManager, DatabaseEditor
// Step 2 of IMPROVEMENTS.md - Group database settings
// =============================================================================

namespace TrionControlPanel.Desktop.Extensions.Modules.Lists
{
    /// <summary>
    /// Configuration for the MySQL database server.
    /// Contains connection settings, paths, and database names.
    /// </summary>
    /// <remarks>
    /// This class groups the ~13 database-related properties that were
    /// scattered in AppSettings into a single cohesive configuration object.
    /// </remarks>
    public class DatabaseConfig
    {
        #region Connection Settings
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets the database server hostname or IP address.
        /// Default: "localhost"
        /// </summary>
        public string Host { get; set; } = "localhost";

        /// <summary>
        /// Gets or sets the database server port.
        /// Default: "3306"
        /// </summary>
        public string Port { get; set; } = "3306";

        /// <summary>
        /// Gets or sets the database username for authentication.
        /// Default: "phoenix"
        /// </summary>
        public string Username { get; set; } = "phoenix";

        /// <summary>
        /// Gets or sets the database password for authentication.
        /// Default: "phoenix"
        /// </summary>
        public string Password { get; set; } = "phoenix";

        #endregion

        #region Database Names
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets the name of the world database.
        /// Contains game world data (NPCs, items, quests, etc.)
        /// </summary>
        public string WorldDatabase { get; set; } = "wotlk_world";

        /// <summary>
        /// Gets or sets the name of the authentication database.
        /// Contains account data, realm list, etc.
        /// </summary>
        public string AuthDatabase { get; set; } = "wotlk_auth";

        /// <summary>
        /// Gets or sets the name of the characters database.
        /// Contains player character data, inventories, etc.
        /// </summary>
        public string CharactersDatabase { get; set; } = "wotlk_characters";

        /// <summary>
        /// Gets or sets the name of the hotfix database.
        /// Used by modern cores for client hotfixes. "N/A" if not applicable.
        /// </summary>
        public string HotfixDatabase { get; set; } = "N/A";

        #endregion

        #region Path Settings
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets the full path to the MySQL executable (mysqld).
        /// </summary>
        public string ExecutablePath { get; set; } = "N/A";

        /// <summary>
        /// Gets or sets the working directory for MySQL (bin folder).
        /// </summary>
        public string WorkingDirectory { get; set; } = "N/A";

        /// <summary>
        /// Gets or sets the root location of the MySQL installation.
        /// </summary>
        public string InstallLocation { get; set; } = "N/A";

        /// <summary>
        /// Gets or sets the MySQL executable name.
        /// Default: "mysqld"
        /// </summary>
        public string ExecutableName { get; set; } = "mysqld";

        #endregion

        #region State Properties
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets whether the database server is installed.
        /// </summary>
        public bool IsInstalled { get; set; }

        #endregion

        #region Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Builds a MySQL connection string from the current settings.
        /// </summary>
        /// <param name="database">Optional specific database to connect to.</param>
        /// <returns>A MySQL connection string.</returns>
        public string BuildConnectionString(string? database = null)
        {
            var db = database ?? AuthDatabase;
            return $"Server={Host};Port={Port};Database={db};Uid={Username};Pwd={Password};";
        }

        /// <summary>
        /// Checks if the database configuration is valid for connection.
        /// </summary>
        /// <returns>True if all required connection settings are provided.</returns>
        public bool IsConfigured()
        {
            return !string.IsNullOrEmpty(Host) &&
                   !string.IsNullOrEmpty(Port) &&
                   !string.IsNullOrEmpty(Username);
        }

        /// <summary>
        /// Returns a string representation for debugging.
        /// </summary>
        public override string ToString()
        {
            return $"MySQL @ {Host}:{Port} (Installed: {IsInstalled})";
        }

        #endregion
    }
}
