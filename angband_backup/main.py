"""Entry point for the Angband backup GUI application."""

from PySide6 import QtWidgets

from .config import load_config
from .gui import MainWindow


def main() -> None:
    """Launch the application."""
    # Load configuration so any defaults are initialised early.
    _ = load_config()

    app = QtWidgets.QApplication([])
    window = MainWindow()
    window.show()
    app.exec()


if __name__ == "__main__":
    main()
