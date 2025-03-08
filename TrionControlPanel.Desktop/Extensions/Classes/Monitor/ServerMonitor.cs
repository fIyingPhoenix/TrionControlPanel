
using System.Diagnostics;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;

namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    // Monitors the status of various servers (logon and world) and applications.
    public class ServerMonitor
    {
        // Checks if any logon server has started.
        public static bool ServerStartedLogon()
        {
            if (FormData.UI.Form.CustLogonStarted ||
                FormData.UI.Form.ClassicLogonStarted ||
                FormData.UI.Form.TBCLogonStarted ||
                FormData.UI.Form.WotLKLogonStarted ||
                FormData.UI.Form.CataLogonStarted ||
                FormData.UI.Form.MOPLogonStarted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks if any logon server is running.
        public static bool ServerRunningLogon()
        {
            if (FormData.UI.Form.CustLogonStarted ||
                FormData.UI.Form.ClassicLogonStarted ||
                FormData.UI.Form.TBCLogonStarted ||
                FormData.UI.Form.WotLKLogonStarted ||
                FormData.UI.Form.CataLogonStarted ||
                FormData.UI.Form.MOPLogonStarted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks if any world server has started.
        public static bool ServerStartedWorld()
        {
            if (FormData.UI.Form.CustWorldStarted ||
                FormData.UI.Form.ClassicWorldStarted ||
                FormData.UI.Form.TBCWorldStarted ||
                FormData.UI.Form.WotLKWorldStarted ||
                FormData.UI.Form.CataWorldStarted ||
                FormData.UI.Form.MOPWorldStarted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks if any world server is running.
        public static bool ServerRunningWorld()
        {
            if (FormData.UI.Form.CustWorldRunning ||
                FormData.UI.Form.ClassicWorldRunning ||
                FormData.UI.Form.TBCWorldRunning ||
                FormData.UI.Form.WotLKWorldRunning ||
                FormData.UI.Form.CataWorldRunning ||
                FormData.UI.Form.MOPWorldRunning)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks if an application with the specified process ID is running.
        public static bool IsApplicationRuning(int ProcessId)
        {
            try
            {
                // This checks if a process is still running.
                // If the process is running, HasExited will return false.
                // To indicate that the process is running, you need to invert the result and return true.
                Process process = Process.GetProcessById(ProcessId);
                return !process.HasExited;
            }
            catch (ArgumentException)
            {
                return false; // Process with the specified ID does not exist
            }
        }

        // Checks if an application with the specified process name is running.
        public static bool IsApplicationRunningName(string ProcessName)
        {
            // If the process is running and has the same name as ProcessName
            // it will have a length of 1 or more depending on the process
            Process[] ProcessID = Process.GetProcessesByName(ProcessName);
            if (ProcessID.Length <= 0) // it can't be less than 0 but I just want to be sure
                return false;
            else
                return true;
        }
    }
}
