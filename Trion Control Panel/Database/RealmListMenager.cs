using MySql.Data.MySqlClient;
using System.Data;
using TrionControlPanel.Alerts;
using TrionControlPanelSettings;

namespace TrionControlPanel.Database
{
    internal class RealmListMenager
    {
        public static bool GetRealmListSucces { get; set; }

        Settings Settings = new();
        public void GetRealmList()
        {
            try
            {
                string reamlist = "";
                //AscEmu
                if (Settings._Data.SelectedCore == 0)
                {
                    reamlist = "realms";
                }
                //AzerothCore, CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP), cMaNGOS, Vanilla MaNGOS
                else if (
                    Settings._Data.SelectedCore == 1 |
                    Settings._Data.SelectedCore == 2 |
                    Settings._Data.SelectedCore == 3 |
                    Settings._Data.SelectedCore == 4 |
                    Settings._Data.SelectedCore == 5 |
                    Settings._Data.SelectedCore == 6)
                {
                    reamlist = "realmlist";
                }
                //
                string sqlCommand = $"SELECT * FROM {reamlist}";
                MySQLConnect databaseConnection = new();
                MySqlCommand command = new(sqlCommand, databaseConnection.GetConnection);
                MySqlDataAdapter _dataAdapter = new(command);
                DataTable table = new();
                _dataAdapter.Fill(table);
                //AscEmu
                if (Settings._Data.SelectedCore == 0)
                {
                    if (table.Rows.Count > 0)
                    {
                        RealmListBuild.RealmID = table.Rows[0][0].ToString();
                        RealmListBuild.RealmName = table.Rows[0][1].ToString();
                        RealmListBuild.RealmAddress = null;
                        RealmListBuild.RealmLocalAddress = null;
                        RealmListBuild.RealmSubMask = null;
                        RealmListBuild.RealmPort = null;
                        RealmListBuild.RealmTimeZone = null;
                        RealmListBuild.GameBuild = null;
                        RealmListBuild.GameRegion = null;
                    }
                    else
                    {
                        GetRealmListSucces = false;
                    }
                }
                //AzerothCore
                if (Settings._Data.SelectedCore == 1)
                {
                    if (table.Rows.Count > 0)
                    {
                        RealmListBuild.RealmID = table.Rows[0][0].ToString();
                        RealmListBuild.RealmName = table.Rows[0][1].ToString();
                        RealmListBuild.RealmAddress = table.Rows[0][2].ToString();
                        RealmListBuild.RealmLocalAddress = table.Rows[0][3].ToString();
                        RealmListBuild.RealmSubMask = table.Rows[0][4].ToString();
                        RealmListBuild.RealmPort = table.Rows[0][5].ToString();
                        RealmListBuild.RealmTimeZone = table.Rows[0][8].ToString();
                        RealmListBuild.GameBuild = table.Rows[0][11].ToString();
                        RealmListBuild.GameRegion = null;
                    }
                    else
                    {
                        GetRealmListSucces = false;
                    }
                }
                //CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP)
                else if (Settings._Data.SelectedCore == 3 | Settings._Data.SelectedCore == 4 | Settings._Data.SelectedCore == 5)
                {
                    if (table.Rows.Count > 0)
                    {
                        RealmListBuild.RealmID = table.Rows[0][0].ToString();
                        RealmListBuild.RealmName = table.Rows[0][1].ToString();
                        RealmListBuild.RealmAddress = table.Rows[0][2].ToString();
                        RealmListBuild.RealmLocalAddress = table.Rows[0][3].ToString();
                        RealmListBuild.RealmSubMask = table.Rows[0][4].ToString();
                        RealmListBuild.RealmPort = table.Rows[0][5].ToString();
                        RealmListBuild.RealmTimeZone = table.Rows[0][8].ToString();
                        RealmListBuild.GameBuild = table.Rows[0][11].ToString();
                        RealmListBuild.GameRegion = table.Rows[0][12].ToString();
                    }
                    else
                    {
                        GetRealmListSucces = false;
                    }
                }
                //cMaNGOS
                else if (Settings._Data.SelectedCore == 2)
                {
                    if (table.Rows.Count > 0)
                    {
                        RealmListBuild.RealmID = table.Rows[0][0].ToString();
                        RealmListBuild.RealmName = table.Rows[0][1].ToString();
                        RealmListBuild.RealmAddress = table.Rows[0][2].ToString();
                        RealmListBuild.RealmLocalAddress = null;
                        RealmListBuild.RealmSubMask = null;
                        RealmListBuild.RealmPort = table.Rows[0][3].ToString();
                        RealmListBuild.RealmTimeZone = table.Rows[0][6].ToString();
                        RealmListBuild.GameBuild = table.Rows[0][9].ToString();
                        RealmListBuild.GameRegion = null;
                    }
                    else
                    {
                        GetRealmListSucces = false;
                    }
                }
                //Vanilla MaNGOS
                else if (Settings._Data.SelectedCore == 6)
                {
                    if (table.Rows.Count > 0)
                    {
                        RealmListBuild.RealmID = table.Rows[0][0].ToString();
                        RealmListBuild.RealmName = table.Rows[0][1].ToString();
                        RealmListBuild.RealmAddress = table.Rows[0][2].ToString();
                        RealmListBuild.RealmLocalAddress = table.Rows[0][3].ToString();
                        RealmListBuild.RealmSubMask = table.Rows[0][4].ToString();
                        RealmListBuild.RealmPort = table.Rows[0][5].ToString();
                        RealmListBuild.RealmTimeZone = table.Rows[0][8].ToString();
                        RealmListBuild.GameBuild = table.Rows[0][11].ToString();
                        RealmListBuild.GameRegion = table.Rows[0][12].ToString();
                    }
                    else
                    {
                        GetRealmListSucces = false;
                    }
                }
            
            }
            catch
            {
                GetRealmListSucces = false;
               
            }

            
        }
        public void SaveRealmList()
        {
            //memo on me: chek after if i need to close the connection ;
            MySQLConnect databaseConnection = new();
            //AscEmu
            if (Settings._Data.SelectedCore == 0)
            {

                MySqlCommand command = new($@"UPDATE `realms` SET `password` = '{RealmListBuild.RealmName}' WHERE `id` = `{RealmListBuild.RealmID}`;", databaseConnection.GetConnection);
                databaseConnection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormAlert.ShowAlert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormAlert.ShowAlert("Error Execute Query!", NotificationType.Error);
                }
            }
            //AzerothCore
            if (Settings._Data.SelectedCore == 1)
            {
                string sqlCommand = $@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `localAddress` = '{RealmListBuild.RealmLocalAddress}', `localSubnetMask` = '{RealmListBuild.RealmSubMask}', `port` = {RealmListBuild.RealmPort}, `icon` = 0, `flag` = 2, `timezone` = {RealmListBuild.RealmTimeZone}, `gamebuild` = {RealmListBuild.GameBuild} WHERE `id` = {RealmListBuild.RealmID};";
                MySqlCommand command = new(sqlCommand, databaseConnection.GetConnection);
                databaseConnection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormAlert.ShowAlert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormAlert.ShowAlert("Error Execute Query!", NotificationType.Error);
                }
            }
            //CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP)
            else if (Settings._Data.SelectedCore == 3 | Settings._Data.SelectedCore == 4 | Settings._Data.SelectedCore == 5)
            {
                string sqlCommand = $@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `localAddress` = '{RealmListBuild.RealmLocalAddress}', `localSubnetMask` = '{RealmListBuild.RealmSubMask}', `port` = {RealmListBuild.RealmPort}, `timezone` = {RealmListBuild.RealmTimeZone}, `gamebuild` = {RealmListBuild.GameBuild}, `Region` = {RealmListBuild.GameRegion} WHERE `id` = {RealmListBuild.RealmID};";
                MySqlCommand command = new(sqlCommand, databaseConnection.GetConnection);
                databaseConnection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormAlert.ShowAlert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormAlert.ShowAlert("Error Execute Query!", NotificationType.Error);
                }
            }
            //cMaNGOS
            if (Settings._Data.SelectedCore == 2)
            {
                string sqlCommand = $@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `port` = {RealmListBuild.RealmPort}, `timezone` = {RealmListBuild.RealmTimeZone}, `realmbuilds` = '{RealmListBuild.GameBuild}' WHERE `id` = {RealmListBuild.RealmID};";
                MySqlCommand command = new(sqlCommand, databaseConnection.GetConnection);
                databaseConnection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormAlert.ShowAlert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormAlert.ShowAlert("Error Execute Query!", NotificationType.Error);
                }

            }
            //vMaNGOS
            if (Settings._Data.SelectedCore == 6)
            {
                string sqlCommand = $@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `localAddress` = '{RealmListBuild.RealmLocalAddress}', `localSubnetMask` = '{RealmListBuild.RealmSubMask}', `port` = {RealmListBuild.RealmPort},`timezone` = {RealmListBuild.RealmTimeZone}, `gamebuild_min` = {RealmListBuild.GameBuild}, `gamebuild_max` = {RealmListBuild.GameRegion} WHERE `id` = {RealmListBuild.RealmID};";
                MySqlCommand command = new(sqlCommand, databaseConnection.GetConnection);
                databaseConnection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormAlert.ShowAlert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormAlert.ShowAlert("Error Execute Query!", NotificationType.Error);
                }
            }
        }
    }
    internal class RealmListBuild
    {
        internal static string? RealmID { get; set; }
        internal static string? RealmName { get; set; }
        internal static string? RealmAddress { get; set; }
        internal static string? RealmLocalAddress { get; set; }
        internal static string? RealmSubMask { get; set; }
        internal static string? RealmPort { get; set; }
        internal static string? RealmTimeZone { get; set; }
        internal static string? GameBuild { get; set; }
        internal static string? GameRegion { get; set; }
    }
}
