using TrionControlPanel.TabsComponents;
using TrionControlPanel.Properties;
using TrionControlPanel.Classes;
using TrionControlPanel.Forms;

namespace TrionControlPanel
{
    public partial class FormMain : Form
    {
        HomeControl homeControl = new();
        SettingControl settingControl = new();
        LoadingControl loadingControl = new();
        TerminalControl terminalControl = new();
        StatusClass _statusClass = new();
        int CrashCountWorld = 0;
        int CrashCountBnet = 0;
        int CrashCountMysql = 0;
        public FormMain()
        {
            //fix the problem with thread calls
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //Load Home Controls
            pnlTabs.Controls.Add(loadingControl);
            if (!Directory.Exists($@"{Directory.GetCurrentDirectory()}\mysql"))
            {
                Settings.Default.MySQLocation = $@"{Directory.GetCurrentDirectory()}\mysql";
                Settings.Default.Save();
            }        
        }
        public static void Alert(string message, NotificationType eType)
        {
            //make the laert work.
            FormAlert frm = new(); //dont change this. its fix the Cannot access a disposed object and scall the notification up.
            frm.ShowAlert(message, eType);
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

            if (Settings.Default.ServerCrashCheck == true & homeControl._isRuningBnet == true & homeControl._isRuningWorld == true)
            {
                timerCrashCheck.Start();
            }
            else if (Settings.Default.ServerCrashCheck == false)
            {
                timerCrashCheck.Stop();
            }
        }
        public static void DelayAction(int millisecond, Action action)
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += delegate

            {
                action.Invoke();
                timer.Stop();
            };

            timer.Interval = millisecond;
            timer.Start();
        }
        private void StartCoreScript(int milliseconds)
        {
            if (_statusClass.MySQLstatus() == true)
            {
                homeControl._isRuningMysql = true;
            }
            else
            {
                _statusClass.StartMysql();
                homeControl._isRuningMysql = true;
            }
            //
            
            var timer1 = new System.Windows.Forms.Timer();
            //
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                homeControl._isRuningBnet = true;
                homeControl._isRuningWorld = true;
                _statusClass.StartBnet();
                _statusClass.StartWorld();
            };
        }
        
        private void StartCoreWithWindows()
        {
            if (Settings.Default.StartCoreWithWindows == true)
            {
                StartCoreScript(7000);
            }
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            StartCoreWithWindows();
            if (!Directory.Exists($@"{Directory.GetCurrentDirectory()}\mysql"))
            {
                Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\mysql");
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
            else if (Settings.Default.TogleStayInTray == false)
            {
                mainNotify.Dispose();
                Environment.Exit(0);
            }
        }
        private void MainNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
        private void timerCrashCheck_Tick(object sender, EventArgs e)
        {
            //crash save approval trying!
            if (_statusClass.WorldStatus()== false & homeControl._isRuningWorld == true & CrashCountWorld < 5)
            {
                CrashCountWorld = +1;
                _statusClass.StartWorld();
            }
            else if (_statusClass.WorldStatus() == false & homeControl._isRuningWorld == true & CrashCountWorld > 5 )
            {
                homeControl._isRuningWorld = false;
                CrashCountWorld = 0;
                Alert("World server could not be started again! Fatal Error!", NotificationType.Info);
            }
            if (_statusClass.BnetStatus() == false & homeControl._isRuningBnet == true & CrashCountBnet < 5)
            {
                CrashCountBnet = +1;
                _statusClass.StartBnet();
            }
            else if (_statusClass.BnetStatus() == false & homeControl._isRuningBnet == true & CrashCountBnet > 5)
            {
                homeControl._isRuningBnet = false;
                CrashCountBnet = 0;
                Alert("Bnet/Auth server could not be started again! Fatal Error!", NotificationType.Info);
            }
            if (_statusClass.MySQLstatus() ==false & homeControl._isRuningMysql == true & CrashCountMysql < 5)
            {
                CrashCountMysql = +1;
                _statusClass.StartBnet();
            }
            else if (_statusClass.MySQLstatus() == false & homeControl._isRuningMysql == true & CrashCountMysql > 5)
            {
                homeControl._isRuningMysql = false;
                CrashCountMysql= 0;
                Alert("MySQL server could not be started again! Fatal Error!", NotificationType.Info);
            }
        }
        private void showTrionItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void startTrionItem_Click(object sender, EventArgs e)
        {
            StartCoreScript(5000);
        }

        private void stopTrionItem_Click(object sender, EventArgs e)
        {
            homeControl._isRuningBnet = false;
            homeControl._isRuningWorld = false;
            homeControl._isRuningMysql = false;
            _statusClass.KillWorld();
            _statusClass.KillBnet();
            _statusClass.KillMysql();
        }

        private void exitTrionItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
