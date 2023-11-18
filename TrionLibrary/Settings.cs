using Org.BouncyCastle.Tls;
using System;
using System.IO;
using System.Xml.Serialization;

namespace TrionLibrary
{
    public class Data
    {

    }
    public class Settings
    {
        public class Server
        {
            public static EnumModels.ServerStatus WorldServerStatus;
            public static EnumModels.ServerStatus LoginServerStatus;
            public static EnumModels.ServerStatus MysqlServerStatus;
        } 
        
    }
}
