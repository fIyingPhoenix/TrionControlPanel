// =============================================================================
// AccessManager.cs
// Purpose: Database access layer providing MySQL connection and query operations
// Used by: AccountManager, RealmListManager, SPP Installer
// Steps 6, 11, 14 of IMPROVEMENTS.md - XML Docs, Comments, Enhanced Logging
// =============================================================================

using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.Text;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    /// <summary>
    /// Provides database access functionality for MySQL operations.
    /// Handles connections, queries, and SQL script execution using Dapper.
    /// </summary>
    /// <remarks>
    /// This static class is the primary data access layer for the application.
    /// It uses Dapper for efficient ORM-style operations with raw SQL.
    ///
    /// Key features:
    /// - Connection string generation from AppSettings
    /// - Connection testing for database health checks
    /// - Generic methods for loading and saving data
    /// - Large SQL file execution with progress reporting
    ///
    /// Example usage:
    /// <code>
    /// // Test connection
    /// bool connected = await AccessManager.ConnectionTest(settings, "auth");
    ///
    /// // Load data
    /// var accounts = await AccessManager.LodaDataList&lt;Account, dynamic&gt;(
    ///     "SELECT * FROM account WHERE id = @Id",
    ///     new { Id = 1 },
    ///     connectionString);
    /// </code>
    /// </remarks>
    public class AccessManager
    {
        #region Constants
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>Buffer size for reading SQL files.</summary>
        private const int FileStreamBufferSize = 8192;

        /// <summary>Maximum length of SQL text shown in debug logs.</summary>
        private const int SqlLogPreviewLength = 100;

        /// <summary>Minimum interval in milliseconds between progress reports.</summary>
        private const int ProgressReportIntervalMs = 250;

        /// <summary>Number of bytes in one megabyte (for speed calculations).</summary>
        private const double BytesPerMB = 1024.0 * 1024.0;

        /// <summary>Milliseconds per second (for speed calculations).</summary>
        private const double MsPerSecond = 1000.0;

        #endregion

        #region SQL Script Execution
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Asynchronously executes a large SQL script file against a MySQL database,
        /// with batching and detailed progress updates.
        /// </summary>
        /// <param name="filePath">Full path to the SQL script file.</param>
        /// <param name="connectionString">MySQL connection string.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <param name="progress">
        /// Optional progress reporter for completion percentage (0-100).
        /// </param>
        /// <param name="elapsedTime">
        /// Optional progress reporter for elapsed time in seconds.
        /// </param>
        /// <param name="speed">
        /// Optional progress reporter for processing speed in MB/s.
        /// </param>
        /// <returns>A task that completes when the script has been executed.</returns>
        /// <remarks>
        /// This method is designed for importing large database dumps efficiently.
        /// It reads the file line by line and executes statements as they complete
        /// (when a semicolon delimiter is encountered).
        ///
        /// Progress is reported every 250ms to avoid UI performance issues.
        /// The method handles cancellation gracefully and logs all errors.
        /// </remarks>
        /// <example>
        /// <code>
        /// var progress = new Progress&lt;double&gt;(p => progressBar.Value = (int)p);
        /// await AccessManager.ExecuteSqlFileAsync(
        ///     "world_database.sql",
        ///     connectionString,
        ///     CancellationToken.None,
        ///     progress);
        /// </code>
        /// </example>
        public static async Task ExecuteSqlFileAsync(
            string filePath,
            string connectionString,
            CancellationToken cancellationToken,
            IProgress<double>? progress = null,
            IProgress<double>? elapsedTime = null,
            IProgress<double>? speed = null)
        {
            await Task.Run(async () =>
            {
                var fileInfo = new FileInfo(filePath);
                if (!fileInfo.Exists)
                {
                    TrionLogger.Log($"SQL file not found: {filePath}", "ERROR");
                    return;
                }

                long totalBytes = fileInfo.Length;
                long totalBytesRead = 0;
                long previousBytesRead = 0;
                var startTime = DateTime.Now;
                var stopwatch = Stopwatch.StartNew();

                try
                {
                    await using var connection = new MySqlConnection(connectionString);
                    await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                    await using var fileStream = new FileStream(
                        filePath,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read,
                        bufferSize: FileStreamBufferSize,
                        useAsync: true);
                    using var reader = new StreamReader(fileStream, Encoding.UTF8);

                    var commandStringBuilder = new StringBuilder();
                    string? line;

                    while ((line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false)) != null)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var trimmedLine = line.Trim();
                        commandStringBuilder.AppendLine(line);

                        // Execute when statement is complete (ends with semicolon)
                        if (trimmedLine.EndsWith(';'))
                        {
                            string commandText = commandStringBuilder.ToString();
                            commandStringBuilder.Clear();

                            if (!string.IsNullOrWhiteSpace(commandText))
                            {
                                TrionLogger.Log(
                                    $"Executing SQL: {commandText[..Math.Min(commandText.Length, SqlLogPreviewLength)]}...",
                                    "DEBUG");

                                await connection.ExecuteAsync(
                                    new CommandDefinition(commandText, cancellationToken: cancellationToken)).ConfigureAwait(false);
                            }
                        }

                        // Report progress at regular intervals (every 250ms)
                        if (stopwatch.ElapsedMilliseconds >= ProgressReportIntervalMs)
                        {
                            totalBytesRead = fileStream.Position;
                            double progressValue = totalBytes > 0
                                ? (double)totalBytesRead / totalBytes * 100
                                : 0;
                            double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;
                            double speedValue = elapsedSeconds > 0
                                ? (totalBytesRead - previousBytesRead) / BytesPerMB /
                                  (stopwatch.ElapsedMilliseconds / MsPerSecond)
                                : 0;

                            progress?.Report(progressValue);
                            elapsedTime?.Report(elapsedSeconds);
                            speed?.Report(speedValue);

                            previousBytesRead = totalBytesRead;
                            stopwatch.Restart();
                        }
                    }

                    // Execute any remaining command in the buffer
                    if (commandStringBuilder.Length > 0)
                    {
                        await connection.ExecuteAsync(
                            new CommandDefinition(commandStringBuilder.ToString(), cancellationToken: cancellationToken)).ConfigureAwait(false);
                    }

                    // Final progress report
                    progress?.Report(100.0);
                    elapsedTime?.Report((DateTime.Now - startTime).TotalSeconds);
                    TrionLogger.Log($"SQL script execution completed successfully for {fileInfo.Name}.", "INFO");
                }
                catch (OperationCanceledException)
                {
                    TrionLogger.Warning("SQL script execution was canceled.");
                }
                catch (MySqlException ex)
                {
                    // Use enhanced logging with full exception details (Step 14)
                    TrionLogger.LogException(ex, "ExecuteSqlFileAsync");
                    TrionLogger.LogDatabaseOperation("ExecuteScript", fileInfo.Name, false, additionalInfo: ex.Message);
                }
                catch (IOException ex)
                {
                    // Use enhanced logging with full exception details (Step 14)
                    TrionLogger.LogException(ex, "ExecuteSqlFileAsync");
                }
                catch (Exception ex)
                {
                    // Use enhanced logging with full exception details (Step 14)
                    TrionLogger.LogException(ex, "ExecuteSqlFileAsync");
                }
            });
        }

        #endregion

        #region Connection Management
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Constructs a MySQL connection string without a specific database.
        /// </summary>
        /// <param name="Settings">Application settings containing database credentials.</param>
        /// <returns>A connection string for MySQL server-level operations.</returns>
        /// <remarks>
        /// Use this overload for operations that don't target a specific database,
        /// such as listing databases or creating new databases.
        /// </remarks>
        public static string ConnectionString(AppSettings Settings)
        {
            return $"Server={Settings.DBServerHost};Port={Settings.DBServerPort};User Id={Settings.DBServerUser};Password={Settings.DBServerPassword};";
        }

        /// <summary>
        /// Constructs a MySQL connection string for a specific database.
        /// </summary>
        /// <param name="Settings">Application settings containing database credentials.</param>
        /// <param name="Database">The name of the database to connect to.</param>
        /// <returns>A connection string targeting the specified database.</returns>
        /// <example>
        /// <code>
        /// string connStr = AccessManager.ConnectionString(settings, "wotlk_auth");
        /// </code>
        /// </example>
        public static string ConnectionString(AppSettings Settings, string Database)
        {
            return $"Server={Settings.DBServerHost};Port={Settings.DBServerPort};User Id={Settings.DBServerUser};Password={Settings.DBServerPassword};Database={Database}";
        }

        /// <summary>
        /// Tests the connection to a specific MySQL database.
        /// </summary>
        /// <param name="Settings">Application settings containing database credentials.</param>
        /// <param name="Database">The name of the database to test.</param>
        /// <returns>True if connection was successful, false otherwise.</returns>
        /// <remarks>
        /// This method attempts to open a connection to the database and
        /// immediately closes it. Any errors are logged and result in false.
        /// </remarks>
        /// <example>
        /// <code>
        /// bool isConnected = await AccessManager.ConnectionTest(settings, "wotlk_auth");
        /// if (isConnected)
        /// {
        ///     // Database is accessible
        /// }
        /// </code>
        /// </example>
        public static async Task<bool> ConnectionTest(AppSettings Settings, string Database)
        {
            using MySqlConnection conn = new(ConnectionString(Settings, Database));
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    return true;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Connection test failed: {ex.Message}", "ERROR");
                return false;
            }

            return false;
        }

        #endregion

        #region Data Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Loads a list of items from the database using a parameterized SQL query.
        /// </summary>
        /// <typeparam name="T">The type to map results to.</typeparam>
        /// <typeparam name="U">The type of the parameters object.</typeparam>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="parameters">
        /// Anonymous object or strongly-typed object containing query parameters.
        /// </param>
        /// <param name="connectionString">MySQL connection string.</param>
        /// <returns>A list of items of type T.</returns>
        /// <remarks>
        /// Uses Dapper for efficient object mapping. Parameters should match
        /// the @parameter placeholders in the SQL query.
        /// </remarks>
        /// <example>
        /// <code>
        /// var accounts = await AccessManager.LodaDataList&lt;Account, dynamic&gt;(
        ///     "SELECT * FROM account WHERE expansion >= @MinExpansion",
        ///     new { MinExpansion = 2 },
        ///     connectionString);
        /// </code>
        /// </example>
        public static async Task<List<T>> LodaDataList<T, U>(string sql, U parameters, string connectionString)
        {
            using IDbConnection con = new MySqlConnection(connectionString);
            var rows = await con.QueryAsync<T>(sql, parameters).ConfigureAwait(false);
            return rows.ToList();
        }

        /// <summary>
        /// Loads a single item from the database using a parameterized SQL query.
        /// </summary>
        /// <typeparam name="T">The type to map the result to.</typeparam>
        /// <typeparam name="U">The type of the parameters object.</typeparam>
        /// <param name="sql">The SQL query to execute (should return exactly one row).</param>
        /// <param name="parameters">
        /// Anonymous object or strongly-typed object containing query parameters.
        /// </param>
        /// <param name="connectionString">MySQL connection string.</param>
        /// <returns>A single item of type T.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the query returns zero or more than one row.
        /// </exception>
        /// <example>
        /// <code>
        /// var account = await AccessManager.LoadDataType&lt;Account, dynamic&gt;(
        ///     "SELECT * FROM account WHERE id = @Id",
        ///     new { Id = 1 },
        ///     connectionString);
        /// </code>
        /// </example>
        public static async Task<T> LoadDataType<T, U>(string sql, U parameters, string connectionString)
        {
            using IDbConnection connectionNoList = new MySqlConnection(connectionString);
            var rows = await connectionNoList.QuerySingleAsync<T>(sql, parameters).ConfigureAwait(false);
            return rows;
        }

        /// <summary>
        /// Executes a parameterized SQL command for inserting, updating, or deleting data.
        /// </summary>
        /// <typeparam name="T">The type of the parameters object.</typeparam>
        /// <param name="sql">The SQL command to execute (INSERT, UPDATE, DELETE).</param>
        /// <param name="parameters">
        /// Anonymous object or strongly-typed object containing query parameters.
        /// </param>
        /// <param name="connectionString">MySQL connection string.</param>
        /// <returns>A task that completes when the command has been executed.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if connectionString, sql, or parameters is null or empty.
        /// </exception>
        /// <remarks>
        /// This method validates all inputs before execution and logs any errors.
        /// Use parameterized queries to prevent SQL injection.
        /// </remarks>
        /// <example>
        /// <code>
        /// await AccessManager.SaveData(
        ///     "UPDATE account SET expansion = @Expansion WHERE id = @Id",
        ///     new { Expansion = 2, Id = 1 },
        ///     connectionString);
        /// </code>
        /// </example>
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

            using IDbConnection connectionSave = new MySqlConnection(connectionString);
            try
            {
                await connectionSave.ExecuteAsync(sql, parameters).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred: {ex.Message}", "ERROR");
                throw;
            }
        }

        #endregion
    }
}
