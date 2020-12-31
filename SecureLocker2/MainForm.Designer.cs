namespace SecureLocker2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.encryptFile = new System.Windows.Forms.Button();
            this.encryptFolder = new System.Windows.Forms.Button();
            this.decryptFolder = new System.Windows.Forms.Button();
            this.decryptFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // encryptFile
            // 
            this.encryptFile.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.encryptFile.Location = new System.Drawing.Point(113, 101);
            this.encryptFile.Name = "encryptFile";
            this.encryptFile.Size = new System.Drawing.Size(210, 32);
            this.encryptFile.TabIndex = 4;
            this.encryptFile.Text = "Dosya Şifrele";
            this.encryptFile.UseVisualStyleBackColor = true;
            this.encryptFile.Click += new System.EventHandler(this.encryptFile_Click);
            // 
            // encryptFolder
            // 
            this.encryptFolder.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.encryptFolder.Location = new System.Drawing.Point(113, 239);
            this.encryptFolder.Name = "encryptFolder";
            this.encryptFolder.Size = new System.Drawing.Size(210, 32);
            this.encryptFolder.TabIndex = 5;
            this.encryptFolder.Text = "Klasör Şifrele";
            this.encryptFolder.UseVisualStyleBackColor = true;
            this.encryptFolder.Click += new System.EventHandler(this.encryptFolder_Click);
            // 
            // decryptFolder
            // 
            this.decryptFolder.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decryptFolder.Location = new System.Drawing.Point(113, 286);
            this.decryptFolder.Name = "decryptFolder";
            this.decryptFolder.Size = new System.Drawing.Size(210, 32);
            this.decryptFolder.TabIndex = 7;
            this.decryptFolder.Text = "Klasör Şifresi Çöz";
            this.decryptFolder.UseVisualStyleBackColor = true;
            this.decryptFolder.Click += new System.EventHandler(this.decryptFolder_Click);
            // 
            // decryptFile
            // 
            this.decryptFile.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decryptFile.Location = new System.Drawing.Point(113, 149);
            this.decryptFile.Name = "decryptFile";
            this.decryptFile.Size = new System.Drawing.Size(210, 32);
            this.decryptFile.TabIndex = 6;
            this.decryptFile.Text = "Dosya Şifresi Çöz";
            this.decryptFile.UseVisualStyleBackColor = true;
            this.decryptFile.Click += new System.EventHandler(this.decryptFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(143, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 33);
            this.label1.TabIndex = 8;
            this.label1.Text = "SecureLocker2";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 335);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(420, 32);
            this.progressBar.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SecureLocker2.Properties.Resources._891399;
            this.pictureBox1.Location = new System.Drawing.Point(83, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 378);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.decryptFolder);
            this.Controls.Add(this.decryptFile);
            this.Controls.Add(this.encryptFolder);
            this.Controls.Add(this.encryptFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Ana Menü";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button encryptFile;
        private System.Windows.Forms.Button encryptFolder;
        private System.Windows.Forms.Button decryptFolder;
        private System.Windows.Forms.Button decryptFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}