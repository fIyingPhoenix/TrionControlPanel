using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Tls;
using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TrionLibrary
{
    public class Data
    {
        public static string Message = string.Empty;
        public static string SettingsDataFile = $@"{Directory.GetCurrentDirectory()}\Settings.xml"; //HardCoded File Location. Maybe one day a dynamic one
        public static Settings Settings = new Settings();

        public static void CreateSettingsFile(bool PopulateSettingsData)
        {
            if (!File.Exists(SettingsDataFile))
            {
                var createFile = File.Create(SettingsDataFile); //To fix the "File in use Error!"
                createFile.Close();
            }
            if (PopulateSettingsData == true)
            {
                Settings.WorldDatabase = "world";
                Settings.AuthDatabase = "auth";
                Settings.CharactersDatabase = "characters";
                Settings.MySQLLocation = "";
                Settings.MySQLServerHost = "localhost";
                Settings.MySQLServerUser = "root";
                Settings.MySQLServerPassword = "fIyingPhoenix";
                Settings.MySQLServerPort = "3306";
                Settings.MySQLExecutableName = "mysqld";
                Settings.CoreLocation = "";
                Settings.WorldExecutableLocation = "";
                Settings.BnetExecutableLocation = "";
                Settings.WorldExecutableName = "WorldServer";
                Settings.BnetExecutableName = "BNetServer";
                Settings.ServerCrash = false;
                Settings.NotificationSound = true;
                Settings.ConsolHide = false;
                Settings.StayInTray = false;
                Settings.RunWithWindows = false;
                Settings.CustomNames = false;
                Settings.RunServerWithWindows = false;
                Settings.SettingsUpdate = false;
                Settings.SelectedCore = EnumModels.Cores.TrinityCore;
                WriteData(Settings, SettingsDataFile);
            }
        }
        public static void SaveSettings()
        {
            try
            {
                if (File.Exists(SettingsDataFile))
                    WriteData(Settings, SettingsDataFile);
            }catch (Exception ex)
            {
                Message = ex.Message;
            }    
        }
        public static void LoadSettings() 
        {
            try
            {
                if (File.Exists(SettingsDataFile))
                    Settings = ReaderData(SettingsDataFile);
                else
                {
                    CreateSettingsFile(true);
                    Settings = ReaderData(SettingsDataFile);
                }
               
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        private static void WriteData(object o, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(o.GetType());
            TextWriter writer = new StreamWriter(fileName);
            serializer.Serialize(writer, o);
            writer.Close();
        }
        private static Settings ReaderData(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            FileStream reader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            Settings = (Settings)serializer.Deserialize(reader);
            reader.Close();
            return Settings;
        }
        public class Server
        {
            public static EnumModels.ServerStatus WorldServerStatus;
            public static EnumModels.ServerStatus LoginServerStatus;
            public static EnumModels.ServerStatus MysqlServerStatus;
        }
    }
    public class Settings
    {
        public string WorldDatabase;
        public string AuthDatabase;
        public string CharactersDatabase;
        public string MySQLLocation;
        public string MySQLServerHost;
        public string MySQLServerUser;
        public string MySQLServerPassword;
        public string MySQLServerPort;
        public string MySQLExecutableName;
        public string MySQLExecutablePath;
        public string CoreLocation;
        public string WorldExecutableLocation;
        public string BnetExecutableLocation;
        public string WorldExecutableName;
        public string BnetExecutableName;
        public bool SettingsUpdate;
        public bool ServerCrash;
        public bool NotificationSound;
        public bool ConsolHide;
        public bool StayInTray;
        public bool RunWithWindows;
        public bool CustomNames;
        public bool RunServerWithWindows;
        public EnumModels.Cores SelectedCore;
    }
}
