using MySql.Data.MySqlClient;
using System.Data;

namespace CypherCore_Server_Laucher.Classes
{
    internal class ConnectionClass
    {
        private static  MySqlConnection MySqlCore = new MySqlConnection("server=localhost;database=auth;port=3306;uid=root;pwd=root;");

        

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
                MessageBox.Show (ex.Message);
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
