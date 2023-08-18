using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace FrogcomposbandManager
{
    public partial class Form1 : Form
    {
        public string[]? SaveFiles { get; set; }
        public string? SavePath { get; set; } //= @"C:\Games\Frogcomposband\lib\save\";
        public string? BackupPath { get; set; } //= @"C:\Games\Frogcomposband\lib\save\backup\";
        public string? ExePath { get; set; } //= @"C:\Games\Frogcomposband\frogcomposband.exe";

        public Form1()
        {
            InitializeComponent();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            try
            {
                string configJson = File.ReadAllText("config.json");
                JObject config = JObject.Parse(configJson);
                SavePath = (string?)config["SavePath"];
                BackupPath = (string?)config["BackupPath"];
                ExePath = (string?)config["ExePath"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the configuration: " + ex.Message);
                // You may set default values here if needed
            }
        }

        private void RefreshFileList()
        {
            try
            {
                lbSaves.Items.Clear();
                if (SavePath != null)
                {
                    SaveFiles = Directory.GetFiles(SavePath);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        private void btnRefreshSaves_Click(object sender, EventArgs e)
        {
            RefreshFileList();
        }

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
    }
}