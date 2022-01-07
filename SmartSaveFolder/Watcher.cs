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


    }
}
