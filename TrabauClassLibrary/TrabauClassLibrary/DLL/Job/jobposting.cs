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
    public class jobposting : dbCon
    {
        public DataSet GetSkillsList(string SkillType)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_GetSkillsList");
                dbc.Parameters.Add(new SqlParameter("SkillType", SkillType));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetLocationList()
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_GetLocationList");
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string SaveJob(string Title, string Category, string Description, string TypeOfWork, string PaymentType, string XML_ScreeningQuestions,
            string FE_ProjectSkills_Deliverable, string FE_ProjectSkills_Languages, string FE_ProjectSkills, string BusinessSize, string AdditionalSkills,
            string Visibility, int NoOfFreelancers, string Location, string LocationDistanceType, string LocationZipCode, int LocationDistance,
            string LocationDistanceUnits, string XML_TalentScreeningQuestions, string BudgetType, int BudgetValue, int Bonus_BudgetValue, string LevelOfExperience,
            int JobId, string IPAddress, int UserId, string XML_ProjectFiles, string XML_People)
        {
            string response = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_SaveJob");
                dbc.Parameters.Add(new SqlParameter("Title", Title));
                dbc.Parameters.Add(new SqlParameter("Category", Category));
                dbc.Parameters.Add(new SqlParameter("Description", Description));
                dbc.Parameters.Add(new SqlParameter("TypeOfWork", TypeOfWork));
                dbc.Parameters.Add(new SqlParameter("PaymentType", PaymentType));
                dbc.Parameters.Add(new SqlParameter("XML_ScreeningQuestions", XML_ScreeningQuestions));
                dbc.Parameters.Add(new SqlParameter("FE_ProjectSkills_Deliverable", FE_ProjectSkills_Deliverable));
                dbc.Parameters.Add(new SqlParameter("FE_ProjectSkills_Languages", FE_ProjectSkills_Languages));
                dbc.Parameters.Add(new SqlParameter("FE_ProjectSkills", FE_ProjectSkills));
                dbc.Parameters.Add(new SqlParameter("BusinessSize", BusinessSize));
                dbc.Parameters.Add(new SqlParameter("AdditionalSkills", AdditionalSkills));
                dbc.Parameters.Add(new SqlParameter("Visibility", Visibility));
                dbc.Parameters.Add(new SqlParameter("NoOfFreelancers", NoOfFreelancers));
                dbc.Parameters.Add(new SqlParameter("Location", Location));
                dbc.Parameters.Add(new SqlParameter("LocationDistanceType", LocationDistanceType));
                dbc.Parameters.Add(new SqlParameter("LocationZipCode", LocationZipCode));
                dbc.Parameters.Add(new SqlParameter("LocationDistance", LocationDistance));
                dbc.Parameters.Add(new SqlParameter("LocationDistanceUnits", LocationDistanceUnits));
                dbc.Parameters.Add(new SqlParameter("XML_TalentScreeningQuestions", XML_TalentScreeningQuestions));
                dbc.Parameters.Add(new SqlParameter("BudgetType", BudgetType));
                dbc.Parameters.Add(new SqlParameter("BudgetValue", BudgetValue));
                dbc.Parameters.Add(new SqlParameter("Bonus_BudgetValue", Bonus_BudgetValue));
                dbc.Parameters.Add(new SqlParameter("LevelOfExperience", LevelOfExperience));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("XML_ProjectFiles", XML_ProjectFiles));
                dbc.Parameters.Add(new SqlParameter("XML_People", XML_People));
                response = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                response = "error:Error while posting job, please refresh and try again";
            }

            return response;
        }

        public DataSet GetPostedJobs(int UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_GetPostedJobs");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetPostedJobDetails(int UserId, int JobId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_GetPostedJobDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.CommandTimeout = 60;
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetPostedJobMenu(int UserId, int JobId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_GetPostedJobMenu");
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

        public DataSet SearchFreelancers(int UserId, int JobId, string Text, string Type)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_SearchFreelancers");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("Text", Text));
                dbc.Parameters.Add(new SqlParameter("Type", Type));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string SaveJobDetails(Int64 UserId, int JobId, Int64 AddUserId, string Type, bool Value, string IPAddress)
        {
            string response = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_SaveDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("AddUserId", AddUserId));
                dbc.Parameters.Add(new SqlParameter("Type", Type));
                dbc.Parameters.Add(new SqlParameter("Value", Value));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                response = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                response = "error:Error while saving details, please refresh and try again";
            }

            return response;
        }


        public string RemoveJob(Int64 UserId, int JobId, string IPAddress)
        {
            string response = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_RemoveJob");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                response = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                response = "error:Error while removing posted job, please refresh and try again";
            }

            return response;
        }


        public string UpdateVisibility(Int64 UserId, int JobId, string IPAddress, string Visibility)
        {
            string response = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_UpdateVisibility");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                dbc.Parameters.Add(new SqlParameter("Visibility", Visibility));
                response = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                response = "error:Error while changing posted job visibility, please refresh and try again";
            }

            return response;
        }


        public DataSet GetPeopleTitle(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_JobPosting_GetPeopleTitle");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }



        public DataSet GetJobSubmittedProposals(int JobId, Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetJobSubmittedProposals");
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetJobSubmittedProposalDetails(int JobId, int ProposalId, Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetJobSubmittedProposalDetails");
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("ProposalId", ProposalId));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet SaveJobProposalAction(int JobId, int ProposalId, Int64 UserId, string Action, string IPAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SaveJobProposalAction");
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("ProposalId", ProposalId));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Action", Action));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetMyHires(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetMyHires");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet FlagForInterview(int JobId, int ProposalId, string IPAddress, Int64 EntryBy, string Flag)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_UserJobs_SaveFlagForInterview");
                dbc.Parameters.Add(new SqlParameter("Flag", Flag));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("ProposalId", ProposalId));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                dbc.Parameters.Add(new SqlParameter("EntryBy", EntryBy));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet InterviewRequest(int ProposalId, Int64 UserId, string IPAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Proposal_InterviewRequest");
                dbc.Parameters.Add(new SqlParameter("ProposalId", ProposalId));
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


        public DataSet RejectInterview_Request(Int64 UserId, int ProposalId, string ActionType)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Proposals_InterviewRequestAction");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ProposalId", ProposalId));
                dbc.Parameters.Add(new SqlParameter("ActionType", ActionType));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet SendJobToFriend(Int64 UserId, int JobId, string EmailAddress, string Name, string IPAddress, Int64 Freelancer_UserID = 0)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_UserJobs_SendToFriend");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("JobId", JobId));
                dbc.Parameters.Add(new SqlParameter("EmailAddress", EmailAddress));
                dbc.Parameters.Add(new SqlParameter("Name", Name));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                dbc.Parameters.Add(new SqlParameter("Freelancer_UserID", Freelancer_UserID));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }



        public DataSet GetJobSubmittedAllProposals(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetJobSubmittedAllProposals");
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
