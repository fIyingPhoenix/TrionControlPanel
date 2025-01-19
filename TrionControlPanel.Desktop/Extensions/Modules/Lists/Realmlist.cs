namespace TrionControlPanel.Desktop.Extensions.Modules.Lists
{
    public class Realmlist
    {
        public class TrinityBased
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string LocalAddress { get; set; }
            public string LocalSubnetMask { get; set; }
            public int Port { get; set; }
            public int GameBuild { get; set; }
        }
        public class AscemuBased
        {
            public int ID { get; set; }
            public string Password { get; set; }
            public string StatusChangeTime { get; set; }
        }
        public class MangosBased
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public int Port { get; set; }
            public int GameBuild { get; set; }
        }
    }
}
