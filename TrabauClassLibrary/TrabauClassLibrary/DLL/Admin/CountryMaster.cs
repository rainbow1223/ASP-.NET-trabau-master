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
    public class CountryMaster : dbCon
    {
        public DataSet GetCountries(Int64 UserId, string text, int PageNumber, int PageSize, string sortColumn, string sortDirection)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetAllCountries");
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

        public DataSet GetCountryDetails(Int64 UserId, int CountryId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetCountryDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("CountryId", CountryId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet SaveCountryDetails(Int64 UserId, string CountryName, string CountryCode, string CountryPrefix, string TimeZone,string TimeZoneDetails, int CountryID, bool IsActive)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_SaveCountryDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("CountryName", CountryName));
                dbc.Parameters.Add(new SqlParameter("CountryCode", CountryCode));
                dbc.Parameters.Add(new SqlParameter("CountryPrefix", CountryPrefix));
                dbc.Parameters.Add(new SqlParameter("TimeZone", TimeZone));
                dbc.Parameters.Add(new SqlParameter("TimeZoneDetails", TimeZoneDetails));
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
