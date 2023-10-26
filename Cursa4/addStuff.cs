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
    public partial class addStuff : Form
    {
        DataBase dataBase = new DataBase();
        DataBase dataBase2 = new DataBase();
        enum RowState1
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }
        enum RowState2
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }
        int selectedRow;
        int selectedRow1;
        public addStuff()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID1", "ID Сотрудника");
            dataGridView1.Columns.Add("F", "Фамилия");
            dataGridView1.Columns.Add("I", "Имя");
            dataGridView1.Columns.Add("O", "Отчество");
            dataGridView1.Columns.Add("dolj", "Должность");
            dataGridView1.Columns.Add("prava", "Права");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetInt32(5), RowState1.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Сотрудники;";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void CreateColumns2()
        {
            dataGridView2.Columns.Add("ID1", "ID права");
            dataGridView2.Columns.Add("prava", "Права");
            dataGridView2.Columns.Add("pass", "Пароль");
            dataGridView2.Columns.Add("IsNew", String.Empty);
            dataGridView2.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow2(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), RowState2.ModifiedNew);
        }

        private void RefreshDataGrid2(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Права;";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow2(dgw, reader);
            }

            reader.Close();
        }

        private void addStuff_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
            CreateColumns2();
            RefreshDataGrid2(dataGridView2);
        }

        private void SearchBox(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"Select * from Сотрудники Where concat ([ID Сотрудника], Фамилия, Имя, Отчество, Должность, [ID Права]) like '%" + textBox1.Text + "%'";

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
            textBox17.Text = "";
            textBox16.Text = "";
            textBox15.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchBox(dataGridView1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            RefreshDataGrid2(dataGridView2);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClearFilds();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addForm1 form1 = new addForm1();
            form1.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow1 = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectedRow1];
                textBox17.Text = row.Cells[0].Value.ToString();
                textBox16.Text = row.Cells[1].Value.ToString();
                textBox15.Text = row.Cells[2].Value.ToString();
            }
        }

        private void ChangeRow()
        {
            var SelectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id1 = textBox2.Text;
            var F = textBox3.Text;
            var I = textBox5.Text;
            var O = textBox4.Text;
            var dolj = textBox6.Text;
            var id2 = textBox10.Text;

            if (dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                dataGridView1.Rows[SelectedRowIndex].SetValues(id1, F, I, O, dolj, id2);
                dataGridView1.Rows[SelectedRowIndex].Cells[6].Value = RowState1.Modified;
            }
        }

        private void updateRows()
        {

            dataBase.openConnection();
            for (int ind = 0; ind < dataGridView1.Rows.Count; ind++)
            {
                Debug.WriteLine(ind);
                var rowState = (RowState1)dataGridView1.Rows[ind].Cells[6].Value;

                if (rowState == RowState1.Existed)
                {
                    MessageBox.Show("Ничего не происходит");
                    continue;
                }

                if (rowState == RowState1.Deleted)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id = Convert.ToInt32(dataGridView1.Rows[ind].Cells[0].Value);
                    var deleteQuery = $"Delete from Сотрудники Where [ID Сотрудника] = '{id}';";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState1.Modified)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id1 = dataGridView1.Rows[ind].Cells[0].Value.ToString();
                    var F = dataGridView1.Rows[ind].Cells[1].Value.ToString();
                    var I = dataGridView1.Rows[ind].Cells[2].Value.ToString();
                    var O = dataGridView1.Rows[ind].Cells[3].Value.ToString();
                    var prava = dataGridView1.Rows[ind].Cells[4].Value.ToString();
                    var id2 = dataGridView1.Rows[ind].Cells[5].Value.ToString();


                    var changeQuery = $"Update Сотрудники Set [ID Сотрудника] = '{id1}', Фамилия = '{F}', Имя = '{I}', Отчество = '{O}', Должность = '{prava}', [ID Права] = '{id2}' Where [ID Сотрудника] = '{id1}'";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closeConnection();
        }

        private void ChangeRow2()
        {
            var SelectedRowIndex = dataGridView2.CurrentCell.RowIndex;

            var id1 = textBox17.Text;
            var prava = textBox16.Text;
            var pass = textBox15.Text;

            if (dataGridView2.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                dataGridView2.Rows[SelectedRowIndex].SetValues(id1, prava, pass);
                dataGridView2.Rows[SelectedRowIndex].Cells[3].Value = RowState2.Modified;
            }
        }

        private void updateRows2()
        {

            dataBase.openConnection();
            for (int ind = 0; ind < dataGridView2.Rows.Count; ind++)
            {
                Debug.WriteLine(ind);
                var rowState = (RowState2)dataGridView2.Rows[ind].Cells[3].Value;

                if (rowState == RowState2.Existed)
                {
                    MessageBox.Show("Ничего не происходит");
                    continue;
                }

                if (rowState == RowState2.Deleted)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id = Convert.ToInt32(dataGridView2.Rows[ind].Cells[0].Value);
                    var deleteQuery = $"Delete from Права Where [ID Права] = '{id}';";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState2.Modified)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id1 = dataGridView2.Rows[ind].Cells[0].Value.ToString();
                    var prava = dataGridView2.Rows[ind].Cells[1].Value.ToString();
                    var pass = dataGridView2.Rows[ind].Cells[2].Value.ToString();


                    var changeQuery = $"Update Права Set [ID Права] = '{id1}', Права = '{prava}', Пароль = '{pass}' Where [ID Права] = '{id1}'";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closeConnection();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            ChangeRow2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeRow();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            updateRows();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            updateRows2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addForm2cs addForm2Cs = new addForm2cs();
            addForm2Cs.Show();
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

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
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
