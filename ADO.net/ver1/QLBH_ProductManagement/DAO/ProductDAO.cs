using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_ProductManagement.DAO
{
    public class ProductDAO
    {
        public db db = new db();
        public DataTable GetAllData()
        {
            string sql = "Select * from Product";
            var rs = db.GetData(sql);

            return rs;
        }
    }
}
