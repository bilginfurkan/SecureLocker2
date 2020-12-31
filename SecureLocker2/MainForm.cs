using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureLocker2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void encryptFile_Click(object sender, EventArgs e)
        {
            var file = GetFile();
            if (file == null)
                return;
            if (file.EndsWith(Cipher.ADDED_EXTENSION))
            {
                MessageBox.Show("Bu dosya zaten şifreli, bir daha şifrelemene gerek yok.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Cipher.EncryptFile(file, Program.key);
            MessageBox.Show("Başarıyla şifrelendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void decryptFile_Click(object sender, EventArgs e)
        {
            var file = GetFile($"Şifreli dosyalar (*{Cipher.ADDED_EXTENSION})|*{Cipher.ADDED_EXTENSION}");
            if (file == null)
                return;

            Cipher.DecryptFile(file, Program.key);
            MessageBox.Show("Başarılıyla şifresi çözüldü.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetFile(string filter = null)
        {
            var openFile = new OpenFileDialog()
            {
                RestoreDirectory = true,
                CheckFileExists = true,
                CheckPathExists = true
            };
            if (filter != null)
                openFile.Filter = filter;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                return openFile.FileName;
            }
            return null;
        }

        private string[] GetFiles(string extension = "*.*")
        {
            var openFolder = new CommonOpenFileDialog();
            openFolder.AllowNonFileSystemItems = true;
            openFolder.Multiselect = false;
            openFolder.IsFolderPicker = true;
            openFolder.Title = "Klasör seçiniz";

            if (openFolder.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return null;
            }

            // get all the directories in selected dirctory
            var dirs = openFolder.FileName;
            return Directory.GetFiles(dirs, extension, SearchOption.AllDirectories);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private async void encryptFolder_Click(object sender, EventArgs e)
        {
            if (await Crypt(true))
                MessageBox.Show("Başarılıyla klasörünüz şifrelendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void decryptFolder_Click(object sender, EventArgs e)
        {
            if (await Crypt(false))
                MessageBox.Show("Başarılıyla klasörünüzün şifresi çözüldü.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public async Task<bool> Crypt(bool encrypt = false, string[] fileArray = null)
        {
            var files = fileArray == null ? GetFiles(!encrypt ? "*"+Cipher.ADDED_EXTENSION : "*.*") : fileArray;
            if (files == null)
                return false;

            var fileChunked = Extensions.SplitToChunks(files, 2);
            var doneChunks = new HashSet<int>();

            var i = 0;

            ChangeButtonEnabled(false);
            foreach (var fileDump in fileChunked)
            {
                var n = i;
                TaskController.WorkAsync(() =>
                {
                    foreach (var file in fileDump)
                    {
                        if (encrypt && file.EndsWith(Cipher.ADDED_EXTENSION))
                            continue;

                        if (encrypt)
                            Cipher.EncryptFile(file, Program.key);
                        else
                            Cipher.DecryptFile(file, Program.key);
                    }

                    lock (doneChunks)
                        doneChunks.Add(n);
                    return 0;
                });
                i++;
            }

            progressBar.Value = 0;
            progressBar.Maximum = fileChunked.Count();
            while (LockAndGet(doneChunks).Count < fileChunked.Count())
            {
                progressBar.Value = LockAndGet(doneChunks).Count;
                await Task.Delay(100);
            }

            progressBar.Value = progressBar.Maximum;
            ChangeButtonEnabled(true);
            return true;
        }

        private void ChangeButtonEnabled(bool e)
        {
            foreach (Control c in Controls)
            {
                Button b = c as Button;
                if (b != null)
                {
                    b.Enabled = e;
                }
            }
        }

        private T LockAndGet<T>(T t)
        {
            lock (t)
                return t;
        }

        string[] txts = new string[]
        {
            "easter egg buldun, tebrikler."
        };
        Random random = new Random();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(txts[random.Next(0, txts.Length)], "hop", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
