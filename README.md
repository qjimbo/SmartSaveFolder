# SmartSaveFolder for No Man's Sky

SmartSaveFolder changes the Save Game Location of No Man's Sky to be a local SAVEGAMES subfolder next to each NMS.exe Executable. The purpose is to avoid Save Game conflicts when you have multiple versions of No Man's Sky installed.

Release page: https://nomansskyretro.com/smartsavefolder.php

Reddit Thread: https://www.reddit.com/r/NMS_Foundations/comments/rr43q8/smartsavefolder_for_no_mans_sky_pc_solve_savegame/

The program is functionally complete.

# Notes

The project is written in C# and the solution file is for Visual Studio 2010, using .NET 4.0.

SmartSaveFolder uses ManagementEventWatcher to detect when NMS.exe is launched (InstanceCreationEvent) and closed (InstanceDeletionEvent).

Moving folders is done with Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory as this is more robust than C#'s Directory.Move when the directory is in use.

Folders are replaced with Symbolic Links using Kernal32.dll's CreateSymbolicLink function.

On the first time running, the current user is granted permission to create Symbolic Links and a restart is required. As of v1.03, admin rights are no longer needed.
