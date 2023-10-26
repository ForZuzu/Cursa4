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
    public partial class addForms : Form
    {
        DataBase dataBase = new DataBase();
        public addForms()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var id1 = textBox2.Text;
            var id2 = textBox3.Text;
            var kol = textBox5.Text;
            var id3 = textBox4.Text;
            var datus = textBox6.Text;

            var addQuery = $"Insert into Партия ([ID Партии], [ID Поставщика], Количество, [ID Техники], [Дата поставки]) values ('{id1}', '{id2}', '{kol}', '{id3}', '{datus}');";

            var command = new SqlCommand(addQuery, dataBase.getConnection());
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Запись успешно создана!");   
            }
            catch
            {
                var command2 = $"Insert into Поставщики([ID Поставщика], Название) values('222', 'NEW');";

                MessageBox.Show("Ошибка! Возможно вы ввели несуществующий ID Поставщика");
            }

            

            dataBase.closeConnection(); 
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }
    }
}
