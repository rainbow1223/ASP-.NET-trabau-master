using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabauClassLibrary.DLL.Job
{
    public class searchjob : dbCon
    {
        public DataSet SearchJobs(Int64 UserId, string Type, string Text, int PageNumber, string JobType, string ExpLevel, string ClientHistory,
            string NoOfProposals, string Budget, string HoursPerWeek, string ProjectLength, string SavedJobs)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SearchJobs");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Type", Type));
                dbc.Parameters.Add(new SqlParameter("Text", Text));
                dbc.Parameters.Add(new SqlParameter("PageNumber", PageNumber));
                dbc.Parameters.Add(new SqlParameter("JobType", JobType));
                dbc.Parameters.Add(new SqlParameter("ExpLevel", ExpLevel));
                dbc.Parameters.Add(new SqlParameter("ClientHistory", ClientHistory));
                dbc.Parameters.Add(new SqlParameter("NoOfProposals", NoOfProposals));
                dbc.Parameters.Add(new SqlParameter("Budget", Budget));
                dbc.Parameters.Add(new SqlParameter("HoursPerWeek", HoursPerWeek));
                dbc.Parameters.Add(new SqlParameter("ProjectLength", ProjectLength));
                dbc.Parameters.Add(new SqlParameter("SavedJobs", SavedJobs));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet SearchJobs_Op(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SearchJobs_Op");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetJobDetails(Int64 UserId, int JobId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SearchJobs_GetJobDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetAdditionalFileDetails(Int64 UserId, int JobId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SearchJobs_GetAdditionalFileDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet SubmitProposal(Int64 UserId, int JobId, double BIDAmount, string ProjectLength, string CoverLetter, string IPAddress,
            string Screen_Answers_XML, DataTable Apply_Job_Files_XML)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SearchJobs_SubmitProposal");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("BIDAmount", BIDAmount));
                dbc.Parameters.Add(new SqlParameter("ProjectLength", ProjectLength));
                dbc.Parameters.Add(new SqlParameter("CoverLetter", CoverLetter));
                dbc.Parameters.Add(new SqlParameter("Screen_Answers_XML", Screen_Answers_XML));
                dbc.Parameters.Add(new SqlParameter("Apply_Job_Files_XML", Apply_Job_Files_XML));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string SaveJob(Int64 UserId, int JobId)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SearchJobs_Save");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while submitting proposal, please refresh and try again";
            }

            return val;
        }


        public DataSet GetUserCategories(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SearchJobs_GetUserCategories");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetAdvanceControlFilters(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SearchJobs_GetAdvanceControlFilters");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetSubmittedProposals(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetSubmittedProposals");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetSubmittedProposalDetails(Int64 UserId, int ProposalId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetSubmittedProposalDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ProposalId", ProposalId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetProposal_AdditionalFileDetails(Int64 UserId, int ProposalId, int JobId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_GetProposal_AdditionalFileDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ProposalId", ProposalId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public List<string> GetUsersList(string Prefix, Int64 UserId)
        {
            List<string> lst = new List<string>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SearchJobs_GetUsers");
                dbc.Parameters.Add(new SqlParameter("Prefix", Prefix));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                lst = db.ExecuteDataSet(dbc).Tables[0].AsEnumerable()
                            .Select(r => r.Field<string>("Text") + "::" + MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(r.Field<string>("Value").ToString(), Trabau_Keys.Profile_Key)))
                            .ToList();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }
    }
}
