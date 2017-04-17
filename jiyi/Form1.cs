using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jiyi
{
    public partial class Form1 : Form
    {
        Chuli cl=new Chuli();
        int n=-1;
        public Form1()
        {
            InitializeComponent();
            show();
        }
        void show()
        {
            n = cl.next();
            if (n == -1) {
                //MessageBox.Show("该学习新的了");
                textBox1.Text = "";
                textBox2.Text = "";
                button3.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
            else {
                textBox1.Text = cl.mem[n, 0];
                textBox2.Text = "";
                button3.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }
            zcsn.Text = cl.tn.ToString();
            xyfxn.Text = cl.fxcs.ToString();
            button1.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            cl.no(n);
            n = cl.next();
            show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cl.add(textBox1.Text, textBox2.Text);
            show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cl.yes(n);
            n = cl.next();
            show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = cl.mem[n, 1];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cl.alter(textBox1.Text, textBox2.Text,n);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
