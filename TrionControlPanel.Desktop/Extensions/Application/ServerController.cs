// =============================================================================
// ServerController.cs
// Purpose: Centralized controller for all server operations (database, world, logon)
// Used by: MainForm, background services
// Steps 8, 9 of IMPROVEMENTS.md - Server Control Logic, Region-based organization
// =============================================================================

using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Application
{
    /// <summary>
    /// Centralized controller for all server operations.
    /// Manages database, world, and logon servers for all expansions.
    /// </summary>
    /// <remarks>
    /// This class provides a unified API for server management, abstracting away
    /// the complexity of handling multiple expansions. It uses the ExpansionConfig
    /// model to work with expansions generically instead of having separate code
    /// for each expansion.
    ///
    /// Key features:
    /// - Start/stop database server with MySQL configuration
    /// - Start/stop world and logon servers for specific or all expansions
    /// - Automatic crash detection and restart
    /// - Status tracking through FormData and SystemData
    ///
    /// Example usage:
    /// <code>
    /// var controller = new ServerController(settings);
    /// await controller.StartDatabaseAsync("--console");
    /// await controller.StartExpansionAsync(SPP.WrathOfTheLichKing, ServerType.World);
    /// await controller.StartAllEnabledServersAsync();
    /// </code>
    /// </remarks>
    public class ServerController
    {
        #region Fields
        // ─────────────────────────────────────────────────────────────────────

        private readonly AppSettings _settings;

        #endregion

        #region Constructors
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Initializes a new instance of the ServerController.
        /// </summary>
        /// <param name="settings">Application settings containing server configurations.</param>
        /// <exception cref="ArgumentNullException">Thrown if settings is null.</exception>
        public ServerController(AppSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        #endregion

        #region Database Server Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Starts the MySQL database server.
        /// </summary>
        /// <param name="arguments">Command-line arguments for MySQL (e.g., "--console").</param>
        /// <returns>True if the database started successfully.</returns>
        /// <remarks>
        /// Sets <see cref="SystemData.DatabaseStartTime"/> on successful start.
        /// The database process ID is tracked in SystemData for monitoring.
        /// </remarks>
        public async Task<bool> StartDatabaseAsync(string? arguments = null)
        {
            if (FormData.UI.Form.DBRunning || FormData.UI.Form.DBStarted)
            {
                TrionLogger.Log("Database is already running or started.");
                return true;
            }

            if (string.IsNullOrEmpty(_settings.DBExeLoc))
            {
                TrionLogger.Log("Database executable path not configured.", "WARNING");
                return false;
            }

            SystemData.DatabaseStartTime = DateTime.Now;
            await AppExecuteManager.StartDatabase(_settings, arguments ?? "");

            TrionLogger.Log("Database server started.");
            return FormData.UI.Form.DBStarted;
        }

        /// <summary>
        /// Stops the MySQL database server.
        /// </summary>
        /// <returns>A task that completes when the database has stopped.</returns>
        public async Task StopDatabaseAsync()
        {
            if (!FormData.UI.Form.DBStarted && !FormData.UI.Form.DBRunning)
            {
                TrionLogger.Log("Database is not running.");
                return;
            }

            await AppExecuteManager.StopDatabase();
            TrionLogger.Log("Database server stopped.");
        }

        /// <summary>
        /// Gets whether the database server is currently running.
        /// </summary>
        public bool IsDatabaseRunning => FormData.UI.Form.DBRunning && FormData.UI.Form.DBStarted;

        #endregion

        #region World Server Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Starts all enabled world servers based on settings.
        /// </summary>
        /// <returns>A task that completes when all servers have been started.</returns>
        public async Task StartAllWorldServersAsync()
        {
            await AppExecuteManager.StartWorld(_settings);
            TrionLogger.Log("All enabled world servers started.");
        }

        /// <summary>
        /// Stops all running world servers.
        /// </summary>
        /// <returns>A task that completes when all servers have stopped.</returns>
        public async Task StopAllWorldServersAsync()
        {
            await AppExecuteManager.StopWorld();
            TrionLogger.Log("All world servers stopped.");
        }

        /// <summary>
        /// Starts the world server for a specific expansion.
        /// </summary>
        /// <param name="expansion">The expansion to start.</param>
        /// <returns>True if the server started successfully.</returns>
        public async Task<bool> StartWorldServerAsync(SPP expansion)
        {
            var config = _settings.GetExpansionConfig(expansion);

            if (!config.IsInstalled || string.IsNullOrEmpty(config.WorldExePath))
            {
                TrionLogger.Log($"World server for {expansion} is not configured.", "WARNING");
                return false;
            }

            if (IsWorldRunning(expansion))
            {
                TrionLogger.Log($"World server for {expansion} is already running.");
                return true;
            }

            bool success = await AppExecuteManager.StartWorldSeparate(
                config.WorldExePath,
                config.WorkingDirectory,
                config.WorldExeName,
                _settings.ConsoleHide);

            if (success)
            {
                SetWorldStarted(expansion, true);
                TrionLogger.Log($"World server for {expansion} started.");
            }

            return success;
        }

        /// <summary>
        /// Gets whether any world server is currently running.
        /// </summary>
        public bool IsAnyWorldRunning => ServerMonitor.ServerRunningWorld();

        /// <summary>
        /// Gets whether any world server has been started.
        /// </summary>
        public bool IsAnyWorldStarted => ServerMonitor.ServerStartedWorld();

        #endregion

        #region Logon Server Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Starts all enabled logon servers based on settings.
        /// </summary>
        /// <returns>A task that completes when all servers have been started.</returns>
        public async Task StartAllLogonServersAsync()
        {
            await AppExecuteManager.StartLogon(_settings);
            TrionLogger.Log("All enabled logon servers started.");
        }

        /// <summary>
        /// Stops all running logon servers.
        /// </summary>
        /// <returns>A task that completes when all servers have stopped.</returns>
        public async Task StopAllLogonServersAsync()
        {
            await AppExecuteManager.StopLogon();
            TrionLogger.Log("All logon servers stopped.");
        }

        /// <summary>
        /// Starts the logon server for a specific expansion.
        /// </summary>
        /// <param name="expansion">The expansion to start.</param>
        /// <returns>True if the server started successfully.</returns>
        public async Task<bool> StartLogonServerAsync(SPP expansion)
        {
            var config = _settings.GetExpansionConfig(expansion);

            if (!config.IsInstalled || string.IsNullOrEmpty(config.LogonExePath))
            {
                TrionLogger.Log($"Logon server for {expansion} is not configured.", "WARNING");
                return false;
            }

            if (IsLogonRunning(expansion))
            {
                TrionLogger.Log($"Logon server for {expansion} is already running.");
                return true;
            }

            bool success = await AppExecuteManager.StartLogonSeparate(
                config.LogonExePath,
                config.WorkingDirectory,
                config.LogonExeName,
                _settings.ConsoleHide);

            if (success)
            {
                SetLogonStarted(expansion, true);
                TrionLogger.Log($"Logon server for {expansion} started.");
            }

            return success;
        }

        /// <summary>
        /// Gets whether any logon server is currently running.
        /// </summary>
        public bool IsAnyLogonRunning => ServerMonitor.ServerRunningLogon();

        /// <summary>
        /// Gets whether any logon server has been started.
        /// </summary>
        public bool IsAnyLogonStarted => ServerMonitor.ServerStartedLogon();

        #endregion

        #region Combined Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Starts all enabled servers (world and logon) for all configured expansions.
        /// </summary>
        /// <returns>A task that completes when all servers have been started.</returns>
        public async Task StartAllEnabledServersAsync()
        {
            await StartAllWorldServersAsync();
            await StartAllLogonServersAsync();
        }

        /// <summary>
        /// Stops all running servers (world and logon).
        /// </summary>
        /// <returns>A task that completes when all servers have stopped.</returns>
        public async Task StopAllServersAsync()
        {
            await StopAllWorldServersAsync();
            await StopAllLogonServersAsync();
        }

        /// <summary>
        /// Starts both world and logon servers for a specific expansion.
        /// </summary>
        /// <param name="expansion">The expansion to start.</param>
        /// <returns>True if both servers started successfully.</returns>
        public async Task<bool> StartExpansionAsync(SPP expansion)
        {
            bool worldStarted = await StartWorldServerAsync(expansion);
            bool logonStarted = await StartLogonServerAsync(expansion);
            return worldStarted && logonStarted;
        }

        /// <summary>
        /// Starts servers for all installed and enabled expansions.
        /// </summary>
        /// <returns>A list of expansions that were successfully started.</returns>
        public async Task<List<SPP>> StartInstalledExpansionsAsync()
        {
            var startedExpansions = new List<SPP>();
            var installedExpansions = _settings.GetStartupExpansions();

            foreach (var kvp in installedExpansions)
            {
                bool success = await StartExpansionAsync(kvp.Key);
                if (success)
                {
                    startedExpansions.Add(kvp.Key);
                }
            }

            return startedExpansions;
        }

        #endregion

        #region Crash Detection
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks for crashed servers and restarts them if crash detection is enabled.
        /// </summary>
        /// <returns>A task that completes when crash checking is done.</returns>
        /// <remarks>
        /// This method should be called periodically (e.g., via a timer) when
        /// <see cref="AppSettings.ServerCrashDetection"/> is enabled.
        /// </remarks>
        public async Task CheckAndRestartCrashedServersAsync()
        {
            if (!_settings.ServerCrashDetection)
            {
                return;
            }

            await AppExecuteManager.CheckAndRestartServers(_settings);
        }

        #endregion

        #region Status Helpers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks if the world server for a specific expansion is running.
        /// </summary>
        /// <param name="expansion">The expansion to check.</param>
        /// <returns>True if the world server is running.</returns>
        public bool IsWorldRunning(SPP expansion)
        {
            return expansion switch
            {
                SPP.Custom => FormData.UI.Form.CustWorldRunning,
                SPP.Classic => FormData.UI.Form.ClassicWorldRunning,
                SPP.TheBurningCrusade => FormData.UI.Form.TBCWorldRunning,
                SPP.WrathOfTheLichKing => FormData.UI.Form.WotLKWorldRunning,
                SPP.Cataclysm => FormData.UI.Form.CataWorldRunning,
                SPP.MistsOfPandaria => FormData.UI.Form.MOPWorldRunning,
                _ => false
            };
        }

        /// <summary>
        /// Checks if the logon server for a specific expansion is running.
        /// </summary>
        /// <param name="expansion">The expansion to check.</param>
        /// <returns>True if the logon server is running.</returns>
        public bool IsLogonRunning(SPP expansion)
        {
            return expansion switch
            {
                SPP.Custom => FormData.UI.Form.CustLogonRunning,
                SPP.Classic => FormData.UI.Form.ClassicLogonRunning,
                SPP.TheBurningCrusade => FormData.UI.Form.TBCLogonRunning,
                SPP.WrathOfTheLichKing => FormData.UI.Form.WotLKLogonRunning,
                SPP.Cataclysm => FormData.UI.Form.CataLogonRunning,
                SPP.MistsOfPandaria => FormData.UI.Form.MOPLogonRunning,
                _ => false
            };
        }

        /// <summary>
        /// Checks if the world server for a specific expansion was started.
        /// </summary>
        /// <param name="expansion">The expansion to check.</param>
        /// <returns>True if the world server was started.</returns>
        public bool IsWorldStarted(SPP expansion)
        {
            return expansion switch
            {
                SPP.Custom => FormData.UI.Form.CustWorldStarted,
                SPP.Classic => FormData.UI.Form.ClassicWorldStarted,
                SPP.TheBurningCrusade => FormData.UI.Form.TBCWorldStarted,
                SPP.WrathOfTheLichKing => FormData.UI.Form.WotLKWorldStarted,
                SPP.Cataclysm => FormData.UI.Form.CataWorldStarted,
                SPP.MistsOfPandaria => FormData.UI.Form.MOPWorldStarted,
                _ => false
            };
        }

        /// <summary>
        /// Checks if the logon server for a specific expansion was started.
        /// </summary>
        /// <param name="expansion">The expansion to check.</param>
        /// <returns>True if the logon server was started.</returns>
        public bool IsLogonStarted(SPP expansion)
        {
            return expansion switch
            {
                SPP.Custom => FormData.UI.Form.CustLogonStarted,
                SPP.Classic => FormData.UI.Form.ClassicLogonStarted,
                SPP.TheBurningCrusade => FormData.UI.Form.TBCLogonStarted,
                SPP.WrathOfTheLichKing => FormData.UI.Form.WotLKLogonStarted,
                SPP.Cataclysm => FormData.UI.Form.CataLogonStarted,
                SPP.MistsOfPandaria => FormData.UI.Form.MOPLogonStarted,
                _ => false
            };
        }

        /// <summary>
        /// Gets the server status for all expansions.
        /// </summary>
        /// <returns>Dictionary mapping expansions to their server status.</returns>
        public Dictionary<SPP, ExpansionServerStatus> GetAllServerStatus()
        {
            var status = new Dictionary<SPP, ExpansionServerStatus>();

            foreach (SPP expansion in Enum.GetValues<SPP>())
            {
                status[expansion] = new ExpansionServerStatus
                {
                    Expansion = expansion,
                    WorldStarted = IsWorldStarted(expansion),
                    WorldRunning = IsWorldRunning(expansion),
                    LogonStarted = IsLogonStarted(expansion),
                    LogonRunning = IsLogonRunning(expansion)
                };
            }

            return status;
        }

        #endregion

        #region Private Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Sets the world started flag for a specific expansion.
        /// </summary>
        private static void SetWorldStarted(SPP expansion, bool started)
        {
            switch (expansion)
            {
                case SPP.Custom:
                    FormData.UI.Form.CustWorldStarted = started;
                    break;
                case SPP.Classic:
                    FormData.UI.Form.ClassicWorldStarted = started;
                    break;
                case SPP.TheBurningCrusade:
                    FormData.UI.Form.TBCWorldStarted = started;
                    break;
                case SPP.WrathOfTheLichKing:
                    FormData.UI.Form.WotLKWorldStarted = started;
                    break;
                case SPP.Cataclysm:
                    FormData.UI.Form.CataWorldStarted = started;
                    break;
                case SPP.MistsOfPandaria:
                    FormData.UI.Form.MOPWorldStarted = started;
                    break;
            }
        }

        /// <summary>
        /// Sets the logon started flag for a specific expansion.
        /// </summary>
        private static void SetLogonStarted(SPP expansion, bool started)
        {
            switch (expansion)
            {
                case SPP.Custom:
                    FormData.UI.Form.CustLogonStarted = started;
                    break;
                case SPP.Classic:
                    FormData.UI.Form.ClassicLogonStarted = started;
                    break;
                case SPP.TheBurningCrusade:
                    FormData.UI.Form.TBCLogonStarted = started;
                    break;
                case SPP.WrathOfTheLichKing:
                    FormData.UI.Form.WotLKLogonStarted = started;
                    break;
                case SPP.Cataclysm:
                    FormData.UI.Form.CataLogonStarted = started;
                    break;
                case SPP.MistsOfPandaria:
                    FormData.UI.Form.MOPLogonStarted = started;
                    break;
            }
        }

        #endregion
    }

    #region Supporting Types
    // ─────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Represents the server status for a single expansion.
    /// </summary>
    public class ExpansionServerStatus
    {
        /// <summary>
        /// The expansion this status applies to.
        /// </summary>
        public SPP Expansion { get; set; }

        /// <summary>
        /// Whether the world server was started by this application.
        /// </summary>
        public bool WorldStarted { get; set; }

        /// <summary>
        /// Whether the world server process is currently running.
        /// </summary>
        public bool WorldRunning { get; set; }

        /// <summary>
        /// Whether the logon server was started by this application.
        /// </summary>
        public bool LogonStarted { get; set; }

        /// <summary>
        /// Whether the logon server process is currently running.
        /// </summary>
        public bool LogonRunning { get; set; }

        /// <summary>
        /// Gets whether the world server has crashed (started but not running).
        /// </summary>
        public bool WorldCrashed => WorldStarted && !WorldRunning;

        /// <summary>
        /// Gets whether the logon server has crashed (started but not running).
        /// </summary>
        public bool LogonCrashed => LogonStarted && !LogonRunning;

        /// <summary>
        /// Gets a summary string of the server status.
        /// </summary>
        public override string ToString()
        {
            return $"{Expansion}: World={WorldRunning}, Logon={LogonRunning}";
        }
    }

    #endregion
}
