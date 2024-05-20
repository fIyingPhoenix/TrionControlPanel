using TrionControlPanelDesktop.FormData;
using TrionLibrary;

namespace TrionControlPanelDesktop.Controls
{
    public partial class HomeControl : UserControl
    {
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
            if (User.UI.Form.WorldisRunning == true) { PICWorldServerStatus.Image = Properties.Resources.verbindungsstatus__an_35; }
            if (User.UI.Form.WorldisRunning == false) { PICWorldServerStatus.Image = Properties.Resources.verbindungsstatus_35; }
            if (User.UI.Form.LogonisRunning == true) { PICLogonServerStatus.Image = Properties.Resources.verbindungsstatus__an_35; }
            if (User.UI.Form.LogonisRunning == false) { PICLogonServerStatus.Image = Properties.Resources.verbindungsstatus_35; }
            if (User.UI.Form.MySQLisRunning == true) { PICMySqlServerStatus.Image = Properties.Resources.verbindungsstatus__an_35; }
            if (User.UI.Form.MySQLisRunning == false) { PICMySqlServerStatus.Image = Properties.Resources.verbindungsstatus_35; }
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
        static double CalculatePercentage(double whole, double part)
        {
            return (part / whole) * 100;
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            ServerStatus();
            TimerRam.Enabled = true;
            if (PCResorcePbarRAM.Value > 0) { User.UI.Form.StartUpLoading++; }
            try
            {
                Thread PCResorceUsageThread = new(() =>
                {
                    User.UI.Resource.MachineTotalRam = SystemWatcher.TotalRam();
                    User.UI.Resource.MachineUsageRam = SystemWatcher.TotalRam() - SystemWatcher.CurentPcRamUsage();
                    //
                    RamProcent = CalculatePercentage(User.UI.Resource.MachineTotalRam, User.UI.Resource.MachineUsageRam);
                    //
                    User.UI.Resource.AuthTotalRam = (int)User.UI.Resource.MachineUsageRam;
                    User.UI.Resource.WorldTotalRam = (int)User.UI.Resource.MachineUsageRam;
                    //
                    User.UI.Resource.WorldUsageRam = SystemWatcher.ApplicationRamUsage(Data.Settings.WorldExecutableName);
                    User.UI.Resource.WorldCPUUsage = SystemWatcher.ApplicationCpuUsage(Data.Settings.WorldExecutableName,0);
                    User.UI.Resource.AuthUsageRam = SystemWatcher.ApplicationRamUsage(Data.Settings.LogonExecutableName);
                    User.UI.Resource.AuthCPUUsage = SystemWatcher.ApplicationCpuUsage(Data.Settings.LogonExecutableName, 0);
                    User.UI.Resource.MachineCPUUsage = SystemWatcher.MachineCpuUtilization();
                    //
                    User.UI.Form.MySQLisRunning = SystemWatcher.ApplicationRuning(Data.Settings.MySQLExecutableName);
                    User.UI.Form.WorldisRunning = SystemWatcher.ApplicationRuning(Data.Settings.WorldExecutableName);
                    User.UI.Form.LogonisRunning = SystemWatcher.ApplicationRuning(Data.Settings.LogonExecutableName);
                });
                PCResorceUsageThread.Start();
                LBLMysqlPort.Text = $"ProcessID: {string.Join(", ", User.System.DatabaseProcessID)}";
                LBLLogonPort.Text = $"ProcessID: {string.Join(", ", User.System.LogonProcessesID)}";
                LBLWordPort.Text = $"ProcessID: {string.Join(", ", User.System.WorldProcessesID)}";
                PCResorcePbarRAM.Maximum = User.UI.Resource.MachineTotalRam;
                PCResorcePbarRAM.Value = User.UI.Resource.MachineUsageRam;
                PCResorcePbarCPU.Value = User.UI.Resource.MachineCPUUsage;
                WorldPbarRAM.Maximum = User.UI.Resource.WorldTotalRam;
                WorldPbarRAM.Value = User.UI.Resource.WorldUsageRam;
                WorldPbarCPU.Maximum = User.UI.Resource.WorldCPUUsage;
                LoginPbarRAM.Maximum = User.UI.Resource.AuthTotalRam;
                LoginPbarRAM.Value = User.UI.Resource.AuthUsageRam;
                LoginPbarRAM.Value = User.UI.Resource.AuthCPUUsage;
            }
            catch
            {
            }
        }
        private void TimerRam_Tick(object sender, EventArgs e)
        {
            RamProcentage();
        }

        private void TimerStopWatch_Tick(object sender, EventArgs e)
        {
            if (User.UI.Form.WorldisRunning == true && User.System.WorldProcessesID.Count > 0)
            {
                TimeSpan elapsedTime = DateTime.Now - User.System.WorldStartTime;
                LBLUpTimeWorld.Text = $"Up Time: {elapsedTime.Days}D : {elapsedTime.Hours}H : {elapsedTime.Minutes}M : {elapsedTime.Seconds}S";
            }
            if (User.UI.Form.MySQLisRunning == true && User.System.DatabaseProcessID.Count > 0)
            {
                TimeSpan elapsedTime = DateTime.Now - User.System.DatabaseStartTime;
                LBLUpTimeDatabase.Text = $"Up Time: {elapsedTime.Days}D : {elapsedTime.Hours}H : {elapsedTime.Minutes}M : {elapsedTime.Seconds}S";
            }
            if (User.UI.Form.LogonisRunning == true && User.System.LogonProcessesID.Count >  0)
            {
                TimeSpan elapsedTime = DateTime.Now - User.System.LogonStartTime;
                LBLUpTimeLogon.Text = $"Up Time: {elapsedTime.Days}D : {elapsedTime.Hours}H : {elapsedTime.Minutes}M : {elapsedTime.Seconds}S";
            }
        }
    }
}
