using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrionDiscordBot.Encrypting;

namespace TrionDiscordBot.Data
{
    
    internal class Database
    {
        
        public static string ConnectionList()
        {
           return new($"Server=192.168.188.25;Port=3306;User Id=trinity;Password=andrei1993kanda12;Database=auth"); ;
        }
        
        public static Task SaveData<T>(string Sql, T parameters)
        { 
            using (IDbConnection connection = new MySqlConnection(ConnectionList()))
            {
                return connection.ExecuteAsync(Sql, parameters);
            }
        }

        public async Task<List<T>> LoadData<T,U>(string Sql,U parameters)
        {
            using(IDbConnection connection = new MySqlConnection(ConnectionList()))
            {
                var rows = await connection.QueryAsync<T>(Sql, parameters);
                return rows.ToList();
            }
        }
    }
}
