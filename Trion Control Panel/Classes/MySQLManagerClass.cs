using TrionControlPanel.Properties;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace TrionControlPanel.Classes
{
    internal class DatabaseConnection
    {
        private readonly static MySqlConnection MySqlCore = new($"Server={Settings.Default.MySQLServerHost};Port={Settings.Default.MySQLServerPort};User Id={Settings.Default.MySQLServerUsername};Password={Settings.Default.MySQLServerPassword};Database={Settings.Default.AuthDatabaseName}");
        internal MySqlConnection GetConnection
        {
            get
            {
                return MySqlCore;
            }
        }
        public static void MySqlConnect()
        {
            try
            {
                if (MySqlCore.State == ConnectionState.Closed)
                {
                    MySqlCore.Open();
                }
            }
            catch (Exception ex)
            {
                FormMain.Alert(ex.ToString(), NotificationType.Error);
            }
        }
        internal static void MySqlConnectCheck()
        {
            try
            {
                if (MySqlCore.State == ConnectionState.Closed)
                {
                    MySqlCore.Open();
                    Settings.Default.MySQLConnect = true;
                    Settings.Default.MySQLConnectFaild = false;
                    Settings.Default.Save();
                    FormMain.Alert($"The SQL Connection is {MySqlCore.State}", NotificationType.Success);
                    MySqlCore.Close();
                }
            }
            catch (Exception ex)
            {
                FormMain.Alert(ex.Message, NotificationType.Error);
                Settings.Default.MySQLConnectFaild = true;
                Settings.Default.Save();
            }
        }
        internal static void MySqlDisconnectCheck()
        {
            try
            {
                if (MySqlCore.State == ConnectionState.Open)
                {
                    MySqlCore.Close();
                }
            }
            catch (Exception ex)
            {
                FormMain.Alert(ex.Message, NotificationType.Error);
            }
        }
    }
    internal class RealmListMenager
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
            DatabaseConnection databaseConnection = new();
            MySqlCommand command = new ($"SELECT * FROM {reamlist}", databaseConnection.GetConnection);
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
            DatabaseConnection databaseConnection = new();
            //AscEmu
            if (Settings.Default.SelectedCore == 0)
            {
                MySqlCommand command = new ($@"UPDATE `realms` SET `password` = '{RealmListBuild.RealmName}' WHERE `id` = `{RealmListBuild.RealmID}`;", databaseConnection.GetConnection);
                DatabaseConnection.MySqlConnect();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormMain.Alert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormMain.Alert("Error Execute Query!", NotificationType.Error);
                }
                DatabaseConnection.MySqlDisconnectCheck();
            }
            //AzerothCore
            if (Settings.Default.SelectedCore == 1)
            {
                MySqlCommand command = new ($@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `localAddress` = '{RealmListBuild.RealmLocalAddress}', `localSubnetMask` = '{RealmListBuild.RealmSubMask}', `port` = {RealmListBuild.RealmPort}, `icon` = 0, `flag` = 2, `timezone` = {RealmListBuild.RealmTimeZone}, `gamebuild` = {RealmListBuild.GameBuild} WHERE `id` = {RealmListBuild.RealmID};", databaseConnection.GetConnection);
                DatabaseConnection.MySqlConnect();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormMain.Alert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormMain.Alert("Error Execute Query!", NotificationType.Error);
                }
                DatabaseConnection.MySqlDisconnectCheck();
            }
            //CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP)
            else if (Settings.Default.SelectedCore == 3 | Settings.Default.SelectedCore == 4 | Settings.Default.SelectedCore == 5)
            {
                MySqlCommand command = new($@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `localAddress` = '{RealmListBuild.RealmLocalAddress}', `localSubnetMask` = '{RealmListBuild.RealmSubMask}', `port` = {RealmListBuild.RealmPort}, `timezone` = {RealmListBuild.RealmTimeZone}, `gamebuild` = {RealmListBuild.GameBuild}, `Region` = {RealmListBuild.GameRegion} WHERE `id` = {RealmListBuild.RealmID};", databaseConnection.GetConnection);
                DatabaseConnection.MySqlConnect();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormMain.Alert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormMain.Alert("Error Execute Query!", NotificationType.Error);
                }
                DatabaseConnection.MySqlDisconnectCheck();
            }
            //cMaNGOS
            if (Settings.Default.SelectedCore == 2)
            {
                MySqlCommand command = new ($@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `port` = {RealmListBuild.RealmPort}, `timezone` = {RealmListBuild.RealmTimeZone}, `realmbuilds` = '{RealmListBuild.GameBuild}' WHERE `id` = {RealmListBuild.RealmID};", databaseConnection.GetConnection);
                DatabaseConnection.MySqlConnect();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormMain.Alert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormMain.Alert("Error Execute Query!", NotificationType.Error);
                }
                DatabaseConnection.MySqlDisconnectCheck();
            }
            //vMaNGOS
            if (Settings.Default.SelectedCore == 6)
            {
                MySqlCommand command = new($@"UPDATE `realmlist` SET `name` = '{RealmListBuild.RealmName}', `address` = '{RealmListBuild.RealmAddress}', `localAddress` = '{RealmListBuild.RealmLocalAddress}', `localSubnetMask` = '{RealmListBuild.RealmSubMask}', `port` = {RealmListBuild.RealmPort},`timezone` = {RealmListBuild.RealmTimeZone}, `gamebuild_min` = {RealmListBuild.GameBuild}, `gamebuild_max` = {RealmListBuild.GameRegion} WHERE `id` = {RealmListBuild.RealmID};", databaseConnection.GetConnection);
                DatabaseConnection.MySqlConnect();
                if (command.ExecuteNonQuery() == 1)
                {
                    FormMain.Alert("Query Execute Successfuly!", NotificationType.Success);
                }
                else
                {
                    FormMain.Alert("Error Execute Query!", NotificationType.Error);
                }
                DatabaseConnection.MySqlDisconnectCheck();
            }
        }
    }
    internal class AccountMenager
    {
        public static bool CheckForUsername(string username)
        {
            DatabaseConnection databaseConnection = new();
            MySqlCommand command = new("SELECT id FROM account WHERE username = @username", databaseConnection.GetConnection);
            command.Parameters.Add("@username", MySqlDbType.Text).Value = username;
            MySqlDataAdapter _dataAdapter = new(command);
            DataTable table = new();
            _dataAdapter.Fill(table);
            databaseConnection.GetConnection.Close();
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP)
        public string CalculatePassHashBnet(string name, string password)
        {
            //Hash Calculate system (Bnet Users)
            SHA256 sha256 = SHA256.Create();
            var nameHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(name));
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(nameHash.ToHexString() + ":" + password)).ToHexString(true);
        }
        public string CalculatePasswordHashAscEmu(string name, string password)
        {
            using (SHA1Managed sha1 = new())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes($"{name.ToUpper()}:{password.ToUpper()}"));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    // can be "x2" if you want LowerCase - can be "X2" if you want UpperCase
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
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
