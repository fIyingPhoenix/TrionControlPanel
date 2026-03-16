using System.ComponentModel;
using System.IO;
using System.Text.Json;

namespace Trion.Desktop.Services;

public class LocalizationService : ILocalizationService
{
    private Dictionary<string, string> _english = [];
    private Dictionary<string, string> _current  = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    // ── Available languages ───────────────────────────────────────────────────

    public IReadOnlyList<string> AvailableLanguages => ScanAvailableLanguages();

    private static IReadOnlyList<string> ScanAvailableLanguages()
    {
        var dir = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Resources", "Localization");

        if (!Directory.Exists(dir))
            return ["en-US"];

        var locales = Directory
            .EnumerateFiles(dir, "Strings.*.json")
            .Select(f =>
            {
                // "Strings.en-US.json" -> GetFileNameWithoutExtension -> "Strings.en-US"
                var stem = Path.GetFileNameWithoutExtension(f);
                var dot  = stem.IndexOf('.');
                return dot >= 0 ? stem[(dot + 1)..] : null;
            })
            .Where(l => !string.IsNullOrEmpty(l))
            .Order()
            .ToList()!;

        // en-US is always present and always first
        locales.Remove("en-US");
        locales.Insert(0, "en-US");

        return locales.AsReadOnly();
    }

    // ── Indexer: locale string with en-US fallback ────────────────────────────

    public string this[string key]
    {
        get
        {
            if (_current.TryGetValue(key, out var v)) return v;
            if (_english.TryGetValue(key, out var e)) return e;
            return key;   // last resort: return the key name itself
        }
    }

    // ── LoadLanguage ──────────────────────────────────────────────────────────

    public void LoadLanguage(string locale)
    {
        if (string.IsNullOrWhiteSpace(locale)) locale = "en-US";

        // Always load English as the baseline so that any key missing from
        // the selected locale transparently falls back to its English value.
        _english = LoadFile("en-US");

        _current = locale.Equals("en-US", StringComparison.OrdinalIgnoreCase)
            ? _english
            : LoadFile(locale);

        // Notify XAML bindings that use indexer syntax: {Binding [Key]}
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));
    }

    // ── Internal helpers ──────────────────────────────────────────────────────

    private static Dictionary<string, string> LoadFile(string locale)
    {
        var path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Resources", "Localization",
            $"Strings.{locale}.json");

        if (!File.Exists(path)) return [];

        try
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? [];
        }
        catch
        {
            return [];
        }
    }
}
