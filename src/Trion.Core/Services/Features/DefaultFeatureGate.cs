using Trion.Core.Abstractions.Services;

namespace Trion.Core.Services.Features;

/// <summary>
/// Default gate that enables all features.
/// Replaced in Phoenix Network builds by a license-checked implementation (PN-1).
/// </summary>
public sealed class DefaultFeatureGate : IFeatureGate
{
    public bool IsEnabled(TrionFeature feature) => true;
}
