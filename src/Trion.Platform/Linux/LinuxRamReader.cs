using System.Runtime.Versioning;

namespace Trion.Platform.Linux;

[SupportedOSPlatform("linux")]
public sealed class LinuxRamReader
{
    private readonly string _memInfoPath;

    public LinuxRamReader(string memInfoPath = "/proc/meminfo")
        => _memInfoPath = memInfoPath;

    /// <summary>
    /// Reads <c>/proc/meminfo</c> and returns (totalBytes, usedBytes).
    /// Used = Total - MemAvailable.
    /// </summary>
    public (long TotalBytes, long UsedBytes) GetRamBytes()
    {
        long total = 0, available = 0;

        foreach (var line in File.ReadLines(_memInfoPath))
        {
            if (line.StartsWith("MemTotal:", StringComparison.Ordinal))
                total = ParseKb(line);
            else if (line.StartsWith("MemAvailable:", StringComparison.Ordinal))
                available = ParseKb(line);

            if (total > 0 && available > 0) break; // both found, no need to read further
        }

        var totalBytes = total     * 1024L;
        var usedBytes  = (total - available) * 1024L;
        return (totalBytes, usedBytes);
    }

    private static long ParseKb(string line)
    {
        // Format: "MemTotal:       16384000 kB"
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length >= 2 && long.TryParse(parts[1], out var v) ? v : 0;
    }
}
