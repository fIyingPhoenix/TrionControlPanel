using Dapper;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TrionLibrary
    public  class MySQLDataAccess
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
