using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using TrionControlPanel.Alerts;
using TrionControlPanel.Classes;
using TrionControlPanel.Properties;

namespace TrionControlPanel.Database
{
    internal class MySQLConnect
    {
        private readonly static MySqlConnection MySqlCore = new($"Server={Settings.Default.MySQLServerHost};Port={Settings.Default.MySQLServerPort};User Id={Settings.Default.MySQLServerUsername};Password={Settings.Default.MySQLServerPassword};Database={Settings.Default.AuthDatabaseName}");
        internal MySqlConnection GetConnection
        {
            get
            {
                return MySqlCore;
            }
        }
        internal static void MySqlConnect()
        {
            if (MySqlCore.State == ConnectionState.Closed)
            {
                MySqlCore.Open();
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
    internal class AccountMenager
    {
        public static bool CheckForUsername(string username)
        {
            string sqlCommand = "SELECT id FROM account WHERE username = @username";
            MySQLConnect databaseConnection = new();
            MySqlCommand command = new(sqlCommand, databaseConnection.GetConnection);
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
}
