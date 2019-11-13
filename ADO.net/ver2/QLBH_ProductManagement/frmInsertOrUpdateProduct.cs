using QLBH_ProductManagement.DAO;
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
        ProductDAO productDAO = new ProductDAO();
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

            string sql = "";
            int rs = 0;
            if (_productId > 0)//Update
            {
                //Cau truy van
                product.Id = _productId;
                rs = productDAO.Update(product);
            }
            else//Insert
            {
                //Cau truy van
                rs = productDAO.Insert(product);
            }   

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
