using TrionControlPanel.TabsComponents;
using TrionControlPanel.Classes;
using TrionControlPanel.Alerts;
using MetroFramework.Forms;
using TrionControlPanel.Settings;

namespace TrionControlPanel
{
    public partial class FormMain : MetroForm
    {
        readonly Settings.Settings Settings = new();
        readonly HomeControl  homeControl = new();
        readonly SettingControl settingControl = new();
        readonly LoadingControl loadingControl = new();
        readonly TerminalControl terminalControl = new();
        readonly SystemStatus _statusClass = new();
        //
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
            loadingControl.Dock = DockStyle.Fill;

            if (Settings._Data.StartCoreOnRun == true && Settings._Data.MySQLExecutablePath == null)
            {
                FormAlert.ShowAlert("Select first the MySQL Location",NotificationType.Info);
            }   
        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            //Load Home Controls by button
            pnlTabs.Controls.Clear();
            pnlTabs.Controls.Add(homeControl);
            homeControl.Dock = DockStyle.Fill;
        }
        private void BtnTerminal_Click(object sender, EventArgs e)
        {
            //Load Terminal Controls by button
            pnlTabs.Controls.Clear();
            pnlTabs.Controls.Add(terminalControl);
        }
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            //Load Settings Controls by button
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
            }
            else
            {
                btnHome.Enabled = false;
                btnTerminal.Enabled = false;
                btnSettings.Enabled = false;
            }

            if (Settings._Data.ServerCrashCheck == true & homeControl._isRuningBnet == true & homeControl._isRuningWorld == true)
            {
                timerCrashCheck.Start();
            }
            else if (Settings._Data.ServerCrashCheck == false)
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
            if (Settings._Data.RunWithWindows == true)
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

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings._Data.StayInTray == true)
            {
                this.Hide();
                e.Cancel = true;
            }
            else if (Settings._Data.StayInTray == false)
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

                FormAlert.ShowAlert("World server could not be started again! Fatal Error!", NotificationType.Info);
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

                FormAlert.ShowAlert("Bnet/Auth server could not be started again! Fatal Error!", NotificationType.Info);
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

                FormAlert.ShowAlert("MySQL server could not be started again! Fatal Error!", NotificationType.Info);
            }
        }
        private void ShowTrionItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }
        private void StartTrionItem_Click(object sender, EventArgs e)
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
        private void ExitTrionItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
