using MetroFramework;
using MetroFramework.Forms;
using System.Diagnostics;
using System.Reflection;
using TrionControlPanelDesktop.Controls;
using TrionControlPanelDesktop.Controls.Notification;
using TrionControlPanelDesktop.Data;
using TrionLibrary.Setting;
using static TrionLibrary.Models.Enums;


namespace TrionControlPanelDesktop
{
    public partial class MainForm : MetroForm
    {
        readonly static DatabaseControl databaseControl = new();
        readonly static HomeControl homeControl = new();
        readonly static LoadingControl loadingControl = new();
        readonly static SettingsControl settingsControl = new();
        readonly static DownloadControl downloadControl = new();
        readonly static NotificationsControl notificationsControl = new();
        //
        static CurrentControl CurrentControl { get; set; }
        //
        //
        async void LoadData()
        {
            CheckForIllegalCrossThreadCalls = false;
            CurrentControl = CurrentControl.Load;
            PNLControl.Controls.Clear();
            PNLControl.Controls.Add(loadingControl);
            LblVersion.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
            //if (Data.Settings.RunServerWithWindows) { await RunAll(); }
            await Setting.Save();
        }
        static async Task ClosingToDo()
        {
            await Setting.Save();
        }
        public MainForm()
        {
            //flickering fix
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            InitializeComponent();
            TLTHome.BackColor = Color.Red;
            if (File.Exists("setup.exe")) { File.Delete("setup.exe"); }
        }
        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            await Setting.Load();
            LoadData();
        }
        private void SettingsBTN_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Settings)
            {
                CurrentControl = CurrentControl.Settings;
                ChangeControl();
            }
        }
        private void HomeBTN_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Home)
            {
                CurrentControl = CurrentControl.Home;
                ChangeControl();
            }
        }
        private void TerminaBTN_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Database)
            {
                CurrentControl = CurrentControl.Database;
                ChangeControl();
            }
        }
        private void ButtonsDesing()
        {
            BTNNotification.NotificationCount = User.UI.Form.Notyfications;
            BTNDownload.NotificationCount = DownloadControl.CurrentDownloadCount;
            BTNDownload.Visible = DownloadControl.CurrentDownloadCount > 0;
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            ButtonsDesing();
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
                if (Setting.List.FirstRun == true)
                {

                }

            }
        }
        private void BTNDownload_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Download)
            {
                CurrentControl = CurrentControl.Download;
                ChangeControl();
            }
        }
        private void BTNNotification_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Notify)
            {
                CurrentControl = CurrentControl.Notify;
                ChangeControl();
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
            User.System.DatabaseStartTime = DateTime.Now;
            Setting.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
            string arg = $@"--defaults-file={Directory.GetCurrentDirectory()}/my.ini --console";
            Main.StartDatabase(arg);
        }
        private void BTNStartLogin_Click(object sender, EventArgs e)
        {
            Main.StartLogon();
        }
        private void BTNStartWorld_Click(object sender, EventArgs e)
        {
            Main.StartWorld();
        }
        public static void LoadDownload()
        {
            if (CurrentControl != CurrentControl.Download)
            {
                CurrentControl = CurrentControl.Download;
                ChangeControl();
            }
        }
        public static void ChangeControl()
        {
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
                case CurrentControl.Database:
                    PNLControl.Controls.Clear();
                    PNLControl.Controls.Add(databaseControl);
                    break;
                default: break;
            }
        }
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Setting.List.StayInTray)
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
                if (MetroMessageBox.Show(this, "It seems like you are stuck on the loading screen. Do you want to fix the problem?", "Question.", Setting.List.NotificationSound, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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