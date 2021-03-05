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
    public partial class Form2 : Form
    {
        public Form2(string input)
        {
            InitializeComponent();
            textBox2.Text = input;
            Form2_inc();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text.Length.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label2.Text = textBox2.Text.Length.ToString();
        }
        public void Form2_inc()
        {
         try
            {
                if (textBox2.Text == "")
                {
                    return;
                }
                String Solution = "";
                String code = textBox2.Text;
                char sesc = code[0];
                int count = 1;
                for (int i = 1; i < code.Length; i++)
                {
                    if (code[i] == sesc)
                    {
                        count++;
                        if (count > 9)
                        {
                            count = 9;
                        }
                    }
                    else
                    {
                        Solution += count.ToString() + sesc;
                        sesc = code[i];
                        count = 1;
                    }
                }
                Solution += count.ToString() + sesc;
                textBox2.Text = Solution;
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
    }
}
