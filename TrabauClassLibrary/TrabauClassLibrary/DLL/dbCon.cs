using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace TrabauClassLibrary.DLL
{
    public class dbCon : IDisposable
    {
        protected SqlDatabase db;
        protected SqlConnection con = new SqlConnection();

        protected dbCon()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["TrabauConnectionString"].ConnectionString;
            db = new SqlDatabase(con.ConnectionString);
        }


        private bool isDisposed = false;
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // if (con != null) con.Dispose();
                }
            }
            isDisposed = true;
        }
    }
}
