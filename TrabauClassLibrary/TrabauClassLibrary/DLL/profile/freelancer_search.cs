using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabauClassLibrary.DLL.profile
{
    public class freelancer_search : dbCon
    {
        public DataSet Search_Freelancers(Int64 UserId, int PageNumber, string SearchText, string Category, string HourlyRate, string JobSuccess,
            string EarnedAmount, string Language, string IPAddress, string ProfileType)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Search_Freelancers");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("PageNumber", PageNumber));
                dbc.Parameters.Add(new SqlParameter("SearchText", SearchText));
                dbc.Parameters.Add(new SqlParameter("Category", Category));
                dbc.Parameters.Add(new SqlParameter("HourlyRate", HourlyRate));
                dbc.Parameters.Add(new SqlParameter("JobSuccess", JobSuccess));
                dbc.Parameters.Add(new SqlParameter("EarnedAmount", EarnedAmount));
                dbc.Parameters.Add(new SqlParameter("Language", Language));
                dbc.Parameters.Add(new SqlParameter("ProfileType", ProfileType));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetFilters(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Search_GetFilters");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet Get_RecentSearchHistory(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabauGet_RecentSearchHistory");
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
