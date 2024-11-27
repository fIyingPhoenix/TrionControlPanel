namespace TrionControlPanel.Classes.Models
{
    public class UserModelWeb : IUserModelWeb
    {
        public int Id { get; set; } = 0; 
        public string Name { get; set; } = "Username"; // varchar(32)
        public int Access { get; set; } = 0; // tinyint, default 0 max 4
        public string Email { get; set; } = "mail@example.com";// varchar(255)
        public int Expansion { get; set; } = 0; // tinyint, default 0
        public string LastIP { get; set; } = "127.0.0.1"; // varchar(15), default '127.0.0.1'
        public string CurrentIP { get; set; } = "127.0.0.1"; // varchar(15), default '127.0.0.1'
        public bool IsDonor { get; set; } = false; //bool, default false
        public bool IsLogediIn { get; set; } = false; //bool, default false

    }

}
