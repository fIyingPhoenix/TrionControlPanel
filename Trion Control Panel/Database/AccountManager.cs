using TrionControlPanel.Properties;
using TrionControlPanel.Classes;
using MySql.Data.MySqlClient;
using System.Data;

namespace TrionControlPanel.Database
{
    internal class DatabaseConnection
    {
        private static MySqlConnection MySqlCore = new ($"Server={Settings.Default.MySQLServerHost};Port={Settings.Default.MySQLServerPort};User Id={Settings.Default.MySQLServerUsername};Password={Settings.Default.MySQLServerPassword};Database={Settings.Default.AuthDatabaseName}");
        internal  MySqlConnection GetConnection
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
            catch
            {
                FormMain.Alert("error", NotificationType.Error);
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
                    FormMain.Alert($"The SQL Connection is {MySqlCore.State.ToString()}", NotificationType.Success);
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
    internal class AccountManager
    {
        public static void GetRealmList()
        {
            DatabaseConnection databaseConnection = new();
            MySqlCommand command = new MySqlCommand("SELECT * FROM realmlist", databaseConnection.GetConnection);
            MySqlDataAdapter _dataAdapter = new(command);
            DataTable table = new();
            _dataAdapter.Fill(table);
            //AscEmu
            if (Settings.Default.SelectedCore == 0)
            {
                if (table.Rows.Count > 0)
                {
                    DatabaseSources.RealmID = table.Rows[0][0].ToString();
                    DatabaseSources.RealmName = table.Rows[0][1].ToString();
                    DatabaseSources.RealmAddress = null;
                    DatabaseSources.RealmLocalAddress = null;
                    DatabaseSources.RealmSubMask = null;
                    DatabaseSources.RealmPort = null;
                    DatabaseSources.RealmTimeZone = null;
                    DatabaseSources.GameBuild = null;
                    DatabaseSources.GameRegion = null;
                }
                else
                {
                    FormMain.Alert("No realm list found!", NotificationType.Error);
                }
            }
            //AzerothCore
            if (Settings.Default.SelectedCore == 1)
            {
                if (table.Rows.Count > 0)
                {
                    DatabaseSources.RealmID = table.Rows[0][0].ToString();
                    DatabaseSources.RealmName = table.Rows[0][1].ToString();
                    DatabaseSources.RealmAddress = table.Rows[0][2].ToString();
                    DatabaseSources.RealmLocalAddress = table.Rows[0][3].ToString();
                    DatabaseSources.RealmSubMask = table.Rows[0][4].ToString();
                    DatabaseSources.RealmPort = table.Rows[0][5].ToString();
                    DatabaseSources.RealmTimeZone = table.Rows[0][8].ToString();
                    DatabaseSources.GameBuild = table.Rows[0][11].ToString();
                    DatabaseSources.GameRegion = null;
                }
                else
                {
                    FormMain.Alert("No realm list found!", NotificationType.Error);
                }
            }
            //CypherCore, TrinityCore, TrinityCore 4.3.4(TCPP)
            else if (Settings.Default.SelectedCore == 3 | Settings.Default.SelectedCore == 4 | Settings.Default.SelectedCore == 5)
            {
                if (table.Rows.Count > 0)
                {
                    DatabaseSources.RealmID = table.Rows[0][0].ToString();
                    DatabaseSources.RealmName = table.Rows[0][1].ToString();
                    DatabaseSources.RealmAddress = table.Rows[0][2].ToString();
                    DatabaseSources.RealmLocalAddress = table.Rows[0][3].ToString();
                    DatabaseSources.RealmSubMask = table.Rows[0][4].ToString();
                    DatabaseSources.RealmPort = table.Rows[0][5].ToString();
                    DatabaseSources.RealmTimeZone = table.Rows[0][8].ToString();
                    DatabaseSources.GameBuild = table.Rows[0][11].ToString();
                    DatabaseSources.GameRegion = table.Rows[0][12].ToString();
                }
                else
                {
                    FormMain.Alert("No realm list found!", NotificationType.Error);
                }
            }
            //cMaNGOS
            else if (Settings.Default.SelectedCore == 2)
            {
                if (table.Rows.Count > 0)
                {
                    DatabaseSources.RealmID = table.Rows[0][0].ToString();
                    DatabaseSources.RealmName = table.Rows[0][1].ToString();
                    DatabaseSources.RealmAddress = table.Rows[0][2].ToString();
                    DatabaseSources.RealmLocalAddress = null;
                    DatabaseSources.RealmSubMask = null;
                    DatabaseSources.RealmPort = table.Rows[0][3].ToString();
                    DatabaseSources.RealmTimeZone = table.Rows[0][6].ToString();
                    DatabaseSources.GameBuild = table.Rows[0][9].ToString();
                    DatabaseSources.GameRegion = null;
                }
                else
                {
                    FormMain.Alert("No realm list found!", NotificationType.Error);
                }
            }
            //Vanilla MaNGOS
            else if (Settings.Default.SelectedCore == 6)
            {
                if (table.Rows.Count > 0)
                {
                    DatabaseSources.RealmID = table.Rows[0][0].ToString();
                    DatabaseSources.RealmName = table.Rows[0][1].ToString();
                    DatabaseSources.RealmAddress = table.Rows[0][2].ToString();
                    DatabaseSources.RealmLocalAddress = table.Rows[0][3].ToString();
                    DatabaseSources.RealmSubMask = table.Rows[0][4].ToString();
                    DatabaseSources.RealmPort = table.Rows[0][5].ToString();
                    DatabaseSources.RealmTimeZone = table.Rows[0][8].ToString();
                    DatabaseSources.GameBuild = table.Rows[0][11].ToString();
                    DatabaseSources.GameRegion = table.Rows[0][12].ToString();
                }
                else
                {
                    FormMain.Alert("No realm list found!", NotificationType.Error);
                }
            }
        }
        public static void SaveRealmList()
        {
            DatabaseConnection databaseConnection = new();
            //AscEmu
            if (Settings.Default.SelectedCore == 0)
            {
                MySqlCommand command = new MySqlCommand($@"UPDATE `realms` SET `password` = '{DatabaseSources.RealmName}' WHERE `id` = `{DatabaseSources.RealmID}`;", databaseConnection.GetConnection);
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
                MySqlCommand command = new MySqlCommand($@"UPDATE `realmlist` SET `name` = '{DatabaseSources.RealmName}', `address` = '{DatabaseSources.RealmAddress}', `localAddress` = '{DatabaseSources.RealmLocalAddress}', `localSubnetMask` = '{DatabaseSources.RealmSubMask}', `port` = {DatabaseSources.RealmPort}, `icon` = 0, `flag` = 2, `timezone` = {DatabaseSources.RealmTimeZone}, `gamebuild` = {DatabaseSources.GameBuild} WHERE `id` = {DatabaseSources.RealmID};", databaseConnection.GetConnection);
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
                MySqlCommand command = new MySqlCommand($@"UPDATE `realmlist` SET `name` = '{DatabaseSources.RealmName}', `address` = '{DatabaseSources.RealmAddress}', `localAddress` = '{DatabaseSources.RealmLocalAddress}', `localSubnetMask` = '{DatabaseSources.RealmSubMask}', `port` = {DatabaseSources.RealmPort}, `timezone` = {DatabaseSources.RealmTimeZone}, `gamebuild` = {DatabaseSources.GameBuild}, `Region` = {DatabaseSources.GameRegion} WHERE `id` = {DatabaseSources.RealmID};", databaseConnection.GetConnection);
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
                MySqlCommand command = new MySqlCommand($@"UPDATE `realmlist` SET `name` = '{DatabaseSources.RealmName}', `address` = '{DatabaseSources.RealmAddress}', `port` = {DatabaseSources.RealmPort}, `timezone` = {DatabaseSources.RealmTimeZone}, `realmbuilds` = '{DatabaseSources.GameBuild}' WHERE `id` = {DatabaseSources.RealmID};", databaseConnection.GetConnection);
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
                MySqlCommand command = new MySqlCommand($@"UPDATE `realmlist` SET `name` = '{DatabaseSources.RealmName}', `address` = '{DatabaseSources.RealmAddress}', `localAddress` = '{DatabaseSources.RealmLocalAddress}', `localSubnetMask` = '{DatabaseSources.RealmSubMask}', `port` = {DatabaseSources.RealmPort},`timezone` = {DatabaseSources.RealmTimeZone}, `gamebuild_min` = {DatabaseSources.GameBuild}, `gamebuild_max` = {DatabaseSources.GameRegion} WHERE `id` = {DatabaseSources.RealmID};", databaseConnection.GetConnection);
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
        public bool NewUser(string email, string password, bool withGameAccount = true, string gameAccountName = "")
        {
            if (email.Length > 320)
                FormMain.Alert("Email is too long!", NotificationType.Info);

            if (password.Length > 16)
                FormMain.Alert("Password is too long!", NotificationType.Info);

            if (GetId(email) != 0)
                FormMain.Alert("Email exists!", NotificationType.Info);
            return false;
        }
        public static uint GetId(string username)
        {
            DatabaseConnection databaseConnection = new();
            MySqlCommand command = new MySqlCommand("SELECT id FROM account WHERE username = @username", databaseConnection.GetConnection);
            command.Parameters.Add("@username", MySqlDbType.Text).Value = username;
            MySqlDataAdapter _dataAdapter = new(command);
            DataTable table = new();
            _dataAdapter.Fill(table);
            databaseConnection.GetConnection.Close();
            if (table.Rows.Count > 0)
            {
                return 1;
            }
            else
            { 
                return 0;
            }  
        }
        //public string CalculateShaPassHash(string name, string password)
        //{
        //    SHA256 sha256 = SHA256.Create();
        //    var i = sha256.ComputeHash(Encoding.UTF8.GetBytes(name));
        //    return sha256.ComputeHash(Encoding.UTF8.GetBytes(i.ToHexString() + ":" + password)).ToHexString(true);
        //}
    }

    internal class DatabaseSources
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
