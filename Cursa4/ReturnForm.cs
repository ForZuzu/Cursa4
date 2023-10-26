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
    public partial class ReturnForm : Form
    {
        DataBase dataBase = new DataBase();
        public ReturnForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var id1 = textBox2.Text;
            var kol = textBox1.Text;

            var query = $"EXECUTE P2 '{kol}', '{id1}';";
           
            var command = new SqlCommand(query, dataBase.getConnection());
            dataBase.openConnection();
            command.ExecuteNonQuery();
            MessageBox.Show("Процедура выполнена!");

            dataBase.closeConnection();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox1.Text = "";
        }
    }
}
