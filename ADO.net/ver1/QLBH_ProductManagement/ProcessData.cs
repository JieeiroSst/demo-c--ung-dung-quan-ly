﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_ProductManagement
{
    public class ProcessData
    {
        public static DataTable GetData(string sql)
        {
            string connStr = "Data Source=localhost,11434;Initial Catalog=QLBH;Integrated Security=True";
            //Tao ket noi
            SqlConnection sqlConnection = new SqlConnection(connStr);
            
            //Bo chuyen doi du lieu
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
            //Bang chua du lieu
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            return dataTable;
        }
    }
}
