using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_interview_UserControls_wucMyInterviews : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInterviewDetails();
        }
    }

    public void LoadInterviewDetails()
    {
        try
        {
            string ScheduleId = hfInterview_data.Value;
            ScheduleId = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(ScheduleId), Trabau_Keys.Job_Key);
            Interview obj = new Interview();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds_sch = obj.GetMyScheduledInterview_Details(Int32.Parse(ScheduleId),Int64.Parse(UserID));

            if (ds_sch != null)
            {
                if (ds_sch.Tables.Count > 0)
                {
                    if (ds_sch.Tables[0].Rows.Count > 0)
                    {
                        string InterviewDate = ds_sch.Tables[0].Rows[0]["InterviewDate"].ToString();
                        string InterviewFromTime = ds_sch.Tables[0].Rows[0]["InterviewFromTime"].ToString();
                        string InterviewToTime = ds_sch.Tables[0].Rows[0]["InterviewToTime"].ToString();

                        txtInterviewDate.Text = InterviewDate;
                        txtInterviewFromTime.Text = InterviewFromTime;
                        txtInterviewToTime.Text = InterviewToTime;
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }
}