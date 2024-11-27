using Dapper;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
namespace TrionControlPanel.Classes.Connection
{
    public class DataAccess 
    {
        public async Task<List<T>> LoadData<T, U> (string sql, U Parameters, string connectionString)
        {
            using(IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(sql, Parameters);
                return rows.ToList();
            }
        }
        public Task SaveData<U>(string sql, U Parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                return connection.ExecuteAsync(sql, Parameters);
            }
        }
    }
}
