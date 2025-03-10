
using System.Diagnostics;
using System.Runtime.InteropServices;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Application
{
    // Manages the execution of applications, including starting, stopping, and killing processes.
    public class AppExecuteMenager
    {
        // Detaches the calling process from its associated console.
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool FreeConsole();

        // Attaches the calling process to the console of a specified process.
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        // Adds or removes a handler to handle console control events.
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handler, bool add);

        // Sends a control signal to a console process group.
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GenerateConsoleCtrlEvent(Enums.ConsoleCtrlEvent sigevent, uint dwProcessGroupId);

        // Delegate that defines a callback to handle console control events.
        private delegate bool ConsoleCtrlDelegate(Enums.CtrlTypes CtrlType);

        // Starts an application asynchronously and returns the process ID.
        public static async Task<int> ApplicationStart(string Application, string WorkingDirectory, string Name, bool HideWindw, string? Arguments)
        {
            return await StartApplication(Application, WorkingDirectory, Name, HideWindw, Arguments);
        }

        // Helper method to start an application asynchronously and return the process ID.
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
                    TrionLogger.Log($"Started failed {name} Error Message {ex.Message}", "ERROR");
                    id = 0;
                }
            });
            return id;
        }

        // Kills the application with the specified process ID.
        public static void ApplicationKill(int ApplicationID)
        {
            TrionLogger.Log($"Killing application {ApplicationID}");
            var process = Process.GetProcessById(ApplicationID);
            process.Kill(true);
        }

        // Stops the application with the specified process ID.
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

        // Sends a Ctrl+C signal to the specified process.
        private static async Task SendCtrlC(Process process)
        {
            if (AttachConsole((uint)process.Id))
                TrionLogger.Log($"Send ctrl-C to process {process.Id}");
            SetConsoleCtrlHandler(null!, true);
            GenerateConsoleCtrlEvent(Enums.ConsoleCtrlEvent.CTRLC, 0);
            await Task.Delay(1000);
            TrionLogger.Log($"Free console process {process.Id}");
            FreeConsole();
            SetConsoleCtrlHandler(null!, false);
        }

        // Stops multiple applications concurrently.
        public static async Task StopMultipleApplications(IEnumerable<int> applicationIds)
        {
            var stopTasks = applicationIds.Select(ApplicationStop);
            await Task.WhenAll(stopTasks);
        }

        // Starts the database application with the specified settings and arguments.
        public static async Task StartDatabase(AppSettings Settings, string argu)
        {
            SystemData.CleanDatabaseProcessID();
            if (Settings.DBExeLoc != string.Empty)
            {
                int ID = await StartApplication(Settings.DBExeLoc, Settings.DBWorkingDir, Settings.DBExeName, Settings.ConsolHide, argu);
                SystemData.AddToDatabaseProcessID(new ProcessID() { ID = ID, Name = Settings.DBExeName });
                FormData.UI.Form.DBStarted = true;
            }
        }

        // Stops the database application.
        public static async Task StopDatabase()
        {
            try
            {
                var processIds = SystemData.GetDatabaseProcessID().Select(p => p.ID);
                await StopMultipleApplications(processIds);
                SystemData.CleanDatabaseProcessID();
                FormData.UI.Form.DBStarted = false;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Failed to stop database: {ex.Message}");
            }
        }

        // Starts the world applications based on the specified settings.
        public static async Task StartWorld(AppSettings Settings)
        {
            SystemData.CleanWolrdProcessID();
            if (Settings.LaunchCustomCore && !FormData.UI.Form.CustWorldRunning)
            {
                int ID = await StartApplication(Settings.CustomWorldExeLoc, Settings.CustomWorkingDirectory, Settings.CustomWorldExeName, Settings.ConsolHide, null!);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.CustomWorldExeName });
                FormData.UI.Form.CustWorldStarted = true;
            }
            if (Settings.LaunchClassicCore && !FormData.UI.Form.ClassicWorldRunning)
            {
                int ID = await StartApplication(Settings.ClassicWorldExeLoc, Settings.ClassicWorkingDirectory, Settings.ClassicWorldName, Settings.ConsolHide, null!);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.ClassicWorldName });
                FormData.UI.Form.ClassicWorldStarted = true;
            }
            if (Settings.LaunchTBCCore && !FormData.UI.Form.TBCWorldRunning)
            {
                int ID = await StartApplication(Settings.TBCWorldExeLoc, Settings.TBCWorkingDirectory, Settings.TBCWorldName, Settings.ConsolHide, null!);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.TBCWorldName });
                FormData.UI.Form.TBCWorldStarted = true;
            }
            if (Settings.LaunchWotLKCore && !FormData.UI.Form.WotLKWorldRunning)
            {
                int ID = await StartApplication(Settings.WotLKWorldExeLoc, Settings.WotLKWorkingDirectory, Settings.WotLKWorldName, Settings.ConsolHide, null!);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.WotLKWorldName });
                FormData.UI.Form.WotLKWorldStarted = true;
            }
            if (Settings.LaunchCataCore && !FormData.UI.Form.CataWorldRunning)
            {
                int ID = await StartApplication(Settings.CataWorldExeLoc, Settings.CataWorkingDirectory, Settings.CataWorldName, Settings.ConsolHide, null!);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.CataWorldName });
                FormData.UI.Form.CataWorldStarted = true;
            }
            if (Settings.LaunchMoPCore && !FormData.UI.Form.MOPWorldRunning)
            {
                int ID = await StartApplication(Settings.MopWorldExeLoc, Settings.MopWorkingDirectory, Settings.MoPWorldName, Settings.ConsolHide, null);
                SystemData.AddToWorldProcessesID(new ProcessID() { ID = ID, Name = Settings.MoPWorldName });
                FormData.UI.Form.MOPWorldStarted = true;
            }
            if (SystemData.GetTotalWorldProcessIDCount() > 0)
            {
                SystemData.WorldStartTime = DateTime.Now;
            }
        }

        // Starts the logon applications based on the specified settings.
        public static async Task StartLogon(AppSettings Settings)
        {
            SystemData.CleanLogonProcessID();
            if (Settings.LaunchCustomCore && !FormData.UI.Form.CustLogonRunning)
            {
                int ID = await StartApplication(Settings.CustomLogonExeLoc, Settings.CustomWorkingDirectory, Settings.CustomWorldExeName, Settings.ConsolHide, null);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.CustomWorldExeName });
                FormData.UI.Form.CustLogonStarted = true;
            }
            if (Settings.LaunchClassicCore && !FormData.UI.Form.ClassicLogonRunning)
            {
                int ID = await StartApplication(Settings.ClassicLogonExeLoc, Settings.ClassicWorkingDirectory, Settings.ClassicLogonName, Settings.ConsolHide, null!);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.ClassicLogonName });
                FormData.UI.Form.ClassicLogonStarted = true;
            }
            if (Settings.LaunchTBCCore && !FormData.UI.Form.TBCLogonRunning)
            {
                int ID = await StartApplication(Settings.TBCLogonExeLoc, Settings.TBCWorkingDirectory, Settings.TBCLogonName, Settings.ConsolHide, null!);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.TBCLogonName });
                FormData.UI.Form.TBCLogonStarted = true;
            }
            if (Settings.LaunchWotLKCore && !FormData.UI.Form.WotLKLogonRunning)
            {
                int ID = await StartApplication(Settings.WotLKLogonExeLoc, Settings.WotLKWorkingDirectory, Settings.WotLKLogonName, Settings.ConsolHide, null);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.WotLKLogonName });
                FormData.UI.Form.WotLKLogonStarted = true;
            }
            if (Settings.LaunchCataCore && !FormData.UI.Form.CataLogonRunning)
            {
                int ID = await StartApplication(Settings.CataLogonExeLoc, Settings.CataWorkingDirectory, Settings.CataLogonName, Settings.ConsolHide, null);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.CataLogonName });
                FormData.UI.Form.CataLogonStarted = true;
            }
            if (Settings.LaunchMoPCore && !FormData.UI.Form.MOPLogonRunning)
            {
                int ID = await StartApplication(Settings.MopLogonExeLoc, Settings.MopWorkingDirectory, Settings.MoPLogonName, Settings.ConsolHide, null);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = Settings.MoPLogonName });
                FormData.UI.Form.MOPLogonStarted = true;
            }
            if (SystemData.GetTotalLogonProcessIDCount() > 0)
            {
                SystemData.LogonStartTime = DateTime.Now;
            }
        }

        // Stops all world applications.
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
            SystemData.CleanWolrdProcessID();
        }

        // Stops all logon applications.
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
        }

        // Starts a world application separately.
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

        // Starts a logon application separately.
        public static async Task<bool> StartLogonSeparate(string LogonExecutabel, string LogonWorkingDirectory, string LogonExecutabeName, bool HideConsol)
        {
            try
            {
                int ID = await StartApplication(LogonExecutabel, LogonWorkingDirectory, LogonExecutabeName, HideConsol, null);
                SystemData.AddToLogonProcessesID(new ProcessID() { ID = ID, Name = LogonExecutabeName });
                FormData.UI.Form.MOPLogonStarted = true;
                return true;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Failed to start logon application: {ex.Message}");
                return false;
            }
        }
    }
}

