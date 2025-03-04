using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace TrionControlPanel.API.Classes.Database
{
    public class AccessManager
    {
        private readonly string _connectionString;

        // Constructor to initialize connection string from configuration
        public AccessManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default")!;
        }

        // Helper method to create and return a new MySqlConnection instance
        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // Method to execute a SQL query that returns a list of results
        public async Task<List<T>> LoadDataList<T, U>(string sql, U parameters)
        {
            try
            {
                using (IDbConnection con = CreateConnection())
                {
                    // Log query execution
                    TrionLogger.Log($"Executing LoadDataList with SQL: {sql} and parameters: {parameters}", "INFO");
                    return (await con.QueryAsync<T>(sql, parameters)).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log and throw a custom exception for easier debugging
                TrionLogger.Log($"Error executing LoadDataList with SQL: {sql} and parameters: {parameters}. Exception: {ex.Message}", "ERROR");
                throw new ApplicationException("An error occurred while fetching data from the database.", ex);
            }
        }

        // Method to execute a SQL query that returns a single result
        public async Task<T> LoadDataType<T, U>(string sql, U parameters)
        {
            try
            {
                using (IDbConnection con = CreateConnection())
                {
                    // Log query execution
                    TrionLogger.Log($"Executing LoadDataType with SQL: {sql} and parameters: {parameters}", "INFO");
                    return await con.QuerySingleAsync<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                // Log and throw a custom exception for easier debugging
                TrionLogger.Log($"Error executing LoadDataType with SQL: {sql} and parameters: {parameters}. Exception: {ex.Message}", "ERROR");
                throw new ApplicationException("An error occurred while fetching data from the database.", ex);
            }
        }

        // Method to execute a SQL query to save data
        public async Task SaveData<T>(string sql, T parameters)
        {
            try
            {
                // Validate input parameters for SQL and parameters
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
                    // Log query execution
                    TrionLogger.Log($"Executing SaveData with SQL: {sql} and parameters: {parameters}", "INFO");
                    await con.ExecuteAsync(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                // Log and throw a custom exception for easier debugging
                TrionLogger.Log($"Error executing SaveData with SQL: {sql} and parameters: {parameters}. Exception: {ex.Message}", "ERROR");
                throw new ApplicationException("An error occurred while saving data to the database.", ex);
            }
        }
    }
}
