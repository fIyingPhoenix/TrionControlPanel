namespace TrionLibrary
{
    public class EnumModels
    {
        public enum DDNSSerivce
        {
            DuckDNS
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
            Console,
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
    }
}
