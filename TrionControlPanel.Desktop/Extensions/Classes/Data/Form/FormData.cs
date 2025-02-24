namespace TrionControlPanel.Desktop.Extensions.Classes.Data.Form
{
    public class FormData
    {
        public class Infos
        {
            public class Install
            {
                public static bool Trion { get; set; }
                public static bool Database { get; set; }
                public static bool Classic { get; set; }
                public static bool TBC { get; set; }
                public static bool WotLK { get; set; }
                public static bool Cata { get; set; }
                public static bool Mop { get; set; }
            }
        }
        public class Attempt
        {
            public static int CustomLogon { get; set; }
            public static int CustomWorld { get; set; }
            public static int ClassicLogon { get; set; }
            public static int ClassicWorld { get; set; }
            public static int TBCLogon { get; set; }
            public static int TBCWorld { get; set; }
            public static int WotlkLogon { get; set; }
            public static int WotlkWorld { get; set; }
            public static int CataLogon { get; set; }
            public static int CataWorld { get; set; }
            public static int MopLogon { get; set; }
            public static int MopWorld { get; set; }
            public static int Database { get; set; }
        }
        public class UI
        {
            public class Version
            {
                public class Online
                {
                    public static string Trion { get; set; } = "N/A";
                    public static string Database { get; set; } = "N/A";
                    public static string Classic { get; set; } = "N/A";
                    public static string TBC { get; set; } = "N/A";
                    public static string WotLK { get; set; } = "N/A";
                    public static string Cata { get; set; } = "N/A";
                    public static string Mop { get; set; } = "N/A";
                }
                public class Local
                {
                    public static string Trion { get; set; } = "N/A";
                    public static string Database { get; set; } = "N/A";
                    public static string Classic { get; set; } = "N/A";
                    public static string TBC { get; set; } = "N/A";
                    public static string WotLK { get; set; } = "N/A";
                    public static string Cata { get; set; } = "N/A";
                    public static string Mop { get; set; } = "N/A";
                }
                public class Update
                {
                    public static bool Trion { get; set; }
                    public static bool Database { get; set; }
                    public static bool Classic { get; set; }
                    public static bool TBC { get; set; }
                    public static bool WotLK { get; set; }
                    public static bool Cata { get; set; }
                    public static bool Mop { get; set; }
                }
            }
            public class Form
            {
                //DB
                public static bool DBRunning { get; set; }
                public static bool DBStarted { get; set; }
                //Custom 
                public static bool CustWorldRunning { get; set; }
                public static bool CustLogonRunning { get; set; }
                public static bool CustWorldStarted { get; set; }
                public static bool CustLogonStarted { get; set; }
                //SPP Classic
                public static bool ClassicWorldRunning { get; set; }
                public static bool ClassicLogonRunning { get; set; }
                public static bool ClassicWorldStarted { get; set; }
                public static bool ClassicLogonStarted { get; set; }
                //SPP TBC
                public static bool TBCWorldRunning { get; set; }
                public static bool TBCLogonRunning { get; set; }
                public static bool TBCWorldStarted { get; set; }
                public static bool TBCLogonStarted { get; set; }
                //SPP WotLK
                public static bool WotLKWorldRunning { get; set; }
                public static bool WotLKLogonRunning { get; set; }
                public static bool WotLKWorldStarted { get; set; }
                public static bool WotLKLogonStarted { get; set; }
                //SPP Cata
                public static bool CataWorldRunning { get; set; }
                public static bool CataLogonRunning { get; set; }
                public static bool CataWorldStarted { get; set; }
                public static bool CataLogonStarted { get; set; }
                //SPP MOP
                public static bool MOPWorldRunning { get; set; }
                public static bool MOPLogonRunning { get; set; }
                public static bool MOPWorldStarted { get; set; }
                public static bool MOPLogonStarted { get; set; }
                //
                public static bool InstallingEmulator { get; set; } 
                public static bool LoadData { get; set; }
                public static int Notyfications { get; set; }
                public static int StartUpLoading { get; set; }
            }
        }
    }
}
