using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Animation;

namespace TrionUpdater;

public partial class UpdaterWindow : Window
{
    private readonly Dictionary<string, string> _args;

    // Progress bar track width — read after layout pass
    private double TrackWidth => ProgressFill.Parent is FrameworkElement parent
        ? parent.ActualWidth
        : 384;  // fallback before layout pass

    public UpdaterWindow(Dictionary<string, string> args)
    {
        InitializeComponent();
        _args = args;

        // Show version info in the footer if provided
        if (args.TryGetValue("from", out string? fromVer) &&
            args.TryGetValue("to",   out string? toVer))
        {
            VersionLabel.Text = $"v{fromVer}  →  v{toVer}";
        }

        Loaded += async (_, _) => await RunUpdateAsync();
    }

    // ── Update flow ───────────────────────────────────────────────────────────

    private async Task RunUpdateAsync()
    {
        if (!_args.TryGetValue("pid",    out string? pidStr)  ||
            !_args.TryGetValue("source", out string? source)  ||
            !_args.TryGetValue("target", out string? target))
        {
            SetStatus("Missing arguments — cannot update.", "#F44336");
            PhaseLabel.Text = "Error";
            return;
        }

        if (!int.TryParse(pidStr, out int pid))
        {
            SetStatus($"Invalid process ID: {pidStr}", "#F44336");
            PhaseLabel.Text = "Error";
            return;
        }

        try
        {
            // ── Step 1: Wait for the main app to exit ─────────────────────────
            PhaseLabel.Text = "Waiting…";
            SetStatus("Waiting for Trion Control Panel to close…");
            AnimateProgress(0, 20, durationMs: 400);

            await WaitForProcessAsync(pid);

            // ── Step 2: Validate the downloaded file ──────────────────────────
            AnimateProgress(20, 35, durationMs: 300);
            SetStatus("Verifying downloaded file…");
            await Task.Delay(300);

            if (!System.IO.File.Exists(source))
            {
                SetStatus($"Update file not found:\n{source}", "#F44336");
                PhaseLabel.Text = "Error";
                return;
            }

            // ── Step 3: Backup + replace ──────────────────────────────────────
            PhaseLabel.Text = "Updating…";
            SetStatus("Replacing application files…");
            AnimateProgress(35, 75, durationMs: 600);

            await Task.Run(() => ReplaceExe(source, target));

            // ── Step 4: Restart ───────────────────────────────────────────────
            PhaseLabel.Text = "Restarting…";
            SetStatus("Update complete. Restarting Trion Control Panel…");
            AnimateProgress(75, 100, durationMs: 400);

            await Task.Delay(800);

            Process.Start(new ProcessStartInfo(target) { UseShellExecute = true });
            Application.Current.Shutdown();
        }
        catch (Exception ex)
        {
            PhaseLabel.Text = "Error";
            SetStatus($"Update failed: {ex.Message}", "#F44336");
        }
    }

    // ── Wait for the main process to exit ────────────────────────────────────

    private static async Task WaitForProcessAsync(int pid)
    {
        try
        {
            var proc = Process.GetProcessById(pid);
            await proc.WaitForExitAsync().WaitAsync(TimeSpan.FromSeconds(30));
        }
        catch (ArgumentException) { /* Already exited */ }

        // Small buffer — OS can hold the file lock briefly after process exit
        await Task.Delay(500);
    }

    // ── Atomic file replacement with backup ───────────────────────────────────

    private static void ReplaceExe(string source, string target)
    {
        string backup = target + ".bak";
        try
        {
            if (System.IO.File.Exists(target))
                System.IO.File.Copy(target, backup, overwrite: true);

            System.IO.File.Copy(source, target, overwrite: true);
            System.IO.File.Delete(source);

            if (System.IO.File.Exists(backup))
                System.IO.File.Delete(backup);
        }
        catch
        {
            // Restore backup if copy failed
            if (System.IO.File.Exists(backup) && !System.IO.File.Exists(target))
                System.IO.File.Copy(backup, target);
            throw;
        }
    }

    // ── UI helpers ────────────────────────────────────────────────────────────

    private void SetStatus(string text, string? color = null)
    {
        Dispatcher.Invoke(() =>
        {
            StatusLabel.Text = text;
            if (color is not null)
                StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(
                    (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(color));
        });
    }

    private void AnimateProgress(double fromPct, double toPct, int durationMs)
    {
        Dispatcher.Invoke(() =>
        {
            double trackWidth = TrackWidth;
            double fromWidth  = trackWidth * fromPct / 100.0;
            double toWidth    = trackWidth * toPct   / 100.0;

            var anim = new DoubleAnimation(fromWidth, toWidth,
                TimeSpan.FromMilliseconds(durationMs))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
            };

            ProgressFill.BeginAnimation(WidthProperty, anim);
        });
    }
}
