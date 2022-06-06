using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherCoreServerLaucher.UI
{
    internal class CustomWebBrowser : WebBrowser
    {
        public CustomWebBrowser()
        {
            
            this.DoubleBuffered = true;
            this.ScrollBarsEnabled = false;
            this.AllowNavigation = false;
        }
    }
}
