// =============================================================================
// Program.cs
// Purpose: Application entry point and global exception handling
// Step 14 of IMPROVEMENTS.md - Add Logging Throughout with Context
// =============================================================================

using TrionControlPanel.Desktop.Extensions.Classes.Monitor;

namespace TrionControlPanelDesktop
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Log application startup (Step 14)
            TrionLogger.LogAppLifecycle("Starting", $"Version: {Application.ProductVersion}");

            // Set up global exception handling
            Application.ThreadException += new ThreadExceptionEventHandler(GlobalThreadExceptionHandler);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalDomainExceptionHandler);

            // Configure high DPI settings
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ApplicationConfiguration.Initialize();

            TrionLogger.LogAppLifecycle("Initialized", "Configuration complete, launching MainForm");

            Application.Run(new MainForm());

            // Log application shutdown (Step 14)
            TrionLogger.LogAppLifecycle("Stopped", "Application exited normally");
        }

        /// <summary>
        /// Handles unhandled exceptions on UI threads.
        /// </summary>
        private static void GlobalThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            // Use enhanced logging with full exception details (Step 14)
            TrionLogger.Critical($"Unhandled Thread Exception occurred");
            TrionLogger.LogException(e.Exception, "GlobalThreadException");

            MessageBox.Show(
                "An unexpected error occurred. Please check the logs for details.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// Handles unhandled exceptions on non-UI threads.
        /// </summary>
        private static void GlobalDomainExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            // Use enhanced logging with full exception details (Step 14)
            TrionLogger.Critical($"Unhandled Domain Exception occurred (IsTerminating: {e.IsTerminating})");
            TrionLogger.LogException(ex, "GlobalDomainException");

            // This is often fatal, but we log it first
            if (e.IsTerminating)
            {
                TrionLogger.LogAppLifecycle("Crashed", "Application terminated due to unhandled exception");
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
