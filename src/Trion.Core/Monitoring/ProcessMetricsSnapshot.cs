namespace Trion.Core.Monitoring;

internal sealed class ProcessMetricsSnapshot
{
    internal int            Pid          { get; init; }
    internal string         Name         { get; init; } = string.Empty;
    internal double         PrevCpuMs    { get; set; }   // was CpuPercent — stores TotalProcessorTime ms
    internal DateTime       StartTime    { get; init; }  // used for PID reuse detection
    internal long           RamBytes     { get; set; }
    internal long           DiskReadBytes  { get; set; }
    internal long           DiskWriteBytes { get; set; }
    internal long           NetRecvBytes   { get; set; }
    internal long           NetSentBytes   { get; set; }
    internal DateTimeOffset Time         { get; set; } = DateTimeOffset.UtcNow;
}
