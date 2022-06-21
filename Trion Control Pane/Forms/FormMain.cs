using TrionControlPanel.TabsComponents;
using TrionControlPanel.Properties;


namespace TrionControlPanel
{
    public partial class FormMain : Form
    {
        readonly HomeControl homeControl = new();
        readonly SettingControl settingControl = new();
        readonly LoadingControl loadingControl = new();
        readonly TerminalControl terminalControl = new();
        public FormMain()
        {
            //fix the problem with thread calls
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //Load Home Controls
            pnlTabs.Controls.Add(loadingControl);
        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            //Load Home Controls by button
            pnlTabs.Controls.Clear();
            pnlTabs.Controls.Add(homeControl);
        }
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            pnlTabs.Controls.Clear();
            pnlTabs.Controls.Add(settingControl);
        }
        private void TimerCheck_Tick(object sender, EventArgs e)
        {
            if(homeControl.totalRamUsageProgressBar.Value > 0)
            {
                timerCheck.Stop();
                pnlTabs.Controls.Clear();
                pnlTabs.Controls.Add(homeControl);
                btnHome.Enabled = true;
                btnTerminal.Enabled = true; 
                btnSettings.Enabled = true;
                
            }else
            {
                btnHome.Enabled = false;
                btnTerminal.Enabled = false;
                btnSettings.Enabled = false;
            }
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists($@"{Directory.GetCurrentDirectory()}\MySQL"))
            {
                Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\MySQL");
            }   
        }
        private void BtnTerminal_Click(object sender, EventArgs e)
        {
            //Load Home Controls by button
            pnlTabs.Controls.Clear();
            pnlTabs.Controls.Add(terminalControl);
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.Default.TogleStayInTray == true)
            {
                this.Hide();
                e.Cancel = true;
            }
            else
            {
                Environment.Exit(0);
            }
        }
        private void MainNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
       
    }
}
