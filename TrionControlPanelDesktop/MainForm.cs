using MetroFramework;
using MetroFramework.Forms;
using System.Diagnostics;
using System.Reflection;
using TrionControlPanelDesktop.Controls;
using TrionControlPanelDesktop.Controls.Notification;
using TrionControlPanelDesktop.Data;
using TrionControlPanelDesktop.Download;
using TrionLibrary.Network;
using TrionLibrary.Setting;
using TrionLibrary.Sys;
using static TrionLibrary.Models.Enums;

namespace TrionControlPanelDesktop
{
    public partial class MainForm : MetroForm
    {
        List<int> Ports = [3306, 8085, 3724];
        List<int> OpenPorts = new();
        string Result {  get; set; }
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
            CurrentControl = CurrentControl.Load;
            PNLControl.Controls.Clear();
            PNLControl.Controls.Add(loadingControl);

            //if (Data.Settings.RunServerWithWindows) { await RunAll(); }
            await Setting.Save();
        }
        static async Task ClosingToDo()
        {

            if (User.System.DatabaseProcessID.Count != 0)
            {
                foreach (var PID in User.System.DatabaseProcessID)
                {
                    var processToRemove = User.System.DatabaseProcessID.Single(r => r.Name == Setting.List.DBExeName);
                    await Watcher.ApplicationStop(processToRemove.ID);
                }
            }
            if (User.System.LogonProcessesID.Count != 0)
            {
                foreach (var PID in User.System.LogonProcessesID)
                {
                    Watcher.ApplicationKill(PID.ID);
                }
            }

            if (User.System.WorldProcessesID.Count != 0)
            {
                foreach (var PID in User.System.WorldProcessesID)
                {
                     Watcher.ApplicationKill(PID.ID);
                }
            }

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
            LblVersion.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version!.ToString()}";
            TLTHome.BackColor = Color.Red;
            if (File.Exists("setup.exe")) { File.Delete("setup.exe"); }
        }
        private async Task CheckPorts()
        {
            foreach (var port in Ports)
            {
                if (await Helper.IsPortOpen(port, "127.0.0.1"))
                {
                    OpenPorts.Add(port); 
                }
            }
            Result = String.Join(", ", OpenPorts);
            if (!String.IsNullOrEmpty(Result)) 
            {
                MetroMessageBox.Show(this, $"The port: {Result} is used! \n You need the ports to start the servers!", "Warning!", Setting.List.NotificationSound, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }
        private async void MainForm_LoadAsync(object sender, EventArgs e)
        { 
            await Setting.Load();
            await Main.CheckForUpdate();
            User.UI.Form.StartUpLoading++;
            LoadData();
            await CheckPorts();

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
           
            if (Main.ServerStatusWorld() && Main.ServerStartedWorld())
            { BTNStartWorld.Image = Properties.Resources.power_on_30; }
            else { BTNStartWorld.Image = Properties.Resources.power_off_30; }
            //
            if (Main.ServerStatusLogon() && Main.ServerStartedLogon())
            { BTNStartLogin.Image = Properties.Resources.power_on_30; }
            else { BTNStartLogin.Image = Properties.Resources.power_off_30; }
            //
            if (User.UI.Form.DBRunning && User.UI.Form.DBStarted) { BTNStartMySQL.Image = Properties.Resources.power_on_30; }
            else { BTNStartMySQL.Image = Properties.Resources.power_off_30; }

            BTNNotification.NotificationCount = User.UI.Form.Notyfications;
            BTNDownload.NotificationCount = DownloadControl.CurrentDownloadCount;
            BTNDownload.Visible = DownloadControl.CurrentDownloadCount > 0;
        }
        private async void TimerWacher_Tick(object sender, EventArgs e)
        {
            ButtonsDesing();
            if (User.UI.Form.StartUpLoading == 3)
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
            }
            if (await Helper.IsPortOpen(3306, "127.0.0.1") && User.System.DatabaseProcessID.Count != 0)
            {
                BTNStartWorld.Enabled = true;
                BTNStartLogin.Enabled = true;
            }
            else
            {
                BTNStartWorld.Enabled = false;
                BTNStartLogin.Enabled = false;
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
        private async void BTNStartMySQL_Click(object sender, EventArgs e)
        {
            if (!User.UI.Form.DBRunning && !User.UI.Form.DBStarted)
            {
                User.System.DatabaseStartTime = DateTime.Now;
                Setting.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                string arg = $"--defaults-file=\"{Directory.GetCurrentDirectory()}/my.ini\" --console";
                await Main.StartDatabase(arg);
                await Main.DatabaseRunIDCHeck(Setting.List.DBWorkingDir, Setting.List.DBExeName);
            }
            else
            {
                await Main.StopDatabase();
            }
        }
        private void BTNStartLogin_Click(object sender, EventArgs e)
        {
            if (!Main.ServerStatusLogon())
            { Task.Run(async () => await Main.StartLogon()); }
            else
            { Task.Run(async () => await Main.StopLogon()); }
        }
        private void BTNStartWorld_Click(object sender, EventArgs e)
        {
            if (!Main.ServerStatusWorld() && !Main.ServerStartedWorld())
            { Task.Run(async () => await Main.StartWorld()); }
            else
            { Task.Run(async () => await Main.StopWorld()); }
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
        private void TimerCrashDetected_Tick(object sender, EventArgs e)
        {
            if (Setting.List.ServerCrashDetection == true) { Task.Run(async () => await Main.CrashDetector(5)); }
           
        }
    }
}