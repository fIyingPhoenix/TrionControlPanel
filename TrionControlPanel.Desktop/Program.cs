using TrionControlPanel.Desktop.Extensions.Classes.Monitor;

namespace TrionControlPanelDesktop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set up global exception handling
            Application.ThreadException += new ThreadExceptionEventHandler(GlobalThreadExceptionHandler);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalDomainExceptionHandler);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //DPI Fix Try
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        private static void GlobalThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            TrionLogger.Log($"Unhandled Thread Exception: {e.Exception.Message}\n{e.Exception.StackTrace}", "CRITICAL");
            MessageBox.Show("An unexpected error occurred. Please check the logs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void GlobalDomainExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            TrionLogger.Log($"Unhandled Domain Exception: {ex.Message}\n{ex.StackTrace}", "CRITICAL");
            // This is often fatal, but we log it first.
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}