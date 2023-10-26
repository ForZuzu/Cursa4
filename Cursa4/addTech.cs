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

namespace Cursa4
{
    public partial class addTech : Form
    {
        DataBase dataBase = new DataBase();
        enum RowState1
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }
        int selectedRow;
        public addTech()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID1", "ID Техники");
            dataGridView1.Columns.Add("ID2", "ID Партии");
            dataGridView1.Columns.Add("ID3", "ID Производителя");
            dataGridView1.Columns.Add("name", "Название");
            dataGridView1.Columns.Add("type", "Тип");
            dataGridView1.Columns.Add("datus", "Дата Выпуска");
            dataGridView1.Columns.Add("grante", "Срок гарантии(мес.)");
            dataGridView1.Columns.Add("price", "Цена");
            dataGridView1.Columns.Add("kol", "Количество");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetString(3), record.GetString(4), record.GetDateTime(5), record.GetInt32(6), record.GetInt32(7), record.GetInt32(8), RowState.ModifiedNew);
        }


        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Техника;";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void addTech_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }


        private void SearchBox(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"Select * from Техника Where concat ([ID Техники], [ID Партии], [ID Производителя], Название, Тип, [Дата Выпуска], [Срок гарантии(мес)], Цена, [На складе(штук)]) like '%" + textBox1.Text + "%'";

            SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void ClearFilds()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox5.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
                textBox10.Text = row.Cells[5].Value.ToString();
                textBox9.Text = row.Cells[6].Value.ToString();
                textBox8.Text = row.Cells[7].Value.ToString();
                textBox7.Text = row.Cells[8].Value.ToString();
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            SearchBox(dataGridView1);
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            ClearFilds();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellForm sellForm = new SellForm();
            sellForm.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            ReturnForm returnForm = new ReturnForm();
            returnForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddTechForm addTechForm = new AddTechForm();
            addTechForm.Show();
        }

        private void updateRows()
        {
            dataBase.openConnection();
            for (int ind = 0; ind < dataGridView1.Rows.Count; ind++)
            {
                Debug.WriteLine(ind);
                var rowState = (RowState1)dataGridView1.Rows[ind].Cells[9].Value;

                if (rowState == RowState1.Existed)
                {
                    MessageBox.Show("Ничего не происходит");
                    continue;
                }

                if (rowState == RowState1.Modified)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id1 = dataGridView1.Rows[ind].Cells[0].Value.ToString();
                    var id2 = dataGridView1.Rows[ind].Cells[1].Value.ToString();
                    var id3 = dataGridView1.Rows[ind].Cells[2].Value.ToString();
                    var name = dataGridView1.Rows[ind].Cells[3].Value.ToString();
                    var type = dataGridView1.Rows[ind].Cells[4].Value.ToString();
                    var datus = dataGridView1.Rows[ind].Cells[5].Value.ToString();
                    var grante = dataGridView1.Rows[ind].Cells[6].Value.ToString();
                    var price = dataGridView1.Rows[ind].Cells[7].Value.ToString();
                    var kol = dataGridView1.Rows[ind].Cells[8].Value.ToString();

                    var changeQuery = $"Update Техника Set [ID Техники] = '{id1}', [ID Партии] = '{id2}', [ID Производителя] = '{id3}', Название = '{name}', Тип = '{type}', [Дата Выпуска] = '{datus}', [Срок гарантии(мес)] = '{grante}', Цена = '{price}', [На складе(штук)] = '{kol}' Where [ID Техники] = '{id1}';";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closeConnection();
        }

        private void ChangeRow()
        {
            var SelectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id1 = textBox2.Text;
            var id2 = textBox3.Text;
            var id3 = textBox5.Text;
            var name = textBox4.Text;
            var type = textBox6.Text;  
            var datus = textBox10.Text; 
            var grante = textBox9.Text; 
            var price = textBox8.Text;
            var kol = textBox7.Text;

            if (dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                dataGridView1.Rows[SelectedRowIndex].SetValues(id1, id2, id3, name, type, datus, grante, price, kol);
                dataGridView1.Rows[SelectedRowIndex].Cells[9].Value = RowState1.Modified;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ChangeRow();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            updateRows();

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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
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
