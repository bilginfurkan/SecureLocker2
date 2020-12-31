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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database.instance.passwordHash = Hash.Md5(textBox1.Text);
            Database.Save();

            var form = new MainForm();
            form.Show();
            this.Hide();
        }
    }
}
