namespace TrionControlPanel.Desktop.Extensions.Modules
{
    public class Enums
    {
        // Enum for control events that can be generated using GenerateConsoleCtrlEvent.
        public enum ConsoleCtrlEvent
        {
            CTRLC = 0,      // Represents a CTRL+C signal.
            CTRLBREAK = 1,  // Represents a CTRL+BREAK signal.
            CTRLCLOSE = 2,  // Indicates the console is closing.
            CTRLLOGOFF = 5, // Indicates the user is logging off.
            CTRLSHUTDOWN = 6 // Indicates the system is shutting down.
        }

        // Enum for control event types handled by ConsoleCtrlDelegate.
        public enum CtrlTypes
        {
            CTRLCEVENT = 0,      // CTRL+C event.
            CTRLBREAKEVENT,      // CTRL+BREAK event.
            CTRLCLOSEEVENT,      // Console window close event.
            CTRLLOGOFFEVENT = 5, // User logoff event.
            CTRLSHUTDOWNEVENT    // System shutdown event.
        }
        public enum AccountOpResult
        {
            Ok,
            NameTooLong,
            PassTooLong,
            EmailTooLong,
            NameAlreadyExist,
            EmailAlreadyExist,
            NameNotExist,
            DBInternalError,
            BadLink
        }
        public enum RealmListOpResult
        {
            Ok,
            DBInternalError,
            BadEmulator,
            BadLink
        }
        public enum TrionTheme
        {
            TrionBlue,
            Purple,
            Green,
            Orange,
        }
        public enum DDNSerivce
        {
            Afraid,
            AllInkl,
            Cloudflare,
            DuckDNS,
            NoIP,
            Dynu,
            dynDNS,
            Enom,
            Freemyip,
            OVH,
            STRATO
        }
        public enum ServerType
        {
            Database,
            World,
            Logon
        }

        public enum ServerStatus
        {
            Running,
            NotRunning
        }
        public enum CurrentControl
        {
            Home,
            Control,
            Settings,
            Load,
            Download,
            Database,
            Notify
        }
        public enum LoadWebsite
        {
            True,
            False
        }
        public enum Cores
        {
            AscEmu,
            AzerothCore,
            CMaNGOS,
            CypherCore,
            TrinityCore335,
            TrinityCore,
            TrinityCoreClassic,
            VMaNGOS
        }
        public enum SPP
        {
            Classic,
            TheBurningCrusade,
            WrathOfTheLichKing,
            Cataclysm,
            MistOfPandaria
        }
        public enum Databases
        {
            Custom,
            Classic,
            TBC,
            WotLK,
            Cata,
            MoP
        }
    }
}
