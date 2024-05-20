using TrionDatabase;
using TrionLibrary;

namespace TrionControlPanelDesktop.Controls
{
    public partial class DatabaseControl : UserControl
    {
        List<DataModels.RealmList> RList;
        List<DataModels.RealmListAscemu> RListAscemu;
        List<DataModels.RealmListMangos> RListMangos;
        public DatabaseControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            _ = LoadData();
        }
        private async Task LoadData()
        {
            await LoadRealmList();
            TXTInternIP.Text = NetworkHelper.GetInternalIpAddress();
            TXTPublicIP.Text = await NetworkHelper.GetExternalIpAddress();
        }
        private async Task LoadRealmList()
        {
            try
            {
                CBOXReamList.Items.Clear();

                if (Data.Settings.SelectedCore == EnumModels.Cores.AzerothCore ||
                    Data.Settings.SelectedCore == EnumModels.Cores.CypherCore ||
                    Data.Settings.SelectedCore == EnumModels.Cores.TrinityCore ||
                    Data.Settings.SelectedCore == EnumModels.Cores.TrinityCore335 ||
                    Data.Settings.SelectedCore == EnumModels.Cores.TrinityCoreClassic)
                {
                    RList?.Clear();
                    RList = await SQLDataAccess.LodaData<DataModels.RealmList, dynamic>(SQLDataConnect.LoadRealmSql(), new { }, SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase));
                    foreach (var realm in RList!)
                    {
                        CBOXReamList.Items.Add(realm.Name);
                    }
                    CBOXReamList.SelectedIndex = 0;
                }
                if (Data.Settings.SelectedCore == EnumModels.Cores.CMaNGOS ||
                    Data.Settings.SelectedCore == EnumModels.Cores.VMaNGOS)
                {
                    RListMangos?.Clear();
                    RListMangos = await SQLDataAccess.LodaData<DataModels.RealmListMangos, dynamic>(SQLDataConnect.LoadRealmSql(), new { }, SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase));
                    foreach (var realm in RListMangos!)
                    {
                        CBOXReamList.Items.Add(realm.Name);
                    }
                    CBOXReamList.SelectedIndex = 0;
                }
                if (Data.Settings.SelectedCore == EnumModels.Cores.AscEmu)
                {
                    RListAscemu?.Clear();
                    RListAscemu = await SQLDataAccess.LodaData<DataModels.RealmListAscemu, dynamic>(SQLDataConnect.LoadRealmSql(), new { }, SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase));
                    foreach (var realm in RListAscemu!)
                    {
                        CBOXReamList.Items.Add(realm.ID);
                    }
                    CBOXReamList.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Data.Message = "Error loding data: " + ex.Message;
            }

        }
        private async Task SaveRealmList()
        {
            //try
            //{
                if (Data.Settings.SelectedCore == EnumModels.Cores.AzerothCore ||
                    Data.Settings.SelectedCore == EnumModels.Cores.CypherCore ||
                    Data.Settings.SelectedCore == EnumModels.Cores.TrinityCore ||
                    Data.Settings.SelectedCore == EnumModels.Cores.TrinityCore335 ||
                    Data.Settings.SelectedCore == EnumModels.Cores.TrinityCoreClassic)
                {
                    //`name`, `address`, `port`, `icon`, `flag`, `timezone`
                    SQLDataAccess.SaveData(SQLDataConnect.SaveRealmSql(), new
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

                    }, SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase));
                    await LoadRealmList();
                }
                if (Data.Settings.SelectedCore == EnumModels.Cores.CMaNGOS ||
                    Data.Settings.SelectedCore == EnumModels.Cores.VMaNGOS)
                {
                    RListMangos?.Clear();
                    SQLDataAccess.SaveData(SQLDataConnect.SaveRealmSql(), new
                    {
                        Name = TXTRealmName.Text,
                        Address = TXTRealmAddress.Text,
                        Port = TXTRealmPort.Text,
                        Icon = TXTRealmIcon.Text,
                        Flag = TXTRealmFlag.Text,
                        Timezone = TXTRealmTime.Text,
                        ID = TXTRealmID.Text

                    }, SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase));
                    await LoadRealmList();
                }
                if (Data.Settings.SelectedCore == EnumModels.Cores.AscEmu)
                {
                    RListAscemu?.Clear();
                    SQLDataAccess.SaveData(SQLDataConnect.SaveRealmSql(), new
                    {
                        Password = TXTRealmName.Text,
                        StatusChangeTime = TXTRealmAddress.Text,

                    }, SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase));
                    await LoadRealmList();
                }
            //}
            //catch (Exception ex)
            //{
            //    Data.Message = "Error saving data: " + ex.Message;
            //}
        }
        private void CBOXReamList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataModels.RealmList SearchList = RList.Find(obj => obj.Name == CBOXReamList.SelectedItem.ToString())!;
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
    }

}
