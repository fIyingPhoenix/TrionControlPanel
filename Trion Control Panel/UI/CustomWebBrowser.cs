
namespace TrionControlPanel.UI
{
    internal class CustomWebBrowser : WebBrowser
    {
        public CustomWebBrowser()
        { 
            this.DoubleBuffered = true;
            this.ScrollBarsEnabled = false;
            this.AllowNavigation = false;
            this.ScriptErrorsSuppressed = true;

            // Create a Timer object that knows to call our TimerCallback
            // method once every 2000 milliseconds.
            System.Threading.Timer timerRefresh = new(TimerCallback, null, 0, 50000);
        }
        private void TimerCallback(object o)
        {
            this.Refresh();
        }
    }
}
