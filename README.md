# Angband Backup Utility

This repository contains a small GUI application written with PySide6 to
create timestamped backups of a directory.

## Building a standalone executable

A PyInstaller spec file is provided so the application can be bundled into
a single-file executable.

1. Install PyInstaller (only required once):

   ```bash
   pip install pyinstaller
   ```

2. Run PyInstaller using the spec file:

   ```bash
   pyinstaller pyinstaller.spec
   ```

The resulting executable will be placed in the `dist/` directory.
