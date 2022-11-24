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
    public class profile_updates : dbCon
    {
        public string UpdateProfileTitle(Int64 UserId, string Title, Int64 UpdateBy)
        {
            string val = "error:Error while updating profile title, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdateProfileTitle");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Title", Title));
                dbc.Parameters.Add(new SqlParameter("UpdateBy", UpdateBy));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating profile title, please refresh and try again";
            }

            return val;
        }

        public string UpdateProfileOverview(Int64 UserId, string Overview, Int64 UpdateBy)
        {
            string val = "error:Error while updating profile overview, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdateProfileOverview");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Overview", Overview));
                dbc.Parameters.Add(new SqlParameter("UpdateBy", UpdateBy));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating profile overview, please refresh and try again";
            }

            return val;
        }


        public string GetSkills(Int64 UserId)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Profile_GetSkills");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "";
            }

            return val;
        }


        public string UpdateProfileSkills(Int64 UserId, string Skills, Int64 UpdateBy)
        {
            string val = "error:Error while updating profile skills, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdateProfileSkills");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Skills", Skills));
                dbc.Parameters.Add(new SqlParameter("UpdateBy", UpdateBy));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating profile skills, please refresh and try again";
            }

            return val;
        }

        public DataSet GetSkillsName(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Profile_GetSkillsName");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetPortfolios(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Profile_GetPortfolios");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string UpdateProfileHourlyRate(Int64 UserId, decimal HourlyRate, Int64 UpdateBy)
        {
            string val = "error:Error while updating profile hourly rate, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdateProfileHourlyRate");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("HourlyRate", HourlyRate));
                dbc.Parameters.Add(new SqlParameter("UpdateBy", UpdateBy));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating profile hourly rate, please refresh and try again";
            }

            return val;
        }


        public string UpdateProfileVisibility(Int64 UserId, string Visibility, Int64 UpdateBy)
        {
            string val = "error:Error while updating profile visibility, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdateProfileVisibility");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Visibility", Visibility));
                dbc.Parameters.Add(new SqlParameter("UpdateBy", UpdateBy));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating profile visibility, please refresh and try again";
            }

            return val;
        }

        public string UpdateProfileAvailability(Int64 UserId, string Availability, Int64 UpdateBy, string VacationDate)
        {
            string val = "error:Error while updating profile availability, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdateProfileAvailability");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Availability", Availability));
                dbc.Parameters.Add(new SqlParameter("VacationDate", VacationDate));
                dbc.Parameters.Add(new SqlParameter("UpdateBy", UpdateBy));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating profile availability, please refresh and try again";
            }

            return val;
        }
    }
}
