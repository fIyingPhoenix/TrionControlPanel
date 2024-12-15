using MaterialSkin;
using MaterialSkin.Controls;
using MetroFramework;
using TrionControlPanelDesktop.Data;
using TrionLibrary.Network;
using TrionLibrary.Setting;
using TrionLibrary.Sys;
using static TrionLibrary.Models.Enums;

namespace TrionControlPanelDesktop
{
    public partial class MainForm : MaterialForm
    {
        private readonly MaterialSkinManager? materialSkinManager;

        List<int> Ports = [3306, 8085, 3724];
        List<int> OpenPorts = [];
        string Result { get; set; }

        static CurrentControl CurrentControl { get; set; }
        //
        //
        async void LoadData()
        {
            CurrentControl = CurrentControl.Load;

            //if (Data.Settings.RunServerWithWindows) { await RunAll(); }
            await Setting.Save();
        }
        //Closing running processes when trion die.
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
            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;
            // Set this to false to disable backcolor enforcing on non-materialSkin components
            // This HAS to be set before the AddFormToManage()
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            //Check if an update has ben Dowloaded and Delete it!
            if (File.Exists("setup.exe")) { File.Delete("setup.exe"); }

        }

        //Chekk if ports needed for server are used and yell
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
        //Loading...
        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            // MaterialSkinManager properties
            materialSkinManager!.AddFormToManage(this);
            materialSkinManager!.Theme = MaterialSkinManager.Themes.TRION;
            Invalidate(); // Marks the entire form for repaint
            Update();     // Forces the repaint immediately
            await Setting.Load();
            await Main.CheckForUpdate();
            User.UI.Form.StartUpLoading++;
            LoadData();
            await CheckPorts();
        }
        private async void TimerWacher_Tick(object sender, EventArgs e)
        {

        }
        private void StartWorldTSMItem_Click(object sender, EventArgs e)
        {
        }
        private void StartLogonTSMItem_Click(object sender, EventArgs e)
        {
        }
        private void StartDatabaseTSMItem_Click(object sender, EventArgs e)
        {
        }
        private async void BTNStartMySQL_Click(object sender, EventArgs e)
        {

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

                }
            }
        }
        private void TimerCrashDetected_Tick(object sender, EventArgs e)
        {
            if (Setting.List.ServerCrashDetection == true) { Task.Run(async () => await Main.CrashDetector(5)); }
        }
    }
}