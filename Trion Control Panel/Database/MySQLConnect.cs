using System.Data;
using MySql.Data.MySqlClient;
using TrionControlPanel.Settings;
using TrionControlPanel.Alerts;

namespace TrionControlPanel.Database
{
    internal class MySQLConnect
    {

        public static string ConnectionString(string host, string port, string user, string password, string database)
        {
           return new($"Server={host};Port={port};User Id={user};Password={password};Database={database}");
        }
        private readonly MySqlConnection Connection = new(ConnectionString(new(Data._MySQLServerHost), new(Data._MySQLServerPort), new(Data._MySQLServerUser), new(Data._MySQLServerPassword), new(Data._AuthDatabase)));
        internal MySqlConnection GetConnection
        {
            get
            {
                return Connection;
            }
        }
        internal void Open()
        {
            
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
        public void Close()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }
        internal bool MySqlConnectCheck()
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                    FormAlert.ShowAlert($"The SQL Connection is {Connection.State}", NotificationType.Info);
                    Connection.Close();
                    return true;
                }
            }
            catch (Exception MySqlConnect)
            {
                FormAlert.ShowAlert(MySqlConnect.Message, NotificationType.Error);
                return false;
                
            }
            return true;
        }
  
    }
}
