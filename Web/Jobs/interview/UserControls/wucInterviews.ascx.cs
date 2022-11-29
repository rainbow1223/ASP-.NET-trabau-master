using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_interview_UserControls_wucInterviews : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayMyInterviews();
        }
    }

    public void DisplayMyInterviews()
    {
        try
        {
            Interview obj = new Interview();
            string UserID = Session["Trabau_UserId"].ToString();
            string InterviewFilter = hfInterviewFilter.Value;
            ddlInterviewFilter.SelectedValue = InterviewFilter;
            DataSet ds_interviews = obj.GetInterviews(Int64.Parse(UserID), InterviewFilter);
            var interviews = ds_interviews.Tables[0].ToDynamic().Select(x => new
            {
                x.JobTitle,
                x.UserId,
                x.Title,
                x.Name,
                x.LocalTime,
                x.TotalEarning,
                x.HourlyRate,
                x.Overview,
                profile_id = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.UserId.ToString(), Trabau_Keys.Profile_Key)),
                ProposalId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.ProposalId.ToString() + "-" + x.JobId.ToString(), Trabau_Keys.Job_Key)),
                InterviewId_Enc = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.InterviewId.ToString() + "-0", Trabau_Keys.Job_Key)),
                x.InterviewId,
                x.InterviewRequest,
                x.InterviewRequestStatus,
                x.CanTakeAction_InterviewRequest,
                x.CanSchedule
            }).ToList();

            var interviews_sch = ds_interviews.Tables[1].ToDynamic().Select(x => new
            {
                x.Interview_Schedule,
                x.InterviewId,
                InterviewId_Enc = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.InterviewId.ToString() + "-" + x.ScheduleId.ToString(), Trabau_Keys.Job_Key)),
                x.InterviewActionType,
                x.InterviewStarted,
                x.CanViewReport
            }).ToList();

            rInterviews.DataSource = interviews;
            rInterviews.DataBind();

            if (rInterviews.Items.Count == 0)
            {
                div_no_result.Visible = true;
            }

            foreach (RepeaterItem item in rInterviews.Items)
            {
                string _UserId = (item.FindControl("lblUserId") as Label).Text;
                string InterviewId = (item.FindControl("lblInterviewId") as Label).Text;

                try
                {
                    Repeater rInterview_Schedules = (item.FindControl("rInterview_Schedules") as Repeater);
                    var interview_sch_selected = interviews_sch.Where(xx => xx.InterviewId == Int32.Parse(InterviewId)).ToList();
                    rInterview_Schedules.DataSource = interview_sch_selected;
                    rInterview_Schedules.DataBind();

                    int Interview_Count = interview_sch_selected.Count();
                    if (Interview_Count > 0)
                    {
                        (item.FindControl("btn_remove_interview") as HtmlAnchor).Visible = false;
                    }

                    interview_sch_selected = null;
                }
                catch (Exception)
                {
                    (item.FindControl("btn_remove_interview") as HtmlAnchor).Visible = false;
                }


                _UserId = _UserId + "~" + DateTime.Now.ToString();
                _UserId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(_UserId, Trabau_Keys.Profile_Key));

                string ran = Guid.NewGuid().ToString();
                (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data-key", ran + "_userpic_" + (item.ItemIndex + 1000).ToString());
                (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data", _UserId);
                (item.FindControl("imgFL_ProfilePic") as HtmlImage).Src = "../../assets/uploads/loading_pic.gif";


            }
        }
        catch (Exception)
        {
        }
    }
}