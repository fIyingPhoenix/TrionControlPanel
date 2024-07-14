using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System;
using System.IO;
using TrionLibrary.Sys;

namespace TrionLibrary.Database
{
    public class Access
    {
        public static async Task<List<string>> GetTables(string connectionString)
        {
            List<string> tables = [];
            try
            {
                using MySqlConnection connection = new(connectionString);
                connection.Open();

                string sql = "SELECT table_name FROM information_schema.tables WHERE table_schema = DATABASE()";

                using MySqlCommand command = new(sql, connection);
                await using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tables.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                Infos.Message = $"Error getting tables: {ex.Message}";
            }
            return tables;
        }
        public static void DeleteTable(string connectionString, string tableName)
        {
            try
            {
                using MySqlConnection connection = new(connectionString);
                connection.Open();

                string sql = $"DROP TABLE IF EXISTS `{tableName}`";

                using MySqlCommand command = new(sql, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Infos.Message = $"Error deleting table '{tableName}': {ex.Message}";
            }
        }
        public static Task DumpAllTables(string connectionString, string outputFile)
        {
            Thread MachineCpuUtilizationThread = new(() =>
            {
                using (MySqlConnection connection = new(connectionString))
                {
                    using (StreamWriter writer = new(outputFile))
                    {
                        connection.Open();
                        // Retrieve a list of tables in the database
                        List<string> tableNames = [];
                        using (MySqlCommand command = new("SHOW TABLES", connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    tableNames.Add(reader.GetString(0));
                                }
                            }
                        }
                        // Write CREATE TABLE and INSERT INTO statements for each table to the output file
                        foreach (string tableName in tableNames)
                        {
                            // Retrieve CREATE TABLE statement
                            string createTableStatement = GetCreateTableStatement(connection, tableName);
                            writer.WriteLine(createTableStatement);

                            // Retrieve INSERT INTO statements
                            string insertStatements = GetInsertStatements(connection, tableName);
                            writer.WriteLine(insertStatements);
                        }
                    }
                }
            });

            MachineCpuUtilizationThread.Start();
            return Task.CompletedTask;
        }
        static string GetCreateTableStatement(MySqlConnection connection, string tableName)
        {
            string createTableStatement = string.Empty;
            using (MySqlCommand command = new($"SHOW CREATE TABLE `{tableName}`", connection))
            {
                using MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    createTableStatement = reader.GetString(1) + ";";
                }
            }
            return createTableStatement;
        }
        static string GetInsertStatements(MySqlConnection connection, string tableName)
        {
            StringBuilder insertStatements = new();
            using (MySqlCommand command = new($"SELECT * FROM `{tableName}`", connection))
            {
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    StringBuilder values = new();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (!reader.IsDBNull(i))
                        {
                            string value = reader.GetValue(i).ToString()!;
                            values.Append($"'{value}',");
                        }
                        else
                        {
                            values.Append("NULL,");
                        }
                    }
                    values.Length--; // Remove the last comma
                    insertStatements.AppendLine($"INSERT INTO {tableName} VALUES ({values});");
                }
            }
            return insertStatements.ToString();
        }
        public static async Task<List<T>> LodaData<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection con = new MySqlConnection(connectionString))
            {
                var rows = await con.QueryAsync<T>(sql, parameters);
                return rows.ToList();
            }
        }
        public static void SaveData<T>(string sql, T parameters, string connectionString)
        {
            using IDbConnection con = new MySqlConnection(connectionString);
            con.Execute(sql, parameters);
        }
    }
}
