using MetroFramework;
using MetroFramework.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
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
            //CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            TLTHome.BackColor = Color.Red;

            if (File.Exists("setup.exe")) { File.Delete("setup.exe"); }
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
                User.UI.Form.LoadData = true;
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
            //
            BTNNotification.NotificationCount = User.UI.Form.Notyfications;
            BTNDownload.NotificationCount = User.UI.Download.DownloadStatus;
            BTNDownload.NotificationCount = User.UI.Download.CurrentDownloads;
            BTNDownload.Visible = User.UI.Download.CurrentDownloads > 0;
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            ButtonsDesing();
            if (LoadControl == true)
            {
                TimerChangeControl.Start();
            }
            if (User.UI.Form.StartUpLoading == 2)
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
                if (Data.Settings.FirstRun == true)
                {
                    
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
            int port = int.Parse(Data.Settings.DBServerPort);
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
        }
        private void BTNStartLogin_Click(object sender, EventArgs e)
        {

        }
        private void BTNStartWorld_Click(object sender, EventArgs e)
        {
            
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
        private void TimerLoadingCheck_Tick(object sender, EventArgs e)
        {
            TimerLoadingCheck.Stop();
            if (CurrentControl == CurrentControl.Load)
            {
                if (MetroMessageBox.Show(this, "It seems like you are stuck on the loading screen. Do you want to fix the problem?", "Question.", Data.Settings.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Process.Start("TrionWorker.exe", "--FixLoading");
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}