
using System.Diagnostics;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;

namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    public class ServerMonitor
    {
        public static bool ServerStartedLogon()
        {
            if (FormData.UI.Form.CustLogonStarted ||
                FormData.UI.Form.ClassicLogonStarted ||
                FormData.UI.Form.TBCLogonStarted ||
                FormData.UI.Form.WotLKLogonStarted ||
                FormData.UI.Form.CataLogonStarted ||
                FormData.UI.Form.MOPLogonStarted)
            { return true; }
            else { return false; }
        }
        public static bool ServerRunningLogon()
        {
            if (FormData.UI.Form.CustLogonStarted ||
                FormData.UI.Form.ClassicLogonStarted ||
                FormData.UI.Form.TBCLogonStarted ||
                FormData.UI.Form.WotLKLogonStarted ||
                FormData.UI.Form.CataLogonStarted ||
                FormData.UI.Form.MOPLogonStarted)
            { return true; }
            else { return false; }
        }
        public static bool ServerStartedWorld()
        {
            if (FormData.UI.Form.CustWorldStarted ||
                FormData.UI.Form.ClassicWorldStarted ||
                FormData.UI.Form.TBCWorldStarted ||
                FormData.UI.Form.WotLKWorldStarted ||
                FormData.UI.Form.CataWorldStarted ||
                FormData.UI.Form.MOPWorldStarted)
            { return true; }
            else { return false; }
        }
        public static bool ServerRunningWorld()
        {
            if (FormData.UI.Form.CustWorldRunning||
                FormData.UI.Form.ClassicWorldRunning ||
                FormData.UI.Form.TBCWorldRunning ||
                FormData.UI.Form.WotLKWorldRunning ||
                FormData.UI.Form.CataWorldRunning ||
                FormData.UI.Form.MOPWorldRunning)
            { return true; }
            else { return false; }
        }
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
        // The same shit as Abow, just by names
        public static bool IsApplicationRunningName(string ProcessName)
        {
           // If the process is running and hve the same name as ProcessName
           // it wil a Lenght of 1 or more depends on the procees
            Process[] ProcessID = Process.GetProcessesByName(ProcessName);
            if (ProcessID.Length <= 0) // it can't be less then 0 but i just want to be sure
                return false;
            else
                return true;
        }
    }
}
