namespace Trion.Core.Agent;

/// <summary>
/// Configuration for <c>AgentClient</c> (the Trion.Web side of the IPC channel).
/// Bind from the <c>"Agent"</c> configuration section — same section the agent
/// reads its own <c>AgentOptions</c> from, so a single config block is shared.
/// </summary>
public sealed class AgentClientOptions
{
    /// <summary>Named pipe name used on Windows.</summary>
    public string PipeName      { get; set; } = "TrionAgentPipe";

    /// <summary>Unix domain socket path used on Linux.</summary>
    public string SocketPath    { get; set; } = "/run/trion/agent.sock";

    /// <summary>
    /// HMAC-SHA256 shared secret.  Must match <c>AgentOptions.HmacSharedKey</c>
    /// on the agent side.  Set via environment variable <c>Agent__HmacSharedKey</c>.
    /// </summary>
    public string HmacSharedKey { get; set; } = string.Empty;

    /// <summary>Per-operation timeout in seconds (default 30).</summary>
    public int TimeoutSeconds   { get; set; } = 30;
}
