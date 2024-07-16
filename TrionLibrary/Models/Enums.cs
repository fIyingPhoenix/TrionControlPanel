namespace TrionLibrary.Models
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
        public enum TcpTableClass
        {
            TCP_TABLE_BASIC_LISTENER,
            TCP_TABLE_BASIC_CONNECTIONS,
            TCP_TABLE_BASIC_ALL,
            TCP_TABLE_OWNER_PID_LISTENER,
            TCP_TABLE_OWNER_PID_CONNECTIONS,
            TCP_TABLE_OWNER_PID_ALL,
            TCP_TABLE_OWNER_MODULE_LISTENER,
            TCP_TABLE_OWNER_MODULE_CONNECTIONS,
            TCP_TABLE_OWNER_MODULE_ALL
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
            MistOfPandaria,
        }
    }
}
