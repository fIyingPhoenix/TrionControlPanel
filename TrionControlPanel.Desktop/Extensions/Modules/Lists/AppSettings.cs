// =============================================================================
// AppSettings.cs
// Purpose: Application configuration settings persisted to Settings.json
// Step 2 of IMPROVEMENTS.md - Added ExpansionConfig helper methods
// =============================================================================

using Newtonsoft.Json;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Modules.Lists
{
    /// <summary>
    /// Application settings persisted to Settings.json.
    /// Contains configuration for database, expansions, DDNS, and UI preferences.
    /// </summary>
    /// <remarks>
    /// The expansion-specific properties (ClassicWorkingDirectory, TBCWorldExeLoc, etc.)
    /// are kept for backward compatibility with existing Settings.json files.
    /// Use GetExpansionConfig() and SetExpansionConfig() for cleaner code access.
    /// </remarks>
    public class AppSettings
    {
        #region Database Settings (Legacy - for JSON compatibility)
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>World database name.</summary>
        public string WorldDatabase { get; set; } = "wotlk_world";

        /// <summary>Authentication database name.</summary>
        public string AuthDatabase { get; set; } = "wotlk_auth";

        /// <summary>Characters database name.</summary>
        public string CharactersDatabase { get; set; } = "wotlk_characters";

        /// <summary>Hotfix database name (N/A if not used).</summary>
        public string HotfixDatabase { get; set; } = "N/A";

        /// <summary>MySQL executable path.</summary>
        public string DBExeLoc { get; set; } = "N/A";

        /// <summary>MySQL working directory (bin folder).</summary>
        public string DBWorkingDir { get; set; } = "N/A";

        /// <summary>MySQL installation location.</summary>
        public string DBLocation { get; set; } = "N/A";

        /// <summary>Database server hostname.</summary>
        public string DBServerHost { get; set; } = "localhost";

        /// <summary>Database server username.</summary>
        public string DBServerUser { get; set; } = "phoenix";

        /// <summary>Database server password.</summary>
        public string DBServerPassword { get; set; } = "phoenix";

        /// <summary>Database server port.</summary>
        public string DBServerPort { get; set; } = "3306";

        /// <summary>MySQL executable name.</summary>
        public string DBExeName { get; set; } = "mysqld";

        /// <summary>Whether database is installed.</summary>
        public bool DBInstalled { get; set; }

        #endregion

        #region Custom Core Settings (Legacy - for JSON compatibility)
        // ─────────────────────────────────────────────────────────────────────

        public string CustomWorkingDirectory { get; set; } = "";
        public string CustomWorldExeLoc { get; set; } = "";
        public string CustomLogonExeLoc { get; set; } = "";
        public string CustomLogonExeName { get; set; } = "worldserver";
        public string CustomWorldExeName { get; set; } = "authserver";
        public string CustomLogonName { get; set; } = "Custom Core";
        public string CustomWorldName { get; set; } = "Custom Core";
        public bool LaunchCustomCore { get; set; }
        public bool CustomInstalled { get; set; }

        #endregion

        #region Classic Core Settings (Legacy - for JSON compatibility)
        // ─────────────────────────────────────────────────────────────────────

        public string ClassicWorkingDirectory { get; set; } = "";
        public string ClassicWorldExeLoc { get; set; } = "";
        public string ClassicLogonExeLoc { get; set; } = "";
        public string ClassicLogonExeName { get; set; } = "";
        public string ClassicWorldExeName { get; set; } = "";
        public string ClassicWorldName { get; set; } = "";
        public string ClassicLogonName { get; set; } = "";
        public bool LaunchClassicCore { get; set; }
        public bool ClassicInstalled { get; set; }

        #endregion

        #region TBC Core Settings (Legacy - for JSON compatibility)
        // ─────────────────────────────────────────────────────────────────────

        public string TBCWorkingDirectory { get; set; } = "";
        public string TBCWorldExeLoc { get; set; } = "";
        public string TBCLogonExeLoc { get; set; } = "";
        public string TBCLogonExeName { get; set; } = "";
        public string TBCWorldExeName { get; set; } = "";
        public string TBCWorldName { get; set; } = "";
        public string TBCLogonName { get; set; } = "";
        public bool LaunchTBCCore { get; set; }
        public bool TBCInstalled { get; set; }

        #endregion

        #region WotLK Core Settings (Legacy - for JSON compatibility)
        // ─────────────────────────────────────────────────────────────────────

        public string WotLKWorkingDirectory { get; set; } = "";
        public string WotLKWorldExeLoc { get; set; } = "";
        public string WotLKLogonExeLoc { get; set; } = "";
        public string WotLKLogonExeName { get; set; } = "";
        public string WotLKWorldExeName { get; set; } = "";
        public string WotLKWorldName { get; set; } = "";
        public string WotLKLogonName { get; set; } = "";
        public bool LaunchWotLKCore { get; set; }
        public bool WotLKInstalled { get; set; }

        #endregion

        #region Cataclysm Core Settings (Legacy - for JSON compatibility)
        // ─────────────────────────────────────────────────────────────────────

        public string CataWorkingDirectory { get; set; } = "";
        public string CataWorldExeLoc { get; set; } = "";
        public string CataLogonExeLoc { get; set; } = "";
        public string CataLogonExeName { get; set; } = "";
        public string CataWorldExeName { get; set; } = "";
        public string CataWorldName { get; set; } = "";
        public string CataLogonName { get; set; } = "";
        public bool LaunchCataCore { get; set; }
        public bool CataInstalled { get; set; }

        #endregion

        #region MoP Core Settings (Legacy - for JSON compatibility)
        // ─────────────────────────────────────────────────────────────────────

        public string MopWorkingDirectory { get; set; } = "";
        public string MopWorldExeLoc { get; set; } = "";
        public string MopLogonExeLoc { get; set; } = "";
        public string MopLogonExeName { get; set; } = "";
        public string MopWorldExeName { get; set; } = "";
        public string MoPWorldName { get; set; } = "";
        public string MoPLogonName { get; set; } = "";
        public bool LaunchMoPCore { get; set; }
        public bool MOPInstalled { get; set; }

        #endregion

        #region DDNS Settings
        // ─────────────────────────────────────────────────────────────────────

        public string DDNSDomain { get; set; } = "N/A";
        public string DDNSUsername { get; set; } = "N/A";
        public string DDNSPassword { get; set; } = "N/A";
        public int DDNSInterval { get; set; } = 1000;
        public string IPAddress { get; set; } = "";

        #endregion

        #region Trion Settings
        // ─────────────────────────────────────────────────────────────────────

        public TrionTheme TrionTheme { get; set; } = TrionTheme.TrionBlue;
        public string TrionIcon { get; set; } = "Trion New Logo";
        public string TrionLanguage { get; set; } = "en";
        public string SupporterKey { get; set; } = "null";
        public bool AutoUpdateCore { get; set; } = true;
        public bool AutoUpdateTrion { get; set; } = true;
        public bool AutoUpdateDatabase { get; set; } = true;
        public bool NotificationSound { get; set; }
        /// <summary>Gets or sets whether to hide server console windows.</summary>
        public bool ConsoleHide { get; set; }
        public bool StayInTray { get; set; }
        public bool RunWithWindows { get; set; }
        public bool CustomNames { get; set; }
        public bool RunServerWithWindows { get; set; }
        public bool FirstRun { get; set; }
        public bool DDNSRunOnStartup { get; set; }
        public bool ServerCrashDetection { get; set; }
        public Cores SelectedCore { get; set; } = Cores.AzerothCore;
        /// <summary>Gets or sets the selected Dynamic DNS service provider.</summary>
        public DDNSService DDNSService { get; set; } = DDNSService.DuckDNS;
        public SPP SelectedSPP { get; set; } = SPP.WrathOfTheLichKing;
        public Databases SelectedDatabases { get; set; } = Databases.WotLK;

        #endregion

        #region Account Settings
        // ─────────────────────────────────────────────────────────────────────

        public bool CreateBnetAccount { get; set; }

        #endregion

        #region ExpansionConfig Helper Methods (Step 2)
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets the expansion configuration for the specified expansion type.
        /// Provides a cleaner API than accessing individual properties.
        /// </summary>
        /// <param name="expansion">The expansion type to get configuration for.</param>
        /// <returns>An ExpansionConfig object populated from the legacy properties.</returns>
        public ExpansionConfig GetExpansionConfig(SPP expansion)
        {
            return expansion switch
            {
                SPP.Custom => new ExpansionConfig
                {
                    WorkingDirectory = CustomWorkingDirectory,
                    WorldExePath = CustomWorldExeLoc,
                    LogonExePath = CustomLogonExeLoc,
                    WorldExeName = CustomWorldExeName,
                    LogonExeName = CustomLogonExeName,
                    WorldDisplayName = CustomWorldName,
                    LogonDisplayName = CustomLogonName,
                    LaunchOnStartup = LaunchCustomCore,
                    IsInstalled = CustomInstalled
                },
                SPP.Classic => new ExpansionConfig
                {
                    WorkingDirectory = ClassicWorkingDirectory,
                    WorldExePath = ClassicWorldExeLoc,
                    LogonExePath = ClassicLogonExeLoc,
                    WorldExeName = ClassicWorldExeName,
                    LogonExeName = ClassicLogonExeName,
                    WorldDisplayName = ClassicWorldName,
                    LogonDisplayName = ClassicLogonName,
                    LaunchOnStartup = LaunchClassicCore,
                    IsInstalled = ClassicInstalled
                },
                SPP.TheBurningCrusade => new ExpansionConfig
                {
                    WorkingDirectory = TBCWorkingDirectory,
                    WorldExePath = TBCWorldExeLoc,
                    LogonExePath = TBCLogonExeLoc,
                    WorldExeName = TBCWorldExeName,
                    LogonExeName = TBCLogonExeName,
                    WorldDisplayName = TBCWorldName,
                    LogonDisplayName = TBCLogonName,
                    LaunchOnStartup = LaunchTBCCore,
                    IsInstalled = TBCInstalled
                },
                SPP.WrathOfTheLichKing => new ExpansionConfig
                {
                    WorkingDirectory = WotLKWorkingDirectory,
                    WorldExePath = WotLKWorldExeLoc,
                    LogonExePath = WotLKLogonExeLoc,
                    WorldExeName = WotLKWorldExeName,
                    LogonExeName = WotLKLogonExeName,
                    WorldDisplayName = WotLKWorldName,
                    LogonDisplayName = WotLKLogonName,
                    LaunchOnStartup = LaunchWotLKCore,
                    IsInstalled = WotLKInstalled
                },
                SPP.Cataclysm => new ExpansionConfig
                {
                    WorkingDirectory = CataWorkingDirectory,
                    WorldExePath = CataWorldExeLoc,
                    LogonExePath = CataLogonExeLoc,
                    WorldExeName = CataWorldExeName,
                    LogonExeName = CataLogonExeName,
                    WorldDisplayName = CataWorldName,
                    LogonDisplayName = CataLogonName,
                    LaunchOnStartup = LaunchCataCore,
                    IsInstalled = CataInstalled
                },
                SPP.MistsOfPandaria => new ExpansionConfig
                {
                    WorkingDirectory = MopWorkingDirectory,
                    WorldExePath = MopWorldExeLoc,
                    LogonExePath = MopLogonExeLoc,
                    WorldExeName = MopWorldExeName,
                    LogonExeName = MopLogonExeName,
                    WorldDisplayName = MoPWorldName,
                    LogonDisplayName = MoPLogonName,
                    LaunchOnStartup = LaunchMoPCore,
                    IsInstalled = MOPInstalled
                },
                _ => new ExpansionConfig()
            };
        }

        /// <summary>
        /// Sets the expansion configuration for the specified expansion type.
        /// Updates the legacy properties from the ExpansionConfig object.
        /// </summary>
        /// <param name="expansion">The expansion type to set configuration for.</param>
        /// <param name="config">The configuration to apply.</param>
        public void SetExpansionConfig(SPP expansion, ExpansionConfig config)
        {
            switch (expansion)
            {
                case SPP.Custom:
                    CustomWorkingDirectory = config.WorkingDirectory;
                    CustomWorldExeLoc = config.WorldExePath;
                    CustomLogonExeLoc = config.LogonExePath;
                    CustomWorldExeName = config.WorldExeName;
                    CustomLogonExeName = config.LogonExeName;
                    CustomWorldName = config.WorldDisplayName;
                    CustomLogonName = config.LogonDisplayName;
                    LaunchCustomCore = config.LaunchOnStartup;
                    CustomInstalled = config.IsInstalled;
                    break;

                case SPP.Classic:
                    ClassicWorkingDirectory = config.WorkingDirectory;
                    ClassicWorldExeLoc = config.WorldExePath;
                    ClassicLogonExeLoc = config.LogonExePath;
                    ClassicWorldExeName = config.WorldExeName;
                    ClassicLogonExeName = config.LogonExeName;
                    ClassicWorldName = config.WorldDisplayName;
                    ClassicLogonName = config.LogonDisplayName;
                    LaunchClassicCore = config.LaunchOnStartup;
                    ClassicInstalled = config.IsInstalled;
                    break;

                case SPP.TheBurningCrusade:
                    TBCWorkingDirectory = config.WorkingDirectory;
                    TBCWorldExeLoc = config.WorldExePath;
                    TBCLogonExeLoc = config.LogonExePath;
                    TBCWorldExeName = config.WorldExeName;
                    TBCLogonExeName = config.LogonExeName;
                    TBCWorldName = config.WorldDisplayName;
                    TBCLogonName = config.LogonDisplayName;
                    LaunchTBCCore = config.LaunchOnStartup;
                    TBCInstalled = config.IsInstalled;
                    break;

                case SPP.WrathOfTheLichKing:
                    WotLKWorkingDirectory = config.WorkingDirectory;
                    WotLKWorldExeLoc = config.WorldExePath;
                    WotLKLogonExeLoc = config.LogonExePath;
                    WotLKWorldExeName = config.WorldExeName;
                    WotLKLogonExeName = config.LogonExeName;
                    WotLKWorldName = config.WorldDisplayName;
                    WotLKLogonName = config.LogonDisplayName;
                    LaunchWotLKCore = config.LaunchOnStartup;
                    WotLKInstalled = config.IsInstalled;
                    break;

                case SPP.Cataclysm:
                    CataWorkingDirectory = config.WorkingDirectory;
                    CataWorldExeLoc = config.WorldExePath;
                    CataLogonExeLoc = config.LogonExePath;
                    CataWorldExeName = config.WorldExeName;
                    CataLogonExeName = config.LogonExeName;
                    CataWorldName = config.WorldDisplayName;
                    CataLogonName = config.LogonDisplayName;
                    LaunchCataCore = config.LaunchOnStartup;
                    CataInstalled = config.IsInstalled;
                    break;

                case SPP.MistsOfPandaria:
                    MopWorkingDirectory = config.WorkingDirectory;
                    MopWorldExeLoc = config.WorldExePath;
                    MopLogonExeLoc = config.LogonExePath;
                    MopWorldExeName = config.WorldExeName;
                    MopLogonExeName = config.LogonExeName;
                    MoPWorldName = config.WorldDisplayName;
                    MoPLogonName = config.LogonDisplayName;
                    LaunchMoPCore = config.LaunchOnStartup;
                    MOPInstalled = config.IsInstalled;
                    break;
            }
        }

        /// <summary>
        /// Gets all expansion configurations as a dictionary.
        /// Useful for iterating over all expansions.
        /// </summary>
        /// <returns>A dictionary mapping expansion types to their configurations.</returns>
        public Dictionary<SPP, ExpansionConfig> GetAllExpansionConfigs()
        {
            return new Dictionary<SPP, ExpansionConfig>
            {
                { SPP.Custom, GetExpansionConfig(SPP.Custom) },
                { SPP.Classic, GetExpansionConfig(SPP.Classic) },
                { SPP.TheBurningCrusade, GetExpansionConfig(SPP.TheBurningCrusade) },
                { SPP.WrathOfTheLichKing, GetExpansionConfig(SPP.WrathOfTheLichKing) },
                { SPP.Cataclysm, GetExpansionConfig(SPP.Cataclysm) },
                { SPP.MistsOfPandaria, GetExpansionConfig(SPP.MistsOfPandaria) }
            };
        }

        /// <summary>
        /// Gets all installed expansion configurations.
        /// </summary>
        /// <returns>A dictionary of only the installed expansions.</returns>
        public Dictionary<SPP, ExpansionConfig> GetInstalledExpansions()
        {
            return GetAllExpansionConfigs()
                .Where(kvp => kvp.Value.IsInstalled)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        /// <summary>
        /// Gets all expansions configured to launch on startup.
        /// </summary>
        /// <returns>A dictionary of expansions with LaunchOnStartup enabled.</returns>
        public Dictionary<SPP, ExpansionConfig> GetStartupExpansions()
        {
            return GetAllExpansionConfigs()
                .Where(kvp => kvp.Value.LaunchOnStartup && kvp.Value.IsInstalled)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        #endregion

        #region DatabaseConfig Helper Methods (Step 2)
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets the database configuration as a DatabaseConfig object.
        /// Provides a cleaner API than accessing individual DB properties.
        /// </summary>
        /// <returns>A DatabaseConfig object populated from the legacy properties.</returns>
        public DatabaseConfig GetDatabaseConfig()
        {
            return new DatabaseConfig
            {
                Host = DBServerHost,
                Port = DBServerPort,
                Username = DBServerUser,
                Password = DBServerPassword,
                WorldDatabase = WorldDatabase,
                AuthDatabase = AuthDatabase,
                CharactersDatabase = CharactersDatabase,
                HotfixDatabase = HotfixDatabase,
                ExecutablePath = DBExeLoc,
                WorkingDirectory = DBWorkingDir,
                InstallLocation = DBLocation,
                ExecutableName = DBExeName,
                IsInstalled = DBInstalled
            };
        }

        /// <summary>
        /// Sets the database configuration from a DatabaseConfig object.
        /// Updates the legacy properties from the DatabaseConfig.
        /// </summary>
        /// <param name="config">The configuration to apply.</param>
        public void SetDatabaseConfig(DatabaseConfig config)
        {
            DBServerHost = config.Host;
            DBServerPort = config.Port;
            DBServerUser = config.Username;
            DBServerPassword = config.Password;
            WorldDatabase = config.WorldDatabase;
            AuthDatabase = config.AuthDatabase;
            CharactersDatabase = config.CharactersDatabase;
            HotfixDatabase = config.HotfixDatabase;
            DBExeLoc = config.ExecutablePath;
            DBWorkingDir = config.WorkingDirectory;
            DBLocation = config.InstallLocation;
            DBExeName = config.ExecutableName;
            DBInstalled = config.IsInstalled;
        }

        #endregion
    }
}
