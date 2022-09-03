using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Win32;
using TrionControlPanel.Alerts;
using TrionControlPanel.Database;
using TrionControlPanel.Settings;

namespace TrionControlPanel.TabsComponents
{

    public partial class SettingControl : UserControl
    {
        AlertBox alertBox = new();
        Settings.Settings Settings = new();
        public void LoadSettings()
        {
            txtAuthDatabase.Text = Settings._Data.AuthDatabase;
            txtMysqlLocation.Text = Settings._Data.MySQLLocation;
            txtMySqlServer.Text = Settings._Data.MySQLServerHost;
            txtMySqlUser.Text = Settings._Data.MySQLServerUser;
            txtMySqlPassowrd.Text = Settings._Data.MySQLServerPassword;
            txtMySqlPort.Text = Settings._Data.MySQLServerPort;
            txtMysqlName.Text = Settings._Data.MySQLExecutableName;
            txtCoreLocation.Text = Settings._Data.CoreLocation;
            txtWorldName.Text = Settings._Data.WorldExecutableName;
            txtBnetName.Text = Settings._Data.BnetExecutableName;
            tglNotySound.Checked = Settings._Data.NotificationSound;
            tglHideConsole.Checked = Settings._Data.ConsolHide;
            tglStayInTray.Checked = Settings._Data.StayInTray;
            tglStartOnStartup.Checked = Settings._Data.RunWithWindows;
            tglCustomNames.Checked = Settings._Data.CustomNames;
            tglStartServer.Checked = Settings._Data.StartCoreOnRun;
            comboBoxCore.SelectedIndex = Settings._Data.SelectedCore;
        }
        private void SaveSettings()
        {
            Settings._Data.AuthDatabase = txtAuthDatabase.Text;
            Settings._Data.MySQLLocation = txtMysqlLocation.Text;
            Settings._Data.MySQLServerHost = txtMySqlServer.Text;
            Settings._Data.MySQLServerUser = txtMySqlUser.Text;
            Settings._Data.MySQLServerPassword = txtMySqlPassowrd.Text;
            Settings._Data.MySQLServerPort = txtMySqlPort.Text;
            Settings._Data.MySQLExecutableName = txtMysqlName.Text;
            Settings._Data.CoreLocation = txtCoreLocation.Text;
            Settings._Data.WorldExecutableName = txtWorldName.Text;
            Settings._Data.BnetExecutableName = txtBnetName.Text;
            Settings._Data.NotificationSound = tglNotySound.Checked;
            Settings._Data.ConsolHide = tglHideConsole.Checked;
            Settings._Data.StayInTray = tglStayInTray.Checked;
            Settings._Data.RunWithWindows = tglStartOnStartup.Checked;
            Settings._Data.CustomNames = tglCustomNames.Checked;
            Settings._Data.StartCoreOnRun = tglStartServer.Checked;
            Settings._Data.SelectedCore = comboBoxCore.SelectedIndex;
        }
        private void GetMySQLLocation()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                string mysqlName = $@"{Settings._Data.MySQLExecutableName}.exe";
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    foreach (string f in Directory.EnumerateFiles(fbd.SelectedPath, mysqlName, SearchOption.AllDirectories))
                    {
                        string exeFolderPath = Path.GetDirectoryName(f)!;
                        Settings._Data.MySQLLocation = Path.GetFullPath(Path.Combine(exeFolderPath, @"..\"));
                        Settings._Data.MySQLExecutablePath = f;
                        txtMysqlLocation.Text = Settings._Data.MySQLLocation;
                    }
                }
            }
        }

