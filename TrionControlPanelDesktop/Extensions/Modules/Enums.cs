namespace TrionControlPanelDesktop.Extensions.Modules
{
    public class Enums
    {
        public enum AccountOpResult
        {
            Ok,
            NameTooLong,
            PassTooLong,
            EmailTooLong,
            NameAlreadyExist,
            NameNotExist,
            DBInternalError,
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
