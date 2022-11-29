using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_Posting_UserControls_wucInterviewDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetInterviewDetails();
        }
    }

    public void GetInterviewDetails()
    {
        try
        {
            Session["Trabau_Interview_Started"] = null;
            Session["Trabau_InterviewQuestions"] = null;
            string Interview_data = hfInterview_data.Value;
            Interview_data = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(Interview_data), Trabau_Keys.Job_Key);
            string InterviewId = Interview_data.Split('-')[0];
            string ScheduleId = Interview_data.Split('-')[1];

            if (ScheduleId != "" && ScheduleId != "0")
            {
                Interview obj = new Interview();
                string UserID = Session["Trabau_UserId"].ToString();
                DataSet ds_sch = obj.GetScheduledInterview_Details(Int32.Parse(ScheduleId), Int64.Parse(UserID));

                if (ds_sch != null)
                {
                    if (ds_sch.Tables.Count > 0)
                    {
                        if (ds_sch.Tables[0].Rows.Count > 0)
                        {
                            string InterviewDate = ds_sch.Tables[0].Rows[0]["InterviewDate"].ToString();
                            string InterviewFromTime = ds_sch.Tables[0].Rows[0]["InterviewFromTime"].ToString();
                            string InterviewToTime = ds_sch.Tables[0].Rows[0]["InterviewToTime"].ToString();
                            string InterviewType = ds_sch.Tables[0].Rows[0]["InterviewType"].ToString();
                            string InterviewContactNumber = ds_sch.Tables[0].Rows[0]["InterviewContactNumber"].ToString();
                            bool InterviewStarted = Convert.ToBoolean(ds_sch.Tables[0].Rows[0]["InterviewStarted"].ToString());
                            bool CanUpdateResponse = Convert.ToBoolean(ds_sch.Tables[0].Rows[0]["CanUpdateResponse"].ToString());
                            string InterviewResponse = ds_sch.Tables[0].Rows[0]["InterviewResponse"].ToString();

                            if (InterviewContactNumber != string.Empty && InterviewStarted)
                            {
                                div_interview_contact_info.Visible = true;
                                lblContactNumber.Text = InterviewContactNumber;
                            }
                            Session["Trabau_Interview_Started"] = InterviewStarted;

                            txtInterviewDate.Text = InterviewDate;
                            txtInterviewFromTime.Text = InterviewFromTime;
                            txtInterviewToTime.Text = InterviewToTime;
                            ddlInterviewType.SelectedValue = InterviewType;

                            txtInterviewDate.Enabled = !InterviewStarted;
                            txtInterviewFromTime.Enabled = !InterviewStarted;
                            txtInterviewToTime.Enabled = !InterviewStarted;
                            ddlInterviewType.Enabled = !InterviewStarted;


                            DataTable dt_ques = new DataTable();
                            dt_ques.Columns.Add("Question_Enc", typeof(string));
                            dt_ques.Columns.Add("Question", typeof(string));
                            dt_ques.Columns.Add("Ques_Key", typeof(string));
                            dt_ques.Columns.Add("Answer", typeof(string));

                            string ul = "";
                            for (int i = 0; i < ds_sch.Tables[1].Rows.Count; i++)
                            {
                                DataRow dr_ques = dt_ques.NewRow();
                                string Question = ds_sch.Tables[1].Rows[i]["Question"].ToString();
                                string Question_Key = ds_sch.Tables[1].Rows[i]["QuestionKey"].ToString();
                                string _Answer = ds_sch.Tables[1].Rows[i]["Answer"].ToString();

                                dr_ques["Question_Enc"] = Question;
                                dr_ques["Question"] = "<ques class='ques-content'>" + Question + "</ques>" + (InterviewStarted == false ? "<input type='button' value='Remove' class='btn-remove-question' />" : "");
                                dr_ques["Ques_Key"] = Question_Key;
                                dr_ques["Answer"] = _Answer;
                                dt_ques.Rows.Add(dr_ques);

                                string answer = string.Empty;
                                if (InterviewStarted)
                                {
                                    answer = _Answer;
                                    if (answer != string.Empty)
                                    {
                                        answer = "</br><b>Freelancer Answer: </b>" + answer;
                                    }
                                }
                                //string li = "<li class='interview-ques' data='" + Question_Key + "'>" + "<ques class='ques-content'>" + Question + "</ques>" + (InterviewStarted == false ? "<input type='button' value='Remove' class='btn-remove-question' />" : "") + answer + "</li>";
                                //ul = ul + li;
                            }
                            // ol_questions.InnerHtml = ul;
                            if (ul != string.Empty)
                            {
                                //  h4_ques_header.Attributes.Add("style", "display:block");
                            }

                            //for (var i = 0; i < json_data[1].length; i++)
                            //{
                            //    var li = '<li data="' + json_data[1][i].Ques_Key + '">' + json_data[1][i].Question + '</li>';
                            //    ul = ul + li;
                            //}

                            Session["Trabau_InterviewQuestions"] = dt_ques;
                            btnSubmitSchedule.Value = "Update Interview Schedule";
                            btnCancelSchedule.Visible = !InterviewStarted;
                            btnSubmitSchedule.Visible = !InterviewStarted;

                            if (CanUpdateResponse || InterviewResponse != string.Empty)
                            {
                                div_Interview_Response.Visible = true;
                                if (InterviewResponse == string.Empty)
                                {
                                    string _ScheduleId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(ScheduleId, Trabau_Keys.Job_Key));
                                    div_Interview_Response.Attributes.Add("data", _ScheduleId);
                                    DataSet ds_responses = obj.GetInterviewResponses();
                                    ddlInterviewResponse.DataSource = ds_responses;
                                    ddlInterviewResponse.DataTextField = "ResponseName";
                                    ddlInterviewResponse.DataValueField = "ResponseId";
                                    ddlInterviewResponse.DataBind();
                                }
                                else
                                {
                                    div_Interview_Response.InnerHtml = InterviewResponse;
                                }
                            }
                            //  div_ques.Visible = !InterviewStarted;
                            //  div_add_ques.Visible = !InterviewStarted;
                        }
                        else
                        {
                            Session["Trabau_InterviewQuestions"] = null;
                        }
                    }
                    else
                    {
                        Session["Trabau_InterviewQuestions"] = null;
                    }
                }
                else
                {
                    Session["Trabau_InterviewQuestions"] = null;
                }
            }
            else
            {
                DataTable dt_ques = new DataTable();
                dt_ques.Columns.Add("Question_Enc", typeof(string));
                dt_ques.Columns.Add("Question", typeof(string));
                dt_ques.Columns.Add("Ques_Key", typeof(string));
                dt_ques.Columns.Add("Answer", typeof(string));

                Session["Trabau_InterviewQuestions"] = dt_ques;
            }

        }
        catch (Exception)
        {

        }
    }
}