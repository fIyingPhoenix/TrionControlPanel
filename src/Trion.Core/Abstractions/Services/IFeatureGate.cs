namespace Trion.Core.Abstractions.Services;

public enum TrionFeature
{
    EmulatorInstaller,
    ClientDataExtractor,
    CsvExport,
    CloudBackup,
    AutoUpdater,
}

public interface IFeatureGate
{
    bool IsEnabled(TrionFeature feature);
}
