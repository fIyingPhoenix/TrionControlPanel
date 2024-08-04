using Microsoft.Win32;
using System.Net;
using TrionLibrary.Setting;
using TrionLibrary.Sys;

namespace TrionControlPanelDesktop.Settings
{
    public class SettingsData
    {
        public static string GetWorkingDirectory()
        {
            using FolderBrowserDialog FolderDialog = new();
            // Set the initial selected folder
            FolderDialog.SelectedPath = Directory.GetCurrentDirectory();
            // Set the title of the dialog
            FolderDialog.Description = "Select a folder";
            // Show the folder browser dialog
            DialogResult result = FolderDialog.ShowDialog();

            // Check if the user clicked OK
            if (result == DialogResult.OK)
            {
                // Return the selected folder path
                return FolderDialog.SelectedPath;
            }
            else
            {
                // Return empty string 
                return string.Empty;
            }
        }
        public static bool UpdateDNSIP(string url, string ip)
        {
            if (!string.IsNullOrEmpty(ip))
            {
                try
                {
                    // Create a request for the URL
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";

                    // Get the response
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        // Check the status code
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            // Request succeeded
                            Infos.Message = " DNS update request succeeded!";
                            Setting.List.IPAddress = ip;
                            return true;
                        }
                        else
                        {
                            // Request failed
                            Infos.Message = $"Response status code: {response.StatusCode}";
                            return false;
                        }
                    }
                }
                catch (WebException webEx)
                {
                    // Handle web exceptions, e.g., 404, 500, etc.
                    if (webEx.Response is HttpWebResponse errorResponse)
                    {
                        Infos.Message = $"Request failed with status code: {errorResponse.StatusCode}";
                    }
                    else
                    {
                        Infos.Message = $"Request failed: {webEx.Message}";
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Infos.Message = $"An error occurred: {ex.Message}";
                    return false;
                }
            }
            return false;
        }
        public static void RemoveFromStartup(string appName)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)!;
                key.DeleteValue(appName, false);
                key.Close();
                Infos.Message = "Trion Control Panel removed from Windows startup successfully.";
            }
            catch (Exception ex)
            {
                Infos.Message = "Error removing Trion Control Panel from Windows startup: " + ex.Message;
            }
        }
        public static void AddToStartup(string appName, string executablePath)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)!;
                key.SetValue(appName, executablePath);
                key.Close();
                Infos.Message = "Trion Control Panel added to Windows startup successfully.";
            }
            catch (Exception ex)
            {
                Infos.Message = "Error adding Trion Control Panel to Windows startup: " + ex.Message;
            }
        } 
    }
}
