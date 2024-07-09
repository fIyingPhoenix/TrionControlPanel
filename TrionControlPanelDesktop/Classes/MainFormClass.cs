using System.Net.Sockets;
using TrionControlPanelDesktop.FormData;
using TrionLibrary;

namespace TrionControlPanelDesktop.Classes
{
    public class MainFormClass
    {
        public static void StartDatabase(string argu)
        {
            if (Data.Settings.DBExecutablePath != string.Empty)
            {
                SystemWatcher.ApplicationStart(
                    Data.Settings.DBExecutablePath, 
                    Data.Settings.DBLocation,
                    Data.Settings.DBExecutableName, 
                    Data.Settings.ConsolHide ,
                    argu);
            }
        }
        public static async Task<bool> IsPortOpen()
        {
            string host = "localhost";
            int port = int.Parse(Data.Settings.DBServerPort);
            try
            {
                using TcpClient tcpClient = new();
                await tcpClient.ConnectAsync(host, port);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void StartWorld()
        {     
            if (Data.Settings.CustomInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                    Data.Settings.CustomWorldExeLoc,
                    Data.Settings.CustomWorkingDirectory,
                    Data.Settings.CustomWorldExeName,
                    Data.Settings.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new ProcessID() 
                { ID = ID , Name = Data.Settings.CustomWorldExeName });
            }
            if (Data.Settings.ClassicInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                    Data.Settings.ClassicWorldExeLoc,
                    Data.Settings.ClassicWorkingDirectory,
                    Data.Settings.ClassicWorldExeName,
                    Data.Settings.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new ProcessID() 
                { ID = ID, Name = Data.Settings.ClassicWorldExeName });
            }
            if (Data.Settings.TBCInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                    Data.Settings.TBCWorldExeLoc,
                    Data.Settings.TBCWorkingDirectory,
                    Data.Settings.TBCWorldExeName,
                    Data.Settings.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new ProcessID() 
                { ID = ID, Name = Data.Settings.TBCWorldExeName });
            }
            if (Data.Settings.WotLKInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                     Data.Settings.WotLKWorldExeLoc,
                     Data.Settings.WotLKWorkingDirectory,
                     Data.Settings.WotLKWorldExeName,
                     Data.Settings.ConsolHide,
                     null
                );
                User.System.WorldProcessesID.Add(new ProcessID() 
                { ID = ID, Name = Data.Settings.WotLKWorldExeName });
            }
            if (Data.Settings.CataInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                    Data.Settings.CataWorldExeLoc,
                    Data.Settings.CataWorkingDirectory,
                    Data.Settings.CataWorldExeName,
                    Data.Settings.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new ProcessID() 
                { ID = ID, Name = Data.Settings.CataWorldExeName });
            }
            if (Data.Settings.MOPInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                    Data.Settings.MopWorldExeLoc,
                    Data.Settings.MopWorkingDirectory,
                    Data.Settings.MopWorldExeName,
                    Data.Settings.ConsolHide,
                    null
                    );
                User.System.WorldProcessesID.Add(new ProcessID() 
                { ID = ID, Name = Data.Settings.MopWorldExeName });
            }
        }
        public static void StartLogon()
        {
            if (Data.Settings.CustomInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                    Data.Settings.CustomLogonExeLoc,
                    Data.Settings.CustomWorkingDirectory,
                    Data.Settings.CustomLogonExeName,
                    Data.Settings.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new ProcessID()
                { ID = ID, Name = Data.Settings.CustomLogonExeName });
            }
            if (Data.Settings.ClassicInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                    Data.Settings.ClassicLogonExeLoc,
                    Data.Settings.ClassicWorkingDirectory,
                    Data.Settings.ClassicLogonExeName,
                    Data.Settings.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new ProcessID()
                { ID = ID, Name = Data.Settings.ClassicLogonExeName });
            }
            if (Data.Settings.TBCInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                    Data.Settings.TBCLogonExeLoc,
                    Data.Settings.TBCWorkingDirectory,
                    Data.Settings.TBCLogonExeName,
                    Data.Settings.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new ProcessID()
                { ID = ID, Name = Data.Settings.TBCLogonExeName });
            }
            if (Data.Settings.WotLKInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                     Data.Settings.WotLKLogonExeLoc,
                     Data.Settings.WotLKWorkingDirectory,
                     Data.Settings.WotLKLogonExeName,
                     Data.Settings.ConsolHide,
                     null
                );
                User.System.LogonProcessesID.Add(new ProcessID()
                { ID = ID, Name = Data.Settings.WotLKLogonExeName });
            }
            if (Data.Settings.CataInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                    Data.Settings.CataLogonExeLoc,
                    Data.Settings.CataWorkingDirectory,
                    Data.Settings.CataLogonExeName,
                    Data.Settings.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new ProcessID()
                { ID = ID, Name = Data.Settings.CataLogonExeName });
            }
            if (Data.Settings.MOPInstalled)
            {
                int ID = SystemWatcher.ApplicationStart(
                    Data.Settings.MopLogonExeLoc,
                    Data.Settings.MopWorkingDirectory,
                    Data.Settings.MopLogonExeName,
                    Data.Settings.ConsolHide,
                    null
                    );
                User.System.LogonProcessesID.Add(new ProcessID()
                { ID = ID, Name = Data.Settings.MopLogonExeName });
            }
        }
        public static void StopWorld()
        {
            if (Data.Settings.CustomInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Data.Settings.CustomWorldExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Data.Settings.ClassicInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Data.Settings.ClassicWorldExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Data.Settings.TBCInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Data.Settings.TBCWorldExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Data.Settings.WotLKInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Data.Settings.WotLKWorldExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Data.Settings.CataInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Data.Settings.CataWorldExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Data.Settings.MOPInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Data.Settings.MopWorldExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
        }
        public static void StopLogon()
        {
            if (Data.Settings.CustomInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Data.Settings.CustomLogonExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Data.Settings.ClassicInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Data.Settings.ClassicLogonExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Data.Settings.TBCInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Data.Settings.TBCLogonExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Data.Settings.WotLKInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Data.Settings.WotLKLogonExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Data.Settings.CataInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Data.Settings.CataLogonExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Data.Settings.MOPInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Data.Settings.MopLogonExeName);
                SystemWatcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
        }
    }
}
