// =============================================================================
// ExpansionConfig.cs
// Purpose: Reusable configuration model for a single WoW expansion's server setup
// Used by: AppSettings, MainForm, ServerController
// Step 2 of IMPROVEMENTS.md - Eliminate property duplication
// =============================================================================

namespace TrionControlPanel.Desktop.Extensions.Modules.Lists
{
    /// <summary>
    /// Configuration for a single WoW expansion's server setup.
    /// Contains paths, executable names, display names, and launch preferences.
    /// </summary>
    /// <remarks>
    /// This class replaces the 9 repeated properties that were duplicated
    /// for each expansion (Classic, TBC, WotLK, Cata, MoP, Custom) in AppSettings.
    /// Using this model reduces ~54 properties to 6 ExpansionConfig instances.
    /// </remarks>
    public class ExpansionConfig
    {
        #region Directory Properties
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets the root directory containing server files for this expansion.
        /// Example: "C:\Servers\WotLK" or "/wotlk"
        /// </summary>
        public string WorkingDirectory { get; set; } = "";

        #endregion

        #region Executable Paths
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets the full path to the world server executable.
        /// Example: "C:\Servers\WotLK\worldserver.exe"
        /// </summary>
        public string WorldExePath { get; set; } = "";

        /// <summary>
        /// Gets or sets the full path to the logon/auth server executable.
        /// Example: "C:\Servers\WotLK\authserver.exe"
        /// </summary>
        public string LogonExePath { get; set; } = "";

        #endregion

        #region Executable Names
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets the world server executable filename (without path).
        /// Example: "worldserver" or "worldserver.exe"
        /// </summary>
        public string WorldExeName { get; set; } = "";

        /// <summary>
        /// Gets or sets the logon server executable filename (without path).
        /// Example: "authserver" or "authserver.exe"
        /// </summary>
        public string LogonExeName { get; set; } = "";

        #endregion

        #region Display Names
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets the display name for the world server.
        /// Used in UI labels and process identification.
        /// Example: "Wrath of the Lich King World"
        /// </summary>
        public string WorldDisplayName { get; set; } = "";

        /// <summary>
        /// Gets or sets the display name for the logon server.
        /// Used in UI labels and process identification.
        /// Example: "Wrath of the Lich King Logon"
        /// </summary>
        public string LogonDisplayName { get; set; } = "";

        #endregion

        #region State Properties
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets whether this expansion's servers should launch on application startup.
        /// </summary>
        public bool LaunchOnStartup { get; set; }

        /// <summary>
        /// Gets or sets whether this expansion is installed and ready to use.
        /// </summary>
        public bool IsInstalled { get; set; }

        #endregion

        #region Constructors
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Initializes a new instance with default empty values.
        /// </summary>
        public ExpansionConfig()
        {
        }

        /// <summary>
        /// Initializes a new instance with the specified display names.
        /// </summary>
        /// <param name="worldDisplayName">Display name for the world server.</param>
        /// <param name="logonDisplayName">Display name for the logon server.</param>
        public ExpansionConfig(string worldDisplayName, string logonDisplayName)
        {
            WorldDisplayName = worldDisplayName;
            LogonDisplayName = logonDisplayName;
        }

        #endregion

        #region Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks if this expansion has valid configuration for launching servers.
        /// </summary>
        /// <returns>True if both executables and working directory are configured.</returns>
        public bool IsConfigured()
        {
            return !string.IsNullOrEmpty(WorkingDirectory) &&
                   !string.IsNullOrEmpty(WorldExePath) &&
                   !string.IsNullOrEmpty(LogonExePath);
        }

        /// <summary>
        /// Resets all configuration to default empty values.
        /// </summary>
        public void Reset()
        {
            WorkingDirectory = "";
            WorldExePath = "";
            LogonExePath = "";
            WorldExeName = "";
            LogonExeName = "";
            WorldDisplayName = "";
            LogonDisplayName = "";
            LaunchOnStartup = false;
            IsInstalled = false;
        }

        /// <summary>
        /// Creates a copy of this configuration.
        /// </summary>
        public ExpansionConfig Clone()
        {
            return new ExpansionConfig
            {
                WorkingDirectory = WorkingDirectory,
                WorldExePath = WorldExePath,
                LogonExePath = LogonExePath,
                WorldExeName = WorldExeName,
                LogonExeName = LogonExeName,
                WorldDisplayName = WorldDisplayName,
                LogonDisplayName = LogonDisplayName,
                LaunchOnStartup = LaunchOnStartup,
                IsInstalled = IsInstalled
            };
        }

        /// <summary>
        /// Returns a string representation for debugging.
        /// </summary>
        public override string ToString()
        {
            return $"{WorldDisplayName} (Installed: {IsInstalled}, Launch: {LaunchOnStartup})";
        }

        #endregion
    }
}
