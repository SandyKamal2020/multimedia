﻿using System;
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
    public partial class Huffman : Form
    {
        public Huffman(string input)
        {
            InitializeComponent();
            textBox1.Text = input;
        }
        private SolidBrush blackBrush;

        int totalNodes = 0, maxTreeHeight = 0;
        nodes the_tree = null;
        private Pen blackPen;

        Dictionary<char, int> repetation_chars = new Dictionary<char, int>();

        List<List<string>> Encode_Bits = new List<List<string>>();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text.Length.ToString();

        }
        private void huffman_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            panel1.Paint += new PaintEventHandler(panel1_Paint);
            panel1.SizeChanged += new EventHandler(panel1_SizeChanged);
            panel1.Font = new Font("SansSerif", 20.0f, FontStyle.Bold);
            blackPen = new Pen(Color.Black);
            blackBrush = new SolidBrush(Color.Black);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            repetation_chars.Clear();
            if (textBox1.Text == "")
            {
                return;
            }
            String code = textBox1.Text;
            for (int i = 0; i < code.Length; i++)
            {
                if (repetation_chars.ContainsKey(code[i]))
                {
                    repetation_chars[code[i]]++;
                }
                else repetation_chars[code[i]] = 1;
            }
            List<Tuple<int, char>> rep = new List<Tuple<int, char>>();

            var arrayOfAllKeys = repetation_chars.Keys.ToArray();
            for (int i = 0; i < arrayOfAllKeys.Length; i++)
            {
                Tuple<int, char> ttt = Tuple.Create(repetation_chars[arrayOfAllKeys[i]], arrayOfAllKeys[i]);
                rep.Add(ttt);
            }
            rep.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            List<nodes> rep_node = new List<nodes>();
            for (int i = 0; i < rep.Count; i++)
            {
                nodes temp = new nodes();
                temp.prob = rep[i].Item1;
                temp.sympol = rep[i].Item2.ToString();
                rep_node.Add(temp);
            }
            nodes c = new nodes();
            the_tree = generate_Tree_for_huffman(rep_node);

            Encode_Bits.Clear();

            List<String> temp1 = new List<string>();
            temp1.Add("Sympol");
            temp1.Add("bits");
            temp1.Add("counter");
            Encode_Bits.Add(temp1);

            calculate_bits(the_tree, "");
            int depth = 1;
            totalNodes = 0;
            InorderTraversal(the_tree, depth);
            maxTreeHeight = TreeHeight(the_tree);
            panel1.Invalidate();
            panel1.Invalidate();
        }
       

    
           
       
        private int TreeHeight(nodes t)
        {
            if (t == null) return -1;
            else return 1 + Math.Max(TreeHeight(t.Left), TreeHeight(t.Right));
        }

        private void InorderTraversal(nodes t, int depth)
        {
            if (t != null)
            {
                InorderTraversal(t.Left, depth + 1); //add 1 to depth (y coordinate) 
                t.Xpos = totalNodes++ + 1; //x coord is node number in inorder traversal
                t.Ypos = depth - 1; // mark y coord as depth
                InorderTraversal(t.Right, depth + 1);
            }
        }

        nodes generate_Tree_for_huffman(List<nodes> list)
        {
            if (list.Count <= 1)
            {
                list[0].sympol = list[0].prob.ToString();
                return list[0];
            }
            if (list.Count == 2)
            {
                nodes node_left = new nodes();
                nodes node_right = new nodes();

                node_left = list[0];
                node_right = list[1];

                nodes final = new nodes();
                final.Left = node_left;
                final.Right = node_right;
                final.prob = node_right.prob + node_left.prob;
                final.sympol = final.prob.ToString();
                return final;
            }

            List<nodes> temp_lest = new List<nodes>();
            temp_lest = list;

            nodes node_left1 = new nodes();
            nodes node_right1 = new nodes();

            node_left1 = list[0];

            node_right1 = list[1];

            nodes left_or_right = new nodes();
            left_or_right.Left = node_left1;
            left_or_right.Right = node_right1;
            left_or_right.prob = node_right1.prob + node_left1.prob;
            left_or_right.sympol = left_or_right.prob.ToString();

            temp_lest.RemoveAt(0);
            temp_lest.RemoveAt(0);



            temp_lest.Add(left_or_right);
            //temp_lest.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            temp_lest.Sort((x, y) => x.prob.CompareTo(y.prob));


            nodes last_final = new nodes();
            last_final = generate_Tree_for_huffman(temp_lest);

            return last_final;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (the_tree != null)
                DrawTree(the_tree, e.Graphics);
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {

        }

        public void DrawTree(nodes root, Graphics g)
        {
            try
            {
                panel1.Width = ClientSize.Width - 20;
                panel1.Height = ClientSize.Height - 20;

                int Width = panel1.Width;
                int Height = panel1.Height;
                int dx = 0, dy = 0, dx2 = 0, dy2 = 0, ys = 20;
                int XSCALE, YSCALE;
                int treeHeight = TreeHeight(root);

                XSCALE = (int)(Width / totalNodes); //scale x by total nodes in tree
                YSCALE = (int)((Height - ys) / (maxTreeHeight + 1)); //scale y by tree height

                if (root != null)
                {
                    // inorder traversal to draw each node
                    DrawTree(root.Left, g); // do left side of inorder traversal 
                    dx = root.Xpos * XSCALE; // get x,y coords., and scale them 
                    dy = root.Ypos * YSCALE;
                    string s = root.sympol.ToString(); //get the word at this node
                    s += "(" + root.prob.ToString() + ")";
                    g.DrawString(s, panel1.Font, blackBrush, new PointF(dx - ys, dy));
                    // this draws the lines from a node to its children, if any
                    if (root.Left != null)
                    {
                        //draws the line to left child if it exists
                        dx2 = root.Left.Xpos * XSCALE;
                        dy2 = root.Left.Ypos * YSCALE;
                        g.DrawLine(blackPen, dx, dy, dx2, dy2);
                    }

                    if (root.Right != null)
                    {
                        //draws the line to right child if it exists
                        dx2 = root.Right.Xpos * XSCALE;//get right child x,y scaled position
                        dy2 = root.Right.Ypos * YSCALE;
                        g.DrawLine(blackPen, dx, dy, dx2, dy2);
                    }

                    DrawTree(root.Right, g); //now do right side of inorder traversal 
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
        }

        public string calculate_bits(nodes root, string code)
        {
            if (root.Left == null && root.Right == null)
            {
                List<String> temp = new List<string>();
                temp.Add(root.sympol);
                temp.Add(code);
                temp.Add(root.prob.ToString());
                Encode_Bits.Add(temp);
                return "";

            }
            calculate_bits(root.Left, code + "0");
            calculate_bits(root.Right, code + "1");

            return "";
        }
      
       // public string calculate_bits(nodes root, string code)
        //{
          //  if (root.Left == null && root.Right == null)
           // {
           //     List<String> temp = new List<string>();
           //     temp.Add(root.sympol);
               // temp.Add(code);
             //   temp.Add(root.prob.ToString());
             //   Encode_Bits.Add(temp);
             //   return "";
         //   }

          //  calculate_bits(root.Left, code + "0");
         //   calculate_bits(root.Right, code + "1");


          //  return "";
       // }

      
        private void button2_Click(object sender, EventArgs e)
        {
            Data_Show data = new Data_Show(Encode_Bits);
            data.Show();
 
        }

        private void Huffman_Load_1(object sender, EventArgs e)
        {

        }

    }
}
