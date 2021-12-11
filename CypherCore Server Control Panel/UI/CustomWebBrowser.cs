using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherCoreServerLaucher.UI
{
    internal class CustomWebBrowser : WebBrowser
    {
        //private bool linksOpenInSystemBrowser = false;
        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    linksOpenInSystemBrowser = false;
        //    this.Navigate(this.Url);
        //}

        private void OnBrowserKlick(object sender, NavigateEventArgs e)
        {

        }
      
        public CustomWebBrowser()
        {
          //linksOpenInSystemBrowser = false;
            this.DoubleBuffered = true;
            this.ScrollBarsEnabled = false;
            this.AllowNavigation = false;

        }

        

    }
}
