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
    public partial class frmInsertOrUpdateProduct : Form
    {
        int _productId = 0;

        public delegate void InsertDone();
        public event InsertDone WhenInsertDone = null;
        public frmInsertOrUpdateProduct()
        {
            
            InitializeComponent();
            this.Text = "Insert New";
            btnProcess.Text = "Save";
        }

        public frmInsertOrUpdateProduct(Product product)
        {
            InitializeComponent();

            this.Text = "Update";
            btnProcess.Text = "Update";

            _productId = product.Id;

            txtCode.Text = product.Code;
            txtName.Text = product.Name;
            txtDescription.Text = product.Description;
            nmrQuantity.Value = product.Quantity;
            nmrPrice.Value = (decimal)product.Price;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void frmInsertOrUpdateProduct_Load(object sender, EventArgs e)
        {
            DataTable dt = ProcessData.GetData("Select * from Supplier");
            cboSupplier.DataSource = dt;
            cboSupplier.DisplayMember = "Name";
            cboSupplier.ValueMember = "Id";
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Product product = GetProductInformation();

            string connStr = "Data Source=localhost,11434;Initial Catalog=QLBH;Integrated Security=True";
            //Tao ket noi
            SqlConnection sqlConnection = new SqlConnection(connStr);

            string sql = "";
            if (_productId > 0)//Update
            {
                //Cau truy van
                sql =
     string.Format("Update Product Set Code='{0}', Name='{1}', Description='{2}', Quantity={3}, Price = {4}, SupplierId={5} Where Id = {6}"
     , product.Code, product.Name, product.Description, product.Quantity, product.Price, product.SupplierId, _productId);
            }
            else//Insert
            {
                //Cau truy van
                sql =
     string.Format("Insert Into Product(Code, Name, Description, Quantity, Price, SupplierId) Values('{0}', '{1}', '{2}', {3}, {4}, {5})"  
     , product.Code, product.Name, product.Description, product.Quantity, product.Price, product.SupplierId);
            }
           
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);

            sqlConnection.Open();
            int rs = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            if (rs > 0)
            {
                //Thong bao cho form kia biet
                if(WhenInsertDone!=null)
                    WhenInsertDone();

                MessageBox.Show("Update Successfully!!!!!!");
            }
        }

        private Product GetProductInformation()
        {
            var product = new Product();

            product.Code = txtCode.Text;
            product.Name = txtName.Text;
            product.Description = txtDescription.Text;
            product.Quantity = (int)nmrQuantity.Value;
            product.Price = (float)nmrPrice.Value;
            product.SupplierId = int.Parse(cboSupplier.SelectedValue.ToString());

            return product;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
