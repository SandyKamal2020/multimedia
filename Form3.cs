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
    public partial class Form3 : Form
    {
        public Form3(string input)
        {
            InitializeComponent();
            textBox1.Text = input;
            Form3_inc();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text.Length.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label2.Text = textBox2.Text.Length.ToString();
        }
        private void Form3_inc()
        {
            try
            {

                String code = textBox1.Text;
                String Solution = "";
                for (int i = 0; i < code.Length; i += 2)
                {
                    int num = int.Parse(code[i].ToString());
                    char sympol = code[i + 1];
                    for (int j = 0; j < num; j++)
                    {
                        Solution += sympol;
                    }
                }
                textBox2.Text = Solution;
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message.ToString());
            }

        }

        private void rlc_decode_Load(object sender, EventArgs e)
        {

        }
    }
}
