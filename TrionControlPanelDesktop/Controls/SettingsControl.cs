using System.Reflection;
using System.Diagnostics;
using static TrionLibrary.Models.Enums;
using TrionLibrary.Setting;
using TrionControlPanelDesktop.Data;
using TrionLibrary.Sys;
using TrionLibrary.Database;
using TrionLibrary.Network;

namespace TrionControlPanelDesktop.Controls
{
    public partial class SettingsControl : UserControl
    {
        bool readFiles = false;
        static System.Threading.Timer TextTimer;
        //
        public SettingsControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            ComboBoxCores.Items.AddRange(Enum.GetNames(typeof(Cores)));
            ComboBoxSPPVersion.Items.AddRange(Enum.GetNames(typeof(SPP)));
            ComboBoxDDNService.Items.AddRange(Enum.GetNames(typeof(DDNSerivce)));
            _ = LoadData();
        }
        private void EnableCustomNames()
        {
            TXTBoxLoginExecName.ReadOnly = !Setting.List.CustomNames;
            TXTBoxWorldExecName.ReadOnly = !Setting.List.CustomNames;
            TXTBoxMySQLExecName.ReadOnly = !Setting.List.CustomNames;
        }
        private async Task LoadData()
        {
            //Load Installed Emulators
            TGLClassicInstalled.Checked = Setting.List.ClassicInstalled;
            TGLTBCInstalled.Checked = Setting.List.TBCInstalled;
            TGLWotLKInstalled.Checked = Setting.List.WotLKInstalled;
            TGLCataInstalled.Checked = Setting.List.CataInstalled;
            TGLMoPInstalled.Checked = Setting.List.MOPInstalled;
            //
            ComboBoxCores.OnSelectedIndexChanged -= ComboBoxCores_OnSelectedIndexChanged;
            ComboBoxDDNService.OnSelectedIndexChanged -= ComboBoxCores_OnSelectedIndexChanged;
            ComboBoxSPPVersion.OnSelectedIndexChanged -= ComboBoxCores_OnSelectedIndexChanged;
            ComboBoxCores.SelectedItem = Setting.List.SelectedCore.ToString();
            ComboBoxDDNService.SelectedItem = Setting.List.DDNSerivce.ToString();
            ComboBoxSPPVersion.SelectedItem = Setting.List.SelectedSPP.ToString();
            ComboBoxCores.OnSelectedIndexChanged += ComboBoxCores_OnSelectedIndexChanged;
            ComboBoxDDNService.OnSelectedIndexChanged += ComboBoxCores_OnSelectedIndexChanged;
            ComboBoxSPPVersion.OnSelectedIndexChanged += ComboBoxCores_OnSelectedIndexChanged;
            //Load Names
            TXTBoxLoginExecName.Text = Setting.List.CustomLogonExeName;
            TXTBoxWorldExecName.Text = Setting.List.CustomWorldExeName;
            TXTBoxMySQLExecName.Text = Setting.List.DBExecutableName;
            //Working Directory
            TXTBoxCoreLocation.Text = Setting.List.CustomWorkingDirectory;
            TXTBoxMySQLLocation.Text = Setting.List.DBLocation;
            //MySQL Host Data
            TXTMysqlHost.Text = Setting.List.DBServerHost;
            TXTMysqlPort.Text = Setting.List.DBServerPort;
            TXTMysqlUser.Text = Setting.List.DBServerUser;
            TXTMysqlPassword.Text = Setting.List.DBServerPassword;
            //MysqlDatabases
            TXTCharDatabase.Text = Setting.List.CharactersDatabase;
            TXTAuthDatabase.Text = Setting.List.AuthDatabase;
            TXTWorldDatabase.Text = Setting.List.WorldDatabase;
            //ToggleButtons
            TGLAutoUpdateTrion.Checked = Setting.List.AutoUpdateTrion;
            TGLAutoUpdateCore.Checked = Setting.List.AutoUpdateCore;
            TGLHideConsole.Checked = Setting.List.ConsolHide;
            TGLNotificationSound.Checked = Setting.List.NotificationSound;
            TGLStayInTrey.Checked = Setting.List.StayInTray;
            TGLCustomNames.Checked = Setting.List.CustomNames;
            TGLRunTrionStartup.Checked = Setting.List.RunWithWindows;
            //Update Loader
            EnableCustomNames();
            //Version Load
            User.UI.Version.OFF.Trion = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
            User.UI.Version.OFF.Database = Infos.Version.Local(Setting.List.DBExeLoca);
            User.UI.Version.OFF.Classic = Infos.Version.Local(Setting.List.ClassicWorldExeLoc);
            User.UI.Version.OFF.TBC =  Infos.Version.Local(Setting.List.TBCDBExeLoca);
            User.UI.Version.OFF.WotLK = Infos.Version.Local(Setting.List.WotLKDBExeLoca);
            User.UI.Version.OFF.Cata = Infos.Version.Local(Setting.List.CataDBExeLoca);
            User.UI.Version.OFF.Mop = Infos.Version.Local(Setting.List.MopDBExeLoca);
            User.UI.Version.ON.Trion = await Infos.Version.Online(Links.Version.Trion);
            User.UI.Version.ON.Database = await Infos.Version.Online(Links.Version.Database);
            User.UI.Version.ON.Classic = await Infos.Version.Online(Links.Version.Classic);
            User.UI.Version.ON.TBC = await Infos.Version.Online(Links.Version.TBC);
            User.UI.Version.ON.WotLK = await Infos.Version.Online(Links.Version.WotLK);
            User.UI.Version.ON.Cata = await Infos.Version.Online(Links.Version.Cata);
            User.UI.Version.ON.Mop = await Infos.Version.Online(Links.Version.Mop);
            //Update Labels
            LBLTrionVersion.Text = $"Trion Version: Local {User.UI.Version.OFF.Trion} / Online: {User.UI.Version.ON.Trion}";
            LBLDBVersion.Text = $"Database Version: \n •Local: {User.UI.Version.OFF.Database} \n •Online: {User.UI.Version.ON.Database} ";
            LBLClassicVersion.Text = $"Classic Version: \n •Local: {User.UI.Version.OFF.Classic} \n •Online: {User.UI.Version.OFF.Classic} ";
            LBLTBCVersion.Text = $"TBC Version: \n •Local: {User.UI.Version.OFF.TBC} \n •Online: {User.UI.Version.OFF.TBC} ";
            LBLWotLKVersion.Text = $"WotLK Version: \n •Local: {User.UI.Version.OFF.WotLK} \n •Online: {User.UI.Version.OFF.WotLK} ";
            LBLCataVersion.Text = $"Cata Version: \n •Local: {User.UI.Version.OFF.Cata} \n •Online: {User.UI.Version.OFF.Cata} ";
            LBLMoPVersion.Text = $"MoP Version: \n •Local: {User.UI.Version.OFF.Mop} \n •Online: {User.UI.Version.OFF.Mop} ";
            //DDNS
            TXTDDNSDomain.Text = Setting.List.DDNSDomain;
            TXTDDNSUsername.Text = Setting.List.DDNSUsername;
            TXTDDNSPassword.Text = Setting.List.DDNSPassword;
            TXTDDNSInterval.Text = Setting.List.DDNSInterval.ToString();
            TGLDDNSRunOnStartup.Checked = Setting.List.DDNSRunOnStartup;
            User.UI.Form.StartUpLoading++;
        }
        private async void ComboBoxCores_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBoxCores.SelectedItem)
            {
                case "Asc Emu":
                    Setting.List.CustomWorldExeName = "world";
                    Setting.List.CustomLogonExeName = "logon";
                    Setting.List.SelectedCore = Cores.AscEmu;
                    Setting.List.CharactersDatabase = "ascemu_char";
                    Setting.List.WorldDatabase = "ascemu_world";
                    Setting.List.AuthDatabase = "ascemu_logon";
                    Setting.List.DBServerUser = "root";
                    Setting.List.DBServerPassword = "root";
                    break;
                case "AzerothCore":
                    Setting.List.CustomWorldExeName = "worldserver";
                    Setting.List.CustomLogonExeName = "authserver";
                    Setting.List.SelectedCore = Cores.AzerothCore;
                    Setting.List.CharactersDatabase = "acore_characters";
                    Setting.List.WorldDatabase = "acore_world";
                    Setting.List.AuthDatabase = "acore_auth";
                    Setting.List.DBServerUser = "acore";
                    Setting.List.DBServerPassword = "acore";
                    break;
                case "CMaNGOS":
                    Setting.List.CustomWorldExeName = "mangosd";
                    Setting.List.CustomLogonExeName = "realmd";
                    Setting.List.SelectedCore = Cores.CMaNGOS;
                    Setting.List.CharactersDatabase = "characters";
                    Setting.List.WorldDatabase = "mangos";
                    Setting.List.AuthDatabase = "realmd";
                    Setting.List.DBServerUser = "mangos";
                    Setting.List.DBServerPassword = "mangos";
                    break;
                case "CypherCore":
                    Setting.List.CustomWorldExeName = "WorldServer";
                    Setting.List.CustomLogonExeName = "BNetServer";
                    Setting.List.SelectedCore = Cores.CypherCore;
                    Setting.List.CharactersDatabase = "characters";
                    Setting.List.WorldDatabase = "world";
                    Setting.List.AuthDatabase = "auth";
                    Setting.List.DBServerUser = "trinity";
                    Setting.List.DBServerPassword = "trinity";
                    break;
                case "TrinityCore":
                    Setting.List.CustomWorldExeName = "worldserver";
                    Setting.List.CustomLogonExeName = "bnetserver";
                    Setting.List.SelectedCore = Cores.TrinityCore;
                    Setting.List.CharactersDatabase = "characters";
                    Setting.List.WorldDatabase = "world";
                    Setting.List.AuthDatabase = "auth";
                    Setting.List.DBServerUser = "trinity";
                    Setting.List.DBServerPassword = "trinity";
                    break;
                case "TrinityCore335":
                    Setting.List.CustomLogonExeName = "authserver";
                    Setting.List.CustomWorldExeName = "worldserver";
                    Setting.List.SelectedCore = Cores.TrinityCore335;
                    Setting.List.CharactersDatabase = "characters";
                    Setting.List.WorldDatabase = "world";
                    Setting.List.AuthDatabase = "auth";
                    Setting.List.DBServerUser = "trinity";
                    Setting.List.DBServerPassword = "trinity";
                    break;
                case "TrinityCoreClassic":
                    Setting.List.CustomLogonExeName = "bnetserver";
                    Setting.List.CustomWorldExeName = "worldserver";
                    Setting.List.SelectedCore = Cores.TrinityCoreClassic;
                    Setting.List.CharactersDatabase = "characters";
                    Setting.List.WorldDatabase = "world";
                    Setting.List.AuthDatabase = "auth";
                    Setting.List.DBServerUser = "trinity";
                    Setting.List.DBServerPassword = "trinity";
                    break;
                case "VMaNGOS":
                    Setting.List.CustomWorldExeName = "mangosd";
                    Setting.List.CustomLogonExeName = "realmd";
                    Setting.List.SelectedCore = Cores.VMaNGOS;
                    Setting.List.CharactersDatabase = "characters";
                    Setting.List.WorldDatabase = "mangos";
                    Setting.List.AuthDatabase = "realmd";
                    Setting.List.DBServerUser = "mangos";
                    Setting.List.DBServerPassword = "mangos";
                    break;
            }
            TXTBoxLoginExecName.Text = Setting.List.CustomLogonExeName;
            TXTBoxWorldExecName.Text = Setting.List.CustomWorldExeName;
            await LoadData();
            Infos.Message = $"The core has been changed to {ComboBoxCores.SelectedItem}";
        }
        private void TGLStayInTrey_CheckedChanged(object sender, EventArgs e)
        {
            Setting.List.StayInTray = TGLStayInTrey.Checked;
        }
        private void TGLNotificationSound_CheckedChanged(object sender, EventArgs e)
        {
            Setting.List.NotificationSound = TGLNotificationSound.Checked;
        }
        private void TGLHideConsole_CheckedChanged(object sender, EventArgs e)
        {
            Setting.List.ConsolHide = TGLHideConsole.Checked;
        }
        private void TGLAutoUpdateTrion_CheckedChanged(object sender, EventArgs e)
        {
            Setting.List.AutoUpdateTrion = TGLAutoUpdateTrion.Checked;
        }
        private void TGLAutoUpdateCore_CheckedChanged(object sender, EventArgs e)
        {
            Setting.List.AutoUpdateCore = TGLAutoUpdateCore.Checked;
        }
        private void TGLAutoUpdateMySQL_CheckedChanged(object sender, EventArgs e)
        {
            Setting.List.AutoUpdateMySQL = TGLAutoUpdateMySQL.Checked;
        }
        private void TGLCustomNames_CheckedChanged(object sender, EventArgs e)
        {
            Setting.List.CustomNames = TGLCustomNames.Checked;
            EnableCustomNames();
        }
        private void TGLServerStartup_CheckedChanged(object sender, EventArgs e)
        {
            Setting.List.RunServerWithWindows = TGLServerStartup.Checked;
        }
        private async void BTNMySQLExecLovation_Click(object sender, EventArgs e)
        {
            string Folder = Settings.GetWorkingDirectory();
            try
            {
                if (Folder != string.Empty)
                {
                    if (Infos.GetExecutableLocation(Folder, Setting.List.DBExecutableName) != string.Empty)
                    {
                        Setting.List.DBExeLoca = Infos.GetExecutableLocation(Folder, Setting.List.DBExecutableName);
                        Setting.List.DBLocation = Path.GetFullPath(Path.Combine(Setting.List.DBExeLoca, @"..\"));
                        await Setting.Save();
                        Setting.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                        await Setting.Save();
                        await LoadData();
                    }
                    else
                    {
                        Infos.Message = "Faild to find MySQL Lication.";
                    }
                }
                else
                {
                    Infos.Message = "Faild to select the folder.";
                }
            }
            catch (Exception ex)
            {
                Infos.Message = ex.Message;
            }
        }
        private async void BTNCoreExecLovation_Click(object sender, EventArgs e)
        {
            string Folder = Settings.GetWorkingDirectory();
            if (Folder != string.Empty)
            {
                if (Infos.GetExecutableLocation(Folder, Setting.List.CustomWorldExeName) != string.Empty &&
                    Infos.GetExecutableLocation(Folder, Setting.List.CustomLogonExeName) != string.Empty)
                {
                    Setting.List.CustomWorldExeLoc = Infos.GetExecutableLocation(Folder, Setting.List.CustomWorldExeName);
                    Setting.List.CustomWorldExeLoc = Infos.GetExecutableLocation(Folder, Setting.List.CustomLogonExeName);
                    Setting.List.CustomWorkingDirectory = Path.GetFullPath(Folder);
                    await Setting.Save();
                    await LoadData();
                }
            }
            else
            {
                Infos.Message = "Getting Core File location failed!";
            }
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {

            if (User.UI.Form.LoadData == true)
            {
                User.UI.Form.LoadData = false;
                _ = LoadData();
            }
            if (readFiles)
            {
                BTNInstallSPP.Enabled = false;
                BTNRepairSPP.Enabled = false;
                BTNUninstallSPP.Enabled = false;
                PBarReadingFiles.Visible = true;
                LBLReadingFiles.Visible = true;
            }
            else
            {
                BTNInstallSPP.Enabled = true;
                BTNRepairSPP.Enabled = true;
                BTNUninstallSPP.Enabled = true;
                PBarReadingFiles.Visible = false;
                LBLReadingFiles.Visible = false;
            }
        }
        private void BTNDiscord_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Links.Discord);
        }
        private void SaveDataTextbox(object state)
        {
            // Seve data
            Setting.List.DBExecutableName = TXTBoxMySQLExecName.Text;
            Setting.List.CustomWorldExeName = TXTBoxWorldExecName.Text;
            Setting.List.CustomLogonExeName = TXTBoxLoginExecName.Text;
            Setting.List.DBServerHost = TXTMysqlHost.Text;
            Setting.List.DBServerPort = TXTMysqlPort.Text;
            Setting.List.DBServerUser = TXTMysqlUser.Text;
            Setting.List.DBServerPassword = TXTMysqlPassword.Text;
            Setting.List.AuthDatabase = TXTAuthDatabase.Text;
            Setting.List.WorldDatabase = TXTWorldDatabase.Text;
            Setting.List.CharactersDatabase = TXTCharDatabase.Text;
            Setting.List.DDNSDomain = TXTDDNSDomain.Text;
            Setting.List.DDNSUsername = TXTDDNSUsername.Text;
            Setting.List.DDNSPassword = TXTDDNSPassword.Text;
            Setting.List.DDNSInterval = Convert.ToInt32(TXTDDNSInterval.Text);
        }
        private void TXTBox_TextChanged(object sender, EventArgs e)
        {
            // Stop the timer if it's running
            TextTimer?.Dispose();
            // Start a new timer
            TextTimer = new(SaveDataTextbox, null, 1000, Timeout.Infinite);
        }
        private void BTNTrionUpdate_Click(object sender, EventArgs e)
        {
        }
        private void TGLRunTrionStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (TGLRunTrionStartup.Checked == true) { Settings.AddToStartup("Trion Control Panel", Application.ExecutablePath.ToString()); Setting.List.RunWithWindows = true; }
            if (TGLRunTrionStartup.Checked == false) { Settings.RemoveFromStartup("Trion Control Panel"); Setting.List.RunWithWindows = false; }
        }
        private async void BTNTestConnection_Click(object sender, EventArgs e)
        {
            if (await Connect.Test() == true)
            {
                BTNTestConnection.ForeColor = Color.Green;
                BTNTestConnection.Text = "   Success!!";
                TimerConnectSucess.Start();
            }
            else
            {
                BTNTestConnection.ForeColor = Color.Red;
                BTNTestConnection.Text = "   Failed!!";
                TimerConnectSucess.Start();
            }
        }
        private void TimerConnectSucess_Tick(object sender, EventArgs e)
        {
            TimerConnectSucess.Stop();
            BTNTestConnection.ForeColor = Color.White;
            BTNTestConnection.Text = "   Test Connection";
        }
        private void BTNCoreOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Setting.List.CustomWorkingDirectory);
        }
        private void BTNMySQLOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Setting.List.DBLocation);
        }
        private async void BTNDeleteAuth_Click(object sender, EventArgs e)
        {
            BTNDeleteAuth.Text = "   Working!!";
            BTNDeleteAuth.ForeColor = Color.Orange;
            BTNDeleteAuth.Click -= BTNDeleteAuth_Click;
            // Get all tables in the database
            List<string> tables = await Access.GetTables(Connect.String(Setting.List.AuthDatabase));
            // Delete each table
            foreach (string table in tables)
            {
                Access.DeleteTable(Connect.String(Setting.List.AuthDatabase), table);
            }
            Infos.Message = "Auth Tables deleted successfully.";
            BTNDeleteAuth.Click += BTNDeleteAuth_Click;
            BTNDeleteAuth.Text = "   Delete Auth Database";
            BTNDeleteAuth.ForeColor = Color.White;
        }
        private async void BTNDeleteChar_Click(object sender, EventArgs e)
        {
            BTNDeleteChar.Text = "   Working!!";
            BTNDeleteChar.ForeColor = Color.Orange;
            BTNDeleteChar.Click -= BTNDeleteChar_Click;
            // Get all tables in the database
            List<string> tables = await Access.GetTables(Connect.String(Setting.List.AuthDatabase));
            // Delete each table
            foreach (string table in tables)
            {
                Access.DeleteTable(Connect.String(Setting.List.AuthDatabase), table);
            }
            Infos.Message = "Char Tables deleted successfully.";
            BTNDeleteChar.Click += BTNDeleteChar_Click;
            BTNDeleteChar.Text = "   Delete Char Database";
            BTNDeleteChar.ForeColor = Color.White;
        }
        private async void BTNDeleteWorld_Click(object sender, EventArgs e)
        {
            BTNDeleteWorld.Text = "   Working!!";
            BTNDeleteWorld.ForeColor = Color.Orange;
            BTNDeleteWorld.Click -= BTNDeleteWorld_Click;
            // Get all tables in the database
            List<string> tables = await Access.GetTables(Connect.String(Setting.List.WorldDatabase));
            // Delete each table
            foreach (string table in tables)
            {
                Access.DeleteTable(Connect.String(Setting.List.WorldDatabase), table);
            }
            Infos.Message = "Wolrd Tables deleted successfully.";
            BTNDeleteWorld.Click += BTNDeleteWorld_Click;
            BTNDeleteWorld.Text = "   Delete World Database";
            BTNDeleteWorld.ForeColor = Color.White;
        }
        private void TimerEnDis_Tick(object sender, EventArgs e)
        {
        }
        private void BTNFixMysql_Click(object sender, EventArgs e)
        {

        }
        private void ComboBoxDDNService_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBoxDDNService.SelectedItem)
            {
                case "DuckDNS":
                    Setting.List.DDNSerivce = DDNSerivce.DuckDNS;
                    break;
                case "NoIP":
                    Setting.List.DDNSerivce = DDNSerivce.NoIP;
                    break;
                case "Dynu":
                    Setting.List.DDNSerivce = DDNSerivce.Dynu;
                    break;
                case "Enom":
                    Setting.List.DDNSerivce = DDNSerivce.Enom;
                    break;
                case "AllInkl":
                    Setting.List.DDNSerivce = DDNSerivce.AllInkl;
                    break;
                case "dynDNS":
                    Setting.List.DDNSerivce = DDNSerivce.dynDNS;
                    break;
                case "STRATO":
                    Setting.List.DDNSerivce = DDNSerivce.STRATO;
                    break;
                case "Freemyip":
                    Setting.List.DDNSerivce = DDNSerivce.Freemyip;
                    break;
                case "Afraid":
                    Setting.List.DDNSerivce = DDNSerivce.Afraid;
                    break;
                case "OVH":
                    Setting.List.DDNSerivce = DDNSerivce.OVH;
                    break;
            }
        }
        private void TXTDDNSInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar);
        }
        private void TGLDDNSRunOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            Setting.List.DDNSRunOnStartup = TGLDDNSRunOnStartup.Checked;
        }
        private async void TimerDDNSInterval_Tick(object sender, EventArgs e)
        {
            TimerDDNSInterval.Interval = Setting.List.DDNSInterval;
            string CurrentIP = await Helper.GetExternalIpAddress();
            string URL = await User.API.DDNSUpdateURL(TXTDDNSDomain.Text, TXTDDNSUsername.Text, TXTDDNSPassword.Text);
            if (!CurrentIP.Contains(Setting.List.IPAddress))
            {
                Infos.Message = $"Updateing {URL} with {CurrentIP}";
                Settings.UpdateDNSIP(URL, CurrentIP);
            }
        }
        private void BTNSaveData_Click(object sender, EventArgs e)
        {
            TimerDDNSInterval.Start();
        }
        private void BTNWebiste_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Links.DDNSWebsits());
        }

        private void ComboBoxSPPVersion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBoxSPPVersion.SelectedItem)
            {
                case "Classic":
                    Setting.List.SelectedSPP = SPP.Classic;

                    break;
                case "TheBurningCrusade":
                    Setting.List.SelectedSPP = SPP.TheBurningCrusade;
                    break;
                case "WrathOfTheLichKing":
                    Setting.List.SelectedSPP = SPP.WrathOfTheLichKing;
                    break;
                case "Cataclysm":
                    Setting.List.SelectedSPP = SPP.Cataclysm;
                    break;
                case "MistOfPandaria":
                    Setting.List.SelectedSPP = SPP.MistOfPandaria;
                    break;
            }
        }

        private void BTNInstallSPP_Click(object sender, EventArgs e)
        {
            Thread DwonloadThread = new(async () =>
            {
                try
                {
                }
                catch (Exception ex)
                {
                    Infos.Message = ex.Message;
                }
                switch (Setting.List.SelectedSPP)
                    {
                        case SPP.Classic:

                            break;
                        case SPP.TheBurningCrusade:

                            break;
                        case SPP.WrathOfTheLichKing:
                            readFiles = true;
                            if (!Directory.Exists(Links.Install.WotLK)) { Directory.CreateDirectory(Links.Install.WotLK); }
                            DownloadControl.Title = "Install Single Player Project - Wrath of the Lich King";
                            await Download.CompareAndExportChangesOnline(Links.Install.WotLK, $"{Links.MainHost}{Links.Hashe.WotLK}");
                            break;
                        case SPP.Cataclysm:

                            break;
                        case SPP.MistOfPandaria:

                            break;
                    }
            });
            DwonloadThread.Start();
        }

        private void BTNRepairSPP_Click(object sender, EventArgs e)
        {

        }

        private void BTNUninstallSPP_Click(object sender, EventArgs e)
        {

        }
    }
}
