
using System.Diagnostics;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    // Monitors the status of various servers (logon and world) and applications.
    public class ServerMonitor
    {
        // Checks if any logon server has started.
        public static bool ServerStartedLogon()
        {
            return FormData.UI.Form.CustLogonStarted ||
                   FormData.UI.Form.ClassicLogonStarted ||
                   FormData.UI.Form.TBCLogonStarted ||
                   FormData.UI.Form.WotLKLogonStarted ||
                   FormData.UI.Form.CataLogonStarted ||
                   FormData.UI.Form.MOPLogonStarted;
        }

        // Checks if any logon server is running.
        public static bool ServerRunningLogon()
        {
            return FormData.UI.Form.CustLogonStarted ||
                   FormData.UI.Form.ClassicLogonStarted ||
                   FormData.UI.Form.TBCLogonStarted ||
                   FormData.UI.Form.WotLKLogonStarted ||
                   FormData.UI.Form.CataLogonStarted ||
                   FormData.UI.Form.MOPLogonStarted;
        }

        // Checks if any world server has started.
        public static bool ServerStartedWorld()
        {
            return FormData.UI.Form.CustWorldStarted ||
                   FormData.UI.Form.ClassicWorldStarted ||
                   FormData.UI.Form.TBCWorldStarted ||
                   FormData.UI.Form.WotLKWorldStarted ||
                   FormData.UI.Form.CataWorldStarted ||
                   FormData.UI.Form.MOPWorldStarted;
        }

        // Checks if any world server is running.
        public static bool ServerRunningWorld()
        {
            return FormData.UI.Form.CustWorldRunning ||
                   FormData.UI.Form.ClassicWorldRunning ||
                   FormData.UI.Form.TBCWorldRunning ||
                   FormData.UI.Form.WotLKWorldRunning ||
                   FormData.UI.Form.CataWorldRunning ||
                   FormData.UI.Form.MOPWorldRunning;
        }

        // Asynchronously checks if logon servers are running.
        public static async Task ServerRunningLogonAsync()
        {
            var currentRunning = SystemData.GetLogonProcessesID();
            if (currentRunning.Count > 0)
            {
                await Task.WhenAll(currentRunning.Select(item => Task.Run(() =>
                {
                    switch (item.Name)
                    {
                        case "WoW Classic Logon":
                            FormData.UI.Form.ClassicLogonRunning = IsApplicationRunning(item.ID);
                            break;
                        case "The Burning Crusade Logon":
                            FormData.UI.Form.TBCLogonRunning = IsApplicationRunning(item.ID);
                            break;
                        case "Wrath of the Lich King Logon":
                            FormData.UI.Form.WotLKLogonRunning = IsApplicationRunning(item.ID);
                            break;
                        case "Cataclysm Logon":
                            FormData.UI.Form.CataLogonRunning = IsApplicationRunning(item.ID);
                            break;
                        case "Mists of Pandaria Logon":
                            FormData.UI.Form.MOPLogonRunning = IsApplicationRunning(item.ID);
                            break;
                        case "Custom Core":
                            FormData.UI.Form.CustLogonRunning = IsApplicationRunning(item.ID);
                            break;
                    }
                })));
            }
        }

        // Asynchronously checks if world servers are running.
        public static async Task ServerRunningWorldAsync()
        {
            var currentRunning = SystemData.GetWorldProcessesID();
            if (currentRunning.Count > 0)
            {
                await Task.WhenAll(currentRunning.Select(item => Task.Run(() =>
                {
                    switch (item.Name)
                    {
                        case "WoW Classic World":
                            FormData.UI.Form.ClassicWorldRunning = IsApplicationRunning(item.ID);
                            break;
                        case "The Burning Crusade World":
                            FormData.UI.Form.TBCWorldRunning = IsApplicationRunning(item.ID);
                            break;
                        case "Wrath of the Lich King World":
                            FormData.UI.Form.WotLKWorldRunning = IsApplicationRunning(item.ID);
                            break;
                        case "Cataclysm World":
                            FormData.UI.Form.CataWorldRunning = IsApplicationRunning(item.ID);
                            break;
                        case "Mists of Pandaria World":
                            FormData.UI.Form.MOPWorldRunning = IsApplicationRunning(item.ID);
                            break;
                        case "Custom Core":
                            FormData.UI.Form.CustWorldRunning = IsApplicationRunning(item.ID);
                            break;
                    }
                })));
            }
        }
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
                            FormData.UI.Form.DBRunning = IsApplicationRunning(item.ID);
                            break;
                    }
                })));
            }
        }

        // Checks if an application with the specified process ID is running.
        public static bool IsApplicationRunning(int processId)
        {
            try
            {
                Process process = Process.GetProcessById(processId);
                return !process.HasExited;
            }
            catch (ArgumentException)
            {
                return false; // Process with the specified ID does not exist
            }
        }

        // Checks if an application with the specified process name is running.
        public static bool IsApplicationRunningName(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }
    }
}
