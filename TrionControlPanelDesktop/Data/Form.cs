using System.Text;
using TrionLibrary.Models;
using TrionLibrary.Network;
using TrionLibrary.Setting;

namespace TrionControlPanelDesktop.Data
{
    public class User
    {
        public class API
        {
            //OneDriveAPI
            public static string DownloadOneDriveAPI(string url)
            {
                // Convert URL to Base64 and adjust it for OneDrive's API requirements
                string encodedUrl = "u!" + Convert.ToBase64String(Encoding.UTF8.GetBytes(url))
                    .TrimEnd('=')
                    .Replace('/', '_')
                    .Replace('+', '-');

                // Return the OneDrive API direct download link
                return $"https://api.onedrive.com/v1.0/shares/{encodedUrl}/root/content";
            }
            //Gogole Drive API need fixing
            public static string DownloadGoogleDriveAPi(string url)
            {
                // The base URL segment indicating the start of the file ID in a Google Drive link
                const string startText = "https://drive.google.com/file/d/";
                // The segment indicating the end of the file ID in the link
                const string endText = "/view?usp=drive_link";
                // The template for generating a direct download link using the file ID
                const string directURL = "https://drive.google.com/uc?export=download&id=";

                // Find the starting position of the file ID
                int startIndex = url.IndexOf(startText) + startText.Length;
                // Find the ending position of the file ID
                int endIndex = url.IndexOf(endText, startIndex);

                // If both start and end markers are found, extract and construct the direct URL
                return startIndex > startText.Length - 1 && endIndex > startIndex
                    ? directURL + url[startIndex..endIndex] // Extract file ID and append to the direct URL template
                    : null!; // Return null if the format is incorrect
            }
            //DDNS links
            public static async Task<string> DDNSUpdateURL(string Domain, string Username, string Password)
            {
                return Setting.List.DDNSerivce switch
                {
                    Enums.DDNSerivce.DuckDNS => $"http://www.duckdns.org/update?domains={Domain}&token={Password}&ip={await Helper.GetExternalIpAddress()}",
                    Enums.DDNSerivce.NoIP => $"http://{Username}:{Password}@dynupdate.no-ip.com/nic/update?hostname={Domain}&myip={await Helper.GetExternalIpAddress()}",
                    Enums.DDNSerivce.Dynu => $"http://{Username}:{Password}@members.dyndns.org/v3/update?hostname={Domain}&myip={await Helper.GetExternalIpAddress()}",
                    Enums.DDNSerivce.Enom => $"http://dynamic.name-services.com/interface.asp?command=SetDnsHost&HostName={Domain}&Zone={Username}&DomainPassword={Password}&Address={await Helper.GetExternalIpAddress()}",
                    Enums.DDNSerivce.AllInkl => $"http://{Username}:{Password}@dyndns.kasserver.com/?myip={await Helper.GetExternalIpAddress()}",
                    Enums.DDNSerivce.dynDNS => $"http://{Username}:{Password}@update.dyndns.it/nic/update?hostname={Domain}",
                    Enums.DDNSerivce.STRATO => $"http://{Username}:{Password}@dyndns.strato.com/nic/update?hostname={Domain}&myip={await Helper.GetExternalIpAddress()}",
                    Enums.DDNSerivce.Freemyip => $"http://freemyip.com/update?domain={Domain}&token={Username}&myip={await Helper.GetExternalIpAddress()}",
                    Enums.DDNSerivce.Afraid => $"http://sync.afraid.org/u/{Username}/",
                    Enums.DDNSerivce.OVH => $"http://{Username}:{Password}@www.ovh.com/nic/update?system=dyndns&hostname={Domain}&myip={await Helper.GetExternalIpAddress()}",
                    Enums.DDNSerivce.Cloudflare => $"https://api.cloudflare.com/client/v4/zones/{Username}/dns_records/{Password}",
                    _ => "",
                };
            }
        }
        public class UI
        {
            public class Version
            {
                public class ON
                {
                    public static string Trion { get; set; }
                    public static string Database { get; set; }
                    public static string Classic { get; set; }
                    public static string TBC { get; set; }
                    public static string WotLK { get; set; }
                    public static string Cata { get; set; }
                    public static string Mop { get; set; }
                }
                public class OFF
                {
                    public static string Trion { get; set; }
                    public static string Database { get; set; }
                    public static string Classic { get; set; }
                    public static string TBC { get; set; }
                    public static string WotLK { get; set; }
                    public static string Cata { get; set; }
                    public static string Mop { get; set; }
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
            public class Resource
            {
                public static int MachineTotalRam { get; set; }
                public static int MachineUsageRam { get; set; }
                public static int MachineCPUUsage { get; set; }
                public static int WorldTotalRam { get; set; }
                public static int WorldUsageRam { get; set; }
                public static int WorldCPUUsage { get; set; }
                public static int CurrentWorldID { get; set; }
                public static int AuthTotalRam { get; set; }
                public static int AuthUsageRam { get; set; }
                public static int AuthCPUUsage { get; set; }
                public static int CurrentAuthID { get; set; }
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
                public static bool LoadData { get; set; }
                public static int Notyfications { get; set; }
                public static int StartUpLoading { get; set; }
            }
        }
        public class System
        {
            public static DateTime DatabaseStartTime { get; set; }
            public static DateTime WorldStartTime { get; set; }
            public static DateTime LogonStartTime { get; set; }

            public static List<Lists.ProcessID> DatabaseProcessID = [];
            public static List<Lists.ProcessID> WorldProcessesID = [];
            public static List<Lists.ProcessID> LogonProcessesID = [];
            public static List<Lists.ProcessPort> DatabasePort = [];
            public static List<Lists.ProcessPort> WorldPort = [];
            public static List<Lists.ProcessPort> LogonPort = [];
        }
    }
}
