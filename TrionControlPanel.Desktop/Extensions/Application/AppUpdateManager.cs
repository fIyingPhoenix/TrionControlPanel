using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Application
{
    public class AppUpdateManager
    {
        // Asynchronously fetches the latest version of the Single Player Project (SPP) from the online API
        public static async Task GetSPPVersionOnline(AppSettings Settings)
        {
            try
            {
                using HttpClient httpClient = new();
                HttpResponseMessage response = await httpClient.GetAsync(Links.APIRequests.GetSPPVersion(Settings.SupporterKey));

                TrionLogger.Log($"Getting data from {Links.APIRequests.GetSPPVersion(Settings.SupporterKey)}, Response code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<dynamic>();

                    TrionLogger.Log($"Repack Versions updated!", "SUCCESS");

                    // Updating the UI with the latest versions from the online API
                    FormData.UI.Version.Online.Trion = result.trion;
                    FormData.UI.Version.Online.Database = result.database;
                    FormData.UI.Version.Online.Classic = result.classicSPP;
                    FormData.UI.Version.Online.TBC = result.tbcSPP;
                    FormData.UI.Version.Online.WotLK = result.wotlkSPP;
                    FormData.UI.Version.Online.Cata = result.cataSPP;
                    FormData.UI.Version.Online.Mop = result.mopSPP;

                    TrionLogger.Log($"Classic: {result.classicSPP}, TBC: {result.tbcSPP}, WOTLK: {result.wotlkSPP}, CATA: {result.cataSPP}, MOP: {result.mopSPP}", "SUCCESS");
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    TrionLogger.Log($"GetSPPVersionOnline API Error: {response.StatusCode} - {error}", "ERROR");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log network errors
                TrionLogger.Log($"GetSPPVersionOnline Network error: {ex.Message}", "ERROR");
            }
            catch (Exception ex)
            {
                // Log unexpected errors
                TrionLogger.Log($"Unexpected error: {ex.Message}", "ERROR");
            }
        }

        // Retrieves the local version of a given application file located at 'Location'
        public static string GetLocalVersion(string Location)
        {
            try
            {
                if (!string.IsNullOrEmpty(Location))
                {
                    if (Location == "N/A")
                    {
                        return "N/A";
                    }

                    var versionInfo = FileVersionInfo.GetVersionInfo(Location);

                    // Check if the version info contains "SPP"
                    if (versionInfo.FileVersion!.Contains("SPP"))
                    {
                        string pattern = @"\b\d{4}[-/]\d{2}[-/]\d{2}\b";
                        Regex regex = new(pattern);

                        // Find matching date in version info
                        MatchCollection matches = regex.Matches(versionInfo.ToString());

                        foreach (Match match in matches.Cast<Match>())
                        {
                            return match.Value;
                        }
                    }

                    return versionInfo.FileVersion;
                }

                return "N/A";
            }
            catch (Exception ex)
            {
                // Log any errors encountered while retrieving the version
                TrionLogger.Log($@"Failed to get the application version! {ex.Message} {Location}");
                return "N/A";
            }
        }

        // Fetches the local versions of all relevant executables
        public static void GetSPPVersionOffline(AppSettings Settings)
        {
            try
            {
                // Assign local version information to the UI
                FormData.UI.Version.Local.Trion = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
                FormData.UI.Version.Local.Database = GetLocalVersion(Settings.DBExeLoc);
                FormData.UI.Version.Local.Classic = GetLocalVersion(Settings.ClassicWorldExeLoc);
                FormData.UI.Version.Local.TBC = GetLocalVersion(Settings.TBCWorldExeLoc);
                FormData.UI.Version.Local.WotLK = GetLocalVersion(Settings.WotLKWorldExeLoc);
                FormData.UI.Version.Local.Cata = GetLocalVersion(Settings.CataWorldExeLoc);
                FormData.UI.Version.Local.Mop = GetLocalVersion(Settings.MopWorldExeLoc);
            }
            catch (Exception ex)
            {
                // Log any errors encountered
                TrionLogger.Log($"Unexpected error: {ex.Message}", "ERROR");
            }
        }

        // Compares two version strings and returns the result
        private static int VersionCompare(string ver1, string ver2)
        {
            if (ver1 != "N/A" && ver2 != "N/A")
            {
                Version version1 = new(ver1);
                Version version2 = new(ver2);
                return version1.CompareTo(version2);
            }
            return 0;
        }

        // Checks if the online version is newer than the local version
        static bool CheckForUpdate(string localVersion, string onlineVersion)
        {
            return !string.IsNullOrEmpty(localVersion) &&
                   !string.IsNullOrEmpty(onlineVersion) &&
                   VersionCompare(localVersion, onlineVersion) < 0;
        }

        // Checks if the online date is later than the local date
        static bool CheckForDateUpdate(string localDate, string onlineDate)
        {
            return DateTime.TryParse(localDate, out DateTime local) &&
                   DateTime.TryParse(onlineDate, out DateTime online) &&
                   local < online && online != DateTime.MinValue;
        }

        // Checks for updates by comparing local and online versions for all components
        public static void CheckForUpdate(AppSettings Settings)
        {
            // Trion and Database Updates
            FormData.UI.Version.Update.Trion = CheckForUpdate(FormData.UI.Version.Local.Trion, FormData.UI.Version.Online.Trion);
            FormData.UI.Version.Update.Database = CheckForUpdate(FormData.UI.Version.Local.Database, FormData.UI.Version.Online.Database);

            // Single Player Project Updates
            FormData.UI.Version.Update.Classic = CheckForDateUpdate(FormData.UI.Version.Local.Classic, FormData.UI.Version.Online.Classic);
            FormData.UI.Version.Update.TBC = CheckForDateUpdate(FormData.UI.Version.Local.TBC, FormData.UI.Version.Online.TBC);
            FormData.UI.Version.Update.WotLK = CheckForDateUpdate(FormData.UI.Version.Local.WotLK, FormData.UI.Version.Online.WotLK);
            FormData.UI.Version.Update.Cata = CheckForDateUpdate(FormData.UI.Version.Local.Cata, FormData.UI.Version.Online.Cata);
            FormData.UI.Version.Update.Mop = CheckForDateUpdate(FormData.UI.Version.Local.Mop, FormData.UI.Version.Online.Mop);
        }
    }
}
