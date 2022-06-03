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
    public partial class FavList2 : Form
    {
        public FavList2()
        {
            InitializeComponent();
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
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //remove from favorites
        }
    }
}
