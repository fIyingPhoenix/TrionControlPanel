using TrionControlPanel.Properties;
using TrionControlPanel.Classes;
using MySql.Data.MySqlClient;
using System.Data;
using TrionControlPanel.TabsComponents;

namespace TrionControlPanel.Database
{
    internal class DatabaseConnection
    {
        private  MySqlConnection MySqlCore = new ($"Server={Settings.Default.MySQLServerHost};Port={Settings.Default.MySQLServerPort};User Id={Settings.Default.MySQLServerUsername};Password={Settings.Default.MySQLServerPassword};Database={Settings.Default.AuthDatabaseName}");
        internal MySqlConnection GetConnection
        {
            get
            {
                return MySqlCore;
            }
        }
        internal void MySqlConnectCheck()
        {
            try
            {
                if (MySqlCore.State == ConnectionState.Closed)
                {
                    MySqlCore.Open();
                    Settings.Default.MySQLConnect = true;
                    Settings.Default.MySQLConnectFaild = false;
                    Settings.Default.Save();
                    HomeControl.Alert($"The SQL Connection is {MySqlCore.State.ToString()}", NotificationType.Success);
                    MySqlCore.Close();
                }
            }
            catch (Exception ex)
            {
                HomeControl.Alert(ex.Message, NotificationType.Error);
                Settings.Default.MySQLConnectFaild = true;
                Settings.Default.Save();
            }
        }
        internal void MySqlDisconnectCheck()
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
                HomeControl.Alert(ex.Message, NotificationType.Error);
            }
        }
    }
    public class AccountManager 
    {
        DatabaseConnection databaseConnection = new();
        public bool NewUser(string email, string password, bool withGameAccount = true, string gameAccountName = "")
        {
            if (email.Length > 320)
                HomeControl.Alert("Email is too long!", NotificationType.Info);

            if (password.Length > 16)
                HomeControl.Alert("Password is too long!", NotificationType.Info);

            if (GetId(email) != 0)
                HomeControl.Alert("Email exists!", NotificationType.Info);
            return false;
        }
        public uint GetId(string username)
        {
            MySqlCommand command = new MySqlCommand("SELECT id FROM account WHERE username = @username", databaseConnection.GetConnection);
            command.Parameters.Add("@username", MySqlDbType.Text).Value = username;
            MySqlDataAdapter _dataAdapter = new(command);
            DataTable table = new();
            _dataAdapter.Fill(table);
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

}
