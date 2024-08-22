using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace SmartSaveFolder
{
    public class Watcher
    {
        public static void CleanupOrphanedWatchers(string ProcessName)
        {            
            string startQuery = "SELECT TargetInstance" +
                           " FROM __InstanceCreationEvent " + "WITHIN  2 " +
                           " WHERE TargetInstance ISA 'Win32_Process' " +
                           " AND TargetInstance.Name = '" + ProcessName + "'";
            string endQuery = "SELECT TargetInstance" +
                           " FROM __InstanceDeletionEvent " + "WITHIN  2 " +
                           " WHERE TargetInstance ISA 'Win32_Process' " +
                           " AND TargetInstance.Name = '" + ProcessName + "'";
            string scope = "\\\\.\\root\\CIMV2";

            // Find all instances of ManagementEventWatcher that were created using the same query and scope
            var startWatchers = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(ManagementEventWatcher).IsAssignableFrom(p))
                .Select(x => (ManagementEventWatcher)Activator.CreateInstance(x))
                .Where(w => w.Query.QueryString == startQuery && w.Scope.Path.Path == scope).ToList();

            var endWatchers = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(ManagementEventWatcher).IsAssignableFrom(p))
                .Select(x => (ManagementEventWatcher)Activator.CreateInstance(x))
                .Where(w => w.Query.QueryString == endQuery && w.Scope.Path.Path == scope).ToList();

            var watchers = new List<ManagementEventWatcher>();
            watchers.AddRange(startWatchers);
            watchers.AddRange(endWatchers);
            // Stop and dispose of each watcher
            foreach (var watcher in watchers)
            {
                watcher.Stop();
                watcher.Dispose();
            }
        }

        public static ManagementEventWatcher WatchForProcessStart(string ProcessName, EventArrivedEventHandler Callback)
        {
            string Query = "SELECT TargetInstance" +
                " FROM __InstanceCreationEvent " + "WITHIN  2 " +
                " WHERE TargetInstance ISA 'Win32_Process' " +
                " AND TargetInstance.Name = '" + ProcessName + "'";
            string Scope = "\\\\.\\root\\CIMV2";
            ManagementEventWatcher Watcher = new ManagementEventWatcher(Scope, Query);
            Watcher.EventArrived += Callback;
            Watcher.Start();
            return Watcher;
        }

        public static ManagementEventWatcher WatchForProcessEnd(string ProcessName, EventArrivedEventHandler Callback)
        {
            string Query = "SELECT TargetInstance" +
                " FROM __InstanceDeletionEvent " + "WITHIN  2 " +
                " WHERE TargetInstance ISA 'Win32_Process' " +
                " AND TargetInstance.Name = '" + ProcessName + "'";
            string Scope = "\\\\.\\root\\CIMV2";
            ManagementEventWatcher Watcher = new ManagementEventWatcher(Scope, Query);
            Watcher.EventArrived += Callback;
            Watcher.Start();
            return Watcher;
        }

        public static ProcessInfo GetProcessInfo(EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            uint processId = Convert.ToUInt32(targetInstance["ProcessId"]);
            string executablePath = ProcessAPI.GetProcessPath(processId);
            

            return new ProcessInfo
            {
                ProcessId = Convert.ToInt32(targetInstance["ProcessId"]),
                ExecutablePath = executablePath
            };
        }

        public class ProcessInfo
        {
            public int ProcessId { get; set; }            
            public string ExecutablePath { get; set; }
        }

    }
}
