using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Classes
{

    // NetworkManager class for handling network-related operations.
    public class NetworkManager
    {
        // Sets the API server URL based on the availability of the main and backup hosts.
        public static async Task GetAPIServer()
        {
            if (await IsWebsiteOnlineAsync($"{Links.MainHost}/Trion/GetWebsitePing")) { Links.APIServer = Links.MainHost; }
            if (await IsWebsiteOnlineAsync($"{Links.BackupHost}/Trion/GetWebsitePing")) { Links.APIServer = Links.BackupHost; }
            else { Links.APIServer = Links.MainHost; }
        }

        // Checks if the input string is a valid domain name.
        public static bool IsDomainName(string input)
        {
            string pattern = @"^(?!-)[A-Za-z0-9-]{1,63}(?<!-)\.[A-Za-z]{2,6}(\.[A-Za-z]{2,6})?$";
            Regex regex = new(pattern, RegexOptions.Compiled);
            return regex.IsMatch(input);
        }

        // Checks if a specific port is open on the given host.
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

        // Gets the external IP address from the specified URL.
        public static async Task<string> GetExternalIpAddress(string url)
        {
            try
            {
                using (HttpClient client = new())
                {
                    TrionLogger.Log($"Getting external IPv4 address from {url}");
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<dynamic>();
                        TrionLogger.Log($"Loaded external IPv4 address: {result.iPv4Address}");
                        return result.iPv4Address;
                    }
                    else
                    {
                        TrionLogger.Log($"Error fetching IP {response.StatusCode} ,Url {url}", "ERROR");
                        return "0.0.0.0";
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                TrionLogger.Log($"HTTP request error: {ex.Message}", "ERROR");
                return "0.0.0.0";
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error fetching IP Message {ex.Message},Url {url}", "ERROR");
                return "0.0.0.0";
            }
        }

        // Gets the internal IP address of the local machine.
        public static string GetInternalIpAddress()
        {
            try
            {
                foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (networkInterface.OperationalStatus == OperationalStatus.Up &&
                        networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                        networkInterface.NetworkInterfaceType != NetworkInterfaceType.Ppp &&
                        !networkInterface.Description.ToLower().Contains("virtual") &&
                        !networkInterface.Description.ToLower().Contains("vmware") &&
                        !networkInterface.Description.ToLower().Contains("hyper-v"))
                    {
                        foreach (UnicastIPAddressInformation ipAddressInfo in networkInterface.GetIPProperties().UnicastAddresses)
                        {
                            if (ipAddressInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                TrionLogger.Log($"Loaded internal IPv4 address {ipAddressInfo.Address}");
                                return ipAddressInfo.Address.ToString();
                            }
                        }
                    }
                }
                TrionLogger.Log($"No active physical IPv4 address found!", "ERROR");
                return "0.0.0.0";
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"No active physical IPv4 address found! {ex.Message}", "ERROR");
                return "0.0.0.0";
            }
        }

        // Checks if a website is online by sending an HTTP GET request.
        public static async Task<bool> IsWebsiteOnlineAsync(string url)
        {
            TrionLogger.Log($"Website: {url} Checking");
            try
            {
                using HttpClient httpClient = new();
                var response = await httpClient.GetAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                TrionLogger.Log($"Website: {url} HTTP request error: {ex.Message}");
                return false;
            }
            catch (TaskCanceledException ex)
            {
                TrionLogger.Log($"Website: {url} Timeout: {ex.Message}");
                return false;
            }
        }

        // Updates the DNS IP address for the specified settings.
        public static bool UpdateDNSIP(AppSettings Settings)
        {
            if (!string.IsNullOrEmpty(Settings.DDNSDomain) && !string.IsNullOrEmpty(Settings.IPAddress))
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Links.DDNSWebsits(Settings.DDNSerivce));
                    request.Method = "GET";

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            TrionLogger.Log($"DNS update request succeeded! IP{Settings.IPAddress}, Domain: {Settings.DDNSDomain}");
                            return true;
                        }
                        else
                        {
                            TrionLogger.Log($"Response status code: {response.StatusCode}", "ERROR");
                            return false;
                        }
                    }
                }
                catch (WebException webEx)
                {
                    if (webEx.Response is HttpWebResponse errorResponse)
                    {
                        TrionLogger.Log($"Request failed with status code: {errorResponse.StatusCode}", "ERROR");
                    }
                    else
                    {
                        TrionLogger.Log($"Request failed: {webEx.Message}", "ERROR");
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    TrionLogger.Log($"An error occurred: {ex.Message}", "ERROR");
                    return false;
                }
            }
            return false;
        }

        // Downloads a speed test file to measure download speed.
        public static async Task DownlaodSeppd(CancellationToken cancellationToken = default)
        {
            string apiUrl = "https://localhost:7107/Trion/DownloadSpeed"; // Replace with your actual server URL

            using HttpClient client = new();
            client.Timeout = TimeSpan.FromMinutes(1); // Ensure enough time

            byte[] buffer = new byte[4 * 1024 * 1024]; // 4 MB buffer
            long totalBytesRead = 0;
            int bytesRead;

            try
            {
                using HttpResponseMessage response = await client.GetAsync(apiUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                Stopwatch stopwatch = Stopwatch.StartNew();

                while (!cancellationToken.IsCancellationRequested && (bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    totalBytesRead += bytesRead;
                }

                stopwatch.Stop();
                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                double speedMbps = (totalBytesRead * 8) / (elapsedSeconds * 1_000_000); // Convert to Mbps

                TrionLogger.Log($"Downloaded {totalBytesRead / (1024.0 * 1024.0):F2} MB in {elapsedSeconds:F2} seconds.");
                TrionLogger.Log($"Estimated Download Speed: {speedMbps:F2} Mbps");
            }
            catch (OperationCanceledException)
            {
                TrionLogger.Log("Speed test stopped after 4 seconds.");
            }
            catch (HttpRequestException ex)
            {
                TrionLogger.Log($"HTTP request error: {ex.Message}", "ERROR");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error: {ex.Message}", "ERROR");
            }
        }

        // Gets a list of server files from the specified URL.
        public static async Task<List<FileList>> GetServerFiles(string URL, IProgress<string>? Count = null)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    using HttpClient httpClient = new();
                    HttpResponseMessage response = await httpClient.GetAsync(URL).ConfigureAwait(false);
                    TrionLogger.Log($"Getting data from {URL}, Response code: {response.StatusCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var filesObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                        List<FileList> fileList = new List<FileList>();

                        await Task.Run(() =>
                        {
                            foreach (var file in filesObject!.files)
                            {
                                FileList fileItem = new()
                                {
                                    Name = file.name,
                                    Size = file.size,
                                    Hash = file.hash,
                                    Path = file.path
                                };

                                lock (fileList)
                                {
                                    fileList.Add(fileItem);
                                }

                                Count?.Report($"{fileList.Count} / {filesObject!.files.Count}");
                            }
                        });

                        return fileList;
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        TrionLogger.Log($"GetServerFiles API Error: {response.StatusCode} - {error}", "ERROR");
                        return null!;
                    }
                });

                return await task.ConfigureAwait(false);
            }
            catch (HttpRequestException ex)
            {
                TrionLogger.Log($"GetServerFiles Network error: {ex.Message}", "ERROR");
                return null!;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Unexpected error: {ex.Message}", "ERROR");
                return null!;
            }
        }
    }
}
