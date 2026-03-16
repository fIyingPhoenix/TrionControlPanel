using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Trion.Desktop.Services;
using Trion.Desktop.ViewModels;

namespace Trion.Desktop;

public partial class MainWindow : Window
{
    private readonly ISettingsService _settings;
    private readonly ITrayService     _tray;

    public MainWindow(MainWindowViewModel viewModel, ISettingsService settings, ITrayService tray)
    {
        InitializeComponent();
        DataContext = viewModel;
        _settings   = settings;
        _tray       = tray;
        Loaded += Window_Loaded;
    }

    // ── Tray: hide instead of close when StayInTray is enabled ───────────────

    protected override void OnClosing(CancelEventArgs e)
    {
        // During app shutdown the close cannot (and should not) be cancelled
        if (_settings.Current.StayInTray
            && !Application.Current.Dispatcher.HasShutdownStarted)
        {
            e.Cancel = true;
            Hide();
            _tray.ShowBalloon(
                "Trion Control Panel",
                "Trion is still running in the system tray. Right-click the icon to exit.");
            return;
        }

        base.OnClosing(e);
    }

    // ── DWM rounded corners (Windows 11+) ────────────────────────────────────

    [DllImport("dwmapi.dll", PreserveSig = true)]
    private static extern int DwmSetWindowAttribute(
        nint hwnd, uint attr, ref int attrValue, int attrSize);

    private const uint DWMWA_WINDOW_CORNER_PREFERENCE = 33;
    private const int  DWMWCP_ROUND                   = 2;

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            int pref = DWMWCP_ROUND;
            DwmSetWindowAttribute(hwnd, DWMWA_WINDOW_CORNER_PREFERENCE,
                                  ref pref, Marshal.SizeOf<int>());
        }
        catch
        {
            // Not on Windows 11 — corners stay square, no action needed
        }
    }

    // ── Maximize fix — constrain to work area so bottom bar stays visible ─────
    //
    // WindowStyle.None + WindowChrome extends the window slightly beyond screen
    // edges when maximized (to hide the resize hit-test border). Without this
    // hook the bottom row gets clipped off-screen.

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
        source?.AddHook(WndProc);
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == 0x0024 /* WM_GETMINMAXINFO */)
        {
            FixMaximizeBounds(hwnd, lParam);
            handled = true;
        }
        return IntPtr.Zero;
    }

    private static void FixMaximizeBounds(IntPtr hwnd, IntPtr lParam)
    {
        var monitor = MonitorFromWindow(hwnd, 0x00000002 /* MONITOR_DEFAULTTONEAREST */);
        if (monitor == IntPtr.Zero) return;

        var info = new MONITORINFO { cbSize = Marshal.SizeOf<MONITORINFO>() };
        GetMonitorInfo(monitor, ref info);

        var work = info.rcWork;
        var full = info.rcMonitor;

        var mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);
        mmi.ptMaxPosition = new WINPOINT(work.left - full.left, work.top - full.top);
        mmi.ptMaxSize     = new WINPOINT(work.right - work.left, work.bottom - work.top);
        Marshal.StructureToPtr(mmi, lParam, true);
    }

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    [StructLayout(LayoutKind.Sequential)]
    private struct MINMAXINFO
    {
        public WINPOINT ptReserved;
        public WINPOINT ptMaxSize;
        public WINPOINT ptMaxPosition;
        public WINPOINT ptMinTrackSize;
        public WINPOINT ptMaxTrackSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MONITORINFO
    {
        public int     cbSize;
        public WINRECT rcMonitor;
        public WINRECT rcWork;
        public uint    dwFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct WINRECT { public int left, top, right, bottom; }

    [StructLayout(LayoutKind.Sequential)]
    private struct WINPOINT
    {
        public int x, y;
        public WINPOINT(int x, int y) { this.x = x; this.y = y; }
    }

    // ── Title-bar interaction ─────────────────────────────────────────────────

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
            ToggleMaximize();
        else
            DragMove();
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e) =>
        WindowState = WindowState.Minimized;

    private void MaximizeButton_Click(object sender, RoutedEventArgs e) =>
        ToggleMaximize();

    private void CloseButton_Click(object sender, RoutedEventArgs e) =>
        Close();

    private void ToggleMaximize() =>
        WindowState = WindowState == WindowState.Maximized
            ? WindowState.Normal
            : WindowState.Maximized;
}
