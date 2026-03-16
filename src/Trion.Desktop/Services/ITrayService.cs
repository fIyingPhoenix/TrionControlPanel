using System.Windows;

namespace Trion.Desktop.Services;

public interface ITrayService : IDisposable
{
    /// <summary>Attaches the tray icon to the main window and makes it visible.</summary>
    void Initialize(Window mainWindow);

    /// <summary>Shows a balloon notification in the system tray.</summary>
    void ShowBalloon(string title, string message);

    /// <summary>Re-generates the tray and window icon using the given accent colour.</summary>
    void SetAccentColor(System.Drawing.Color accent);

}
