using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherCore_Server_Laucher.Classes
{
    public enum NotificationAction
    {
        wait,
        start,
        close
    }

    public enum NotificationType
    {
        Success,
        Warning,
        Error,
        Info
    }

}
