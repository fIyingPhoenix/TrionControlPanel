
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
        // Method to get all available languages
        public static List<string> GetAvailableLanguages()
        {
            string languageDir = "Languages";
            if (!Directory.Exists(languageDir))
            {
                throw new DirectoryNotFoundException($"Languages directory not found: {languageDir}");
            }

            var languageFiles = Directory.GetFiles(languageDir, "*.json");
            return languageFiles.Select(Path.GetFileNameWithoutExtension).ToList()!;
        }

    }
}
