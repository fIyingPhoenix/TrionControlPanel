using System.ComponentModel;

namespace TrionControlPanel.Desktop.Extensions.Modules.Lists
{
    public class AppSettings
    {
        //Database Names
        public string WorldDatabase { get; set; } = "wotlk_world";
        public string AuthDatabase { get; set; } = "wotlk_auth";
        public string CharactersDatabase { get; set; } = "wotlk_characters";
        public string HotfixDatabase { get; set; } = "N/A";
        //Dataabase Settings
        public string DBExeLoc { get; set; } = "N/A";
        public string DBWorkingDir { get; set; } = "N/A";
        public string DBLocation { get; set; } = "N/A";
        public string DBServerHost { get; set; } = "localhost";
        public string DBServerUser { get; set; } = "phoenix";
        public string DBServerPassword { get; set; } = "phoenix";
        public string DBServerPort { get; set; } = "3306";
        public string DBExeName { get; set; } = "mysqld";
        public bool DBInstalled { get; set; }
        //Custom Cores
        public string CustomWorkingDirectory { get; set; } = "";
        public string CustomWorldExeLoc { get; set; } = "";
        public string CustomLogonExeLoc { get; set; } = "";
        public string CustomLogonExeName { get; set; } = "worldserver";
        public string CustomWorldExeName { get; set; } = "authserver";
        public string CustomLogonName { get; set; } = "Custom Core";
        public string CustomWorldName { get; set; } = "Custom Core";
        public bool LaunchCustomCore { get; set; }
        public bool CustomInstalled { get; set; }
        //Classic Core
        public string ClassicWorkingDirectory { get; set; } = "";
        public string ClassicWorldExeLoc { get; set; } = "";
        public string ClassicLogonExeLoc { get; set; } = "";
        public string ClassicLogonExeName { get; set; } = "";
        public string ClassicWorldExeName { get; set; } = "";
        public string ClassicWorldName { get; set; } = "";
        public string ClassicLogonName { get; set; } = "";
        public bool LaunchClassicCore { get; set; }
        public bool ClassicInstalled { get; set; }
        //TBC Core
        public string TBCWorkingDirectory { get; set; } = "";
        public string TBCWorldExeLoc { get; set; } = "";
        public string TBCLogonExeLoc { get; set; } = "";
        public string TBCLogonExeName { get; set; } = "";
        public string TBCWorldExeName { get; set; } = "";
        public string TBCWorldName { get; set; } = "";
        public string TBCLogonName { get; set; } = "";
        public bool LaunchTBCCore { get; set; }
        public bool TBCInstalled { get; set; }
        //WotLK Core
        public string WotLKWorkingDirectory { get; set; } = "";
        public string WotLKWorldExeLoc { get; set; } = "";
        public string WotLKLogonExeLoc { get; set; } = "";
        public string WotLKLogonExeName { get; set; } = "";
        public string WotLKWorldExeName { get; set; } = "";
        public string WotLKWorldName { get; set; } = "";
        public string WotLKLogonName { get; set; } = "";
        public bool LaunchWotLKCore { get; set; }
        public bool WotLKInstalled { get; set; }
        //Cata Core
        public string CataWorkingDirectory { get; set; } = "";
        public string CataWorldExeLoc { get; set; } = "";
        public string CataLogonExeLoc { get; set; } = "";
        public string CataLogonExeName { get; set; } = "";
        public string CataWorldExeName { get; set; } = "";
        public string CataWorldName { get; set; } = "";
        public string CataLogonName { get; set; } = "";
        public bool LaunchCataCore { get; set; }
        public bool CataInstalled { get; set; }
        //Mop Core
        public string MopWorkingDirectory { get; set; } = "";
        public string MopWorldExeLoc { get; set; } = "";
        public string MopLogonExeLoc { get; set; } = "";
        public string MopLogonExeName { get; set; } = "";
        public string MopWorldExeName { get; set; } = "";
        public string MoPWorldName { get; set; } = "";
        public string MoPLogonName { get; set; } = "";
        public bool LaunchMoPCore { get; set; }
        public bool MOPInstalled { get; set; }
        //DDNS Settings
        public string DDNSDomain { get; set; } = "N/A";
        public string DDNSUsername { get; set; } = "N/A";
        public string DDNSPassword { get; set; } = "N/A";
        public int DDNSInterval { get; set; } = 1000;
        public string IPAddress { get; set; } = "";
        //Trion
        public Enums.TrionTheme TrionTheme { get; set; } = Enums.TrionTheme.TrionBlue;
        public string TrionIcon { get; set; } = "Trion New Logo";
        public string TrionLanguage { get; set; } = "en";
        public string SupporterKey { get; set; } = "null";
        public bool AutoUpdateCore { get; set; } = true;
        public bool AutoUpdateTrion { get; set; } = true;
        public bool AutoUpdateDatabase { get; set; } = true;
        public bool NotificationSound { get; set; }
        public bool ConsolHide { get; set; }
        public bool StayInTray { get; set; }
        public bool RunWithWindows { get; set; }
        public bool CustomNames { get; set; }
        public bool RunServerWithWindows { get; set; }
        public bool FirstRun { get; set; }
        public bool DDNSRunOnStartup { get; set; }
        public bool ServerCrashDetection { get; set; }
        public Enums.Cores SelectedCore { get; set; } = Enums.Cores.AzerothCore;
        public Enums.DDNSerivce DDNSerivce { get; set; } = Enums.DDNSerivce.DuckDNS;
        public Enums.SPP SelectedSPP { get; set; } = Enums.SPP.WrathOfTheLichKing;
        public Enums.Databases SelectedDatabases { get; set; } = Enums.Databases.WotLK;
        //Account
        public bool CreateBnetAccount { get; set; }
    }
}
