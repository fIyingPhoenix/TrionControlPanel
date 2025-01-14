
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    public class AccessMenager
    {
        public static string ConnectionString(AppSettings Settings, string Database)
        {
            return new($"Server={Settings.DBServerHost};Port={Settings.DBServerPort};User Id={Settings.DBServerUser};Password={Settings.DBServerPassword};Database={Database}");
        }
        public static async Task<bool> ConnectionTest(AppSettings Settings, string Database)
        {
            MySqlConnection conn = new(ConnectionString(Settings, Database));
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
        public static async Task<List<T>> LodaDataList<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection con = new MySqlConnection(connectionString))
            {
                var rows = await con.QueryAsync<T>(sql, parameters);
                return rows.ToList();
            }
        }
        public static async Task<T> LoadDataType<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connectionNoList = new MySqlConnection(connectionString))
            {
                var rows = await connectionNoList.QuerySingleAsync<T>(sql, parameters);
                return rows;
            }
        }
        public static async Task SaveData<T>(string sql, T parameters, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
            }

            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql), "SQL query cannot be null.");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters), "Parameters cannot be null.");
            }

            // Create the connection
            using (IDbConnection connectionSave = new MySqlConnection(connectionString))
            {
                try
                {
                    // Execute the query asynchronously using Dapper
                    await connectionSave.ExecuteAsync(sql, parameters);

                }
                catch (Exception ex)
                {
                    // Handle any exception that occurs during connection or execution
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    throw;
                }
                finally
                {

                }
            }
        }
    }
}
