using MySql.Data.MySqlClient;
using System.Data;
using TrionControlPanel.Alerts;
using TrionControlPanel.Properties;

namespace TrionControlPanel.Database
{
    internal static class RealmListMenager
    {
        public static void GetRealmList()
        {
            try
            {
                string reamlist = "";
                //AscEmu
                if (Settings.Default.SelectedCore == 0)
                {
                    reamlist = "realms";
                }
                //AzerothCore, CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP), cMaNGOS, Vanilla MaNGOS
                else if (
                    Settings.Default.SelectedCore == 1 |
                    Settings.Default.SelectedCore == 2 |
                    Settings.Default.SelectedCore == 3 |
                    Settings.Default.SelectedCore == 4 |
                    Settings.Default.SelectedCore == 5 |
                    Settings.Default.SelectedCore == 6)
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
                if (Settings.Default.SelectedCore == 0)
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
                        FormMain.Alert("Realm List could not be found!", NotificationType.Error);
                    }
                }
                //AzerothCore
                if (Settings.Default.SelectedCore == 1)
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
                        FormMain.Alert("Realm List could not be found!", NotificationType.Error);
                    }
                }
                //CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP)
                else if (Settings.Default.SelectedCore == 3 | Settings.Default.SelectedCore == 4 | Settings.Default.SelectedCore == 5)
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
                        FormMain.Alert("Realm List could not be found!", NotificationType.Error);
                    }
                }
                //cMaNGOS
                else if (Settings.Default.SelectedCore == 2)
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
                        FormMain.Alert("Realm List could not be found!", NotificationType.Error);
                    }
                }
                //Vanilla MaNGOS
                else if (Settings.Default.SelectedCore == 6)
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
                        FormMain.Alert("Realm List could not be found!", NotificationType.Error);
                    }
                }
            }
            catch
            {
                FormMain.Alert("Connecting to server failed!", NotificationType.Error);
            }
        }
        public static void SaveRealmList()
        {
            MySQLConnect databaseConnection = new();
            //AscEmu
            if (Settings.Default.SelectedCore == 0)
            {

                MySqlCommand command = new($@"UPDATE `realms` SET `password` = '{RealmListBuild.RealmName}' WHERE `id` = `{RealmListBuild.RealmID}`;", databaseConnection.GetConnection);
                MySQLConnect.MySqlConnect();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormMain.Alert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormMain.Alert("Error Execute Query!", NotificationType.Error);
                }
                MySQLConnect.MySqlDisconnectCheck();
            }
            //AzerothCore
            if (Settings.Default.SelectedCore == 1)
            {
                string sqlCommand = $@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `localAddress` = '{RealmListBuild.RealmLocalAddress}', `localSubnetMask` = '{RealmListBuild.RealmSubMask}', `port` = {RealmListBuild.RealmPort}, `icon` = 0, `flag` = 2, `timezone` = {RealmListBuild.RealmTimeZone}, `gamebuild` = {RealmListBuild.GameBuild} WHERE `id` = {RealmListBuild.RealmID};";
                MySqlCommand command = new(sqlCommand, databaseConnection.GetConnection);
                MySQLConnect.MySqlConnect();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormMain.Alert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormMain.Alert("Error Execute Query!", NotificationType.Error);
                }
                MySQLConnect.MySqlDisconnectCheck();
            }
            //CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP)
            else if (Settings.Default.SelectedCore == 3 | Settings.Default.SelectedCore == 4 | Settings.Default.SelectedCore == 5)
            {
                string sqlCommand = $@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `localAddress` = '{RealmListBuild.RealmLocalAddress}', `localSubnetMask` = '{RealmListBuild.RealmSubMask}', `port` = {RealmListBuild.RealmPort}, `timezone` = {RealmListBuild.RealmTimeZone}, `gamebuild` = {RealmListBuild.GameBuild}, `Region` = {RealmListBuild.GameRegion} WHERE `id` = {RealmListBuild.RealmID};";
                MySqlCommand command = new(sqlCommand, databaseConnection.GetConnection);
                MySQLConnect.MySqlConnect();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormMain.Alert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormMain.Alert("Error Execute Query!", NotificationType.Error);
                }
                MySQLConnect.MySqlDisconnectCheck();
            }
            //cMaNGOS
            if (Settings.Default.SelectedCore == 2)
            {
                string sqlCommand = $@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `port` = {RealmListBuild.RealmPort}, `timezone` = {RealmListBuild.RealmTimeZone}, `realmbuilds` = '{RealmListBuild.GameBuild}' WHERE `id` = {RealmListBuild.RealmID};";
                MySqlCommand command = new(sqlCommand, databaseConnection.GetConnection);
                MySQLConnect.MySqlConnect();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormMain.Alert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormMain.Alert("Error Execute Query!", NotificationType.Error);
                }
                MySQLConnect.MySqlDisconnectCheck();
            }
            //vMaNGOS
            if (Settings.Default.SelectedCore == 6)
            {
                string sqlCommand = $@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `localAddress` = '{RealmListBuild.RealmLocalAddress}', `localSubnetMask` = '{RealmListBuild.RealmSubMask}', `port` = {RealmListBuild.RealmPort},`timezone` = {RealmListBuild.RealmTimeZone}, `gamebuild_min` = {RealmListBuild.GameBuild}, `gamebuild_max` = {RealmListBuild.GameRegion} WHERE `id` = {RealmListBuild.RealmID};";
                MySqlCommand command = new(sqlCommand, databaseConnection.GetConnection);
                MySQLConnect.MySqlConnect();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormMain.Alert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormMain.Alert("Error Execute Query!", NotificationType.Error);
                }
                MySQLConnect.MySqlDisconnectCheck();
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
