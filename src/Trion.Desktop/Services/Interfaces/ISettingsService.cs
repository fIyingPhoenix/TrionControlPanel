using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public interface ISettingsService
{
    AppSettings Current { get; }
    void Load();
    void Save();
}
