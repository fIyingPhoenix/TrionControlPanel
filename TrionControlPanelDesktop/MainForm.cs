using MetroFramework.Forms;
using System.Reflection;
using TrionControlPanelDesktop.Controls;
using TrionControlPanelDesktop.Data;
using static TrionControlPanelDesktop.Models.EnumModels;

namespace TrionControlPanelDesktop
{
    public partial class MainForm : MetroForm
    {
        readonly HomeControl homeControl = new();
        void LoadData()
        {
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
    }
}