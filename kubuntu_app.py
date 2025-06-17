import json
import os
import fnmatch
import shutil
import subprocess
import tkinter as tk
from tkinter import ttk, messagebox

CONFIG_PATH = os.path.join('RoguelikeManager', 'config.json')

class ManagerApp(tk.Tk):
    def __init__(self):
        super().__init__()
        self.title('Roguelike Manager')
        self.geometry('400x300')
        self.config_data = {}
        self.save_path = ''
        self.backup_path = ''
        self.exe_path = ''
        self.file_filter = '*'
        self.create_widgets()
        self.load_configuration()

    def create_widgets(self):
        frame = ttk.Frame(self)
        frame.pack(fill='x', padx=10, pady=5)

        ttk.Label(frame, text='Game:').pack(side='left')
        self.game_box = ttk.Combobox(frame, state='readonly')
        self.game_box.pack(side='left', fill='x', expand=True)
        self.game_box.bind('<<ComboboxSelected>>', self.on_game_change)

        self.listbox = tk.Listbox(self)
        self.listbox.pack(fill='both', expand=True, padx=10, pady=5)

        btn_frame = ttk.Frame(self)
        btn_frame.pack(padx=10, pady=5)

        ttk.Button(btn_frame, text='Reload', command=self.refresh_files).grid(row=0, column=0, padx=2)
        ttk.Button(btn_frame, text='Backup', command=self.backup_file).grid(row=0, column=1, padx=2)
        ttk.Button(btn_frame, text='Restore', command=self.restore_file).grid(row=0, column=2, padx=2)
        ttk.Button(btn_frame, text='Launch', command=self.launch_game).grid(row=0, column=3, padx=2)
        ttk.Button(btn_frame, text='Delete', command=self.delete_file).grid(row=0, column=4, padx=2)

    def load_configuration(self):
        try:
            with open(CONFIG_PATH, 'r', encoding='utf-8') as f:
                self.config_data = json.load(f)
            games = list(self.config_data.get('Games', {}).keys())
            self.game_box['values'] = games
            last = self.config_data.get('Settings', {}).get('LastSelectedGame')
            if last in games:
                self.game_box.set(last)
            elif games:
                self.game_box.current(0)
            self.on_game_change()
        except Exception as e:
            messagebox.showerror('Configuration Error', str(e))

    def save_last_game(self):
        self.config_data.setdefault('Settings', {})['LastSelectedGame'] = self.game_box.get()
        try:
            with open(CONFIG_PATH, 'w', encoding='utf-8') as f:
                json.dump(self.config_data, f, indent=4)
        except Exception as e:
            messagebox.showerror('Write Error', str(e))

    def on_game_change(self, event=None):
        game = self.game_box.get()
        info = self.config_data.get('Games', {}).get(game, {})
        self.save_path = info.get('SavePath', '')
        self.backup_path = info.get('BackupPath', '')
        self.exe_path = info.get('ExePath', '')
        self.file_filter = info.get('FileFilter', '*')
        self.save_last_game()
        self.refresh_files()

    def refresh_files(self):
        self.listbox.delete(0, tk.END)
        if os.path.isdir(self.save_path):
            for fname in os.listdir(self.save_path):
                if fnmatch.fnmatch(fname, self.file_filter):
                    self.listbox.insert(tk.END, fname)

    def _selected(self):
        if not self.listbox.curselection():
            messagebox.showwarning('No Selection', 'Select a save file first.')
            return None
        return self.listbox.get(self.listbox.curselection()[0])

    def backup_file(self):
        fname = self._selected()
        if not fname:
            return
        try:
            os.makedirs(self.backup_path, exist_ok=True)
            shutil.copy2(os.path.join(self.save_path, fname), os.path.join(self.backup_path, fname))
        except Exception as e:
            messagebox.showerror('Backup Error', str(e))

    def restore_file(self):
        fname = self._selected()
        if not fname:
            return
        try:
            shutil.copy2(os.path.join(self.backup_path, fname), os.path.join(self.save_path, fname))
        except Exception as e:
            messagebox.showerror('Restore Error', str(e))

    def launch_game(self):
        fname = self._selected()
        if not fname:
            return
        try:
            subprocess.Popen([self.exe_path, fname])
        except Exception as e:
            messagebox.showerror('Launch Error', str(e))

    def delete_file(self):
        fname = self._selected()
        if not fname:
            return
        try:
            os.remove(os.path.join(self.save_path, fname))
            backup = os.path.join(self.backup_path, fname)
            if os.path.exists(backup):
                os.remove(backup)
            self.refresh_files()
        except Exception as e:
            messagebox.showerror('Delete Error', str(e))

if __name__ == '__main__':
    app = ManagerApp()
    app.mainloop()
