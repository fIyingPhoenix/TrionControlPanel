using CypherCore_Server_Laucher.Classes;
using CypherCore_Server_Laucher.Forms;
using CypherCoreServerLaucher.Properties;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CypherCore_Server_Laucher.TabsComponents
{
    public partial class HomeControl : UserControl
    {

        StatusClass _statusClass = new StatusClass();

        public void Alert(string message, NotificationType eType)
        {
            //make the laert work.
            FormAlert frm = new FormAlert(); //dont change this. its fix the Cannot access a disposed object and scall the notification up.
            frm.ShowAlert(message, eType);
        }

        private void StartWorld()
        {
            try
            {
               
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    // You can start any process, HelloWorld is a do-nothing example.
                    myProcess.StartInfo.FileName = Settings.Default.WorldCoreLocation;

                    if (Settings.Default.TogleConsolHide == false)
                    {
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.Start();
                    }
                    else if (Settings.Default.TogleConsolHide == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    Alert("Starting World Server!", NotificationType.Info);
                }
            }
            catch (Exception ex)    
            {
                Alert(ex.Message, NotificationType.Error);
            }
            
        }
        private void StartBnet()
        {
            try
            {

                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    // You can start any process, HelloWorld is a do-nothing example.
                    myProcess.StartInfo.FileName = Settings.Default.BnetCoreLocation;

                    if (Settings.Default.TogleConsolHide == false)
                    {
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.Start();
                    }
                    else if (Settings.Default.TogleConsolHide == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    Alert("Starting Bnet Server!", NotificationType.Info);
                }
            }
            catch (Exception ex)
            {
                Alert(ex.Message, NotificationType.Error);
            }
        }

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
                    BnetCpuUsageProgressBar.Value = _statusClass.BnetCpuUsage() / 10;
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
                    worldCpuUsageProgressBar.Value = _statusClass.WorldCpuUsage() / 10;
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

        private void HomeControl_Load(object sender, EventArgs e)
        {

        }

        private void btnStartWorld_Click(object sender, EventArgs e)
        {
            StartWorld();
        }

        private void btnStartBent_Click(object sender, EventArgs e)
        {
            StartBnet();
        }

        private void bntStartAll_Click(object sender, EventArgs e)
        {
            StartBnet();
            StartWorld();
        }

        private void bntStopAll_Click(object sender, EventArgs e)
        {
            _statusClass.KillWorld();
            _statusClass.KillBnet();    
        }

        private void btnStopWorld_Click(object sender, EventArgs e)
        {
            _statusClass.KillWorld();
            
        }

        private void btnStopBnet_Click(object sender, EventArgs e)
        {
            _statusClass.KillBnet();
        }
    }
}
