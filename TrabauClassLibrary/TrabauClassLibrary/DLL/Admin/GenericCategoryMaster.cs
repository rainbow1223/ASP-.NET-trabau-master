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
    public class GenericCategoryMaster : dbCon
    {
        public DataSet GetGenericCategories(Int64 UserId, string text, int PageNumber, int PageSize, string sortColumn, string sortDirection)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetAllGenericCategories");
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


        public DataSet GetCategoryDetails(Int64 UserId, int CategoryId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetGenericCategoryDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("CategoryId", CategoryId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetCategoryTypes(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetGenericCategoryTypes");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet SaveCategoryDetails(Int64 UserId, string CategoryName, int CategoryId, string CategoryType, bool IsActive)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_SaveGenericCategoryDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("CategoryName", CategoryName));
                dbc.Parameters.Add(new SqlParameter("CategoryId", CategoryId));
                dbc.Parameters.Add(new SqlParameter("CategoryType", CategoryType));
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
