using CypherCore_Server_Laucher.Classes;
using CypherCore_Server_Laucher.Forms;
using CypherCoreServerLaucher.Properties;
using System.Diagnostics;

namespace CypherCore_Server_Laucher.TabsComponents
{
    public partial class HomeControl : UserControl
    {

        readonly StatusClass _statusClass = new();
        private bool _isRuningBnet = false;
        private bool _isRuningWorld = false;
        public static void Alert(string message, NotificationType eType)
        {
            //make the laert work.
            FormAlert frm = new(); //dont change this. its fix the Cannot access a disposed object and scall the notification up.
            frm.ShowAlert(message, eType);
        }
        private static void StartWorld()
        {
            try
            {
                using (Process myProcess = new())
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
        private static void StartBnet()
        {
            try
            {

                using (Process myProcess = new())
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
            Thread BnetResourcesUsageThread = new(() =>
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
            Thread WorldResourcesUsageThread = new(() =>
            {
                try
                {
                    worldCpuUsageProgressBar.Value = _statusClass.WorldCpuUsage() / 10;
                    worldRamUsageProgressBar.Value = _statusClass.WorldRamUsage();
                }
                catch
                {

                }

            });
            WorldResourcesUsageThread.Start();
        }

        private void ServerStatusTimer_Tick(object sender, EventArgs e)
        {
            Thread PCResorceUsageThread = new(() =>
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
                if (_isRuningWorld == true)
                {
                    Alert("World server crashed or shutdown unexpectedly.", NotificationType.Error);
                    _isRuningBnet = false;
                }

            }
            if (_statusClass.BnetStatus() == true)
            {
                bnetServerLight.BackColor = Color.Green;
                BnetResourceTimer.Start();


            }
            else
            {
                if (_isRuningBnet == true)
                {
                    Alert("Bnet server crashed or shutdown unexpectedly.", NotificationType.Error);
                    _isRuningBnet = false;
                }
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

        private void BtnStartWorld_Click(object sender, EventArgs e)
        {
            _isRuningWorld = true;
            StartWorld();
        }

        private void BtnStartBent_Click(object sender, EventArgs e)
        {
            _isRuningBnet = true;
            StartBnet();
        }

        private void BntStartAll_Click(object sender, EventArgs e)
        {
            _isRuningBnet = true;
            _isRuningWorld = true;
            StartBnet();
            StartWorld();
        }

        private void BntStopAll_Click(object sender, EventArgs e)
        {
            _isRuningBnet = false;
            _isRuningWorld = false;
            _statusClass.KillWorld();
            _statusClass.KillBnet();
        }

        private void BtnStopWorld_Click(object sender, EventArgs e)
        {
            _isRuningWorld = false;
            _statusClass.KillWorld();

        }

        private void BtnStopBnet_Click(object sender, EventArgs e)
        {
            _isRuningBnet = false;
            _statusClass.KillBnet();
        }
    }
}
