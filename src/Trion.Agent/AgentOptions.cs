namespace Trion.Agent;

public sealed class AgentOptions
{
    /// <summary>Named pipe name on Windows.</summary>
    public string PipeName { get; set; } = "TrionAgentPipe";

    /// <summary>Unix socket path on Linux.</summary>
    public string SocketPath { get; set; } = "/run/trion/agent.sock";

    /// <summary>HMAC shared key — set via environment variable Agent__HmacSharedKey.</summary>
    public string HmacSharedKey { get; set; } = string.Empty;
}
