
using CypherCore_Server_Laucher.Classes;
using CypherCore_Server_Laucher.TabsComponents;
using CypherCoreServerLaucher.TabsComponents;
using System.Reflection;

namespace CypherCore_Server_Laucher
{
    public partial class FormMain : Form
    {
        readonly ConnectionClass _connectionClass = new();
        readonly HomeControl homeControl = new();
        readonly SettingControl settingControl = new();
        readonly LoadingControl loadingControl = new();

        public FormMain()
        {
            //fix the problem with thread calls
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            //Load Home Controls

            pnlTabs.Controls.Add(loadingControl);
        }
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            //test mysql connection
            _connectionClass.MySqlConnect();
            if(_connectionClass.MySQLConnected() == true)
            {
                btnConnect.BackColor = Color.Green;
            }
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
                btnSettings.Enabled = true; 
                btnTerminal.Enabled = true;
                //btnConnect.Enabled = true;  
            }else
            {
                btnHome.Enabled = false;
                btnSettings.Enabled = false;
                btnTerminal.Enabled = false;
            }
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
        }
    }
}
