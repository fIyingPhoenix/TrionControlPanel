using TrionLibrary.Models;
using TrionLibrary.Network;
using TrionLibrary.Setting;
using TrionLibrary.Database;
using TrionLibrary.Sys;
using TrionControlPanelDesktop.Data;

namespace TrionControlPanelDesktop.Controls
{
    public partial class DatabaseControl : UserControl
    {
        List<Realmlist.Trinity> RealmlistTrinity;
        List<Realmlist.Ascemu> RealmlistAscemu;
        List<Realmlist.Mangos> RealmlistMangos;
        
        public DatabaseControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            TabControl1.TabPages.Remove(tPageAccount);
            _ = LoadData();
        }
        private async Task LoadData()
        {
            if (User.UI.Form.DBRunning)
            {
                await LoadRealmList();
            }
            TXTInternIP.Text = Helper.GetInternalIpAddress();
            TXTPublicIP.Text = await Helper.GetExternalIpAddress();
        }
        private async Task LoadRealmList()
        {
            try
            {
                CBOXReamList.Items.Clear();

                if (Setting.List.SelectedCore == Enums.Cores.AzerothCore ||
                    Setting.List.SelectedCore == Enums.Cores.CypherCore ||
                    Setting.List.SelectedCore == Enums.Cores.TrinityCore ||
                    Setting.List.SelectedCore == Enums.Cores.TrinityCore335 ||
                    Setting.List.SelectedCore == Enums.Cores.TrinityCoreClassic)
                {
                    RealmlistTrinity?.Clear();
                    RealmlistTrinity = await Access.LodaData<Realmlist.Trinity, dynamic>(SQLQuery.LoadRealm(), new { }, Connect.String(Setting.List.AuthDatabase));
                    foreach (var realm in RealmlistTrinity!)
                    {
                        CBOXReamList.Items.Add(realm.Name);
                    }
                    CBOXReamList.SelectedIndex = 0;
                }
                if (Setting.List.SelectedCore == Enums.Cores.CMaNGOS ||
                    Setting.List.SelectedCore == Enums.Cores.VMaNGOS)
                {
                    RealmlistMangos?.Clear();
                    RealmlistMangos = await Access.LodaData<Realmlist.Mangos, dynamic>(SQLQuery.LoadRealm(), new { }, Connect.String(Setting.List.AuthDatabase));
                    foreach (var realm in RealmlistMangos!)
                    {
                        CBOXReamList.Items.Add(realm.Name);
                    }
                    CBOXReamList.SelectedIndex = 0;
                }
                if (Setting.List.SelectedCore == Enums.Cores.AscEmu)
                {
                    RealmlistAscemu?.Clear();
                    RealmlistAscemu = await Access.LodaData<Realmlist.Ascemu, dynamic>(SQLQuery.LoadRealm(), new { }, Connect.String(Setting.List.AuthDatabase));
                    foreach (var realm in RealmlistAscemu!)
                    {
                        CBOXReamList.Items.Add(realm.ID);
                    }
                    CBOXReamList.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Infos.Message = "Error loding data: " + ex.Message;
            }

        }
        private async Task SaveRealmList()
        {
            try
            {
                if (Setting.List.SelectedCore == Enums.Cores.AzerothCore ||
                    Setting.List.SelectedCore == Enums.Cores.CypherCore ||
                    Setting.List.SelectedCore == Enums.Cores.TrinityCore ||
                    Setting.List.SelectedCore == Enums.Cores.TrinityCore335 ||
                    Setting.List.SelectedCore == Enums.Cores.TrinityCoreClassic)
                {
                    //`name`, `address`, `port`, `icon`, `flag`, `timezone`
                    Access.SaveData(SQLQuery.SaveRealm(), new
                    {
                        Name = TXTRealmName.Text,
                        Address = TXTRealmAddress.Text,
                        LocalAddress = TXTRealmLocalAddress.Text,
                        LocalSubnetMask = TXTRealmSubnetMask.Text,
                        Port = TXTRealmPort.Text,
                        Icon = TXTRealmIcon.Text,
                        Flag = TXTRealmFlag.Text,
                        Timezone = TXTRealmTime.Text,
                        ID = TXTRealmID.Text

                    }, Connect.String(Setting.List.AuthDatabase));
                    await LoadRealmList();
                }
                if (Setting.List.SelectedCore == Enums.Cores.CMaNGOS ||
                    Setting.List.SelectedCore == Enums.Cores.VMaNGOS)
                {
                    RealmlistMangos?.Clear();
                    Access.SaveData(SQLQuery.SaveRealm(), new
                    {
                        Name = TXTRealmName.Text,
                        Address = TXTRealmAddress.Text,
                        Port = TXTRealmPort.Text,
                        Icon = TXTRealmIcon.Text,
                        Flag = TXTRealmFlag.Text,
                        Timezone = TXTRealmTime.Text,
                        ID = TXTRealmID.Text

                    }, Connect.String(Setting.List.AuthDatabase));
                    await LoadRealmList();
                }
                if (Setting.List.SelectedCore == Enums.Cores.AscEmu)
                {
                    RealmlistAscemu?.Clear();
                    Access.SaveData(SQLQuery.SaveRealm(), new
                    {
                        Password = TXTRealmName.Text,
                        StatusChangeTime = TXTRealmAddress.Text,

                    }, Connect.String(Setting.List.AuthDatabase));
                    await LoadRealmList();
                }
            }
            catch (Exception ex)
            {
                Infos.Message = "Error saving data: " + ex.Message;
            }
        }
        private void CBOXReamList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Realmlist.Trinity SearchList = RealmlistTrinity.Find(obj => obj.Name == CBOXReamList.SelectedItem.ToString())!;
            if (SearchList != null)
            {
                TXTRealmID.Text = SearchList.ID.ToString();
                TXTRealmName.Text = SearchList.Name.ToString();
                TXTRealmAddress.Text = SearchList.Address.ToString();
                TXTRealmLocalAddress.Text = SearchList.LocalAddress.ToString();
                TXTRealmSubnetMask.Text = SearchList.LocalSubnetMask.ToString();
                TXTRealmPort.Text = SearchList.Port.ToString();
                TXTRealmIcon.Text = SearchList.Icon.ToString();
                TXTRealmFlag.Text = SearchList.Flag.ToString();
                TXTRealmTime.Text = SearchList.Timezone.ToString();
            }
        }
        private async void BTNForceUpdate_Click(object sender, EventArgs e)
        {
            await LoadRealmList();
        }
        private async void DatabaseControl_LoadAsync(object sender, EventArgs e)
        {
            await LoadData();
        }
        private async void BTNOpenIntern_Click(object sender, EventArgs e)
        {
            TXTRealmAddress.Text = TXTInternIP.Text;
            await SaveRealmList();
        }
        private async void BTNOpenPublic_ClickAsync(object sender, EventArgs e)
        {
            TXTRealmAddress.Text = TXTPublicIP.Text;
            await SaveRealmList();
        }
        private async void BTNSaveData_ClickAsync(object sender, EventArgs e)
        {
            await SaveRealmList();
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {

        }
    }

}
