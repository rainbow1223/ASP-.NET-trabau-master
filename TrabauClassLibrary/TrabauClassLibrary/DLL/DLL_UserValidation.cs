using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabauClassLibrary.DLL
{
    public class DLL_UserValidation : dbCon
    {
        public DataSet ValidateUser(string UserName, string Password, string IPAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_ValidateUser");
                dbc.Parameters.Add(new SqlParameter("UserName", UserName));
                dbc.Parameters.Add(new SqlParameter("Password", Password));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet RequestSendEmail(string UserName, string IPAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_RequestSendEmail");
                dbc.Parameters.Add(new SqlParameter("UserName", UserName));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet ResetPassword(Int64 UserId, string Password, string IPAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_ResetPassword");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Password", Password));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet VerifyEmailAddress(int UserId, string IPAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_VerifyEmailAddress");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string CheckEmailAddressVerificationStatus(int UserId)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_CheckEmailAddressVerificationStatus");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "-1";
            }

            return val;
        }
    }
}
