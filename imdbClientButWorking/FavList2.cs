using System;
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
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            favoriteList = await databaseController.GetFavListAsync(UserId);
            dataGridView2.DataSource = await databaseController.GetFavListAsync(UserId);
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[dataGridView2.ColumnCount - 1].Visible = false;

        }

        #region SearchButtonFunctions
        private async void button1_Click(object sender, EventArgs e)
        {
            //search by title
            dataGridView1.DataSource = null;
            resultList.Clear();
            SearchData search = await databaseController.SearchImdbTask(textBox1.Text);
            if (search.Results != null)
            {
                bool morethanfive = search.Results.Count >= 5;
                for (int i = 0; i < Convert.ToInt32(morethanfive) * 5 + Convert.ToInt32(!morethanfive) * search.Results.Count; i++)
                {
                    resultList.Add(await databaseController.GetMovieFromImdbTask(search.Results[i].Id));
                }
                dataGridView1.DataSource = resultList;
            }
            else
            {
                MessageBox.Show(search.ErrorMessage);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //search by year
            int year = Convert.ToInt32(numericUpDown1.Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //search by actor
            string input = textBox2.Text;
        }
        #endregion

        #region FavoriteManagement
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
        #endregion

        #region EnterEvents
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.PerformClick();
            }
        }
        #endregion
    }
}
