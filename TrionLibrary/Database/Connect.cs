using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using TrionLibrary.Sys;


namespace TrionLibrary.Database
{
    public class Connect
    {
        public static async Task<bool> Test()
        {
            MySqlConnection conn = new(String(Setting.Setting.List.AuthDatabase));
            bool status = false;
            await Task.Run(() =>
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        status = true;
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    status = false;
                }
                return status;
            });
            return status;
        }
        public static string String(string Database)
        {
            return new($"Server={Setting.Setting.List.DBServerHost};Port={Setting.Setting.List.DBServerPort};User Id={Setting.Setting.List.DBServerUser};Password={Setting.Setting.List.DBServerPassword};Database={Database}");
        }
    }
}
