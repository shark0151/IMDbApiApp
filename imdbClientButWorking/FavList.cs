﻿using System;
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

        private async void button1_Click(object sender, EventArgs e)
        {
            
            User user = new User
            {
                username = textBox1.Text,
                password = textBox2.Text
            };
            if (await databaseController.CheckUserCredentials(user))//login check
            {
                FavList2 favList2 = new FavList2(databaseController);
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

        private async void button2_Click(object sender, EventArgs e)
        {
            if(!await databaseController.CheckUserExists(textBox1.Text))//check user existence, register and login
            {
                if (textBox2.Text.Length < 4)
                {
                    MessageBox.Show("Password needs to be at leats 4 characters.");
                }
                else
                {
                    User user = new User
                    {
                        username = textBox1.Text,
                        password = textBox2.Text
                    };
                    bool successful = await databaseController.CreateUser(user);
                    if (successful)
                    {
                        button1.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("There was an error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Account already exists.");
            }
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
