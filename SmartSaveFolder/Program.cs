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

        static FormWindowState StartupWindowState = FormWindowState.Normal;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {          
            if (args.Length > 0)
            {
                // Check for the "-grantprivilege" command line argument
                if (args[0].ToLower() == "-grantprivilege")
                {
                    // Check that the username argument is present
                    if (args.Length > 1)
                    {
                        string username = string.Join(" ", args.Skip(1));

                        // Call the GrantPrivilege method with the specified username
                        Permissions.GrantPrivilege(username);

                        // Exit the application
                        return;
                    }
                    else
                    {
                        // Display an error message if the username argument is missing
                        Console.WriteLine("Error: Missing username argument for -grantprivilege option.");
                        return;
                    }
                }
                if (args[0].ToLower() == "-minimized")
                {
                    StartupWindowState = FormWindowState.Minimized;
                }
            }

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
                var _MainForm = new MainForm();
                _MainForm.WindowState = StartupWindowState;
                Application.Run(_MainForm);
            }
        }
    }
}
