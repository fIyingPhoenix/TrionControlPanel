using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using Trion.Desktop.Views;
using WinForms        = System.Windows.Forms;
using GdiColor        = System.Drawing.Color;
using GdiFontStyle    = System.Drawing.FontStyle;

namespace Trion.Desktop.Services;

public sealed class TrayService : ITrayService
{
    private WinForms.NotifyIcon? _icon;
    private Window?              _window;
    private Icon?                _generatedIcon;  // tracks generated icons so we can dispose them

    [DllImport("user32.dll")]
    private static extern bool DestroyIcon(IntPtr handle);

    public void Initialize(Window mainWindow)
    {
        _window = mainWindow;

        // Load the embedded favicon.ico that is already a WPF resource
        Icon appIcon;
        try
        {
            var sri = Application.GetResourceStream(
                          new Uri("pack://application:,,,/Resources/Images/favicon.ico"));
            appIcon = sri is not null ? new Icon(sri.Stream) : SystemIcons.Application;
        }
        catch
        {
            appIcon = SystemIcons.Application;
        }

        _icon = new WinForms.NotifyIcon
        {
            Text    = "Trion Control Panel",
            Icon    = appIcon,
            Visible = true,
        };

        // Double-click restores; right-click shows themed WPF popup
        _icon.DoubleClick += (_, _) => Application.Current.Dispatcher.BeginInvoke(Restore);
        _icon.MouseUp     += OnMouseUp;
    }

    private void OnMouseUp(object? sender, WinForms.MouseEventArgs e)
    {
        if (e.Button != WinForms.MouseButtons.Right) return;

        // Capture cursor position on the WinForms thread before dispatching
        int x = WinForms.Cursor.Position.X;
        int y = WinForms.Cursor.Position.Y;

        Application.Current.Dispatcher.BeginInvoke(() =>
        {
            var menu = new TrayMenuWindow(Restore, Exit, x, y);
            menu.Show();
        });
    }

    public void ShowBalloon(string title, string message)
    {
        _icon?.ShowBalloonTip(3000, title, message, WinForms.ToolTipIcon.Info);
    }

    public void SetAccentColor(GdiColor accent)
    {
        if (_icon is null) return;

        var newIcon = BuildAccentIcon(accent);

        // Swap tray icon, dispose the previous generated one
        var old = _generatedIcon;
        _generatedIcon = newIcon;
        _icon.Icon     = newIcon;
        old?.Dispose();

        // Update the WPF window icon on the UI thread
        if (_window is not null)
        {
            var source = BuildWpfIcon(accent);
            _window.Dispatcher.BeginInvoke(() => _window.Icon = source);
        }
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private void Restore()
    {
        if (_window is null) return;
        _window.Show();
        _window.WindowState = WindowState.Normal;
        _window.Activate();
    }

    private static void Exit()
    {
        Application.Current.Shutdown();
    }

    /// <summary>
    /// Draws a 32×32 circle filled with <paramref name="accent"/> and a white "T"
    /// centred inside it, then returns it as a GDI <see cref="Icon"/>.
    /// </summary>
    private static Icon BuildAccentIcon(GdiColor accent)
    {
        using var bmp = new Bitmap(32, 32);
        using var g   = Graphics.FromImage(bmp);

        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(GdiColor.Transparent);

        using (var fill = new SolidBrush(accent))
            g.FillEllipse(fill, 1, 1, 30, 30);

        var letterColor = accent.GetBrightness() > 0.55f
            ? GdiColor.FromArgb(30, 30, 30)
            : GdiColor.White;

        using var font      = new Font("Segoe UI", 14f, GdiFontStyle.Bold, GraphicsUnit.Pixel);
        using var textBrush = new SolidBrush(letterColor);

        var sz  = g.MeasureString("T", font);
        var pos = new PointF((32 - sz.Width) / 2f, (32 - sz.Height) / 2f);
        g.DrawString("T", font, textBrush, pos);

        // GetHicon gives us a handle we must destroy after cloning
        IntPtr hIcon = bmp.GetHicon();
        try   { return (Icon)Icon.FromHandle(hIcon).Clone(); }
        finally { DestroyIcon(hIcon); }
    }

    /// <summary>
    /// Same drawing as <see cref="BuildAccentIcon"/> but returned as a frozen
    /// WPF <see cref="BitmapSource"/> for use as <see cref="Window.Icon"/>.
    /// </summary>
    private static BitmapSource BuildWpfIcon(GdiColor accent)
    {
        using var bmp = new Bitmap(32, 32);
        using var g   = Graphics.FromImage(bmp);

        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(GdiColor.Transparent);

        using (var fill = new SolidBrush(accent))
            g.FillEllipse(fill, 1, 1, 30, 30);

        var letterColor = accent.GetBrightness() > 0.55f
            ? GdiColor.FromArgb(30, 30, 30)
            : GdiColor.White;

        using var font      = new Font("Segoe UI", 14f, GdiFontStyle.Bold, GraphicsUnit.Pixel);
        using var textBrush = new SolidBrush(letterColor);

        var sz  = g.MeasureString("T", font);
        var pos = new PointF((32 - sz.Width) / 2f, (32 - sz.Height) / 2f);
        g.DrawString("T", font, textBrush, pos);

        using var ms = new MemoryStream();
        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        ms.Seek(0, SeekOrigin.Begin);

        var bi = new BitmapImage();
        bi.BeginInit();
        bi.CacheOption  = BitmapCacheOption.OnLoad;
        bi.StreamSource = ms;
        bi.EndInit();
        bi.Freeze();
        return bi;
    }

    public void Dispose()
    {
        _generatedIcon?.Dispose();
        _generatedIcon = null;
        _icon?.Dispose();
        _icon = null;
    }
}
