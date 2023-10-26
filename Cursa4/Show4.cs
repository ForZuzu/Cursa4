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
    public partial class Show4 : Form
    {
        DataBase dataBase = new DataBase();
        public Show4()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID1", "ID Сотрудника");
            dataGridView1.Columns.Add("name1", "Фамилия");
            dataGridView1.Columns.Add("name2", "Имя");
            dataGridView1.Columns.Add("name3", "Отчество");
            dataGridView1.Columns.Add("job", "Должность");
            dataGridView1.Columns.Add("ID2", "ID Права");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetInt32(5), RowState.ModifiedNew);
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

        private void Show4_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
    }
}
