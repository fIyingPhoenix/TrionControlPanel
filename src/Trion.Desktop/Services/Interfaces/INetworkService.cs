namespace Trion.Desktop.Services;

public interface INetworkService
{
    /// <summary>
    /// Returns all active, non-virtual, non-loopback IPv4 addresses on this machine.
    /// </summary>
    IReadOnlyList<string> GetInternalIpv4Addresses();

    /// <summary>
    /// Fetches the public IPv4 address from the Trion API
    /// (falls back to api.ipify.org on failure).
    /// </summary>
    Task<string> GetPublicIpv4Async(CancellationToken ct = default);
}
