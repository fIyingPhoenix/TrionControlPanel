// =============================================================================
// AppExecuteManager.cs
// Purpose: Manages starting, stopping, and monitoring of external server processes
// Used by: MainForm, ServerMonitor
// Steps 6, 9, 11, 13 of IMPROVEMENTS.md - XML Docs, Regions, Comments, Events
// =============================================================================

using System.Diagnostics;
using System.Runtime.InteropServices;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Events;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Application
{
    /// <summary>
    /// Manages the execution of external server processes including starting, stopping, and killing.
    /// Handles graceful shutdown using Windows console signals (Ctrl+C).
    /// </summary>
    /// <remarks>
    /// This class uses Windows-specific P/Invoke calls to attach to process consoles
    /// and send control signals. It is not portable to non-Windows platforms.
    ///
    /// Key features:
    /// - Async process start/stop operations
    /// - Graceful shutdown via Ctrl+C signal
    /// - Automatic fallback to Kill() if graceful shutdown times out
    /// - Support for multiple expansion servers (Classic, TBC, WotLK, Cata, MoP)
    /// - Crash detection and automatic restart capability
    /// </remarks>
    public class AppExecuteManager
    {
        #region P/Invoke Declarations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Detaches the calling process from its associated console.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool FreeConsole();

        /// <summary>
        /// Attaches the calling process to the console of a specified process.
        /// </summary>
        /// <param name="dwProcessId">The process ID to attach to.</param>
        /// <returns>True if successful, false otherwise.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        /// <summary>
        /// Adds or removes a handler function for console control events.
        /// </summary>
        /// <param name="handler">The handler delegate (null to use default).</param>
        /// <param name="add">True to add the handler, false to remove it.</param>
        /// <returns>True if successful, false otherwise.</returns>
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handler, bool add);

        /// <summary>
        /// Sends a control signal to a group of console processes.
        /// </summary>
        /// <param name="sigevent">The type of signal to generate.</param>
        /// <param name="dwProcessGroupId">The process group ID (0 for all processes attached to console).</param>
        /// <returns>True if successful, false otherwise.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GenerateConsoleCtrlEvent(Enums.ConsoleCtrlEvent sigevent, uint dwProcessGroupId);

        /// <summary>
        /// Delegate for handling console control events.
        /// </summary>
        /// <param name="CtrlType">The type of control event.</param>
        /// <returns>True if the event was handled, false otherwise.</returns>
        private delegate bool ConsoleCtrlDelegate(Enums.CtrlTypes CtrlType);

        #endregion

        #region Public Methods - General Process Management
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Starts an application asynchronously and returns the process ID.
        /// </summary>
        /// <param name="Application">Full path to the executable file.</param>
        /// <param name="WorkingDirectory">Working directory for the process.</param>
        /// <param name="Name">Friendly name for logging purposes.</param>
        /// <param name="HideWindw">If true, starts with no visible console window.</param>
        /// <param name="Arguments">Optional command-line arguments.</param>
        /// <returns>
        /// The process ID if successful, 0 if the process failed to start.
        /// </returns>
        /// <example>
        /// <code>
        /// int pid = await AppExecuteManager.ApplicationStart(
        ///     @"C:\Server\worldserver.exe",
        ///     @"C:\Server",
        ///     "World Server",
        ///     hideWindow: true,
        ///     arguments: null);
        /// </code>
        /// </example>
        public static async Task<int> ApplicationStart(string Application, string WorkingDirectory, string Name, bool HideWindw, string? Arguments)
        {
            return await StartApplication(Application, WorkingDirectory, Name, HideWindw, Arguments);
        }

        /// <summary>
        /// Forcefully terminates a process by its ID.
        /// </summary>
        /// <param name="ApplicationID">The process ID to kill.</param>
        /// <remarks>
        /// This method immediately terminates the process without allowing it to clean up.
        /// Use <see cref="ApplicationStop"/> for graceful shutdown when possible.
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown if the process ID is invalid.</exception>
        public static void ApplicationKill(int ApplicationID)
        {
            TrionLogger.Log($"Killing application {ApplicationID}");
            var process = Process.GetProcessById(ApplicationID);
            process.Kill(true);
        }

        /// <summary>
        /// Gracefully stops a process by sending a Ctrl+C signal.
        /// Falls back to Kill() if the process doesn't exit within 15 seconds.
        /// </summary>
        /// <param name="ApplicationID">The process ID to stop.</param>
        /// <returns>A task that completes when the process has exited.</returns>
        /// <remarks>
        /// The graceful shutdown process:
        /// 1. Attaches to the target process's console
        /// 2. Disables Ctrl+C handling in the current process
        /// 3. Sends Ctrl+C signal to all processes in the console group
        /// 4. Waits up to 15 seconds for the process to exit
        /// 5. If timeout occurs, forcefully kills the process
        /// </remarks>
        public static async Task ApplicationStop(int ApplicationID)
        {
            TrionLogger.Log($"Stopping Process: {ApplicationID}");
            await Task.Run(async () =>
            {
                try
                {
                    var process = Process.GetProcessById(ApplicationID);
                    await SendCtrlC(process);
                    if (!process.WaitForExit(15000))
                    {
                        process.Kill(true);
                    }
                }
                catch (Exception ex)
                {
                    TrionLogger.Log($"Failed: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Stops multiple applications concurrently.
        /// </summary>
        /// <param name="applicationIds">Collection of process IDs to stop.</param>
        /// <returns>A task that completes when all processes have been stopped.</returns>
        /// <remarks>
        /// All processes are stopped in parallel using Task.WhenAll for efficiency.
        /// Each process receives a graceful shutdown signal via <see cref="ApplicationStop"/>.
        /// </remarks>
        public static async Task StopMultipleApplications(IEnumerable<int> applicationIds)
        {
            var stopTasks = applicationIds.Select(ApplicationStop);
            await Task.WhenAll(stopTasks);
        }

        #endregion

        #region Public Methods - Database Server
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Starts the MySQL database server with the specified settings.
        /// </summary>
        /// <param name="Settings">Application settings containing database configuration.</param>
        /// <param name="argu">Command-line arguments for MySQL (e.g., --console).</param>
        /// <returns>A task that completes when the database has started.</returns>
        /// <remarks>
        /// Clears any existing database process IDs before starting.
        /// Sets <see cref="FormData.UI.Form.DBStarted"/> to true on success.
        /// </remarks>
        public static async Task StartDatabase(AppSettings Settings, string argu)
        {
            SystemData.CleanDatabaseProcessID();
            if (Settings.DBExeLoc != string.Empty)
            {
                int ID = await StartApplication(Settings.DBExeLoc, Settings.DBWorkingDir, Settings.DBExeName, Settings.ConsoleHide, argu);
                SystemData.AddToDatabaseProcessID(new ProcessID() { ID = ID, Name = Settings.DBExeName });
                FormData.UI.Form.DBStarted = true;

                // Raise event to notify UI of database start (Step 13)
                if (ID > 0)
                {
                    AppEvents.RaiseDatabaseStatusChanged(true, ID);
                    AppEvents.NotifySuccess($"Database server started (PID: {ID})");
                }
            }
        }

        /// <summary>
        /// Stops the MySQL database server.
        /// </summary>
        /// <returns>A task that completes when the database has stopped.</returns>
        /// <remarks>
        /// Gracefully stops all database processes and clears process tracking.
        /// Sets <see cref="FormData.UI.Form.DBStarted"/> to false on completion.
        /// </remarks>
        public static async Task StopDatabase()
        {
            try
            {
                var processIds = SystemData.GetDatabaseProcessID().Select(p => p.ID);
                await StopMultipleApplications(processIds);
                SystemData.CleanDatabaseProcessID();
                FormData.UI.Form.DBStarted = false;

                // Raise event to notify UI of database stop (Step 13)
                AppEvents.RaiseDatabaseStatusChanged(false);
                AppEvents.NotifyInfo("Database server stopped");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Failed to stop database: {ex.Message}");
                AppEvents.NotifyError($"Failed to stop database: {ex.Message}");
            }
        }

        #endregion

        #region Public Methods - World Servers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Starts all enabled world servers based on the application settings.
        /// </summary>
        /// <param name="Settings">Application settings containing expansion configurations.</param>
        /// <returns>A task that completes when all enabled world servers have started.</returns>
        /// <remarks>
        /// Checks each expansion's LaunchCore setting and running status before starting.
        /// Supported expansions: Custom, Classic, TBC, WotLK, Cataclysm, MoP.
        /// Records the start time in <see cref="SystemData.WorldStartTime"/> if any server starts.
        /// </remarks>
        public static async Task StartWorld(AppSettings Settings)
        {
            SystemData.ClearWorldProcessIds();
            if (Settings.LaunchCustomCore && !FormData.UI.Form.CustWorldRunning)
            {
                int ID = await StartApplication(Settings.CustomWorldExeLoc, Settings.CustomWorkingDirectory, Settings.CustomWorldExeName, Settings.ConsoleHide, null!);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.CustomWorldExeName });
                FormData.UI.Form.CustWorldStarted = true;
            }
            if (Settings.LaunchClassicCore && !FormData.UI.Form.ClassicWorldRunning)
            {
                int ID = await StartApplication(Settings.ClassicWorldExeLoc, Settings.ClassicWorkingDirectory, Settings.ClassicWorldName, Settings.ConsoleHide, null!);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.ClassicWorldName });
                FormData.UI.Form.ClassicWorldStarted = true;
            }
            if (Settings.LaunchTBCCore && !FormData.UI.Form.TBCWorldRunning)
            {
                int ID = await StartApplication(Settings.TBCWorldExeLoc, Settings.TBCWorkingDirectory, Settings.TBCWorldName, Settings.ConsoleHide, null!);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.TBCWorldName });
                FormData.UI.Form.TBCWorldStarted = true;
            }
            if (Settings.LaunchWotLKCore && !FormData.UI.Form.WotLKWorldRunning)
            {
                int ID = await StartApplication(Settings.WotLKWorldExeLoc, Settings.WotLKWorkingDirectory, Settings.WotLKWorldName, Settings.ConsoleHide, null!);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.WotLKWorldName });
                FormData.UI.Form.WotLKWorldStarted = true;
            }
            if (Settings.LaunchCataCore && !FormData.UI.Form.CataWorldRunning)
            {
                int ID = await StartApplication(Settings.CataWorldExeLoc, Settings.CataWorkingDirectory, Settings.CataWorldName, Settings.ConsoleHide, null!);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.CataWorldName });
                FormData.UI.Form.CataWorldStarted = true;
            }
            if (Settings.LaunchMoPCore && !FormData.UI.Form.MOPWorldRunning)
            {
                int ID = await StartApplication(Settings.MopWorldExeLoc, Settings.MopWorkingDirectory, Settings.MoPWorldName, Settings.ConsoleHide, null);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.MoPWorldName });
                FormData.UI.Form.MOPWorldStarted = true;
            }
            if (SystemData.GetTotalWorldProcessIDCount() > 0)
            {
                SystemData.WorldStartTime = DateTime.Now;

                // Raise event to notify UI of world servers start (Step 13)
                AppEvents.RaiseServerStatusChanged(Enums.ServerType.World, null, true);
                AppEvents.NotifySuccess($"World servers started ({SystemData.GetTotalWorldProcessIDCount()} server(s))");
            }
        }

        /// <summary>
        /// Stops all running world servers.
        /// </summary>
        /// <returns>A task that completes when all world servers have stopped.</returns>
        /// <remarks>
        /// Resets all expansion-specific WorldStarted flags to false.
        /// Clears world process tracking via <see cref="SystemData.ClearWorldProcessIds"/>.
        /// </remarks>
        public static async Task StopWorld()
        {
            var processIds = SystemData.GetWorldProcessesID().Select(p => p.ID);
            await StopMultipleApplications(processIds);
            FormData.UI.Form.CustWorldStarted = false;
            FormData.UI.Form.ClassicWorldStarted = false;
            FormData.UI.Form.TBCWorldStarted = false;
            FormData.UI.Form.WotLKWorldStarted = false;
            FormData.UI.Form.CataWorldStarted = false;
            FormData.UI.Form.MOPWorldStarted = false;
            SystemData.ClearWorldProcessIds();

            // Raise event to notify UI of world servers stop (Step 13)
            AppEvents.RaiseServerStatusChanged(Enums.ServerType.World, null, false);
            AppEvents.NotifyInfo("World servers stopped");
        }

        /// <summary>
        /// Starts a single world server independently of the main start routine.
        /// </summary>
        /// <param name="WorldExecutabel">Full path to the world server executable.</param>
        /// <param name="WorldWorkingDirectory">Working directory for the process.</param>
        /// <param name="WorldExecutabeName">Display name for logging and tracking.</param>
        /// <param name="HideConsole">If true, hides the console window.</param>
        /// <returns>True if the server started successfully, false otherwise.</returns>
        /// <remarks>
        /// Used for starting individual expansion servers or restarting crashed servers.
        /// Adds the process to tracking via <see cref="SystemData.AddToWorldProcessesID"/>.
        /// </remarks>
        public static async Task<bool> StartWorldSeparate(string WorldExecutabel, string WorldWorkingDirectory, string WorldExecutabeName, bool HideConsole)
        {
            try
            {
                int ID = await StartApplication(WorldExecutabel, WorldWorkingDirectory, WorldExecutabeName, HideConsole, null);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = WorldExecutabeName });
                return true;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Failed to start world application: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Public Methods - Logon Servers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Starts all enabled logon/auth servers based on the application settings.
        /// </summary>
        /// <param name="Settings">Application settings containing expansion configurations.</param>
        /// <returns>A task that completes when all enabled logon servers have started.</returns>
        /// <remarks>
        /// Checks each expansion's LaunchCore setting and running status before starting.
        /// Supported expansions: Custom, Classic, TBC, WotLK, Cataclysm, MoP.
        /// Records the start time in <see cref="SystemData.LogonStartTime"/> if any server starts.
        /// </remarks>
        public static async Task StartLogon(AppSettings Settings)
        {
            SystemData.CleanLogonProcessID();
            if (Settings.LaunchCustomCore && !FormData.UI.Form.CustLogonRunning)
            {
                int ID = await StartApplication(Settings.CustomLogonExeLoc, Settings.CustomWorkingDirectory, Settings.CustomWorldExeName, Settings.ConsoleHide, null);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.CustomWorldExeName });
                FormData.UI.Form.CustLogonStarted = true;
            }
            if (Settings.LaunchClassicCore && !FormData.UI.Form.ClassicLogonRunning)
            {
                int ID = await StartApplication(Settings.ClassicLogonExeLoc, Settings.ClassicWorkingDirectory, Settings.ClassicLogonName, Settings.ConsoleHide, null!);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.ClassicLogonName });
                FormData.UI.Form.ClassicLogonStarted = true;
            }
            if (Settings.LaunchTBCCore && !FormData.UI.Form.TBCLogonRunning)
            {
                int ID = await StartApplication(Settings.TBCLogonExeLoc, Settings.TBCWorkingDirectory, Settings.TBCLogonName, Settings.ConsoleHide, null!);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.TBCLogonName });
                FormData.UI.Form.TBCLogonStarted = true;
            }
            if (Settings.LaunchWotLKCore && !FormData.UI.Form.WotLKLogonRunning)
            {
                int ID = await StartApplication(Settings.WotLKLogonExeLoc, Settings.WotLKWorkingDirectory, Settings.WotLKLogonName, Settings.ConsoleHide, null);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.WotLKLogonName });
                FormData.UI.Form.WotLKLogonStarted = true;
            }
            if (Settings.LaunchCataCore && !FormData.UI.Form.CataLogonRunning)
            {
                int ID = await StartApplication(Settings.CataLogonExeLoc, Settings.CataWorkingDirectory, Settings.CataLogonName, Settings.ConsoleHide, null);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.CataLogonName });
                FormData.UI.Form.CataLogonStarted = true;
            }
            if (Settings.LaunchMoPCore && !FormData.UI.Form.MOPLogonRunning)
            {
                int ID = await StartApplication(Settings.MopLogonExeLoc, Settings.MopWorkingDirectory, Settings.MoPLogonName, Settings.ConsoleHide, null);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.MoPLogonName });
                FormData.UI.Form.MOPLogonStarted = true;
            }
            if (SystemData.GetTotalLogonProcessIDCount() > 0)
            {
                SystemData.LogonStartTime = DateTime.Now;

                // Raise event to notify UI of logon servers start (Step 13)
                AppEvents.RaiseServerStatusChanged(Enums.ServerType.Logon, null, true);
                AppEvents.NotifySuccess($"Logon servers started ({SystemData.GetTotalLogonProcessIDCount()} server(s))");
            }
        }

        /// <summary>
        /// Stops all running logon/auth servers.
        /// </summary>
        /// <returns>A task that completes when all logon servers have stopped.</returns>
        /// <remarks>
        /// Resets all expansion-specific LogonStarted flags to false.
        /// Clears logon process tracking via <see cref="SystemData.CleanLogonProcessID"/>.
        /// </remarks>
        public static async Task StopLogon()
        {
            var processIds = SystemData.GetLogonProcessesID().Select(p => p.ID);
            await StopMultipleApplications(processIds);
            FormData.UI.Form.CustLogonStarted = false;
            FormData.UI.Form.ClassicLogonStarted = false;
            FormData.UI.Form.TBCLogonStarted = false;
            FormData.UI.Form.WotLKLogonStarted = false;
            FormData.UI.Form.CataLogonStarted = false;
            FormData.UI.Form.MOPLogonStarted = false;
            SystemData.CleanLogonProcessID();

            // Raise event to notify UI of logon servers stop (Step 13)
            AppEvents.RaiseServerStatusChanged(Enums.ServerType.Logon, null, false);
            AppEvents.NotifyInfo("Logon servers stopped");
        }

        /// <summary>
        /// Starts a single logon server independently of the main start routine.
        /// </summary>
        /// <param name="LogonExecutabel">Full path to the logon server executable.</param>
        /// <param name="LogonWorkingDirectory">Working directory for the process.</param>
        /// <param name="LogonExecutabeName">Display name for logging and tracking.</param>
        /// <param name="HideConsol">If true, hides the console window.</param>
        /// <returns>True if the server started successfully, false otherwise.</returns>
        /// <remarks>
        /// Used for starting individual expansion servers or restarting crashed servers.
        /// Adds the process to tracking via <see cref="SystemData.AddToLogonProcessesID"/>.
        /// </remarks>
        public static async Task<bool> StartLogonSeparate(string LogonExecutabel, string LogonWorkingDirectory, string LogonExecutabeName, bool HideConsol)
        {
            try
            {
                int ID = await StartApplication(LogonExecutabel, LogonWorkingDirectory, LogonExecutabeName, HideConsol, null);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = LogonExecutabeName });
                return true;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Failed to start logon application: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Public Methods - Crash Detection
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks for crashed servers and automatically restarts them.
        /// </summary>
        /// <param name="Settings">Application settings containing server configurations.</param>
        /// <returns>A task that completes when all crashed servers have been restarted.</returns>
        /// <remarks>
        /// A server is considered crashed if:
        /// - It was started by this application (Started = true)
        /// - It is no longer running (Running = false)
        ///
        /// This method should be called periodically (e.g., via a timer) to enable
        /// automatic crash recovery. Each crashed server is logged before restart.
        /// </remarks>
        public static async Task CheckAndRestartServers(AppSettings Settings)
        {
            // World Servers
            if (FormData.UI.Form.CustWorldStarted && !FormData.UI.Form.CustWorldRunning)
            {
                TrionLogger.Log("Crash detected: Custom World. Restarting...");
                await StartWorldSeparate(Settings.CustomWorldExeLoc, Settings.CustomWorkingDirectory, Settings.CustomWorldExeName, Settings.ConsoleHide);
            }
            if (FormData.UI.Form.ClassicWorldStarted && !FormData.UI.Form.ClassicWorldRunning)
            {
                TrionLogger.Log("Crash detected: Classic World. Restarting...");
                await StartWorldSeparate(Settings.ClassicWorldExeLoc, Settings.ClassicWorkingDirectory, Settings.ClassicWorldName, Settings.ConsoleHide);
            }
            if (FormData.UI.Form.TBCWorldStarted && !FormData.UI.Form.TBCWorldRunning)
            {
                TrionLogger.Log("Crash detected: TBC World. Restarting...");
                await StartWorldSeparate(Settings.TBCWorldExeLoc, Settings.TBCWorkingDirectory, Settings.TBCWorldName, Settings.ConsoleHide);
            }
            if (FormData.UI.Form.WotLKWorldStarted && !FormData.UI.Form.WotLKWorldRunning)
            {
                TrionLogger.Log("Crash detected: WotLK World. Restarting...");
                await StartWorldSeparate(Settings.WotLKWorldExeLoc, Settings.WotLKWorkingDirectory, Settings.WotLKWorldName, Settings.ConsoleHide);
            }
            if (FormData.UI.Form.CataWorldStarted && !FormData.UI.Form.CataWorldRunning)
            {
                TrionLogger.Log("Crash detected: Cata World. Restarting...");
                await StartWorldSeparate(Settings.CataWorldExeLoc, Settings.CataWorkingDirectory, Settings.CataWorldName, Settings.ConsoleHide);
            }
            if (FormData.UI.Form.MOPWorldStarted && !FormData.UI.Form.MOPWorldRunning)
            {
                TrionLogger.Log("Crash detected: MoP World. Restarting...");
                await StartWorldSeparate(Settings.MopWorldExeLoc, Settings.MopWorkingDirectory, Settings.MoPWorldName, Settings.ConsoleHide);
            }

            // Logon Servers
            if (FormData.UI.Form.CustLogonStarted && !FormData.UI.Form.CustLogonRunning)
            {
                TrionLogger.Log("Crash detected: Custom Logon. Restarting...");
                await StartLogonSeparate(Settings.CustomLogonExeLoc, Settings.CustomWorkingDirectory, Settings.CustomWorldExeName, Settings.ConsoleHide);
            }
            if (FormData.UI.Form.ClassicLogonStarted && !FormData.UI.Form.ClassicLogonRunning)
            {
                TrionLogger.Log("Crash detected: Classic Logon. Restarting...");
                await StartLogonSeparate(Settings.ClassicLogonExeLoc, Settings.ClassicWorkingDirectory, Settings.ClassicLogonName, Settings.ConsoleHide);
            }
            if (FormData.UI.Form.TBCLogonStarted && !FormData.UI.Form.TBCLogonRunning)
            {
                TrionLogger.Log("Crash detected: TBC Logon. Restarting...");
                await StartLogonSeparate(Settings.TBCLogonExeLoc, Settings.TBCWorkingDirectory, Settings.TBCLogonName, Settings.ConsoleHide);
            }
            if (FormData.UI.Form.WotLKLogonStarted && !FormData.UI.Form.WotLKLogonRunning)
            {
                TrionLogger.Log("Crash detected: WotLK Logon. Restarting...");
                await StartLogonSeparate(Settings.WotLKLogonExeLoc, Settings.WotLKWorkingDirectory, Settings.WotLKLogonName, Settings.ConsoleHide);
            }
            if (FormData.UI.Form.CataLogonStarted && !FormData.UI.Form.CataLogonRunning)
            {
                TrionLogger.Log("Crash detected: Cata Logon. Restarting...");
                await StartLogonSeparate(Settings.CataLogonExeLoc, Settings.CataWorkingDirectory, Settings.CataLogonName, Settings.ConsoleHide);
            }
            if (FormData.UI.Form.MOPLogonStarted && !FormData.UI.Form.MOPLogonRunning)
            {
                TrionLogger.Log("Crash detected: MoP Logon. Restarting...");
                await StartLogonSeparate(Settings.MopLogonExeLoc, Settings.MopWorkingDirectory, Settings.MoPLogonName, Settings.ConsoleHide);
            }
        }

        #endregion

        #region Private Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Internal implementation for starting an application asynchronously.
        /// </summary>
        /// <param name="application">Full path to the executable.</param>
        /// <param name="workingDirectory">Working directory for the process.</param>
        /// <param name="name">Display name for logging.</param>
        /// <param name="hideWindow">If true, creates no visible window.</param>
        /// <param name="arguments">Optional command-line arguments.</param>
        /// <returns>The process ID if successful, 0 if failed.</returns>
        private static async Task<int> StartApplication(string application, string workingDirectory, string name, bool hideWindow, string? arguments)
        {
            int id = 0;
            await Task.Run(() =>
            {
                try
                {
                    using Process myProcess = new();
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = application;
                    myProcess.StartInfo.WorkingDirectory = workingDirectory;
                    if (arguments != null)
                    {
                        myProcess.StartInfo.Arguments = arguments;
                    }
                    myProcess.StartInfo.CreateNoWindow = hideWindow;
                    myProcess.Start();
                    TrionLogger.Log($"Started: {name} with process ID: {myProcess.Id}");
                    id = myProcess.Id;
                }
                catch (Exception ex)
                {
                    // Use enhanced logging with exception details (Step 14)
                    TrionLogger.LogException(ex, $"StartApplication({name})");
                    id = 0;
                }
            });
            return id;
        }

        /// <summary>
        /// Sends a Ctrl+C signal to a process for graceful shutdown.
        /// </summary>
        /// <param name="process">The process to send the signal to.</param>
        /// <returns>A task that completes when the signal has been sent.</returns>
        /// <remarks>
        /// Process:
        /// 1. Attach to the target process's console
        /// 2. Disable Ctrl+C handling in our process (so we don't terminate ourselves)
        /// 3. Send Ctrl+C to all processes in the console group
        /// 4. Wait briefly for the signal to be processed
        /// 5. Detach from the console and restore Ctrl+C handling
        /// </remarks>
        private static async Task SendCtrlC(Process process)
        {
            // Step 1: Attach to target's console
            if (AttachConsole((uint)process.Id))
                TrionLogger.Log($"Send ctrl-C to process {process.Id}");

            // Step 2: Disable Ctrl+C handling in our process
            SetConsoleCtrlHandler(null!, true);

            // Step 3: Send Ctrl+C to all processes in console group
            GenerateConsoleCtrlEvent(Enums.ConsoleCtrlEvent.CTRLC, 0);

            // Step 4: Wait for signal to be processed
            await Task.Delay(1000);

            // Step 5: Cleanup - detach and restore handler
            TrionLogger.Log($"Free console process {process.Id}");
            FreeConsole();
            SetConsoleCtrlHandler(null!, false);
        }

        #endregion
    }
}
