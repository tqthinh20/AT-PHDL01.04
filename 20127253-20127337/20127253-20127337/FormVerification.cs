using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20127253_20127337
{
    public partial class FormVerification : Form
    {
        public FormVerification()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DynamicPassword.checkDynamicPassword(DynamicPassword.passwordToCode(textBox2.Text)) == true)
            {
                MessageBox.Show("Correct password!");
                Globals.correct = 1;
            }  
            else
            {
                MessageBox.Show("Invalid password!");
                Globals.correct = 0;
            }

            if (Globals.correct == 1)
                this.Close();
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
