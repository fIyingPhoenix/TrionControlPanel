using static TrionLibrary.EnumModels;
using TrionLibrary;
using TrionControlPanelDesktop.FormData;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

namespace TrionControlPanelDesktop.Controls
{
    public partial class SettingsControl : UserControl
    {
        private readonly string TrionVersOFF = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
        private string TrionVersON = "";
        private string MySQLVerOFF = "";
        private string MySQLVerON = "";
        private string SPPVerOFF = "";
        private string SPPVerON = "";
        private bool TrionUpdate = false;
        private bool MysqlUpdate = false;
        private bool SppUpdate = false;

        static System.Threading.Timer TextTimer;
        public SettingsControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            ComboBoxCores.Items.AddRange(Enum.GetNames(typeof(Cores)));
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
        private async void SettingsControl_LoadAsync(object sender, EventArgs e)
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            ComboBoxCores.SelectedItem = Data.Settings.SelectedCore.ToString();
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
            TrionVersON = await Data.Version.GetOnline(WebLinks.TrionVer);
            MySQLVerOFF = Data.Version.GetLocal(Data.Settings.MySQLExecutableLocation);
            MySQLVerON = await Data.Version.GetOnline(WebLinks.MySQLVer);
            SPPVerOFF = Data.Version.GetLocal(Data.Settings.WorldExecutableLocation);
            SPPVerON = await Data.Version.GetOnline(WebLinks.SPPCoreVer);
            //Update Labels
            LBLTrionVersion.Text = $"Trion Version: Local {TrionVersOFF} / Online: {TrionVersON}";
            LBLMySQLVersion.Text = $"MySQL Version: \n •Local: {MySQLVerOFF} \n •Online: {MySQLVerON} ";
            LBLCoreVersion.Text = $"Core Version: \n •Local: {SPPVerOFF} \n •Online: {SPPVerON} ";
            UIData.StartUpLoading++;
        }
        private void ComboBoxCores_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBoxCores.SelectedItem)
            {
                case "AscEmu":
                    Data.Settings.WorldExecutableName = "world";
                    Data.Settings.LogonExecutableName = "logon";
                    Data.Settings.SelectedCore = Cores.AscEmu;
                    break;
                case "AzerothCore":
                    Data.Settings.WorldExecutableName = "worldserver";
                    Data.Settings.LogonExecutableName = "authserver";
                    Data.Settings.SelectedCore = Cores.AzerothCore;
                    Data.Settings.CharactersDatabase = "acore_characters";
                    Data.Settings.WorldDatabase = "acore_world";
                    Data.Settings.AuthDatabase = "acore_auth";
                    break;
                case "CMaNGOS":
                    Data.Settings.WorldExecutableName = "mangosd";
                    Data.Settings.LogonExecutableName = "realmd";
                    Data.Settings.SelectedCore = Cores.CMaNGOS;
                    break;
                case "CypherCore":
                    Data.Settings.WorldExecutableName = "WorldServer";
                    Data.Settings.LogonExecutableName = "BNetServer";
                    Data.Settings.SelectedCore = Cores.CypherCore;
                    break;
                case "TrinityCore":
                    Data.Settings.WorldExecutableName = "bnetserver";
                    Data.Settings.LogonExecutableName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCore;
                    break;
                case "TrinityCore335":
                    Data.Settings.WorldExecutableName = "authserver";
                    Data.Settings.LogonExecutableName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCore335;
                    break;
                case "TrinityCoreClassic":
                    Data.Settings.WorldExecutableName = "bnetserver";
                    Data.Settings.LogonExecutableName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCoreClassic;
                    break;
                case "VMaNGOS":
                    Data.Settings.WorldExecutableName = "mangosd";
                    Data.Settings.LogonExecutableName = "realmd";
                    Data.Settings.SelectedCore = Cores.VMaNGOS;
                    break;
            }
            TXTBoxLoginExecName.Text = Data.Settings.LogonExecutableName;
            TXTBoxWorldExecName.Text = Data.Settings.WorldExecutableName;
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
            if (UIData.StartupUpdateCheck)
            {
                UIData.StartupUpdateCheck = false;
                CheckForUpdate();
            }
            if (UIData.LoadData == true)
            {
                UIData.LoadData = false;
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
        private void CheckForUpdate()
        {
            // Single Player Project Update
            if (DateTime.TryParse(SPPVerOFF, out DateTime SPPLocal) && DateTime.TryParse(SPPVerON, out DateTime SPPOnline))
            {
                if (SPPLocal < SPPOnline && SPPOnline != DateTime.MinValue)
                {
                    if (Data.Settings.AutoUpdateCore)
                    {
                        DownlaodThread(WebLinks.SPPCoreUpdate);
                    }
                    else
                    {
                        Data.Message = "A new update to the Single Player Project is available!";
                        SppUpdate = true;
                    }
                }
            }
            Thread.Sleep(100);
            // MySQL Update
            if (!string.IsNullOrEmpty(MySQLVerOFF) && !string.IsNullOrEmpty(MySQLVerON))
            {
                if (VersionCompare(MySQLVerOFF, MySQLVerON) < 0)
                {
                    if (Data.Settings.AutoUpdateMySQL)
                    {
                        DownlaodThread(WebLinks.MySQLUpdate);
                    }
                    else
                    {
                        Data.Message = "A new update for MySQL Server is available!";
                        MysqlUpdate = true;
                    }
                }
            }
            Thread.Sleep(100);
            // Trion Update
            if (!string.IsNullOrEmpty(TrionVersOFF) && !string.IsNullOrEmpty(TrionVersON))
            {
                if (VersionCompare(TrionVersOFF, TrionVersON) < 0)
                {
                    if (Data.Settings.AutoUpdateTrion)
                    {
                        DownlaodThread(WebLinks.TrionUpdate);
                    }
                    else
                    {
                        Data.Message = "A new update for Trion Control Panel is available!";
                        TrionUpdate = true;
                    }
                }
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
        }
        private void TXTBox_TextChanged(object sender, EventArgs e)
        {
            // Stop the timer if it's running
            TextTimer?.Dispose();
            // Start a new timer
            TextTimer = new(SaveDataTextbox, null, 1000, Timeout.Infinite);
        }
        private void BTNFixMysql_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            SystemWatcher.ApplicationKill(Data.Settings.MySQLExecutableName);
            Directory.Delete($@"{path}\database\data");
            string SQLLocation = $@"{path}\database\extra\initMySQL.sql";
            SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName, Data.Settings.ConsolHide, $"--initialize-insecure --init-file=\"{SQLLocation}\" --console");
        }
        private void BTNTrionUpdate_Click(object sender, EventArgs e)
        {
            if (TrionUpdate) { DownlaodThread(WebLinks.TrionUpdate); DownloadControl.Title = "Trion Control Panel Update.S"; }
            if (SppUpdate) { DownlaodThread(WebLinks.SPPCoreUpdate); DownloadControl.Title = "Single Player Project Update."; }
            if (MysqlUpdate) { DownlaodThread(WebLinks.MySQLUpdate); DownloadControl.Title = "MySQL Server Update."; }
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
    }
}
