using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication9
{
    public partial class Data_Show : Form
    {
        List<List<String>> mylist = new List<List<string>>();
        public Data_Show()
        {
            InitializeComponent();
        }

        public Data_Show(List<List<String>> mylist)
        {
            this.mylist = mylist;
            InitializeComponent();
        }

        private void Data_Show_Load(object sender, EventArgs e)
        {
             try
            {


                dataGridView1.RowCount = mylist.Count;
                dataGridView1.ColumnCount = mylist[0].Count;

                for (int i = 0; i < mylist[0].Count; i++)
                {
                    dataGridView1.Columns[i].HeaderText = mylist[0][i];
                }

                for (int i = 0; i < mylist.Count; i++)
                {

                    for (int j = 0; j < mylist[0].Count; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = mylist[i][j];
                    }
                }
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
