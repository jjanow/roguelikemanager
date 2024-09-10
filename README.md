# Roguelike Manager

A quick and dirty save game manager for roguelikes. It will backup and restore saves for disasters/scumming. File locations are configured in the `config.json` file.

## Features

- **Reload Saves**: Refresh the list of save files.
- **Backup**: Create backups of your save files.
- **Restore**: Restore save files from backups.
- **Launch**: Launch the game with the selected save file.
- **Delete**: Delete selected save files.

## Configuration

The configuration file `config.json` contains the paths for the save files, backup location, and the executable path. Here's an example:

```json
{
  "SavePath": "C:\\Games\\Frogcomposband\\lib\\save\\",
  "BackupPath": "C:\\Games\\Frogcomposband\\lib\\save\\backup\\",
  "ExePath": "C:\\Games\\Frogcomposband\\frogcomposband.exe"
}
```

## User Interface

The application provides a simple user interface with buttons to perform various actions related to save file management.

- **Reload**: Refreshes the list of save files.
- **Backup**: Backs up the selected save file.
- **Restore**: Restores the selected save file from the backup.
- **Launch**: Launches the game with the selected save file.
- **Delete**: Deletes the selected save file.

## Building and Running

The project is a Windows Forms application written in C#. You can open the solution in Visual Studio and build it using the standard build process.
