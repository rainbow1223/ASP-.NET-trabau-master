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
    public class Interview : dbCon
    {
        public DataSet GetInterviews(Int64 UserId, string InterviewFilter)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_GetInterviews");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("InterviewFilter", InterviewFilter));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet ScheduleInterview(Int64 EntryBy, int InterviewId, string InterviewDate, string InterviewFromTime, string InterviewToTime,
            string XML_InterviewQuestions, string InterviewType, int ScheduleId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_ScheduleInterview");
                dbc.Parameters.Add(new SqlParameter("InterviewId", InterviewId));
                dbc.Parameters.Add(new SqlParameter("InterviewDate", InterviewDate));
                dbc.Parameters.Add(new SqlParameter("InterviewFromTime", InterviewFromTime));
                dbc.Parameters.Add(new SqlParameter("InterviewToTime", InterviewToTime));
                dbc.Parameters.Add(new SqlParameter("EntryBy", EntryBy));
                dbc.Parameters.Add(new SqlParameter("XML_InterviewQuestions", XML_InterviewQuestions));
                dbc.Parameters.Add(new SqlParameter("InterviewType", InterviewType));
                dbc.Parameters.Add(new SqlParameter("ScheduleId", ScheduleId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetScheduledInterview_Details(int ScheduleId, Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_GetScheduledInterview_Details");
                dbc.Parameters.Add(new SqlParameter("ScheduleId", ScheduleId));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet Cancel_ScheduledInterview(Int64 UserId, int ScheduleId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_Cancel_ScheduledInterview");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ScheduleId", ScheduleId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetMyScheduledInterview_Details(int ScheduleId, Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_GetMyScheduledInterview_Details");
                dbc.Parameters.Add(new SqlParameter("ScheduleId", ScheduleId));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }



        public DataSet GetMyInterviews(Int64 UserId, string InterviewFilter)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_GetFreelancers_Interviews");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("InterviewFilter", InterviewFilter));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet Freelancer_Interview_Action(Int64 UserId, int ScheduleId, string ActionType, string ContactNumber)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_Freelancer_Interview_Action");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ScheduleId", ScheduleId));
                dbc.Parameters.Add(new SqlParameter("ActionType", ActionType));
                dbc.Parameters.Add(new SqlParameter("ContactNumber", ContactNumber));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet UpdateInterviewSchedule(Int64 UserId, int ScheduleId, string InterviewDate, string InterviewFromTime, string InterviewToTime)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_Freelancer_UpdateInterviewSchedule");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ScheduleId", ScheduleId));
                dbc.Parameters.Add(new SqlParameter("InterviewDate", InterviewDate));
                dbc.Parameters.Add(new SqlParameter("InterviewFromTime", InterviewFromTime));
                dbc.Parameters.Add(new SqlParameter("InterviewToTime", InterviewToTime));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetInterviewScheduleDetails(Int64 UserId, int ScheduleId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_GetInterviewScheduleDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ScheduleId", ScheduleId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string SaveInterviewQuestionAnswers(Int64 UserId, int QuestionId, int ScheduleId, string Answer, string IPAddress, bool IsLastQuestion)
        {
            string response = "error:Error while saving question answer, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_SaveInterviewQuestionAnswers");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("QuestionId", QuestionId));
                dbc.Parameters.Add(new SqlParameter("ScheduleId", ScheduleId));
                dbc.Parameters.Add(new SqlParameter("Answer", Answer));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                dbc.Parameters.Add(new SqlParameter("IsLastQuestion", IsLastQuestion));
                response = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {

            }

            return response;
        }


        public DataSet GetInterviewResponses()
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_GetInterviewResponses");
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet SaveInterviewResponse(int ScheduleId, Int64 UserId, int ResponseId, string IPAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_SaveInterviewResponse");
                dbc.Parameters.Add(new SqlParameter("ScheduleId", ScheduleId));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ResponseId", ResponseId));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetInterviewReport(Int64 UserId, int ScheduleId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Jobs_GetScheduledInterview_Report");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ScheduleId", ScheduleId));
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
