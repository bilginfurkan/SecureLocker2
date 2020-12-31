using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureLocker2
{
    public partial class LoginForm : Form
    {
        Action<MainForm> after;

        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(Action<MainForm> after)
        {
            this.after = after;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Hash.Md5(textBox1.Text) == Database.instance.passwordHash)
            {
                Program.key = Hash.Sha256(textBox1.Text);

                var form = new MainForm();
                if (after != null)
                {
                    after(form);
                }
                else
                {
                    form.Show();
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı şifre girdin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
