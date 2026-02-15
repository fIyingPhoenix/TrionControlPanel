using System.Diagnostics;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;

namespace TrionControlPanel.Desktop.Extensions.Classes.Network
{
    public class SpeedTestResult
    {
        /// <summary>
        /// Latency or Time to First Byte (TTFB) in milliseconds.
        /// </summary>
        public long LatencyMs { get; set; }

        /// <summary>
        /// Download speed in Megabits per second (Mbps).
        /// </summary>
        public double DownloadSpeedMbps { get; set; }

        /// <summary>
        /// Set if an error occurs during the test.
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    public class SpeedTestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl;

        public SpeedTestService(string baseApiUrl, HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? NetworkManager.SharedClient;
            _baseApiUrl = baseApiUrl.TrimEnd('/');
        }

        /// <summary>
        /// Runs a full speed test, measuring both latency and download speed.
        /// </summary>
        /// <param name="downloadSizeInMB">The size of the file to use for the download test.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        public async Task<SpeedTestResult> RunTestAsync(int downloadSizeInMB = 25, CancellationToken cancellationToken = default)
        {
            var result = new SpeedTestResult();
            try
            {
                // 1. Measure Latency
                result.LatencyMs = await MeasureLatencyAsync(cancellationToken).ConfigureAwait(false);

                // 2. Measure Download Speed
                result.DownloadSpeedMbps = await MeasureDownloadSpeedAsync(downloadSizeInMB, cancellationToken).ConfigureAwait(false);

                TrionLogger.Info($"Speed test complete: Latency={result.LatencyMs}ms, Download={result.DownloadSpeedMbps}Mbps");
            }
            catch (OperationCanceledException)
            {
                result.ErrorMessage = "Speed test was canceled.";
                TrionLogger.Warning("Speed test was canceled.");
            }
            catch (Exception ex)
            {
                result.ErrorMessage = $"Test failed: {ex.Message}";
                TrionLogger.LogException(ex, "SpeedTest");
            }
            return result;
        }

        /// <summary>
        /// Measures the Time to First Byte (TTFB) by hitting a minimal endpoint.
        /// </summary>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        private async Task<long> MeasureLatencyAsync(CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            var requestUrl = $"{_baseApiUrl}/Ping";

            stopwatch.Start();

            // HttpCompletionOption.ResponseHeadersRead is CRITICAL for accurate latency.
            // It ensures the 'await' completes as soon as headers are received, not after the body downloads.
            using (var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                stopwatch.Stop();
                response.EnsureSuccessStatusCode(); // Ensure we got a 2xx response
                return stopwatch.ElapsedMilliseconds;
            }
        }

        /// <summary>
        /// Measures download throughput by timing the download of a file of known size.
        /// </summary>
        /// <param name="sizeInMB">The size of the test file in megabytes.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        private async Task<double> MeasureDownloadSpeedAsync(int sizeInMB, CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            var requestUrl = $"{_baseApiUrl}/DownloadSpeedTest?sizeInMB={sizeInMB}";

            // Make the request and start the timer
            using (var response = await _httpClient.GetAsync(requestUrl, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                stopwatch.Start();

                // Download the entire response body
                var content = await response.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);

                stopwatch.Stop();

                // Get total bytes from the actual downloaded content length
                long totalBytes = content.Length;
                double seconds = stopwatch.Elapsed.TotalSeconds;

                if (seconds == 0) return 0;

                // Calculate speed in Megabits per second (Mbps)
                // (Bytes * 8) -> bits
                // (bits / seconds) -> bits per second
                // (bps / 1_000_000) -> megabits per second
                double speedMbps = totalBytes * 8 / (seconds * 1000 * 1000);

                return Math.Round(speedMbps, 2);
            }
        }
    }
}