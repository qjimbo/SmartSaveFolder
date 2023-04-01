using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace SmartSaveFolder
{
    public class Permissions
    {
        public static void GrantPrivilege(string username)
        {
            string tempDir = Path.GetTempPath();
            string outputFilePath = Path.Combine(tempDir, "secpol.cfg");
            string localSdbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "security", "local.sdb");

            ExportSecurityPolicy(outputFilePath);

            string oldValue = "SeCreateSymbolicLinkPrivilege = ";
            string newValue = "SeCreateSymbolicLinkPrivilege = " + username + ",";
            ReplaceSecurityPolicyValue(outputFilePath, oldValue, newValue);

            ConfigureSecurityPolicy(outputFilePath, localSdbPath);

            File.Delete(outputFilePath);
        }

        private static string ExportSecurityPolicy(string outputFilePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "secedit.exe";
            startInfo.Arguments = "/export /cfg \"" + outputFilePath + "\"";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }

        private static void ReplaceSecurityPolicyValue(string filePath, string oldValue, string newValue)
        {
            string fileContent = File.ReadAllText(filePath);
            fileContent = fileContent.Replace(oldValue, newValue);
            File.WriteAllText(filePath, fileContent);
        }

        private static string ConfigureSecurityPolicy(string inputFilePath, string localSdbPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "secedit.exe";
            startInfo.Arguments = "/configure /db \"" + localSdbPath + "\" /cfg \"" + inputFilePath + "\"";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }
    }
}
