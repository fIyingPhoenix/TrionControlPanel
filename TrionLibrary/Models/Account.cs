namespace TrionLibrary.Models
{
    public class Account
    {
        //Account model for Trinity Core / AzerothCore pre Bnet!
        public class TrinityACore
        {
            public string Username;
            public byte[] Salt;
            public byte[] Verifier;
            public string Email;
            public int RegMail;
        }
    }
}
