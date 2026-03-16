using System.ComponentModel;

namespace Trion.Desktop.Services;

public interface ILocalizationService : INotifyPropertyChanged
{
    string this[string key] { get; }
    IReadOnlyList<string> AvailableLanguages { get; }
    void LoadLanguage(string locale);
}
