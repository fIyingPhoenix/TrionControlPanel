using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrionLibrary
{
    public class EnumModels
    {
        public enum ServerStatus
        {
            Running,
            NotRunning
        }
        public enum CurrentControl
        {
            Home,
            Control,
            Settings
        }
       public enum LoadWebsite
        {
            True,
            False
        }
    }
}
