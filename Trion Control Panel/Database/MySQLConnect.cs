using System.Data;
using MySql.Data.MySqlClient;
using TrionControlPanel.Settings;
using TrionControlPanel.Alerts;

namespace TrionControlPanel.Database
{
    internal class MySQLConnect
    {
        AlertBox alertBox = new();
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
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
            }
            catch (Exception ex)
            {
                alertBox.ShowAlert(ex.Message, NotificationType.Error);
            }
        }
        public void Close()
        {
            try
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                alertBox.ShowAlert(ex.Message, NotificationType.Error);
            }
        }
        internal void MySqlConnectCheck()
        {
            Thread MySQLExecute = new(() =>
            {
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                        alertBox.ShowAlert($"The SQL Connection is {Connection.State}", NotificationType.Success);
                        Connection.Close();
                    }
                }
                catch (Exception MySqlConnect)
                {
                    alertBox.ShowAlert(MySqlConnect.Message, NotificationType.Error);
                }
            });
            MySQLExecute.Start();
        }
  
    }
}
