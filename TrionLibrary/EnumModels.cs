namespace TrionLibrary
{
    public class EnumModels
    {
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
