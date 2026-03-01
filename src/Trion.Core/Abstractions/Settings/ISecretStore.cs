namespace Trion.Core.Abstractions.Settings;

public interface ISecretStore
{
    string? GetSecret(string key);
    void SetSecret(string key, string value);
    void DeleteSecret(string key);
}
