using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SmartSaveFolder
{
    public class SymbolicLink
    {
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        static extern bool CreateSymbolicLink(
            string lpSymlinkFileName,
            string lpTargetFileName,
            uint dwFlags
        );
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool RemoveDirectory(string lpPathName);

        const uint SYMBOLIC_LINK_FLAG_FILE = 0x0;
        const uint SYMBOLIC_LINK_FLAG_DIRECTORY = 0x1;

        public static bool Create(string sourceFolder, string targetFolder)
        {
            return CreateSymbolicLink(
                sourceFolder,            // New Link
                targetFolder,            // Source
                SYMBOLIC_LINK_FLAG_DIRECTORY);
        }

        public static void Delete(string sourceFolder)
        {
            RemoveDirectory(sourceFolder);
        }
    }
}
