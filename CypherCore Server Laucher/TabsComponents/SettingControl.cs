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
            txtWorldLocation.Text = Settings.Default.WorldCoreLocation;
            txtBnetLocation.Text = Settings.Default.BnetCoreLocation;
            txtMySqlPassowrd.Text = Settings.Default.MySQLServerPassword;
            txtMySqlPort.Text = Settings.Default.MySQLServerPort;
            txtMySqlServer.Text = Settings.Default.MySQLServerName;
            txtMySqlUser.Text = Settings.Default.MySQLServerUsername;
            txtWorldDatabase.Text = Settings.Default.WorldDatabaseName;
            txtAuthDatabase.Text = Settings.Default.AuthDatabaseName;
            txtCharactersDatabase.Text = Settings.Default.CharactersDatabaseName;
            tglHideConsole.Checked = Settings.Default.TogleConsolHide;
            tglNotySound.Checked = Settings.Default.TogelNotySound;
            tglStayInTray.Checked = Settings.Default.TogleStayInTray;
        }
        private void SaveSettings()
        {
            //loading data form settings file(xml)
            Settings.Default.WorldCoreLocation = txtWorldLocation.Text;
            Settings.Default.BnetCoreLocation = txtBnetLocation.Text;
            Settings.Default.MySQLServerPassword = txtMySqlPassowrd.Text;
            Settings.Default.MySQLServerPort= txtMySqlPort.Text ;
            Settings.Default.MySQLServerName = txtMySqlServer.Text;
            Settings.Default.MySQLServerUsername = txtMySqlUser.Text;
            Settings.Default.WorldDatabaseName = txtWorldDatabase.Text;
            Settings.Default.AuthDatabaseName = txtAuthDatabase.Text ;
            Settings.Default.CharactersDatabaseName=txtCharactersDatabase.Text ;
            Settings.Default.TogleConsolHide = tglHideConsole.Checked;
            Settings.Default.TogelNotySound = tglNotySound.Checked;
            Settings.Default.TogleStayInTray=tglStayInTray.Checked;
            Settings.Default.Save();

        }
        public void Alert(string message, NotificationType eType)
        {
            //make the laert work.
            FormAlert frm = new FormAlert(); //dont change this. its fix the Cannot access a disposed object and scall the notification up.
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

        private void txtMySqlPort_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void bntLocation_Click(object sender, EventArgs e)
        {
            //call GetCoreLocation
            GetCoreLocation();
        }

        private void bntOpenLocation_Click(object sender, EventArgs e)
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
    }
}
