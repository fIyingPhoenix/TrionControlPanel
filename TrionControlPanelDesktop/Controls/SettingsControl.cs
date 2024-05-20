using static TrionLibrary.EnumModels;
using TrionLibrary;
using TrionControlPanelDesktop.FormData;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;
using System.Data;
using MySql.Data.MySqlClient;
using TrionDatabase;
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
            ComboBoxDDNService.Items.AddRange(Enum.GetNames(typeof(DDNSerivce)));
            User.UI.Update.StartupUpdateCheck = true;
        }
        private static string GetFolder()
        {
            using (FolderBrowserDialog FolderDialog = new())
            {
                // Set the initial selected folder
                FolderDialog.SelectedPath = Directory.GetCurrentDirectory();
                // Set the title of the dialog
                FolderDialog.Description = "Select a folder";
                // Show the folder browser dialog
                DialogResult result = FolderDialog.ShowDialog();

                // Check if the user clicked OK
                if (result == DialogResult.OK)
                {
                    // Return the selected folder path
                    return FolderDialog.SelectedPath;
                }
                else
                {
                    // Return empty string 
                    return string.Empty;
                }
            }
        }
        private void CustomNames()
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
            TXTBoxLoginExecName.Text = Data.Settings.LogonExecutableName;
            TXTBoxWorldExecName.Text = Data.Settings.WorldExecutableName;
            TXTBoxMySQLExecName.Text = Data.Settings.MySQLExecutableName;
            //Working Directory
            TXTBoxCoreLocation.Text = Data.Settings.CoreLocation;
            TXTBoxMySQLLocation.Text = Data.Settings.MySQLLocation;
            //MySQL Host Data
            TXTMysqlHost.Text = Data.Settings.MySQLServerHost;
            TXTMysqlPort.Text = Data.Settings.MySQLServerPort;
            TXTMysqlUser.Text = Data.Settings.MySQLServerUser;
            TXTMysqlPassword.Text = Data.Settings.MySQLServerPassword;
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
            CustomNames();
            //Version Load
            User.UI.Update.TrionVersOFF = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
            User.UI.Update.TrionVersON = await Data.Version.GetOnline(WebLinks.TrionVer);
            User.UI.Update.MySQLVerOFF = Data.Version.GetLocal(Data.Settings.MySQLExecutableLocation);
            User.UI.Update.MySQLVerON = await Data.Version.GetOnline(WebLinks.MySQLVer);
            User.UI.Update.SPPVerOFF = Data.Version.GetLocal(Data.Settings.WorldExecutableLocation);
            User.UI.Update.SPPVerON = await Data.Version.GetOnline(WebLinks.SPPCoreVer);
            //Update Labels
            LBLTrionVersion.Text = $"Trion Version: Local {User.UI.Update.TrionVersOFF} / Online: {User.UI.Update.TrionVersON}";
            LBLMySQLVersion.Text = $"MySQL Version: \n •Local: {User.UI.Update.MySQLVerOFF} \n •Online: {User.UI.Update.MySQLVerON} ";
            LBLCoreVersion.Text = $"Core Version: \n •Local: {User.UI.Update.SPPVerOFF} \n •Online: {User.UI.Update.SPPVerON} ";
            CheckForUpdate();
            User.UI.Form.StartUpLoading++;
        }
        private async void ComboBoxCores_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBoxCores.SelectedItem)
            {
                case "AscEmu":
                    Data.Settings.WorldExecutableName = "world";
                    Data.Settings.LogonExecutableName = "logon";
                    Data.Settings.SelectedCore = Cores.AscEmu;
                    Data.Settings.CharactersDatabase = "ascemu_char";
                    Data.Settings.WorldDatabase = "ascemu_world";
                    Data.Settings.AuthDatabase = "ascemu_logon";
                    Data.Settings.MySQLServerUser = "root";
                    Data.Settings.MySQLServerPassword = "root";
                    break;
                case "AzerothCore":
                    Data.Settings.WorldExecutableName = "worldserver";
                    Data.Settings.LogonExecutableName = "authserver";
                    Data.Settings.SelectedCore = Cores.AzerothCore;
                    Data.Settings.CharactersDatabase = "acore_characters";
                    Data.Settings.WorldDatabase = "acore_world";
                    Data.Settings.AuthDatabase = "acore_auth";
                    Data.Settings.MySQLServerUser = "acore";
                    Data.Settings.MySQLServerPassword = "acore";
                    break;
                case "CMaNGOS":
                    Data.Settings.WorldExecutableName = "mangosd";
                    Data.Settings.LogonExecutableName = "realmd";
                    Data.Settings.SelectedCore = Cores.CMaNGOS;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "mangos";
                    Data.Settings.AuthDatabase = "realmd";
                    Data.Settings.MySQLServerUser = "mangos";
                    Data.Settings.MySQLServerPassword = "mangos";
                    break;
                case "CypherCore":
                    Data.Settings.WorldExecutableName = "WorldServer";
                    Data.Settings.LogonExecutableName = "BNetServer";
                    Data.Settings.SelectedCore = Cores.CypherCore;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "world";
                    Data.Settings.AuthDatabase = "auth";
                    Data.Settings.MySQLServerUser = "trinity";
                    Data.Settings.MySQLServerPassword = "trinity";
                    break;
                case "TrinityCore":
                    Data.Settings.WorldExecutableName = "bnetserver";
                    Data.Settings.LogonExecutableName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCore;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "world";
                    Data.Settings.AuthDatabase = "auth";
                    Data.Settings.MySQLServerUser = "trinity";
                    Data.Settings.MySQLServerPassword = "trinity";
                    break;
                case "TrinityCore335":
                    Data.Settings.WorldExecutableName = "authserver";
                    Data.Settings.LogonExecutableName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCore335;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "world";
                    Data.Settings.AuthDatabase = "auth";
                    Data.Settings.MySQLServerUser = "trinity";
                    Data.Settings.MySQLServerPassword = "trinity";
                    break;
                case "TrinityCoreClassic":
                    Data.Settings.WorldExecutableName = "bnetserver";
                    Data.Settings.LogonExecutableName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCoreClassic;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "world";
                    Data.Settings.AuthDatabase = "auth";
                    Data.Settings.MySQLServerUser = "trinity";
                    Data.Settings.MySQLServerPassword = "trinity";
                    break;
                case "VMaNGOS":
                    Data.Settings.WorldExecutableName = "mangosd";
                    Data.Settings.LogonExecutableName = "realmd";
                    Data.Settings.SelectedCore = Cores.VMaNGOS;
                    Data.Settings.CharactersDatabase = "characters";
                    Data.Settings.WorldDatabase = "mangos";
                    Data.Settings.AuthDatabase = "realmd";
                    Data.Settings.MySQLServerUser = "mangos";
                    Data.Settings.MySQLServerPassword = "mangos";
                    break;
            }
            TXTBoxLoginExecName.Text = Data.Settings.LogonExecutableName;
            TXTBoxWorldExecName.Text = Data.Settings.WorldExecutableName;
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
            CustomNames();
        }
        private void TGLServerStartup_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.RunServerWithWindows = TGLServerStartup.Checked;
        }
        private async void BTNMySQLExecLovation_Click(object sender, EventArgs e)
        {
            string Folder = GetFolder();
            try
            {
                if (Folder != string.Empty)
                {
                    if (Data.GetExecutableLocation(Folder, Data.Settings.MySQLExecutableName) != string.Empty)
                    {
                        Data.Settings.MySQLExecutableLocation = Data.GetExecutableLocation(Folder, Data.Settings.MySQLExecutableName);
                        Data.Settings.MySQLLocation = Path.GetFullPath(Path.Combine(Data.Settings.MySQLExecutableLocation, @"..\"));
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
            string Folder = GetFolder();
            if (Folder != string.Empty)
            {
                if (Data.GetExecutableLocation(Folder, Data.Settings.WorldExecutableName) != string.Empty &&
                    Data.GetExecutableLocation(Folder, Data.Settings.LogonExecutableName) != string.Empty)
                {
                    Data.Settings.WorldExecutableLocation = Data.GetExecutableLocation(Folder, Data.Settings.WorldExecutableName);
                    Data.Settings.LogonExecutableLocation = Data.GetExecutableLocation(Folder, Data.Settings.LogonExecutableName);
                    Data.Settings.CoreLocation = Path.GetFullPath(Folder);
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
            DownlaodThread(WebLinks.SPPCoreFiles);
        }
        private void BTNDownloadMySQL_Click(object sender, EventArgs e)
        {
            Data.Message = "MySQL Server is downloading!";
            DownloadControl.Title = "Installing MySQL Server.";
            DownloadControl.InstallMySQL = true;
            DownlaodThread(WebLinks.MySQLFiles);
        }
        public static async Task CreateBC()
        {
            string path = Directory.GetCurrentDirectory();
            if (!Directory.Exists("Backup"))
            { Directory.CreateDirectory("Backup"); }
            if (User.UI.Form.MySQLisRunning != true)
            {
                SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName, true, $"--console");
            }
            SQLDataConnect.DumpAllTables(SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase), path + "\\Backup\\AuthBackup.sql");
            SQLDataConnect.DumpAllTables(SQLDataConnect.ConnectionString(Data.Settings.CharactersDatabase), path + "\\Backup\\CharBackup.sql");
            SQLDataConnect.DumpAllTables(SQLDataConnect.ConnectionString(Data.Settings.WorldDatabase), path + "\\Backup\\WorldBackup.sql");
        }
        private async void CheckForUpdate()
        {
            // Single Player Project Update
            if (DateTime.TryParse(User.UI.Update.SPPVerOFF, out DateTime SPPLocal) && DateTime.TryParse(User.UI.Update.SPPVerON, out DateTime SPPOnline))
            {
                if (SPPLocal < SPPOnline && SPPOnline != DateTime.MinValue)
                {
                    if (Data.Settings.AutoUpdateCore)
                    {
                        await CreateBC();
                        DownlaodThread(WebLinks.SPPCoreUpdate);
                    }
                    else
                    {

                        if (MetroMessageBox.Show(this, "A new update to the Single Player Project is available!", "Info.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            if (MetroMessageBox.Show(this, "Do you want to create a database backup?", "Question.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                await CreateBC();
                            }
                            DownlaodThread(WebLinks.SPPCoreUpdate);
                        }
                    }
                    User.UI.Update.SppUpdate = true;
                }
            }
            Thread.Sleep(100);
            // MySQL Update
            if (!string.IsNullOrEmpty(User.UI.Update.MySQLVerOFF) && !string.IsNullOrEmpty(User.UI.Update.MySQLVerON))
            {
                if (VersionCompare(User.UI.Update.MySQLVerOFF, User.UI.Update.MySQLVerON) < 0)
                {
                    if (Data.Settings.AutoUpdateMySQL)
                    {
                        await CreateBC();

                        DownlaodThread(WebLinks.MySQLUpdate);
                    }
                    else
                    {

                        if (MetroMessageBox.Show(this, "A new update for MySQL Server is available!", "Info.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            if (MetroMessageBox.Show(this, "Do you want to create a database backup?", "Question.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                await CreateBC();
                            }
                            DownlaodThread(WebLinks.MySQLUpdate);
                        }
                    }
                    User.UI.Update.MysqlUpdate = true;
                }
            }
            Thread.Sleep(100);
            // Trion Update
            if (!string.IsNullOrEmpty(User.UI.Update.TrionVersOFF) && !string.IsNullOrEmpty(User.UI.Update.TrionVersON))
            {
                if (VersionCompare(User.UI.Update.TrionVersOFF, User.UI.Update.TrionVersON) < 0)
                {
                    if (Data.Settings.AutoUpdateTrion)
                    {
                        await CreateBC();
                        DownlaodThread(WebLinks.TrionUpdate);
                    }
                    else
                    {
                        if (MetroMessageBox.Show(this, "A new update for MySQL Server is available!", "Info.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            if (MetroMessageBox.Show(this, "Do you want to create a database backup?", "Question.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                await CreateBC();
                            }
                            DownlaodThread(WebLinks.TrionUpdate);
                        }
                    }
                }
                User.UI.Update.TrionUpdate = true;
            }
        }
        private static int VersionCompare(string ver1, string ver2)
        {
            if (ver1 != "N/A" && ver2 != "N/A")
            {
                string[] vComps1 = ver1.Split('.');
                string[] vComps2 = ver2.Split('.');
                int[] vNumb1 = Array.ConvertAll(vComps1, int.Parse);
                int[] vNumb2 = Array.ConvertAll(vComps2, int.Parse);

                for (int i = 0; i < Math.Min(vNumb1.Length, vNumb2.Length); i++)
                {
                    if (vNumb1[i] != vNumb2[i])
                    {
                        return vNumb1[i].CompareTo(vNumb2[i]);
                    }
                }
                return vNumb1.Length.CompareTo(vNumb2.Length);
            }
            return 0;
        }
        public static void DownlaodThread(string Weblink)
        {
            Thread DwonloadThread = new(async () =>
            {
                await Task.Run(() => DownloadControl.AddToList(Weblink));
            });
            DwonloadThread.Start();
        }
        private void BTNAuthConfig_Click(object sender, EventArgs e)
        {
            string location = Data.Settings.CoreLocation + "\\configs\\authserver.conf";
            Process.Start("explorer.exe", location);
        }
        private void BTNDiscord_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", WebLinks.Discord);
        }
        private void SaveDataTextbox(object state)
        {
            // Seve data
            Data.Settings.MySQLExecutableName = TXTBoxMySQLExecName.Text;
            Data.Settings.WorldExecutableName = TXTBoxWorldExecName.Text;
            Data.Settings.LogonExecutableName = TXTBoxLoginExecName.Text;
            Data.Settings.MySQLServerHost = TXTMysqlHost.Text;
            Data.Settings.MySQLServerPort = TXTMysqlPort.Text;
            Data.Settings.MySQLServerUser = TXTMysqlUser.Text;
            Data.Settings.MySQLServerPassword = TXTMysqlPassword.Text;
            Data.Settings.AuthDatabase = TXTAuthDatabase.Text;
            Data.Settings.WorldDatabase = TXTWorldDatabase.Text;
            Data.Settings.CharactersDatabase = TXTCharDatabase.Text;
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
            if (User.UI.Update.TrionUpdate == true) { DownlaodThread(WebLinks.TrionUpdate); DownloadControl.Title = "Trion Control Panel Update.S"; }
            if (User.UI.Update.SppUpdate == true) { DownlaodThread(WebLinks.SPPCoreUpdate); DownloadControl.Title = "Single Player Project Update."; }
            if (User.UI.Update.MysqlUpdate == true) { DownlaodThread(WebLinks.MySQLUpdate); DownloadControl.Title = "MySQL Server Update."; }
        }
        public static void AddToStartup(string appName, string executablePath)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)!;
                key.SetValue(appName, executablePath);
                key.Close();
                Data.Message = "Trion Control Panel added to Windows startup successfully.";
            }
            catch (Exception ex)
            {
                Data.Message = "Error adding Trion Control Panel to Windows startup: " + ex.Message;
            }
        }
        public static void RemoveFromStartup(string appName)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)!;
                key.DeleteValue(appName, false);
                key.Close();
                Data.Message = "Trion Control Panel removed from Windows startup successfully.";
            }
            catch (Exception ex)
            {
                Data.Message = "Error removing Trion Control Panel from Windows startup: " + ex.Message;
            }
        }
        private void TGLRunTrionStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (TGLRunTrionStartup.Checked == true) { AddToStartup("Trion Control Panel", Application.ExecutablePath.ToString()); Data.Settings.RunWithWindows = true; }
            if (TGLRunTrionStartup.Checked == false) { RemoveFromStartup("Trion Control Panel"); Data.Settings.RunWithWindows = false; }
        }
        private async void BTNTestConnection_Click(object sender, EventArgs e)
        {
            if (await TestConnection() == true)
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
        private static async Task<bool> TestConnection()
        {
            MySqlConnection conn = new(SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase));
            bool status = false;
            await Task.Run(() =>
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        Data.Message = $"The SQL Connection is {conn.State}";
                        status = true;
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    Data.Message = ex.Message;
                    status = false;
                }
                return status;
            });
            return status;
        }
        private void TimerConnectSucess_Tick(object sender, EventArgs e)
        {
            TimerConnectSucess.Stop();
            BTNTestConnection.ForeColor = Color.White;
            BTNTestConnection.Text = "   Test Connection";
        }
        private void BTNCoreOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Data.Settings.CoreLocation);
        }
        private void BTNMySQLOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Data.Settings.MySQLLocation);
        }
        private void BTNWorldConfig_Click(object sender, EventArgs e)
        {
            string location = Data.Settings.CoreLocation + "\\configs\\worldserver.conf";
            Process.Start("explorer.exe", location);
        }
        private void BTNModsConfig_Click(object sender, EventArgs e)
        {
            string location = Data.Settings.CoreLocation + "\\configs\\modules";
            Process.Start("explorer.exe", location);
        }
        private async void BTNDeleteAuth_Click(object sender, EventArgs e)
        {
            BTNDeleteAuth.Text = "   Working!!";
            BTNDeleteAuth.ForeColor = Color.Orange;
            BTNDeleteAuth.Click -= BTNDeleteAuth_Click;
            // Get all tables in the database
            List<string> tables = await SQLDataConnect.GetTables(SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase));
            // Delete each table
            foreach (string table in tables)
            {
                SQLDataConnect.DeleteTable(SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase), table);
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
            List<string> tables = await SQLDataConnect.GetTables(SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase));
            // Delete each table
            foreach (string table in tables)
            {
                SQLDataConnect.DeleteTable(SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase), table);
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
            List<string> tables = await SQLDataConnect.GetTables(SQLDataConnect.ConnectionString(Data.Settings.WorldDatabase));
            // Delete each table
            foreach (string table in tables)
            {
                SQLDataConnect.DeleteTable(SQLDataConnect.ConnectionString(Data.Settings.WorldDatabase), table);
            }
            Data.Message = "Wolrd Tables deleted successfully.";
            BTNDeleteWorld.Click += BTNDeleteWorld_Click;
            BTNDeleteWorld.Text = "   Delete World Database";
            BTNDeleteWorld.ForeColor = Color.White;
        }
        private void TimerEnDis_Tick(object sender, EventArgs e)
        {
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
        private void BTNFixMysql_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            string SQLLocation = "";

            BTNFixMysql.Text = "Working!!!";
            BTNFixMysql.ForeColor = Color.Orange;
            BTNFixMysql.Click -= BTNFixMysql_Click;
            if (!Directory.Exists("Backup"))
            { Directory.CreateDirectory("Backup"); }
            if (User.UI.Form.MySQLisRunning == true)
            {
                if (CBAuthBackup.Checked == true)
                {
                    SQLDataConnect.DumpAllTables(SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase), path + "\\Backup\\AuthBackup.sql");
                }
                if (CBCharBackup.Checked == true)
                {
                    SQLDataConnect.DumpAllTables(SQLDataConnect.ConnectionString(Data.Settings.CharactersDatabase), path + "\\Backup\\CharBackup.sql");
                }
                if (CBWorldBackup.Checked == true)
                {
                    SQLDataConnect.DumpAllTables(SQLDataConnect.ConnectionString(Data.Settings.WorldDatabase), path + "\\Backup\\WorldBackup.sql");
                }

                SystemWatcher.ApplicationKill(Data.Settings.MySQLExecutableName);
                Directory.Delete($@"{path}\database\data", true);
                if (User.UI.Update.SPPVerOFF != "N/A")
                {
                    SQLLocation = $@"{path}\database\extra\initSPP.sql";
                }
                else if (User.UI.Update.SPPVerOFF == "N/A")
                {
                    SQLLocation = $@"{path}\database\extra\initMySQL.sql";
                }
                SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName, Data.Settings.ConsolHide, $"--initialize-insecure --init-file=\"{SQLLocation}\" --console");
            }
            else
            {
                if (MetroMessageBox.Show(this, "Core Directory not Found! Do you want To look for it?", "Info.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    SystemWatcher.ApplicationKill(Data.Settings.MySQLExecutableName);
                    Directory.Delete($@"{path}\database\data", true);
                    if (User.UI.Update.SPPVerOFF != "N/A")
                    {
                        SQLLocation = $@"{path}\database\extra\initSPP.sql";
                    }
                    else if (User.UI.Update.SPPVerOFF == "N/A")
                    {
                        SQLLocation = $@"{path}\database\extra\initMySQL.sql";
                    }
                    SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName, Data.Settings.ConsolHide, $"--initialize-insecure --init-file=\"{SQLLocation}\" --console");
                }

            }

            BTNFixMysql.Click += BTNFixMysql_Click;
            BTNFixMysql.Text = "Start";
            BTNFixMysql.ForeColor = Color.White;
        }

        private void ComboBoxDDNService_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBoxDDNService.SelectedItem)
            {
                case "DuckDNS":
                    break;
                case "DynamicDNS":
                    break;
                case "Dynu":
                    break;
                case "Enom":
                    break;
                case "AllInkl":
                    break;
                case "dynDNS":
                    break;
                case "STRATO":
                    break;
                case "Freemyip":
                    break;
                case "Afraid":
                    break;
                case "OVH":
                    break;
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
