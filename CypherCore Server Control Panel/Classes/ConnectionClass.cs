using CypherCore_Server_Laucher.Forms;
using CypherCoreServerLaucher.Properties;
using MySql.Data.MySqlClient;
using System.Data;

namespace CypherCore_Server_Laucher.Classes
{
    internal class ConnectionClass
    {
        private static string host = Settings.Default.MySQLServerName;
        private static string port = Settings.Default.MySQLServerPort;
        private static string username = Settings.Default.MySQLServerUsername;
        private static string password = Settings.Default.MySQLServerPassword;
        private static string database = Settings.Default.AuthDatabaseName;
        private static MySqlConnection MySqlCore = new MySqlConnection($"server={host};database={database};port={port};uid={username};pwd={password};");
        public void Alert(string message, NotificationType eType)
        {
            //make the laert work.
            FormAlert frm = new FormAlert(); //dont change this. its fix the Cannot access a disposed object and scall the notification up.
            frm.ShowAlert(message, eType);
        }
        internal MySqlConnection GetConnection
        {
            get
            {
                return MySqlCore;
            }
        }
        internal void MySqlConnect()
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
                Alert(ex.Message,NotificationType.Error);
                //MessageBox.Show("Error connect to the server!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal void MySqlDisconnect()
        {
            try
            {
                if (MySqlCore.State == ConnectionState.Open)
                {
                    MySqlCore.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error disconnect from the server!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal bool MySQLConnected()
        {
            if (MySqlCore.State == ConnectionState.Open)
            {
                return true;
            }
            else if (MySqlCore.State == ConnectionState.Closed)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
