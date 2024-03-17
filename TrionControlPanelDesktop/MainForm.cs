using MetroFramework;
using MetroFramework.Forms;
using System.Reflection;
using System.Runtime.CompilerServices;
using TrionControlPanelDesktop.Controls;
using TrionControlPanelDesktop.Controls.Notification;
using TrionControlPanelDesktop.FormData;
using TrionLibrary;
using static TrionLibrary.EnumModels;

namespace TrionControlPanelDesktop
{
    public partial class MainForm : MetroForm
    {
        readonly HomeControl homeControl = new();
        readonly LoadingControl loadingControl = new();
        readonly SettingsControl settingsControl = new();
        readonly DownloadControl downloadControl = new();
        readonly NotificationsControl notificationsControl = new();
        public static CurrentControl CurrentControl { get; set; }
        private static bool LoadControl = false;
        void LoadData()
        {
            CurrentControl = CurrentControl.Load;
            PNLControl.Controls.Clear();
            PNLControl.Controls.Add(loadingControl);
            LblVersion.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
            Data.SaveSettings();
        }
        public MainForm()
        {
            //flickering fix
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            //fix the problem with thread calls
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void SettingsBTN_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Settings)
            {
                UIData.LoadData = true;
                CurrentControl = CurrentControl.Settings;
                TimerChangeControl.Start();
            }
        }
        private void HomeBTN_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Home)
            {
                CurrentControl = CurrentControl.Home;
                TimerChangeControl.Start();
            }
        }
        private void TerminaBTN_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Control)
            {
                CurrentControl = CurrentControl.Control;
                TimerChangeControl.Start();
            }
        }
        private void ButtonsDesing()
        {
            if (SystemWatcher.ApplicationRuning(Data.Settings.LogonExecutableName) == ServerStatus.NotRunning)
            {
                BTNStartLogin.Text = "            Start  Login  ";
                BTNStartLogin.Image = Properties.Resources.play_35;
                UIData.LogonisRunning = false;
            }
            if (SystemWatcher.ApplicationRuning(Data.Settings.LogonExecutableName) == ServerStatus.Running)
            {
                BTNStartLogin.Text = "            Stop  Login  ";
                BTNStartLogin.Image = Properties.Resources.stop_35;
                UIData.LogonisRunning = true;
            }
            if (SystemWatcher.ApplicationRuning(Data.Settings.WorldExecutableName) == ServerStatus.NotRunning)
            {
                BTNStartWorld.Text = "            Start  World ";
                BTNStartWorld.Image = Properties.Resources.play_35;
                UIData.WorldisRunning = false;
            }
            if (SystemWatcher.ApplicationRuning(Data.Settings.WorldExecutableName) == ServerStatus.Running)
            {
                BTNStartWorld.Text = "            Stop  World ";
                BTNStartWorld.Image = Properties.Resources.stop_35;
                UIData.WorldisRunning = true;
            }
            if (SystemWatcher.ApplicationRuning(Data.Settings.MySQLExecutableName) == ServerStatus.NotRunning)
            {
                BTNStartMySQL.Text = "            Start  MySQL  ";
                BTNStartMySQL.Image = Properties.Resources.play_35;
                UIData.MySQLisRunning = false;
            }
            if (SystemWatcher.ApplicationRuning(Data.Settings.MySQLExecutableName) == ServerStatus.Running)
            {
                BTNStartMySQL.Text = "            Stop  MySQL  ";
                BTNStartMySQL.Image = Properties.Resources.stop_35;
                UIData.MySQLisRunning = true;
            }
            if (SystemWatcher.ApplicationRuning(Data.Settings.MySQLExecutableName) == ServerStatus.Running || SystemWatcher.ApplicationRuning(Data.Settings.WorldExecutableName) == ServerStatus.Running || SystemWatcher.ApplicationRuning(Data.Settings.LogonExecutableName) == ServerStatus.Running)
            {
                BTNStartAll.Text = "            Stop  All  ";
                BTNStartAll.Image = Properties.Resources.stop_35;
                if (UIData.MySQLisRunning == false) { UIData.MySQLisRunning = true; }
                if (UIData.WorldisRunning == false) { UIData.WorldisRunning = true; }
                if (UIData.LogonisRunning == false) { UIData.LogonisRunning = true; }
            }
            if (SystemWatcher.ApplicationRuning(Data.Settings.MySQLExecutableName) == ServerStatus.NotRunning || SystemWatcher.ApplicationRuning(Data.Settings.WorldExecutableName) == ServerStatus.NotRunning || SystemWatcher.ApplicationRuning(Data.Settings.LogonExecutableName) == ServerStatus.NotRunning)
            {
                BTNStartAll.Text = "            Start  All ";
                BTNStartAll.Image = Properties.Resources.play_35;
                if (UIData.MySQLisRunning) { UIData.MySQLisRunning = false; }
                if (UIData.WorldisRunning) { UIData.WorldisRunning = false; }
                if (UIData.LogonisRunning) { UIData.LogonisRunning = false; }
            }
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            BTNNotification.NotificationCount = UIData.Notyfications;
            BTNDownload.NotificationCount = UIData.DownloadStatus;
            BTNDownload.NotificationCount = UIData.CurrentDownloads;
            BTNDownload.Visible = UIData.CurrentDownloads > 0;
            ButtonsDesing();
            if (LoadControl == true)
            {
                TimerChangeControl.Start();
            }
            if (UIData.StartUpLoading == 2)
            {
                BTNStartAll.Visible = true;
                BTNStartLogin.Visible = true;
                BTNStartWorld.Visible = true;
                BTNStartMySQL.Visible = true;
                BTNHome.Visible = true;
                BTNSettings.Visible = true;
                BTNTermina.Visible = true;
                PNLControl.Controls.Clear();
                PNLControl.Controls.Add(homeControl);
                CurrentControl = CurrentControl.Home;
                UIData.StartupUpdateCheck = true;
                if (Data.Settings.FirstRun == true)
                {
                    StartDirectoryScan(Directory.GetCurrentDirectory());
                }
            }
        }
        static void ManageProcess(bool isRunning, string executableLocation, string executableName)
        {
            if (!isRunning && !string.IsNullOrEmpty(executableLocation))
            {
                SystemWatcher.ApplicationStart(executableLocation, executableName, Data.Settings.ConsolHide, null);
            }
            else if (isRunning)
            {
                SystemWatcher.ApplicationKill(executableName);
            }
        }
        private void BTNStartAll_Click(object sender, EventArgs e)
        {
            ManageProcess(UIData.WorldisRunning, Data.Settings.WorldExecutableLocation, Data.Settings.WorldExecutableName);
            string configFile = Path.Combine(Directory.GetCurrentDirectory(), "my.ini");
            ManageProcess(UIData.MySQLisRunning, Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName);

            if (!UIData.MySQLisRunning && !string.IsNullOrEmpty(Data.Settings.MySQLExecutableLocation))
            {
                if (!File.Exists(configFile))
                {
                    Data.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                }
                SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName, Data.Settings.ConsolHide, $"--defaults-file=\"{configFile}\" --console");
            }

            ManageProcess(UIData.LogonisRunning, Data.Settings.LogonExecutableLocation, Data.Settings.LogonExecutableName);
        }
        private void BTNDownload_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Download)
            {
                CurrentControl = CurrentControl.Download;
                TimerChangeControl.Start();
            }
        }
        private void BTNNotification_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Notify)
            {
                CurrentControl = CurrentControl.Notify;
                TimerChangeControl.Start();
            }
        }
        private void BTNStartMySQL_Click(object sender, EventArgs e)
        {
            if (!UIData.MySQLisRunning && Data.Settings.MySQLExecutableLocation != string.Empty)
            {
                string configFile = "my.ini";
                if (!File.Exists(configFile))
                {
                    Data.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                }
                SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName, Data.Settings.ConsolHide, $"--defaults-file=\"{configFile}\" --console");
            }
            else if (UIData.MySQLisRunning)
            {
                SystemWatcher.ApplicationKill(Data.Settings.MySQLExecutableName);
            }
        }
        private void BTNStartLogin_Click(object sender, EventArgs e)
        {
            ManageProcess(UIData.LogonisRunning, Data.Settings.LogonExecutableLocation, Data.Settings.LogonExecutableName);
        }
        private void BTNStartWorld_Click(object sender, EventArgs e)
        {
            ManageProcess(UIData.WorldisRunning, Data.Settings.WorldExecutableLocation, Data.Settings.WorldExecutableName);
        }
        public void StartDirectoryScan(string path)
        {
            if (Data.GetExecutableLocation(path, Data.Settings.MySQLExecutableName) != string.Empty)
            {
                Data.Settings.MySQLExecutableLocation = Data.GetExecutableLocation(path, Data.Settings.MySQLExecutableName);
                if (Data.Settings.MySQLExecutableLocation != string.Empty)
                {
                    string BinFolder = Path.GetDirectoryName(Data.Settings.MySQLExecutableLocation)!;
                    Data.Settings.MySQLLocation = Path.GetFullPath(Path.Combine(BinFolder, @"..\"));
                }
            }
            else
            {
                if (MetroMessageBox.Show(this, "MySQL Directory not Found! Do you want To look for it?", "Info!", Data.Settings.NotificationSound, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var FolderBrowser = new FolderBrowserDialog())
                    {
                        DialogResult Result = FolderBrowser.ShowDialog();
                        if (Result == DialogResult.OK && !string.IsNullOrWhiteSpace(FolderBrowser.SelectedPath))
                        {
                            Data.Settings.MySQLExecutableLocation = Data.GetExecutableLocation(FolderBrowser.SelectedPath, Data.Settings.MySQLExecutableName);
                            if (Data.Settings.MySQLExecutableLocation != string.Empty)
                            {
                                string BinFolder = Path.GetDirectoryName(Data.Settings.MySQLExecutableLocation)!;
                                Data.Settings.MySQLLocation = Path.GetFullPath(Path.Combine(BinFolder, @"..\"));
                            }
                        }
                    }
                }
            }
            if (Data.GetExecutableLocation(path, Data.Settings.WorldExecutableName) != string.Empty)
            {
                Data.Settings.LogonExecutableLocation = Data.GetExecutableLocation(path, Data.Settings.LogonExecutableName);
                Data.Settings.WorldExecutableLocation = Data.GetExecutableLocation(path, Data.Settings.WorldExecutableName);
                Data.Settings.CoreLocation = Path.GetDirectoryName(Data.Settings.WorldExecutableLocation);
            }
            else
            {

                if (MetroMessageBox.Show(this, "Core Directory not Found! Do you want To look for it?", "Info!", Data.Settings.NotificationSound, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var FolderBrowser = new FolderBrowserDialog())
                    {
                        DialogResult Result = FolderBrowser.ShowDialog();
                        if (Result == DialogResult.OK && !string.IsNullOrWhiteSpace(FolderBrowser.SelectedPath))
                        {
                            Data.Settings.LogonExecutableLocation = Data.GetExecutableLocation(FolderBrowser.SelectedPath, Data.Settings.LogonExecutableName);
                            Data.Settings.WorldExecutableLocation = Data.GetExecutableLocation(FolderBrowser.SelectedPath, Data.Settings.WorldExecutableName);
                            Data.Settings.CoreLocation = Path.GetDirectoryName(Data.Settings.WorldExecutableLocation);
                        }
                    }
                }
            }
            Data.Settings.FirstRun = false;
            Data.SaveSettings();
        }
        private void TimerChangeControl_Tick(object sender, EventArgs e)
        {
            TimerChangeControl.Stop();
            switch (CurrentControl)
            {
                case CurrentControl.Home:
                    PNLControl.Controls.Clear();
                    PNLControl.Controls.Add(homeControl);
                    break;
                case CurrentControl.Settings:
                    PNLControl.Controls.Clear();
                    PNLControl.Controls.Add(settingsControl);
                    break;
                case CurrentControl.Download:
                    PNLControl.Controls.Clear();
                    PNLControl.Controls.Add(downloadControl);
                    break;
                case CurrentControl.Notify:
                    PNLControl.Controls.Clear();
                    PNLControl.Controls.Add(notificationsControl);
                    break;
                default: break;
            }
        }
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await Data.SaveSettings();
        }
    }
}