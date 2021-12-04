
using CypherCore_Server_Laucher.Classes;
using CypherCore_Server_Laucher.TabsComponents;
using System.Reflection;

namespace CypherCore_Server_Laucher
{
    public partial class FormMain : Form
    {

        ConnectionClass _connectionClass = new ConnectionClass();
        HomeControl homeControl = new HomeControl();
        SettingControl settingControl = new SettingControl();
    

        public FormMain()
        {

            //fix the problem with thread calls
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            //Load Home Controls

            pnlTabs.Controls.Add(homeControl);
        }

   

        private void btnConnect_Click(object sender, EventArgs e)
        {

            //test mysql connection
            _connectionClass.MySqlConnect();
            if(_connectionClass.MySQLConnected() == true)
            {
                btnConnect.BackColor = Color.Green;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            //Load Home Controls by button
            pnlTabs.Controls.Clear();
            pnlTabs.Controls.Add(homeControl);
           
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlTabs.Controls.Clear();
            pnlTabs.Controls.Add(settingControl);
        }
    }
}
