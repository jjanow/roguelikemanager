from __future__ import annotations

from pathlib import Path

from PySide6 import QtWidgets

from .backup import perform_backup
from .config import load_config


class MainWindow(QtWidgets.QMainWindow):
    """Main application window."""

    def __init__(self) -> None:
        super().__init__()
        self.setWindowTitle("Angband Backup")
        self._build_ui()

    def _build_ui(self) -> None:
        central = QtWidgets.QWidget()
        layout = QtWidgets.QVBoxLayout(central)

        self.backup_btn = QtWidgets.QPushButton("Backup Now")
        self.settings_btn = QtWidgets.QPushButton("Settings")

        layout.addWidget(self.backup_btn)
        layout.addWidget(self.settings_btn)

        self.setCentralWidget(central)

        exit_action = QtWidgets.QAction("Exit", self)
        exit_action.triggered.connect(QtWidgets.QApplication.quit)

        file_menu = self.menuBar().addMenu("File")
        file_menu.addAction(exit_action)

        self.backup_btn.clicked.connect(self.on_backup)
        self.settings_btn.clicked.connect(self.on_settings)

    def on_backup(self) -> None:
        config = load_config()
        source_dir = config.get("source_dir", str(Path.home()))
        target_dir = config["backup_dir"]
        path = perform_backup(source_dir, target_dir)
        QtWidgets.QMessageBox.information(self, "Backup Completed", f"Backup saved to {path}")

    def on_settings(self) -> None:
        QtWidgets.QMessageBox.information(self, "Settings", "Settings placeholder")


def main() -> None:
    app = QtWidgets.QApplication([])
    window = MainWindow()
    window.show()
    app.exec()


if __name__ == "__main__":
    main()
