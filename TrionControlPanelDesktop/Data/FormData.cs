using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrionControlPanelDesktop.FormData
{
    public class UIData
    {
        public static bool MySQLisRunning = false;
        public static bool WorldisRunning = false;
        public static bool LogonisRunning = false;

        public static string TrionVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static string DownloadOneDriveAPI(string url)
        {
            // Get Download Url          
            string base64Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(url));
            string encodedUrl = "u!" + base64Value.TrimEnd('=').Replace('/', '_').Replace('+', '-');
            return  string.Format("https://api.onedrive.com/v1.0/shares/{0}/root/content", encodedUrl);
        }
        public static int StartUpLoading { get; set; }
        public static string ContributorsURL()
        {
            return "https://camo.githubusercontent.com/a95087807c72a5c7a697006485e3ce5135441239763a61b61bf9bde5d95291af/68747470733a2f2f696d672e736869656c64732e696f2f6769746875622f636f6e7472696275746f72732f664979696e6750686f656e69782f5472696f6e436f6e74726f6c50616e656c2e7376673f7374796c653d666f722d7468652d6261646765";
        }
        public static string ForksURL()
        {
            return "https://camo.githubusercontent.com/ea7ac43ae1ee648390bc428320c35baf6f0fe587e098e41220453f0843fdccf3/68747470733a2f2f696d672e736869656c64732e696f2f6769746875622f666f726b732f664979696e6750686f656e69782f5472696f6e436f6e74726f6c50616e656c2e7376673f7374796c653d666f722d7468652d6261646765";
        }
        public static string StarsURL()
        {
            return "https://camo.githubusercontent.com/83feb12931cb2b9d94a7eed56bb9047d43ef8e0ddabdab13acfc0f3259752281/68747470733a2f2f696d672e736869656c64732e696f2f6769746875622f73746172732f664979696e6750686f656e69782f5472696f6e436f6e74726f6c50616e656c2e7376673f7374796c653d666f722d7468652d6261646765";
        }
        public static string IssuesURL()
        {
            return "https://camo.githubusercontent.com/7b6146b75c4aa0f524810a797d61abfbfcde72b2d33826911819b21118ec8ff5/68747470733a2f2f696d672e736869656c64732e696f2f6769746875622f6973737565732f664979696e6750686f656e69782f5472696f6e436f6e74726f6c50616e656c2e7376673f7374796c653d666f722d7468652d6261646765";
        }
        public static string CodeQualityURL()
        {
            return "https://camo.githubusercontent.com/2125f70052fc2f8ee12556574d3ade67cab4c4bbb6e815ace3d7b8f74c2e933a/68747470733a2f2f696d672e736869656c64732e696f2f636f6465666163746f722f67726164652f6769746875622f664979696e6750686f656e69782f5472696f6e436f6e74726f6c50616e656c3f7374796c653d666f722d7468652d6261646765";
        }
    }
}
