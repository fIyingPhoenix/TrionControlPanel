using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using TrionLibrary;

namespace TrionDatabase
{
    public class SQLDataConnect
    {
        public static async Task<List<string>> GetTables(string connectionString)
        {
            List<string> tables = [];
            try
            {
                using (MySqlConnection connection = new(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT table_name FROM information_schema.tables WHERE table_schema = DATABASE()";

                    using (MySqlCommand command = new(sql, connection))
                    {
                        await using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                             tables.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Data.Message = $"Error getting tables: {ex.Message}";
            }
            return tables;
        }
        public static void DeleteTable(string connectionString, string tableName)
        {
            try
            {
                using (MySqlConnection connection = new(connectionString))
                {
                    connection.Open();

                    string sql = $"DROP TABLE IF EXISTS `{tableName}`";

                    using (MySqlCommand command = new(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Data.Message = $"Error deleting table '{tableName}': {ex.Message}";
            }
        }
        public static string SaveDataRealmSql()
        {
            return Data.Settings.SelectedCore switch
            {
                EnumModels.Cores.AscEmu => "UPDATE `realms` SET `password` = @Password, `status_change_time` = @StatusChangeTime WHERE `id` = @ID;",
                EnumModels.Cores.AzerothCore => "UPDATE `realmlist` SET `name` = @Name, `address` = @Address, `localAddress` = @LocalAddress , `localSubnetMask` = @LocalSubnetMask, `port` = @Port, `icon` = @Icon, `flag` = @Flag, `timezone` = @Timezone WHERE `id` = @ID;",
                EnumModels.Cores.CMaNGOS => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `port` = @Port, `icon` = @Icon, `realmflags` = @Realmflags, `timezone` = @Timezone, `allowedSecurityLevel` = @AllowedSecurityLevel WHERE `id` = @ID;",
                EnumModels.Cores.CypherCore => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `icon` = @Icon, `flag` = @Flag, `timezone` = @Timezone WHERE `id` = @ID;",
                EnumModels.Cores.TrinityCore335 => $"UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `icon` = @Icon, `flag` = @Flag, `timezone` = @Timezone WHERE `id` = @ID;",
                EnumModels.Cores.TrinityCore => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `icon` = @Icon, `flag` = @Flag, `timezone` = @Timezone WHERE `id` = @ID;",
                EnumModels.Cores.TrinityCoreClassic => $"UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `icon` = @Icon, `flag` = @Flag, `timezone` = @Timezone WHERE `id` = @ID;",
                EnumModels.Cores.VMaNGOS => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `port` = @Port, `icon` = @Icon, `realmflags` = @Realmflags, `timezone` = @Timezone WHERE `id` = @ID;",
                _ => ""
            };
        }
        public static string LoadDataRealmSql()
        {
            return Data.Settings.SelectedCore switch
            {
                EnumModels.Cores.AscEmu => $"SELECT * FROM `realms` LIMIT 10;",
                EnumModels.Cores.AzerothCore => $"SELECT * FROM `realmlist` LIMIT 10;",
                EnumModels.Cores.CMaNGOS => $"SELECT * FROM `realmlist` LIMIT 10;",
                EnumModels.Cores.CypherCore => $"SELECT * FROM `realmlist` LIMIT 10;",
                EnumModels.Cores.TrinityCore335 => $"SELECT * FROM `realmlist` LIMIT 10;",
                EnumModels.Cores.TrinityCore => $"SELECT * FROM `realmlist` LIMIT 10;",
                EnumModels.Cores.TrinityCoreClassic => $"SELECT * FROM `realmlist` LIMIT 10;",
                EnumModels.Cores.VMaNGOS => $"SELECT * FROM `realmlist` LIMIT 10;",
                _ => ""
            };
        }
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
        public static void SaveData<T> (string sql,T parameters,string connectionString)
        {
            using IDbConnection con = new MySqlConnection(connectionString);
            con.Execute(sql, parameters);

        }
    }
}
