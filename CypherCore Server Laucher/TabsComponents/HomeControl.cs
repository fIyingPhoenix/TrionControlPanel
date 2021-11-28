using CypherCore_Server_Laucher.Classes;

namespace CypherCore_Server_Laucher.TabsComponents
{
    public partial class HomeControl : UserControl
    {
        StatusClass _statusClass = new StatusClass();
      
        public HomeControl()
        {
            InitializeComponent();
        }

        private void BnetResourceTimer_Tick(object sender, EventArgs e)
        {
            Thread BnetResourcesUsageThread = new Thread(() =>
            {
                try
                {
                    BnetCpuUsageProgressBar.Value = _statusClass.BnetCpuUsage();
                    BnetRamUsageProgressBar.Value = _statusClass.BnetRamUsage();
                }
                catch
                {

                }
              
            });
            BnetResourcesUsageThread.Start();
        }

        private void WorldResourceTimer_Tick(object sender, EventArgs e)
        {
            Thread WorldResourcesUsageThread = new Thread(() =>
            {
                try
                {
                    worldCpuUsageProgressBar.Value = _statusClass.WorldCpuUsage();
                    worldRamUsageProgressBar.Value = _statusClass.WorldRamUsage();
                }catch
                {

                }
                
            });
            WorldResourcesUsageThread.Start();
        }

        private void ServerStatusTimer_Tick(object sender, EventArgs e)
        {

            Thread PCResorceUsageThread = new Thread(() =>
            {
                try
                {
                    worldRamUsageProgressBar.Maximum = _statusClass.TotalPCRam();
                    BnetRamUsageProgressBar.Maximum = _statusClass.TotalPCRam();
                    totalRamUsageProgressBar.Maximum = _statusClass.TotalPCRam();
                    totalCpuUsageProgressBar.Value = _statusClass.TotalCpuUsage();
                    totalRamUsageProgressBar.Value = _statusClass.TotalPCRam() - _statusClass.CurentPcRamUsage();

                }
                catch     
                {
                 
                }
            });
            PCResorceUsageThread.Start();


            if (_statusClass.WorldStatus() == true)
            {
                worldServerLight.BackColor = Color.Green;
                WorldResourceTimer.Start();
            }
            else
            {
                worldServerLight.BackColor = Color.Red;
                WorldResourceTimer.Stop();
            }
            if (_statusClass.BnetStatus() == true)
            {
                bnetServerLight.BackColor = Color.Green;
                BnetResourceTimer.Start();
                
            }
            else
            {
                bnetServerLight.BackColor = Color.Red;
                BnetResourceTimer.Stop();
            }
            if (_statusClass.MySQLstatus() == true)
            {
                mysqlServerLight.BackColor = Color.Green;
            }
            else
            {
                mysqlServerLight.BackColor = Color.Red;
            }
            if (_statusClass.ApacheStatus() == true)
            {
                apacheServerLight.BackColor = Color.Green;
            }
            else
            {
                apacheServerLight.BackColor = Color.Red;
            }

        }

    }
}
