using MetroFramework.Forms;
using System.Reflection;
using TrionControlPanelDesktop.Controls;
using TrionControlPanelDesktop.Data;
using TrionLibrary;
using static TrionLibrary.EnumModels;

namespace TrionControlPanelDesktop
{
    public partial class MainForm : MetroForm
    {
        readonly HomeControl homeControl = new();
        readonly SettingsControl settingsControl = new();
        CurrentControl CurrentControl { get; set; }
        void LoadData()
        {
            CurrentControl = CurrentControl.Home;
            PNLControl.Controls.Clear();
            PNLControl.Controls.Add(homeControl);
            LblVersion.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
            ContiWEB.Url = FormData.ContributorsURL();
            ContiWEB.LoadWebsite = LoadWebsite.True;
            ForkWEB.Url = FormData.ForksURL();
            ForkWEB.LoadWebsite = LoadWebsite.True;
            StarsWEB.Url = FormData.StarsURL();
            StarsWEB.LoadWebsite = LoadWebsite.True;
            IssuesWEB.Url = FormData.IssuesURL();
            IssuesWEB.LoadWebsite = LoadWebsite.True;
            QualityWEB.Url = FormData.CodeQualityURL();
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
            if(CurrentControl != CurrentControl.Settings)
            {
                PNLControl.Controls.Clear();
                PNLControl.Controls.Add(settingsControl);
                CurrentControl = CurrentControl.Settings;
            }
        }

        private void HomeBTN_Click(object sender, EventArgs e)
        {
            if(CurrentControl != CurrentControl.Home) {
                PNLControl.Controls.Clear();
                PNLControl.Controls.Add(homeControl);
                CurrentControl = CurrentControl.Home;
            }
        }

        private void BTNStartLOgin_Click(object sender, EventArgs e)
        {
            //DialogResult dr = MetroMessageBox.Show(this, "\n\nContinue Logging Out?", "EMPLOYEE MODULE | LOG OUT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            if (SystemWatcher.Message != string.Empty)
            {
                TimerWacher.Stop();
                MessageBox.Show(SystemWatcher.Message);
                SystemWatcher.Message = string.Empty;
                TimerWacher.Start();
            }
        }
    }
}