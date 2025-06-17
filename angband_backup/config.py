import json
from pathlib import Path

CONFIG_PATH = Path.home() / ".config" / "angband_backup.json"
DEFAULT_BACKUP_DIR = Path.home() / "Angband_Backups"


def load_config() -> dict:
    """Load configuration from ``CONFIG_PATH``.

    Returns a dictionary with at least a ``backup_dir`` key. If the
    configuration file does not exist, a dictionary containing the
    default backup directory is returned.
    """
    if CONFIG_PATH.is_file():
        try:
            with CONFIG_PATH.open("r", encoding="utf-8") as f:
                config = json.load(f)
        except json.JSONDecodeError:
            config = {}
    else:
        config = {}

    if "backup_dir" not in config:
        config["backup_dir"] = str(DEFAULT_BACKUP_DIR)

    return config


def save_config(config: dict) -> None:
    """Write ``config`` as JSON to ``CONFIG_PATH``.

    The directory containing ``CONFIG_PATH`` is created if it does not
    already exist.
    """
    CONFIG_PATH.parent.mkdir(parents=True, exist_ok=True)
    with CONFIG_PATH.open("w", encoding="utf-8") as f:
        json.dump(config, f, indent=4)
