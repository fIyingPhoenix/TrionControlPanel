using TrionControlPanel.Database;
using TrionControlPanel.Alerts;
using TrionControlPanel.Settings;
using TrionControlPanel.UI;

namespace TrionControlPanel.TabsComponents
{
    public partial class TerminalControl : UserControl
    {
        Settings.Settings Settings = new();
        RealmListMenager RealmListMenager = new();
        public TerminalControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer, true);
        }
        private void LoadServerInfos()
        {
            RealmSettings();
            txtRealmName.Text = RealmListBuild.RealmName!;
            txtRealmAddress.Text = RealmListBuild.RealmAddress!;
            txtRealmLocalAddress.Text = RealmListBuild.RealmLocalAddress!;
            txtRealmSubMask.Text = RealmListBuild.RealmSubMask!;
            txtRealmPort.Text = RealmListBuild.RealmPort!;
            txtRealmTimeZone.Text = RealmListBuild.RealmTimeZone!;
            txtRealmGameBuild.Text = RealmListBuild.GameBuild!;
            txtRealmRegion.Text = RealmListBuild.GameRegion!;
        }
        private void GetRealmList()
        { 
            RealmListMenager.GetRealmList();
            if (RealmListMenager.GetRealmList() == false )
            {
                FormAlert.ShowAlert(RealmListMenager.RealmListMenagerMessage, NotificationType.Error);
            }
        }
        private void BtnLoadRealm_Click(object sender, EventArgs e)
        {
            GetRealmList();
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
            if (Settings._Data.SelectedCore == 3 | Settings._Data.SelectedCore == 4 | Settings._Data.SelectedCore == 5)
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
            if (Settings._Data.SelectedCore == 0)
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
            if (Settings._Data.SelectedCore == 1)
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
            if (Settings._Data.SelectedCore == 2)
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
            if (Settings._Data.SelectedCore == 6)
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
            RealmListBuild.RealmName = txtRealmName.Text;
            RealmListBuild.RealmAddress = txtRealmAddress.Text;
            RealmListBuild.RealmLocalAddress = txtRealmLocalAddress.Text;
            RealmListBuild.RealmSubMask =txtRealmSubMask.Text;
            RealmListBuild.RealmPort = txtRealmPort.Text;
            RealmListBuild.RealmTimeZone = txtRealmTimeZone.Text;
            RealmListBuild.GameBuild = txtRealmGameBuild.Text;
            RealmListBuild.GameRegion = txtRealmRegion.Text;
            RealmListMenager.SaveRealmList();
        }

        private void TerminalControl_Load(object sender, EventArgs e)
        {
            LoadServerInfos();
        }
    }
}
