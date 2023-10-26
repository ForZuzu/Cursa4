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
    public partial class addForm1 : Form
    {
        DataBase dataBase = new DataBase();
        public addForm1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var id1 = textBox2.Text;
            var f = textBox3.Text;
            var i = textBox5.Text;
            var o = textBox4.Text;
            var dolj = textBox6.Text;
            var id2 = textBox10.Text;
            var prava = textBox9.Text;
            var pass = textBox8.Text;

            var addQuery1 = $"Insert into Сотрудники ([ID Сотрудника], Фамилия, Имя, Отчество, Должность, [ID Права]) values ('{id1}', '{f}', '{i}', '{o}', '{dolj}', '{id2}');";
            var addQuery2 = $"Insert into Права ([ID Права], Права, Пароль) values ('{id2}', '{prava}', '{pass}');";


            var command = new SqlCommand(addQuery1, dataBase.getConnection());
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Запись успешно создана!");
            }
            catch
            {
                MessageBox.Show("Ошибка! Возможно вы ввели несуществующий ID Поставщика");
            }
            dataBase.closeConnection();
        }
    }
}
