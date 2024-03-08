using static TrionLibrary.EnumModels;
using TrionLibrary;
using TrionControlPanelDesktop.FormData;
using System.Reflection;
using System.Diagnostics;
using MetroFramework;

namespace TrionControlPanelDesktop.Controls
{
    public partial class SettingsControl : UserControl
    {
        private readonly string TrionVersOFF = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
        private string TrionVersON = "N/A";
        private string MySQLVerOFF = "N/A";
        private string MySQLVerON = "N/A";
        private string SPPVerOFF = "N/A";
        private string SPPVerON = "N/A";
        static System.Threading.Timer TextTimer;
        public SettingsControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            _ = LoadData();
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
        private void SettingsControl_Load(object sender, EventArgs e)
        {
            ComboBoxCores.Items.AddRange(Enum.GetNames(typeof(Cores)));
            ComboBoxCores.SelectedItem = Data.Settings.SelectedCore.ToString();
        }
        private async Task LoadData()
        {
            await Data.LoadSettings();
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
            Data.Settings.CustomNames = TGLCustomNames.Checked; ;
            CustomNames();
        }
        private async void BTNMySQLExecLovation_Click(object sender, EventArgs e)
        {
            string Folder = GetFolder();
            if (Folder != string.Empty)
            {
                if (Data.GetExecutableLocation(Folder, Data.Settings.MySQLExecutableName) != string.Empty)
                {
                    Data.Settings.MySQLExecutableLocation = Data.GetExecutableLocation(Folder, Data.Settings.MySQLExecutableName);
                    Data.Settings.MySQLLocation = Path.GetFullPath(Path.Combine(Data.Settings.MySQLExecutableLocation, @"..\"));
                    await Data.SaveSettings();
                    Data.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                    await LoadData();

                }
                else
                {
                    MessageBox.Show("Wrong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (MessageBox.Show("", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Do something! Download a new mysql server or idk yet
                }
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
                    await LoadData(); ;
                }
            }
            else
            {
                if (MessageBox.Show("", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Do something! Download a new mysql server or idk yet
                }
            }
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            if (UIData.StartupUpdateCheck)
            {
                UIData.StartupUpdateCheck = false;
                CheckForUpdate();
            }
        }
        private void BtnDownloadSPP_ClickAsync(object sender, EventArgs e)
        {
            DownlaodThread(WebLinks.SPPCoreFiles);
        }
        private void BTNDownloadMySQL_Click(object sender, EventArgs e)
        {
            DownlaodThread(WebLinks.MySQLFiles);
        }
        private void CheckForUpdate()
        {
            //SPP Update with Date 
            DateTime SPPLocal = DateTime.Parse(SPPVerOFF);
            DateTime SPPOnline = DateTime.Parse(SPPVerON);
            if (SPPOnline != DateTime.MinValue)
            {
                if (SPPLocal < SPPOnline)
                {
                    if (Data.Settings.AutoUpdateCore)
                    {
                        DownlaodThread(WebLinks.SPPCoreUpdate);
                    }
                    else
                    {
                        if (MetroMessageBox.Show(this, "A new Single Player Project version is available.\nDo you want To download it?", "Update Available!", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                        {
                            DownlaodThread(WebLinks.SPPCoreUpdate);
                        }
                    }
                }
            }
            //MySQL and Trion number based Version
            string MySQLLocal = MySQLVerOFF;
            string MySQLOnline = MySQLVerON;
            string TrionLocal = TrionVersOFF;
            string TrionOnline = TrionVersON;

            if (MySQLOnline != string.Empty)// A LITTLE FAIL PROOF and so i can reuse the struings
            {
                string[] vComps1 = MySQLLocal.Split('.');
                string[] vComps2 = MySQLOnline.Split('.');
                int[] vNumb1 = Array.ConvertAll(vComps1, int.Parse);
                int[] vNumb2 = Array.ConvertAll(vComps2, int.Parse);
                for (int i = 0; i < Math.Min(vNumb1.Length, vNumb2.Length); i++)
                {
                    if (vNumb1[i] < vNumb2[i])
                    {
                        if (Data.Settings.AutoUpdateMySQL)
                        {
                            DownlaodThread(WebLinks.MySQLUpdate);
                        }
                        else
                        {
                            MainForm form = new();
                            if (MetroMessageBox.Show(form, "A new MySQl version is available.\nDo you want To download it?", "Update Available!", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                            {
                                DownlaodThread(WebLinks.MySQLUpdate);
                            }
                        }
                    }
                }
            }
            if (TrionOnline != string.Empty)
            {
                string[] vComps1 = TrionLocal.Split('.');
                string[] vComps2 = TrionOnline.Split('.');
                int[] vNumb1 = Array.ConvertAll(vComps1, int.Parse);
                int[] vNumb2 = Array.ConvertAll(vComps2, int.Parse);
                for (int i = 0; i < Math.Min(vNumb1.Length, vNumb2.Length); i++)
                {
                    if (vNumb1[i] < vNumb2[i])
                    {
                        if (Data.Settings.AutoUpdateTrion)
                        {
                            DownlaodThread(WebLinks.TrionUpdate);
                        }
                        else
                        {
                            MainForm form = new();
                            if (MetroMessageBox.Show(form, "A new Trion version is available.\nDo you want To download it?", "Update Available!", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                            {
                                DownlaodThread(WebLinks.TrionUpdate);
                            }
                        }
                    }
                }
            }
        }
        private void DownlaodThread(string Weblink)
        {
            Thread DwonloadThread = new(() =>
            {
                Task.Run(() => DownloadControl.AddToList(Weblink));
                MainForm.LoadDownloadControl();
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

    }
}
