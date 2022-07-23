using TrionControlPanel.Classes;
using TrionControlPanel.Properties;

namespace TrionControlPanel.TabsComponents
{
    public partial class TerminalControl : UserControl
    {
        public TerminalControl()
        {
            InitializeComponent();
        }
        private void LoadServerInfos()
        {
            RealmSettings();
            txtRealmName.Texts = RealmListBuild.RealmName!;  
            txtRealmAddress.Texts = RealmListBuild.RealmAddress!; 
            txtRealmLocalAddress.Texts = RealmListBuild.RealmLocalAddress!; 
            txtRealmSubMask.Texts = RealmListBuild.RealmSubMask!; 
            txtRealmPort.Texts = RealmListBuild.RealmPort!;
            txtRealmTimeZone.Texts = RealmListBuild.RealmTimeZone!; 
            txtRealmGameBuild.Texts = RealmListBuild.GameBuild!; 
            txtRealmRegion.Texts = RealmListBuild.GameRegion!; 
        }
        private void TimerCheck_Tick(object sender, EventArgs e)
        {
            
            if (tglShowPassword.Checked  == false)
            {
                txtPassword.PasswordChar = false;
                txtRePassword.PasswordChar = false;
            }
            else if (tglShowPassword.Checked == true)
            {
                txtPassword.PasswordChar = true;
                txtRePassword.PasswordChar = true;
            }
        }
        private void BtnLoadRealm_Click(object sender, EventArgs e)
        {
            RealmListMenager.GetRealmList();
            LoadServerInfos();
        }

        private void AllowJustNumbers(object sender, KeyPressEventArgs e)
        {
            //allow just numbers/ digits
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point. i dont thik is needed but its nice to have
            if ((e.KeyChar == '.') && (((TextBox)sender).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        internal void RealmSettings()
        {
            //CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP)
            if (Settings.Default.SelectedCore == 3 | Settings.Default.SelectedCore == 4 | Settings.Default.SelectedCore == 5)
            {
                lblWorldName.Text = "Realm Name:";
                lblRealmBuild.Text = "Realm Build:";
                lblRealmRegion.Text = "Realm Region:";
                txtRealmPort.Enabled = true;
                txtRealmTimeZone.Enabled = true;
                txtRealmGameBuild.Enabled = true;
                txtRealmRegion.Enabled = true;
                txtRealmAddress.Enabled = true;
                txtRealmLocalAddress.Enabled = true;
                txtRealmSubMask.Enabled = true;
                txtRealmRegion.Enabled = true;
            }
            //AscEmu
            if (Settings.Default.SelectedCore == 0)
            {
                lblWorldName.Text = "Realm Password:";
                lblRealmBuild.Text = "Realm Build:";
                lblRealmRegion.Text = "Realm Region:";
                txtRealmPort.Enabled = false;
                txtRealmSubMask.Enabled = false;
                txtRealmTimeZone.Enabled = false;
                txtRealmGameBuild.Enabled = false;
                txtRealmLocalAddress.Enabled = false;
                txtRealmRegion.Enabled = false;
                txtRealmAddress.Enabled = false;
                RealmListBuild.RealmSubMask = "N/A";
                RealmListBuild.RealmAddress = "N/A";
                RealmListBuild.RealmPort = "N/A";
                RealmListBuild.RealmLocalAddress = "N/A";
                RealmListBuild.GameRegion = "N/A";
                RealmListBuild.RealmTimeZone = "N/A";
                RealmListBuild.GameBuild = "N/A";
            }
            //AzerothCore
            if (Settings.Default.SelectedCore == 1)
            {
                txtRealmPort.Enabled = true;
                txtRealmTimeZone.Enabled = true;
                txtRealmGameBuild.Enabled = true;;
                txtRealmAddress.Enabled = true;
                txtRealmLocalAddress.Enabled = true;
                txtRealmSubMask.Enabled = true;
                lblWorldName.Text = "Realm Name:";
                lblRealmBuild.Text = "Realm Build:";
                lblRealmRegion.Text = "Realm Region:";
                RealmListBuild.GameRegion = "N/A";
                txtRealmRegion.Enabled = false;
            }
            //cMaNGOS
            if (Settings.Default.SelectedCore == 2)
            {
                lblWorldName.Text = "Realm Name:";
                lblRealmBuild.Text = "Realm Build:";
                lblRealmRegion.Text = "Realm Region:";
                txtRealmPort.Enabled = true;
                txtRealmTimeZone.Enabled = true;
                txtRealmGameBuild.Enabled = true;
                txtRealmRegion.Enabled = true;
                txtRealmAddress.Enabled = true;
                txtRealmLocalAddress.Enabled = false;
                txtRealmSubMask.Enabled = false;
                txtRealmRegion.Enabled = false;
                RealmListBuild.RealmSubMask = "N/A";
                RealmListBuild.GameRegion = "N/A";
                RealmListBuild.RealmLocalAddress = "N/A";
            }
            //vMaNGOS
            if (Settings.Default.SelectedCore == 6)
            {
                lblWorldName.Text = "Realm Name:";
                txtRealmPort.Enabled = true;
                txtRealmTimeZone.Enabled = true;
                txtRealmGameBuild.Enabled = true;
                txtRealmRegion.Enabled = true;
                txtRealmAddress.Enabled = true;
                txtRealmLocalAddress.Enabled = true;
                txtRealmSubMask.Enabled = true;
                txtRealmRegion.Enabled = true;
                lblRealmBuild.Text = "Realm Build Min:";
                lblRealmRegion.Text = "Realm Build Max:";
            }
        }

        private void BtnSetRealm_Click(object sender, EventArgs e)
        {
            RealmListBuild.RealmName = txtRealmName.Texts;
            RealmListBuild.RealmAddress = txtRealmAddress.Texts;
            RealmListBuild.RealmLocalAddress = txtRealmLocalAddress.Texts;
            RealmListBuild.RealmSubMask =txtRealmSubMask.Texts;
            RealmListBuild.RealmPort = txtRealmPort.Texts;
            RealmListBuild.RealmTimeZone = txtRealmTimeZone.Texts;
            RealmListBuild.GameBuild = txtRealmGameBuild.Texts;
            RealmListBuild.GameRegion = txtRealmRegion.Texts;
            RealmListMenager.SaveRealmList();
        }

        private void TerminalControl_Load(object sender, EventArgs e)
        {
            RealmListMenager.GetRealmList();
            LoadServerInfos();
        }
    }
}
