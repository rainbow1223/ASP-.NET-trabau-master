using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_interview_UserControls_wucInterviewReport : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetInterviewReport();
        }
    }

    public void GetInterviewReport()
    {
        try
        {
            Session["Trabau_Interview_Started"] = null;
            Session["Trabau_InterviewQuestions"] = null;
            string Interview_data = hfInterview_data.Value;
            Interview_data = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(Interview_data), Trabau_Keys.Job_Key);
            string ScheduleId = "";
            if (Interview_data.Contains("-"))
            {
                string InterviewId = Interview_data.Split('-')[0];
                ScheduleId = Interview_data.Split('-')[1];
            }
            else
            {
                ScheduleId = Interview_data.Split('-')[0];
            }
            if (ScheduleId != "" && ScheduleId != "0")
            {
                Interview obj = new Interview();
                string UserID = Session["Trabau_UserId"].ToString();
                DataSet ds_sch = obj.GetInterviewReport(Int64.Parse(UserID), Int32.Parse(ScheduleId));

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

                            lblInterviewDate.Text = InterviewDate;
                            lblInterviewFromTime.Text = InterviewFromTime;
                            lblInterviewToTime.Text = InterviewToTime;
                            lblInterviewType.Text = InterviewType;


                            DataTable dt_ques = new DataTable();
                            dt_ques.Columns.Add("Question_Enc", typeof(string));
                            dt_ques.Columns.Add("Question", typeof(string));
                            dt_ques.Columns.Add("Ques_Key", typeof(string));
                            dt_ques.Columns.Add("Answer", typeof(string));

                            string ul = "";
                            for (int i = 0; i < ds_sch.Tables[1].Rows.Count; i++)
                            {
                                string Question = ds_sch.Tables[1].Rows[i]["Question"].ToString();
                                string _Answer = ds_sch.Tables[1].Rows[i]["Answer"].ToString();

                                _Answer = "</br><b>Freelancer Answer: </b>" + _Answer;
                                string li = "<li class='interview-ques'><ques class='ques-content'>" + Question + "</ques>" + _Answer + "</li>";

                                ul = ul + li;
                                //DataRow dr_ques = dt_ques.NewRow();

                                //string Question_Key = ds_sch.Tables[1].Rows[i]["QuestionKey"].ToString();

                                //dr_ques["Question_Enc"] = Question;
                                //dr_ques["Question"] = "<ques class='ques-content'>" + Question + "</ques>" + (InterviewStarted == false ? "<input type='button' value='Remove' class='btn-remove-question' />" : "");
                                //dr_ques["Ques_Key"] = Question_Key;
                                //dr_ques["Answer"] = _Answer;
                                //dt_ques.Rows.Add(dr_ques);


                                //string li = "<li class='interview-ques' data='" + Question_Key + "'>" + "<ques class='ques-content'>" + Question + "</ques>" + (InterviewStarted == false ? "<input type='button' value='Remove' class='btn-remove-question' />" : "") + answer + "</li>";
                                //ul = ul + li;
                            }
                            ol_questions.InnerHtml = ul;



                            if (InterviewResponse != string.Empty)
                            {
                                div_Interview_Response.Visible = true;

                                div_Interview_Response.InnerHtml = InterviewResponse;
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
                Session["Trabau_InterviewQuestions"] = null;
            }
        }
        catch (Exception)
        {
        }
    }
}