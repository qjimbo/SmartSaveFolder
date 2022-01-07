using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Diagnostics;

namespace SmartSaveFolder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public ManagementEventWatcher watchForStart;
        public ManagementEventWatcher watchForEnd;
        public string oldSaveGamePath;
        public string bakSaveGamePath;
        public string newSaveGamePath;
        public bool closing = false;
        public bool running = false;

        public void WriteToLog(string text, string notify = "")
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.Invoke((MethodInvoker)delegate()
            {
                textBox.AppendText("[" + date + "] " + text + Environment.NewLine);
                File.AppendAllText("log.txt", "[" + date + "] " + text + Environment.NewLine);
                if (notify != "")
                    notifyIcon.ShowBalloonTip(1000, notify, text, ToolTipIcon.Info);
            });
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;
            oldSaveGamePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HelloGames\\NMS";

            // Wait
            watchForStart = Watcher.WatchForProcessStart("NMS.exe",NoMansSkyStarted);
            watchForEnd = Watcher.WatchForProcessEnd("NMS.exe", NoMansSkyClosed);
            WriteToLog("Waiting for No Man's Sky to Start");
            
        }

        private void NoMansSkyStarted(object sender, EventArrivedEventArgs e)
        {
            WriteToLog("No Man's Sky Started (SmartSaveFolder cannot exit while NMS is running)");
            running = true;

            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            int id = Int32.Parse(((UInt32)targetInstance["ProcessId"]).ToString());
            string path = (string)targetInstance["ExecutablePath"];
            newSaveGamePath = Path.Combine(Path.GetDirectoryName(path), "SAVEGAMES");

            // Move Existing Save Games Path
            bakSaveGamePath = oldSaveGamePath + ".bak";
            if (Directory.Exists(oldSaveGamePath) && !Directory.Exists(bakSaveGamePath))
            {

                    if(ForceMoveFolder.Execute(oldSaveGamePath, bakSaveGamePath))
                        WriteToLog("Moved Existing Save Games Folder");
                    else
                    {
                        WriteToLog("Unable to move existing Save Games Folder, please make sure you have no files or explorer windows open.","SmartSaveFolder Error");
                        return;
                    }                
            }
            // Create Symbolic Link Path
            if (!Directory.Exists(newSaveGamePath))
                Directory.CreateDirectory(newSaveGamePath);
            var success = SymbolicLink.Create(oldSaveGamePath,newSaveGamePath);

            // Create DefaultUser folder and any Steam User Folders if Found
            if(Directory.Exists(bakSaveGamePath))
            {
                var bakFolderPaths = Directory.GetFileSystemEntries(bakSaveGamePath);
                foreach (var bakFolderPath in bakFolderPaths)
                {
                    string bakFolder = Path.GetFileName(bakFolderPath);
                    if (bakFolder.StartsWith("st_") || bakFolder == "DefaultUser")
                    {
                        var newSaveGamePathUser = newSaveGamePath + "\\" + bakFolder;
                        var newSaveGamePathUserCache = newSaveGamePathUser + "\\cache";
                        if (!Directory.Exists(newSaveGamePathUser))
                        {
                            WriteToLog("Created User Save Game Path " + bakFolder);
                            Directory.CreateDirectory(newSaveGamePathUser);
                        }
                        if (!Directory.Exists(newSaveGamePathUserCache))
                            Directory.CreateDirectory(newSaveGamePathUserCache);
                        
                    }
                }
            }
            this.Invoke((MethodInvoker)delegate()
            {
                buttonExit.Enabled = false;
                buttonCurrent.Enabled = true;
            });
            WriteToLog("Save Games Path set to " + newSaveGamePath, "No Man's Sky Detected");

        }

        private void NoMansSkyClosed(object sender, EventArrivedEventArgs e)
        {
            WriteToLog("No Man's Sky Closed");
            running = false;
            this.Invoke((MethodInvoker)delegate()
            {
                buttonExit.Enabled = true;
                buttonCurrent.Enabled = false;
            });
            SymbolicLink.Delete(oldSaveGamePath); // Remove Link
            if (Directory.Exists(bakSaveGamePath))
            {
                if (ForceMoveFolder.Execute(bakSaveGamePath, oldSaveGamePath)) // Restore original path instead of link
                    WriteToLog("Restored Save Games Folder");
                else
                {
                    WriteToLog("Unable to restore Save Games Folder, please make sure you have no files or explorer windows open.", "SmartSaveFolder Error");
                    return;
                }
            }
            WriteToLog("Save Games Path set to default location", "No Man's Sky Closed");
        }

        private void buttonOriginal_Click(object sender, EventArgs e)
        {
            if (running)
                if(Directory.Exists(bakSaveGamePath))
                    Process.Start(bakSaveGamePath);
            else
               if(Directory.Exists(oldSaveGamePath))
                    Process.Start(oldSaveGamePath);
        }

        private void buttonCurrent_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(newSaveGamePath))
                Process.Start(newSaveGamePath);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            closing = true;
            Application.Exit();
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            
            this.Show();
            WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.ShowBalloonTip(1000, "SmartSaveFolder is still running", "Click on the tray icon and click the exit button to close", ToolTipIcon.Info);
                Hide();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closing)
                return;
            notifyIcon.ShowBalloonTip(1000, "SmartSaveFolder is still running", "Click on the tray icon and click the exit button to close", ToolTipIcon.Info);
            Hide();
            e.Cancel = true;
        }

        private void pictureBoxLink_Click(object sender, EventArgs e)
        {
            Process.Start("https://nomansskyretro.com");
        }

        private void pictureBoxLink_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pictureBoxLink, "Visit NoMansSkyRetro.com");
        }

    }
}
