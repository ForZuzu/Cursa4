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
using System.Diagnostics;

namespace Cursa4
{

    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class Form2 : Form
    {
        DataBase dataBase = new DataBase();

        int selectedRow;
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID1", "ID Партии");
            dataGridView1.Columns.Add("ID2", "ID Поставщика");
            dataGridView1.Columns.Add("kol", "Количество");
            dataGridView1.Columns.Add("ID3", "ID Техники");
            dataGridView1.Columns.Add("datus", "Дата поставки");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(3), record.GetDateTime(4), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Партия;";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
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
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addForms addForm = new addForms();
            addForm.Show();
        }
        private void SearchBox(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"Select * from Партия Where concat ([ID Партии], [ID Поставщика], Количество, [ID Техники], [Дата поставки]) like '%" + textBox1.Text + "%'";

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

        private void updateRows()
        {

            dataBase.openConnection();
            for (int ind = 0; ind < dataGridView1.Rows.Count; ind++)
            {
                Debug.WriteLine(ind);
                var rowState = (RowState)dataGridView1.Rows[ind].Cells[5].Value;

                if (rowState == RowState.Existed)
                {
                    MessageBox.Show("Ничего не происходит");
                    continue;
                }

                if (rowState == RowState.Deleted)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id = Convert.ToInt32(dataGridView1.Rows[ind].Cells[0].Value);
                    var deleteQuery = $"Delete from Партия Where [ID Партии] = '{id}';";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id1 = dataGridView1.Rows[ind].Cells[0].Value.ToString();
                    var id2 = dataGridView1.Rows[ind].Cells[1].Value.ToString();
                    var kol = dataGridView1.Rows[ind].Cells[2].Value.ToString();
                    var id3 = dataGridView1.Rows[ind].Cells[3].Value.ToString();
                    var datus = dataGridView1.Rows[ind].Cells[4].Value.ToString();

                    var changeQuery = $"Update Партия Set [ID Партии] = '{id1}', [ID Поставщика] = '{id2}', Количество = '{kol}', [ID Техники] = '{id3}', [Дата поставки] = '{datus}' Where [ID Партии] = '{id1}'";
                    
                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closeConnection();
        }


        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            updateRows();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataBase.closeConnection();
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeRow();
            ClearFilds();
        }

        private void ChangeRow()
        {
            var SelectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id1 = textBox2.Text;
            var id2 = textBox3.Text;
            var kol = textBox5.Text;
            var id3 = textBox4.Text;
            var datus = textBox6.Text;

            if (dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                dataGridView1.Rows[SelectedRowIndex].SetValues(id1, id2, kol, id3, datus);
                dataGridView1.Rows[SelectedRowIndex].Cells[5].Value = RowState.Modified;
            }
        }

        private void ClearFilds()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClearFilds();
        }
    }
}
