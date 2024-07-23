﻿using System.Net.Sockets;
using TrionLibrary.Models;
using TrionLibrary.Setting;
using TrionLibrary.Sys;


namespace TrionControlPanelDesktop.Data
{
    public class Main
    {
        public static void StartDatabase(string argu)
        {
            if (Setting.List.DBExecutablePath != string.Empty)
            {
                Watcher.ApplicationStart(
                    Setting.List.DBExecutablePath,
                    Setting.List.DBLocation,
                    Setting.List.DBExecutableName,
                    Setting.List.ConsolHide,
                    argu);
            }
        }
        public static async Task<bool> IsPortOpen()
        {
            string host = "localhost";
            int port = int.Parse(Setting.List.DBServerPort);
            try
            {
                using TcpClient tcpClient = new();
                await tcpClient.ConnectAsync(host, port);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void StartWorld()
        {
            if (Setting.List.CustomInstalled)
            {
                int ID = Watcher.ApplicationStart(
                    Setting.List.CustomWorldExeLoc,
                    Setting.List.CustomWorkingDirectory,
                    Setting.List.CustomWorldExeName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.CustomWorldExeName });
            }
            if (Setting.List.ClassicInstalled)
            {
                int ID = Watcher.ApplicationStart(
                    Setting.List.ClassicWorldExeLoc,
                    Setting.List.ClassicWorkingDirectory,
                    Setting.List.ClassicWorldExeName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.ClassicWorldExeName });
            }
            if (Setting.List.TBCInstalled)
            {
                int ID = Watcher.ApplicationStart(
                    Setting.List.TBCWorldExeLoc,
                    Setting.List.TBCWorkingDirectory,
                    Setting.List.TBCWorldExeName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.TBCWorldExeName });
            }
            if (Setting.List.WotLKInstalled)
            {
                int ID = Watcher.ApplicationStart(
                     Setting.List.WotLKWorldExeLoc,
                     Setting.List.WotLKWorkingDirectory,
                     Setting.List.WotLKWorldExeName,
                     Setting.List.ConsolHide,
                     null
                );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.WotLKWorldExeName });
            }
            if (Setting.List.CataInstalled)
            {
                int ID = Watcher.ApplicationStart(
                    Setting.List.CataWorldExeLoc,
                    Setting.List.CataWorkingDirectory,
                    Setting.List.CataWorldExeName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.CataWorldExeName });
            }
            if (Setting.List.MOPInstalled)
            {
                int ID = Watcher.ApplicationStart(
                    Setting.List.MopWorldExeLoc,
                    Setting.List.MopWorkingDirectory,
                    Setting.List.MopWorldExeName,
                    Setting.List.ConsolHide,
                    null
                    );
                User.System.WorldProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.MopWorldExeName });
            }
        }
        public static void StartLogon()
        {
            if (Setting.List.CustomInstalled)
            {
                int ID = Watcher.ApplicationStart(
                    Setting.List.CustomLogonExeLoc,
                    Setting.List.CustomWorkingDirectory,
                    Setting.List.CustomLogonExeName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.CustomLogonExeName });
            }
            if (Setting.List.ClassicInstalled)
            {
                int ID = Watcher.ApplicationStart(
                    Setting.List.ClassicLogonExeLoc,
                    Setting.List.ClassicWorkingDirectory,
                    Setting.List.ClassicLogonExeName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.ClassicLogonExeName });
            }
            if (Setting.List.TBCInstalled)
            {
                int ID = Watcher.ApplicationStart(
                    Setting.List.TBCLogonExeLoc,
                    Setting.List.TBCWorkingDirectory,
                    Setting.List.TBCLogonExeName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.TBCLogonExeName });
            }
            if (Setting.List.WotLKInstalled)
            {
                int ID = Watcher.ApplicationStart(
                     Setting.List.WotLKLogonExeLoc,
                     Setting.List.WotLKWorkingDirectory,
                     Setting.List.WotLKLogonExeName,
                     Setting.List.ConsolHide,
                     null
                );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.WotLKLogonExeName });
            }
            if (Setting.List.CataInstalled)
            {
                int ID = Watcher.ApplicationStart(
                    Setting.List.CataLogonExeLoc,
                    Setting.List.CataWorkingDirectory,
                    Setting.List.CataLogonExeName,
                    Setting.List.ConsolHide,
                    null
                );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.CataLogonExeName });
            }
            if (Setting.List.MOPInstalled)
            {
                int ID = Watcher.ApplicationStart(
                    Setting.List.MopLogonExeLoc,
                    Setting.List.MopWorkingDirectory,
                    Setting.List.MopLogonExeName,
                    Setting.List.ConsolHide,
                    null
                    );
                User.System.LogonProcessesID.Add(new Lists.ProcessID()
                { ID = ID, Name = Setting.List.MopLogonExeName });
            }
        }
        public static void StopWorld()
        {
            if (Setting.List.CustomInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CustomWorldExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Setting.List.ClassicInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.ClassicWorldExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Setting.List.TBCInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.TBCWorldExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Setting.List.WotLKInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.WotLKWorldExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Setting.List.CataInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.CataWorldExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Setting.List.MOPInstalled)
            {
                var processToRemove = User.System.WorldProcessesID.Single(r => r.Name == Setting.List.MopWorldExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
        }
        public static void StopLogon()
        {
            if (Setting.List.CustomInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CustomLogonExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Setting.List.ClassicInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.ClassicLogonExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Setting.List.TBCInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.TBCLogonExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Setting.List.WotLKInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.WotLKLogonExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Setting.List.CataInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.CataLogonExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
            if (Setting.List.MOPInstalled)
            {
                var processToRemove = User.System.LogonProcessesID.Single(r => r.Name == Setting.List.MopLogonExeName);
                Watcher.ApplicationStop(processToRemove.ID);
                User.System.WorldProcessesID.Remove(processToRemove);
            }
        }
    }
}