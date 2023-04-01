using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace SmartSaveFolder
{
    static class Program
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Get the current executable's filename
            string exeName = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName);

            // Check if the app is already running
            Process[] processes = Process.GetProcessesByName(exeName).Where(p => p.Id != Process.GetCurrentProcess().Id).ToArray();

            if (processes.Length > 0)
            {
                // If the app is running, focus on the existing instance
                SetForegroundWindow(processes[0].MainWindowHandle);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
