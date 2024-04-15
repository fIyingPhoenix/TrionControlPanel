using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using TrionLibrary;

namespace TrionDatabase
{
    public class SQLDataConnect
    {
        public static string ConnectionString(string Database)
        {
            return new($"Server={Data.Settings.MySQLServerHost};Port={Data.Settings.MySQLServerPort};User Id={Data.Settings.MySQLServerUser};Password={Data.Settings.MySQLServerPassword};Database={Database}");
        }
    }
    public class SQLDataAccess
    {
        public static async Task<List<T>> LodaData<T,U> (string sql ,U parameters, string connectionString)
        {
            using (IDbConnection con = new MySqlConnection (connectionString))
            {
                var rows = await con.QueryAsync<T>(sql, parameters);
                return rows.ToList();
            }
        }
        public static Task SaveData<T> (string sql,T parameters,string connectionString)
        {
            using (IDbConnection con = new MySqlConnection(connectionString))
            {
                return con.ExecuteAsync(sql, parameters);
            }
        
        }
    }
}
