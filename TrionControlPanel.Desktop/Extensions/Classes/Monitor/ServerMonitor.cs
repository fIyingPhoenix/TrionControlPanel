// =============================================================================
// ServerMonitor.cs
// Purpose: Monitors server running status by checking process existence
// Dependencies: FormData, SystemData, AppEvents
// Steps 9, 13 of IMPROVEMENTS.md - Region organization, Event-Based UI Updates
// =============================================================================

using System.Diagnostics;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Events;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    /// <summary>
    /// Monitors the status of various servers (logon and world) and applications.
    /// Raises events when server status changes for decoupled UI updates.
    /// </summary>
    public class ServerMonitor
    {
        #region Fields - Status Change Tracking
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Tracks the previous database running state for change detection.
        /// </summary>
        private static bool _previousDbRunning;

        /// <summary>
        /// Tracks the previous world server running state for change detection.
        /// </summary>
        private static bool _previousWorldRunning;

        /// <summary>
        /// Tracks the previous logon server running state for change detection.
        /// </summary>
        private static bool _previousLogonRunning;

        #endregion

        #region Public Methods - Status Checks (Synchronous)
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks if any logon server has been started by this application.
        /// </summary>
        /// <returns>True if any logon server was started, false otherwise.</returns>
        public static bool ServerStartedLogon()
        {
            return FormData.UI.Form.CustLogonStarted ||
                   FormData.UI.Form.ClassicLogonStarted ||
                   FormData.UI.Form.TBCLogonStarted ||
                   FormData.UI.Form.WotLKLogonStarted ||
                   FormData.UI.Form.CataLogonStarted ||
                   FormData.UI.Form.MOPLogonStarted;
        }

        /// <summary>
        /// Checks if any logon server is currently running.
        /// </summary>
        /// <returns>True if any logon server is running, false otherwise.</returns>
        public static bool ServerRunningLogon()
        {
            return FormData.UI.Form.CustLogonRunning ||
                   FormData.UI.Form.ClassicLogonRunning ||
                   FormData.UI.Form.TBCLogonRunning ||
                   FormData.UI.Form.WotLKLogonRunning ||
                   FormData.UI.Form.CataLogonRunning ||
                   FormData.UI.Form.MOPLogonRunning;
        }

        /// <summary>
        /// Checks if any world server has been started by this application.
        /// </summary>
        /// <returns>True if any world server was started, false otherwise.</returns>
        public static bool ServerStartedWorld()
        {
            return FormData.UI.Form.CustWorldStarted ||
                   FormData.UI.Form.ClassicWorldStarted ||
                   FormData.UI.Form.TBCWorldStarted ||
                   FormData.UI.Form.WotLKWorldStarted ||
                   FormData.UI.Form.CataWorldStarted ||
                   FormData.UI.Form.MOPWorldStarted;
        }

        /// <summary>
        /// Checks if any world server is currently running.
        /// </summary>
        /// <returns>True if any world server is running, false otherwise.</returns>
        public static bool ServerRunningWorld()
        {
            return FormData.UI.Form.CustWorldRunning ||
                   FormData.UI.Form.ClassicWorldRunning ||
                   FormData.UI.Form.TBCWorldRunning ||
                   FormData.UI.Form.WotLKWorldRunning ||
                   FormData.UI.Form.CataWorldRunning ||
                   FormData.UI.Form.MOPWorldRunning;
        }

        #endregion

        #region Public Methods - Status Monitoring (Asynchronous)
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks if logon servers are running and raises events for status changes.
        /// </summary>
        /// <returns>A task that completes when the check is finished.</returns>
        /// <remarks>
        /// Updates FormData.UI.Form flags for each expansion's logon server status.
        /// Raises AppEvents.ServerStatusChanged when the overall logon status changes.
        /// </remarks>
        public static async Task ServerRunningLogonAsync()
        {
            var current = SystemData.GetLogonProcessesID();

            await Task.Run(() =>
            {
                var hs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var item in current)
                {
                    if (IsApplicationRunning(item.ID))
                        hs.Add(item.Name);
                }

                FormData.UI.Form.ClassicLogonRunning = hs.Contains("WoW Classic Logon");
                FormData.UI.Form.TBCLogonRunning = hs.Contains("The Burning Crusade Logon");
                FormData.UI.Form.WotLKLogonRunning = hs.Contains("Wrath of the Lich King Logon");
                FormData.UI.Form.CataLogonRunning = hs.Contains("Cataclysm Logon");
                FormData.UI.Form.MOPLogonRunning = hs.Contains("Mists of Pandaria Logon");
                FormData.UI.Form.CustLogonRunning = hs.Contains("Custom Core");

                // Raise event if any logon server status changed (Step 13)
                bool anyLogonRunning = ServerRunningLogon();
                if (anyLogonRunning != _previousLogonRunning)
                {
                    _previousLogonRunning = anyLogonRunning;
                    TrionLogger.Info($"Logon server status changed: Running={anyLogonRunning}");
                    // Raise generic logon status change - UI will check individual expansions
                    AppEvents.RaiseServerStatusChanged(ServerType.Logon, null, anyLogonRunning);
                }
            });
        }

        /// <summary>
        /// Checks if world servers are running and raises events for status changes.
        /// </summary>
        /// <returns>A task that completes when the check is finished.</returns>
        /// <remarks>
        /// Updates FormData.UI.Form flags for each expansion's world server status.
        /// Raises AppEvents.ServerStatusChanged when the overall world status changes.
        /// </remarks>
        public static async Task ServerRunningWorldAsync()
        {
            var current = SystemData.GetWorldProcessesID();

            await Task.Run(() =>
            {
                var hs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var item in current)
                {
                    if (IsApplicationRunning(item.ID))
                        hs.Add(item.Name);
                }

                FormData.UI.Form.ClassicWorldRunning = hs.Contains("WoW Classic World");
                FormData.UI.Form.TBCWorldRunning = hs.Contains("The Burning Crusade World");
                FormData.UI.Form.WotLKWorldRunning = hs.Contains("Wrath of the Lich King World");
                FormData.UI.Form.CataWorldRunning = hs.Contains("Cataclysm World");
                FormData.UI.Form.MOPWorldRunning = hs.Contains("Mists of Pandaria World");
                FormData.UI.Form.CustWorldRunning = hs.Contains("Custom Core");

                // Raise event if any world server status changed (Step 13)
                bool anyWorldRunning = ServerRunningWorld();
                if (anyWorldRunning != _previousWorldRunning)
                {
                    _previousWorldRunning = anyWorldRunning;
                    TrionLogger.Info($"World server status changed: Running={anyWorldRunning}");
                    // Raise generic world status change - UI will check individual expansions
                    AppEvents.RaiseServerStatusChanged(ServerType.World, null, anyWorldRunning);
                }
            });
        }

        /// <summary>
        /// Checks if the database server is running and raises event if status changed.
        /// </summary>
        /// <returns>A task that completes when the check is finished.</returns>
        /// <remarks>
        /// Specifically looks for the "mysqld" process name.
        /// Raises AppEvents.DatabaseStatusChanged when the database status changes.
        /// </remarks>
        public static async Task ServerRunningDatabaseAsync()
        {
            var currentRunning = SystemData.GetDatabaseProcessID();
            if (currentRunning.Count > 0)
            {
                await Task.WhenAll(currentRunning.Select(item => Task.Run(() =>
                {
                    switch (item.Name)
                    {
                        case "mysqld":
                            bool isRunning = IsApplicationRunning(item.ID);
                            FormData.UI.Form.DBRunning = isRunning;

                            // Raise event if status changed (Step 13)
                            if (isRunning != _previousDbRunning)
                            {
                                _previousDbRunning = isRunning;
                                TrionLogger.Info($"Database status changed: Running={isRunning}, PID={item.ID}");
                                AppEvents.RaiseDatabaseStatusChanged(isRunning, item.ID);
                            }
                            break;
                    }
                })));
            }
            else
            {
                // No database processes found - it's not running
                if (_previousDbRunning)
                {
                    _previousDbRunning = false;
                    FormData.UI.Form.DBRunning = false;
                    TrionLogger.Info("Database status changed: Running=False (no processes found)");
                    AppEvents.RaiseDatabaseStatusChanged(false);
                }
            }
        }

        #endregion

        #region Public Methods - Process Utilities
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks if an application with the specified process ID is running.
        /// </summary>
        /// <param name="processId">The process ID to check.</param>
        /// <returns>True if the process exists and has not exited, false otherwise.</returns>
        public static bool IsApplicationRunning(int processId)
        {
            try
            {
                Process process = Process.GetProcessById(processId);
                return !process.HasExited;
            }
            catch (ArgumentException)
            {
                TrionLogger.Debug($"Process {processId} not found");
                return false; // Process with the specified ID does not exist
            }
        }

        /// <summary>
        /// Checks if an application with the specified process name is running.
        /// </summary>
        /// <param name="processName">The process name to search for (without .exe extension).</param>
        /// <returns>True if at least one process with the name exists, false otherwise.</returns>
        public static bool IsApplicationRunningName(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }

        #endregion
    }
}
