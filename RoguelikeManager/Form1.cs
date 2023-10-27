using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace RoguelikeManager
{
    public partial class Form1 : Form
    {
        public string[]? SaveFiles { get; set; }
        public string? SavePath { get; set; } //= @"C:\Games\Frogcomposband\lib\save\";
        public string? BackupPath { get; set; } //= @"C:\Games\Frogcomposband\lib\save\backup\";
        public string? ExePath { get; set; } //= @"C:\Games\Frogcomposband\frogcomposband.exe";
        public string? FileFilter { get; set; }

        public Form1()
        {
            InitializeComponent();
            LoadConfiguration();
            gameSelectionComboBox.SelectedIndexChanged += new EventHandler(GameSelectionComboBox_SelectedIndexChanged);
        }

        private void LoadConfiguration()
        {
            try
            {
                string configJson = File.ReadAllText("config.json");
                JObject? config = JObject.Parse(configJson);
                JObject? games = config["Games"] as JObject;

                if (config["Games"] is JObject games && gameSelectionComboBox.SelectedItem != null)
                {
                    string selectedGame = gameSelectionComboBox.SelectedItem.ToString();
                    FileFilter = (string?)games[selectedGame]?["FileFilter"];
                }

                // Populate the gameSelectionComboBox
                if (games != null)
                {
                    foreach (var game in games.Properties())
                    {
                        gameSelectionComboBox.Items.Add(game.Name);
                    }
                }

                // Set the last selected game
                string? lastSelectedGame = config["Settings"]?["LastSelectedGame"]?.ToString();
                if (lastSelectedGame != null)
                {
                    gameSelectionComboBox.SelectedItem = lastSelectedGame;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the configuration: " + ex.Message);
            }
        }

        // Refreshes the list of save files by reading them from the SavePath directory
        private void RefreshFileList()
        {
            try
            {
                lbSaves.Items.Clear();
                if (SavePath != null)
                {
                    string fileFilter = FileFilter ?? "*.*";  // Use the FileFilter or default to "*.*"
                    SaveFiles = Directory.GetFiles(SavePath, fileFilter);
                    foreach (string file in SaveFiles)
                    {
                        if (File.Exists(file))
                        {
                            lbSaves.Items.Add(file.Split("\\").Last().Trim());
                        }
                    }
                    if (lbSaves.Items.Count > 0)
                    {
                        lbSaves.SelectedItem = lbSaves.Items[0];
                    }
                }
                else
                {
                    SaveFiles = Array.Empty<string>();
                    // Optionally, you could log a warning or show a message to the user here
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while refreshing the file list: " + ex.Message);
            }
        }

        // Validates the configuration paths
        private string ValidateConfiguration()
        {
            if (SavePath != null && !Directory.Exists(SavePath))
                return "Save path is not a valid directory.";
            if (BackupPath != null && !Directory.Exists(BackupPath))
                return "Backup path is not a valid directory.";
            if (ExePath != null && !File.Exists(ExePath))
                return "Executable path is not a valid file.";

            return string.Empty; // No validation errors
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        private void btnRefreshSaves_Click(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        // Handles the click event for the Backup button, copying the selected save file to the backup directory
        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(SavePath + lbSaves.SelectedItem, BackupPath + lbSaves.SelectedItem, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while backing up the file: " + ex.Message);
            }
        }

        // Handles the click event for the Restore button, copying the selected backup file to the save directory
        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(BackupPath + lbSaves.SelectedItem, SavePath + lbSaves.SelectedItem, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while restoring the file: " + ex.Message);
            }
        }

        // Handles the click event for the Launch button, starting the game with the selected save file
        private void btnLaunch_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = ExePath;
                process.StartInfo.Arguments = lbSaves.SelectedItem.ToString();
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while launching the game: " + ex.Message);
            }
        }

        // Handles the click event for the Delete button, deleting the selected save file and its backup
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(SavePath + lbSaves.SelectedItem);
                File.Delete(BackupPath + lbSaves.SelectedItem);
                RefreshFileList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the file: " + ex.Message);
            }
        }

#nullable disable
        private void GameSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Read the existing config.json
                string configJson = File.ReadAllText("config.json");
                JObject config = JObject.Parse(configJson);

                // Update the LastSelectedGame in the Settings
                if (config["Settings"] is JObject settings)
                {
                    settings["LastSelectedGame"] = gameSelectionComboBox.SelectedItem?.ToString();
                }
                else
                {
                    // If "Settings" doesn't exist, create it
                    config["Settings"] = new JObject(new JProperty("LastSelectedGame", gameSelectionComboBox.SelectedItem?.ToString()));
                }

                // Write the updated config back to config.json
                File.WriteAllText("config.json", config.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the configuration: " + ex.Message);
            }
        }
#nullable restore
    }
}