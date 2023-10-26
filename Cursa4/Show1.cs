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
    public partial class Show1 : Form
    {
        DataBase dataBase = new DataBase();
        public Show1()
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

        private void Show1_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
    }
}
