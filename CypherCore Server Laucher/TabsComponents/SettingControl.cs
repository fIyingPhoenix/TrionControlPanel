using System.ComponentModel;
using Cypher_CoreServer_Laucher.Properties;
using CypherCore_Server_Laucher.Classes;
using CypherCore_Server_Laucher.Forms;
namespace CypherCore_Server_Laucher.TabsComponents
{
    public partial class SettingControl : UserControl
    {
        FormAlert frm = new FormAlert();

        private void LoadSettings()
        {
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
        public void Alert(string title, string message, NotificationType eType)
        {
            frm.ShowAlert(title, message, eType);
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
            LoadSettings();
        }

        private void txtMySqlPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
