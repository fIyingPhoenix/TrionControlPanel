namespace TrionControlPanel.Desktop.Extensions.Modules.Lists
{
    public class Account
    {
        public class Auth
        {
            public string Username { get; set; }
            public byte[] Salt { get; set; }
            public byte[] Verifier { get; set; }
            public string Email { get; set; }
            public int RegMail { get; set; }
        }
    }
}
