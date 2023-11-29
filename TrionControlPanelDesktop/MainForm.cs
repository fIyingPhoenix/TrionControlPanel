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
            ContiWEB.Url = UIData.ContributorsURL();
            ContiWEB.LoadWebsite = LoadWebsite.True;
            ForkWEB.Url = UIData.ForksURL();
            ForkWEB.LoadWebsite = LoadWebsite.True;
            StarsWEB.Url = UIData.StarsURL();
            StarsWEB.LoadWebsite = LoadWebsite.True;
            IssuesWEB.Url = UIData.IssuesURL();
            IssuesWEB.LoadWebsite = LoadWebsite.True;
            QualityWEB.Url = UIData.CodeQualityURL();
            QualityWEB.LoadWebsite = LoadWebsite.True;
        }
        public MainForm()
        {
            //flickering fix
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            //fix the problem with thread calls
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            LoadData();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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

        private void BTNStartLOgin_Click(object sender, EventArgs e)
        {
            if (Data.Server.LoginServerStatus == ServerStatus.NotRunning)
            {
                SystemWatcher.ApplicationStart(Data.Settings.BnetExecutableLocation, Data.Settings.ConsolHide);
            }
        }
        private void ButtonsDesing()
        {
            if (Data.Server.LoginServerStatus == ServerStatus.NotRunning)
            {
                BTNStartLOgin.Text = "Start Login  ";
                BTNStartLOgin.Image = Properties.Resources.play_35;

            }
            else if (Data.Server.LoginServerStatus == ServerStatus.Running)
            {
                BTNStartLOgin.Text = "Stop Login  ";
                BTNStartLOgin.Image = Properties.Resources.stopp_35;
            }
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            if (UIData.StartUpLoading == 2)
            {
                HomeBTN.Enabled = true;
                SettingsBTN.Enabled = true;
                TerminaBTN.Enabled = true;
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

        }

        private void BTNStartLOgin_Click_1(object sender, EventArgs e)
        {
        }

        private void BTNStartWorld_Click(object sender, EventArgs e)
        {
        }

        private void PNLControl_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}