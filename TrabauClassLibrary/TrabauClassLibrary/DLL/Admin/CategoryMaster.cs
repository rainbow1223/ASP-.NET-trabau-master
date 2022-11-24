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
    public class CategoryMaster : dbCon
    {
        public DataSet GetCategories(Int64 UserId, string text, int PageNumber, int PageSize, string sortColumn, string sortDirection)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetAllCategories");
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


        public DataSet GetServiceCategoryDetails(Int64 UserId, int ServiceCategoryId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetServiceCategoryDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ServiceCategoryId", ServiceCategoryId));
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
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetCategoryTypes");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet SaveServiceCategory(Int64 UserId, string ServiceCategoryName, int ServiceCategoryId, int CategoryTypeId, bool IsActive)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_SaveServiceCategory");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ServiceCategoryName", ServiceCategoryName));
                dbc.Parameters.Add(new SqlParameter("ServiceCategoryId", ServiceCategoryId));
                dbc.Parameters.Add(new SqlParameter("CategoryTypeId", CategoryTypeId));
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
