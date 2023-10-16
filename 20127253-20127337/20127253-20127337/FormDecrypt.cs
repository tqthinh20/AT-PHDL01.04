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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _20127253_20127337
{
    public partial class FormDecrypt : Form
    {
        public FormDecrypt()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();  //Browse file from computer
            ofd.ShowDialog();
            textBox1.Text = ofd.FileName;   //Get file name
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormVerification f = new FormVerification();
            f.ShowDialog();

            if (Globals.correct == 1)
            {
                try
                {
                    if (File.Exists(textBox1.Text))
                    {
                        string dir = Path.GetDirectoryName(textBox1.Text);
                        string outputFile = dir + "\\" + textBox2.Text;

                        byte[] Key = HASH.HashSHA256(Encoding.UTF8.GetBytes(textBox3.Text));  //Generate AES key
                        AES.FileDecrypt(textBox1.Text, outputFile, Key); //Decrypt chosen file with Key

                        MessageBox.Show("Decrypt successfully!");

                        this.Hide();
                        FormMenu form = new FormMenu();
                        form.ShowDialog();
                        this.Close();
                    }

                    else
                    {
                        MessageBox.Show("Invalid input!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMenu form = new FormMenu();
            form.ShowDialog();
            this.Close();
        }
    }
}
