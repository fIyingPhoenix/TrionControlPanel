using System.Windows.Media;

namespace Trion.Desktop.Services;

public interface IThemeService
{
    string CurrentTheme { get; }
    string CurrentColor { get; }
    bool   IsDark       { get; }

    /// <summary>Fired on the UI thread whenever the accent colour changes.</summary>
    event Action<Color>? AccentChanged;

    void SetTheme(string themeName);
    void SetColor(string color);
    void ToggleVariant();
}
