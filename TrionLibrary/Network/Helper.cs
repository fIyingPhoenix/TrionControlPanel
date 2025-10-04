using System;
using System.Diagnostics;
using System.Management;
using System.Net.Http;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TrionLibrary.Sys;
using Newtonsoft.Json.Linq;
using TrionLibrary.Crypto;
using System.Security.Policy;
using System.Net;

namespace TrionLibrary.Network
{
    public class Helper
    {
        public static async Task<(string, bool)> IsProcessUsingPort(int processId, int port)
        {
            string Message;
            bool PortInUse;
            if (Watcher.OSRuinning() == "Windows")
            {
                try
                {
                    var tcpConnections = Ports.GetAllTcpConnections();
                    foreach (var conn in tcpConnections)
                    {
                        if (conn.LocalPort == port && conn.ProcessId == processId)
                        {
                            PortInUse = true;
                        }
                    }
                    PortInUse = false;
                    Message = $"PortInUse: {port} by ProcsessID {processId}";
                }
                catch (Exception ex) { Message = ex.Message; PortInUse = false; }
                await Task.Delay(10);
                return (Message, PortInUse);
            }
            else if (Watcher.OSRuinning() == "Unix")
            {
                try
                {
                    string command;
                    string args;

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        command = "netstat";
                        args = $"-ano | findstr :{port}";
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        command = "ss";
                        args = $"-tuln | grep :{port}";
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        command = "netstat";
                        args = $"-an | grep .{port}";
                    }
                    else
                    {
                        throw new NotSupportedException("Unsupported operating system.");
                    }

                    Process process = new();
                    process.StartInfo.FileName = "/bin/bash";
                    process.StartInfo.Arguments = $"-c \"{command} {args}\"";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(output))
                    {
                        Message = $"Port {port} is in use: {output}";
                        PortInUse = true;
                    }
                    else
                    {
                        Message = $"Port {port} is not in use.";
                        PortInUse = false;
                    }
                }
                catch (Exception ex)
                {
                    Message = $"Error: {ex.Message}";
                    PortInUse = false;
                }
                await Task.Delay(10);
                return (Message, PortInUse);
            }
            await Task.Delay(10); Message = ""; PortInUse = false;
            return (Message, PortInUse);
        }
        public static async Task<bool> IsPortOpen(int Port, string Host)
        {
            try
            {
                using TcpClient tcpClient = new();
                await tcpClient.ConnectAsync(Host, Port);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<string> GetExternalIpAddress()
        {
            string externalIpAddress = string.Empty;
            try
            {
                using (HttpClient client = new())
                {
                    HttpResponseMessage response = await client.GetAsync("https://flying-phoenix.dev/api/getip.php");
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        // Parse the JSON response
                        JObject json = JObject.Parse(responseBody);
                        externalIpAddress = json["ip"].ToString();
                    }
                    else
                    {
                        Infos.Message = "Failed to retrieve IP address. Status code: " + response.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                Infos.Message = "Error getting external IP address: " + ex.Message;
            }
            return externalIpAddress;
        }
        public static string GetInternalIpAddress()
        {
            string internalIpAddress = string.Empty;
            try
            {
                NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface networkInterface in networkInterfaces)
                {
                    // Skip loopback and virtual network interfaces
                    if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                        !networkInterface.Description.Contains("virtual", StringComparison.InvariantCultureIgnoreCase) && // Exclude virtual adapters
                        networkInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                        foreach (UnicastIPAddressInformation ipAddress in ipProperties.UnicastAddresses)
                        {
                            if (ipAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                internalIpAddress = ipAddress.Address.ToString();
                            }
                        }
                    }
                }
                return internalIpAddress;
            }
            catch (Exception ex)
            {
                Infos.Message = "Error getting IPv4 addresses: " + ex.Message;
                return internalIpAddress;
            }
        }
        public static async Task<bool> IsWebsiteOnlineAsync(string url)
        {
            try
            {
                using (HttpClient client = new())
                {
                    // Set a timeout to avoid hanging
                    client.Timeout = TimeSpan.FromSeconds(10);

                    // Send a GET request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check if the status code indicates success (200-299)
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException)
            {
                // Handle web exceptions (e.g., network issues)
                return false;
            }
            catch (TaskCanceledException)
            {
                // Handle timeout exception
                return false;
            }
        }

    }
}
