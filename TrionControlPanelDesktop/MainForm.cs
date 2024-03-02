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
        CurrentControl CurrentControl { get; set; }
        void LoadData()
        {
            CurrentControl = CurrentControl.Load;
            PNLControl.Controls.Clear();
            PNLControl.Controls.Add(loadingControl);
            LblVersion.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
            if (Data.Settings.MySQLLocation != string.Empty)
            {
                Data.Settings.MySQLExecutableLocation = Data.GetExecutableLocation(Data.Settings.MySQLLocation, Data.Settings.MySQLExecutableName);
            }
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
                PNLControl.Controls.Clear();
                PNLControl.Controls.Add(settingsControl);
                CurrentControl = CurrentControl.Settings;
            }
        }

        private void HomeBTN_Click(object sender, EventArgs e)
        {
            if (CurrentControl != CurrentControl.Home)
            {
                PNLControl.Controls.Clear();
                PNLControl.Controls.Add(homeControl);
                CurrentControl = CurrentControl.Home;
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
            }
            if (Data.Message != string.Empty)
            {
                TimerWacher.Stop();
                MessageBox.Show(Data.Message);
                Data.Message = string.Empty;
                TimerWacher.Start();
            }
        }
        private void TerminaBTN_Click(object sender, EventArgs e)
        {
        }
        private void BTNStartMySQL_Click(object sender, EventArgs e)
        {
            if (UIData.MySQLisRunning == false)
            {
                if (Data.Settings.MySQLExecutableLocation != string.Empty)
                {
                    string Arguments = $@"{Directory.GetCurrentDirectory()}\my.ini";
                    SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.ConsolHide, $"--defaults-file=\"{Arguments}\"");
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
    }
}