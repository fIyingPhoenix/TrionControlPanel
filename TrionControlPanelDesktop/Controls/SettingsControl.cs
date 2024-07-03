using System.Reflection;
using System.Diagnostics;
using static TrionLibrary.EnumModels;
using TrionLibrary;
using TrionDatabase;
using TrionControlPanelDesktop.Classes;
using TrionControlPanelDesktop.FormData;
using MetroFramework;

namespace TrionControlPanelDesktop.Controls
{
    public partial class SettingsControl : UserControl
    {
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
            TXTBoxLoginExecName.ReadOnly = !Data.Settings.CustomNames;
            TXTBoxWorldExecName.ReadOnly = !Data.Settings.CustomNames;
            TXTBoxMySQLExecName.ReadOnly = !Data.Settings.CustomNames;
        }
        private async Task LoadData()
        {
            ComboBoxCores.OnSelectedIndexChanged -= ComboBoxCores_OnSelectedIndexChanged;
            ComboBoxCores.SelectedItem = Data.Settings.SelectedCore.ToString();
            ComboBoxDDNService.SelectedItem = Data.Settings.DDNSerivce.ToString();
            ComboBoxCores.OnSelectedIndexChanged += ComboBoxCores_OnSelectedIndexChanged;
            //Load Names
            TXTBoxLoginExecName.Text = Data.Settings.CustomLogonExeName;
            TXTBoxWorldExecName.Text = Data.Settings.CustomWorldExeName;
            TXTBoxMySQLExecName.Text = Data.Settings.DBExecutableName;
            //Working Directory
            TXTBoxCoreLocation.Text = Data.Settings.CustomWorkingDirectory;
            TXTBoxMySQLLocation.Text = Data.Settings.DBLocation;
            //MySQL Host Data
            TXTMysqlHost.Text = Data.Settings.DBServerHost;
            TXTMysqlPort.Text = Data.Settings.DBServerPort;
            TXTMysqlUser.Text = Data.Settings.DBServerUser;
            TXTMysqlPassword.Text = Data.Settings.DBServerPassword;
            //MysqlDatabases
            TXTCharDatabase.Text = Data.Settings.CharactersDatabase;
            TXTAuthDatabase.Text = Data.Settings.AuthDatabase;
            TXTWorldDatabase.Text = Data.Settings.WorldDatabase;
            //ToggleButtons
            TGLAutoUpdateTrion.Checked = Data.Settings.AutoUpdateTrion;
            TGLAutoUpdateCore.Checked = Data.Settings.AutoUpdateCore;
            TGLHideConsole.Checked = Data.Settings.ConsolHide;
            TGLNotificationSound.Checked = Data.Settings.NotificationSound;
            TGLStayInTrey.Checked = Data.Settings.StayInTray;
            TGLCustomNames.Checked = Data.Settings.CustomNames;
            TGLRunTrionStartup.Checked = Data.Settings.RunWithWindows;
            //Update Loader
            EnableCustomNames();
            //Version Load
            User.UI.Version.OFF.Trion = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
            User.UI.Version.OFF.Database = Data.Version.GetLocal(Data.Settings.DBExeLoca, Data.Settings.DBExecutableName);
            User.UI.Version.ON.Trion = await Data.Version.GetOnline(WebLinks.Version.Trion);
            User.UI.Version.ON.Database = await Data.Version.GetOnline(WebLinks.Version.Database);
            User.UI.Version.ON.Classic = await Data.Version.GetOnline(WebLinks.Version.Classic);
            User.UI.Version.ON.TBC = await Data.Version.GetOnline(WebLinks.Version.TBC);
            User.UI.Version.ON.WotLK = await Data.Version.GetOnline(WebLinks.Version.WotLK);
            User.UI.Version.ON.Cata = await Data.Version.GetOnline(WebLinks.Version.Cata);
            User.UI.Version.ON.Mop = await Data.Version.GetOnline(WebLinks.Version.Mop);
            //Update Labels
            LBLTrionVersion.Text = $"Trion Version: Local {User.UI.Version.OFF.Trion} / Online: {User.UI.Version.ON.Trion}";
            LBLDBVersion.Text = $"Database Version: \n •Local: {User.UI.Version.OFF.Database} \n •Online: {User.UI.Version.ON.Database} ";
            LBLClassicVersion.Text = $"Classic Version: \n •Local: {User.UI.Version.OFF.Classic} \n •Online: {User.UI.Version.OFF.Classic} ";
            LBLTBCVersion.Text = $"TBC Version: \n •Local: {User.UI.Version.OFF.TBC} \n •Online: {User.UI.Version.OFF.TBC} ";
            LBLWotLKVersion.Text = $"WotLK Version: \n •Local: {User.UI.Version.OFF.WotLK} \n •Online: {User.UI.Version.OFF.WotLK} ";
            LBLCataVersion.Text = $"Cata Version: \n •Local: {User.UI.Version.OFF.Cata} \n •Online: {User.UI.Version.OFF.Cata} ";
            LBLMoPVersion.Text = $"MoP Version: \n •Local: {User.UI.Version.OFF.Mop} \n •Online: {User.UI.Version.OFF.Mop} ";
            //DDNS
            TXTDDNSDomain.Text = Data.Settings.DDNSDomain;
            TXTDDNSUsername.Text = Data.Settings.DDNSUsername;
            TXTDDNSPassword.Text = Data.Settings.DDNSPassword;
            TXTDDNSInterval.Text = Data.Settings.DDNSInterval.ToString();
            TGLDDNSRunOnStartup.Checked = Data.Settings.DDNSRunOnStartup;
            User.UI.Form.StartUpLoading++;
        }
        private async void ComboBoxCores_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBoxCores.SelectedItem)
            {
                case "AscEmu":
                    Data.Settings.CustomWorldExeName = "world";
                    Data.Settings.CustomLogonExeName = "logon";
                    Data.Settings.SelectedCore = Cores.AscEmu;
                    Data.Settings.CharactersDatabase = "ascemu_char";
                    Data.Settings.WorldDatabase = "ascemu_world";
                    Data.Settings.AuthDatabase = "ascemu_logon";
                    Data.Settings.DBServerUser = "root";
                    Data.Settings.DBServerPassword = "root";
                    break;
                case "AzerothCore":
                    Data.Settings.CustomWorldExeName = "worldserver";
                    Data.Settings.CustomLogonExeName = "authserver";
                    Data.Settings.SelectedCore = Cores.AzerothCore;
                    Data.Settings.CharactersDatabase = "acore_characters";
                    Data.Settings.WorldDatabase = "acore_world";
                    Data.Settings.AuthDatabase = "acore_auth";
                    Data.Settings.DBServerUser = "acore";
                    Data.Settings.DBServerPassword = "acore";
                    break;
                case "CMaNGOS":
                    Data.Settings.CustomWorldExeName = "mangosd";
                    Data.Settings.CustomLogonExeName = "realmd";
                    Data.Settings.SelectedCore = Cores.CMaNGOS;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "mangos";
                    Data.Settings.AuthDatabase = "realmd";
                    Data.Settings.DBServerUser = "mangos";
                    Data.Settings.DBServerPassword = "mangos";
                    break;
                case "CypherCore":
                    Data.Settings.CustomWorldExeName = "WorldServer";
                    Data.Settings.CustomLogonExeName = "BNetServer";
                    Data.Settings.SelectedCore = Cores.CypherCore;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "world";
                    Data.Settings.AuthDatabase = "auth";
                    Data.Settings.DBServerUser = "trinity";
                    Data.Settings.DBServerPassword = "trinity";
                    break;
                case "TrinityCore":
                    Data.Settings.CustomWorldExeName = "worldserver";
                    Data.Settings.CustomLogonExeName = "bnetserver";
                    Data.Settings.SelectedCore = Cores.TrinityCore;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "world";
                    Data.Settings.AuthDatabase = "auth";
                    Data.Settings.DBServerUser = "trinity";
                    Data.Settings.DBServerPassword = "trinity";
                    break;
                case "TrinityCore335":
                    Data.Settings.CustomLogonExeName = "authserver";
                    Data.Settings.CustomWorldExeName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCore335;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "world";
                    Data.Settings.AuthDatabase = "auth";
                    Data.Settings.DBServerUser = "trinity";
                    Data.Settings.DBServerPassword = "trinity";
                    break;
                case "TrinityCoreClassic":
                    Data.Settings.CustomLogonExeName = "bnetserver";
                    Data.Settings.CustomWorldExeName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCoreClassic;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "world";
                    Data.Settings.AuthDatabase = "auth";
                    Data.Settings.DBServerUser = "trinity";
                    Data.Settings.DBServerPassword = "trinity";
                    break;
                case "VMaNGOS":
                    Data.Settings.CustomWorldExeName = "mangosd";
                    Data.Settings.CustomLogonExeName = "realmd";
                    Data.Settings.SelectedCore = Cores.VMaNGOS;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "mangos";
                    Data.Settings.AuthDatabase = "realmd";
                    Data.Settings.DBServerUser = "mangos";
                    Data.Settings.DBServerPassword = "mangos";
                    break;
            }
            TXTBoxLoginExecName.Text = Data.Settings.CustomLogonExeName;
            TXTBoxWorldExecName.Text = Data.Settings.CustomWorldExeName;
            await LoadData();
            Data.Message = $"The core has been changed to {ComboBoxCores.SelectedItem}";
        }
        private void TGLStayInTrey_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.StayInTray = TGLStayInTrey.Checked;
        }
        private void TGLNotificationSound_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.NotificationSound = TGLNotificationSound.Checked;
        }
        private void TGLHideConsole_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.ConsolHide = TGLHideConsole.Checked;
        }
        private void TGLAutoUpdateTrion_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.AutoUpdateTrion = TGLAutoUpdateTrion.Checked;
        }
        private void TGLAutoUpdateCore_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.AutoUpdateCore = TGLAutoUpdateCore.Checked;
        }
        private void TGLAutoUpdateMySQL_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.AutoUpdateMySQL = TGLAutoUpdateMySQL.Checked;
        }
        private void TGLCustomNames_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.CustomNames = TGLCustomNames.Checked;
            EnableCustomNames();
        }
        private void TGLServerStartup_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.RunServerWithWindows = TGLServerStartup.Checked;
        }
        private async void BTNMySQLExecLovation_Click(object sender, EventArgs e)
        {
            string Folder = SettingsClass.GetWorkingDirectory();
            try
            {
                if (Folder != string.Empty)
                {
                    if (Data.GetExecutableLocation(Folder, Data.Settings.DBExecutableName) != string.Empty)
                    {
                        Data.Settings.DBExeLoca = Data.GetExecutableLocation(Folder, Data.Settings.DBExecutableName);
                        Data.Settings.DBLocation = Path.GetFullPath(Path.Combine(Data.Settings.DBExeLoca, @"..\"));
                        await Data.SaveSettings();
                        Data.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                        await Data.SaveSettings();
                        await LoadData();
                    }
                    else
                    {
                        Data.Message = "Faild to find MySQL Lication.";
                    }
                }
                else
                {
                    Data.Message = "Faild to select the folder.";
                }
            }
            catch (Exception ex)
            {
                Data.Message = ex.Message;
            }
        }
        private async void BTNCoreExecLovation_Click(object sender, EventArgs e)
        {
            string Folder = SettingsClass.GetWorkingDirectory();
            if (Folder != string.Empty)
            {
                if (Data.GetExecutableLocation(Folder, Data.Settings.CustomWorldExeName) != string.Empty &&
                    Data.GetExecutableLocation(Folder, Data.Settings.CustomLogonExeName) != string.Empty)
                {
                    Data.Settings.CustomWorldExeLoc = Data.GetExecutableLocation(Folder, Data.Settings.CustomWorldExeName);
                    Data.Settings.CustomWorldExeLoc = Data.GetExecutableLocation(Folder, Data.Settings.CustomLogonExeName);
                    Data.Settings.CustomWorkingDirectory = Path.GetFullPath(Folder);
                    await Data.SaveSettings();
                    await LoadData();
                }
            }
            else
            {
                Data.Message = "Getting Core File location failed!";
            }
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            if (User.UI.Form.LoadData == true)
            {
                User.UI.Form.LoadData = false;
                _ = LoadData();
            }
        }
        private void BtnDownloadSPP_ClickAsync(object sender, EventArgs e)
        {
            Data.Message = "Single Player Project is downloading!";
            DownloadControl.Title = "Installing Single Player Project.";
            DownloadControl.InstallSPP = true;
        }
        private void BTNDownloadMySQL_Click(object sender, EventArgs e)
        {
            Data.Message = "MySQL Server is downloading!";
            DownloadControl.Title = "Installing MySQL Server.";
            DownloadControl.InstallMySQL = true;
        }
        public static async Task CreateBC()
        {
        }
        private void BTNDiscord_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", WebLinks.Discord);
        }
        private void SaveDataTextbox(object state)
        {
            // Seve data
            Data.Settings.DBExecutableName = TXTBoxMySQLExecName.Text;
            Data.Settings.CustomWorldExeName = TXTBoxWorldExecName.Text;
            Data.Settings.CustomLogonExeName = TXTBoxLoginExecName.Text;
            Data.Settings.DBServerHost = TXTMysqlHost.Text;
            Data.Settings.DBServerPort = TXTMysqlPort.Text;
            Data.Settings.DBServerUser = TXTMysqlUser.Text;
            Data.Settings.DBServerPassword = TXTMysqlPassword.Text;
            Data.Settings.AuthDatabase = TXTAuthDatabase.Text;
            Data.Settings.WorldDatabase = TXTWorldDatabase.Text;
            Data.Settings.CharactersDatabase = TXTCharDatabase.Text;
            Data.Settings.DDNSDomain = TXTDDNSDomain.Text;
            Data.Settings.DDNSUsername = TXTDDNSUsername.Text;
            Data.Settings.DDNSPassword = TXTDDNSPassword.Text;
            Data.Settings.DDNSInterval = Convert.ToInt32(TXTDDNSInterval.Text);
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
            if (TGLRunTrionStartup.Checked == true) { SettingsClass.AddToStartup("Trion Control Panel", Application.ExecutablePath.ToString()); Data.Settings.RunWithWindows = true; }
            if (TGLRunTrionStartup.Checked == false) { SettingsClass.RemoveFromStartup("Trion Control Panel"); Data.Settings.RunWithWindows = false; }
        }
        private async void BTNTestConnection_Click(object sender, EventArgs e)
        {
            if (await DataConnect.TestConnection() == true)
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
            Process.Start("explorer.exe", Data.Settings.CustomWorkingDirectory);
        }
        private void BTNMySQLOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Data.Settings.DBLocation);
        }
        private async void BTNDeleteAuth_Click(object sender, EventArgs e)
        {
            BTNDeleteAuth.Text = "   Working!!";
            BTNDeleteAuth.ForeColor = Color.Orange;
            BTNDeleteAuth.Click -= BTNDeleteAuth_Click;
            // Get all tables in the database
            List<string> tables = await DataConnect.GetTables(DataConnect.ConnectionString(Data.Settings.AuthDatabase));
            // Delete each table
            foreach (string table in tables)
            {
                DataConnect.DeleteTable(DataConnect.ConnectionString(Data.Settings.AuthDatabase), table);
            }
            Data.Message = "Auth Tables deleted successfully.";
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
            List<string> tables = await DataConnect.GetTables(DataConnect.ConnectionString(Data.Settings.AuthDatabase));
            // Delete each table
            foreach (string table in tables)
            {
                DataConnect.DeleteTable(DataConnect.ConnectionString(Data.Settings.AuthDatabase), table);
            }
            Data.Message = "Char Tables deleted successfully.";
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
            List<string> tables = await DataConnect.GetTables(DataConnect.ConnectionString(Data.Settings.WorldDatabase));
            // Delete each table
            foreach (string table in tables)
            {
                DataConnect.DeleteTable(DataConnect.ConnectionString(Data.Settings.WorldDatabase), table);
            }
            Data.Message = "Wolrd Tables deleted successfully.";
            BTNDeleteWorld.Click += BTNDeleteWorld_Click;
            BTNDeleteWorld.Text = "   Delete World Database";
            BTNDeleteWorld.ForeColor = Color.White;
        }
        private void TimerEnDis_Tick(object sender, EventArgs e)
        {
            //Enable / Disable buttons.
            if (DownloadControl.InstallSPP == true || DownloadControl.InstallMySQL == true)
            {
                BtnDownloadSPP.Enabled = false;
                BTNDownlaodMySQL.Enabled = false;
            }
            else if (DownloadControl.InstallSPP == false || DownloadControl.InstallMySQL == false)
            {
                BtnDownloadSPP.Enabled = true;
                BTNDownlaodMySQL.Enabled = true;
            }
        }
        private async void BTNFixMysql_Click(object sender, EventArgs e)
        {

        }
        private void ComboBoxDDNService_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBoxDDNService.SelectedItem)
            {
                case "DuckDNS":
                    Data.Settings.DDNSerivce = DDNSerivce.DuckDNS;
                    break;
                case "NoIP":
                    Data.Settings.DDNSerivce = DDNSerivce.NoIP;
                    break;
                case "Dynu":
                    Data.Settings.DDNSerivce = DDNSerivce.Dynu;
                    break;
                case "Enom":
                    Data.Settings.DDNSerivce = DDNSerivce.Enom;
                    break;
                case "AllInkl":
                    Data.Settings.DDNSerivce = DDNSerivce.AllInkl;
                    break;
                case "dynDNS":
                    Data.Settings.DDNSerivce = DDNSerivce.dynDNS;
                    break;
                case "STRATO":
                    Data.Settings.DDNSerivce = DDNSerivce.STRATO;
                    break;
                case "Freemyip":
                    Data.Settings.DDNSerivce = DDNSerivce.Freemyip;
                    break;
                case "Afraid":
                    Data.Settings.DDNSerivce = DDNSerivce.Afraid;
                    break;
                case "OVH":
                    Data.Settings.DDNSerivce = DDNSerivce.OVH;
                    break;
            }
        }
        private void TXTDDNSInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar);
        }
        private void TGLDDNSRunOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.DDNSRunOnStartup = TGLDDNSRunOnStartup.Checked;
        }
        private async void TimerDDNSInterval_Tick(object sender, EventArgs e)
        {
            TimerDDNSInterval.Interval = Data.Settings.DDNSInterval;
            string CurrentIP = await NetworkHelper.GetExternalIpAddress();
            string URL = await User.API.DDNSUpdateURL(TXTDDNSDomain.Text, TXTDDNSUsername.Text, TXTDDNSPassword.Text);
            if (!CurrentIP.Contains(Data.Settings.IPAddress))
            {
                Data.Message = $"Updateing {URL} with {CurrentIP}";
                SettingsClass.UpdateDNSIP(URL, CurrentIP);
            }
        }

        private void BTNSaveData_Click(object sender, EventArgs e)
        {
            TimerDDNSInterval.Start();
        }
        private void BTNWebiste_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", WebLinks.DDNSWebsits());
        }
    }
}
