using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DontiaChinaProxy.App_Code
{
    public class LibCon
    {
        SqlConnection dbConnect = new SqlConnection();

        public SqlConnection _DentalConOpen()
        {
            if (dbConnect.State == ConnectionState.Closed)
            {
                dbConnect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConDentaLINKSGDB_Service"].ConnectionString);
            }
            dbConnect.Open();
            return dbConnect;
        }

        public void _DentalConClose()
        {
            if (dbConnect.State == ConnectionState.Open)
            {
                dbConnect.Close();
                dbConnect.Dispose();
            }
        }
    }
}