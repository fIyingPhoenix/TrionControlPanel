using System.ComponentModel;
using CypherCoreServerLaucher.Properties;
using CypherCore_Server_Laucher.Classes;
using CypherCore_Server_Laucher.Forms;
using System.Diagnostics;
using System.Windows;

namespace CypherCore_Server_Laucher.TabsComponents
{
    public partial class SettingControl : UserControl
    {
        //settings data located in appdata/local/CypherCoreServerLaucher

        private void LoadSettings()
        {
            //loading data form settings file(xml)
            txtWorldLocation.Texts = Settings.Default.WorldCoreLocation;
            txtBnetLocation.Texts = Settings.Default.BnetCoreLocation;
            txtMySqlPassowrd.Texts = Settings.Default.MySQLServerPassword;
            txtMySqlPort.Texts = Settings.Default.MySQLServerPort;
            txtMySqlServer.Texts = Settings.Default.MySQLServerName;
            txtMySqlUser.Texts = Settings.Default.MySQLServerUsername;
            txtWorldDatabase.Texts = Settings.Default.WorldDatabaseName;
            txtAuthDatabase.Texts = Settings.Default.AuthDatabaseName;
            txtCharactersDatabase.Texts = Settings.Default.CharactersDatabaseName;
            tglHideConsole.Checked = Settings.Default.TogleConsolHide;
            tglNotySound.Checked = Settings.Default.TogelNotySound;
            tglStayInTray.Checked = Settings.Default.TogleStayInTray;
        }
        private void SaveSettings()
        {
            //loading data form settings file(xml)
            Settings.Default.WorldCoreLocation = txtWorldLocation.Texts;
            Settings.Default.BnetCoreLocation = txtBnetLocation.Texts;
            Settings.Default.MySQLServerPassword = txtMySqlPassowrd.Texts;
            Settings.Default.MySQLServerPort= txtMySqlPort.Texts;
            Settings.Default.MySQLServerName = txtMySqlServer.Texts;
            Settings.Default.MySQLServerUsername = txtMySqlUser.Texts;
            Settings.Default.WorldDatabaseName = txtWorldDatabase.Texts;
            Settings.Default.AuthDatabaseName = txtAuthDatabase.Texts;
            Settings.Default.CharactersDatabaseName=txtCharactersDatabase.Texts;
            Settings.Default.TogleConsolHide = tglHideConsole.Checked;
            Settings.Default.TogelNotySound = tglNotySound.Checked;
            Settings.Default.TogleStayInTray=tglStayInTray.Checked;
            Settings.Default.Save();

        }
        public static void Alert(string message, NotificationType eType)
        {
            //make the laert work.
            FormAlert frm = new(); //dont change this. its fix the Cannot access a disposed object and scall the notification up.
            frm.ShowAlert(message, eType);
        }

        private void GetCoreLocation()
        {
            // getting the core files and location. saves to settings. dose not seed to press save 
            using (var fbd = new FolderBrowserDialog())
            {
                
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    foreach (string f in Directory.GetFiles(fbd.SelectedPath, "WorldServer.exe", SearchOption.AllDirectories))
                    {
                        Settings.Default.CoreLocation = Path.GetDirectoryName(f);
                        Settings.Default.Save();
                    }

                    foreach (string f in Directory.EnumerateFiles(fbd.SelectedPath, "WorldServer.exe", SearchOption.AllDirectories))
                    {
                        txtWorldLocation.Text = f;
                    };
                    foreach (string f in Directory.EnumerateFiles(fbd.SelectedPath, "BNetServer.exe", SearchOption.AllDirectories))
                    {
                        txtBnetLocation.Text = f;
                    };
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
            //call GetCoreLocation
            GetCoreLocation();
        }

        private void BntOpenLocation_Click(object sender, EventArgs e)
        {
            //just a fail safe. inc ase the CoreLocation is empty.
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

        private void tglStayInTray_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void customToggleButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tglNotySound_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
