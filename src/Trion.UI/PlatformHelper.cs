using System;
using System.Linq;

namespace Trion.UI
{
    public static class PlatformHelper
    {
        private static bool? _isMaui;
        private static bool? _isWeb;

        public static bool IsMaui
        {
            get
            {
                if (_isMaui.HasValue) return _isMaui.Value;
                _isMaui = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Any(a => a.GetName().Name?.Equals("Microsoft.Maui", StringComparison.OrdinalIgnoreCase) == true);
                return _isMaui.Value;
            }
        }

        public static bool IsWeb
        {
            get
            {
                if (_isWeb.HasValue) return _isWeb.Value;
                // Web builds will load "Trion.Web" or "Microsoft.AspNetCore.Server"
                _isWeb = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Any(a =>
                        a.GetName().Name?.Equals("Trion.Web", StringComparison.OrdinalIgnoreCase) == true ||
                        a.GetName().Name?.StartsWith("Microsoft.AspNetCore.Server", StringComparison.OrdinalIgnoreCase) == true);
                return _isWeb.Value;
            }
        }

        public static string PlatformName => IsMaui ? "MAUI Desktop" : IsWeb ? "Blazor Web" : "Unknown";
    }
}
