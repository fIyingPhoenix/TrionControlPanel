using CypherCoreServerLaucher.Properties;
using CypherCore_Server_Laucher.Classes;
using CypherCore_Server_Laucher.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using CypherCore_Server_Laucher.TabsComponents;

namespace CypherCoreServerLaucher.Database
{
    internal class DatabaseConnection
    {
        private static  MySqlConnection MySqlCore = new MySqlConnection($"Server={Settings.Default.MySQLServerHost};Port={Settings.Default.MySQLServerPort};User Id={Settings.Default.MySQLServerUsername};Password={Settings.Default.MySQLServerPassword};Database={Settings.Default.AuthDatabaseName}");

        internal MySqlConnection GetConnection
        {
            get
            {
                return MySqlCore;
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
                HomeControl.Alert(ex.Message, NotificationType.Error);
            }
        }
    }
    internal class AccountManager 
    {
    

    }
}
