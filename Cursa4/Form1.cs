using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Cursa4
{
    public partial class Form1 : Form
    {
        DataBase dataBase = new DataBase();
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            DataTable table2 = new DataTable();

            string queryString = $"Select [ID Сотрудника], Должность From Сотрудники Where [ID Права] IN (Select [ID Права] From Права Where Пароль = '{passUser}') And [ID Сотрудника] = '{loginUser}' And Должность = 'Приёмщик';";
            string queryString1 = $"Select [ID Сотрудника], Должность From Сотрудники Where [ID Права] IN (Select [ID Права] From Права Where Пароль = '{passUser}') And [ID Сотрудника] = '{loginUser}' And Должность = 'Продавец';";
            string queryString2 = $"Select [ID Сотрудника], Должность From Сотрудники Where [ID Права] IN (Select [ID Права] From Права Where Пароль = '{passUser}') And [ID Сотрудника] = '{loginUser}' And Должность = 'Администатор';";


            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());
            SqlCommand sqlCommand1 = new SqlCommand(queryString1, dataBase.getConnection());
            SqlCommand sqlCommand2= new SqlCommand(queryString2, dataBase.getConnection());


            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);

            adapter.SelectCommand = sqlCommand1;
            adapter.Fill(table1);

            adapter.SelectCommand = sqlCommand2;
            adapter.Fill(table2);

            if (table.Rows.Count == 1 || table1.Rows.Count == 1 || table2.Rows.Count == 1)
            {
                if (table.Rows.Count == 1)
                {
                    Form2 form2 = new Form2();
                    MessageBox.Show("Успешный вход Приёмщик");
                    this.Hide();
                    form2.ShowDialog();
                }
                if (table1.Rows.Count == 1)
                {
                    Form3 form3 = new Form3();
                    MessageBox.Show("Успешный вход Продавца");
                    this.Hide();
                    form3.ShowDialog();
                }
                if (table2.Rows.Count == 1)
                {
                    Form4 form4 = new Form4();
                    MessageBox.Show("Успешный вход Администратора");
                    this.Hide();
                    form4.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataBase.closeConnection();
            Application.Exit();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
        }
    }
}
