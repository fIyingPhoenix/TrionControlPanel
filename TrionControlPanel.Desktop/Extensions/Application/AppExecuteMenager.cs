
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
        // This method detaches the calling process from its associated console, if any.
        // Returns true if the operation succeeds.
        // Returns false if the operation fails (use Marshal.GetLastWin32Error() to retrieve detailed error information).
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool FreeConsole();
        // PInvoke declarations: These are interop calls that allow us to use native Windows API functions in managed code.

        [DllImport("kernel32.dll", SetLastError = true)]
        // Attaches the calling process to the console of a specified process.
        // dwProcessId: The identifier of the process whose console is to be attached.
        private static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll")]
        // Adds or removes an application-defined HandlerRoutine (ConsoleCtrlDelegate) to handle console control events.
        // handler: A delegate to handle control signals (e.g., CTRL+C).
        // add: Set to true to add the handler, or false to remove it.
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handler, bool add);

        [DllImport("kernel32.dll", SetLastError = true)]
        // Sends a control signal (e.g., CTRL+C or CTRL+BREAK) to a console process group.
        // sigevent: The type of control signal to generate.
        // dwProcessGroupId: The identifier of the process group to receive the signal.
        private static extern bool GenerateConsoleCtrlEvent(Enums.ConsoleCtrlEvent sigevent, uint dwProcessGroupId);

        // Delegate that defines a callback to handle console control events.
        // CtrlType: The type of control event (e.g., CTRL+C or CTRL+BREAK).
        private delegate bool ConsoleCtrlDelegate(Enums.CtrlTypes CtrlType);

        // This method starts an application asynchronously and returns the process ID (or 0 in case of failure).
        public static async Task<int> ApplicationStart(
            string Application,          // The full path to the application executable.
            string WorkingDirectory,     // The directory where the application will be executed.
            string Name,                 // A friendly name for the application, used for logging.
            bool HideWindw,              // Whether the application's window should be hidden or not.
            string? Arguments            // Additional arguments to pass to the application.
        )
        {
            int id = 0; // Initialize the process ID to 0.

            // Run the process start logic in a separate thread to avoid blocking the calling thread.
            await Task.Run(async () =>
            {
                try
                {
                    // Create a new process object to start the application.
                    using Process myProcess = new();

                    // Configure the process to not use the shell to start the application.
                    myProcess.StartInfo.UseShellExecute = false;

                    // Set the executable file name for the process.
                    myProcess.StartInfo.FileName = Application;

                    // Set the working directory where the application will be executed.
                    myProcess.StartInfo.WorkingDirectory = WorkingDirectory;

                    // If arguments are provided, pass them to the application.
                    if (Arguments != null)
                    {
                        myProcess.StartInfo.Arguments = Arguments;
                    }

                    // Configure whether the application's window should be hidden or visible.
                    if (HideWindw == false)
                    {
                        myProcess.StartInfo.CreateNoWindow = false; // Show the application's window.
                        myProcess.Start(); // Start the application.
                    }
                    else if (HideWindw == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true; // Hide the application's window.
                        myProcess.Start(); // Start the application.
                    }

                    // Log a message indicating the application has started successfully.
                    await TrionLogger.Log($"Started: {Name} with process ID: {myProcess.Id}");
                    //Infos.Message = $@"Started: {Name}!"; 

                    // Retrieve the process ID of the started application.
                    id = myProcess.Id;
                }
                catch (Exception ex) // Handle any exceptions that occur during process startup.
                {
                    // Log the exception message for debugging or error reporting.
                    // Infos.Message = ex.Message;
                    await TrionLogger.Log($"Started failed {Name} Error Message {ex.Message}", "ERROR");
                    // Set the process ID to 0 to indicate failure.
                    id = 0;
                }
            });

            // Return the process ID (or 0 if the process failed to start).
            return id;
        }
        public static async void ApplicationKill(int ApplicationID)
        {
            await TrionLogger.Log($"Killing application {ApplicationID}");
            var process = Process.GetProcessById(ApplicationID);
            process.Kill(true);// Ensures the process is killed immediately
        }
        public static async Task ApplicationStop(int ApplicationID)
        {
            await TrionLogger.Log($"Stopping Process: {ApplicationID}");
            try
            {
                var process = Process.GetProcessById(ApplicationID);
                await SendCtrlC(process);
                if (!process.WaitForExit(15000)) // wait for 15 seconds, save world!
                {
                    // If the process did not exit, forcefully terminate it
                    //Infos.Message = "The process did not exit for more then 15 seconds. Kill it!";
                    process.Kill(true);
                }
            }
            catch (Exception ex)
            {
                await TrionLogger.Log($"Failed: {ex.Message}");
                // Infos.Message = $"Error: {ex.Message}";
            }
        }
        private static async Task SendCtrlC(Process process)
        {
            // Attach to the process's console
            if (AttachConsole((uint)process.Id))

                await TrionLogger.Log($"Send ctrl-C to process {process.Id}");
            // Set up a control-C event handler to ignore it in this process
            SetConsoleCtrlHandler(null!, true);

            // Send Ctrl+C to the console process group
            GenerateConsoleCtrlEvent(Enums.ConsoleCtrlEvent.CTRLC, 0);

            // Allow some time for the event to be processed
            await Task.Delay(1000);
            await TrionLogger.Log($"Free console process {process.Id}");
            // Detach from the console
            FreeConsole();

            // Re-enable the control-C handling in this process
            SetConsoleCtrlHandler(null!, false);
        }
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
                SystemData.AddToDatabaseProcessID(new ProcessID()
                { ID = ID, Name = Settings.DBExeName });
                FormData.UI.Form.DBStarted = true;
            }
        }
        public static async Task StopDatabase()
        {
            try
            {
                foreach (var Process in SystemData.GetDatabaseProcessID())
                {
                    await ApplicationStop(Process.ID);

                }
                SystemData.CleanDatabaseProcessID();
                FormData.UI.Form.DBStarted = false;
            }
            catch (Exception ex)
            {
                //Infos.Message = ex.Message;
            }
        }
        public static async Task StartWorld(AppSettings Settings)
        {
            SystemData.CleanWolrdProcessID();
            if (Settings.LaunchCustomCore && !FormData.UI.Form.CustWorldRunning)
            {
                int ID = await ApplicationStart(
                    Settings.CustomWorldExeLoc,
                    Settings.CustomWorkingDirectory,
                    Settings.CustomWorldExeName,
                    Settings.ConsolHide,
                    null!
                );
                SystemData.AddToWorldProcessesID(new ProcessID()
                { ID = ID, Name = Settings.CustomWorldExeName });
                FormData.UI.Form.CustWorldStarted = true;
            }
            if (Settings.LaunchClassicCore && !FormData.UI.Form.ClassicWorldRunning)
            {
                int ID = await ApplicationStart(
                    Settings.ClassicWorldExeLoc,
                    Settings.ClassicWorkingDirectory,
                    Settings.ClassicWorldName,
                    Settings.ConsolHide,
                    null!
                );
                SystemData.AddToWorldProcessesID(new ProcessID()
                { ID = ID, Name = Settings.ClassicWorldName });
                FormData.UI.Form.ClassicWorldStarted = true;
            }

            if (Settings.LaunchTBCCore && !FormData.UI.Form.TBCWorldRunning)
            {
                int ID = await ApplicationStart(
                    Settings.TBCWorldExeLoc,
                    Settings.TBCWorkingDirectory,
                    Settings.TBCWorldName,
                    Settings.ConsolHide,
                    null!
                );
                SystemData.AddToWorldProcessesID(new ProcessID()
                { ID = ID, Name = Settings.TBCWorldName });
                FormData.UI.Form.TBCWorldStarted = true;
            }
            if (Settings.LaunchWotLKCore && !FormData.UI.Form.WotLKWorldRunning)
            {
                int ID = await ApplicationStart(
                     Settings.WotLKWorldExeLoc,
                     Settings.WotLKWorkingDirectory,
                     Settings.WotLKWorldName,
                     Settings.ConsolHide,
                     null!
                );
                SystemData.AddToWorldProcessesID(new ProcessID()
                { ID = ID, Name = Settings.WotLKWorldName });
                FormData.UI.Form.WotLKWorldStarted = true;
            }
            if (Settings.LaunchCataCore && !FormData.UI.Form.CataWorldRunning)
            {
                int ID = await ApplicationStart(
                    Settings.CataWorldExeLoc,
                    Settings.CataWorkingDirectory,
                    Settings.CataWorldName,
                    Settings.ConsolHide,
                    null!
                );
                SystemData.AddToWorldProcessesID(new ProcessID()
                { ID = ID, Name = Settings.CataWorldName });
                FormData.UI.Form.CataWorldStarted = true;
            }
            if (Settings.LaunchMoPCore && !FormData.UI.Form.MOPWorldRunning)
            {
                int ID = await ApplicationStart(
                    Settings.MopWorldExeLoc,
                    Settings.MopWorkingDirectory,
                    Settings.MoPWorldName,
                    Settings.ConsolHide,
                    null
                    );
                SystemData.AddToWorldProcessesID(new ProcessID()
                { ID = ID, Name = Settings.MoPWorldName });
                FormData.UI.Form.MOPWorldStarted = true;
            }

            if (SystemData.GetTotalWorldProcessIDCount() > 0)
            {
                SystemData.WorldStartTime = DateTime.Now;
            }
        }
        public static async Task StartLogon(AppSettings Settings)
        {
            SystemData.CleanLogonProcessID();
            if (Settings.LaunchCustomCore && !FormData.UI.Form.CustLogonRunning)
            {
                int ID = await ApplicationStart(
                   Settings.CustomLogonExeLoc,
                   Settings.CustomWorkingDirectory,
                   Settings.CustomWorldExeName,
                   Settings.ConsolHide,
                   null
               );
                SystemData.AddToLogonProcessesID(new ProcessID()
                { ID = ID, Name = Settings.CustomWorldExeName });
                FormData.UI.Form.CustLogonStarted = true;
            }
            if (Settings.LaunchClassicCore && !FormData.UI.Form.ClassicLogonRunning)
            {
                int ID = await ApplicationStart(
                    Settings.ClassicLogonExeLoc,
                    Settings.ClassicWorkingDirectory,
                    Settings.ClassicLogonName,
                    Settings.ConsolHide,
                    null!
                );
                SystemData.AddToLogonProcessesID(new ProcessID()
                { ID = ID, Name = Settings.ClassicLogonName });
                FormData.UI.Form.ClassicLogonStarted = true;
            }
            if (Settings.LaunchTBCCore && !FormData.UI.Form.TBCLogonRunning)
            {
                int ID = await ApplicationStart(
                    Settings.TBCLogonExeLoc,
                    Settings.TBCWorkingDirectory,
                    Settings.TBCLogonName,
                    Settings.ConsolHide,
                    null!
                );
                SystemData.AddToLogonProcessesID(new ProcessID()
                { ID = ID, Name = Settings.TBCLogonName });
                FormData.UI.Form.TBCLogonStarted = true;
            }
            if (Settings.LaunchWotLKCore && !FormData.UI.Form.WotLKLogonRunning)
            {
                int ID = await ApplicationStart(
                     Settings.WotLKLogonExeLoc,
                     Settings.WotLKWorkingDirectory,
                     Settings.WotLKLogonName,
                     Settings.ConsolHide,
                     null
                );
                SystemData.AddToLogonProcessesID(new ProcessID()
                { ID = ID, Name = Settings.WotLKLogonName });
                FormData.UI.Form.WotLKLogonStarted = true;
            }
            if (Settings.LaunchCataCore && !FormData.UI.Form.CataLogonRunning)
            {
                int ID = await ApplicationStart(
                    Settings.CataLogonExeLoc,
                    Settings.CataWorkingDirectory,
                    Settings.CataLogonName,
                    Settings.ConsolHide,
                    null
                );
                SystemData.AddToLogonProcessesID(new ProcessID()
                { ID = ID, Name = Settings.CataLogonName });
                FormData.UI.Form.CataLogonStarted = true;
            }
            if (Settings.LaunchMoPCore && !FormData.UI.Form.MOPLogonRunning)
            {
                int ID = await ApplicationStart(
                    Settings.MopLogonExeLoc,
                    Settings.MopWorkingDirectory,
                    Settings.MoPLogonName,
                    Settings.ConsolHide,
                    null
                    );
                SystemData.AddToLogonProcessesID(new ProcessID()
                { ID = ID, Name = Settings.MoPLogonName });
                FormData.UI.Form.MOPLogonStarted = true;
            }

            if (SystemData.GetTotalLogonProcessIDCount() > 0)
            {
                SystemData.LogonStartTime = DateTime.Now;
            }
        }
        public static async Task StopWorld()
        {
            foreach (var Process in SystemData.GetWorldProcessesID())
            {
                await ApplicationStop(Process.ID);
            }
            FormData.UI.Form.CustWorldStarted = false;
            FormData.UI.Form.ClassicWorldStarted = false;
            FormData.UI.Form.TBCWorldStarted = false;
            FormData.UI.Form.WotLKWorldStarted = false;
            FormData.UI.Form.CataWorldStarted = false;
            FormData.UI.Form.MOPWorldStarted = false;
            SystemData.CleanWolrdProcessID();
        }
        public static async Task StopLogon()
        {
            foreach (var Process in SystemData.GetLogonProcessesID())
            {
                await ApplicationStop(Process.ID);
            }
            FormData.UI.Form.CustLogonStarted = false;
            FormData.UI.Form.ClassicLogonStarted = false;
            FormData.UI.Form.TBCLogonStarted = false;
            FormData.UI.Form.WotLKLogonStarted = false;
            FormData.UI.Form.CataLogonStarted = false;
            FormData.UI.Form.MOPLogonStarted = false;
            SystemData.CleanLogonProcessID();
        }
        public static async Task<bool> StartWorldSeparate(string WorldExecutabel, string WorldWorkingDirectory, string WorldExecutabeName, bool HideConsole)
        {
            try
            {
                int ID = await ApplicationStart(
                    WorldExecutabel,
                    WorldWorkingDirectory,
                    WorldExecutabeName,
                    HideConsole,
                    null
                    );
                SystemData.AddToWorldProcessesID(new ProcessID()
                { ID = ID, Name = WorldExecutabeName });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static async Task<bool> StartLogonSeparate(string LogonExecutabel, string LogonWorkingDirectory, string LogonExecutabeName, bool HideConsol)
        {
            try
            {
                int ID = await ApplicationStart(
                    LogonExecutabel,
                    LogonWorkingDirectory,
                    LogonExecutabeName,
                    HideConsol,
                    null
                    );
                SystemData.AddToLogonProcessesID(new ProcessID()
                { ID = ID, Name = LogonExecutabeName });
                FormData.UI.Form.MOPLogonStarted = true;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

