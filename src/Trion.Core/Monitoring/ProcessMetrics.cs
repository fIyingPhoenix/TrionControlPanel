using System.Text.Json.Serialization;

namespace Trion.Core.Monitoring;

public sealed record ProcessMetrics(
    [property: JsonPropertyName("pid")] int Pid,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("cpu")] double CpuPercent,
    [property: JsonPropertyName("ramMb")] long RamMb,
    [property: JsonPropertyName("diskRead")] long DiskReadBytes,
    [property: JsonPropertyName("diskWrite")] long DiskWriteBytes,
    [property: JsonPropertyName("netRecv")] long NetRecvBytes,
    [property: JsonPropertyName("netSent")] long NetSentBytes,
    [property: JsonPropertyName("timestamp")] DateTimeOffset Timestamp);