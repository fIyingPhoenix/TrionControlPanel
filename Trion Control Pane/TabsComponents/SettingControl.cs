using System.ComponentModel;
using TrionControlPanel.Database;
using TrionControlPanel.Properties;
using TrionControlPanel.Classes;
using TrionControlPanel.Forms;
using System.Diagnostics;

namespace TrionControlPanel.TabsComponents
{
    public partial class SettingControl : UserControl
    {
        DatabaseConnection databaseConnection = new();
        //settings data located in appdata/local/CypherCoreServerLaucher

        private void LoadSettings()
        {
            //loading data form settings file(xml)
            comboBoxCore.SelectedIndex = Settings.Default.ComboBoxCore;
            txtWorldLocation.Texts = Settings.Default.WorldCoreLocation;
            txtBnetLocation.Texts = Settings.Default.BnetCoreLocation;
            txtMySqlPassowrd.Texts = Settings.Default.MySQLServerPassword;
            txtMySqlPort.Texts = Settings.Default.MySQLServerPort;
            txtMySqlServer.Texts = Settings.Default.MySQLServerHost;
            txtMySqlUser.Texts = Settings.Default.MySQLServerUsername;
            txtAuthDatabase.Texts = Settings.Default.AuthDatabaseName;
            txtWorldName.Texts = Settings.Default.WorldCoreName;
            txtApacheName.Texts = Settings.Default.ApacheCoreName;
            txtBnetName.Texts = Settings.Default.BnetCoreName;
            txtMysqlName.Texts = Settings.Default.MySQLCoreName;
            tglCustomNames.Checked = Settings.Default.TogelCustomNames;
            tglHideConsole.Checked = Settings.Default.TogleConsolHide;
            tglNotySound.Checked = Settings.Default.TogelNotySound;
            tglStayInTray.Checked = Settings.Default.TogleStayInTray;
        }
        private void SaveSettings()
        {
            //loading data form settings file(xml)
            Settings.Default.ComboBoxCore = comboBoxCore.SelectedIndex;
            Settings.Default.WorldCoreName = txtWorldName.Texts;
            Settings.Default.ApacheCoreName = txtApacheName.Texts;
            Settings.Default.BnetCoreName = txtBnetName.Texts;
            Settings.Default.MySQLCoreName = txtMysqlName.Texts;
            Settings.Default.WorldCoreLocation = txtWorldLocation.Texts;
            Settings.Default.BnetCoreLocation = txtBnetLocation.Texts;
            Settings.Default.MySQLServerPassword = txtMySqlPassowrd.Texts;
            Settings.Default.MySQLServerPort= txtMySqlPort.Texts;
            Settings.Default.MySQLServerHost = txtMySqlServer.Texts;
            Settings.Default.MySQLServerUsername = txtMySqlUser.Texts;
            Settings.Default.AuthDatabaseName = txtAuthDatabase.Texts;
            Settings.Default.TogleConsolHide = tglHideConsole.Checked;
            Settings.Default.TogelNotySound = tglNotySound.Checked;
            Settings.Default.TogleStayInTray=tglStayInTray.Checked;
            Settings.Default.TogelCustomNames = tglCustomNames.Checked;
            Settings.Default.Save();
        }
        public static void Alert(string message, NotificationType eType)
        {
            //make the alert work.
            FormAlert frm = new(); //dont change this. its fix the Cannot access a disposed object and scall the notification up.
            frm.ShowAlert(message, eType);
        }

