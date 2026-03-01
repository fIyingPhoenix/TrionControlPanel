using System.Runtime.Versioning;
using System.Text.RegularExpressions;

namespace Trion.Platform.Linux;

[SupportedOSPlatform("linux")]
public sealed partial class LinuxDiskReader
{
    private readonly string _diskstatsPath;

    // Exclude loop devices and RAM devices
    [GeneratedRegex(@"^(loop|ram)\d+$", RegexOptions.Compiled)]
    private static partial Regex ExcludedDevice();

    private (long sectorsRead, long sectorsWritten)? _prev;

    public LinuxDiskReader(string diskstatsPath = "/proc/diskstats")
        => _diskstatsPath = diskstatsPath;

    /// <summary>
    /// Returns (bytesRead, bytesWritten) delta since last call.
    /// First call returns (0, 0) to avoid startup spike.
    /// </summary>
    public (long BytesRead, long BytesWritten) GetDiskBytesDelta()
    {
        long totalRead = 0, totalWritten = 0;

        foreach (var line in File.ReadLines(_diskstatsPath))
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            // /proc/diskstats fields: major minor name reads_completed ... sectors_read ... sectors_written
            // Field indices (0-based): 0=major, 1=minor, 2=name, 3=reads, 5=sectors_read, 7=writes, 9=sectors_written
            if (parts.Length < 10) continue;

            var deviceName = parts[2];
            if (ExcludedDevice().IsMatch(deviceName)) continue;

            // Only count physical block devices (single letter + digits, e.g. sda, vda, nvme0n1)
            // Skip partition entries (sda1, nvme0n1p1 etc.) to avoid double-counting
            if (char.IsDigit(deviceName[^1]) && !deviceName.Contains("nvme", StringComparison.Ordinal))
                continue;

            totalRead    += long.Parse(parts[5]);
            totalWritten += long.Parse(parts[9]);
        }

        var cur = (totalRead, totalWritten);

        if (_prev is null)
        {
            _prev = cur;
            return (0L, 0L);
        }

        var delta = (
            BytesRead:    (cur.totalRead    - _prev.Value.sectorsRead)    * 512L,
            BytesWritten: (cur.totalWritten - _prev.Value.sectorsWritten) * 512L
        );

        _prev = cur;
        return delta;
    }
}
