using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace SmartSaveFolder
{
    public class ForceMoveFolder
    {
        public static bool Execute(string source, string target) // https://stackoverflow.com/a/35987838
        {
            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(source, target);
            }
            catch
            {
                try
                {
                    Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(source, target);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
