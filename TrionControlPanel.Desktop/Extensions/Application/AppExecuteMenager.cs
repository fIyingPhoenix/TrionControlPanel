using System.Diagnostics;
using System.Runtime.InteropServices;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Application
{
    public class AppExecuteMenager
    {
        // PInvoke declarations: Allow interop calls to use native Windows API functions in managed code.

        // Detaches the calling process from its associated console (if any).
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool FreeConsole();

        // Attaches the calling process to the console of a specified process.
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        // Adds/removes a handler to process console control events (e.g., CTRL+C).
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handler, bool add);

        // Sends a control signal (e.g., CTRL+C) to a console process group.
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GenerateConsoleCtrlEvent(Enums.ConsoleCtrlEvent sigevent, uint dwProcessGroupId);

        // Delegate to handle console control events.
        private delegate bool ConsoleCtrlDelegate(Enums.CtrlTypes CtrlType);

        // Starts an application asynchronously and returns the process ID.
        public static async Task<int> ApplicationStart(
            string Application,          // The path to the executable.
            string WorkingDirectory,     // Directory where the app runs.
            string Name,                 // Friendly name for logging.
            bool HideWindow,             // Whether to hide the window.
            string? Arguments            // Additional arguments to pass to the app.
        )
        {
            int id = 0;  // Initialize the process ID to 0.

            // Run the process start logic in a separate thread to avoid blocking the caller.
            await Task.Run(() =>
            {
                try
                {
                    using Process myProcess = new();

                    // Set process configurations.
                    myProcess.StartInfo.UseShellExecute = false;  // Don't use shell to start the app.
                    myProcess.StartInfo.FileName = Application;  // Path to the executable.
                    myProcess.StartInfo.WorkingDirectory = WorkingDirectory;  // Set working directory.

                    if (Arguments != null)
                        myProcess.StartInfo.Arguments = Arguments;  // Pass arguments if provided.

                    // Set window visibility.
                    if (!HideWindow)
                    {
                        myProcess.StartInfo.CreateNoWindow = false;  // Show the window.
                        myProcess.Start();
                    }
                    else
                    {
                        myProcess.StartInfo.CreateNoWindow = true;  // Hide the window.
                        myProcess.Start();
                    }

                    // Log successful start.
                    TrionLogger.Log($"Started: {Name} with process ID: {myProcess.Id}");
                    id = myProcess.Id;
                }
                catch (Exception ex)
                {
                    // Log any error that occurs during the process startup.
                    TrionLogger.Log($"Start failed for {Name}. Error: {ex.Message}", "ERROR");
                    id = 0;  // Set ID to 0 to indicate failure.
                }
            });

            // Return process ID or 0 if failure.
            return id;
        }

        // Terminates a process using its ID.
        public static void ApplicationKill(int ApplicationID)
        {
            TrionLogger.Log($"Killing application {ApplicationID}");
            var process = Process.GetProcessById(ApplicationID);
            process.Kill(true);  // Forcefully kill the process.
        }

        // Stops a process by sending a CTRL+C event.
        public static async Task ApplicationStop(int ApplicationID)
        {
            TrionLogger.Log($"Stopping Process: {ApplicationID}");
            try
            {
                var process = Process.GetProcessById(ApplicationID);
                await SendCtrlC(process);  // Send CTRL+C to the process.

                if (!process.WaitForExit(15000))  // Wait for 15 seconds before forcefully killing it.
                {
                    process.Kill(true);  // Kill process if it doesn't exit in time.
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Failed to stop process {ApplicationID}: {ex.Message}");
            }
        }

        // Sends a CTRL+C signal to the process to gracefully terminate it.
        private static async Task SendCtrlC(Process process)
        {
            if (AttachConsole((uint)process.Id))
            {
                TrionLogger.Log($"Sending CTRL+C to process {process.Id}");
                SetConsoleCtrlHandler(null!, true);  // Ignore CTRL+C in this process.
                GenerateConsoleCtrlEvent(Enums.ConsoleCtrlEvent.CTRLC, 0);  // Send the signal.
                await Task.Delay(1000);  // Allow time for the event to be processed.
                TrionLogger.Log($"Freeing console for process {process.Id}");
                FreeConsole();  // Detach from the console.
                SetConsoleCtrlHandler(null!, false);  // Re-enable CTRL+C handling in this process.
            }
        }

        // Starts the database process and logs its ID.
        public static async Task StartDatabase(AppSettings Settings, string argu)
        {
            SystemData.CleanDatabaseProcessID();
            if (Settings.DBExeLoc != string.Empty)
            {
                int ID = await ApplicationStart(
                    Settings.DBExeLoc,
                    Settings.DBWorkingDir,
                    Settings.DBExeName,
                    Settings.ConsolHide,
                    argu);

                SystemData.AddToDatabaseProcessID(new ProcessID() { ID = ID, Name = Settings.DBExeName });
                FormData.UI.Form.DBStarted = true;
            }
        }

        // Starts either world or logon executables based on a flag.
        public static async Task StartWorldOrLogonExecutable(string exeLocation, string workingDirectory, string exeName, bool hideConsole, bool isWorldExecutable)
        {
            if (isWorldExecutable)
                await StartWorldSeparate(exeLocation, workingDirectory, exeName, hideConsole);
            else
                await StartLogonSeparate(exeLocation, workingDirectory, exeName, hideConsole);
        }

        // Stops the database by terminating all associated processes.
        public static async Task StopDatabase()
        {
            try
            {
                foreach (var process in SystemData.GetDatabaseProcessID())
                {
                    await ApplicationStop(process.ID);
                }
                SystemData.CleanDatabaseProcessID();
                FormData.UI.Form.DBStarted = false;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error stopping database: {ex.Message}");
            }
        }

        // Starts world processes based on different core types.
        public static async Task StartWorld(AppSettings Settings)
        {
            SystemData.CleanWorldProcessID();

            // Start world executables based on the settings and conditions.
            if (Settings.LaunchCustomCore && !FormData.UI.Form.CustWorldRunning)
            {
                int ID = await ApplicationStart(
                    Settings.CustomWorldExeLoc,
                    Settings.CustomWorkingDirectory,
                    Settings.CustomWorldExeName,
                    Settings.ConsolHide,
                    null);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.CustomWorldExeName });
                FormData.UI.Form.CustWorldStarted = true;
            }

            // Repeat the same process for different world core types (Classic, TBC, etc).
            // ... [Additional checks for each core type]
        }

        // Stops all world processes and cleans up.
        public static async Task StopWorld()
        {
            foreach (var process in SystemData.GetWorldProcessesID())
            {
                await ApplicationStop(process.ID);
            }
            // Reset UI flags
            FormData.UI.Form.CustWorldStarted = false;
            // ... [Reset other flags]
            SystemData.CleanWorldProcessID();
        }

        // Starts logon processes based on different core types.
        public static async Task StartLogon(AppSettings Settings)
        {
            SystemData.CleanLogonProcessID();

            // Start logon executables based on the settings and conditions.
            // Similar logic as StartWorld.
            // ...
        }

        // Stops all logon processes and cleans up.
        public static async Task StopLogon()
        {
            foreach (var process in SystemData.GetLogonProcessesID())
            {
                await ApplicationStop(process.ID);
            }
            // Reset UI flags for logon.
            FormData.UI.Form.CustLogonStarted = false;
            // ... [Reset other flags]
            SystemData.CleanLogonProcessID();
        }

        // Helper function to start a world executable separately.
        public static async Task<bool> StartWorldSeparate(string WorldExecutable, string WorldWorkingDirectory, string WorldExecutableName, bool HideConsole)
        {
            try
            {
                int ID = await ApplicationStart(
                    WorldExecutable,
                    WorldWorkingDirectory,
                    WorldExecutableName,
                    HideConsole,
                    null);

                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = WorldExecutableName });
                return true;
            }
            catch (Exception ex)
            {
                return false;  // Return false if an error occurs.
            }
        }

        // Helper function to start a logon executable separately.
        public static async Task<bool> StartLogonSeparate(string LogonExecutable, string LogonWorkingDirectory, string LogonExecutableName, bool HideConsole)
        {
            try
            {
                int ID = await ApplicationStart(
                    LogonExecutable,
                    LogonWorkingDirectory,
                    LogonExecutableName,
                    HideConsole,
                    null);

                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = LogonExecutableName });
                FormData.UI.Form.MOPLogonStarted = true;
                return true;
            }
            catch (Exception ex)
            {
                return false;  // Return false if an error occurs.
            }
        }
    }
}
