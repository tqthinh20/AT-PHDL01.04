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
using System.Windows.Forms.VisualStyles;

namespace _20127253_20127337
{
    public partial class FormEncrypt : Form
    {
        public FormEncrypt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();  //Browse file from computer
            ofd.ShowDialog();
            textBox1.Text = ofd.FileName;   //Get file name
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text) && textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                FormVerification f = new FormVerification();
                f.ShowDialog();

                if (Globals.correct == 1)
                {
                    string dir = Path.GetDirectoryName(textBox1.Text);
                    string outputFile = dir + "\\" + textBox2.Text;

                    byte[] Hkey = HASH.HashSHA256(Encoding.UTF8.GetBytes(textBox3.Text));  //Generate AES key
                    AES.FileEncrypt(textBox1.Text, outputFile, Hkey);  //Encrypt chosen file with Hkey

                    if (Globals.encSuccess == 1)
                    {
                        MessageBox.Show("Encryption completed!");

                        this.Hide();
                        FormMenu form = new FormMenu();
                        form.ShowDialog();
                        this.Close();
                    }   
                }
            }
            else
            {
                MessageBox.Show("Invalid input!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMenu form = new FormMenu();
            form.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Show")
            {
                button2.Text = "Hide";
                textBox3.UseSystemPasswordChar = false;
            }
            else if (button2.Text == "Hide")
            {
                button2.Text = "Show";
                textBox3.UseSystemPasswordChar = true;
            }
        }
    }
}
