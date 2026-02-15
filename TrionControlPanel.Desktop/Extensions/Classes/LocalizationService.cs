// =============================================================================
// LocalizationService.cs
// Purpose: Service for managing UI localization with control binding support
// Used by: MainForm, other forms requiring localization
// Step 4 of IMPROVEMENTS.md - Extract Localization Logic
// =============================================================================

using System.Globalization;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    /// <summary>
    /// Service for managing UI localization with a binding system.
    /// Automatically updates bound controls when language changes.
    /// </summary>
    /// <remarks>
    /// This service wraps the Translator class and provides a declarative way
    /// to bind controls to translation keys. Instead of manually setting each
    /// control's text, you bind it once and call ApplyAll() when language changes.
    /// </remarks>
    public class LocalizationService
    {
        #region Nested Types
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Binding information for a control's text property.
        /// </summary>
        private class TextBinding
        {
            public string TranslationKey { get; set; } = "";
            public bool ToUpper { get; set; }
            public Action<Control, string>? CustomSetter { get; set; }
        }

        /// <summary>
        /// Binding information for a control's hint property.
        /// </summary>
        private class HintBinding
        {
            public string TranslationKey { get; set; } = "";
        }

        /// <summary>
        /// Binding information for a control's tooltip.
        /// </summary>
        private class TooltipBinding
        {
            public string TranslationKey { get; set; } = "";
            public ToolTip ToolTip { get; set; } = null!;
        }

        #endregion

        #region Fields
        // ─────────────────────────────────────────────────────────────────────

        private readonly Dictionary<Control, TextBinding> _textBindings = new();
        private readonly Dictionary<Control, HintBinding> _hintBindings = new();
        private readonly Dictionary<Control, TooltipBinding> _tooltipBindings = new();
        private readonly Translator _translator;

        #endregion

        #region Constructors
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Initializes a new instance of the LocalizationService.
        /// </summary>
        /// <param name="translator">The translator instance to use for translations.</param>
        public LocalizationService(Translator translator)
        {
            _translator = translator ?? throw new ArgumentNullException(nameof(translator));
        }

        #endregion

        #region Public Properties
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets the underlying translator instance.
        /// </summary>
        public Translator Translator => _translator;

        /// <summary>
        /// Gets the total number of bindings registered.
        /// </summary>
        public int TotalBindings => _textBindings.Count + _hintBindings.Count + _tooltipBindings.Count;

        #endregion

        #region Binding Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Binds a control's Text property to a translation key.
        /// The control will be updated when ApplyAll() is called.
        /// </summary>
        /// <param name="control">The control to bind.</param>
        /// <param name="translationKey">The key in the language file.</param>
        /// <param name="toUpper">Whether to uppercase the translated text.</param>
        /// <returns>This service instance for fluent chaining.</returns>
        public LocalizationService BindText(Control control, string translationKey, bool toUpper = false)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            if (string.IsNullOrEmpty(translationKey)) throw new ArgumentNullException(nameof(translationKey));

            _textBindings[control] = new TextBinding
            {
                TranslationKey = translationKey,
                ToUpper = toUpper
            };
            return this;
        }

        /// <summary>
        /// Binds a control's Text property with a custom setter action.
        /// Useful for controls with non-standard text properties.
        /// </summary>
        /// <param name="control">The control to bind.</param>
        /// <param name="translationKey">The key in the language file.</param>
        /// <param name="setter">Custom action to set the translated text.</param>
        /// <param name="toUpper">Whether to uppercase the translated text.</param>
        /// <returns>This service instance for fluent chaining.</returns>
        public LocalizationService BindText(Control control, string translationKey, Action<Control, string> setter, bool toUpper = false)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            if (string.IsNullOrEmpty(translationKey)) throw new ArgumentNullException(nameof(translationKey));
            if (setter == null) throw new ArgumentNullException(nameof(setter));

            _textBindings[control] = new TextBinding
            {
                TranslationKey = translationKey,
                ToUpper = toUpper,
                CustomSetter = setter
            };
            return this;
        }

        /// <summary>
        /// Binds a MaterialTextBox's Hint property to a translation key.
        /// </summary>
        /// <param name="control">The MaterialTextBox control to bind.</param>
        /// <param name="translationKey">The key in the language file.</param>
        /// <returns>This service instance for fluent chaining.</returns>
        public LocalizationService BindHint(Control control, string translationKey)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            if (string.IsNullOrEmpty(translationKey)) throw new ArgumentNullException(nameof(translationKey));

            _hintBindings[control] = new HintBinding
            {
                TranslationKey = translationKey
            };
            return this;
        }

        /// <summary>
        /// Binds a control's tooltip to a translation key.
        /// </summary>
        /// <param name="control">The control to bind the tooltip to.</param>
        /// <param name="translationKey">The key in the language file.</param>
        /// <param name="tooltip">The ToolTip component to use.</param>
        /// <returns>This service instance for fluent chaining.</returns>
        public LocalizationService BindTooltip(Control control, string translationKey, ToolTip tooltip)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            if (string.IsNullOrEmpty(translationKey)) throw new ArgumentNullException(nameof(translationKey));
            if (tooltip == null) throw new ArgumentNullException(nameof(tooltip));

            _tooltipBindings[control] = new TooltipBinding
            {
                TranslationKey = translationKey,
                ToolTip = tooltip
            };
            return this;
        }

        /// <summary>
        /// Updates the translation key for an existing text binding.
        /// Useful for dynamic text that changes based on state (e.g., ON/OFF buttons).
        /// </summary>
        /// <param name="control">The control with an existing binding.</param>
        /// <param name="newTranslationKey">The new translation key to use.</param>
        /// <returns>True if the binding was updated, false if no binding exists.</returns>
        public bool UpdateTextKey(Control control, string newTranslationKey)
        {
            if (_textBindings.TryGetValue(control, out var binding))
            {
                binding.TranslationKey = newTranslationKey;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes all bindings for a control.
        /// </summary>
        /// <param name="control">The control to unbind.</param>
        public void Unbind(Control control)
        {
            _textBindings.Remove(control);
            _hintBindings.Remove(control);
            _tooltipBindings.Remove(control);
        }

        /// <summary>
        /// Clears all bindings.
        /// </summary>
        public void ClearAllBindings()
        {
            _textBindings.Clear();
            _hintBindings.Clear();
            _tooltipBindings.Clear();
        }

        #endregion

        #region Apply Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Applies all bound translations to controls.
        /// Call this after changing language or during initialization.
        /// </summary>
        public void ApplyAll()
        {
            ApplyTextBindings();
            ApplyHintBindings();
            ApplyTooltipBindings();
        }

        /// <summary>
        /// Applies only text bindings.
        /// </summary>
        public void ApplyTextBindings()
        {
            foreach (var (control, binding) in _textBindings)
            {
                if (control.IsDisposed) continue;

                string text = _translator.Translate(binding.TranslationKey);
                if (binding.ToUpper)
                {
                    text = text.ToUpper(CultureInfo.InvariantCulture);
                }

                if (binding.CustomSetter != null)
                {
                    binding.CustomSetter(control, text);
                }
                else
                {
                    SetControlText(control, text);
                }
            }
        }

        /// <summary>
        /// Applies only hint bindings.
        /// </summary>
        public void ApplyHintBindings()
        {
            foreach (var (control, binding) in _hintBindings)
            {
                if (control.IsDisposed) continue;

                string text = _translator.Translate(binding.TranslationKey);
                SetControlHint(control, text);
            }
        }

        /// <summary>
        /// Applies only tooltip bindings.
        /// </summary>
        public void ApplyTooltipBindings()
        {
            foreach (var (control, binding) in _tooltipBindings)
            {
                if (control.IsDisposed) continue;

                string text = _translator.Translate(binding.TranslationKey);
                binding.ToolTip.SetToolTip(control, text);
            }
        }

        /// <summary>
        /// Applies translation to a single control immediately.
        /// </summary>
        /// <param name="control">The control to update.</param>
        public void ApplyOne(Control control)
        {
            if (control.IsDisposed) return;

            if (_textBindings.TryGetValue(control, out var textBinding))
            {
                string text = _translator.Translate(textBinding.TranslationKey);
                if (textBinding.ToUpper)
                {
                    text = text.ToUpper(CultureInfo.InvariantCulture);
                }

                if (textBinding.CustomSetter != null)
                {
                    textBinding.CustomSetter(control, text);
                }
                else
                {
                    SetControlText(control, text);
                }
            }

            if (_hintBindings.TryGetValue(control, out var hintBinding))
            {
                string text = _translator.Translate(hintBinding.TranslationKey);
                SetControlHint(control, text);
            }

            if (_tooltipBindings.TryGetValue(control, out var tooltipBinding))
            {
                string text = _translator.Translate(tooltipBinding.TranslationKey);
                tooltipBinding.ToolTip.SetToolTip(control, text);
            }
        }

        #endregion

        #region Direct Translation Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets a translated string directly without binding.
        /// Useful for one-time translations or dynamic content.
        /// </summary>
        /// <param name="key">The translation key.</param>
        /// <returns>The translated text.</returns>
        public string Translate(string key)
        {
            return _translator.Translate(key);
        }

        /// <summary>
        /// Gets a translated string in uppercase without binding.
        /// </summary>
        /// <param name="key">The translation key.</param>
        /// <returns>The translated text in uppercase.</returns>
        public string TranslateUpper(string key)
        {
            return _translator.Translate(key).ToUpper(CultureInfo.InvariantCulture);
        }

        #endregion

        #region Private Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Sets the text property on a control, handling different control types.
        /// </summary>
        private static void SetControlText(Control control, string text)
        {
            // Handle thread safety
            if (control.InvokeRequired)
            {
                control.Invoke(() => SetControlText(control, text));
                return;
            }

            control.Text = text;
        }

        /// <summary>
        /// Sets the hint property on a control that supports it.
        /// </summary>
        private static void SetControlHint(Control control, string hint)
        {
            // Handle thread safety
            if (control.InvokeRequired)
            {
                control.Invoke(() => SetControlHint(control, hint));
                return;
            }

            // Use reflection to set Hint property if it exists
            var hintProperty = control.GetType().GetProperty("Hint");
            hintProperty?.SetValue(control, hint);
        }

        #endregion
    }
}
