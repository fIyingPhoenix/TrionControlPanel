using System;
using System.IO;
using System.Xml.Serialization;

namespace TrionLibrary
{
    public class Data
    {
        public static string Message { get; set; }

        private static string _WorldDatabase;
        private static string _AuthDatabase;
        private static string _CharactersDatabase;
        private static string _MySQLLocation;
        private static string _MySQLServerHost;
        private static string _MySQLServerUser;
        private static string _MySQLServerPassword;
        private static string _MySQLServerPort;
        private static string _MySQLExecutableName;
        private static string _MySQLExecutablePath;
        private static string _CoreLocation;
        private static string _WorldExecutableLocation;
        private static string _BnetExecutableLocation;
        private static string _WorldExecutableName;
        private static string _BnetExecutableName;
        private static bool _SettingsUpdate;
        private static bool _ServerCrashCheck;
        private static bool _NotificationSound;
        private static bool _ConsolHide;
        private static bool _StayInTray;
        private static bool _RunWithWindows;
        private static bool _CustomNames;
        private static bool _StartCoreOnRun;
        private static int _SelectedCore;

        public static string WorldDatabase
        {
            get { return _WorldDatabase; }
            set { _WorldDatabase = value; }
        }
        public static string AuthDatabase
        {
            get { return _AuthDatabase; }
            set { _AuthDatabase = value; }
        }
        public static string CharactersDatabase
        {
            get { return _CharactersDatabase; }
            set { _CharactersDatabase = value; }
        }
        public static string MySQLLocation
        {
            get { return _MySQLLocation; }
            set { _MySQLLocation = value; }
        }
        public static string MySQLExecutablePath
        {
            get { return _MySQLExecutablePath; }
            set { _MySQLExecutablePath = value; }
        }
        public static string MySQLServerPort
        {
            get { return _MySQLServerPort; }
            set { _MySQLServerPort = value; }
        }
        public static string MySQLServerHost
        {
            get { return _MySQLServerHost; }
            set { _MySQLServerHost = value; }
        }
        public static string MySQLServerUser
        {
            get { return _MySQLServerUser; }
            set { _MySQLServerUser = value; }
        }
        public static string MySQLServerPassword
        {
            get { return _MySQLServerPassword; }
            set { _MySQLServerPassword = value; }
        }
        public static string MySQLExecutableName
        {
            get { return _MySQLExecutableName; }
            set { _MySQLExecutableName = value; }
        }
        public static string CoreLocation
        {
            get { return _CoreLocation; }
            set { _CoreLocation = value; }
        }
        public static string WorldExecutableLocation
        {
            get { return _WorldExecutableLocation; }
            set { _WorldExecutableLocation = value; }
        }
        public static string BnetExecutableLocation
        {
            get { return _BnetExecutableLocation; }
            set { _BnetExecutableLocation = value; }
        }
        public static string WorldExecutableName
        {
            get { return _WorldExecutableName; }
            set { _WorldExecutableName = value; }
        }
        public static string BnetExecutableName
        {
            get { return _BnetExecutableName; }
            set { _BnetExecutableName = value; }
        }
        public static bool SettingsUpdate
        {
            get { return _SettingsUpdate; }
            set { _SettingsUpdate = value; }
        }
        public static bool ServerCrashCheck
        {
            get { return _ServerCrashCheck; }
            set { _ServerCrashCheck = value; }
        }
        public static bool NotificationSound
        {
            get { return _NotificationSound; }
            set { _NotificationSound = value; }
        }
        public static bool ConsolHide
        {
            get { return _ConsolHide; }
            set { _ConsolHide = value; }
        }
        public static bool StayInTray
        {
            get { return _StayInTray; }
            set { _StayInTray = value; }
        }
        public static bool RunWithWindows
        {
            get { return _RunWithWindows; }
            set { _RunWithWindows = value; }
        }
        public static bool CustomNames
        {
            get { return _CustomNames; }
            set { _CustomNames = value; }
        }
        public static bool StartCoreOnRun
        {
            get { return _StartCoreOnRun; }
            set { _StartCoreOnRun = value; }
        }
        public static int SelectedCore
        {
            get { return _SelectedCore; }
            set { _SelectedCore = value; }
        }
    }
    public class Settings
    {

        
    }
}
