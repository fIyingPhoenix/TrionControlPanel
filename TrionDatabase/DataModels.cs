
namespace TrionDatabase
{
    public class DataModels
    {
        public class RealmList
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


    }
}
