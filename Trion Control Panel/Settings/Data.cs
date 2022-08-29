using System.IO;
using System.Xml.Serialization;

namespace TrionControlPanel.Settings
{
    public class Data
    {
        public string DataFile = $@"{Directory.GetCurrentDirectory()}\Settings.xml";
        private static string _ErrorMessage;
        private static string _WorldDatabase;
        public static string _AuthDatabase;
        private static string _CharactersDatabase;
        private static string _MySQLLocation;
        public static string _MySQLServerHost;
        public static string _MySQLServerUser;
        public static string _MySQLServerPassword;
        public static string _MySQLServerPort;
        private static string _MySQLExecutableName;
        private static string _MySQLExecutablePath;
        private static string _CoreLocation;
        private static string _WorldExecutableLocation;
        private static string _BnetExecutableLocation;
        private static string _WorldExecutableName;
        private static string _BnetExecutableName;
        private static bool _ServerCrashCheck;
        private static bool _NotificationSound;
        private static bool _ConsolHide;
        private static bool _StayInTray;
        private static bool _RunWithWindows;
        private static bool _CustomNames;
        private static bool _StartCoreOnRun;
        private static int _SelectedCore;
 
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }
        public string WorldDatabase
        {
            get { return _WorldDatabase; }
            set { _WorldDatabase = value; }
        }
        public string AuthDatabase
        {
            get { return _AuthDatabase; }
            set { _AuthDatabase = value; }
        }
        public string CharactersDatabase
        {
            get { return _CharactersDatabase; }
            set { _CharactersDatabase = value; }
        }
        public string MySQLLocation
        {
            get { return _MySQLLocation; }
            set { _MySQLLocation = value; }
        }
        public string MySQLExecutablePath
        {
            get { return _MySQLExecutablePath; }
            set { _MySQLExecutablePath = value; }
        }
        public string MySQLServerPort
        {
            get { return _MySQLServerPort; }
            set { _MySQLServerPort = value; }
        }
        public string MySQLServerHost
        {
            get { return _MySQLServerHost; }
            set { _MySQLServerHost = value; }
        }
        public string MySQLServerUser
        {
            get { return _MySQLServerUser; }
            set { _MySQLServerUser = value; }
        }
        public string MySQLServerPassword
        {
            get { return _MySQLServerPassword; }
            set { _MySQLServerPassword = value; }
        }
        public string MySQLExecutableName
        {
            get { return _MySQLExecutableName; }
            set { _MySQLExecutableName = value; }
        }
        public string CoreLocation
        {
            get { return _CoreLocation; }
            set { _CoreLocation = value; }
        }
        public string WorldExecutableLocation
        {
            get { return _WorldExecutableLocation; }
            set { _WorldExecutableLocation = value; }
        }
        public  string BnetExecutableLocation
        {
            get { return _BnetExecutableLocation; }
            set { _BnetExecutableLocation = value; }
        }
        public string WorldExecutableName
        {
            get { return _WorldExecutableName; }
            set { _WorldExecutableName = value; }
        }
        public string BnetExecutableName
        {
            get { return _BnetExecutableName; }
            set { _BnetExecutableName = value; }
        }
        public bool ServerCrashCheck
        {
            get { return _ServerCrashCheck; }
            set { _ServerCrashCheck = value; }
        }
        public bool NotificationSound
        {
            get { return _NotificationSound; }
            set { _NotificationSound = value; }
        }
        public bool ConsolHide
        {
            get { return _ConsolHide; }
            set { _ConsolHide = value; }
        }
        public bool StayInTray
        {
            get { return _StayInTray; }
            set { _StayInTray = value; }
        }
        public bool RunWithWindows
        {
            get { return _RunWithWindows; }
            set { _RunWithWindows = value; }
        }
        public bool CustomNames
        {
            get { return _CustomNames; }
            set { _CustomNames = value; }
        }
        public bool StartCoreOnRun
        {
            get { return _StartCoreOnRun; }
            set { _StartCoreOnRun = value; }
        }
        public int SelectedCore
        {
            get { return _SelectedCore; }
            set {_SelectedCore = value; }
        }
    }
    public class Settings
    {
        public Data _Data = new();

        public bool SaveSettings()
        {
            try
            {
                WriteData(_Data, _Data.DataFile);
                return true;
            }
            catch (Exception ex)
            {
                _Data.ErrorMessage = ex.Message;
                return false;
            }
        }
        public bool LoadSettings()
        {
            
            if (Exist(_Data.DataFile) == true)
            {
                try
                {
                    _Data = ReaderData(_Data.DataFile);
                    return true;
                }
                catch (Exception ex)
                {
                    _Data.ErrorMessage = ex.Message;
                    return false;
                }
            }
            else
            {
                return FirstLoad();
            }
        }
        private bool FirstLoad()
        {
            try
            {
                
                var createFile = File.Create(_Data.DataFile);
                createFile.Close();
                _Data.WorldDatabase = "world";
                _Data.AuthDatabase = "auth";
                _Data.CharactersDatabase = "characters";
                _Data.MySQLLocation = "";
                _Data.MySQLServerHost = "localhost";
                _Data.MySQLServerUser = "root";
                _Data.MySQLServerPassword = "fIyingPhoenix";
                _Data.MySQLServerPort = "3306";
                _Data.MySQLExecutableName = "mysqld";
                _Data.CoreLocation = "";
                _Data.WorldExecutableLocation = "";
                _Data.BnetExecutableLocation = "";
                _Data.WorldExecutableName = "WorldServer";
                _Data.BnetExecutableName = "BNetServer";
                _Data.ServerCrashCheck = false;
                _Data.NotificationSound = true;
                _Data.ConsolHide = false;
                _Data.StayInTray = false;
                _Data.RunWithWindows = false;
                _Data.CustomNames = false;
                _Data.StartCoreOnRun = false;
                _Data.SelectedCore = 4;
                WriteData(_Data, _Data.DataFile);
                return true;
            }
            catch (Exception ex)
            {
                _Data.ErrorMessage = ex.Message;
                return false;
            }

        }

        private static bool Exist(string fileName)
        {
            if (File.Exists(fileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void WriteData(object o, string fileName)
        {
            XmlSerializer serializer = new(o.GetType());
            TextWriter writer = new StreamWriter(fileName); 
            serializer.Serialize(writer, o);
            writer.Close();
        }
        private  Data ReaderData(string fileName)
        {
            XmlSerializer serializer = new(typeof(Data));
            FileStream reader = new(fileName, FileMode.Open, FileAccess.Read,FileShare.Read);
            _Data = (Data)serializer.Deserialize(reader)!;
            reader.Close();
            return _Data;
        }
    }
}