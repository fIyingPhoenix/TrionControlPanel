using ABI.Windows.Media.Protection.PlayReady;
using TrionDatabase;
using TrionLibrary;
using Windows.ApplicationModel.UserDataAccounts.SystemAccess;

namespace TrionControlPanelDesktop.Controls
{
    public partial class DatabaseControl : UserControl
    {
        List<DataModels.RealmList> RList;
        public DatabaseControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            
        }
        private async Task LoadData()
        {
            await LoadRealmList();
            TXTInternIP.Text = NetworkHelper.GetInternalIpAddress();
            TXTPublicIP.Text = await NetworkHelper.GetExternalIpAddress();
        }
        private static string SaveDataRealmSql()
        {
            return Data.Settings.SelectedCore switch
            {
                EnumModels.Cores.AscEmu => "",
                EnumModels.Cores.AzerothCore => $"SELECT * FROM `realmlist` LIMIT 100;",
                EnumModels.Cores.CMaNGOS => "",
                EnumModels.Cores.CypherCore => "",
                EnumModels.Cores.TrinityCore335 => "",
                EnumModels.Cores.TrinityCore => "",
                EnumModels.Cores.TrinityCoreClassic => "",
                EnumModels.Cores.VMaNGOS => "",
                _ => "",
            };
        }
        private static string LoadDataRealmSql()
        {
            return Data.Settings.SelectedCore switch
            {
                EnumModels.Cores.AscEmu => $"SELECT * FROM `realmlist` LIMIT 100;",
                EnumModels.Cores.AzerothCore => $"SELECT * FROM `realmlist` LIMIT 100;",
                EnumModels.Cores.CMaNGOS => $"SELECT * FROM `realmlist` LIMIT 100;",
                EnumModels.Cores.CypherCore => $"SELECT * FROM `realmlist` LIMIT 100;",
                EnumModels.Cores.TrinityCore335 => $"SELECT * FROM `realmlist` LIMIT 100;",
                EnumModels.Cores.TrinityCore => $"SELECT * FROM `realmlist` LIMIT 100;",
                EnumModels.Cores.TrinityCoreClassic => $"SELECT * FROM `realmlist` LIMIT 100;",
                EnumModels.Cores.VMaNGOS => $"SELECT * FROM `realmlist` LIMIT 100;",
                _ => "",
            };
        }
        private async Task LoadRealmList()
        {
            try
            {
                CBOXReamList.Items.Clear();
                if(RList != null) { RList.Clear(); }
                RList = await SQLDataAccess.LodaData<DataModels.RealmList, dynamic>(LoadDataRealmSql(), new { }, SQLDataConnect.ConnectionString(Data.Settings.AuthDatabase));
                foreach (var realm in RList)
                {
                    CBOXReamList.Items.Add(realm.Name);
                }
                CBOXReamList.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Data.Message = "Error getting data: " + ex.Message;
            }

        }
        private async Task SaveRealmList()
        {

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

        private void DatabaseControl_Load(object sender, EventArgs e)
        {
          _ = LoadData();
        }
    }

}
