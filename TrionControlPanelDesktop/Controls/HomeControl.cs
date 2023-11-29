using TrionControlPanelDesktop.FormData;
using TrionLibrary;

namespace TrionControlPanelDesktop.Controls
{
    public partial class HomeControl : UserControl
    {
        private static void FirstLoad()
        {

        }
        public HomeControl()
        {
            this.Dock = DockStyle.Fill;
            InitializeComponent();
        }

        private void HomeControl_Load(object sender, EventArgs e)
        {
            FirstLoad();
        }

        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            if (PCResorcePbarRAM.Value > 0) { UIData.StartUpLoading++; }
            try
            {
                Thread PCResorceUsageThread = new(() =>
                {
                    int CurrentRamUsage = SystemWatcher.TotalRam() - SystemWatcher.CurentPcRamUsage();
                    PCResorcePbarRAM.Maximum = SystemWatcher.TotalRam();
                    PCLoginPbarRAM.Maximum = CurrentRamUsage;
                    PCWorldPbarRAM.Maximum = CurrentRamUsage;

                    PCWorldPbarRAM.Value = SystemWatcher.ApplicationRamUsage("");
                    PCResorcePbarRAM.Value = CurrentRamUsage;
                    PCResorcePbarCPU.Value = SystemWatcher.MachineCpuUtilization();
                });

                PCResorceUsageThread.Start();
            }
            catch
            {
            }
        }
    }
}
