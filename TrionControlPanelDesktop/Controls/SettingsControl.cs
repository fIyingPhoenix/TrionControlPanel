using static TrionLibrary.EnumModels;
using TrionLibrary;
using TrionControlPanelDesktop.FormData;

namespace TrionControlPanelDesktop.Controls
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            this.Dock = DockStyle.Fill;
            InitializeComponent();
            LoadData();
        }


        private void SettingsControl_Load(object sender, EventArgs e)
        {
            ComboBoxCores.Items.AddRange(Enum.GetNames(typeof(Cores)));
            ComboBoxCores.SelectedIndex = (int)Data.Settings.SelectedCore;
        }
        private async Task LoadData()
        {
            await Data.LoadSettings();
            TXTBoxLoginExecName.Text = Data.Settings.BnetExecutableName;
            TXTBoxWorldExecName.Text = Data.Settings.WorldExecutableName;
            TXTBoxMySQLExecName.Text = Data.Settings.MySQLExecutableName;
            TXTBoxCoreExecLocation.Text = Data.GetExecutableLocation(Data.Settings.WorldExecutableName);
            TXTBoxMySQLExecLocation.Text = Data.GetExecutableLocation(Data.Settings.MySQLExecutableName);

            UIData.StartUpLoading++;
        }
    }
}
