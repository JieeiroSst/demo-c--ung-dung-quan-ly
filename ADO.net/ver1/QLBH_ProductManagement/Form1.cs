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

namespace QLBH_ProductManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            RefreshData();

        }

        SqlDataAdapter sqlDataAdapter;
        private void RefreshData()
        {
            string connStr = "Data Source=localhost,11434;Initial Catalog=QLBH;Integrated Security=True";
            //Tao ket noi
            SqlConnection sqlConnection = new SqlConnection(connStr);
            //Cau truy van
            string sql = "Select * from Product";

            //Bo chuyen doi du lieu
            sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
            //Bang chua du lieu
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            grvData.DataSource = dataTable;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = int.Parse(grvData.CurrentRow.Cells["Id"].Value.ToString());

            string connStr = "Data Source=localhost,11434;Initial Catalog=QLBH;Integrated Security=True";
            //Tao ket noi
            SqlConnection sqlConnection = new SqlConnection(connStr);
            //Cau truy van
            string sql = string.Format("Delete from Product Where Id = {0}", id);
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);

            sqlConnection.Open();
            int rs = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            if (rs > 0)
            {
                grvData.Rows.Remove(grvData.CurrentRow);
                MessageBox.Show("Delete Successfully!!!!!!!!");
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product product = new Product()
            {
                Id = int.Parse(grvData.CurrentRow.Cells["Id"].Value.ToString()),
                Quantity = int.Parse(grvData.CurrentRow.Cells["Quantity"].Value.ToString()),
                Price = float.Parse(grvData.CurrentRow.Cells["Price"].Value.ToString()),
                Code = grvData.CurrentRow.Cells["Code"].Value.ToString(),
                Name = grvData.CurrentRow.Cells["Name"].Value.ToString(),
                Description = grvData.CurrentRow.Cells["Description"].Value.ToString(),
                Image = grvData.CurrentRow.Cells["Image"].Value.ToString(),
            };

            frmInsertOrUpdateProduct frm = 
                new frmInsertOrUpdateProduct(product);
            frm.WhenInsertDone += Frm_WhenInsertDone;

            frm.ShowDialog();
        }

        private void Frm_WhenInsertDone()
        {
            RefreshData();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsertOrUpdateProduct frm = new frmInsertOrUpdateProduct();
            frm.WhenInsertDone += Frm_WhenInsertDone1;
            frm.ShowDialog();
        }

        private void Frm_WhenInsertDone1()
        {
            RefreshData();
        }

        private void btnUpdateWithStoreProc_Click(object sender, EventArgs e)
        {
            string connStr = "Data Source=localhost,11434;Initial Catalog=QLBH;Integrated Security=True";
            //Tao ket noi
            SqlConnection sqlConnection = new SqlConnection(connStr);

            string sql = "spProduct_Update";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            //Them tham so
            sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = 4;
            sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = "Update Name";
            sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = "No Content";

            sqlConnection.Open();

            //Trar ve so dong bi anh huong
            var affectedRows = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        private void btnUpdateGrid_Click(object sender, EventArgs e)
        {
            ////C1 => Duyet tung dong trong luoi
            //foreach(var row in grvData.Rows)
            //{
            //    //Lay gia tri tung o
            //    //Goi thuc thi cau sql
            //}

            ////C2 => Lay Datatable tu DataSource
            //DataTable dt = (DataTable)grvData.DataSource;
            //foreach(DataRow dr in dt.Rows)
            //{
            //    int Id = int.Parse(dr["Id"].ToString());
            //    if(dr.RowState == DataRowState.Modified)
            //    {
            //        //Thuc thi cau sql o day
            //    }
            //    else if (dr.RowState == DataRowState.Added)
            //    {
            //        //Thuc thi cau sql o day
            //    }
            //    else if (dr.RowState == DataRowState.Deleted)
            //    {
            //        //Thuc thi cau sql o day
            //    }
            //}

            //C3 => Dung SqlCommandBuilder
            SqlCommandBuilder sqlCommandBuilder
                = new SqlCommandBuilder(sqlDataAdapter);
            DataTable dt1 = (DataTable)grvData.DataSource;
            sqlDataAdapter.Update(dt1);

        }
    }
}
