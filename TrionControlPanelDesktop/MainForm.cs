using MetroFramework;
using MetroFramework.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Principal;
using TrionControlPanelDesktop.Controls;
using TrionControlPanelDesktop.Controls.Notification;
using TrionControlPanelDesktop.FormData;
using TrionLibrary;
using static TrionLibrary.EnumModels;

namespace TrionControlPanelDesktop
{
    public partial class MainForm : MetroForm
    {
        readonly DatabaseControl databaseControl = new();
        readonly HomeControl homeControl = new();
        readonly LoadingControl loadingControl = new();
        readonly SettingsControl settingsControl = new();
        readonly DownloadControl downloadControl = new();
        readonly NotificationsControl notificationsControl = new();
        readonly ConsoleControl consoleControl = new();
        //
        CurrentControl CurrentControl { get; set; }
        //
        //
        private static bool LoadControl = false;
        public static bool LoadDownload { get; set; }
        async void LoadData()
        {
            CurrentControl = CurrentControl.Load;
            PNLControl.Controls.Clear();
            PNLControl.Controls.Add(loadingControl);
            LblVersion.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
            if (Data.Settings.RunServerWithWindows) { await RunAll(); }
            await Data.SaveSettings();
        }
        static async Task ClosingToDo()
        {
            SystemWatcher.ApplicationKill(Data.Settings.MySQLExecutableName);
            SystemWatcher.ApplicationKill(Data.Settings.LogonExecutableName);
            SystemWatcher.ApplicationKill(Data.Settings.WorldExecutableName);
            await Data.SaveSettings();
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
        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            await Data.LoadSettings();
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
            if (CurrentControl != CurrentControl.Database)
            {
                CurrentControl = CurrentControl.Database;
                TimerChangeControl.Start();
            }
        }
        private void ButtonsDesing()
        {
            if (UIData.MySQLisRunning)
            {
                BTNStartMySQL.Text = "            Stop  Database  ";
                BTNStartMySQL.Image = Properties.Resources.stop_35;
                StartDatabaseTSMItem.Text = "Stop Database";
            }
            else if (UIData.MySQLisRunning == false)
            {
                BTNStartMySQL.Text = "            Start  Database  ";
                BTNStartMySQL.Image = Properties.Resources.play_35;
                StartDatabaseTSMItem.Text = "Start Database";
            }
            if (UIData.WorldisRunning)
            {
                BTNStartWorld.Text = "            Stop  World ";
                BTNStartWorld.Image = Properties.Resources.stop_35;
                StartWorldTSMItem.Text = "Stop World";
            }
            else if (UIData.WorldisRunning == false)
            {
                BTNStartWorld.Text = "            Start  World ";
                BTNStartWorld.Image = Properties.Resources.play_35;
                StartWorldTSMItem.Text = "Start World";
            }
            if (UIData.LogonisRunning)
            {
                BTNStartLogin.Text = "            Stop  Logon  ";
                BTNStartLogin.Image = Properties.Resources.stop_35;
                StartLogonTSMItem.Text = "Stop Logon";
            }
            else if (UIData.LogonisRunning == false)
            {
                BTNStartLogin.Text = "            Start  Logon  ";
                BTNStartLogin.Image = Properties.Resources.play_35;
                StartLogonTSMItem.Text = "Start Logon";
            }
            //
            BTNNotification.NotificationCount = UIData.Notyfications;
            BTNDownload.NotificationCount = UIData.DownloadStatus;
            BTNDownload.NotificationCount = UIData.CurrentDownloads;
            BTNDownload.Visible = UIData.CurrentDownloads > 0;
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
                BTNStartLogin.Visible = true;
                BTNStartWorld.Visible = true;
                BTNStartMySQL.Visible = true;
                BTNNotification.Visible = true;
                BTNHome.Visible = true;
                BTNSettings.Visible = true;
                BTNdatabase.Visible = true;
                PNLControl.Controls.Clear();
                PNLControl.Controls.Add(homeControl);
                CurrentControl = CurrentControl.Home;
                UIData.StartupUpdateCheck = true;
                if (Data.Settings.FirstRun == true)
                {
                    StartDirectoryScan(Directory.GetCurrentDirectory());
                }

            }
            if (LoadDownload == true)
            {
                LoadDownload = false;
                if (CurrentControl == CurrentControl.Download)
                {
                    CurrentControl = CurrentControl.Home;
                    TimerChangeControl.Start();
                }
                else
                {
                    CurrentControl = CurrentControl.Download;
                    TimerChangeControl.Start();
                }
            }
        }
        static async Task<bool> IsPortOpen()
        {
            string host = "localhost";
            int port = int.Parse(Data.Settings.MySQLServerPort);
            try
            {
                using TcpClient tcpClient = new();
                await tcpClient.ConnectAsync(host, port);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static async Task RunAll()
        {
            if (UIData.MySQLisRunning == false && Data.Settings.MySQLExecutableLocation != string.Empty)
            {
                string configFile = Path.Combine(Directory.GetCurrentDirectory(), "my.ini");
                if (!File.Exists(configFile))
                {
                    Data.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                }
                SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName, Data.Settings.ConsolHide, $"--console");
                UIData.MySQLisStarted = true;
            }
            while (!await IsPortOpen())
            {
                await Task.Delay(1000); // Wait for 1 second before retrying
            }
            if (UIData.LogonisRunning == false && Data.Settings.LogonExecutableLocation != string.Empty)
            {
                SystemWatcher.ApplicationStart(Data.Settings.LogonExecutableLocation, Data.Settings.LogonExecutableName, Data.Settings.ConsolHide, null);
                UIData.LogonisStarted = true;
            }
            if (UIData.WorldisRunning == false && Data.Settings.WorldExecutableLocation != string.Empty)
            {
                SystemWatcher.ApplicationStart(Data.Settings.WorldExecutableLocation, Data.Settings.WorldExecutableName, Data.Settings.ConsolHide, null);
                UIData.WorldisStarted = true;
            }
        }
        private void BTNConsole_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Console)
            {
                CurrentControl = CurrentControl.Console;
                TimerChangeControl.Start();
            }
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
        private void StartWorldTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartWorld_Click(sender, e);
        }
        private void StartLogonTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartLogin_Click(sender, e);
        }
        private void StartDatabaseTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartMySQL_Click(sender, e);
        }
        private void BTNStartMySQL_Click(object sender, EventArgs e)
        {
            if (UIData.MySQLisRunning == false && Data.Settings.MySQLExecutableLocation != string.Empty)
            {
                string configFile = Path.Combine(Directory.GetCurrentDirectory(), "my.ini");
                if (!File.Exists(configFile))
                {
                    Data.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                }
                SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName, Data.Settings.ConsolHide, $"--console");
                UIData.MySQLisStarted = true;
            }
            if (UIData.MySQLisRunning == true)
            {
                SystemWatcher.ApplicationKill(Data.Settings.MySQLExecutableName);
                UIData.MySQLisStarted = false;
            }
        }
        private void BTNStartLogin_Click(object sender, EventArgs e)
        {
            if (UIData.LogonisRunning == false && Data.Settings.LogonExecutableLocation != string.Empty)
            {
                SystemWatcher.ApplicationStart(Data.Settings.LogonExecutableLocation, Data.Settings.LogonExecutableName, Data.Settings.ConsolHide, null);
                UIData.LogonisStarted = true;
            }
            else if (UIData.LogonisRunning == true)
            {
                SystemWatcher.ApplicationKill(Data.Settings.LogonExecutableName);
                UIData.LogonisStarted = false;
            }
        }
        private void BTNStartWorld_Click(object sender, EventArgs e)
        {
            if (UIData.WorldisRunning == false && Data.Settings.WorldExecutableLocation != string.Empty)
            {
                //SystemWatcher.StartWorldTest(Data.Settings.WorldExecutableName, Data.Settings.ConsolHide);
                SystemWatcher.ApplicationStart(Data.Settings.WorldExecutableLocation, Data.Settings.WorldExecutableName, Data.Settings.ConsolHide, null);
                UIData.WorldisStarted = true;
            }
            else if (UIData.WorldisRunning == true)
            {
                SystemWatcher.ApplicationKill(Data.Settings.WorldExecutableName);
                //SystemWatcher.pWorldServer.StandardInput.Close(); ;
                UIData.WorldisStarted = false;
            }
        }
        private void StartDirectoryScan(string path)
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
                if (MetroMessageBox.Show(this, "MySQL Directory not Found! Do you want To look for it?", "Info.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    using var FolderBrowser = new FolderBrowserDialog();
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
            if (Data.GetExecutableLocation(path, Data.Settings.WorldExecutableName) != string.Empty)
            {
                Data.Settings.LogonExecutableLocation = Data.GetExecutableLocation(path, Data.Settings.LogonExecutableName);
                Data.Settings.WorldExecutableLocation = Data.GetExecutableLocation(path, Data.Settings.WorldExecutableName);
                Data.Settings.CoreLocation = Path.GetDirectoryName(Data.Settings.WorldExecutableLocation);
            }
            else
            {

                if (MetroMessageBox.Show(this, "Core Directory not Found! Do you want To look for it?", "Info.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    using var FolderBrowser = new FolderBrowserDialog();
                    DialogResult Result = FolderBrowser.ShowDialog();
                    if (Result == DialogResult.OK && !string.IsNullOrWhiteSpace(FolderBrowser.SelectedPath))
                    {
                        Data.Settings.LogonExecutableLocation = Data.GetExecutableLocation(FolderBrowser.SelectedPath, Data.Settings.LogonExecutableName);
                        Data.Settings.WorldExecutableLocation = Data.GetExecutableLocation(FolderBrowser.SelectedPath, Data.Settings.WorldExecutableName);
                        Data.Settings.CoreLocation = Path.GetDirectoryName(Data.Settings.WorldExecutableLocation);
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
                case CurrentControl.Console:
                    PNLControl.Controls.Clear();
                    PNLControl.Controls.Add(consoleControl);
                    break;
                case CurrentControl.Database:
                    PNLControl.Controls.Clear();
                    PNLControl.Controls.Add(databaseControl);
                    break;
                default: break;
            }
        }
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Data.Settings.StayInTray)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    NIcon.Visible = true;
                    e.Cancel = true;
                    Hide();
                }
            }
            else
            {
                await ClosingToDo();
            }
        }
        private async void ExitTSMItem_ClickAsync(object sender, EventArgs e)
        {
            await ClosingToDo();
            NIcon.Dispose();
            Application.Exit();
        }
        private void OpenTSMItem_Click(object sender, EventArgs e)
        {
            NIcon.Visible = false;
            Show();
        }
        static bool IsUserAdministrator()
        {
            // Check if the current user is an administrator
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        static void RunPowerShellCommand(string command)
        {
            // Launch PowerShell process with the command
            ProcessStartInfo psi = new()
            {
                FileName = "powershell.exe",
                Arguments = "-NoProfile -ExecutionPolicy Bypass -Command " + command,
                Verb = "runas" // Run PowerShell as administrator
            };

            Process.Start(psi);
        }
        private void TimerLoadingCheck_Tick(object sender, EventArgs e)
        {
            TimerLoadingCheck.Stop();
            if (CurrentControl == CurrentControl.Load)
            {
                if (MetroMessageBox.Show(this, "It seems like you are stuck on the loading screen. Do you want to fix the problem?", "Question.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (IsUserAdministrator() == true)
                    {
                        RunPowerShellCommand("lodctr /R");
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Your account is not an administrator!", "Error!.", Data.Settings.NotificationSound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}