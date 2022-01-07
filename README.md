# SmartSaveFolder for No Man's Sky

SmartSaveFolder changes the Save Game Location of No Man's Sky to be a local SAVEGAMES subfolder next to each NMS.exe Executable.

Release page: https://nomansskyretro.com/smartsavefolder.php
Reddit Thread: https://www.reddit.com/r/NMS_Foundations/comments/rr43q8/smartsavefolder_for_no_mans_sky_pc_solve_savegame/

The program is functionally complete.

# Notes

The project is written in C# and the solution file is for Visual Studio 2010, using .NET 4.0.

SmartSaveFolder uses ManagementEventWatcher to detect when NMS.exe is launched (InstanceCreationEvent) and closed (InstanceDeletionEvent).

Moving folders is done with Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory as this is more robust than C#'s Directory.Move when the directory is in use.

Folders are replaced with Symbolic Links using Kernal32.dll's CreateSymbolicLink function.

# Future Plans

1) Don't require elevated permissions each time on launch, modify HelloGames folder permissions once as elevated so doesn't need elevated next time.
2) Publisher sign the app so doesn't show "Unknown Publisher" on the UAC warning.
3) Freeze NMS.exe process until folder rerouted to avoid issues with 1.09.1 and older versions that sometimes don't find the save file because it is not rerouted quickly enough.
4) Multiple Save slots: o Default o Number
5) SCREENSHOTS folder and customizable hotkey - idea: copy save game embedded in screenshot, allow loading a savegame from screenshot (copy files to SAVEGAMES_SCR, then close and restart NMS in "Screenshot Load Mode")
