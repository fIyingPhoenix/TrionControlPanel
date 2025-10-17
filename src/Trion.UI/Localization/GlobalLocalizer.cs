using Microsoft.Extensions.Localization;

namespace Trion.UI.Localization
{
    public class GlobalLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public GlobalLocalizer(IStringLocalizerFactory factory)
        {
            // IMPORTANT:
            // set ResourcesPath = "Localization/Resources" in the host apps.
            // With that, using base name "Strings" here is correct.
            var assemblyName = typeof(GlobalLocalizer).Assembly.GetName().Name!;
            _localizer = factory.Create("Strings", assemblyName);
        }

        public string this[string key] => _localizer[key];
    }
}
