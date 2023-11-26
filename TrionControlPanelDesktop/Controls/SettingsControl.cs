using static TrionLibrary.EnumModels;
using TrionLibrary;

namespace TrionControlPanelDesktop.Controls
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            this.Dock = DockStyle.Fill;
            InitializeComponent();
        }

        private void SettingsControl_Load(object sender, EventArgs e)
        {
            ComboBoxCores.Items.AddRange(Enum.GetNames(typeof(Cores)));
            ComboBoxCores.SelectedIndex = (int)Data.Settings.SelectedCore;
        }
    }
}
