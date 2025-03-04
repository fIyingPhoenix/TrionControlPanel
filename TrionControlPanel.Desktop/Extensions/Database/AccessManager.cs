using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    public class AccessManager
    {
        // Build a MySQL connection string using provided settings and database name
        public static string ConnectionString(AppSettings Settings, string Database)
        {
            return new($"Server={Settings.DBServerHost};Port={Settings.DBServerPort};User Id={Settings.DBServerUser};Password={Settings.DBServerPassword};Database={Database}");
        }

        // Test the connection to the database asynchronously
        public static async Task<bool> ConnectionTest(AppSettings Settings, string Database)
        {
            MySqlConnection conn = new(ConnectionString(Settings, Database));
            bool status = false;

            // Use a Task to test the connection asynchronously
            await Task.Run(() =>
            {
                try
                {
                    // Open connection if it's closed
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        status = true;
                        conn.Close();
                    }
                }
                catch (Exception)
                {
                    // Log failure and keep status as false
                    status = false;
                }
                return status;
            });

            return status;
        }

        // Load a list of data asynchronously from the database using the provided SQL and parameters
        public static async Task<List<T>> LoadDataList<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection con = new MySqlConnection(connectionString))
            {
                // Execute the SQL query and return the result as a list
                var rows = await con.QueryAsync<T>(sql, parameters);
                return rows.ToList();
            }
        }

        // Load a single data record asynchronously from the database using the provided SQL and parameters
        public static async Task<T> LoadDataType<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connectionNoList = new MySqlConnection(connectionString))
            {
                // Execute the SQL query and return the result as a single object
                var rows = await connectionNoList.QuerySingleAsync<T>(sql, parameters);
                return rows;
            }
        }

        // Save data asynchronously to the database using the provided SQL query and parameters
        public static async Task SaveData<T>(string sql, T parameters, string connectionString)
        {
            // Validate connection string
            if (string.IsNullOrEmpty(connectionString))
            {
                TrionLogger.Log("Connection string cannot be null or empty.", "ERROR");
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
            }

            // Validate SQL query
            if (sql == null)
            {
                TrionLogger.Log("SQL query cannot be null", "ERROR");
                throw new ArgumentNullException(nameof(sql), "SQL query cannot be null.");
            }

            // Validate parameters
            if (parameters == null)
            {
                TrionLogger.Log("Parameters cannot be null.", "ERROR");
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
                    // Log error and rethrow exception
                    TrionLogger.Log($"Error occurred: {ex.Message}", "ERROR");
                    throw;
                }
            }
        }
    }
}
