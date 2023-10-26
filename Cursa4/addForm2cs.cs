using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cursa4
{
    public partial class addForm2cs : Form
    {
        DataBase dataBase = new DataBase();

        public addForm2cs()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var id1 = textBox2.Text;
            var prava = textBox3.Text;
            var pass = textBox5.Text;

            var addQuery = $"Insert into Права ([ID Права], Права, Пароль) values ('{id1}', '{prava}', '{pass}');";

            var command = new SqlCommand(addQuery, dataBase.getConnection());
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Запись успешно создана!");
            }
            catch
            {
                MessageBox.Show("Ошибка! Возможно вы ввели что-то не так");
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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
