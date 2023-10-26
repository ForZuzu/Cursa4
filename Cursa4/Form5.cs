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
    enum RowState3
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Form5 : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRow;
        public Form5()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID1", "ID Поставщика");
            dataGridView1.Columns.Add("ID2", "Название");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), RowState3.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Поставщики;";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }
        private void ClearFilds()
        {
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClearFilds();
        }

        private void SearchBox(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"Select * from Поставщики Where concat ([ID Поставщика], Название) like '%" + textBox1.Text + "%'";

            SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchBox(dataGridView1);
        }

        private void ChangeRow()
        {
            var SelectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id1 = textBox2.Text;
            var id2 = textBox3.Text;

            if (dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                dataGridView1.Rows[SelectedRowIndex].SetValues(id1, id2);
                dataGridView1.Rows[SelectedRowIndex].Cells[2].Value = RowState3.Modified;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeRow();
            ClearFilds();
        }

        private void updateRows()
        {

            dataBase.openConnection();
            for (int ind = 0; ind < dataGridView1.Rows.Count; ind++)
            {
                Debug.WriteLine(ind);
                var rowState = (RowState3)dataGridView1.Rows[ind].Cells[2].Value;

                if (rowState == RowState3.Existed)
                {
                    MessageBox.Show("Ничего не происходит");
                    continue;
                }

                if (rowState == RowState3.Deleted)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id = Convert.ToInt32(dataGridView1.Rows[ind].Cells[0].Value);
                    var deleteQuery = $"Delete from Поставщики Where [ID Поставщика] = '{id}';";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState3.Modified)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id1 = dataGridView1.Rows[ind].Cells[0].Value.ToString();
                    var id2 = dataGridView1.Rows[ind].Cells[1].Value.ToString();

                    var changeQuery = $"Update Поставщики Set [ID Поставщика] = '{id1}', Название = '{id2}' Where [ID Поставщика] = '{id1}'";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closeConnection();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            updateRows();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addCompany addCompany = new addCompany();
            addCompany.Show();
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
    }
}
