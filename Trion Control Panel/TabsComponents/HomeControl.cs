using TrionControlPanel.Classes;
using TrionControlPanel.Alerts;
using TrionControlPanel.Properties;
using TrionControlPanelUI;
using System.Net;
using System.IO.Compression;
using System.ComponentModel;
using TrionControlPanelSettings;

namespace TrionControlPanel.TabsComponents
{
    public partial class HomeControl : UserControl
    {
        Settings Settings = new();
        readonly SystemStatus _statusClass = new();
        internal bool _isRuningBnet = false;
        internal bool _isRuningWorld = false;
        internal bool _isRuningMysql = false;
        public HomeControl()
        {
            this.Dock = DockStyle.Fill;
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
                if (_isRuningWorld == true)
                {
                    _isRuningWorld = false;
                    FormAlert.ShowAlert("World server crashed or shutdown unexpectedly.", NotificationType.Error);
                }
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
                if (_isRuningBnet == true)
                {
                    _isRuningBnet = false;
                    FormAlert.ShowAlert("Bnet server crashed or shutdown unexpectedly.", NotificationType.Error);    
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
        }
        private void HomeControl_Load(object sender, EventArgs e)
        { 
        }
        private void BtnStartWorld_Click(object sender, EventArgs e)
        {
            _isRuningWorld = true;
            _statusClass.StartWorld();
        }
        private void BtnStartBent_Click(object sender, EventArgs e)
        {
            _isRuningBnet = true;
            _statusClass.StartBnet();
        }
        private void BntStartAll_Click(object sender, EventArgs e)
        {
            if (_statusClass.MySQLstatus() == true)
            {
                _isRuningMysql = true;
            }
            else
            {
                _statusClass.StartMysql();
                _isRuningMysql = true;
            }
            int milliseconds = 5000;
            var timerStart = new System.Windows.Forms.Timer();
            //
            timerStart.Interval = milliseconds;
            timerStart.Enabled = true;
            timerStart.Start();
            //
            timerStart.Tick += (s, e) =>
            {
                timerStart.Enabled = false;
                timerStart.Stop();
                _isRuningBnet = true;
                _isRuningWorld = true;
                _statusClass.StartBnet();
                _statusClass.StartWorld();
            };
        }
        private void BtnStartMysql_Click(object sender, EventArgs e)
        {
            _statusClass.StartMysql();
            _isRuningMysql = true;
        }
        private void BntStopMysql_Click(object sender, EventArgs e)
        {
            _statusClass.KillMysql();
            _isRuningMysql = false;
        }
        private void BntStopAll_Click(object sender, EventArgs e)
        {
            _isRuningBnet = false;
            _isRuningWorld = false;
            _isRuningMysql = false;
            _statusClass.KillWorld();
            _statusClass.KillBnet();
            _statusClass.KillMysql();
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
        private void BntDownloadMysql_Click(object sender, EventArgs e)
        {
            
            string mysqlName = $@"{Settings._Data.MySQLExecutableName}.exe";
            //
            pBarDownloadMysql.Visible = true;
            //
            if (!Directory.Exists($@"{Directory.GetCurrentDirectory()}\mysql"))
            {
                Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\mysql");
            }
            //
            //
            string sharingUrl = "https://1drv.ms/u/s!ApVjHQD9ApL5mjxAJFwwfyeXzYtO?e=kTxLE1";
            string base64Value = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(sharingUrl));
            string encodedUrl = "u!" + base64Value.TrimEnd('=').Replace('/', '_').Replace('+', '-');
            string resultUrl = string.Format("https://api.onedrive.com/v1.0/shares/{0}/root/content", encodedUrl);
            //
            string location = $@"{Directory.GetCurrentDirectory()}\mysql.zip";
            //
            WebClient webClient = new();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(AsyncCompleted);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            Thread DownloadThread = new(() =>
            {
                webClient.DownloadFileAsync(new Uri(resultUrl), location);
            });
            DownloadThread.Start ();
            
            bntDownloadMysql.Enabled = false;
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pBarDownloadMysql.Value = e.ProgressPercentage;
        }
        private void AsyncCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            bWorkerDownloadComplate.RunWorkerAsync();
        }
        private void BWorkerDownloadComplate_DoWork(object sender, DoWorkEventArgs e)
        {
            string file = $@"{Directory.GetCurrentDirectory()}\mysql.zip";
            string location = $@"{Directory.GetCurrentDirectory()}\";
            ZipFile.ExtractToDirectory(file, location, overwriteFiles: true);
            bntDownloadMysql.Text = "Extractiong MySQL...";
        }
        private void BWorkerDownloadComplate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string mysqlName = $@"{Settings._Data.MySQLExecutableName}.exe";
            bntDownloadMysql.Text = "Extract Complate MySQL...";
            Thread.Sleep(500);
            File.Delete($@"{Directory.GetCurrentDirectory()}\mysql.zip");
            bntDownloadMysql.Text = "Download MySQL Server";
            bntDownloadMysql.Enabled = true;
            pBarDownloadMysql.Visible = false;
            pBarDownloadMysql.Value = 0;
            foreach (string f in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), mysqlName, SearchOption.AllDirectories))
            {
                Settings._Data.MySQLExecutablePath = f;
            }
        }

    }
}
