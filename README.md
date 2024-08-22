# SmartSaveFolder for No Man's Sky

![image](https://github.com/user-attachments/assets/659dc461-af73-4585-8131-6c04b724c7dc)

SmartSaveFolder is an application that runs in the background and changes the Save Game Location of No Man's Sky to be a local SAVEGAMES subfolder next to each NMS.exe Executable. This allows multiple versions of No Man's Sky to be installed with their own separate Save Game data without using scripts or launchers.

## How to Use

1. Start the SmartSaveFolder.exe before you launch No Man's Sky.
2. The first time running, you will have to grant SmartSaveFolder administrator permissions and restart the computer to allow the redirection of the Save Data folder. After that, no administrator permissions are required.
3. After restarting, launch SmartSaveFolder again and Start No Man's Sky. SmartSaveFolder will create a new SAVEGAMES subfolder in the same folder as the NMS.exe you are running for storing Save Game data.

For example, if you run:
```
C:\No Man's Sky_v1.381\Binaries\NMS.exe
```
The save game folder will be:
```
C:\No Man's Sky_v1.381\Binaries\SAVEGAMES
```

If you want to copy existing savegames to this folder, you can click "Open Original Save Games Folder" to view your original save games and "Open Current Save Games Folder" while No Man's Sky is running to view the new save games folder.

You can close and open different No Man's Sky installations and leave SmartSaveFolder running in the background the entire time. SmartSaveFolder can also be set to run at startup.

## Download

[Download SmartSaveFolder v1.06](https://github.com/qjimbo/smartsavefolder/releases/latest)

## Source Code

This project is open source under the MIT License. The solution file is for Visual Studio 2010, using .NET 4.0.

### Technical Details

- SmartSaveFolder uses ManagementEventWatcher to detect when NMS.exe is launched (InstanceCreationEvent) and closed (InstanceDeletionEvent).
- Moving folders is done with Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory as this is more robust than C#'s Directory.Move when the directory is in use.
- Folders are replaced with Symbolic Links using Kernal32.dll's CreateSymbolicLink function.
- As of v1.03, admin rights are no longer needed after the initial setup and restart.

## Links

- [Reddit Thread](https://www.reddit.com/r/NMS_Foundations/comments/rr43q8/smartsavefolder_for_no_mans_sky_pc_solve_savegame/)
