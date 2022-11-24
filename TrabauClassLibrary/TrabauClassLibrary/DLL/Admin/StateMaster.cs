using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabauClassLibrary.DLL.Admin
{
    public class StateMaster : dbCon
    {
        public DataSet GetStates(Int64 UserId, string text, int PageNumber, int PageSize, string sortColumn, string sortDirection)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetAllStates");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("text", text));
                dbc.Parameters.Add(new SqlParameter("PageNumber", PageNumber));
                dbc.Parameters.Add(new SqlParameter("PageSize", PageSize));
                dbc.Parameters.Add(new SqlParameter("sortColumn", sortColumn));
                dbc.Parameters.Add(new SqlParameter("sortDirection", sortDirection));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetStateDetails(Int64 UserId, int StateId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetStateDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("StateId", StateId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet SaveStateDetails(Int64 UserId, string StateName, int StateID, int CountryID, bool IsActive)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_SaveStateDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("StateName", StateName));
                dbc.Parameters.Add(new SqlParameter("StateID", StateID));
                dbc.Parameters.Add(new SqlParameter("CountryID", CountryID));
                dbc.Parameters.Add(new SqlParameter("IsActive", IsActive));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }
    }
}
