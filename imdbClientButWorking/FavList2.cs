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
        DatabaseController databaseController;
        List<TitleData> favoriteList = new List<TitleData>();
        List<TitleData> resultList = new List<TitleData>();
        public FavList2(DatabaseController controller)
        {
            InitializeComponent();
            //DatabaseController databaseController = new DatabaseController();
            UserId = controller.UserId;
            databaseController = controller;
            favoriteList = Task.Run(() => databaseController.GetFavListAsync(UserId)).Result;
            dataGridView2.DataSource = favoriteList;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
        }

        override protected void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


        }
        private async void button1_Click(object sender, EventArgs e)
        {
            //search by title
            dataGridView1.DataSource = null;
            resultList.Clear();
            SearchData search = await databaseController.SearchImdbTask(textBox1.Text);
            if (search != null)
            {
                foreach (SearchResult searchResult in search.Results)
                {
                    resultList.Add(await databaseController.GetMovieFromImdbTask(searchResult.Id));
                }
                dataGridView1.DataSource = resultList;
            }
            else
            {
                MessageBox.Show("Reached limit of request amount (100).");
            }
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
