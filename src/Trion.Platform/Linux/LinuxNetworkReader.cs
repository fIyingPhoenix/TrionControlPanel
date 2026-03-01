using System.Runtime.Versioning;

namespace Trion.Platform.Linux;

[SupportedOSPlatform("linux")]
public sealed class LinuxNetworkReader
{
    private readonly string _netDevPath;
    private (long rx, long tx)? _prev;

    public LinuxNetworkReader(string netDevPath = "/proc/net/dev")
        => _netDevPath = netDevPath;

    /// <summary>
    /// Returns (bytesReceived, bytesSent) delta since last call.
    /// First call returns (0, 0) to avoid startup spike.
    /// </summary>
    public (long BytesReceived, long BytesSent) GetNetworkBytesDelta()
    {
        long totalRx = 0, totalTx = 0;
        int  lineNum = 0;

        foreach (var line in File.ReadLines(_netDevPath))
        {
            lineNum++;
            if (lineNum <= 2) continue; // skip header lines

            var trimmed = line.Trim();
            var colonIdx = trimmed.IndexOf(':');
            if (colonIdx < 0) continue;

            var iface = trimmed[..colonIdx].Trim();
            if (iface == "lo") continue; // skip loopback

            var fields = trimmed[(colonIdx + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            // /proc/net/dev fields after colon: rx_bytes rx_packets rx_errs rx_drop ... tx_bytes tx_packets ...
            // field[0] = rx_bytes, field[8] = tx_bytes
            if (fields.Length < 9) continue;

            totalRx += long.Parse(fields[0]);
            totalTx += long.Parse(fields[8]);
        }

        var cur = (totalRx, totalTx);

        if (_prev is null)
        {
            _prev = cur;
            return (0L, 0L);
        }

        var delta = (
            BytesReceived: cur.totalRx - _prev.Value.rx,
            BytesSent:     cur.totalTx - _prev.Value.tx
        );

        _prev = cur;
        return delta;
    }
}
