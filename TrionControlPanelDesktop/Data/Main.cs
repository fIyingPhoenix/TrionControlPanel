using System.Net.Sockets;
using System.Reflection;
using TrionControlPanelDesktop.Controls;
using TrionLibrary.Models;
using TrionLibrary.Setting;
using TrionLibrary.Sys;


namespace TrionControlPanelDesktop.Data
{
    internal class Attempt
    {
        public static int CustomLogon { get; set; }
        public static int CustomWorld { get; set; }
        public static int ClassicLogon { get; set; }
        public static int ClassicWorld { get; set; }
        public static int TBCLogon { get; set; }
        public static int TBCWorld { get; set; }
        public static int WotlkLogon { get; set; }
        public static int WotlkWorld { get; set; }
        public static int CataLogon { get; set; }
        public static int CataWorld { get; set; }
        public static int MopLogon { get; set; }
        public static int MopWorld { get; set; }
        public static int Database { get; set; }
    }
    public class Main
    {
        public static async Task LoadVersions()
        {
            //Version Load
            User.UI.Version.OFF.Trion = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
            User.UI.Version.OFF.Database = Infos.Version.Local(Setting.List.DBExeLoc);
            User.UI.Version.OFF.Classic = Infos.Version.Local(Setting.List.ClassicWorldExeLoc);
            User.UI.Version.OFF.TBC = Infos.Version.Local(Setting.List.TBCWorldExeLoc);
            User.UI.Version.OFF.WotLK = Infos.Version.Local(Setting.List.WotLKWorldExeLoc);
            User.UI.Version.OFF.Cata = Infos.Version.Local(Setting.List.CataWorldExeLoc);
            User.UI.Version.OFF.Mop = Infos.Version.Local(Setting.List.MopWorldExeLoc);
            User.UI.Version.ON.Trion = await Infos.Version.Online($"{Links.MainCDNHost}{Links.Version.Trion}");
            User.UI.Version.ON.Database = await Infos.Version.Online($"{Links.MainCDNHost}{Links.Version.Database}");
            User.UI.Version.ON.Classic = await Infos.Version.Online($"{Links.MainCDNHost}{Links.Version.Classic}");
            User.UI.Version.ON.TBC = await Infos.Version.Online($"{Links.MainCDNHost}{Links.Version.TBC}");
            User.UI.Version.ON.WotLK = await Infos.Version.Online($"{Links.MainCDNHost}{Links.Version.WotLK}");
            User.UI.Version.ON.Cata = await Infos.Version.Online($"{Links.MainCDNHost}{Links.Version.Cata}");
            User.UI.Version.ON.Mop = await Infos.Version.Online($"{Links.MainCDNHost}{Links.Version.Mop}");
        }
        public static async Task StartUpdate(string Directory, string WebLink, bool startDownload)
        {
            var progress = new Progress<string>(value => { });
            await Task.Run(async () => await DownloadControl.CompareAndExportChangesOnline(Directory, WebLink, progress, startDownload));
        }
        private static int VersionCompare(string ver1, string ver2)
        {
            if (ver1 != "N/A" && ver2 != "N/A")
            {
                Version version1 = new(ver1);
                Version version2 = new(ver2);
                int comparisonResult = version1.CompareTo(version2);
                return comparisonResult;
                //string[] vComps1 = ver1.Split('.');
                //string[] vComps2 = ver2.Split('.');
                //int[] vNumb1 = Array.ConvertAll(vComps1, int.Parse);
                //int[] vNumb2 = Array.ConvertAll(vComps2, int.Parse);

                //for (int i = 0; i < Math.Min(vNumb1.Length, vNumb2.Length); i++)
                //{
                //    if (vNumb1[i] != vNumb2[i])
                //    {
                //        return vNumb1[i].CompareTo(vNumb2[i]);
                //    }
                //}
                //return vNumb1.Length.CompareTo(vNumb2.Length);
            }
            return 0;
        }
        public static async Task CheckForUpdate()
        {
            await LoadVersions();
            // Single Player Project Classic
            if (DateTime.TryParse(User.UI.Version.OFF.Classic, out DateTime SPPLocalClassic) && DateTime.TryParse(User.UI.Version.ON.Classic, out DateTime SPPOnlineClassic))
            {
                if (SPPLocalClassic < SPPOnlineClassic && SPPOnlineClassic != DateTime.MinValue)
                {
                    if (Setting.List.AutoUpdateCore)
                    {
                        if (User.UI.Form.ClassicLogonRunning)
                        {
                            var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.ClassicLogonName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.WorldProcessesID.Remove(processToRemove);
                        }
                        if (User.UI.Form.ClassicWorldRunning)
                        {
                            var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CustomWorldExeName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.WorldProcessesID.Remove(processToRemove);
                        }
                        await StartUpdate(Links.Install.Classic, $"{Links.MainCDNHost}{Links.Hashe.Classic}", true);
                    }
                    User.UI.Version.Update.Classic = true;
                }
                else
                {
                    User.UI.Version.Update.Classic = false;
                }

            }
            Thread.Sleep(100);
            // Single Player Project Update TBC
            if (DateTime.TryParse(User.UI.Version.OFF.TBC, out DateTime SPPLocalTBC) && DateTime.TryParse(User.UI.Version.ON.TBC, out DateTime SPPOnlineTBC))
            {
                if (SPPLocalTBC < SPPOnlineTBC && SPPOnlineTBC != DateTime.MinValue)
                {
                    if (Setting.List.AutoUpdateCore)
                    {
                        if (User.UI.Form.TBCLogonRunning)
                        {
                            var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.TBCLogonName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.WorldProcessesID.Remove(processToRemove);
                        }
                        if (User.UI.Form.TBCWorldRunning)
                        {
                            var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.TBCWorldName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.WorldProcessesID.Remove(processToRemove);
                        }

                        await StartUpdate(Links.Install.TBC, $"{Links.MainCDNHost}{Links.Hashe.TBC}", true);
                    }
                    User.UI.Version.Update.TBC = true;
                }
                else
                {
                    User.UI.Version.Update.TBC = false;
                }
            }
            Thread.Sleep(100);
            // Single Player Project Update WOTLK
            if (DateTime.TryParse(User.UI.Version.OFF.WotLK, out DateTime SPPLocalWOTLK) && DateTime.TryParse(User.UI.Version.ON.WotLK, out DateTime SPPOnlineWOTLK))
            {
                if (SPPLocalWOTLK < SPPOnlineWOTLK && SPPOnlineWOTLK != DateTime.MinValue)
                {
                    if (Setting.List.AutoUpdateCore)
                    {
                        if (User.UI.Form.WotLKLogonRunning)
                        {
                            var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.WotLKLogonName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.WorldProcessesID.Remove(processToRemove);
                        }
                        if (User.UI.Form.WotLKWorldRunning)
                        {
                            var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.WotLKWorldName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.WorldProcessesID.Remove(processToRemove);
                        }

                        await StartUpdate(Links.Install.WotLK, $"{Links.MainCDNHost}{Links.Hashe.WotLK}", true);
                    }
                    User.UI.Version.Update.WotLK = true;
                }
                else
                {

                    User.UI.Version.Update.WotLK = false;
                }

            }
            Thread.Sleep(100);
            // Single Player Project Update
            if (DateTime.TryParse(User.UI.Version.OFF.Cata, out DateTime SPPLocalCata) && DateTime.TryParse(User.UI.Version.ON.Cata, out DateTime SPPOnlineCata))
            {
                if (SPPLocalCata < SPPOnlineCata && SPPOnlineCata != DateTime.MinValue)
                {
                    if (Setting.List.AutoUpdateCore)
                    {
                        if (User.UI.Form.CustLogonRunning)
                        {
                            var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CataLogonName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.WorldProcessesID.Remove(processToRemove);
                        }
                        if (User.UI.Form.CataWorldRunning)
                        {
                            var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CataWorldName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.WorldProcessesID.Remove(processToRemove);
                        }
                        await StartUpdate(Links.Install.Cata, $"{Links.MainCDNHost}{Links.Hashe.Cata}", true);
                    }
                    User.UI.Version.Update.Cata = true;
                }
                else
                {

                    User.UI.Version.Update.Cata = false;
                }
            }
            Thread.Sleep(100);
            // Single Player Project Update
            if (DateTime.TryParse(User.UI.Version.OFF.Mop, out DateTime SPPLocalMOP) && DateTime.TryParse(User.UI.Version.ON.Mop, out DateTime SPPOnlineMOP))
            {
                if (SPPLocalMOP < SPPOnlineMOP && SPPOnlineMOP != DateTime.MinValue)
                {
                    if (Setting.List.AutoUpdateCore)
                    {
                        if (User.UI.Form.MOPLogonRunning)
                        {
                            var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.MoPLogonName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.WorldProcessesID.Remove(processToRemove);
                        }
                        if (User.UI.Form.MOPWorldRunning)
                        {
                            var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.MoPWorldName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.WorldProcessesID.Remove(processToRemove);
                        }
                        await StartUpdate(Links.Install.Mop, $"{Links.MainCDNHost}{Links.Hashe.Mop}", true);
                    }
                    User.UI.Version.Update.Mop = true;
                }
                else
                {

                    User.UI.Version.Update.Mop = false;
                }
            }
            Thread.Sleep(100);
            // Database Update
            if (!string.IsNullOrEmpty(User.UI.Version.OFF.Database) && !string.IsNullOrEmpty(User.UI.Version.ON.Database))
            {
                if (VersionCompare(User.UI.Version.OFF.Database, User.UI.Version.ON.Database) < 0)
                {
                    if (Setting.List.AutoUpdateMySQL)
                    {
                        if (User.UI.Form.DBRunning)
                        {
                            var processToRemove = User.System.DatabaseProcessID.Single(r => r.Name == Setting.List.DBExeName);
                            await Watcher.ApplicationStop(processToRemove.ID);
                            User.System.DatabaseProcessID.Remove(processToRemove);
                        }
                        await StartUpdate(Links.Install.Database, $"{Links.MainCDNHost}{Links.Hashe.Database}", true);
                        User.UI.Version.Update.Database = true;
                    }
                    User.UI.Version.Update.Database = true;
                }
            }
            Thread.Sleep(100);
            // Trion Update
            if (!string.IsNullOrEmpty(User.UI.Version.OFF.Trion) && !string.IsNullOrEmpty(User.UI.Version.ON.Trion))
            {
                if (VersionCompare(User.UI.Version.OFF.Trion, User.UI.Version.ON.Trion) < 0)
                {
                    if (Setting.List.AutoUpdateTrion)
                    {
                        await StartUpdate(Links.Install.Trion, $"{Links.MainCDNHost}{Links.Hashe.Trion}", true);
                    }
                    User.UI.Version.Update.Trion = true;
                }
                User.UI.Version.Update.Trion = false;
            }
        }
        public static async Task StartDatabase(string argu)
        {
            User.System.DatabaseProcessID.Clear();
            if (Setting.List.DBExeLoc != string.Empty)
            {
                int ID = await Watcher.ApplicationStart(
                    Setting.List.DBExeLoc,
                    Setting.List.DBWorkingDir,
                    Setting.List.DBExeName,
                    Setting.List.ConsolHide,
                    argu);
                User.System.DatabaseProcessID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.DBExeName });
                User.UI.Form.DBStarted = true;
            }
        }
        public static async Task StopDatabase()
        {
            var processToRemove = User.System.DatabaseProcessID.Single(r => r.Name == Setting.List.DBExeName);
            await Watcher.ApplicationStop(processToRemove.ID);
            User.System.DatabaseProcessID.Remove(processToRemove);
        }
        public static async Task<bool> IsPortOpen()
        {
            string host = "localhost";
            int port = int.Parse(Setting.List.DBServerPort);
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
        public static async Task StartWorld()
        {
            User.System.WorldProcessesID.Clear();
            if (Setting.List.CustomInstalled && !User.UI.Form.CustLogonRunning)
            {
                int ID = await Watcher.ApplicationStart(
                    Setting.List.CustomWorldExeLoc,
                    Setting.List.CustomWorkingDirectory,
                    Setting.List.CustomWorldExeName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.CustomWorldExeName });
                User.UI.Form.CustWorldStarted = true;
            }
            if (Setting.List.ClassicInstalled && !User.UI.Form.ClassicWorldRunning)
            {
                int ID = await Watcher.ApplicationStart(
                    Setting.List.ClassicWorldExeLoc,
                    Setting.List.ClassicWorkingDirectory,
                    Setting.List.ClassicWorldName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.ClassicWorldName });
                User.UI.Form.ClassicWorldStarted = true;
            }
            
            if (Setting.List.TBCInstalled && !User.UI.Form.TBCWorldRunning)
            {
                int ID = await Watcher.ApplicationStart(
                    Setting.List.TBCWorldExeLoc,
                    Setting.List.TBCWorkingDirectory,
                    Setting.List.TBCWorldName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.TBCWorldName });
                User.UI.Form.TBCWorldStarted = true;
            }
            if (Setting.List.WotLKInstalled && !User.UI.Form.WotLKWorldRunning)
            {
                int ID = await Watcher.ApplicationStart(
                     Setting.List.WotLKWorldExeLoc,
                     Setting.List.WotLKWorkingDirectory,
                     Setting.List.WotLKWorldName,
                     Setting.List.ConsolHide,
                     null
                );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.WotLKWorldName });
                User.UI.Form.WotLKWorldStarted = true;
            }
            if (Setting.List.CataInstalled && !User.UI.Form.CataWorldRunning)
            {
                int ID = await Watcher.ApplicationStart(
                    Setting.List.CataWorldExeLoc,
                    Setting.List.CataWorkingDirectory,
                    Setting.List.CataWorldName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.CataWorldName });
                User.UI.Form.CataWorldStarted = true;
            }
            if (Setting.List.MOPInstalled && !User.UI.Form.MOPWorldRunning)
            {
                int ID = await Watcher.ApplicationStart(
                    Setting.List.MopWorldExeLoc,
                    Setting.List.MopWorkingDirectory,
                    Setting.List.MoPWorldName,
                    Setting.List.ConsolHide,
                    null
                    );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.MoPWorldName });
                User.UI.Form.MOPWorldStarted = true;
            }

            if (User.System.WorldProcessesID.Count > 0)
            {
                User.UI.Resource.CurrentWorldID = User.System.WorldProcessesID[0].ID;
                User.System.WorldStartTime = DateTime.Now;
            }
        }
        public static async Task StartLogon()
        {
            User.System.LogonProcessesID.Clear();
            if (Setting.List.CustomInstalled)
            {
                int ID = await Watcher.ApplicationStart(
                   Setting.List.CustomLogonExeLoc,
                   Setting.List.CustomWorkingDirectory,
                   Setting.List.CustomWorldExeName,
                   Setting.List.ConsolHide,
                   null
               );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.CustomWorldExeName });
                User.UI.Form.CustLogonStarted = true;
            }
            if (Setting.List.ClassicInstalled)
            {
                int ID = await Watcher.ApplicationStart(
                    Setting.List.ClassicLogonExeLoc,
                    Setting.List.ClassicWorkingDirectory,
                    Setting.List.ClassicLogonName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.ClassicLogonName });
                User.UI.Form.ClassicLogonStarted = true;
            }
            if (Setting.List.TBCInstalled)
            {
                int ID = await Watcher.ApplicationStart(
                    Setting.List.TBCLogonExeLoc,
                    Setting.List.TBCWorkingDirectory,
                    Setting.List.TBCLogonName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.TBCLogonName });
                User.UI.Form.TBCLogonStarted = true;
            }
            if (Setting.List.WotLKInstalled)
            {
                int ID = await Watcher.ApplicationStart(
                     Setting.List.WotLKLogonExeLoc,
                     Setting.List.WotLKWorkingDirectory,
                     Setting.List.WotLKLogonName,
                     Setting.List.ConsolHide,
                     null
                );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.WotLKLogonName });
                User.UI.Form.WotLKLogonStarted = true;
            }
            if (Setting.List.CataInstalled)
            {
                int ID = await Watcher.ApplicationStart(
                    Setting.List.CataLogonExeLoc,
                    Setting.List.CataWorkingDirectory,
                    Setting.List.CataLogonName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.CataLogonName });
                User.UI.Form.CataLogonStarted = true;
            }
            if (Setting.List.MOPInstalled)
            {
                int ID = await Watcher.ApplicationStart(
                    Setting.List.MopLogonExeLoc,
                    Setting.List.MopWorkingDirectory,
                    Setting.List.MoPLogonName,
                    Setting.List.ConsolHide,
                    null
                    );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.MoPLogonName });
                User.UI.Form.MOPLogonStarted = true;
            }

            if (User.System.LogonProcessesID.Count > 0)
            {
                User.UI.Resource.CurrentAuthID = User.System.LogonProcessesID[0].ID;
                User.System.LogonStartTime = DateTime.Now;
            }
        }
        public static async Task StopWorld()
        {
            if (User.System.WorldProcessesID.Count > 0)
            {
                if (User.UI.Form.CustWorldStarted && User.UI.Form.ClassicWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.ClassicWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                }
                if (User.UI.Form.ClassicWorldStarted && User.UI.Form.ClassicWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.ClassicWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                }
                if (User.UI.Form.TBCWorldStarted && User.UI.Form.TBCWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.TBCWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                }
                if (User.UI.Form.WotLKWorldStarted && User.UI.Form.WotLKWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.WotLKWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                }
                if (User.UI.Form.CataWorldStarted && User.UI.Form.CataWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CataWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                }
                if (User.UI.Form.MOPWorldStarted && User.UI.Form.MOPWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.MoPWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                }
            }
        }
        public static async Task StopLogon()
        {
            if (User.UI.Form.CustLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CustomLogonExeName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (User.UI.Form.ClassicLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.ClassicLogonName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (User.UI.Form.TBCLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.TBCLogonName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (User.UI.Form.WotLKLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.WotLKLogonName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (User.UI.Form.CataLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CataLogonName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (User.UI.Form.MOPLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.MoPLogonName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
        }
        public static void IsWorldRunning(List<Lists.ProcessID> PIDS)
        {
            foreach (var process in PIDS)
            {
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.CustomWorldExeName) { User.UI.Form.CustWorldRunning = true; }
                else { User.UI.Form.CustWorldRunning = false; }
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.ClassicWorldName) { User.UI.Form.ClassicWorldRunning = true; }
                else { User.UI.Form.ClassicWorldRunning = false; }
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.TBCWorldName) { User.UI.Form.TBCWorldRunning = true; }
                else { User.UI.Form.TBCWorldRunning = false; }
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.WotLKWorldName) { User.UI.Form.WotLKWorldRunning = true; }
                else { User.UI.Form.WotLKWorldRunning = false; }
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.CataWorldName) { User.UI.Form.CataWorldRunning = true; }
                else { User.UI.Form.CataWorldRunning = false; }
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.MoPWorldName) { User.UI.Form.MOPWorldRunning = true; }
                else { User.UI.Form.MOPWorldRunning = false; }
            }
        }
        public static void IsLogonRunning(List<Lists.ProcessID> PIDS)
        {
            foreach (var process in PIDS)
            {
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.CustomLogonExeName) { User.UI.Form.CustLogonRunning = true; }
                else { User.UI.Form.CustLogonRunning = false; }
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.ClassicLogonName) { User.UI.Form.ClassicLogonRunning = true; }
                else { User.UI.Form.ClassicLogonRunning = false; }
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.TBCLogonName) { User.UI.Form.TBCLogonRunning = true; }
                else { User.UI.Form.TBCLogonRunning = false; }
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.WotLKLogonName) { User.UI.Form.WotLKLogonRunning = true; }
                else { User.UI.Form.WotLKLogonRunning = false; }
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.CataLogonName) { User.UI.Form.CataLogonRunning = true; }
                else { User.UI.Form.CataLogonRunning = false; }
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.MoPLogonName) { User.UI.Form.MOPLogonRunning = true; }
                else { User.UI.Form.MOPLogonRunning = false; }
            }
        }
        public static void IsDatabaseRunning(List<Lists.ProcessID> PIDS)
        {
            foreach (var process in PIDS)
            {
                if (Watcher.ApplicationRuning(process.ID) && process.Name == Setting.List.DBExeName)
                {
                    User.UI.Form.DBRunning = true;

                }
                else { User.UI.Form.CustWorldRunning = false; }
            }
        }
        public static async Task CrashDetector(int Attempts)
        {
            if (User.UI.Form.CustWorldStarted && !User.UI.Form.CustWorldRunning)
            {
                for (int Attempt = 1; Attempt != Attempts; Attempt++)
                {
                    if (Attempt != Attempts - 1)
                    {
                        var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CustomWorldExeName);
                        User.System.LogonProcessesID.Remove(processToRemove);
                        try
                        {
                            int ID = await Watcher.ApplicationStart(
                            Setting.List.CustomLogonExeLoc,
                            Setting.List.CustomWorkingDirectory,
                            Setting.List.CustomWorldExeName,
                            Setting.List.ConsolHide,
                            null
                            );
                            User.System.LogonProcessesID.Add(new Lists.ProcessID()
                            { ID = ID, Name = Setting.List.CustomLogonExeName });
                            User.UI.Form.CustLogonStarted = true;
                            await Task.Delay(3000);
                        }
                        catch (Exception ex)
                        {
                            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                            //Optionally, wait before retrying
                            await Task.Delay(3000); //This need tunning
                        }
                    }
                    else
                    {
                        User.UI.Form.CustWorldStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.CustomWorldExeName} ";
                    }
                }
            }
            if (User.UI.Form.CustLogonStarted && !User.UI.Form.CustLogonRunning)
            {
                for (int Attempt = 1; Attempt < Attempts; Attempt++)
                {
                    if (Attempt != Attempts - 1)
                    {
                        var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CustomWorldExeName);
                        User.System.LogonProcessesID.Remove(processToRemove);
                        try
                        {
                            int ID = await Watcher.ApplicationStart(
                            Setting.List.CustomLogonExeLoc,
                            Setting.List.CustomWorkingDirectory,
                            Setting.List.CustomWorldExeName,
                            Setting.List.ConsolHide,
                            null
                            );
                            User.System.LogonProcessesID.Add(new Lists.ProcessID()
                            { ID = ID, Name = Setting.List.CustomLogonExeName });
                            User.UI.Form.CustLogonStarted = true;
                            await Task.Delay(3000);
                        }
                        catch (Exception ex)
                        {
                            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                            // Optionally, wait before retrying
                            await Task.Delay(3000); //This need tunning
                        }
                    }
                    else
                    {
                        User.UI.Form.CustLogonStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.CustomWorldExeName} ";
                    }
                }
            }
            if (User.UI.Form.ClassicWorldStarted && !User.UI.Form.ClassicWorldRunning)
            {
                for (int Attempt = 1; Attempt < Attempts; Attempt++)
                {
                    if (Attempt != Attempts - 1)
                    {
                        var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.ClassicWorldName);
                        User.System.LogonProcessesID.Remove(processToRemove);
                        try
                        {
                            int ID = await Watcher.ApplicationStart(
                            Setting.List.ClassicWorldExeLoc,
                            Setting.List.ClassicWorkingDirectory,
                            Setting.List.ClassicWorldName,
                            Setting.List.ConsolHide,
                            null
                            );
                            User.System.WorldProcessesID.Add(new Lists.ProcessID()
                            { ID = ID, Name = Setting.List.ClassicWorldExeName });
                            User.UI.Form.ClassicWorldStarted = true;
                            await Task.Delay(3000);
                        }
                        catch (Exception ex)
                        {
                            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                            // Optionally, wait before retrying
                            await Task.Delay(3000); //This need tunning
                        }
                    }
                    else
                    {
                        User.UI.Form.ClassicWorldStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.ClassicWorldName}!";
                    }
                }
            }
            if (User.UI.Form.ClassicLogonStarted && !User.UI.Form.ClassicLogonRunning)
            {
                for (int Attempt = 1; Attempt < Attempts; Attempt++)
                {
                    if (Attempt != Attempts - 1)
                    {
                        var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.ClassicLogonName);
                        User.System.LogonProcessesID.Remove(processToRemove);
                        try
                        {
                            int ID = await Watcher.ApplicationStart(
                            Setting.List.ClassicLogonExeLoc,
                            Setting.List.ClassicWorkingDirectory,
                            Setting.List.ClassicLogonName,
                            Setting.List.ConsolHide,
                            null
                            );
                            User.System.LogonProcessesID.Add(new Lists.ProcessID()
                            { ID = ID, Name = Setting.List.ClassicLogonName });
                            User.UI.Form.ClassicLogonStarted = true;
                            await Task.Delay(3000);
                        }
                        catch (Exception ex)
                        {
                            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                            // Optionally, wait before retrying
                            await Task.Delay(3000); //This need tunning
                        }
                    }
                    else
                    {
                        var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.ClassicLogonName);
                        User.UI.Form.ClassicLogonStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.ClassicWorldName}!";
                    }
                }
            }
            if (User.UI.Form.TBCWorldStarted && !User.UI.Form.TBCWorldRunning)
            {
                for (int Attempt = 1; Attempt < Attempts; Attempt++)
                {
                    if (Attempt != Attempts - 1)
                    {
                        var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.TBCWorldName);
                        User.System.LogonProcessesID.Remove(processToRemove);
                        try
                        {
                            int ID = await Watcher.ApplicationStart(
                            Setting.List.TBCWorldExeLoc,
                            Setting.List.TBCWorkingDirectory,
                            Setting.List.TBCWorldName,
                            Setting.List.ConsolHide,
                            null
                            );
                            User.System.WorldProcessesID.Add(new Lists.ProcessID()
                            { ID = ID, Name = Setting.List.TBCWorldName });
                            User.UI.Form.TBCWorldStarted = true;
                            await Task.Delay(3000);
                        }
                        catch (Exception ex)
                        {
                            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                            // Optionally, wait before retrying
                            await Task.Delay(3000);//This need tunning
                        }
                    }
                    else
                    {
                        User.UI.Form.TBCWorldStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.TBCWorldName}!";
                    }
                }
            }
            if (User.UI.Form.TBCLogonStarted && !User.UI.Form.TBCLogonRunning)
            {
                for (int Attempt = 1; Attempt < Attempts; Attempt++)
                {
                    if (Attempt != Attempts - 1)
                    {
                        var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.TBCLogonName);
                        User.System.LogonProcessesID.Remove(processToRemove);
                        try
                        {
                            int ID = await Watcher.ApplicationStart(
                            Setting.List.TBCLogonExeLoc,
                            Setting.List.TBCWorkingDirectory,
                            Setting.List.TBCLogonName,
                            Setting.List.ConsolHide,
                            null
                            );
                            User.System.LogonProcessesID.Add(new Lists.ProcessID()
                            { ID = ID, Name = Setting.List.TBCLogonName });
                            User.UI.Form.TBCLogonStarted = true;
                            await Task.Delay(3000);
                        }
                        catch (Exception ex)
                        {
                            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                            // Optionally, wait before retrying
                            await Task.Delay(3000); //This need tunning
                        }
                    }
                    else
                    {
                        User.UI.Form.TBCLogonStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.TBCLogonName}!";
                    }
                }
            }
            if (User.UI.Form.WotLKWorldStarted && !User.UI.Form.WotLKWorldRunning)
            {
                while (Attempt.WotlkWorld < Attempts)
                {
                    // Remove the old process if it exists
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.WotLKWorldName);
                    User.System.WorldProcessesID.Remove(processToRemove);
                    try
                    {
                        int ID = await Watcher.ApplicationStart(
                        Setting.List.WotLKWorldExeLoc,
                        Setting.List.WotLKWorkingDirectory,
                        Setting.List.WotLKWorldName,
                        Setting.List.ConsolHide,
                        null
                        );
                        // Add new process ID
                        User.System.WorldProcessesID.Add(new Lists.ProcessID()
                        { ID = ID, Name = Setting.List.WotLKWorldName });
                        User.UI.Form.WotLKWorldStarted = true;
                        // Delay to allow the process to stabilize
                        await Task.Delay(3000);
                        Infos.Message = $"{Setting.List.WotLKWorldName} recover attempt: {Attempt.WotlkWorld}";
                        Attempt.WotlkWorld++;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.WotlkWorld} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.WotlkWorld++;
                    }
                    if (Attempt.WotlkWorld == Attempts)
                    {
                        User.UI.Form.WotLKWorldStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.WotLKWorldName}!";
                        break;
                    }
                }

            }
            if (User.UI.Form.WotLKLogonStarted && !User.UI.Form.WotLKLogonRunning)
            {
                
                while (Attempt.WotlkLogon < Attempts)
                {
                    var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.WotLKLogonName);
                    User.System.LogonProcessesID.Remove(processToRemove);
                    try
                    {
                        int ID = await Watcher.ApplicationStart(
                        Setting.List.WotLKLogonExeLoc,
                        Setting.List.WotLKWorkingDirectory,
                        Setting.List.WotLKLogonName,
                        Setting.List.ConsolHide,
                        null
                        );
                        User.System.LogonProcessesID.Add(new Lists.ProcessID()
                        { ID = ID, Name = Setting.List.WotLKLogonName });
                        User.UI.Form.WotLKLogonStarted = true;
                        await Task.Delay(3000); //This need tunning
                        Attempt.WotlkLogon++;
                        Infos.Message = $"{Setting.List.WotLKLogonName} recover attempt:{Attempt.WotlkLogon}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.WotlkLogon} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.WotlkLogon++;
                    }
                    if (Attempt.WotlkLogon == Attempts)
                    {
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.WotLKLogonName}!";
                        User.UI.Form.WotLKLogonStarted = false;                        
                        break;
                    }
                }
            }
            if (User.UI.Form.CataWorldStarted && !User.UI.Form.CataWorldRunning)
            {
                for (int Attempt = 1; Attempt < Attempts; Attempt++)
                {
                    if (Attempt != Attempts - 1)
                    {
                        var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CataWorldName);
                        User.System.LogonProcessesID.Remove(processToRemove);
                        try
                        {
                            int ID = await Watcher.ApplicationStart(
                            Setting.List.CataWorldExeLoc,
                            Setting.List.CataWorkingDirectory,
                            Setting.List.CataWorldName,
                            Setting.List.ConsolHide,
                            null
                            );
                            User.System.WorldProcessesID.Add(new Lists.ProcessID()
                            { ID = ID, Name = Setting.List.CataWorldName });
                            User.UI.Form.CataWorldStarted = true;
                            await Task.Delay(3000);
                        }
                        catch (Exception ex)
                        {
                            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                            // Optionally, wait before retrying
                            await Task.Delay(3000); //This need tunning
                        }
                    }
                    else
                    {
                        User.UI.Form.CataWorldStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.CataWorldName}!";
                    }
                }
            }
            if (User.UI.Form.CataLogonStarted && !User.UI.Form.CataLogonRunning)
            {
                for (int Attempt = 1; Attempt < Attempts; Attempt++)
                {
                    if (Attempt != Attempts - 1)
                    {
                        var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CataLogonName);
                        User.System.LogonProcessesID.Remove(processToRemove);
                        try
                        {
                            int ID = await Watcher.ApplicationStart(
                            Setting.List.CataLogonExeLoc,
                            Setting.List.CataWorkingDirectory,
                            Setting.List.CataLogonName,
                            Setting.List.ConsolHide,
                            null
                            );
                            User.System.LogonProcessesID.Add(new Lists.ProcessID()
                            { ID = ID, Name = Setting.List.CataLogonName });
                            User.UI.Form.CataLogonStarted = true;
                            await Task.Delay(3000);
                        }
                        catch (Exception ex)
                        {
                            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                            // Optionally, wait before retrying
                            await Task.Delay(3000); //This need tunning
                        }
                    }
                    else
                    {
                        User.UI.Form.CataLogonStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.CataLogonName}!";
                    }
                }
            }
            if (User.UI.Form.MOPWorldStarted && !User.UI.Form.MOPWorldRunning)
            {
                for (int Attempt = 1; Attempt < Attempts; Attempt++)
                {
                    if (Attempt != Attempts - 1)
                    {
                        var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.MoPWorldName);
                        User.System.LogonProcessesID.Remove(processToRemove);
                        try
                        {
                            int ID = await Watcher.ApplicationStart(
                            Setting.List.MopLogonExeLoc,
                            Setting.List.MopWorkingDirectory,
                            Setting.List.MoPWorldName,
                            Setting.List.ConsolHide,
                            null
                            );
                            User.System.LogonProcessesID.Add(new Lists.ProcessID()
                            { ID = ID, Name = Setting.List.MoPWorldName });
                            User.UI.Form.MOPLogonStarted = true;
                            await Task.Delay(3000);
                        }
                        catch (Exception ex)
                        {
                            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                            // Optionally, wait before retrying
                            await Task.Delay(3000); //This need tunning
                        }
                    }
                    else
                    {
                        User.UI.Form.MOPWorldStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.MoPWorldName}!";
                    }
                }
            }
            if (User.UI.Form.MOPLogonStarted && !User.UI.Form.MOPLogonRunning)
            {
                for (int Attempt = 1; Attempt < Attempts; Attempt++)
                {
                    if (Attempt != Attempts - 1)
                    {
                        var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.MoPLogonName);
                        User.System.LogonProcessesID.Remove(processToRemove);
                        try
                        {
                            int ID = await Watcher.ApplicationStart(
                            Setting.List.MopLogonExeLoc,
                            Setting.List.MopWorkingDirectory,
                            Setting.List.MoPLogonName,
                            Setting.List.ConsolHide,
                            null
                            );
                            User.System.LogonProcessesID.Add(new Lists.ProcessID()
                            { ID = ID, Name = Setting.List.MoPLogonName });
                            User.UI.Form.MOPLogonStarted = true;
                            await Task.Delay(3000);
                        }
                        catch (Exception ex)
                        {
                            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                            // Optionally, wait before retrying
                            await Task.Delay(3000); //This need tunning
                        }
                    }
                    else
                    {
                        User.UI.Form.MOPLogonStarted = false;
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.MoPLogonName}!";
                    }
                }
            }
            if (User.UI.Form.DBStarted && !User.UI.Form.DBRunning)
            {
                //for (int Attempt = 1; Attempt < Attempts; Attempt++)
                //{
                //    var processToRemove = User.System.DatabaseProcessID.Single(r => r.Name == Setting.List.DBExeName);
                //    User.System.DatabaseProcessID.Remove(processToRemove);
                //    if (Attempt != Attempts - 1)
                //    {
                //        try
                //        {
                //            Setting.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                //            string arg = $@"--defaults-file={Directory.GetCurrentDirectory()}/my.ini --console";
                //            StartDatabase(arg);
                //            await Task.Delay(600000);
                //            break;
                //        }
                //        catch (Exception ex)
                //        {
                //            Infos.Message = $"Attempt {Attempt} failed: {ex.Message}";
                //            // Optionally, wait before retrying
                //            await Task.Delay(3000); //This need tunning
                //        }
                //    }
                //    else
                //    {
                //        User.UI.Form.DBStarted = false;
                //        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.DBExeName}!";
                //    }
                //}
            }
        }
    }
}
