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
            Dock = DockStyle.Fill;
            InitializeComponent();
        }
        private void ServerStatus()
        {
            if (UIData.WorldisRunning == true) { PICWorldServerStatus.Image = Properties.Resources.verbindungsstatus__an_35; }
            if (UIData.WorldisRunning == false) { PICWorldServerStatus.Image = Properties.Resources.verbindungsstatus_35; }
            if (UIData.LogonisRunning == true) { PICLogonServerStatus.Image = Properties.Resources.verbindungsstatus__an_35; }
            if (UIData.LogonisRunning == false) { PICLogonServerStatus.Image = Properties.Resources.verbindungsstatus_35; }
            if (UIData.MySQLisRunning == true) { PICMySqlServerStatus.Image = Properties.Resources.verbindungsstatus__an_35; }
            if (UIData.MySQLisRunning == false) { PICMySqlServerStatus.Image = Properties.Resources.verbindungsstatus_35; }
        }

        private void HomeControl_Load(object sender, EventArgs e)
        {
            FirstLoad();
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            ServerStatus();
            if (PCResorcePbarRAM.Value > 0) { UIData.StartUpLoading++; }
            try
            {
                Thread PCResorceUsageThread = new(() =>
                {
                    int CurrentRamUsage = SystemWatcher.TotalRam() - SystemWatcher.CurentPcRamUsage();
                    PCResorcePbarRAM.Maximum = SystemWatcher.TotalRam();
                    LoginPbarRAM.Maximum = CurrentRamUsage;
                    WorldPbarRAM.Maximum = CurrentRamUsage;

                    WorldPbarRAM.Value = SystemWatcher.ApplicationRamUsage(Data.Settings.WorldExecutableName);
                    WorldPbarCPU.Value = SystemWatcher.ApplicationCpuUsage(Data.Settings.WorldExecutableName);
                    LoginPbarRAM.Value = SystemWatcher.ApplicationRamUsage(Data.Settings.LogonExecutableName);
                    LoginPbarCPU.Value = SystemWatcher.ApplicationCpuUsage(Data.Settings.LogonExecutableName);
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
