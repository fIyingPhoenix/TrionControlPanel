using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrionLibrary.Models;
using TrionLibrary.Sys;

namespace TrionLibrary.Setting
{
    public class Setting
    {
        //Settings File Location
        public static string SettingsDataFile = $@"{Directory.GetCurrentDirectory()}\Settings.xml"; //HardCoded File Location. Maybe one day a dynamic one
        //Global Settings Data

        public static Lists.Setting List = new();
        public static void CreateSettingsFile(bool PopulateSettingsData)
        {
            if (!File.Exists(SettingsDataFile))
            {
                var createFile = File.Create(SettingsDataFile); //To fix the "File in use Error!"
                createFile.Close();
            }
            if (PopulateSettingsData == true)
            {
                List.CustomWorkingDirectory = Directory.GetCurrentDirectory();
                List.WorldDatabase = "acore_world";
                List.AuthDatabase = "acore_auth";
                List.CharactersDatabase = "acore_characters";
                List.DBExeLoc = "N/A";
                List.DBLocation = "N/A";
                List.DBServerHost = "localhost";
                List.DBServerUser = "acore";
                List.DBServerPassword = "acore";
                List.DBServerPort = "3306";
                List.DBExeName = "mysqld";
                List.CustomWorkingDirectory = "N/A";
                List.CustomWorldExeLoc = "N/A";
                List.CustomLogonExeLoc = "N/A";
                List.CustomWorldExeName = "worldserver";
                List.CustomLogonExeName = "authserver";
                List.DDNSDomain = "N/A";
                List.DDNSInterval = 1000;
                List.DDNSUsername = "N/A";
                List.DDNSPassword = "N/A";
                List.IPAddress = "127.0.0.1";
                List.ServerCrash = false;
                List.NotificationSound = true;
                List.ConsolHide = false;
                List.StayInTray = false;
                List.RunWithWindows = false;
                List.CustomNames = false;
                List.RunServerWithWindows = false;
                List.AutoUpdateCore = false;
                List.AutoUpdateTrion = true;
                List.FirstRun = true;
                List.DDNSRunOnStartup = false;
                List.ClassicInstalled = false;
                List.TBCInstalled = false;
                List.WotLKInstalled = false;
                List.CataInstalled = false;
                List.MOPInstalled = false;
                List.SelectedCore = Enums.Cores.AzerothCore;
                List.DDNSerivce = Enums.DDNSerivce.DuckDNS;
                List.SelectedSPP = Enums.SPP.WrathOfTheLichKing;
                WriteData(List, SettingsDataFile);
            }
        }
        public static Task Save()
        {
            try
            {
                if (File.Exists(SettingsDataFile))
                    WriteData(List, SettingsDataFile);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Infos.Message = ex.Message;
                throw new Exception(ex.Message);
            }
        }
        public static async Task Load()
        {
            try
            {
                if (File.Exists(SettingsDataFile))
                {
                    List = ReaderData(SettingsDataFile);
                }
                else
                {
                    CreateSettingsFile(true);
                    List = ReaderData(SettingsDataFile);
                }
            }
            catch (Exception ex)
            {
                Infos.Message = ex.Message;
            }
            await Task.CompletedTask;
        }
        private static void WriteData(object o, string fileName)
        {
            XmlSerializer serializer = new(o.GetType());
            TextWriter writer = new StreamWriter(fileName);
            serializer.Serialize(writer, o);
            writer.Close();
        }
        private static Lists.Setting ReaderData(string fileName)
        {
            XmlSerializer serializer = new(typeof(Lists.Setting));
            FileStream reader = new(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            List = (Lists.Setting)serializer.Deserialize(reader);
            reader.Close();
            return List;
        }
        public static void CreateMySQLConfigFile(string Location)
        {
            string ConfigFile = $@"{Location}\my.ini";
            string DirectoryMYSQL = List.DBLocation.Replace(@"\", "/");
            if (!File.Exists(ConfigFile))
            {
                var createFile = File.Create(ConfigFile);
                createFile.Close();
                List<string> lines = [.. File.ReadAllLines(ConfigFile)];
                lines.Add("[client]");
                lines.Add("port=3306");
                lines.Add("default-character-set = utf8mb4");
                lines.Add("socket=/tmp/mysql.sock");
                lines.Add("");
                lines.Add("[mysqld]");
                lines.Add("");
                lines.Add("# Set the base directory and data directory");
                lines.Add($"basedir={DirectoryMYSQL}");
                lines.Add($"datadir={DirectoryMYSQL}/data");
                lines.Add("");
                lines.Add("# Network settings");
                lines.Add("port=3306");
                lines.Add("bind-address=0.0.0.0");
                lines.Add("");
                lines.Add("# Increase limits");
                lines.Add("max_connections=10000");
                lines.Add("table_open_cache=20000");
                lines.Add("innodb_buffer_pool_size=2G");
                lines.Add("innodb_redo_log_capacity=512M");
                lines.Add("max_allowed_packet=1G");
                lines.Add("");
                lines.Add("# General settings");
                lines.Add("default-storage-engine=INNODB");
                lines.Add("character-set-server=utf8mb4");
                lines.Add("collation-server=utf8mb4_unicode_ci");
                lines.Add("");
                lines.Add("# Log settings");
                lines.Add($@"log_error={DirectoryMYSQL}/logs/mysql_error.log");
                lines.Add("slow_query_log=1");
                lines.Add($@"slow_query_log_file={DirectoryMYSQL}/logs/mysql_slow.log");
                lines.Add("long_query_time=2");
                lines.Add("");
                lines.Add("# Console output");
                lines.Add("general_log=1");
                lines.Add($@"general_log_file={DirectoryMYSQL}/logs/mysql_general.log");
                lines.Add("");
                lines.Add("[mysqld_safe]");
                lines.Add("# Increase open files limit");
                lines.Add("open-files-limit=65535");
                File.WriteAllLines(ConfigFile, lines);
            }
        }
    }
  

}
