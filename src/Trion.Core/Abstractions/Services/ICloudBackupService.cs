namespace Trion.Core.Abstractions.Services;

public interface ICloudBackupService
{
    Task BackupNowAsync(CancellationToken ct = default);
    Task<IReadOnlyList<BackupEntry>> ListBackupsAsync(CancellationToken ct = default);
    Task RestoreAsync(string backupId, string passphrase, CancellationToken ct = default);
    Task<bool> TestConnectionAsync(CancellationToken ct = default);
}
