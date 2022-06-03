using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using imdbClientButWorking.Models;

namespace imdbClientButWorking
{
    public partial class FavList : Form
    {
        DatabaseController databaseController;
        public FavList()
        {
            InitializeComponent();
            databaseController = new DatabaseController();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            User user = new User();
            user.username = textBox1.Text;
            user.password = textBox2.Text;
            if (databaseController.CheckUserCredentials(user).Result)//login check
            {
                FavList2 favList2 = new FavList2(databaseController.UserId);
                this.Hide();
                favList2.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Incorrect credentials.");
                this.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!databaseController.CheckUserExists(textBox1.Text).Result)//check user existence, register and login
            {
                if(textBox2.Text.Length < 4)
                { 
                    MessageBox.Show("Password needs to be at leats 4 characters");
                }
                else
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
