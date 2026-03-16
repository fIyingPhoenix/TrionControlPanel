using System.Windows;

namespace TrionUpdater;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var args   = ParseArgs(e.Args);
        var window = new UpdaterWindow(args);
        MainWindow = window;
        window.Show();
    }

    private static Dictionary<string, string> ParseArgs(string[] args)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i + 1 < args.Length; i += 2)
            dict[args[i].TrimStart('-')] = args[i + 1];
        return dict;
    }
}
