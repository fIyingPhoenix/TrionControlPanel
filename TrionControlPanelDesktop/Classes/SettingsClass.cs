using MetroFramework;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using TrionControlPanelDesktop.Controls;
using TrionControlPanelDesktop.FormData;
using TrionDatabase;
using TrionLibrary;

namespace TrionControlPanelDesktop.Classes
{
    public class SettingsClass
    {
        private async void CheckForUpdate()
        {
           
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
                            Data.Message = " DNS update request succeeded!";
                            Data.Settings.IPAddress = ip;
                            return true;
                        }
                        else
                        {
                            // Request failed
                            Data.Message = $"Response status code: {response.StatusCode}";
                            return false;
                        }
                    }
                }
                catch (WebException webEx)
                {
                    // Handle web exceptions, e.g., 404, 500, etc.
                    if (webEx.Response is HttpWebResponse errorResponse)
                    {
                        Data.Message = $"Request failed with status code: {errorResponse.StatusCode}";
                    }
                    else
                    {
                        Data.Message = $"Request failed: {webEx.Message}";
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Data.Message = $"An error occurred: {ex.Message}";
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
                Data.Message = "Trion Control Panel removed from Windows startup successfully.";
            }
            catch (Exception ex)
            {
                Data.Message = "Error removing Trion Control Panel from Windows startup: " + ex.Message;
            }
        }
        public static void AddToStartup(string appName, string executablePath)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)!;
                key.SetValue(appName, executablePath);
                key.Close();
                Data.Message = "Trion Control Panel added to Windows startup successfully.";
            }
            catch (Exception ex)
            {
                Data.Message = "Error adding Trion Control Panel to Windows startup: " + ex.Message;
            }
        }
        public static void DownlaodADDToList(string Weblink)
        {
            Thread DwonloadThread = new(async () =>
            {
                await Task.Run(() => DownloadControl.AddToList(Weblink));
            });
            DwonloadThread.Start();
        }
    }
}
