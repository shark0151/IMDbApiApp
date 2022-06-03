﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMDbApiLib.Models;

namespace imdbClientButWorking
{
    public partial class FavList2 : Form
    {
        private int UserId;
        DatabaseController DatabaseController;
        List<TitleData> favoriteList = new List<TitleData>();
        public FavList2(DatabaseController controller)
        {
            InitializeComponent();
            //DatabaseController databaseController = new DatabaseController();
            UserId = controller.UserId;
            DatabaseController = controller;
            favoriteList = Task.Run(() => DatabaseController.GetFavListAsync(UserId)).Result;
            dataGridView2.DataSource = favoriteList;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
        }

        override protected void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //search by title
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //search by year
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //search by actor
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //add to favorites
            dataGridView2.Rows.Add(dataGridView1.Rows[e.RowIndex]);
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //remove from favorites
            dataGridView2.Rows.Remove(dataGridView2.Rows[e.RowIndex]);
        }
    }
}
