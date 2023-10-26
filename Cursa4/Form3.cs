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

namespace Cursa4
{
    enum RowState1
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Form3 : Form
    {
        DataBase dataBase = new DataBase();

        int selectedRow;
        public Form3()
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

        private void button1_Click(object sender, EventArgs e)
        {

            SellForm sellForm = new SellForm();
            sellForm.Show();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataBase.closeConnection();
            Application.Exit();
        }

        private void Form3_Load(object sender, EventArgs e)
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
                textBox5.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
                textBox10.Text = row.Cells[5].Value.ToString();
                textBox9.Text = row.Cells[6].Value.ToString();
                textBox8.Text = row.Cells[7].Value.ToString();
                textBox7.Text = row.Cells[8].Value.ToString();
            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchBox(dataGridView1);
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
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClearFilds();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReturnForm returnForm = new ReturnForm();
            returnForm.Show();
        }
    }
}
