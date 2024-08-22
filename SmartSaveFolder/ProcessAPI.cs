using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

public class ProcessAPI
{
    [StructLayout(LayoutKind.Sequential)]
    private struct PROCESSENTRY32
    {
        public uint dwSize;
        public uint cntUsage;
        public uint th32ProcessID;
        public IntPtr th32DefaultHeapID;
        public uint th32ModuleID;
        public uint cntThreads;
        public uint th32ParentProcessID;
        public int pcPriClassBase;
        public uint dwFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szExeFile;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

    [DllImport("kernel32.dll")]
    private static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

    [DllImport("kernel32.dll")]
    private static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern uint QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, StringBuilder lpExeName, ref uint lpdwSize);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

    private const uint PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;

    public struct ProcessInfo
    {
        public uint Id { get; set; }
        public string Path { get; set; }
    }

    public static List<ProcessInfo> GetProcessList()
    {
        var processList = new List<ProcessInfo>();
        IntPtr snapshot = CreateToolhelp32Snapshot(2, 0);

        try
        {
            var processEntry = new PROCESSENTRY32 { dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32)) };

            if (Process32First(snapshot, ref processEntry))
            {
                do
                {
                    string fullPath = GetProcessFullPath(processEntry.th32ProcessID);
                    processList.Add(new ProcessInfo { Id = processEntry.th32ProcessID, Path = fullPath });
                } while (Process32Next(snapshot, ref processEntry));
            }
        }
        finally
        {
            CloseHandle(snapshot);
        }

        return processList;
    }

    public static string GetProcessPath(uint processId)
    {
        return GetProcessFullPath(processId);
    }

    private static string GetProcessFullPath(uint processId)
    {
        var buffer = new StringBuilder(1024);
        uint size = (uint)buffer.Capacity;

        IntPtr hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, processId);
        if (hProcess != IntPtr.Zero)
        {
            try
            {
                if (QueryFullProcessImageName(hProcess, 0, buffer, ref size) != 0)
                {
                    return buffer.ToString();
                }
            }
            finally
            {
                CloseHandle(hProcess);
            }
        }

        return "Path not available";
    }
}