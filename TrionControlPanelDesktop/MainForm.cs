using MetroFramework;
using MetroFramework.Forms;
using System.Reflection;
using TrionControlPanelDesktop.Controls;
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
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
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
        public static void LoadDownloadControl()
        {
            if (CurrentControl != CurrentControl.Download)
            {
                CurrentControl = CurrentControl.Download;
                LoadControl = true;
            }
            else
            {
                CurrentControl = CurrentControl.Home;
                LoadControl = true;
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
                BTNStartLogin.Image = Properties.Resources.stopp_35;
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
                BTNStartWorld.Image = Properties.Resources.stopp_35;
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
                BTNStartMySQL.Image = Properties.Resources.stopp_35;
                UIData.MySQLisRunning = true;
            }
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
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
                HomeBTN.Visible = true;
                SettingsBTN.Visible = true;
                TerminaBTN.Visible = true;
                PNLControl.Controls.Clear();
                PNLControl.Controls.Add(homeControl);
                CurrentControl = CurrentControl.Home;
                UIData.StartupUpdateCheck = true;

                if (Data.Settings.FirstRun == true)
                {
                    StartDirectoryScan(Directory.GetCurrentDirectory());
                }
            }
            if (Data.Message != string.Empty)
            {
                TimerWacher.Stop();
                MetroMessageBox.Show(this, Data.Message,"Info",Data.Settings.NotificationSound, MessageBoxButtons.OK,MessageBoxIcon.Information);
                Data.Message = string.Empty;
                TimerWacher.Start();
            }
        }
        private void BTNStartMySQL_Click(object sender, EventArgs e)
        {
            if (UIData.MySQLisRunning == false)
            {
                if (Data.Settings.MySQLExecutableLocation != string.Empty)
                {
                    string ConfigFile = $@"{Directory.GetCurrentDirectory()}\my.ini";
                    if (!File.Exists(ConfigFile))
                    {
                        Data.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                    }
                    SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.ConsolHide, $"--defaults-file=\"{ConfigFile}\" --console");
                }
            }
            else
            {
                SystemWatcher.ApplicationKill(Data.Settings.MySQLExecutableName);
            }
        }
        private void BTNStartLogin_Click(object sender, EventArgs e)
        {
            if (UIData.LogonisRunning == false)
            {
                if (Data.Settings.LogonExecutableLocation != string.Empty)
                {
                    SystemWatcher.ApplicationStart(Data.Settings.LogonExecutableLocation, Data.Settings.ConsolHide, null);
                }
            }
            else
            {
                SystemWatcher.ApplicationKill(Data.Settings.LogonExecutableName);
            }
        }
        private void BTNStartWorld_Click(object sender, EventArgs e)
        {
            if (UIData.WorldisRunning == false)
            {
                if (Data.Settings.WorldExecutableLocation != string.Empty)
                {
                    SystemWatcher.ApplicationStart(Data.Settings.WorldExecutableLocation, Data.Settings.ConsolHide, null);
                }
            }
            else
            {
                SystemWatcher.ApplicationKill(Data.Settings.WorldExecutableName);
            }
        }
        public static void StartDirectoryScan(string path)
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
                if (MessageBox.Show("MySQL Directory not Found! Do you want To look for it?", "Info!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                if (MessageBox.Show("Core Directory not Found! Do you want To look for it?", "Info!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            if (CurrentControl == CurrentControl.Control)
            {
                TimerChangeControl.Stop();

            }
            if (CurrentControl == CurrentControl.Home)
            {
                TimerChangeControl.Stop();
                PNLControl.Controls.Clear();
                PNLControl.Controls.Add(homeControl);
            }
            if (CurrentControl == CurrentControl.Settings)
            {
                TimerChangeControl.Stop();
                PNLControl.Controls.Clear();
                PNLControl.Controls.Add(settingsControl);
            }
            if (CurrentControl == CurrentControl.Download)
            {
                TimerChangeControl.Stop();
                PNLControl.Controls.Clear();
                PNLControl.Controls.Add(downloadControl);
            }
        }
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await Data.SaveSettings();
        }

        private void BTNStartAll_Click(object sender, EventArgs e)
        {

        }
    }
}