        private void GetCoreLocation()
        {
            // getting the core files and location. saves to settings. dose not seed to press save 
            using (var fbd = new FolderBrowserDialog())
            {
                string worndName = Settings.Default.WorldCoreName + ".exe";
                string bnetName = Settings.Default.BnetCoreName + ".exe";

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    foreach (string f in Directory.GetFiles(fbd.SelectedPath, worndName, SearchOption.AllDirectories))
                    {
                        Settings.Default.CoreLocation = Path.GetDirectoryName(f);
                        Settings.Default.Save();
                        LoadSettings();
                    }

                    foreach (string f in Directory.EnumerateFiles(fbd.SelectedPath, worndName, SearchOption.AllDirectories))
                    {
                        txtWorldLocation.Texts = f;
                        SaveSettings();
                    }
                    foreach (string f in Directory.EnumerateFiles(fbd.SelectedPath, bnetName, SearchOption.AllDirectories))
                    {
                        txtBnetLocation.Texts = f;
                        SaveSettings();
                    }
                }
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
            //call the load settings on load
            LoadSettings();
        }
        private void TxtMySqlPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            //allow just numbers/ digits
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point. i dont thik is needed but its nice to have
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
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
            if (Settings.Default.CoreLocation == string.Empty)
            {
                Alert("Server Location Unknow!", NotificationType.Error);
            }
            else
            {
                try
                {
                    Process.Start("explorer.exe", Settings.Default.CoreLocation);
                }
                catch
                {
                    Alert("Server Location Unknow! or invaluable", NotificationType.Error);
                }    
            }     
        }
        private void timerCheck_Tick(object sender, EventArgs e)
        {
            if(tglCustomNames.Checked == true)
            {
                txtWorldName.ReadOnly = false;
                txtBnetName.ReadOnly = false;
                txtApacheName.ReadOnly = false; 
                txtMysqlName.ReadOnly = false;
            }
            else
            {
                txtWorldName.ReadOnly = true;
                txtBnetName.ReadOnly = true;
                txtApacheName.ReadOnly = true;
                txtMysqlName.ReadOnly = true;
            }
        }
        private void comboBoxCore_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCore.SelectedIndex == 0)
            {
                // AscEmu
                Settings.Default.ComboBoxCore = comboBoxCore.SelectedIndex;
                Settings.Default.BnetCoreName = "logon";
                Settings.Default.WorldCoreName = "world";
                Settings.Default.Save();
                LoadSettings();
            }
            else if(comboBoxCore.SelectedIndex == 1)
            {
                //AzerothCore
                Settings.Default.ComboBoxCore = comboBoxCore.SelectedIndex;
                Settings.Default.BnetCoreName = "authserver";
                Settings.Default.WorldCoreName = "worldserver";
                Settings.Default.Save();
                LoadSettings();
            }
            else if (comboBoxCore.SelectedIndex == 2)
            {
                //Continued MaNGOS
                Settings.Default.ComboBoxCore = comboBoxCore.SelectedIndex;
                Settings.Default.BnetCoreName = "realmd";
                Settings.Default.WorldCoreName = "mangosd";
                Settings.Default.Save();
                LoadSettings();
            }
            else if (comboBoxCore.SelectedIndex == 3)
            {
                //CypherCore
                Settings.Default.ComboBoxCore = comboBoxCore.SelectedIndex;
                Settings.Default.BnetCoreName = "BNetServer";
                Settings.Default.WorldCoreName = "WorldServer";
                Settings.Default.Save();
                LoadSettings();
            }
            else if (comboBoxCore.SelectedIndex == 4)
            {
                //TrinityCore
                Settings.Default.ComboBoxCore = comboBoxCore.SelectedIndex;
                Settings.Default.BnetCoreName = "bnetserver";
                Settings.Default.WorldCoreName = "worldserver";
                Settings.Default.Save();
                LoadSettings();
            }
            else if (comboBoxCore.SelectedIndex == 5)
            {
                //TrinityCore 4.3.4(TCPP)
                Settings.Default.ComboBoxCore = comboBoxCore.SelectedIndex;
                Settings.Default.BnetCoreName = "bnetserver";
                Settings.Default.WorldCoreName = "worldserver";
                Settings.Default.Save();
                LoadSettings();
            }
            else if (comboBoxCore.SelectedIndex == 6)
            {
                //Vanilla MaNGOS
                Settings.Default.ComboBoxCore = comboBoxCore.SelectedIndex;
                Settings.Default.BnetCoreName = "realmd";
                Settings.Default.WorldCoreName = "mangosd";
                Settings.Default.Save();
                LoadSettings();
            }
        }
        private void btnTestMySQL_Click(object sender, EventArgs e)
        {
            databaseConnection.MySqlConnectCheck();
        }
        private void picSettingsInfos_Click(object sender, EventArgs e)
        {
            HomeControl.Alert("You need to restart the application to make the change work!", NotificationType.Info);
        }

        private void tglStayInTray_CheckedChanged(object sender, EventArgs e)
        {
            if(tglStayInTray.Checked== true)
            {
                Settings.Default.TogleStayInTray = true;
            }else if(Settings.Default.TogleStayInTray == false)
            {
                Settings.Default.TogleStayInTray = false;
            }
        }
    }
}
