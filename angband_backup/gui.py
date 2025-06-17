from __future__ import annotations

from pathlib import Path

from PySide6 import QtWidgets, QtGui

from .backup import perform_backup
from .config import load_config, save_config


class SettingsDialog(QtWidgets.QDialog):
    """Dialog for editing application settings."""

    def __init__(self, parent: QtWidgets.QWidget | None = None) -> None:
        super().__init__(parent)
        self.setWindowTitle("Settings")

        layout = QtWidgets.QFormLayout(self)

        self.backup_edit = QtWidgets.QLineEdit()
        browse_btn = QtWidgets.QPushButton("Browse…")

        dir_layout = QtWidgets.QHBoxLayout()
        dir_layout.addWidget(self.backup_edit)
        dir_layout.addWidget(browse_btn)

        layout.addRow("Backup destination", dir_layout)

        buttons = QtWidgets.QDialogButtonBox(
            QtWidgets.QDialogButtonBox.Ok | QtWidgets.QDialogButtonBox.Cancel
        )
        layout.addWidget(buttons)

        browse_btn.clicked.connect(self.on_browse)
        buttons.accepted.connect(self.accept)
        buttons.rejected.connect(self.reject)

    def on_browse(self) -> None:
        directory = QtWidgets.QFileDialog.getExistingDirectory(
            self, "Select backup destination", self.backup_edit.text()
        )
        if directory:
            self.backup_edit.setText(directory)

    def set_backup_dir(self, path: str) -> None:
        self.backup_edit.setText(path)

    def backup_dir(self) -> str:
        return self.backup_edit.text()


class MainWindow(QtWidgets.QMainWindow):
    """Main application window."""

    def __init__(self) -> None:
        super().__init__()
        self.setWindowTitle("Angband Backup")
        self._build_ui()
        self._create_tray_icon()
        # Start hidden so the application primarily runs from the tray
        self.hide()

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

    def _create_tray_icon(self) -> None:
        """Initialise system tray icon and menu."""
        icon = self.style().standardIcon(QtWidgets.QStyle.SP_ComputerIcon)
        self.tray_icon = QtWidgets.QSystemTrayIcon(icon, self)

        menu = QtWidgets.QMenu()
        backup_action = menu.addAction("Backup Now")
        settings_action = menu.addAction("Settings")
        exit_action = menu.addAction("Exit")
        self.tray_icon.setContextMenu(menu)

        backup_action.triggered.connect(self.on_backup)
        settings_action.triggered.connect(self.on_settings)
        exit_action.triggered.connect(QtWidgets.QApplication.quit)

        self.tray_icon.show()

    def on_backup(self) -> None:
        config = load_config()
        source_dir = config.get("source_dir", str(Path.home()))
        target_dir = config["backup_dir"]
        status = perform_backup(source_dir, target_dir)
        if status.startswith("Backup successful"):
            QtWidgets.QMessageBox.information(self, "Backup", status)
        else:
            QtWidgets.QMessageBox.critical(self, "Backup", status)

    def on_settings(self) -> None:
        config = load_config()
        dialog = SettingsDialog(self)
        dialog.set_backup_dir(config["backup_dir"])
        if dialog.exec():
            config["backup_dir"] = dialog.backup_dir()
            save_config(config)
            QtWidgets.QMessageBox.information(
                self, "Settings", "Backup destination updated"
            )


def main() -> None:
    app = QtWidgets.QApplication([])
    window = MainWindow()
    # The main window is hidden by default; run the event loop
    app.exec()


if __name__ == "__main__":
    main()
