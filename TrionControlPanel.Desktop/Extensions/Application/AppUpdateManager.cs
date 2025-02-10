﻿using MySqlX.XDevAPI.Common;
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
        public static async Task GetSPPVersionOnline(AppSettings Settings)
        {
            try
            {
                using HttpClient httpClient = new();
                HttpResponseMessage response = await httpClient.GetAsync(Links.APIRequests.GetSPPVersion(Settings.SupporterKey));
                await TrionLogger.Log($"Getting data from {Links.APIRequests.GetSPPVersion(Settings.SupporterKey)}, Response code : {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<dynamic>();

                    await TrionLogger.Log($"Repack Versions updated!", "SUCCESS");
                    FormData.UI.Version.Online.Trion = result.trion;
                    FormData.UI.Version.Online.Database = result.database;
                    FormData.UI.Version.Online.Classic = result.classicSPP;
                    FormData.UI.Version.Online.TBC = result.tbcSPP;
                    FormData.UI.Version.Online.WotLK = result.wotlkSPP;
                    FormData.UI.Version.Online.Cata = result.cataSPP;
                    FormData.UI.Version.Online.Mop = result.mopSPP;
                    await TrionLogger.Log(
                        $"Classic:{result.classicSPP} ,TBC: {result.tbcSPP}, WOTLK:{result.wotlkSPP}, CATA:{result.cataSPP}, MOP: {result.mopSPP}", "SUCCESS");
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    await TrionLogger.Log($"GetSPPVersionOnline API Error: {response.StatusCode} - {error}", "ERROR");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log or rethrow the exception
                await TrionLogger.Log($"GetSPPVersionOnline Network error: {ex.Message}", "ERROR");

            }
            catch (Exception ex)
            {
                // Log or rethrow the exception
                await TrionLogger.Log($"Unexpected error: {ex.Message}", "ERROR");

            }
        }
        public static async Task<string> GetLocalVersion(string Location)
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
                    // Define a regular expression pattern to match dates in yyyy-MM-dd or yyyy/MM/dd format
                    if (versionInfo.FileVersion!.Contains("SPP"))
                    {
                        string pattern = @"\b\d{4}[-/]\d{2}[-/]\d{2}\b";
                        // Create a Regex object with the pattern
                        Regex regex = new(pattern);
                        // Find all matches in the text
                        MatchCollection matches = regex.Matches(versionInfo.ToString());
                        // Print each match
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
                await TrionLogger.Log($@"Failed to get the application version! {ex.Message} {Location}");
                return "N/A";
            }
        }
        public static async Task GetSPPVersionOffline(AppSettings Settings)
        {
            try
            {
                FormData.UI.Version.Local.Trion = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
                FormData.UI.Version.Local.Database = await GetLocalVersion(Settings.DBExeLoc);
                FormData.UI.Version.Local.Classic = await GetLocalVersion(Settings.ClassicWorldExeLoc);
                FormData.UI.Version.Local.TBC = await GetLocalVersion(Settings.TBCWorldExeLoc);
                FormData.UI.Version.Local.WotLK = await GetLocalVersion(Settings.WotLKWorldExeLoc);
                FormData.UI.Version.Local.Cata = await GetLocalVersion(Settings.CataWorldExeLoc);
                FormData.UI.Version.Local.Mop = await GetLocalVersion(Settings.MopWorldExeLoc);
            }
            catch (Exception ex)
            {
                // Log or rethrow the exception
                await TrionLogger.Log($"Unexpected error: {ex.Message}", "ERROR");

            }
        }
        private static int VersionCompare(string ver1, string ver2)
        {
            if (ver1 != "N/A" && ver2 != "N/A")
            {
                Version version1 = new(ver1);
                Version version2 = new(ver2);
                int comparisonResult = version1.CompareTo(version2);
                return comparisonResult;
            }
            return 0;
        }

        static bool CheckForUpdate(string localVersion, string onlineVersion)
        {
            return !string.IsNullOrEmpty(localVersion) &&
                   !string.IsNullOrEmpty(onlineVersion) &&
                   VersionCompare(localVersion, onlineVersion) < 0;
        }

        static bool CheckForDateUpdate(string localDate, string onlineDate)
        {
            return DateTime.TryParse(localDate, out DateTime local) &&
                   DateTime.TryParse(onlineDate, out DateTime online) &&
                   local < online && online != DateTime.MinValue;
        }
        public static void CheckForUpdate(AppSettings Settings)
        {
            // Trion and Database Updates
            FormData.UI.Version.Update.Trion = CheckForUpdate(FormData.UI.Version.Local.Trion, FormData.UI.Version.Online.Trion);
            FormData.UI.Version.Update.Trion = CheckForUpdate(FormData.UI.Version.Local.Database, FormData.UI.Version.Online.Database);
            // Single Player Project Updates
            FormData.UI.Version.Update.Classic = CheckForDateUpdate(FormData.UI.Version.Local.Classic, FormData.UI.Version.Online.Classic);
            FormData.UI.Version.Update.TBC = CheckForDateUpdate(FormData.UI.Version.Local.TBC, FormData.UI.Version.Online.TBC);
            FormData.UI.Version.Update.WotLK = CheckForDateUpdate(FormData.UI.Version.Local.WotLK, FormData.UI.Version.Online.WotLK);
            FormData.UI.Version.Update.Cata = CheckForDateUpdate(FormData.UI.Version.Local.Cata, FormData.UI.Version.Online.Cata);
            FormData.UI.Version.Update.Mop = CheckForDateUpdate(FormData.UI.Version.Local.Mop, FormData.UI.Version.Online.Mop);
        
        }
    }
}
