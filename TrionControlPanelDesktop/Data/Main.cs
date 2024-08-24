using System.Diagnostics;
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
        public static bool ServerStartedWorld()
        {
            if (User.UI.Form.CustWorldStarted ||
                User.UI.Form.ClassicWorldStarted ||
                User.UI.Form.TBCWorldStarted ||
                User.UI.Form.WotLKWorldStarted ||
                User.UI.Form.CataWorldStarted ||
                User.UI.Form.MOPWorldStarted)
            { return true; }
            else { return false; }
        }
        public static bool ServerStatusWorld()
        {
            if (User.UI.Form.CustWorldRunning ||
                User.UI.Form.ClassicWorldRunning ||
                User.UI.Form.TBCWorldRunning ||
                User.UI.Form.WotLKWorldRunning ||
                User.UI.Form.CataWorldRunning ||
                User.UI.Form.MOPWorldRunning)
            { return true; }
            else { return false; }
        }
        public static bool ServerStatusLogon()
        {
            if (User.UI.Form.CustLogonRunning ||
                User.UI.Form.ClassicLogonRunning ||
                User.UI.Form.TBCLogonRunning ||
                User.UI.Form.WotLKLogonRunning ||
                User.UI.Form.CataLogonRunning ||
                User.UI.Form.MOPLogonRunning)
            { return true; }
            else { return false; }
        }
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
            try
            {
                var processToRemove = User.System.DatabaseProcessID.Single(r => r.Name == Setting.List.DBExeName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.DatabaseProcessID.Clear();
                User.UI.Form.DBStarted = false;
            }
            catch (Exception ex)
            {
                Infos.Message = ex.Message;
            }
        }
        public static async Task StartWorld()
        {
            User.System.WorldProcessesID.Clear();
            if (Setting.List.LaunchCustomCore && !User.UI.Form.CustWorldRunning)
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
            if (Setting.List.LaunchClassicCore && !User.UI.Form.ClassicWorldRunning)
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

            if (Setting.List.LaunchTBCCore && !User.UI.Form.TBCWorldRunning)
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
            if (Setting.List.LaunchWotLKCore && !User.UI.Form.WotLKWorldRunning)
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
            if (Setting.List.LaunchCataCore && !User.UI.Form.CataWorldRunning)
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
            if (Setting.List.LaunchMoPCore && !User.UI.Form.MOPWorldRunning)
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
            if (Setting.List.LaunchCustomCore && !User.UI.Form.CustLogonRunning)
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
            if (Setting.List.LaunchClassicCore && !User.UI.Form.ClassicLogonRunning)
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
            if (Setting.List.LaunchTBCCore && !User.UI.Form.TBCLogonRunning)
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
            if (Setting.List.LaunchWotLKCore && !User.UI.Form.WotLKLogonRunning)
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
            if (Setting.List.LaunchCataCore && !User.UI.Form.CataLogonRunning)
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
            if (Setting.List.LaunchMoPCore && !User.UI.Form.MOPLogonRunning)
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
                    User.UI.Form.CustWorldStarted = false;
                }
                if (User.UI.Form.ClassicWorldStarted && User.UI.Form.ClassicWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.ClassicWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                    User.UI.Form.ClassicWorldStarted = false;
                }
                if (User.UI.Form.TBCWorldStarted && User.UI.Form.TBCWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.TBCWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                    User.UI.Form.TBCWorldStarted = false;
                }
                if (User.UI.Form.WotLKWorldStarted && User.UI.Form.WotLKWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.WotLKWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                    User.UI.Form.WotLKWorldStarted = false;
                }
                if (User.UI.Form.CataWorldStarted && User.UI.Form.CataWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CataWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                    User.UI.Form.CataWorldStarted = false;
                }
                if (User.UI.Form.MOPWorldStarted && User.UI.Form.MOPWorldRunning)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.MoPWorldName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                    User.System.WorldProcessesID.Remove(processToRemove);
                    User.UI.Form.MOPWorldStarted = false;
                }
            }
        }
        public static async Task StopLogon()
        {
            if (User.UI.Form.CustLogonStarted && User.UI.Form.CustLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CustomLogonExeName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.LogonProcessesID.Remove(processToRemove);
                User.UI.Form.CustLogonStarted = false;
            }
            if (User.UI.Form.ClassicLogonStarted && User.UI.Form.ClassicLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.ClassicLogonName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.LogonProcessesID.Remove(processToRemove);
                User.UI.Form.ClassicLogonStarted = false;
            }
            if (User.UI.Form.TBCLogonStarted && User.UI.Form.TBCLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.TBCLogonName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.LogonProcessesID.Remove(processToRemove);
                User.UI.Form.TBCLogonStarted = false;
            }
            if (User.UI.Form.WotLKLogonStarted && User.UI.Form.WotLKLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.WotLKLogonName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.LogonProcessesID.Remove(processToRemove);
                User.UI.Form.WotLKLogonStarted = false;
            }
            if (User.UI.Form.CataLogonStarted && User.UI.Form.CataLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CataLogonName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.LogonProcessesID.Remove(processToRemove);
                User.UI.Form.CataLogonStarted = false;
            }
            if (User.UI.Form.MOPLogonStarted && User.UI.Form.MOPLogonRunning)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.MoPLogonName);
                await Watcher.ApplicationStop(processToRemove.ID);
                User.System.LogonProcessesID.Remove(processToRemove);
                User.UI.Form.MOPLogonStarted = false;
            }
        }
        public static void IsWorldRunning(List<Lists.ProcessID> PIDS)
        {
            if (ServerStartedWorld())
            {
                foreach (var process in PIDS)
                {
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.CustomWorldExeName) { User.UI.Form.CustWorldRunning = true; }
                    else { User.UI.Form.CustWorldRunning = false; }
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.ClassicWorldName) { User.UI.Form.ClassicWorldRunning = true; }
                    else { User.UI.Form.ClassicWorldRunning = false; }
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.TBCWorldName) { User.UI.Form.TBCWorldRunning = true; }
                    else { User.UI.Form.TBCWorldRunning = false; }
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.WotLKWorldName) { User.UI.Form.WotLKWorldRunning = true; }
                    else { User.UI.Form.WotLKWorldRunning = false; }
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.CataWorldName) { User.UI.Form.CataWorldRunning = true; }
                    else { User.UI.Form.CataWorldRunning = false; }
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.MoPWorldName) { User.UI.Form.MOPWorldRunning = true; }
                    else { User.UI.Form.MOPWorldRunning = false; }
                }
            }
            else
            {
                User.UI.Form.CustWorldRunning = false;
                User.UI.Form.ClassicWorldRunning = false;
                User.UI.Form.TBCWorldRunning = false;
                User.UI.Form.WotLKWorldRunning = false;
                User.UI.Form.CataWorldRunning = false;
                User.UI.Form.MOPWorldRunning = false;
            }

        }
        public static void IsLogonRunning(List<Lists.ProcessID> PIDS)
        {
            if (ServerStatusLogon())
            {
                foreach (var process in PIDS)
                {
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.CustomLogonExeName) { User.UI.Form.CustLogonRunning = true; }
                    else { User.UI.Form.CustLogonRunning = false; }
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.ClassicLogonName) { User.UI.Form.ClassicLogonRunning = true; }
                    else { User.UI.Form.ClassicLogonRunning = false; }
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.TBCLogonName) { User.UI.Form.TBCLogonRunning = true; }
                    else { User.UI.Form.TBCLogonRunning = false; }
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.WotLKLogonName) { User.UI.Form.WotLKLogonRunning = true; }
                    else { User.UI.Form.WotLKLogonRunning = false; }
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.CataLogonName) { User.UI.Form.CataLogonRunning = true; }
                    else { User.UI.Form.CataLogonRunning = false; }
                    if (Watcher.IsApplicationRuning(process.ID) && process.Name == Setting.List.MoPLogonName) { User.UI.Form.MOPLogonRunning = true; }
                    else { User.UI.Form.MOPLogonRunning = false; }
                }
            }
            else
            {
                User.UI.Form.CustLogonRunning = false;
                User.UI.Form.ClassicLogonRunning = false;
                User.UI.Form.TBCLogonRunning = false;
                User.UI.Form.WotLKLogonRunning = false;
                User.UI.Form.CataLogonRunning = false;
                User.UI.Form.MOPLogonRunning = false;
            }
        }
        public static async Task DatabaseRunIDCHeck(string ExecutableDirecotry, string ExecutableName)
        {
            await Task.Delay(1000);
            Process[] process = Process.GetProcessesByName(ExecutableName);
            foreach (Process proc in process)
            {
                if (Path.GetDirectoryName(proc.MainModule!.FileName) == ExecutableDirecotry && User.System.DatabaseProcessID.Count > 0)
                {
                    if (User.System.DatabaseProcessID.Any(current => current.ID != proc.Id))
                    {
                        var NewProcessID = new Lists.ProcessID
                        {
                            ID = proc.Id,
                            Name = "DB2"
                        };
                        User.System.DatabaseProcessID.Add(NewProcessID);
                    }
                }
            }
        }
        public static void IsDatabaseRunning(List<Lists.ProcessID> PIDS)
        {
            var processID = User.System.DatabaseProcessID.Single(r => r.Name == Setting.List.DBExeName);
            if (User.UI.Form.DBStarted && Watcher.IsApplicationRuning(processID.ID))
            {
                User.UI.Form.DBRunning = true;
            }
           else { User.UI.Form.DBRunning = false; }
            
        }
        public static async Task CrashDetector(int Attempts)
        {
            if (User.UI.Form.CustWorldStarted && !User.UI.Form.CustWorldRunning)
            {
                while (Attempt.CustomWorld < Attempts)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CustomWorldExeName);
                    User.System.WorldProcessesID.Remove(processToRemove);
                    try
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
                        await Task.Delay(3000); //This need tunning
                        Attempt.CustomWorld++;
                        Infos.Message = $"{Setting.List.CustomWorldExeName} recover attempt:{Attempt.CustomWorld}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.CustomWorld} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.CustomWorld++;
                    }
                    if (Attempt.CustomWorld == Attempts - 1)
                    {
                        var processToRemove2 = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CustomWorldExeName);
                        User.System.WorldProcessesID.Remove(processToRemove2);
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.CustomWorldExeName}!";
                        User.UI.Form.CustWorldStarted = false;
                        break;
                    }
                }
            }
            if (User.UI.Form.CustLogonStarted && !User.UI.Form.CustLogonRunning)
            {
                while (Attempt.CustomLogon < Attempts)
                {
                    var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CustomLogonExeName);
                    User.System.LogonProcessesID.Remove(processToRemove);
                    try
                    {
                        int ID = await Watcher.ApplicationStart(
                        Setting.List.CustomLogonExeLoc,
                        Setting.List.CustomWorkingDirectory,
                        Setting.List.CustomLogonExeName,
                        Setting.List.ConsolHide,
                        null
                        );
                        User.System.LogonProcessesID.Add(new Lists.ProcessID()
                        { ID = ID, Name = Setting.List.CustomLogonExeName });
                        User.UI.Form.CustLogonStarted = true;
                        await Task.Delay(3000); //This need tunning
                        Attempt.CustomLogon++;
                        Infos.Message = $"{Setting.List.CustomLogonExeName} recover attempt:{Attempt.CustomLogon}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.CustomLogon} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.CustomLogon++;
                    }
                    if (Attempt.CustomLogon == Attempts - 1)
                    {
                        var processToRemove2 = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CustomLogonExeName);
                        User.System.LogonProcessesID.Remove(processToRemove2);
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.CustomLogonExeName}!";
                        User.UI.Form.CustLogonStarted = false;
                        break;
                    }
                }
            }
            if (User.UI.Form.ClassicWorldStarted && !User.UI.Form.ClassicWorldRunning)
            {
                while (Attempt.ClassicWorld < Attempts)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.ClassicWorldName);
                    User.System.WorldProcessesID.Remove(processToRemove);
                    try
                    {
                        int ID = await Watcher.ApplicationStart(
                        Setting.List.ClassicLogonExeLoc,
                        Setting.List.ClassicWorkingDirectory,
                        Setting.List.ClassicWorldName,
                        Setting.List.ConsolHide,
                        null
                        );
                        User.System.WorldProcessesID.Add(new Lists.ProcessID()
                        { ID = ID, Name = Setting.List.ClassicWorldName });
                        User.UI.Form.ClassicWorldStarted = true;
                        await Task.Delay(3000); //This need tunning
                        Attempt.ClassicWorld++;
                        Infos.Message = $"{Setting.List.ClassicWorldName} recover attempt:{Attempt.ClassicWorld}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.ClassicWorld} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.ClassicWorld++;
                    }
                    if (Attempt.ClassicWorld == Attempts - 1)
                    {
                        var processToRemove2 = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.ClassicWorldName);
                        User.System.WorldProcessesID.Remove(processToRemove2);
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.ClassicWorldName}!";
                        User.UI.Form.ClassicWorldStarted = false;
                        break;
                    }
                }
            }
            if (User.UI.Form.ClassicLogonStarted && !User.UI.Form.ClassicLogonRunning)
            {
                while (Attempt.ClassicLogon < Attempts)
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
                        await Task.Delay(3000); //This need tunning
                        Attempt.ClassicLogon++;
                        Infos.Message = $"{Setting.List.ClassicLogonName} recover attempt:{Attempt.ClassicLogon}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.ClassicLogon} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.ClassicLogon++;
                    }
                    if (Attempt.ClassicLogon == Attempts - 1)
                    {
                        var processToRemove2 = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.ClassicLogonName);
                        User.System.LogonProcessesID.Remove(processToRemove2);
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.ClassicLogonName}!";
                        User.UI.Form.ClassicLogonStarted = false;
                        break;
                    }
                }
            }
            if (User.UI.Form.TBCWorldStarted && !User.UI.Form.TBCWorldRunning)
            {
                while (Attempt.TBCWorld < Attempts)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.TBCWorldName);
                    User.System.WorldProcessesID.Remove(processToRemove);
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
                        await Task.Delay(3000); //This need tunning
                        Attempt.TBCWorld++;
                        Infos.Message = $"{Setting.List.TBCWorldName} recover attempt:{Attempt.TBCWorld}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.TBCWorld} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.TBCWorld++;
                    }
                    if (Attempt.TBCWorld == Attempts - 1)
                    {
                        var processToRemove2 = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.TBCWorldName);
                        User.System.LogonProcessesID.Remove(processToRemove2);
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.TBCWorldName}!";
                        User.UI.Form.TBCWorldStarted = false;
                        break;
                    }
                }
            }
            if (User.UI.Form.TBCLogonStarted && !User.UI.Form.TBCLogonRunning)
            {
                while (Attempt.TBCLogon < Attempts)
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
                        await Task.Delay(3000); //This need tunning
                        Attempt.TBCLogon++;
                        Infos.Message = $"{Setting.List.TBCLogonName} recover attempt:{Attempt.TBCLogon}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.TBCLogon} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.TBCLogon++;
                    }
                    if (Attempt.TBCLogon == Attempts - 1)
                    {
                        var processToRemove2 = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.TBCLogonName);
                        User.System.LogonProcessesID.Remove(processToRemove2);
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.TBCLogonName}!";
                        User.UI.Form.TBCLogonStarted = false;
                        break;
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
                    if (Attempt.WotlkWorld == Attempts - 1)
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
                    if (Attempt.WotlkLogon == Attempts - 1)
                    {
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.WotLKLogonName}!";
                        User.UI.Form.WotLKLogonStarted = false;
                        break;
                    }
                }
            }
            if (User.UI.Form.CataWorldStarted && !User.UI.Form.CataWorldRunning)
            {
                while (Attempt.CataWorld < Attempts)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CataWorldName);
                    User.System.WorldProcessesID.Remove(processToRemove);
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
                        await Task.Delay(3000); //This need tunning
                        Attempt.CataWorld++;
                        Infos.Message = $"{Setting.List.CataWorldName} recover attempt:{Attempt.CataWorld}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.CataWorld} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.CataWorld++;
                    }
                    if (Attempt.CataWorld == Attempts - 1)
                    {
                        var processToRemove2 = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CataWorldName);
                        User.System.WorldProcessesID.Remove(processToRemove2);
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.CataWorldName}!";
                        User.UI.Form.CataWorldStarted = false;
                        break;
                    }
                }
            }
            if (User.UI.Form.CataLogonStarted && !User.UI.Form.CataLogonRunning)
            {
                while (Attempt.CataLogon < Attempts)
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
                        await Task.Delay(3000); //This need tunning
                        Attempt.CataLogon++;
                        Infos.Message = $"{Setting.List.CataLogonName} recover attempt:{Attempt.CataLogon}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.CataLogon} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.CataLogon++;
                    }
                    if (Attempt.CataLogon == Attempts - 1)
                    {
                        var processToRemove2 = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CataLogonName);
                        User.System.LogonProcessesID.Remove(processToRemove2);
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.CataLogonName}!";
                        User.UI.Form.CataLogonStarted = false;
                        break;
                    }
                }
            }
            if (User.UI.Form.MOPWorldStarted && !User.UI.Form.MOPWorldRunning)
            {
                while (Attempt.MopWorld < Attempts)
                {
                    var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.MoPWorldName);
                    User.System.LogonProcessesID.Remove(processToRemove);
                    try
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
                        await Task.Delay(3000); //This need tunning
                        Attempt.MopWorld++;
                        Infos.Message = $"{Setting.List.MoPWorldName} recover attempt:{Attempt.MopWorld}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.MopWorld} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.MopWorld++;
                    }
                    if (Attempt.MopWorld == Attempts - 1)
                    {
                        var processToRemove2 = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.MoPWorldName);
                        User.System.LogonProcessesID.Remove(processToRemove2);
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.MoPWorldName}!";
                        User.UI.Form.MOPWorldStarted = false;
                        break;
                    }
                }
            }
            if (User.UI.Form.MOPLogonStarted && !User.UI.Form.MOPLogonRunning)
            {
                while (Attempt.MopLogon < Attempts)
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
                        await Task.Delay(3000); //This need tunning
                        Attempt.MopLogon++;
                        Infos.Message = $"{Setting.List.MoPLogonName} recover attempt:{Attempt.MopLogon}";
                        break;
                    }
                    catch (Exception ex)
                    {
                        Infos.Message = $"Attempt {Attempt.MopLogon} failed: {ex.Message}";
                        // Optionally, wait before retrying
                        await Task.Delay(3000); //This need tunning
                        Attempt.MopLogon++;
                    }
                    if (Attempt.MopLogon == Attempts - 1)
                    {
                        var processToRemove2 = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.MoPLogonName);
                        User.System.LogonProcessesID.Remove(processToRemove2);
                        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.MoPLogonName}!";
                        User.UI.Form.MOPLogonStarted = false;
                        break;
                    }
                }
            }
            if (User.UI.Form.DBStarted && !User.UI.Form.DBRunning)
            {
                //await Task.Delay(20000);
                //while (Attempt.Database < Attempts)
                //{
                //    User.System.DatabaseProcessID.Clear();
                //    try
                //    {
                //        Setting.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                //        string arg = $@"--defaults-file={Directory.GetCurrentDirectory()}/my.ini --console";
                //        await StartDatabase(arg);
                //        await Task.Delay(20000);
                //        break;
                //    }
                //    catch (Exception ex)
                //    {
                //        User.System.DatabaseProcessID.Clear();
                //        Infos.Message = $"Attempt {Attempt.Database} failed: {ex.Message}";
                //        // Optionally, wait before retrying
                //        await Task.Delay(20000); //This need tunning
                //    }
                //    if (Attempt.Database == Attempts - 1)
                //    {
                //        User.System.DatabaseProcessID.Clear();
                //        User.UI.Form.DBStarted = false;
                //        Infos.Message = $"Max Attempts reached, Stopping {Setting.List.DBExeName}!";
                //        break;
                //    }
                //}
            }
        }
    }
}
