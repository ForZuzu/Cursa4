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
    public partial class Show7 : Form
    {
        DataBase dataBase = new DataBase();
        public Show7()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID1", "ID Продажи");
            dataGridView1.Columns.Add("ID2", "ID Сделки]");
            dataGridView1.Columns.Add("ID3", "ID Сотрудника");
            dataGridView1.Columns.Add("ID4", "ID Техники]");
            dataGridView1.Columns.Add("datus", "Дата");
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

            string queryString = $"Select * From Продажи;";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void Show7_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
