
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    // AccessManager class for handling database operations.
    public class AccessManager
    {
        static void ExecuteSqlFile(string filePath, string connectionString)
        {
            long totalBytes = new FileInfo(filePath).Length;
            long bytesRead = 0;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    StringBuilder batch = new StringBuilder();
                    string line;
                    int lastReportedProgress = 0;

                    while ((line = reader.ReadLine()!) != null)
                    {
                        bytesRead += Encoding.UTF8.GetByteCount(line) + 2; // +2 for newline characters

                        if (line.Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                        {
                            if (batch.Length > 0)
                            {
                                connection.Execute(batch.ToString());  // Execute SQL batch
                                batch.Clear();
                            }
                        }
                        else
                        {
                            batch.AppendLine(line);
                        }

                        // Report progress every 5%
                        int progress = (int)((bytesRead * 100) / totalBytes);
                        if (progress >= lastReportedProgress + 5)
                        {
                            Console.WriteLine($"Progress: {progress}%");
                            lastReportedProgress = progress;
                        }
                    }

                    // Execute the remaining SQL statements
                    if (batch.Length > 0)
                    {
                        connection.Execute(batch.ToString());
                    }
                }
            }

            Console.WriteLine("Execution completed!");
        }
        // Constructs a connection string for the specified database using the provided settings.
        public static string ConnectionString(AppSettings Settings, string Database)
        {
            return new($"Server={Settings.DBServerHost};Port={Settings.DBServerPort};User Id={Settings.DBServerUser};Password={Settings.DBServerPassword};Database={Database}");
        }

        // Tests the connection to the specified database using the provided settings.
        public static async Task<bool> ConnectionTest(AppSettings Settings, string Database)
        {
            using (MySqlConnection conn = new(ConnectionString(Settings, Database)))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    TrionLogger.Log($"Connection test failed: {ex.Message}", "ERROR");
                    return false;
                }
            }
            return false;
        }

        // Loads a list of data from the database using the specified SQL query and parameters.
        public static async Task<List<T>> LodaDataList<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection con = new MySqlConnection(connectionString))
            {
                var rows = await con.QueryAsync<T>(sql, parameters);
                return rows.ToList();
            }
        }

        // Loads a single data item from the database using the specified SQL query and parameters.
        public static async Task<T> LoadDataType<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connectionNoList = new MySqlConnection(connectionString))
            {
                var rows = await connectionNoList.QuerySingleAsync<T>(sql, parameters);
                return rows;
            }
        }

        // Saves data to the database using the specified SQL query and parameters.
        public static async Task SaveData<T>(string sql, T parameters, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                TrionLogger.Log("Connection string cannot be null or empty.", "ERROR");
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
            }

            if (sql == null)
            {
                TrionLogger.Log("SQL query cannot be null", "ERROR");
                throw new ArgumentNullException(nameof(sql), "SQL query cannot be null.");
            }

            if (parameters == null)
            {
                TrionLogger.Log("Parameters cannot be null.", "ERROR");
                throw new ArgumentNullException(nameof(parameters), "Parameters cannot be null.");
            }

            using (IDbConnection connectionSave = new MySqlConnection(connectionString))
            {
                try
                {
                    await connectionSave.ExecuteAsync(sql, parameters);
                }
                catch (Exception ex)
                {
                    TrionLogger.Log($"Error occurred: {ex.Message}", "ERROR");
                    throw;
                }
            }
        }
    }
}
