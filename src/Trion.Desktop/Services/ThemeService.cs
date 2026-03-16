using System.Windows;
using System.Windows.Media;

namespace Trion.Desktop.Services;

public class ThemeService : IThemeService
{
    public string CurrentTheme { get; private set; } = "TrionBlue Dark";
    public string CurrentColor { get; private set; } = "TrionBlue";
    public bool   IsDark       { get; private set; } = true;

    public event Action<Color>? AccentChanged;

    public void SetTheme(string themeName)
    {
        if (string.IsNullOrWhiteSpace(themeName)) themeName = "TrionBlue Dark";

        // "TrionBlue Dark" → color = "TrionBlue", variant = "Dark"
        // Split on the LAST space so "TrionBlue" (no internal spaces) is kept whole.
        var sep = themeName.LastIndexOf(' ');
        if (sep > 0)
        {
            CurrentColor = themeName[..sep];
            IsDark       = themeName[(sep + 1)..] != "Light";
        }

        ApplyTheme(themeName);
    }

    public void SetColor(string color)
    {
        CurrentColor = color;
        ApplyTheme($"{CurrentColor} {(IsDark ? "Dark" : "Light")}");
    }

    public void ToggleVariant()
    {
        IsDark = !IsDark;
        ApplyTheme($"{CurrentColor} {(IsDark ? "Dark" : "Light")}");
    }

    // ── Internal ──────────────────────────────────────────────────────────────

    private void ApplyTheme(string themeName)
    {
        var fileName = themeName.Replace(" ", "") + "Theme.xaml";

        var dict     = Application.Current.Resources.MergedDictionaries;
        var existing = dict.FirstOrDefault(
            d => d.Source?.ToString().Contains("Theme", StringComparison.OrdinalIgnoreCase) == true);

        if (existing != null) dict.Remove(existing);

        dict.Insert(0, new ResourceDictionary
        {
            Source = new Uri($"pack://application:,,,/Resources/Themes/{fileName}")
        });

        CurrentTheme = themeName;

        // Notify listeners of the new accent colour
        if (Application.Current.Resources["Accent.Primary"] is SolidColorBrush accentBrush)
            AccentChanged?.Invoke(accentBrush.Color);
    }
}
