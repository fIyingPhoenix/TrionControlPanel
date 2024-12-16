

using Newtonsoft.Json;

namespace TrionControlPanelDesktop.Extensions.Modules
{
    public class Translator
    {
        private Dictionary<string, string> Translations;

        public void LoadLanguage(string languageCode)
        {
            string filePath = Path.Combine("Languages", $"{languageCode}.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)!;
            }
            else
            {
                throw new FileNotFoundException($"Language file not found: {filePath}");
            }
        }
        public string Translate(string key)
        {
            return Translations != null && Translations.ContainsKey(key)
                ? Translations[key]
                : key; // Fallback to key if not found
        }

    }
}
