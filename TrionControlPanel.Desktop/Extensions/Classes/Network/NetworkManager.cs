using Newtonsoft.Json;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Classes.Network
{

    // NetworkManager class for handling network-related operations.
    public class NetworkManager
    {
        // Shared HttpClient instance to prevent socket exhaustion and improve performance.
        public static readonly HttpClient SharedClient = new() { Timeout = TimeSpan.FromSeconds(100) }; // Default timeout

        // Indicates if the application is currently running in offline mode.
        public static bool IsOffline { get; private set; } = true;

        // Sets the API server URL based on the availability of the main and backup hosts using parallel checks.
        public static async Task GetAPIServer()
        {
            // Create a CTS for the connection check timeout (e.g., 5 seconds)
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            var mainTask = CheckHost(Links.MainHost, cts.Token);
            var backupTask = CheckHost(Links.BackupHost, cts.Token);

            // Wait for the first one to succeed, or all to complete/fail
            var completedTask = await Task.WhenAny(mainTask, backupTask).ConfigureAwait(false);

            if (await completedTask.ConfigureAwait(false))
            {
                // One of them succeeded. The helper method already set Links.APIServer
                IsOffline = false;
                return;
            }

            // If the first one failed/timed out, check the other one (it might have finished or failed by now)
            if (mainTask != completedTask && await mainTask.ConfigureAwait(false))
            {
                 IsOffline = false;
                 return;
            }
            if (backupTask != completedTask && await backupTask.ConfigureAwait(false))
            {
                 IsOffline = false;
                 return;
            }

            // Both failed
            Links.APIServer = null!;
            IsOffline = true;
            TrionLogger.Log("Unable to connect to any API server. Switching to Offline Mode.", "WARNING");
        }

        private static async Task<bool> CheckHost(string host, CancellationToken token)
        {
            try
            {
                // Use a dedicated request to respect the short timeout token
                var response = await SharedClient.GetAsync($"{host}/Trion/Ping", token).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    Links.APIServer = host;
                    return true;
                }
            }
            catch
            {
                // Ignore errors during ping (timeout, network unreachable, etc.)
            }
            return false;
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
                await tcpClient.ConnectAsync(Host, Port).ConfigureAwait(false);
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
                TrionLogger.Log($"Getting external IPv4 address from {url}");
                HttpResponseMessage response = await SharedClient.GetAsync(url).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<dynamic>().ConfigureAwait(false);
                    TrionLogger.Log($"Loaded external IPv4 address: {result.iPv4Address}");
                    return result.iPv4Address;
                }
                else
                {
                    TrionLogger.Log($"Error fetching IP {response.StatusCode} ,Url {url}", "ERROR");
                    return "0.0.0.0";
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
                // Use SharedClient but with a specific shorter timeout if needed? 
                // For general usage, the default timeout is fine, but for pings we prefer GetAPIServer logic.
                // Keeping this for backward compat with other calls.
                var response = await SharedClient.GetAsync(url).ConfigureAwait(false);
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
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Links.DDNSWebsites(Settings.DDNSService));
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

        /// <summary>
        /// Performs a full speed test (latency and download) against the API
        /// and logs the results.
        /// </summary>
        public static async Task DownlaodSpeed(string url, int downloadSizeMB, CancellationToken cancellationToken = default)
        {
            TrionLogger.Log("Starting speed test...");

            //Create an instance of our new service.
            var speedTester = new SpeedTestService(url);

            try
            {
                //  Run the test.
                SpeedTestResult result = await speedTester.RunTestAsync(downloadSizeMB, cancellationToken).ConfigureAwait(false);

                // 3. Log the results from the result object.
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    // The service already caught an error, so we just log it.
                    TrionLogger.Log($"Speed test failed: {result.ErrorMessage}", "ERROR");
                }
                else
                {
                    // Log the successful results.
                    TrionLogger.Log($"Latency (TTFB): {result.LatencyMs} ms");
                    TrionLogger.Log($"Download Speed: {result.DownloadSpeedMbps:F2} Mbps");
                    TrionLogger.Log("Speed test completed successfully.");
                }
            }
            catch (OperationCanceledException)
            {
                // This will be caught if the cancellationToken is triggered
                // while the test is running.
                TrionLogger.Log("Speed test was canceled by the user.", "INFO");
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors during the test setup or execution.
                TrionLogger.Log($"An unexpected error occurred during the speed test: {ex.Message}", "ERROR");
            }
        }

        // Gets a list of server files from the specified URL.
        public static async Task<List<FileList>> GetServerFiles(string URL, IProgress<string>? Count = null)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    // Use SharedClient instead of creating new one
                    HttpResponseMessage response = await SharedClient.GetAsync(URL).ConfigureAwait(false);
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
