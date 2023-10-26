using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cursa4
{
    public partial class Form4 : Form
    {
        DataBase dataBase = new DataBase();
        public Form4()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataBase.closeConnection();
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Show1 show1 = new Show1();
            show1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Show2 show2 = new Show2();
            show2.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Show3 show3 = new Show3();
            show3.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Show4 show4 = new Show4();
            show4.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Show5 show5 = new Show5();
            show5.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Show6 show6 = new Show6();
            show6.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Show7 show7 = new Show7();
            show7.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addTech addTech = new addTech();
            addTech.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addStuff addStuff = new addStuff();
            addStuff.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }
    }
}
