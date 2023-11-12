using System.ComponentModel;
using TrionControlPanelDesktop.Models;
using static TrionControlPanelDesktop.Models.EnumModels;

namespace TrionControlPanelDesktop.UI
{
    public partial class CustomWebBrowser : UserControl
    {
        private string _url = "";
        private LoadWebsite _loadWebsite = LoadWebsite.False;

        [Category("1 CustomWebBrowser Advance")]
        public LoadWebsite LoadWebsite
        {
            get { return _loadWebsite; }
            set { _loadWebsite = value; }
        }
        [Category("1 CustomWebBrowser Advance")]
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public CustomWebBrowser()
        {
            InitializeComponent();
        }

        private async Task Init()
        {
            await WebViewCore.EnsureCoreWebView2Async(null);
        }
        private async void InitBorwser()
        {
            await Init();
            WebViewCore.CoreWebView2.Navigate(Url);
        }

        private void CustomWebBrowser_Load(object sender, EventArgs e)
        {
            if (LoadWebsite == LoadWebsite.True)
            {
                InitBorwser();
            }
        }
    }
}
