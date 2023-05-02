using Org.BouncyCastle.Bcpg;
using System;
using System.Diagnostics;

namespace TrionLibrary
{
    public static class SystemWatcher
    {
        //World Variables 
        public static string WorldName { get;set; }
        public static int WorldRam { get; set; }
        public static int WorldPID { get; set; }
        //Auth Variables 
        public static string AuthName{ get;set; }
        public static int AuthRam { get; set; }
        public static int AuthPID { get; set; }
        //Trion Control Panel Variables
        public static string DatabaseName { get; set; }
        public static string TCPName { get; set; }
        public static int totalRam {get; set; }
        //
        public static bool WorldRuning()
        {
            WorldName = "";
            Process[] pname = Process.GetProcessesByName(WorldName);
            if (pname.Length <= 0)
                return false;
            else
                return true;
        }
        public static bool WorldKilled()
        {
            bool succes = false;
            WorldName = "";
            foreach (var process in Process.GetProcessesByName(WorldName))
            {
                try { process.Kill(); succes = true; }
                catch (Exception ex){ succes = false;}
            }
            return succes;
        }
        public static bool AuthRuning()
        {
            WorldName = "";
            Process[] pname = Process.GetProcessesByName(AuthName);
            if (pname.Length <= 0)
                return false;
            else
                return true;
        }
        public static bool DatabaseRuning()
        {
            return false;
        }
        public static bool TCPRuning()
        {
            TCPName = "TrionControlPanel";
            Process[] pname = Process.GetProcessesByName(TCPName);
            if (pname.Length <= 0)
                return false;
            else
                return true;
        }
    }
}