        private void GetCoreLocation()
        {
            // getting the core files and location. saves to settings. dose not seed to press save 
            using (var fbd = new FolderBrowserDialog())
            {
                string worndName = $@"{Settings._Data.WorldExecutableName}.exe";
                string bnetName = $@"{Settings._Data.BnetExecutableName}.exe";

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    foreach (string f in Directory.GetFiles(fbd.SelectedPath, worndName, SearchOption.AllDirectories))
                    {
                        Settings._Data.CoreLocation = Path.GetDirectoryName(f)!;
                        txtCoreLocation.Text = Settings._Data.CoreLocation;
                    }
                    foreach (string f in Directory.EnumerateFiles(fbd.SelectedPath, worndName, SearchOption.AllDirectories))
                    {
                        Settings._Data.WorldExecutableLocation = f;
                    }
                    foreach (string f in Directory.EnumerateFiles(fbd.SelectedPath, bnetName, SearchOption.AllDirectories))
                    {
                        Settings._Data.BnetExecutableLocation = f;
                    }
                }
            }
        }

        private void CustomNames()
        {
            if (tglCustomNames.Checked == true)
            {
                txtWorldName.Enabled = true;
                txtBnetName.Enabled = true;
                txtMysqlName.Enabled = true;
            }
            else
            {
                txtWorldName.Enabled = false;
                txtBnetName.Enabled = false;
                txtMysqlName.Enabled = false;
            }
        }
        public SettingControl()
        {
            InitializeComponent();

        }
        public SettingControl(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
        private void SettingControl_Load(object sender, EventArgs e)
        {
            CustomNames();
            LoadSettings();
            LoadSettings();
            
        }
        private void AllowJustNumbers(object sender, KeyPressEventArgs e)
        {
            //allow just numbers/ digits
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point. i dont thik is needed but its nice to have
            if ((e.KeyChar == '.') && (((TextBox)sender).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
        private void BntLocation_Click(object sender, EventArgs e)
        {
            GetCoreLocation();
        }
        private void BntOpenLocation_Click(object sender, EventArgs e)
        {
            //just a fail safe. incase the CoreLocation is empty.
            if (Settings._Data.CoreLocation == string.Empty)
            {
                alertBox.ShowAlert("Server Location Unknow!", NotificationType.Error);
            }
            else
            {
                try
                {

                    Process.Start("explorer.exe", Settings._Data.CoreLocation);
                }
                catch
                {
                    alertBox.ShowAlert("Server Location Unknow or invaluable!", NotificationType.Error);
                }
            }
        }
        private void TimerUpdate_Tick(object sender, EventArgs e)
        {

        }
        private void ComboBoxCore_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxCore.SelectedIndex == 0)
            {
                // AscEmu
                Settings._Data.SelectedCore = comboBoxCore.SelectedIndex;
                Settings._Data.BnetExecutableName = "logon";
                Settings._Data.WorldExecutableName = "world";
            }
            else if (comboBoxCore.SelectedIndex == 1)
            {
                //AzerothCore
                Settings._Data.SelectedCore = comboBoxCore.SelectedIndex;
                Settings._Data.BnetExecutableName = "authserver";
                Settings._Data.WorldExecutableName = "worldserver";
            }
            else if (comboBoxCore.SelectedIndex == 2)
            {
                //Continued MaNGOS
                Settings._Data.SelectedCore = comboBoxCore.SelectedIndex;
                Settings._Data.BnetExecutableName = "realmd";
                Settings._Data.WorldExecutableName = "mangosd";

            }
            else if (comboBoxCore.SelectedIndex == 3)
            {
                //CypherCore
                Settings._Data.SelectedCore = comboBoxCore.SelectedIndex;
                Settings._Data.BnetExecutableName = "BNetServer";
                Settings._Data.WorldExecutableName = "WorldServer";;

            }
            else if (comboBoxCore.SelectedIndex == 4)
            {
                //TrinityCore
                Settings._Data.SelectedCore = comboBoxCore.SelectedIndex;
                Settings._Data.BnetExecutableName = "bnetserver";
                Settings._Data.WorldExecutableName = "worldserver";
            }
            else if (comboBoxCore.SelectedIndex == 5)
            {
                //TrinityCore 4.3.4(TCPP)
                Settings._Data.SelectedCore = comboBoxCore.SelectedIndex;
                Settings._Data.BnetExecutableName = "bnetserver";
                Settings._Data.WorldExecutableName = "worldserver";
            }
            else if (comboBoxCore.SelectedIndex == 6)
            {
                //Vanilla MaNGOS
                Settings._Data.SelectedCore = comboBoxCore.SelectedIndex;
                Settings._Data.BnetExecutableName = "realmd";
                Settings._Data.WorldExecutableName = "mangosd";
            }
        }
        private void BtnTestMySQL_Click(object sender, EventArgs e)
        {
            MySQLConnect databaseConnection = new();
            databaseConnection.MySqlConnectCheck();
        }
        private void TglStayInTray_CheckedChanged(object sender, EventArgs e)
        {
            if (tglStayInTray.Checked == true)
            {
                Settings._Data.StayInTray = true;
            }
            else if (tglStayInTray.Checked == false)
            {
                Settings._Data.StayInTray = false;
            }
        }
        private void BtnFixMysql_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start($@"{Settings._Data.MySQLExecutablePath}", "--initialize --console");
            }
            catch (Exception ex)
            {
                alertBox.ShowAlert(ex.Message, NotificationType.Error);
            }
           
        }
        private void BntMySqlLocation_Click(object sender, EventArgs e)
        {
            string file = Settings._Data.MySQLLocation;
            string location = Path.GetDirectoryName(file)!;
            //just a fail safe. incase the CoreLocation is empty.
            if (Settings._Data.MySQLLocation == string.Empty)
            {
                alertBox.ShowAlert("Server Location Unknow!", NotificationType.Error);
            }
            else
            {
                try
                {
                    Process.Start("explorer.exe", location);
                }
                catch (Exception ex)
                {
          
                    alertBox.ShowAlert(ex.Message, NotificationType.Error);
                }
            }
        }
        private void TglStartOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true)!;
            if (tglStartOnStartup.Checked == true)
            {
                reg.SetValue("Trion Control Panel", Application.ExecutablePath.ToString());
            }
            else if (tglStartOnStartup.Checked == false)
            {
                reg.DeleteValue("Trion Control Panel");
            }
        }
        private void TglStartServer_CheckedChanged(object sender, EventArgs e)
        {
            if (tglStartServer.Checked == true)
                Settings._Data.StartCoreOnRun = true;
            else if (tglStartServer.Checked == false)
                Settings._Data.StartCoreOnRun = false;
        }
        private void TglRestartOnCrash_CheckedChanged(object sender, EventArgs e)
        {
            if (tglRestartOnCrash.Checked == true)
                Settings._Data.ServerCrashCheck = true;
            else if (tglRestartOnCrash.Checked == false)
                Settings._Data.ServerCrashCheck = false;

        }

        private void BtnMySQLLocationSearch_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if(fbd.ShowDialog() == DialogResult.OK)
                {
                    txtMysqlLocation.Text = fbd.SelectedPath;
                    Settings._Data.MySQLLocation = fbd.SelectedPath;
                }
            }
        }

        private void BtnMysqlLocation_Click(object sender, EventArgs e)
        {
            GetMySQLLocation();
        }

        private void BtnMysqlOpenLocation_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Settings._Data.MySQLLocation))
            {
                Process.Start(Settings._Data.MySQLLocation, "explorer.exe");
            }   
            else
            {
                GetMySQLLocation();
            }
        }

        private void TglCustomNames_CheckedChanged(object sender, EventArgs e)
        {
            CustomNames();
        }
    }
}
