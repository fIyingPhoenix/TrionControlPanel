using static TrionLibrary.EnumModels;
using TrionLibrary;
using TrionControlPanelDesktop.FormData;
using System.Reflection;

namespace TrionControlPanelDesktop.Controls
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            _ = LoadData();
        }
        private void SettingsControl_Load(object sender, EventArgs e)
        {
            ComboBoxCores.Items.AddRange(Enum.GetNames(typeof(Cores)));
            ComboBoxCores.SelectedItem = Data.Settings.SelectedCore.ToString();
        }
        private async Task LoadData()
        {
            await Data.LoadSettings();
            //Trion version
            LBLTrionVersion.Text = $"Trion Version: Local {UIData.TrionVersion} / Online {UIData.TrionVersion}";
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
            //Update Loader
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
                    Data.SaveSettings();
                    break;
                case "AzerothCore":
                    Data.Settings.WorldExecutableName = "worldserver";
                    Data.Settings.LogonExecutableName = "authserver";
                    Data.Settings.SelectedCore = Cores.AzerothCore;
                    Data.SaveSettings();
                    break;
                case "CMaNGOS":
                    Data.Settings.WorldExecutableName = "mangosd";
                    Data.Settings.LogonExecutableName = "realmd";
                    Data.Settings.SelectedCore = Cores.CMaNGOS;
                    Data.SaveSettings();
                    break;
                case "CypherCore":
                    Data.Settings.WorldExecutableName = "WorldServer";
                    Data.Settings.LogonExecutableName = "BNetServer";
                    Data.Settings.SelectedCore = Cores.CypherCore;
                    Data.SaveSettings();
                    break;
                case "TrinityCore":
                    Data.Settings.WorldExecutableName = "bnetserver";
                    Data.Settings.LogonExecutableName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCore;
                    Data.SaveSettings();
                    break;
                case "TrinityCore335":
                    Data.Settings.WorldExecutableName = "authserver";
                    Data.Settings.LogonExecutableName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCore335;
                    Data.SaveSettings();
                    break;
                case "TrinityCoreClassic":
                    Data.Settings.WorldExecutableName = "bnetserver";
                    Data.Settings.LogonExecutableName = "worldserver";
                    Data.Settings.SelectedCore = Cores.TrinityCoreClassic;
                    Data.SaveSettings();
                    break;
                case "VMaNGOS":
                    Data.Settings.WorldExecutableName = "mangosd";
                    Data.Settings.LogonExecutableName = "realmd";
                    Data.Settings.SelectedCore = Cores.VMaNGOS;
                    Data.SaveSettings();
                    break;
            }
            TXTBoxLoginExecName.Text = Data.Settings.LogonExecutableName;
            TXTBoxWorldExecName.Text = Data.Settings.WorldExecutableName;
        }
        private void TGLStayInTrey_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.StayInTray = TGLStayInTrey.Checked;
            Data.SaveSettings();
        }
        private void TGLNotificationSound_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.NotificationSound = TGLNotificationSound.Checked;
            Data.SaveSettings();
        }
        private void TGLHideConsole_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.ConsolHide = TGLHideConsole.Checked;
            Data.SaveSettings();
        }
        private void TGLAutoUpdateTrion_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.AutoUpdateTrion = TGLAutoUpdateTrion.Checked;
            Data.SaveSettings();
        }
        private void TGLAutoUpdateCore_CheckedChanged(object sender, EventArgs e)
        {
            Data.Settings.AutoUpdateCore = TGLAutoUpdateCore.Checked;
            Data.SaveSettings();
        }

        private void BTNMySQLExecLovation_Click(object sender, EventArgs e)
        {

            Data.CreateMySQLConfigFile(Data.Settings.MySQLLocation);
        }
    }
}
