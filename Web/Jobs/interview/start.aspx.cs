using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_interview_start : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString["schedule"] != null)
                    {
                        if (Request.QueryString["schedule"] != string.Empty)
                        {
                            string ScheduleId = Request.QueryString["schedule"];
                            ScheduleId = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(ScheduleId), Trabau_Keys.Job_Key);
                            GetMyInterviewDetails(Int32.Parse(ScheduleId));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }

    public void GetMyInterviewDetails(int ScheduleId)
    {
        try
        {
            Interview obj = new Interview();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds_sch = obj.GetInterviewScheduleDetails(Int64.Parse(UserID), ScheduleId);
            if (ds_sch != null)
            {
                if (ds_sch.Tables.Count > 0)
                {
                    if (ds_sch.Tables[0].Rows.Count > 0)
                    {
                        string JobTitle = ds_sch.Tables[0].Rows[0]["JobTitle"].ToString();
                        string JobId = ds_sch.Tables[0].Rows[0]["JobId"].ToString();
                        lblInterviewHeader.Text = "Interview for " + JobTitle;

                        JobId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(JobId, Trabau_Keys.Job_Key));
                        aViewJobPosting.HRef = "~/jobs/searchjobs/viewjob.aspx?job=" + JobId;

                        Session["Trabau_Interview_Ques"] = ds_sch.Tables[1];
                        GetNextQuestion();

                    }
                    else
                    {
                        div_ques.Visible = false;
                        div_ques_finished.Visible = true;
                        div_interview_header.Visible = false;
                    }
                }
                else
                {
                    div_ques.Visible = false;
                    div_ques_finished.Visible = true;
                    div_interview_header.Visible = false;
                }
            }
            else
            {
                div_ques.Visible = false;
                div_ques_finished.Visible = true;
                div_interview_header.Visible = false;
            }

        }
        catch (Exception ex)
        {
        }
    }

    public void GetNextQuestion()
    {
        try
        {
            txtAnswer.Text = string.Empty;

            DataTable dt_sch = Session["Trabau_Interview_Ques"] as DataTable;
            var questions = dt_sch.ToDynamic().Select(x => new
            {
                x.SrNo,
                x.ScheduleId,
                x.Question,
                x.QuestionId,
                x.Answer
            }).ToList();

            if (lblTotalQuestions.Text == string.Empty)
            {
                lblTotalQuestions.Text = "Total Questions: " + questions.Count().ToString();
            }

            var ques = questions.Where(x => x.Answer == string.Empty).OrderBy(o => o.SrNo).Take(1).SingleOrDefault();
            if (ques != null)
            {
                lblQuestion.Text = ques.Question;
                lblQuestionId.Text = ques.QuestionId.ToString();
                lblQuestionNo.Text = "Ques. " + ques.SrNo.ToString();
                lblScheduleId.Text = ques.ScheduleId.ToString();

                if (questions.Count() == ques.SrNo)
                {
                    btnNextQuestion.Visible = false;
                    btnSendtoClient.Visible = true;
                }
            }
            else
            {
                div_ques.Visible = false;
                div_ques_finished.Visible = true;
                div_interview_header.Visible = false;
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnSendtoClient_Click(object sender, EventArgs e)
    {
        SaveQuestion(true);
    }

    protected void btnNextQuestion_Click(object sender, EventArgs e)
    {
        SaveQuestion(false);
    }

    public void SaveQuestion(bool IsLastQuestion)
    {
        try
        {
            string QuestionId = lblQuestionId.Text;
            string ScheduleId = lblScheduleId.Text;
            string Answer = txtAnswer.Text;
            string UserID = Session["Trabau_UserId"].ToString();
            string IPAddress = Request.UserHostAddress;

            Interview obj = new Interview();
            string data = obj.SaveInterviewQuestionAnswers(Int64.Parse(UserID), Int32.Parse(QuestionId), Int32.Parse(ScheduleId), Answer, IPAddress, IsLastQuestion);

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];

            if (response == "success")
            {
                if (!IsLastQuestion)
                {
                    DataTable dt_sch = Session["Trabau_Interview_Ques"] as DataTable;
                    foreach (DataRow row in dt_sch.Rows)
                    {
                        if (row["QuestionId"].ToString() == QuestionId)
                        {
                            row["Answer"] = Answer;
                            break;
                        }
                    }

                    GetNextQuestion();
                }
                else
                {
                    Response.Redirect("~/jobs/interview/myinterviews.aspx");
                }
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "JobPost_Message_Success", "setTimeout(function () { window.location.href='postedjobs.aspx';}, 1000);", true);
                //
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveQuestion_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
        }
        catch (Exception)
        {
        }
    }
}