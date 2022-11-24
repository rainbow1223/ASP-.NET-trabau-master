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
    public class CityMaster : dbCon
    {
        public DataSet GetCities(Int64 UserId, string text, int PageNumber, int PageSize, string sortColumn, string sortDirection)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetCities");
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

        public DataSet GetCityDetails(Int64 UserId, int CityId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetCityDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("CityId", CityId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet SaveCityDetails(Int64 UserId, string CityName, int CityID, int StateID, bool IsActive)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_SaveCityDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("CityName", CityName));
                dbc.Parameters.Add(new SqlParameter("CityID", CityID));
                dbc.Parameters.Add(new SqlParameter("StateID", StateID));
                dbc.Parameters.Add(new SqlParameter("IsActive", IsActive));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetStates(Int64 UserId, int CountryId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetStates");
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


        public DataSet GetCountries(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Admin_GetCountries");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
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
