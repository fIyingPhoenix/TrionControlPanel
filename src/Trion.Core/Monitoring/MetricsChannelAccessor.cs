using System.Threading.Channels;
using Trion.Core.Abstractions.Monitoring;

namespace Trion.Core.Monitoring;

/// <summary>
/// Singleton that owns two bounded <see cref="Channel{T}"/> instances —
/// one for machine metrics and one for process snapshots.
/// Background services write; Blazor components / API endpoints read.
/// </summary>
public sealed class MetricsChannelAccessor
{
    private static readonly BoundedChannelOptions ChannelOpts = new(capacity: 10)
    {
        FullMode    = BoundedChannelFullMode.DropOldest,
        SingleWriter = true,
        SingleReader = false
    };

    private readonly Channel<MachineMetrics>  _machine  = Channel.CreateBounded<MachineMetrics>(ChannelOpts);
    private readonly Channel<ProcessMetrics[]> _process = Channel.CreateBounded<ProcessMetrics[]>(ChannelOpts);

    // Writers — only used by MachineMetricsWorker / ProcessMonitor
    internal ChannelWriter<MachineMetrics>   MachineWriter => _machine.Writer;
    internal ChannelWriter<ProcessMetrics[]> ProcessWriter => _process.Writer;

    // Last cached snapshot for non-blocking reads (API endpoints)
    private volatile MachineMetrics?  _lastMachine;
    private volatile ProcessMetrics[]? _lastProcess;

    internal void SetLastMachine(MachineMetrics m)   => _lastMachine = m;
    internal void SetLastProcess(ProcessMetrics[] p) => _lastProcess = p;

    public MachineMetrics?   LastMachine => _lastMachine;
    public ProcessMetrics[]? LastProcess => _lastProcess;

    /// <summary>
    /// Creates a new reader that receives every metric update written to the channels.
    /// Callers should dispose the reader when done to release resources.
    /// </summary>
    public IMetricsChannelReader CreateReader()
        => new MetricsChannelReader(_machine.Reader, _process.Reader);
}
