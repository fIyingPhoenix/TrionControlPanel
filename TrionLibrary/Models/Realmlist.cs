namespace TrionLibrary.Models
{
    public class Realmlist
    {
        public class Trinity
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string LocalAddress { get; set; }
            public string LocalSubnetMask { get; set; }
            public int Port { get; set; }
            public int Icon { get; set; }
            public int Flag { get; set; }
            public int Timezone { get; set; }
            public int AllowedSecurityLevel { get; set; }
        }
        public class Ascemu
        {
            public int ID { get; set; }
            public string Password { get; set; }
            public string StatusChangeTime { get; set; }
        }
        public class Mangos
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public int Port { get; set; }
            public int Icon { get; set; }
            public int Realmflags { get; set; }
            public int Timezone { get; set; }
            public int AllowedSecurityLevel { get; set; }
        }
    }
}
