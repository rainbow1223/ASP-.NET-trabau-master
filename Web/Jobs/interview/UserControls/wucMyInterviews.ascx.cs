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
            GetMyInterviews();
        }
    }

    public void GetMyInterviews()
    {
        try
        {
            Interview obj = new Interview();
            string UserID = Session["Trabau_UserId"].ToString();
            string InterviewFilter = hfInterviewFilter.Value;
            ddlInterviewFilter.SelectedValue = InterviewFilter;
            DataSet dsjobs = obj.GetMyInterviews(Int64.Parse(UserID), InterviewFilter);
            var interviews = dsjobs.Tables[0].ToDynamic().Select(x => new
            {
                x.JobId,
                x.JobTitle,
                x.Client_Name,
                x.Budget,
                x.BudgetType,
                x.ProjectLength,
                x.Interview_Schedule,
                ScheduleId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.ScheduleId.ToString(), Trabau_Keys.Job_Key)),
                x.CanTakeAction,
                x.InterviewActionType,
                x.InterviewAction_Class,
                x.CanStartInterview,
                x.InterviewStatus,
                x.InterviewStatus_Class,
                x.CanViewReport,
                x.InterviewRequestStatus,
                x.CanViewInterviewRequestStatus
            }).ToList();

            rMyInterviews.DataSource = interviews;
            rMyInterviews.DataBind();

            if (rMyInterviews.Items.Count == 0)
            {
                div_no_result.Visible = true;
            }
        }
        catch (Exception)
        {
        }
    }
}