using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrionLibrary.Models
{
    public class Lists
    {
        public class File
        {
            public string FileName;
            public string FileFullName;
            public string FileHash;
        }
        public class ProcessID
        {
            public int ID;
            public string Name;
        }
        public class ProcessPort
        {
            public int Port;
            public string Name;
        }
        public class Setting
        {
            //Database Names
            public string WorldDatabase;
            public string AuthDatabase;
            public string CharactersDatabase;
            public string HotfixDatabase;
            //Dataabase Settings
            public string DBExeLoca;
            public string DBLocation;
            public string DBServerHost;
            public string DBServerUser;
            public string DBServerPassword;
            public string DBServerPort;
            public string DBExecutableName;
            public string DBExecutablePath;
            public bool DBInstalled;
            //Custom Cores
            public string CustomWorkingDirectory;
            public string CustomWorldExeLoc;
            public string CustomLogonExeLoc;
            public string CustomLogonExeName;
            public string CustomWorldExeName;
            public bool CustomInstalled;
            //Classic Core
            public string ClassicWorkingDirectory;
            public string ClassicWorldExeLoc;
            public string ClassicLogonExeLoc;
            public string ClassicLogonExeName;
            public string ClassicWorldExeName;
            public bool ClassicInstalled;
            //TBC Core
            public string TBCWorkingDirectory;
            public string TBCDBExeLoca;
            public string TBCWorldExeLoc;
            public string TBCLogonExeLoc;
            public string TBCLogonExeName;
            public string TBCWorldExeName;
            public bool TBCInstalled;
            //WotLK Core
            public string WotLKWorkingDirectory;
            public string WotLKDBExeLoca;
            public string WotLKWorldExeLoc;
            public string WotLKLogonExeLoc;
            public string WotLKLogonExeName;
            public string WotLKWorldExeName;
            public bool WotLKInstalled;
            //Cata Core
            public string CataWorkingDirectory;
            public string CataDBExeLoca;
            public string CataWorldExeLoc;
            public string CataLogonExeLoc;
            public string CataLogonExeName;
            public string CataWorldExeName;
            public bool CataInstalled;
            //Mop Core
            public string MopWorkingDirectory;
            public string MopDBExeLoca;
            public string MopWorldExeLoc;
            public string MopLogonExeLoc;
            public string MopLogonExeName;
            public string MopWorldExeName;
            public bool MOPInstalled;
            //DDNS Settings
            public string DDNSDomain;
            public string DDNSUsername;
            public string DDNSPassword;
            public string IPAddress;
            //Trion
            public bool AutoUpdateCore;
            public bool AutoUpdateTrion;
            public bool AutoUpdateMySQL;
            public bool ServerCrash;
            public bool NotificationSound;
            public bool ConsolHide;
            public bool StayInTray;
            public bool RunWithWindows;
            public bool CustomNames;
            public bool RunServerWithWindows;
            public bool FirstRun;
            public bool DDNSRunOnStartup;
            public int DDNSInterval;
            public Enums.Cores SelectedCore;
            public Enums.DDNSerivce DDNSerivce;
            public Enums.SPP SelectedSPP;
        }
    }
}
