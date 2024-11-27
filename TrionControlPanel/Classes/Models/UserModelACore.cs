namespace TrionControlPanel.Classes.Models
{
    public class UserModelACore
    {
        public uint Id { get; set; } // unsigned int in SQL maps to uint in C#

        public string Username { get; set; } = "Username"; // varchar(32)

        public string Email { get; set; } = "mail@example.com";// varchar(255)

        public string RegMail { get; set; } = "mail@example.com";// varchar(255)

        public DateTime JoinDate { get; set; } // timestamp

        public string LastIp { get; set; } = "127.0.0.1"; // varchar(15), default '127.0.0.1'

        public string LastAttemptIp { get; set; } = "127.0.0.1"; // varchar(15), default '127.0.0.1'

        public uint FailedLogins { get; set; } = 0; // unsigned int, default 0

        public byte Locked { get; set; } = 0; // tinyint unsigned, default 0

        public string LockCountry { get; set; } = "00"; // varchar(2), default '00'

        public DateTime? LastLogin { get; set; } // timestamp, nullable

        public uint Online { get; set; } = 0; // unsigned int, default 0

        public byte Expansion { get; set; } = 2; // tinyint unsigned, default 2

        public long MuteTime { get; set; } = 0; // bigint, default 0

        public string MuteReason { get; set; } = ""; // varchar(255)

        public string MuteBy { get; set; } = ""; // varchar(50)

        public byte Locale { get; set; } = 0; // tinyint unsigned, default 0

        public string Os { get; set; } = "Win"; // varchar(3)

        public uint Recruiter { get; set; } = 0; // unsigned int, default 0

        public uint TotalTime { get; set; } = 0; // unsigned int, default 0
    }
}
