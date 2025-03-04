namespace TrionControlPanel.Desktop.Extensions.Modules
{
    public class Enums
    {
        // Enum for control events
        public enum ConsoleCtrlEvent
        {
            CTRLC = 0,      // CTRL+C
            CTRLBREAK = 1,  // CTRL+BREAK
            CTRLCLOSE = 2,  // Console closing
            CTRLLOGOFF = 5, // User logoff
            CTRLSHUTDOWN = 6 // System shutdown
        }

        // Enum for control event types
        public enum CtrlTypes
        {
            CTRLCEVENT = 0,      // CTRL+C
            CTRLBREAKEVENT,      // CTRL+BREAK
            CTRLCLOSEEVENT,      // Console window close
            CTRLLOGOFFEVENT = 5, // Logoff event
            CTRLSHUTDOWNEVENT    // Shutdown event
        }

        // Account operation results
        public enum AccountOpResult
        {
            Success,
            NameTooLong,
            PassTooLong,
            EmailTooLong,
            NameAlreadyExists,
            EmailAlreadyExists,
            NameNotExist,
            DatabaseInternalError,
            BadLink
        }

        // Realm list operation results
        public enum RealmListOpResult
        {
            Success,
            DatabaseInternalError,
            BadEmulator,
            BadLink
        }

        // Trion UI themes
        public enum TrionTheme
        {
            TrionBlue,
            Purple,
            Green,
            Orange
        }

        // Dynamic DNS Services
        public enum DDNSService
        {
            Afraid,
            AllInkl,
            Cloudflare,
            DuckDNS,
            NoIP,
            Dynu,
            DynDNS,
            Enom,
            Freemyip,
            OVH,
            STRATO
        }

        // Server types
        public enum ServerType
        {
            Database,
            World,
            Logon
        }

        // Server status
        public enum ServerStatus
        {
            Active,
            Inactive
        }

        // Current control page
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

        // Website loading status
        public enum LoadWebsite
        {
            True,
            False
        }

        // Core types
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

        // Server patch versions
        public enum SPP
        {
            Classic,
            TheBurningCrusade,
            WrathOfTheLichKing,
            Cataclysm,
            MistOfPandaria
        }

        // Database types
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
