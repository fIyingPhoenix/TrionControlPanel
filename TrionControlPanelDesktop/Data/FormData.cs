using System.Text;
using TrionDatabase;
using TrionLibrary;

namespace TrionControlPanelDesktop.FormData
{
    public class User
    {
        public class API
        {
            //OneDriveAPI
            public static string DownloadOneDriveAPI(string url)
            {
                // Get Download Url          
                string base64Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(url));
                string encodedUrl = "u!" + base64Value.TrimEnd('=').Replace('/', '_').Replace('+', '-');
                return string.Format("https://api.onedrive.com/v1.0/shares/{0}/root/content", encodedUrl);//
            }
            //Gogole Drive API need fixing
            public static string DownloadGoogleDriveAPi(string url)
            {
                string startText = "https://drive.google.com/file/d/";
                string endText = "/view?usp=drive_link";
                string DirectURL = "https://drive.google.com/uc?export=download&&id=";

                int startIndex = url.IndexOf(startText);

                if (startIndex == -1)
                {
                    // Start text not found
                    return null!;
                }
                startIndex += startText.Length;
                int endIndex = url.IndexOf(endText, startIndex);
                if (endIndex == -1)
                {
                    // End text not found
                    return null!;
                }
                string downloadID = url[startIndex..endIndex];
                return DirectURL + downloadID;
            }
            //DDNS links
            public static async Task<string> DDNSUpdateURL(string Domain, string Username, string Password)
            {
                return Data.Settings.DDNSerivce switch
                {
                    EnumModels.DDNSerivce.DuckDNS => $"http://www.duckdns.org/update?domains={Domain}&token={Password}&ip={await NetworkHelper.GetExternalIpAddress()}",
                    EnumModels.DDNSerivce.NoIP => $"http://{Username}:{Password}@dynupdate.no-ip.com/nic/update?hostname={Domain}&myip={await NetworkHelper.GetExternalIpAddress()}",
                    EnumModels.DDNSerivce.Dynu => $"http://{Username}:{Password}@members.dyndns.org/v3/update?hostname={Domain}&myip={await NetworkHelper.GetExternalIpAddress()}",
                    EnumModels.DDNSerivce.Enom => $"http://dynamic.name-services.com/interface.asp?command=SetDnsHost&HostName={Domain}&Zone={Username}&DomainPassword={Password}&Address={await NetworkHelper.GetExternalIpAddress()}",
                    EnumModels.DDNSerivce.AllInkl => $"http://{Username}:{Password}@dyndns.kasserver.com/?myip={await NetworkHelper.GetExternalIpAddress()}",
                    EnumModels.DDNSerivce.dynDNS => $"http://{Username}:{Password}@update.dyndns.it/nic/update?hostname={Domain}",
                    EnumModels.DDNSerivce.STRATO => $"http://{Username}:{Password}@dyndns.strato.com/nic/update?hostname={Domain}&myip={await NetworkHelper.GetExternalIpAddress()}",
                    EnumModels.DDNSerivce.Freemyip => $"http://freemyip.com/update?domain={Domain}&token={Username}&myip={await NetworkHelper.GetExternalIpAddress()}",
                    EnumModels.DDNSerivce.Afraid => $"http://sync.afraid.org/u/{Username}/",
                    EnumModels.DDNSerivce.OVH => $"http://{Username}:{Password}@www.ovh.com/nic/update?system=dyndns&hostname={Domain}&myip={await NetworkHelper.GetExternalIpAddress()}",
                    EnumModels.DDNSerivce.Cloudflare => $"https://api.cloudflare.com/client/v4/zones/{Username}/dns_records/{Password}",
                    _ => "",
                };
            }
        }
        public class UI
        {    
            public class Update
            {
                public static string TrionVersOFF { get; set; }
                public static string TrionVersON { get; set; }
                public static string MySQLVerOFF { get; set; }
                public static string MySQLVerON { get; set; }
                public static string SPPVerOFF { get; set; }
                public static string SPPVerON { get; set; }
                public static bool TrionUpdate { get; set; }
                public static bool MysqlUpdate { get; set; }
                public static bool SppUpdate { get; set; }
                public static bool StartupUpdateCheck { get; set; }
            }
            public class Resource
            {
                public static int MachineTotalRam { get; set; }
                public static int MachineUsageRam { get; set; }
                public static int MachineCPUUsage { get; set; }
                public static int WorldTotalRam { get; set; }
                public static int WorldUsageRam { get; set; }
                public static int WorldCPUUsage { get; set; }
                public static int AuthTotalRam { get; set; }
                public static int AuthUsageRam { get; set; }
                public static int AuthCPUUsage { get; set; }
            }
            public class Download
            {
                public static int DownloadStatus { get; set; }
                public static int CurrentDownloads { get; set; }
            }
            public class Form
            {
                public static bool MySQLisRunning { get; set; }
                public static bool WorldisRunning { get; set; }
                public static bool LogonisRunning { get; set; }
                public static bool LoadData { get; set; }
                public static bool MySQLisStarted { get; set; }
                public static bool WorldisStarted { get; set; }
                public static bool LogonisStarted { get; set; }
                public static int Notyfications { get; set; }
                public static int StartUpLoading { get; set; }
            }
        }
        public class System
        {
            public static DateTime DatabaseStartTime { get; set; }
            public static DateTime WorldStartTime { get; set; }
            public static DateTime LogonStartTime { get; set; }
            public static List<int> DatabaseProcessID = [];
            public static List<int> WorldProcessesID = [];
            public static List<int> LogonProcessesID = [];
            public static List<int> DatabasePort = [];
            public static List<int> WorldPort = [];
            public static List<int> LogonPort = [];
        }
    }
}
