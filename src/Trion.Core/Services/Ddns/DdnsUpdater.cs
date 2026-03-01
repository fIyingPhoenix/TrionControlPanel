using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Services;
using Trion.Core.Abstractions.Settings;

namespace Trion.Core.Services.Ddns;

public sealed class DdnsUpdater : IDdnsUpdater
{
    private const string LastResultKey = "ddns:last_result";

    private static readonly Uri IpifyUri =
        new("https://api.ipify.org?format=text");

    private readonly HttpClient          _http;
    private readonly IOptions<DdnsOptions>   _options;
    private readonly ISettingsRepository _settings;
    private readonly ILogger<DdnsUpdater>    _logger;

    public DdnsUpdater(
        HttpClient          http,
        IOptions<DdnsOptions>   options,
        ISettingsRepository settings,
        ILogger<DdnsUpdater>    logger)
    {
        _http     = http;
        _options  = options;
        _settings = settings;
        _logger   = logger;
    }

    public async Task UpdateNowAsync(CancellationToken ct = default)
    {
        var opts = _options.Value;
        if (opts.Provider == DdnsProvider.None)
            return;

        DdnsUpdateResult result;
        try
        {
            var ip = await GetPublicIpAsync(ct);
            result = opts.Provider switch
            {
                DdnsProvider.NoIp       => await UpdateNoIpAsync(ip, opts, ct),
                DdnsProvider.DuckDns    => await UpdateDuckDnsAsync(ip, opts, ct),
                DdnsProvider.Cloudflare => await UpdateCloudflareAsync(ip, opts, ct),
                _                       => new DdnsUpdateResult(false, null, DateTimeOffset.UtcNow,
                                               "Unknown provider.")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DDNS update failed.");
            result = new DdnsUpdateResult(false, null, DateTimeOffset.UtcNow, ex.Message);
        }

        await _settings.SetAsync(LastResultKey, result, ct);

        if (result.Success)
            _logger.LogInformation("DDNS updated to {Ip}.", result.NewIp);
        else
            _logger.LogWarning("DDNS update failed: {Error}", result.ErrorMessage);
    }

    public Task<DdnsUpdateResult?> GetLastResultAsync() =>
        _settings.GetAsync<DdnsUpdateResult>(LastResultKey);

    // ── Public IP detection ────────────────────────────────────────────────────

    private async Task<string> GetPublicIpAsync(CancellationToken ct)
    {
        var ip = (await _http.GetStringAsync(IpifyUri, ct)).Trim();
        if (string.IsNullOrEmpty(ip))
            throw new InvalidOperationException("Could not detect public IP address.");
        return ip;
    }

    // ── Provider implementations ───────────────────────────────────────────────

    private async Task<DdnsUpdateResult> UpdateNoIpAsync(
        string ip, DdnsOptions opts, CancellationToken ct)
    {
        var credentials = Convert.ToBase64String(
            Encoding.ASCII.GetBytes($"{opts.Username}:{opts.Password}"));

        var uri = "https://dynupdate.no-ip.com/nic/update" +
                  $"?hostname={Uri.EscapeDataString(opts.Hostname)}" +
                  $"&myip={Uri.EscapeDataString(ip)}";

        using var req = new HttpRequestMessage(HttpMethod.Get, uri);
        req.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        req.Headers.UserAgent.ParseAdd("TrionControlPanel/1.0");

        using var resp = await _http.SendAsync(req, ct);
        var body = (await resp.Content.ReadAsStringAsync(ct)).Trim();

        // No-IP returns "good <ip>" or "nochg <ip>" on success
        return body.StartsWith("good ", StringComparison.Ordinal) ||
               body.StartsWith("nochg ", StringComparison.Ordinal)
            ? new DdnsUpdateResult(true, ip, DateTimeOffset.UtcNow)
            : new DdnsUpdateResult(false, null, DateTimeOffset.UtcNow, $"No-IP: {body}");
    }

    private async Task<DdnsUpdateResult> UpdateDuckDnsAsync(
        string ip, DdnsOptions opts, CancellationToken ct)
    {
        // Strip ".duckdns.org" suffix if the user supplied the full hostname
        var domain = opts.Hostname.Replace(
            ".duckdns.org", string.Empty, StringComparison.OrdinalIgnoreCase);

        var uri = "https://www.duckdns.org/update" +
                  $"?domains={Uri.EscapeDataString(domain)}" +
                  $"&token={Uri.EscapeDataString(opts.Token)}" +
                  $"&ip={Uri.EscapeDataString(ip)}";

        var body = (await _http.GetStringAsync(uri, ct)).Trim();

        return body.Equals("OK", StringComparison.OrdinalIgnoreCase)
            ? new DdnsUpdateResult(true, ip, DateTimeOffset.UtcNow)
            : new DdnsUpdateResult(false, null, DateTimeOffset.UtcNow, $"DuckDNS: {body}");
    }

    private async Task<DdnsUpdateResult> UpdateCloudflareAsync(
        string ip, DdnsOptions opts, CancellationToken ct)
    {
        var uri = $"https://api.cloudflare.com/client/v4/zones/{opts.ZoneId}" +
                  $"/dns_records/{opts.RecordId}";

        var body = JsonSerializer.Serialize(new
        {
            type    = "A",
            name    = opts.Hostname,
            content = ip,
            ttl     = 60
        });

        using var req = new HttpRequestMessage(HttpMethod.Put, uri)
        {
            Content = new StringContent(body, Encoding.UTF8, "application/json")
        };
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", opts.ApiToken);

        using var resp = await _http.SendAsync(req, ct);
        var respBody = await resp.Content.ReadAsStringAsync(ct);

        if (!resp.IsSuccessStatusCode)
            return new DdnsUpdateResult(false, null, DateTimeOffset.UtcNow,
                $"Cloudflare HTTP {(int)resp.StatusCode}: {respBody}");

        using var doc = JsonDocument.Parse(respBody);
        return doc.RootElement.TryGetProperty("success", out var success) && success.GetBoolean()
            ? new DdnsUpdateResult(true, ip, DateTimeOffset.UtcNow)
            : new DdnsUpdateResult(false, null, DateTimeOffset.UtcNow, $"Cloudflare: {respBody}");
    }
}
