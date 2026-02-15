// =============================================================================
// Translator.cs
// Purpose: Provides UI localization by loading and retrieving translations from JSON files
// Used by: MainForm, LocalizationService, AlertBox
// Steps 6, 9 of IMPROVEMENTS.md - XML Documentation, Region-based organization
// =============================================================================

using Newtonsoft.Json;

namespace TrionControlPanelDesktop.Extensions.Modules
{
    /// <summary>
    /// Provides localization functionality by loading translations from JSON language files.
    /// </summary>
    /// <remarks>
    /// Language files are stored in the "Languages" folder with naming convention: {languageCode}.json
    /// For example: enUS.json, deDE.json, frFR.json
    ///
    /// JSON format:
    /// <code>
    /// {
    ///   "BTNStartDatabaseTextOFF": "Start Database",
    ///   "BTNStartDatabaseTextON": "Stop Database",
    ///   "LBLCardMachineResourcesTitle": "Machine Resources"
    /// }
    /// </code>
    ///
    /// Usage:
    /// <code>
    /// var translator = new Translator();
    /// translator.LoadLanguage("enUS");
    /// string text = translator.Translate("BTNStartDatabaseTextOFF");
    /// </code>
    /// </remarks>
    public class Translator
    {
        #region Fields
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Dictionary containing all loaded translations for the current language.
        /// Keys are translation identifiers, values are the translated text.
        /// </summary>
        private Dictionary<string, string>? Translations;

        #endregion

        #region Public Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Loads translations from a language file.
        /// </summary>
        /// <param name="languageCode">
        /// The language code corresponding to the JSON file name (without extension).
        /// Examples: "enUS", "deDE", "frFR", "esES"
        /// </param>
        /// <exception cref="FileNotFoundException">
        /// Thrown when the language file cannot be found at Languages/{languageCode}.json
        /// </exception>
        /// <remarks>
        /// The language file must be a valid JSON file containing key-value pairs.
        /// This method replaces any previously loaded translations.
        /// </remarks>
        /// <example>
        /// <code>
        /// var translator = new Translator();
        /// translator.LoadLanguage("enUS");  // Loads Languages/enUS.json
        /// translator.LoadLanguage("deDE");  // Switches to German
        /// </code>
        /// </example>
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

        /// <summary>
        /// Retrieves the translated text for a given key.
        /// </summary>
        /// <param name="key">The translation key to look up.</param>
        /// <returns>
        /// The translated text if found, or the key itself as a fallback if the
        /// translation is not found or no language has been loaded.
        /// </returns>
        /// <remarks>
        /// This method never throws an exception. If a translation is missing,
        /// the key is returned, making it easy to identify missing translations
        /// in the UI (they appear as their key names).
        /// </remarks>
        /// <example>
        /// <code>
        /// string buttonText = translator.Translate("BTNStartDatabaseTextOFF");
        /// // Returns "Start Database" if found, or "BTNStartDatabaseTextOFF" if not
        /// </code>
        /// </example>
        public string Translate(string key)
        {
            return Translations != null && Translations.TryGetValue(key, out var value)
                ? value
                : key; // Fallback to key if not found
        }

        #endregion

        #region Static Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets a list of all available language codes based on JSON files in the Languages folder.
        /// </summary>
        /// <returns>
        /// A list of language codes (file names without the .json extension).
        /// For example: ["enUS", "deDE", "frFR"]
        /// </returns>
        /// <exception cref="DirectoryNotFoundException">
        /// Thrown when the Languages directory doesn't exist.
        /// </exception>
        /// <remarks>
        /// This method scans the Languages directory for all .json files
        /// and returns their names without extensions.
        /// </remarks>
        /// <example>
        /// <code>
        /// var languages = Translator.GetAvailableLanguages();
        /// foreach (var lang in languages)
        /// {
        ///     comboBox.Items.Add(lang);
        /// }
        /// </code>
        /// </example>
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

        #endregion
    }
}
