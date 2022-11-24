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
    public class utility : dbCon
    {
        public DataSet GetCategories()
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Hire_GetCategories");
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetHighestRated_Freelancers(Int64 UserId, int CategoryId, int SkillId, int CountryId, string HourlyRate_Order, string JobSuccess)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Utility_GetHighestRated_Freelancers");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("CategoryId", CategoryId));
                dbc.Parameters.Add(new SqlParameter("SkillId", SkillId));
                dbc.Parameters.Add(new SqlParameter("CountryId", CountryId));
                dbc.Parameters.Add(new SqlParameter("HourlyRate_Order", HourlyRate_Order));
                dbc.Parameters.Add(new SqlParameter("JobSuccess", JobSuccess));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetTopTrending_Freelancers(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Utility_GetTopTrending_Freelancers");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetHourlyRateFilter()
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Utility_GetHourlyRateFilter");
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetJobSuccessFilter()
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Utility_GetJobSuccessFilter");
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet AddToPreferList(Int64 UserId, Int64 Preferred_UserId, string Type, string IPAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Users_AddToPreferList");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Preferred_UserId", Preferred_UserId));
                dbc.Parameters.Add(new SqlParameter("Type", Type));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public void CreateEmailerLog(Int64 UserId, string EmailId, string TemplateURL, string Response, string IPAddress)
        {
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabauCreateEmailerLog");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("EmailId", EmailId));
                dbc.Parameters.Add(new SqlParameter("TemplateURL", TemplateURL));
                dbc.Parameters.Add(new SqlParameter("Response", Response));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                db.ExecuteNonQuery(dbc);
            }
            catch (Exception ex)
            {
            }

        }
    }
}
