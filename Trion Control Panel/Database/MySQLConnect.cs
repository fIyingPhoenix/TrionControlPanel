using System.Data;
using MySql.Data.MySqlClient;
using TrionControlPanel.Alerts;
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
                    FormAlert.ShowAlert($"The SQL Connection is {MySqlCore.State}", NotificationType.Success);
                    MySqlCore.Close();
                }
            }
            catch (Exception ex)
            {
                FormAlert.ShowAlert(ex.Message, NotificationType.Error);
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
                FormAlert.ShowAlert(ex.Message, NotificationType.Error);
            }
        }
    }
}
