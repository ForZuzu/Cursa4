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
    public partial class Show2 : Form
    {
        DataBase dataBase = new DataBase();
        public Show2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID1", "ID Производителя");
            dataGridView1.Columns.Add("ID2", "Название  Производителя");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), RowState.ModifiedNew);
        }


        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Производитель;";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void Show2_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
    }
}
