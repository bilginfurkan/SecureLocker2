using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureLocker2
{
    static class Program
    {
        public static string key;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Database.Load();

            if (args.Length > 0)
            {
                var waitForProcessToShutdown = true;
                var file = args[0];
                var mutex = new Mutex(true, Hash.Md5(file) + "_SL2", out var isMine);
                
                if (!isMine)
                {
                    MessageBox.Show("Zaten bu dosya için bir securelocker açıkmış, nabıcaz? Neyse ben sana temp'i açayım sen oradan dosyanı bulursun.", "vups", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Process.Start("explorer.exe", "\"" + Path.GetTempPath() + "\"");
                    Environment.Exit(0);

                    return;
                }
                
                if (!file.EndsWith(Cipher.ADDED_EXTENSION))
                {
                    MessageBox.Show("Hata", "Amacın ne? Sonu " + Cipher.ADDED_EXTENSION + " ile bitmiyor ki bunun. Nereden bileyim ben bunun şifreli olup olmadığını?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MainForm mainForm = null;
                var newFilePath = $"{Path.GetTempPath()}/sl_{(uint)file.GetHashCode()}_{Path.GetFileName(file)}";
                File.Copy(file, newFilePath, true);

                var unansweredMessageBox = false;
                async Task OnFileChange()
                {
                    unansweredMessageBox = true;
                    var popup = MessageBox.Show("Değişiklikleri kaydedeyim mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (popup == DialogResult.Yes)
                    {
                        var cloned = Path.GetTempPath() + DateTime.Now.GetHashCode(); // Define a random clone path for file
                        File.Copy(newFilePath, cloned, true); // Do it

                        await mainForm.Crypt(true, new string[] { cloned }); // encrypt it

                        File.Copy(cloned + Cipher.ADDED_EXTENSION, file, true); // since the clone now has encryption extension, add it to final mix and copy that into original file
                        File.Delete(cloned + Cipher.ADDED_EXTENSION); // delete other file
                    }
                    unansweredMessageBox = false;
                }

                var loginForm = new LoginForm(async (main) =>
                {
                    mainForm = main;
                    try
                    {
                        await main.Crypt(false, new string[] { newFilePath });
                        newFilePath = newFilePath.Substring(0, newFilePath.Length - Cipher.ADDED_EXTENSION.Length);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    var fileWatcher = new FileWatcher(newFilePath);
                    fileWatcher.Start();

                    fileWatcher.OnChange += (s, e) =>
                    {
                        OnFileChange();
                    };

                    Process process = null;
                    /*if (newFilePath.EndsWith(".txt"))
                    {
                        process = Process.Start("notepad++.exe", "\"" + newFilePath + "\"");
                        waitForProcessToShutdown = false;
                    }
                    else*/
                    process = Process.Start(newFilePath);

                    if (waitForProcessToShutdown)
                    {
                        await process.WaitForExitAsync();

                        if (!unansweredMessageBox && !fileWatcher.Check())
                            await OnFileChange();

                        File.Delete(newFilePath);

                        fileWatcher.Stop();
                        Application.Exit();
                    }
                    else
                    {
                        await Task.Delay(-1);
                    }
                });

                Application.Run(loginForm);
                
                return;
            }

            Form form;
            if (string.IsNullOrEmpty(Database.instance.passwordHash))
            {
                form = new RegisterForm();
            }
            else
            {
                form = new LoginForm();
            }

            Application.Run(form);
        }
    }
}
