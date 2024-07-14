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
                List.DBExeLoca = "";
                List.DBLocation = "";
                List.DBServerHost = "localhost";
                List.DBServerUser = "acore";
                List.DBServerPassword = "acore";
                List.DBServerPort = "3306";
                List.DBExecutableName = "mysqld";
                List.CustomWorkingDirectory = "";
                List.CustomWorldExeLoc = "";
                List.CustomLogonExeLoc = "";
                List.CustomWorldExeName = "worldserver";
                List.CustomLogonExeName = "authserver";
                List.DDNSDomain = "";
                List.DDNSInterval = 1000;
                List.DDNSUsername = "";
                List.DDNSPassword = "";
                List.IPAddress = "0.0.0.0";
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
                List<string> lines = File.ReadAllLines(ConfigFile).ToList();
                lines.Add("[client]");
                lines.Add("port=3306");
                lines.Add("default-character-set = utf8mb4");
                lines.Add("socket=/tmp/mysql.sock");
                lines.Add("");
                lines.Add("[mysqld]");
                lines.Add("port=3306");
                lines.Add("socket=/tmp/mysql.sock");
                lines.Add("key_buffer_size=256M");
                lines.Add("max_allowed_packet=1G");
                lines.Add($"basedir={DirectoryMYSQL}");
                lines.Add($"datadir={DirectoryMYSQL}data");
                lines.Add("");
                lines.Add("[mysqldump]");
                lines.Add("quick");
                lines.Add("max_allowed_packet = 512M");
                lines.Add("");
                File.WriteAllLines(ConfigFile, lines);
            }
        }
    }
  

}
