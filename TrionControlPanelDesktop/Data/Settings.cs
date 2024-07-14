using Microsoft.Win32;
using System.Net;
using TrionLibrary.Setting;
using TrionLibrary.Sys;

namespace TrionControlPanelDesktop.Data
{
    public class Settings
    {
        public static bool AddToListDone { get; set; }
        private async void CheckForUpdate()
        {
            // Single Player Project Update
            if (DateTime.TryParse(User.UI.Version.OFF.WotLK, out DateTime SPPLocal) && DateTime.TryParse(User.UI.Version.ON.WotLK, out DateTime SPPOnline))
            {
                if (SPPLocal < SPPOnline && SPPOnline != DateTime.MinValue)
                {
                    if (Setting.List.AutoUpdateCore)
                    {
                        //do auto update
                    }
                    else
                    {

                    }
                    User.UI.Version.Update.WotLK = true;
                }
                else
                {

                    User.UI.Version.Update.WotLK = false;
                }

            }
            Thread.Sleep(100);
            // MySQL Update
            if (!string.IsNullOrEmpty(User.UI.Version.OFF.Database) && !string.IsNullOrEmpty(User.UI.Version.ON.Database))
            {
                if (VersionCompare(User.UI.Version.OFF.Database, User.UI.Version.ON.Database) < 0)
                {
                    if (Setting.List.AutoUpdateMySQL)
                    {

                    }
                    else
                    {

                    }
                    User.UI.Version.Update.Database = true;
                }
            }
            Thread.Sleep(100);
            // Trion Update
            if (!string.IsNullOrEmpty(User.UI.Version.OFF.Trion) && !string.IsNullOrEmpty(User.UI.Version.ON.Trion))
            {
                if (VersionCompare(User.UI.Version.OFF.Trion, User.UI.Version.ON.Trion) < 0)
                {
                    if (Setting.List.AutoUpdateTrion)
                    {

                    }
                    else
                    {


                    }
                }
                User.UI.Version.Update.Trion = true;
            }
        }
        private static int VersionCompare(string ver1, string ver2)
        {
            if (ver1 != "N/A" && ver2 != "N/A")
            {
                string[] vComps1 = ver1.Split('.');
                string[] vComps2 = ver2.Split('.');
                int[] vNumb1 = Array.ConvertAll(vComps1, int.Parse);
                int[] vNumb2 = Array.ConvertAll(vComps2, int.Parse);

                for (int i = 0; i < Math.Min(vNumb1.Length, vNumb2.Length); i++)
                {
                    if (vNumb1[i] != vNumb2[i])
                    {
                        return vNumb1[i].CompareTo(vNumb2[i]);
                    }
                }
                return vNumb1.Length.CompareTo(vNumb2.Length);
            }
            return 0;
        }
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
