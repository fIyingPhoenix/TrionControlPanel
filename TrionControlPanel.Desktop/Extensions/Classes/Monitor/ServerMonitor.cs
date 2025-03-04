using System.Diagnostics;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;

namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    public class ServerMonitor
    {
        // Check if any logon process has started
        public static bool ServerStartedLogon()
        {
            bool isStarted = FormData.UI.Form.CustLogonStarted ||
                             FormData.UI.Form.ClassicLogonStarted ||
                             FormData.UI.Form.TBCLogonStarted ||
                             FormData.UI.Form.WotLKLogonStarted ||
                             FormData.UI.Form.CataLogonStarted ||
                             FormData.UI.Form.MOPLogonStarted;

            TrionLogger.Log($"ServerStartedLogon: {isStarted}", "INFO");
            return isStarted;
        }

        // Check if any logon process is running
        public static bool ServerRunningLogon()
        {
            bool isRunning = FormData.UI.Form.CustLogonStarted ||
                             FormData.UI.Form.ClassicLogonStarted ||
                             FormData.UI.Form.TBCLogonStarted ||
                             FormData.UI.Form.WotLKLogonStarted ||
                             FormData.UI.Form.CataLogonStarted ||
                             FormData.UI.Form.MOPLogonStarted;

            TrionLogger.Log($"ServerRunningLogon: {isRunning}", "INFO");
            return isRunning;
        }

        // Check if any world process has started
        public static bool ServerStartedWorld()
        {
            bool isStarted = FormData.UI.Form.CustWorldStarted ||
                             FormData.UI.Form.ClassicWorldStarted ||
                             FormData.UI.Form.TBCWorldStarted ||
                             FormData.UI.Form.WotLKWorldStarted ||
                             FormData.UI.Form.CataWorldStarted ||
                             FormData.UI.Form.MOPWorldStarted;

            TrionLogger.Log($"ServerStartedWorld: {isStarted}", "INFO");
            return isStarted;
        }

        // Check if any world process is running
        public static bool ServerRunningWorld()
        {
            bool isRunning = FormData.UI.Form.CustWorldRunning ||
                             FormData.UI.Form.ClassicWorldRunning ||
                             FormData.UI.Form.TBCWorldRunning ||
                             FormData.UI.Form.WotLKWorldRunning ||
                             FormData.UI.Form.CataWorldRunning ||
                             FormData.UI.Form.MOPWorldRunning;

            TrionLogger.Log($"ServerRunningWorld: {isRunning}", "INFO");
            return isRunning;
        }

        // Check if an application with the specified ProcessId is running
        public static bool IsApplicationRunningById(int ProcessId)
        {
            try
            {
                Process process = Process.GetProcessById(ProcessId);
                bool isRunning = !process.HasExited;
                TrionLogger.Log($"IsApplicationRunningById (PID: {ProcessId}): {isRunning}", "INFO");
                return isRunning;
            }
            catch (ArgumentException ex)
            {
                TrionLogger.Log($"Error in IsApplicationRunningById: {ex.Message}", "ERROR");
                return false; // Process does not exist
            }
        }

        // Check if an application with the specified name is running
        public static bool IsApplicationRunningByName(string ProcessName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(ProcessName);
                bool isRunning = processes.Length > 0;
                TrionLogger.Log($"IsApplicationRunningByName (ProcessName: {ProcessName}): {isRunning}", "INFO");
                return isRunning;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error in IsApplicationRunningByName: {ex.Message}", "ERROR");
                return false; // Handle any potential exceptions
            }
        }
    }
}
