using System.Text.Json.Serialization;
using Trion.Core.Agent;

namespace Trion.Agent.Json;

/// <summary>
/// Source-generated JSON serializer context — required for AOT (Release) builds.
/// All protocol types used in serialise/deserialise paths must be listed here.
/// </summary>
[JsonSerializable(typeof(AgentCommand))]
[JsonSerializable(typeof(AgentResponse))]
[JsonSerializable(typeof(LaunchProcessCommand))]
[JsonSerializable(typeof(LaunchProcessResponse))]
[JsonSerializable(typeof(KillProcessCommand))]
[JsonSerializable(typeof(KillProcessResponse))]
[JsonSerializable(typeof(ServiceControlCommand))]
[JsonSerializable(typeof(ServiceControlResponse))]
[JsonSerializable(typeof(IsProcessAliveCommand))]
[JsonSerializable(typeof(IsProcessAliveResponse))]
[JsonSerializable(typeof(PingCommand))]
[JsonSerializable(typeof(PingResponse))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
internal partial class AgentJsonContext : JsonSerializerContext { }
