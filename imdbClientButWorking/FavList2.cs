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
            dataGridView2.DataSource = favoriteList;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
            


        }

        #region SearchButtonFunctions
        private async void button1_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text.Trim();
            string year = checkBox1.Checked? " "+Convert.ToInt32(numericUpDown1.Value).ToString() : "";
            string actor = " " + textBox2.Text.Trim();
            string searchExpression = (title + year + actor).Trim();
            //search by title
            dataGridView1.DataSource = null;
            resultList.Clear();
            SearchData search = await databaseController.SearchImdbTask(searchExpression);
            if (search.Results != null)
            {
                bool morethanfive = search.Results.Count >= 2;
                for (int i = 0; i < Convert.ToInt32(morethanfive) * 2 + Convert.ToInt32(!morethanfive) * search.Results.Count; i ++)
                {
                    resultList.Add(await databaseController.GetMovieFromImdbTask(search.Results[i].Id));
                }
                dataGridView1.DataSource = resultList;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[dataGridView2.ColumnCount - 1].Visible = false;
            }
            else
            {
                MessageBox.Show(search.ErrorMessage);
            }
        }
        
        #endregion

        #region FavoriteManagement
        private async void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //add to favorites
            TitleData x =dataGridView1.Rows[e.RowIndex].DataBoundItem as TitleData;
            bool success =await databaseController.AddToFavListAsync(x.Id);
            if (!success)
            {
                MessageBox.Show("Failed");
            }
        }

        private async void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //remove from favorites
            TitleData x = dataGridView2.Rows[e.RowIndex].DataBoundItem as TitleData;
            bool success = await databaseController.DeleteFromFavListAsync(x.Id);
            if (!success)
            {
                MessageBox.Show("Failed");
            }
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
        #endregion
    }
}
