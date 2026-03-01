using System.Runtime.InteropServices;
using Trion.Platform.Linux;
using Xunit;

namespace Trion.Platform.Tests.Linux;

[Collection("Linux")]
[System.Runtime.Versioning.SupportedOSPlatform("linux")]
public sealed class LinuxMetricsProviderTests : IDisposable
{
    private readonly string _tmpDir = Path.Combine(Path.GetTempPath(), "TrionPlatformTests_" + Guid.NewGuid().ToString("N"));

    public LinuxMetricsProviderTests() => Directory.CreateDirectory(_tmpDir);
    public void Dispose() => Directory.Delete(_tmpDir, recursive: true);

    private string Write(string name, string content)
    {
        var path = Path.Combine(_tmpDir, name);
        File.WriteAllText(path, content);
        return path;
    }

    private LinuxMetricsProvider BuildSut(string procStatPath)
    {
        var diskPath = Write("diskstats", "   8       0 sda 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0");
        var netPath  = Write("net_dev",
            "Inter-|   Receive                                                |  Transmit\n" +
            " face |bytes    packets errs drop fifo frame compressed multicast|bytes    packets errs drop fifo colls carrier compressed\n" +
            "    lo:       0       0    0    0    0     0          0         0        0       0    0    0    0     0       0          0\n" +
            "  eth0:    1024      16    0    0    0     0          0         0     2048      24    0    0    0     0       0          0\n");
        var memPath  = Write("meminfo",
            "MemTotal:       16384000 kB\nMemFree:         8192000 kB\nMemAvailable:    8192000 kB\n");

        return new LinuxMetricsProvider(
            new LinuxDiskReader(diskPath),
            new LinuxNetworkReader(netPath),
            new LinuxRamReader(memPath),
            procStatPath);
    }

    [SkippableFact]
    public void FirstCall_ReturnsCpuZero()
    {
        Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || File.Exists("/.dockerenv") || Directory.Exists("/proc"));

        var procStat = Write("proc_stat",
            "cpu  1000 200 300 4000 50 60 70 80 0 0\n" +
            "cpu0 500 100 150 2000 25 30 35 40 0 0\n");

        var sut     = BuildSut(procStat);
        var metrics = sut.GetSnapshot();

        Assert.Equal(0.0, metrics.CpuPercent);
    }

    [SkippableFact]
    public void SecondCall_ComputesDelta_CorrectPercent()
    {
        Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || Directory.Exists("/proc"));

        // First snapshot: idle=4000, total=5760
        var procStat = Write("proc_stat",
            "cpu  1000 200 300 4000 50 60 70 80 0 0\n");
        var sut = BuildSut(procStat);
        sut.GetSnapshot(); // seed _prevCpu

        // Second snapshot: idle increased by 1000, total increased by 2000
        // => activePercent = (2000 - 1000) / 2000 * 100 = 50%
        File.WriteAllText(procStat,
            "cpu  1500 400 500 5000 100 110 120 130 0 0\n");

        var metrics = sut.GetSnapshot();
        Assert.Equal(50.0, metrics.CpuPercent);
    }

    [SkippableFact]
    public void StealTime_IsCountedInActive()
    {
        Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || Directory.Exists("/proc"));

        // All steal delta; idle delta = 0
        var procStat = Write("proc_stat",
            "cpu  0 0 0 1000 0 0 0 0 0 0\n");
        var sut = BuildSut(procStat);
        sut.GetSnapshot();

        // steal increases by 1000, total increases by 1000, idle unchanged
        // active = 1000, percent = 100%
        File.WriteAllText(procStat,
            "cpu  0 0 0 1000 0 0 0 1000 0 0\n");

        var metrics = sut.GetSnapshot();
        Assert.Equal(100.0, metrics.CpuPercent);
    }

    [SkippableFact]
    public void RamBytes_ParsedCorrectly()
    {
        Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || Directory.Exists("/proc"));

        var procStat = Write("proc_stat_ram", "cpu  0 0 0 0 0 0 0 0 0 0\n");
        var sut = BuildSut(procStat);
        var metrics = sut.GetSnapshot();

        // MemTotal = 16_384_000 kB = 16_777_216_000 bytes
        Assert.Equal(16_384_000L * 1024, metrics.RamTotalBytes);
        // Used = Total - Available = 16_384_000 - 8_192_000 = 8_192_000 kB
        Assert.Equal(8_192_000L * 1024, metrics.RamUsedBytes);
    }
}
