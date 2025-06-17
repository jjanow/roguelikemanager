from __future__ import annotations

from datetime import datetime
from pathlib import Path
import shutil


def perform_backup(source_dir: str, target_base_dir: str) -> str:
    """Create a timestamped copy of ``source_dir``.

    A new subdirectory named with the current timestamp (``YYYYMMDD_HHMMSS``)
    is created inside ``target_base_dir``. All files and directories from
    ``source_dir`` are copied into this new directory.

    Parameters
    ----------
    source_dir:
        The directory whose contents should be backed up.
    target_base_dir:
        The directory in which to create the timestamped backup directory.

    Returns
    -------
    str
        The path to the created backup directory.
    """
    src = Path(source_dir)
    dst_base = Path(target_base_dir)
    timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
    backup_dir = dst_base / timestamp
    backup_dir.mkdir(parents=True, exist_ok=True)

    for item in src.iterdir():
        target = backup_dir / item.name
        if item.is_dir():
            shutil.copytree(item, target)
        else:
            shutil.copy2(item, target)

    return str(backup_dir)
