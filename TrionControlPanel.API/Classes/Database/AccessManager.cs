using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace TrionControlPanel.API.Classes.Database
{
    public class AccessManager
    {
        private readonly string _connectionString;

        public AccessManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default")!;
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<List<T>> LoadDataList<T, U>(string sql, U parameters)
        {
            using (IDbConnection con = CreateConnection())
            {
                return (await con.QueryAsync<T>(sql, parameters)).ToList();
            }
        }

        public async Task<T> LoadDataType<T, U>(string sql, U parameters)
        {
            using (IDbConnection con = CreateConnection())
            {
                return await con.QuerySingleAsync<T>(sql, parameters);
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), "SQL query cannot be null or empty.");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters), "Parameters cannot be null.");
            }

            using (IDbConnection con = CreateConnection())
            {
                await con.ExecuteAsync(sql, parameters);
            }
        }
    }
}
