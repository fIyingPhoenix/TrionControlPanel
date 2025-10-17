using System.Text.Json.Serialization;

namespace Trion.Core.Monitoring;

public sealed record MachineMetrics(
    [property: JsonPropertyName("cpuPercent")] double CpuPercent,
    [property: JsonPropertyName("ramUsedMb")] long RamUsedMb,
    [property: JsonPropertyName("ramTotalMb")] long RamTotalMb,
    [property: JsonPropertyName("diskRead")] long DiskReadBytes,
    [property: JsonPropertyName("diskWrite")] long DiskWriteBytes,
    [property: JsonPropertyName("netRecv")] long NetRecvBytes,
    [property: JsonPropertyName("netSent")] long NetSentBytes,
    [property: JsonPropertyName("timestamp")] DateTimeOffset Timestamp);