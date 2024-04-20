using TrionControlPanelDesktop.FormData;
using TrionLibrary;

namespace TrionControlPanelDesktop.Controls
{
    public partial class HomeControl : UserControl
    {
        static double CurrentRamUsage;
        static double RamProcent;
        bool RamUsageHight;
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
        private void RamProcentage()
        {
            if (RamProcent > 80 && RamUsageHight == false)
            {
                Data.Message = "Your Ram is in a critical availability phase! More than 80% are used!!";
                RamUsageHight = true;
            }
            if (RamProcent < 80)
            {
                RamUsageHight = false;
            }
        }
        static double CalculatePercentage( double whole, double part)
        {
            return (part / whole) * 100;
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            ServerStatus();
            TimerRam.Enabled = true;
            if (PCResorcePbarRAM.Value > 0) { UIData.StartUpLoading++; }
            try
            {
                Thread PCResorceUsageThread = new(() =>
                {
                    CurrentRamUsage = SystemWatcher.TotalRam() - SystemWatcher.CurentPcRamUsage();
                    RamProcent = CalculatePercentage(SystemWatcher.TotalRam(), CurrentRamUsage);
                    PCResorcePbarRAM.Maximum = SystemWatcher.TotalRam();
                    LoginPbarRAM.Maximum = (int)CurrentRamUsage;
                    WorldPbarRAM.Maximum = (int)CurrentRamUsage;

                    WorldPbarRAM.Value = SystemWatcher.ApplicationRamUsage(Data.Settings.WorldExecutableName);
                    WorldPbarCPU.Value = SystemWatcher.ApplicationCpuUsage(Data.Settings.WorldExecutableName);
                    LoginPbarRAM.Value = SystemWatcher.ApplicationRamUsage(Data.Settings.LogonExecutableName);
                    LoginPbarCPU.Value = SystemWatcher.ApplicationCpuUsage(Data.Settings.LogonExecutableName);
                    PCResorcePbarRAM.Value = (int)CurrentRamUsage;
                    PCResorcePbarCPU.Value = SystemWatcher.MachineCpuUtilization();

                    UIData.MySQLisRunning = SystemWatcher.ApplicationRuning(Data.Settings.MySQLExecutableName);
                    UIData.WorldisRunning = SystemWatcher.ApplicationRuning(Data.Settings.WorldExecutableName);
                    UIData.LogonisRunning = SystemWatcher.ApplicationRuning(Data.Settings.LogonExecutableName);
                });

                PCResorceUsageThread.Start();
            }
            catch
            {
            }
        }

        private void TimerRam_Tick(object sender, EventArgs e)
        {
            RamProcentage();
        }
    }
}
