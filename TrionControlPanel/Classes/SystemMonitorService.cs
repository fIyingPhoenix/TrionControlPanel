using System.Diagnostics;
namespace TrionControlPanel.Classes
{
    public class SystemMonitorService
    {
        public static string OSRuinning()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix) return "Unix";
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT) return "Widnows";
            else return "Unknown";
        }
    }
}
