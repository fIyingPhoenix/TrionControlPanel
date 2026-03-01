using System.Runtime.Versioning;
using Trion.Platform.Linux;
using Xunit;

namespace Trion.Platform.Tests.Linux;

[SupportedOSPlatform("linux")]
public sealed class LinuxDiskReaderTests : IDisposable
{
    private readonly string _tmpDir = Path.Combine(Path.GetTempPath(), "TrionDiskTests_" + Guid.NewGuid().ToString("N"));

    public LinuxDiskReaderTests() => Directory.CreateDirectory(_tmpDir);
    public void Dispose() => Directory.Delete(_tmpDir, recursive: true);

    private string WriteDiskstats(string content)
    {
        var path = Path.Combine(_tmpDir, "diskstats");
        File.WriteAllText(path, content);
        return path;
    }

    // /proc/diskstats format (relevant fields):
    // col[0]=major col[1]=minor col[2]=name col[3]=reads col[5]=sectors_read
    // col[7]=writes col[9]=sectors_written
    // Full minimal format: "major minor name reads merged sectors_read ms_read writes merged sectors_written ms_written ..."

    [Fact]
    public void FirstCall_ReturnsZeroDelta()
    {
        var path = WriteDiskstats(
            "   8       0 sda 100 0 2000 0 50 0 1000 0 0 0 0 0 0 0 0 0 0\n");

        var sut = new LinuxDiskReader(path);
        var (r, w) = sut.GetDiskBytesDelta();

        Assert.Equal(0L, r);
        Assert.Equal(0L, w);
    }

    [Fact]
    public void SecondCall_ReturnsDeltaTimesBlockSize()
    {
        var path = WriteDiskstats(
            "   8       0 sda 100 0 2000 0 50 0 1000 0 0 0 0 0 0 0 0 0 0\n");

        var sut = new LinuxDiskReader(path);
        sut.GetDiskBytesDelta(); // seed

        // sectors_read: 2000→4000 (+2000), sectors_written: 1000→2000 (+1000)
        File.WriteAllText(path,
            "   8       0 sda 200 0 4000 0 100 0 2000 0 0 0 0 0 0 0 0 0 0\n");

        var (r, w) = sut.GetDiskBytesDelta();

        Assert.Equal(2000L * 512, r);
        Assert.Equal(1000L * 512, w);
    }

    [Fact]
    public void LoopDevices_AreExcluded()
    {
        var path = WriteDiskstats(
            "   7       0 loop0 50  0 500 0 0 0 0 0 0 0 0 0 0 0 0 0 0\n" +
            "   8       0 sda   100 0 200 0 50 0 100 0 0 0 0 0 0 0 0 0 0\n");

        var sut = new LinuxDiskReader(path);
        sut.GetDiskBytesDelta(); // seed

        // Both loop0 and sda increase; only sda should be counted
        File.WriteAllText(path,
            "   7       0 loop0 150  0 1500 0 0   0 0   0 0 0 0 0 0 0 0 0 0\n" +
            "   8       0 sda   200  0 400  0 100 0 200 0 0 0 0 0 0 0 0 0 0\n");

        var (r, w) = sut.GetDiskBytesDelta();

        // sda: +200 sectors_read, +100 sectors_written
        Assert.Equal(200L * 512, r);
        Assert.Equal(100L * 512, w);
    }

    [Fact]
    public void RamDevices_AreExcluded()
    {
        var path = WriteDiskstats(
            "   1       0 ram0 100 0 200 0 50 0 100 0 0 0 0 0 0 0 0 0 0\n" +
            "   8       0 sda  100 0 200 0 50 0 100 0 0 0 0 0 0 0 0 0 0\n");

        var sut = new LinuxDiskReader(path);
        sut.GetDiskBytesDelta(); // seed

        File.WriteAllText(path,
            "   1       0 ram0 200 0 2000 0 100 0 1000 0 0 0 0 0 0 0 0 0 0\n" +
            "   8       0 sda  200 0 400  0 100 0 200  0 0 0 0 0 0 0 0 0 0\n");

        var (r, w) = sut.GetDiskBytesDelta();

        Assert.Equal(200L * 512, r);  // only sda
        Assert.Equal(100L * 512, w);
    }

    [Fact]
    public void EmptyDiskstats_ReturnsZero()
    {
        var path = WriteDiskstats(string.Empty);
        var sut = new LinuxDiskReader(path);

        var (r, w) = sut.GetDiskBytesDelta();
        Assert.Equal(0L, r);
        Assert.Equal(0L, w);
    }
}
