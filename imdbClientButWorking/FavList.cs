using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imdbClientButWorking
{
    public partial class FavList : Form
    {
        public FavList()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (true)//login check
            {
                FavList2 favList2 = new FavList2();
                favList2.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Incorrect credentials.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(true)//check user existence, register and login
            {
                button1.PerformClick();
            }
            else
            {
                MessageBox.Show("Account already exists.");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
