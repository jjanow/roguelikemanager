namespace RoguelikeManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRefreshSaves = new Button();
            lbSaves = new ListBox();
            btnBackup = new Button();
            btnRestore = new Button();
            btnLaunch = new Button();
            btnDelete = new Button();
            gameSelectionComboBox = new ComboBox();
            SuspendLayout();
            // 
            // btnRefreshSaves
            // 
            btnRefreshSaves.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnRefreshSaves.ForeColor = SystemColors.ActiveCaptionText;
            btnRefreshSaves.Location = new Point(27, 20);
            btnRefreshSaves.Name = "btnRefreshSaves";
            btnRefreshSaves.Size = new Size(137, 59);
            btnRefreshSaves.TabIndex = 0;
            btnRefreshSaves.Text = "RELOAD";
            btnRefreshSaves.UseVisualStyleBackColor = true;
            btnRefreshSaves.Click += btnRefreshSaves_Click;
            // 
            // lbSaves
            // 
            lbSaves.BackColor = SystemColors.InfoText;
            lbSaves.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            lbSaves.ForeColor = SystemColors.Window;
            lbSaves.FormattingEnabled = true;
            lbSaves.ItemHeight = 37;
            lbSaves.Location = new Point(27, 108);
            lbSaves.Name = "lbSaves";
            lbSaves.Size = new Size(473, 670);
            lbSaves.TabIndex = 1;
            // 
            // btnBackup
            // 
            btnBackup.ForeColor = SystemColors.ActiveCaptionText;
            btnBackup.Location = new Point(624, 108);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new Size(128, 100);
            btnBackup.TabIndex = 2;
            btnBackup.Text = "BACKUP";
            btnBackup.UseVisualStyleBackColor = true;
            btnBackup.Click += btnBackup_Click;
            // 
            // btnRestore
            // 
            btnRestore.ForeColor = SystemColors.ActiveCaptionText;
            btnRestore.Location = new Point(624, 400);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(128, 100);
            btnRestore.TabIndex = 3;
            btnRestore.Text = "RESTORE";
            btnRestore.UseVisualStyleBackColor = true;
            btnRestore.Click += btnRestore_Click;
            // 
            // btnLaunch
            // 
            btnLaunch.ForeColor = SystemColors.ActiveCaptionText;
            btnLaunch.Location = new Point(624, 692);
            btnLaunch.Name = "btnLaunch";
            btnLaunch.Size = new Size(128, 100);
            btnLaunch.TabIndex = 4;
            btnLaunch.Text = "LAUNCH";
            btnLaunch.UseVisualStyleBackColor = true;
            btnLaunch.Click += btnLaunch_Click;
            // 
            // btnDelete
            // 
            btnDelete.ForeColor = SystemColors.ActiveCaptionText;
            btnDelete.Location = new Point(363, 20);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(137, 59);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // gameSelectionComboBox
            // 
            gameSelectionComboBox.DropDownHeight = 115;
            gameSelectionComboBox.DropDownWidth = 180;
            gameSelectionComboBox.IntegralHeight = false;
            gameSelectionComboBox.Location = new Point(580, 20);
            gameSelectionComboBox.Name = "gameSelectionComboBox";
            gameSelectionComboBox.Size = new Size(172, 28);
            gameSelectionComboBox.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MidnightBlue;
            ClientSize = new Size(837, 838);
            Controls.Add(btnDelete);
            Controls.Add(btnLaunch);
            Controls.Add(btnRestore);
            Controls.Add(btnBackup);
            Controls.Add(lbSaves);
            Controls.Add(btnRefreshSaves);
            Controls.Add(gameSelectionComboBox);
            ForeColor = SystemColors.ControlLightLight;
            Name = "Form1";
            ShowIcon = false;
            Text = "Frogcomposband Manager";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnRefreshSaves;
        private ListBox lbSaves;
        private Button btnBackup;
        private Button btnRestore;
        private Button btnLaunch;
        private Button btnDelete;
        private ComboBox gameSelectionComboBox;
    }
}