using System.Text.Json.Serialization;

namespace Trion.Core.Monitoring;

public sealed record MachineMetrics(
    [property: JsonPropertyName("cpuPercent")]   double CpuPercent,
    [property: JsonPropertyName("ramTotalBytes")] long   RamTotalBytes,
    [property: JsonPropertyName("ramUsedBytes")]  long   RamUsedBytes,
    [property: JsonPropertyName("diskReadBps")]   long   DiskReadBytesPerSec,
    [property: JsonPropertyName("diskWriteBps")]  long   DiskWriteBytesPerSec,
    [property: JsonPropertyName("netRxBps")]      long   NetworkRxBytesPerSec,
    [property: JsonPropertyName("netTxBps")]      long   NetworkTxBytesPerSec,
    [property: JsonPropertyName("timestamp")]     DateTimeOffset Timestamp = default);
