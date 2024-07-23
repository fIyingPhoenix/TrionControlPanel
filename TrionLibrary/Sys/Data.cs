using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrionLibrary.Sys
{
    public class Infos
    {
        //Error Message (4 later) dont feel do it now
        public static string Message = string.Empty;
        public class Version
        {
            public static async Task<string> Online(string WebLink)
            {
                try
                {
                    using HttpClient client = new();
                    if (!string.IsNullOrEmpty(WebLink))
                    {
                        HttpResponseMessage response = await client.GetAsync(WebLink);
                        if (response.IsSuccessStatusCode)
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            return $"N/A";
                        }
                    }
                    else
                    {
                        return $"N/A";
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    return $"N/A";
                }

            }
            public static string Local(string Location)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Location))
                    {
                        var versionInfo = FileVersionInfo.GetVersionInfo(Location);
                        // Define a regular expression pattern to match dates in yyyy-MM-dd or yyyy/MM/dd format
                        if (versionInfo.FileVersion.Contains("SPP"))
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
                        return "N/A";
                    }
                    else
                    {
                        var versionInfo = FileVersionInfo.GetVersionInfo(Location);
                        return versionInfo.FileVersion;
                    }
                }
                catch (Exception ex)
                {
                    Message = $@"Failed to get the application version! {ex.Message}";
                    return "N/A";
                }
            }
        }
        public static string GetExecutableLocation(string location, string Executable)
        {
            //Search for files in a directory and all subdirectories
            if (Executable != null)
            {
                foreach (string f in Directory.EnumerateFiles(location, $"{Executable}.exe", SearchOption.AllDirectories))
                {
                    return f;
                }
            }
            return string.Empty;
        }
    }
}
