using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_interview_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Trabau_InterviewQuestions"] = null;
        }
    }

    [WebMethod(EnableSession = true)]
    public static string GetMyInterviews(string InterviewFilter)
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    var pageHolder = new Page();
                    var form = new HtmlForm();
                    var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/interview/UserControls/wucInterviews.ascx");
                    try
                    {
                        (viewControl.FindControl("hfInterviewFilter") as HiddenField).Value = InterviewFilter;
                    }
                    catch (Exception)
                    {
                    }
                    var scriptManager = new ScriptManager();
                    form.Controls.Add(scriptManager);
                    form.Controls.Add(viewControl);
                    pageHolder.Controls.Add(form);
                    HttpContext.Current.Server.Execute(pageHolder, output, false);


                    detail.Add("response", "ok");
                    detail.Add("interviews_html", output.ToString().Replace("\r\n", "").ToString());
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("interviews_html", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("interviews_html", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }


    [WebMethod(EnableSession = true)]
    public static string RemoveFromInterview(string ProposalId)
    {
        string val = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();

            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    string data = ProposalId;
                    data = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(data), Trabau_Keys.Job_Key);
                    string _ProposalId = data.Split('-')[0];
                    string JobId = data.Split('-')[1];
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                    string IPAddress = HttpContext.Current.Request.UserHostAddress;

                    jobposting obj = new jobposting();
                    DataSet ds_response = obj.FlagForInterview(Int32.Parse(JobId), Int32.Parse(_ProposalId), IPAddress, Int64.Parse(UserID), "R");
                    string response = "error";
                    string message = "Error while taking action on interview list, please refresh and try again";
                    if (ds_response != null)
                    {
                        if (ds_response.Tables.Count > 0)
                        {
                            if (ds_response.Tables[0].Rows.Count > 0)
                            {
                                response = ds_response.Tables[0].Rows[0]["Response"].ToString();
                                message = ds_response.Tables[0].Rows[0]["Message"].ToString();
                                //string Freelancer_Name = ds_response.Tables[0].Rows[0]["Name"].ToString();
                                //string JobTitle = ds_response.Tables[0].Rows[0]["JobTitle"].ToString();
                                //string EmailId = ds_response.Tables[0].Rows[0]["EmailId"].ToString();

                                if (response == "success")
                                {
                                    try
                                    {
                                        //Emailer obj_email = new Emailer();
                                        //string ClientName = HttpContext.Current.Session["Trabau_FirstName"].ToString();
                                        //string body = "Dear " + Freelancer_Name + ",<br/><br/>You are hired by " + ClientName + " for the job " + JobTitle;

                                        //obj_email.SendEmail(EmailId, "", "", "Trabau Hiring", "You are hired - Trabau", body, null);



                                        //string client_body = "Dear " + ClientName + ",<br/><br/>You have hired " + Freelancer_Name + " (freelancer) for the job " + JobTitle;
                                        //string ClientEmailId = HttpContext.Current.Session["Trabau_EmailId"].ToString();
                                        //obj_email.SendEmail(ClientEmailId, "", "", "Trabau Hiring", "You have hired - Trabau", client_body, null);

                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                        }
                    }


                    detail.Add("action_response", response);
                    detail.Add("action_message", message);

                    detail.Add("response", "ok");
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("action_response", "");
                    detail.Add("action_message", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("action_response", "");
                detail.Add("action_message", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            val = serial.Serialize(details);

        }
        catch (Exception ex)
        {
            val = "";
        }
        return val;
    }


    [WebMethod(EnableSession = true)]
    public static string GetInterviewContentDetails(string data)
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    var pageHolder = new Page();
                    var form = new HtmlForm();
                    var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/Posting/UserControls/wucInterviewDetails.ascx");
                    try
                    {
                        (viewControl.FindControl("hfInterview_data") as HiddenField).Value = data;
                    }
                    catch (Exception)
                    {
                    }
                    var scriptManager = new ScriptManager();
                    form.Controls.Add(scriptManager);
                    form.Controls.Add(viewControl);
                    pageHolder.Controls.Add(form);
                    HttpContext.Current.Server.Execute(pageHolder, output, false);


                    detail.Add("response", "ok");
                    detail.Add("Interview_html", output.ToString().Replace("\r\n", "").ToString());
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("Interview_html", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("Interview_html", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }


    [WebMethod(EnableSession = true)]
    public static string AddInterviewQuestion(string Question)
    {
        string val = "";
        try
        {
            Question = Question.Trim();
            List<List<Dictionary<string, object>>> main_rows = new List<List<Dictionary<string, object>>>();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = new Dictionary<string, object>();

            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    row.Add("response", "ok");
                    row.Add("action_response", "");
                    row.Add("action_message", "");

                    DataTable dt_ques = new DataTable();
                    if (HttpContext.Current.Session["Trabau_InterviewQuestions"] != null)
                    {
                        dt_ques = HttpContext.Current.Session["Trabau_InterviewQuestions"] as DataTable;
                    }
                    else
                    {
                        dt_ques.Columns.Add("Question_Enc", typeof(string));
                        dt_ques.Columns.Add("Question", typeof(string));
                        dt_ques.Columns.Add("Ques_Key", typeof(string));
                    }

                    DataView dv_ques = dt_ques.DefaultView;
                    dv_ques.RowFilter = "Question_Enc='" + Question + "'";
                    if (dv_ques.Count == 0)
                    {
                        DataRow dr_ques = dt_ques.NewRow();
                        dr_ques["Question_Enc"] = Question;
                        dr_ques["Question"] = "<ques class='ques-content'>" + Question + "</ques><input type='button' value='Remove' class='btn-remove-question' />";
                        dr_ques["Ques_Key"] = MiscFunctions.RandomString(20).ToLower();
                        dt_ques.Rows.Add(dr_ques);

                        HttpContext.Current.Session["Trabau_InterviewQuestions"] = dt_ques;

                        row["action_response"] = "success";
                        row["action_message"] = "Question Added";
                        rows.Add(row);
                        main_rows.Add(rows);

                        rows = new List<Dictionary<string, object>>();
                        foreach (DataRow dr in dt_ques.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt_ques.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }

                        main_rows.Add(rows);
                    }
                    else
                    {
                        row["action_response"] = "error";
                        row["action_message"] = "Same question already added in the list";
                        rows.Add(row);
                        main_rows.Add(rows);
                    }
                }
                else
                {
                    row["action_response"] = "error";
                    row["action_message"] = "Error while adding new question, please refresh and try again";
                    rows.Add(row);
                    main_rows.Add(rows);
                }
            }
            else
            {
                row["action_response"] = "error";
                row["action_message"] = "Error while adding new question, please refresh and try again";
                rows.Add(row);
                main_rows.Add(rows);
            }



            JavaScriptSerializer serial = new JavaScriptSerializer();
            val = serial.Serialize(main_rows);

        }
        catch (Exception ex)
        {
            val = "";
        }
        return val;
    }


    [WebMethod(EnableSession = true)]
    public static string RemoveInterviewQuestion(string question)
    {
        string val = "";
        try
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = new Dictionary<string, object>(); ;
            string response = "";
            string message = "";
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    row.Add("response", "ok");

                    DataTable dt_ques = new DataTable();
                    if (HttpContext.Current.Session["Trabau_InterviewQuestions"] != null)
                    {
                        dt_ques = HttpContext.Current.Session["Trabau_InterviewQuestions"] as DataTable;
                        if (dt_ques.Rows.Count > 0)
                        {
                            foreach (DataRow _row in dt_ques.Rows)
                            {
                                if (_row["Ques_Key"].ToString() == question)
                                {
                                    dt_ques.Rows.Remove(_row);
                                    response = "success";
                                    message = "Removed from the interview list";

                                    break;
                                }
                            }
                        }
                        else
                        {
                            response = "error";
                            message = "Error while removing from the question, please refresh and try again";
                        }
                    }
                    else
                    {
                        response = "error";
                        message = "Error while removing from the question, please refresh and try again";
                    }
                }
                else
                {
                    row.Add("response", "");
                    response = "error";
                    message = "Error while removing from the question, please refresh and try again";
                }
            }
            else
            {
                row.Add("response", "");
                response = "error";
                message = "Error while removing from the question, please refresh and try again";
            }
            if (response == string.Empty)
            {
                response = "error";
                message = "Error while removing from the question, please refresh and try again";
            }
            row.Add("action_response", response);
            row.Add("action_message", message);

            rows.Add(row);


            JavaScriptSerializer serial = new JavaScriptSerializer();
            val = serial.Serialize(rows);

        }
        catch (Exception ex)
        {
            val = "";
        }
        return val;
    }


    [WebMethod(EnableSession = true)]
    public static string ScheduleInterview(string Interview_data, string InterviewType, string InterviewDate, string InterviewFromTime, string InterviewToTime)
    {
        string val = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    DateTime time_from = DateTime.Parse("2012/12/12 " + InterviewFromTime);
                    DateTime time_to = DateTime.Parse("2012/12/12 " + InterviewToTime);
                    string response = "error";
                    string message = "Error while taking action on interview schedule, please refresh and try again";

                    if (time_to.TimeOfDay > time_from.TimeOfDay)
                    {
                        Interview_data = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(Interview_data), Trabau_Keys.Job_Key);
                        string InterviewId = Interview_data.Split('-')[0];
                        string ScheduleId = Interview_data.Split('-')[1];

                        DataTable dt_ques = null;
                        if (HttpContext.Current.Session["Trabau_InterviewQuestions"] != null)
                        {
                            dt_ques = HttpContext.Current.Session["Trabau_InterviewQuestions"] as DataTable;
                        }

                        if (dt_ques != null || (InterviewType != "OnTrabau" && InterviewType != "Email"))
                        {
                            string status = string.Empty;
                            if (InterviewType == "OnTrabau" && InterviewType == "Email")
                            {
                                if (dt_ques != null)
                                {
                                    if (dt_ques.Rows.Count > 0)
                                    {
                                        status = "ok";
                                    }
                                }

                            }
                            else
                            {
                                status = "ok";
                            }

                            if (status == "ok")
                            {
                                string XML_InterviewQuestions = string.Empty;
                                if (InterviewType == "OnTrabau" || InterviewType == "Email")
                                {
                                    XML_InterviewQuestions = MiscFunctions.GetXMLString(dt_ques, "InterviewQuestions");
                                }
                                Interview obj = new Interview();
                                string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                                DataSet ds_response = obj.ScheduleInterview(Int64.Parse(UserID), Int32.Parse(InterviewId), InterviewDate, InterviewFromTime, InterviewToTime, XML_InterviewQuestions,
                                    InterviewType, Int32.Parse(ScheduleId));


                                if (ds_response != null)
                                {
                                    if (ds_response.Tables.Count > 0)
                                    {
                                        if (ds_response.Tables[0].Rows.Count > 0)
                                        {
                                            response = ds_response.Tables[0].Rows[0]["Response"].ToString();
                                            message = ds_response.Tables[0].Rows[0]["Message"].ToString();
                                            if (ScheduleId == "0")
                                            {
                                                if (response == "success")
                                                {
                                                    try
                                                    {
                                                        string Freelancer_Name = ds_response.Tables[0].Rows[0]["Name"].ToString();
                                                        string JobTitle = ds_response.Tables[0].Rows[0]["JobTitle"].ToString();
                                                        string EmailId = ds_response.Tables[0].Rows[0]["EmailId"].ToString();
                                                        string Interview_Date = ds_response.Tables[0].Rows[0]["InterviewDate"].ToString();
                                                        string ClientName = HttpContext.Current.Session["Trabau_FirstName"].ToString();
                                                        string ClientEmailId = HttpContext.Current.Session["Trabau_EmailId"].ToString();

                                                        string template_url = "https://www.trabau.com/emailers/xxddcca/interview-request-freelancer.html";

                                                        try
                                                        {
                                                            WebRequest req = WebRequest.Create(template_url);
                                                            WebResponse w_res = req.GetResponse();
                                                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                                            string html = sr.ReadToEnd();

                                                            html = html.Replace("@Name", Freelancer_Name);
                                                            html = html.Replace("@JobTitle", JobTitle);
                                                            html = html.Replace("@ClientName", ClientName);

                                                            string body = html;

                                                            Emailer obj_email = new Emailer();
                                                            string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Ready for hire? – Interview request", body, null);

                                                            try
                                                            {
                                                                utility log = new utility();
                                                                log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
                                                            }
                                                            catch (Exception)
                                                            {
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            try
                                                            {
                                                                utility log = new utility();
                                                                log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                                            }
                                                            catch (Exception)
                                                            {
                                                            }
                                                        }


                                                        try
                                                        {
                                                            template_url = "https://www.trabau.com/emailers/xxddcca/interview-request-client.html";

                                                            WebRequest req = WebRequest.Create(template_url);
                                                            WebResponse w_res = req.GetResponse();
                                                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                                            string html = sr.ReadToEnd();

                                                            html = html.Replace("@Name", ClientName);
                                                            html = html.Replace("@Freelancer_Name", Freelancer_Name);

                                                            string body = html;

                                                            Emailer obj_email = new Emailer();
                                                            string _val = obj_email.SendEmail(ClientEmailId, "", "", "Trabau Notification", "Ready to hire? – Interview request", body, null);

                                                            try
                                                            {
                                                                utility log = new utility();
                                                                log.CreateEmailerLog(Convert.ToInt64(UserID), ClientEmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
                                                            }
                                                            catch (Exception)
                                                            {
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            try
                                                            {
                                                                utility log = new utility();
                                                                log.CreateEmailerLog(Convert.ToInt64(UserID), ClientEmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                                            }
                                                            catch (Exception)
                                                            {
                                                            }
                                                        }


                                                        if (InterviewType == "OnTrabau" || InterviewType == "Email")
                                                        {
                                                            string questions_html = "<table style='width:100%' cellspacing='0'>";
                                                            try
                                                            {
                                                                string th = "<tr>";
                                                                th = th + "<th style='border: 1px solid #cecece;padding: 5px;text-align: left;'>SrNo.</th><th style='border: 1px solid #cecece;padding: 5px;text-align: left;'>Question</th>";
                                                                th = th + "</tr>";
                                                                questions_html = questions_html + th;

                                                                for (int i = 0; i < dt_ques.Rows.Count; i++)
                                                                {
                                                                    string tr = "<tr>";
                                                                    tr = tr + "<td style='border: 1px solid #cecece;padding: 5px;'>" + (i + 1).ToString() + "</td><td style='border: 1px solid #cecece;padding: 5px;'>" + dt_ques.Rows[i]["Question_Enc"].ToString() + "</td>";
                                                                    tr = tr + "</tr>";
                                                                    questions_html = questions_html + tr;
                                                                }
                                                            }
                                                            catch (Exception)
                                                            {
                                                            }
                                                            
                                                            questions_html = questions_html + "</table>";
                                                            try
                                                            {
                                                                template_url = "https://www.trabau.com/emailers/xxddcca/interview-questions-freelancer.html";

                                                                WebRequest req = WebRequest.Create(template_url);
                                                                WebResponse w_res = req.GetResponse();
                                                                StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                                                string html = sr.ReadToEnd();

                                                                html = html.Replace("@Name", Freelancer_Name);
                                                                html = html.Replace("@JobTitle", JobTitle);
                                                                html = html.Replace("@Questions", questions_html);

                                                                string body = html;

                                                                Emailer obj_email = new Emailer();
                                                                string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "You’ve been asked – Interview questions generated", body, null);

                                                                try
                                                                {
                                                                    utility log = new utility();
                                                                    log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
                                                                }
                                                                catch (Exception)
                                                                {
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                try
                                                                {
                                                                    utility log = new utility();
                                                                    log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                                                }
                                                                catch (Exception)
                                                                {
                                                                }
                                                            }




                                                            try
                                                            {
                                                                template_url = "https://www.trabau.com/emailers/xxddcca/interview-questions-client.html";

                                                                WebRequest req = WebRequest.Create(template_url);
                                                                WebResponse w_res = req.GetResponse();
                                                                StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                                                string html = sr.ReadToEnd();

                                                                html = html.Replace("@Name", ClientName);
                                                                html = html.Replace("@JobTitle", JobTitle);
                                                                html = html.Replace("@Questions", questions_html);

                                                                string body = html;

                                                                Emailer obj_email = new Emailer();
                                                                string _val = obj_email.SendEmail(ClientEmailId, "", "", "Trabau Notification", "You asked? – Interview questions generated", body, null);

                                                                try
                                                                {
                                                                    utility log = new utility();
                                                                    log.CreateEmailerLog(Convert.ToInt64(UserID), ClientEmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
                                                                }
                                                                catch (Exception)
                                                                {
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                try
                                                                {
                                                                    utility log = new utility();
                                                                    log.CreateEmailerLog(Convert.ToInt64(UserID), ClientEmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                                                }
                                                                catch (Exception)
                                                                {
                                                                }
                                                            }

                                                        }

                                                    }
                                                    catch (Exception)
                                                    {
                                                    }
                                                    //try
                                                    //{
                                                    //    Emailer obj_email = new Emailer();
                                                    //    string body = "Dear " + Freelancer_Name + ",<br/><br/>You have been selected for interview for job " + JobTitle + " on " + Interview_Date + " between " + InterviewFromTime + " and " + InterviewToTime + ", please reach the client for specific date and time";
                                                    //    if (InterviewType == "Email")
                                                    //    {
                                                    //        string questions_html = "<br/><br/>Answer the following questions<br/>";
                                                    //        questions_html = questions_html + "<table style='width:100%'>";
                                                    //        for (int i = 0; i < dt_ques.Rows.Count; i++)
                                                    //        {
                                                    //            string tr = "<tr>";
                                                    //            tr = tr + "<td>" + (i + 1).ToString() + "</td><td>" + dt_ques.Rows[i]["Question_Enc"].ToString() + "</td>";
                                                    //            tr = tr + "</tr>";
                                                    //            questions_html = questions_html + tr;
                                                    //        }
                                                    //        questions_html = questions_html + "</table>";

                                                    //        body = body + questions_html;

                                                    //    }
                                                    //    obj_email.SendEmail(EmailId, "", "", "Trabau", "Interview Scheduled - Trabau", body, null);

                                                    //}
                                                    //catch (Exception)
                                                    //{

                                                    //}
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                response = "error";
                                message = "Please add questions for the interview and try again";
                            }
                        }
                        else
                        {
                            response = "error";
                            message = "Please add questions for the interview and try again";
                        }
                    }
                    else
                    {
                        response = "error";
                        message = "To Time should be greater than From Time, please check and try again";
                    }

                    detail.Add("action_response", response);
                    detail.Add("action_message", message);

                    detail.Add("response", "ok");

                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("action_response", "");
                    detail.Add("action_message", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("action_response", "");
                detail.Add("action_message", "");
            }

            details.Add(detail);


            JavaScriptSerializer serial = new JavaScriptSerializer();
            val = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return val;
    }



    [WebMethod(EnableSession = true)]
    public static string Cancel_ScheduledInterview(string Interview_data)
    {
        string val = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    string response = "error";
                    string message = "Error while taking action on interview schedule, please refresh and try again";

                    Interview_data = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(Interview_data), Trabau_Keys.Job_Key);
                    string InterviewId = Interview_data.Split('-')[0];
                    string ScheduleId = Interview_data.Split('-')[1];

                    Interview obj = new Interview();
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                    DataSet ds_response = obj.Cancel_ScheduledInterview(Int64.Parse(UserID), Int32.Parse(ScheduleId));


                    if (ds_response != null)
                    {
                        if (ds_response.Tables.Count > 0)
                        {
                            if (ds_response.Tables[0].Rows.Count > 0)
                            {
                                response = ds_response.Tables[0].Rows[0]["Response"].ToString();
                                message = ds_response.Tables[0].Rows[0]["Message"].ToString();
                                if (ScheduleId == "0")
                                {
                                    string Freelancer_Name = ds_response.Tables[0].Rows[0]["Name"].ToString();
                                    string JobTitle = ds_response.Tables[0].Rows[0]["JobTitle"].ToString();
                                    string EmailId = ds_response.Tables[0].Rows[0]["EmailId"].ToString();
                                    string Interview_Date = ds_response.Tables[0].Rows[0]["InterviewDate"].ToString();

                                    if (response == "success")
                                    {
                                        try
                                        {
                                            Emailer obj_email = new Emailer();
                                            string body = "Dear " + Freelancer_Name + ",<br/><br/>Interview scheduled for job " + JobTitle + " on " + Interview_Date + " has been cancelled by Client";

                                            obj_email.SendEmail(EmailId, "", "", "Trabau", "Interview Scheduled Cancelled - Trabau", body, null);

                                        }
                                        catch (Exception)
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }

                    detail.Add("action_response", response);
                    detail.Add("action_message", message);

                    detail.Add("response", "ok");

                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("action_response", "");
                    detail.Add("action_message", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("action_response", "");
                detail.Add("action_message", "");
            }

            details.Add(detail);


            JavaScriptSerializer serial = new JavaScriptSerializer();
            val = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return val;
    }


    [WebMethod(EnableSession = true)]
    public static string CheckInterviewType(string InterviewType)
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    string questions_html = "";
                    detail.Add("response", "ok");
                    if (InterviewType != string.Empty && InterviewType != "undefined" && InterviewType != "0")
                    {
                        if (InterviewType == "OnTrabau" || InterviewType == "Email")
                        {
                            questions_html = "<div class='col-sm-9'>@AddQuestion<div class='col-sm-12'> @Ques_Header <ol id='ol_questions'>@Questions</ol> </div>";
                            string AddQuestion = string.Empty;
                            string ul = "";
                            try
                            {
                                DataTable dt_ques = new DataTable();
                                if (HttpContext.Current.Session["Trabau_InterviewQuestions"] != null)
                                {
                                    dt_ques = HttpContext.Current.Session["Trabau_InterviewQuestions"] as DataTable;


                                    bool InterviewStarted = false;
                                    try
                                    {
                                        if (HttpContext.Current.Session["Trabau_Interview_Started"] != null)
                                        {
                                            InterviewStarted = Convert.ToBoolean(HttpContext.Current.Session["Trabau_Interview_Started"].ToString());
                                        }
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    if (!InterviewStarted)
                                    {
                                        AddQuestion = "<input id='txtNewQuestion' class='form-control textEditor' placeholder='Enter Question' /> </div> <div class='col-sm-3' id='div_add_ques'> <input type='button' value='Add Question' class='cta-btn-sm btn-add-question' /> </div>";
                                    }
                                    for (int i = 0; i < dt_ques.Rows.Count; i++)
                                    {
                                        string Question = dt_ques.Rows[i]["Question"].ToString();
                                        string Question_Key = dt_ques.Rows[i]["Ques_Key"].ToString();
                                        string answer = string.Empty;
                                        if (InterviewStarted)
                                        {
                                            answer = dt_ques.Rows[i]["Answer"].ToString();
                                            if (answer != string.Empty)
                                            {
                                                answer = "</br><b>Freelancer Answer: </b>" + answer;
                                            }
                                        }
                                        string li = "<li class='interview-ques' data='" + Question_Key + "'>" + "<ques class='ques-content'>" + Question + "</ques>" + (InterviewStarted == false ? "<input type='button' value='Remove' class='btn-remove-question' />" : "") + answer + "</li>";
                                        ul = ul + li;
                                    }
                                }

                            }
                            catch (Exception)
                            {
                            }
                            questions_html = questions_html.Replace("@Questions", ul);
                            questions_html = questions_html.Replace("@Ques_Header", (ul != string.Empty ? "<h4 id='h4_ques_header'>Questions</h4>" : ""));
                            questions_html = questions_html.Replace("@AddQuestion", AddQuestion);

                        }
                    }



                    detail.Add("questions_html", questions_html);
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("questions_html", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("questions_html", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }



    [WebMethod(EnableSession = true)]
    public static string Update_InterviewResponse(string Interview_data, string ResponseId)
    {
        string val = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    string response = "error";
                    string message = "Error while taking action on interview, please refresh and try again";

                    string ScheduleId = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(Interview_data), Trabau_Keys.Job_Key);
                    string IPAddress = HttpContext.Current.Request.UserHostAddress;

                    Interview obj = new Interview();
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                    DataSet ds_response = obj.SaveInterviewResponse(Int32.Parse(ScheduleId), Int64.Parse(UserID), Int32.Parse(ResponseId), IPAddress);


                    if (ds_response != null)
                    {
                        if (ds_response.Tables.Count > 0)
                        {
                            if (ds_response.Tables[0].Rows.Count > 0)
                            {
                                response = ds_response.Tables[0].Rows[0]["Response"].ToString();
                                message = ds_response.Tables[0].Rows[0]["Message"].ToString();
                                if (ScheduleId == "0")
                                {
                                    //string Freelancer_Name = ds_response.Tables[0].Rows[0]["Name"].ToString();
                                    //string JobTitle = ds_response.Tables[0].Rows[0]["JobTitle"].ToString();
                                    //string EmailId = ds_response.Tables[0].Rows[0]["EmailId"].ToString();
                                    //string Interview_Date = ds_response.Tables[0].Rows[0]["InterviewDate"].ToString();

                                    //if (response == "success")
                                    //{
                                    //    try
                                    //    {
                                    //        Emailer obj_email = new Emailer();
                                    //        string body = "Dear " + Freelancer_Name + ",<br/><br/>Interview scheduled for job " + JobTitle + " on " + Interview_Date + " has been cancelled by Client";

                                    //        obj_email.SendEmail(EmailId, "", "", "Trabau", "Interview Scheduled Cancelled - Trabau", body, null);

                                    //    }
                                    //    catch (Exception)
                                    //    {

                                    //    }
                                    //}
                                }
                            }
                        }
                    }

                    detail.Add("action_response", response);
                    detail.Add("action_message", message);

                    detail.Add("response", "ok");

                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("action_response", "");
                    detail.Add("action_message", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("action_response", "");
                detail.Add("action_message", "");
            }

            details.Add(detail);


            JavaScriptSerializer serial = new JavaScriptSerializer();
            val = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return val;
    }


    [WebMethod(EnableSession = true)]
    public static string GetInterview_Report(string data)
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    var pageHolder = new Page();
                    var form = new HtmlForm();
                    var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/Interview/UserControls/wucInterviewReport.ascx");
                    try
                    {
                        (viewControl.FindControl("hfInterview_data") as HiddenField).Value = data;
                    }
                    catch (Exception)
                    {
                    }
                    var scriptManager = new ScriptManager();
                    form.Controls.Add(scriptManager);
                    form.Controls.Add(viewControl);
                    pageHolder.Controls.Add(form);
                    HttpContext.Current.Server.Execute(pageHolder, output, false);


                    detail.Add("response", "ok");
                    detail.Add("Interview_html", output.ToString().Replace("\r\n", "").ToString());
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("Interview_html", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("Interview_html", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }


    [WebMethod(EnableSession = true)]
    public static string Interview_RequestAction(string InterviewData, string ActionType)
    {
        string val = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();

            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "H")
                {
                    string data = InterviewData;
                    data = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(data), Trabau_Keys.Job_Key);
                    string _ProposalId = data.Split('-')[0];
                    string JobId = data.Split('-')[1];
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                    string IPAddress = HttpContext.Current.Request.UserHostAddress;

                    jobposting obj = new jobposting();
                    DataSet ds_response = obj.RejectInterview_Request(Int64.Parse(UserID), Int32.Parse(_ProposalId), ActionType);
                    string response = "error";
                    string message = "Error while taking action on interview list, please refresh and try again";
                    if (ds_response != null)
                    {
                        if (ds_response.Tables.Count > 0)
                        {
                            if (ds_response.Tables[0].Rows.Count > 0)
                            {
                                response = ds_response.Tables[0].Rows[0]["Response"].ToString();
                                message = ds_response.Tables[0].Rows[0]["Message"].ToString();

                                if (response == "success")
                                {
                                    try
                                    {
                                        string Freelancer_Name = ds_response.Tables[0].Rows[0]["Freelancer_Name"].ToString();
                                        string JobTitle = ds_response.Tables[0].Rows[0]["JobTitle"].ToString();
                                        string EmailId = ds_response.Tables[0].Rows[0]["Freelancer_EmailId"].ToString();
                                        string ClientName = HttpContext.Current.Session["Trabau_FirstName"].ToString();
                                        string ClientEmailId = HttpContext.Current.Session["Trabau_EmailId"].ToString();
                                        string Freelancer_UserId = ds_response.Tables[0].Rows[0]["Freelancer_UserId"].ToString();

                                        string template_url = string.Empty;

                                        try
                                        {
                                            if (ActionType == "R")
                                            {
                                                template_url = "https://www.trabau.com/emailers/xxddcca/interview-rejected-by-client-to-freelancer.html";
                                            }
                                            WebRequest req = WebRequest.Create(template_url);
                                            WebResponse w_res = req.GetResponse();
                                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                            string html = sr.ReadToEnd();

                                            html = html.Replace("@Name", Freelancer_Name);
                                            html = html.Replace("@JobTitle", JobTitle);
                                            html = html.Replace("@ClientName", ClientName);

                                            string body = html;

                                            Emailer obj_email = new Emailer();
                                            string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Maybe Next Time – Interview Rejected", body, null);

                                            try
                                            {
                                                utility log = new utility();
                                                log.CreateEmailerLog(Convert.ToInt64(Freelancer_UserId), EmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            try
                                            {
                                                utility log = new utility();
                                                log.CreateEmailerLog(Convert.ToInt64(Freelancer_UserId), EmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }


                                        try
                                        {
                                            if (ActionType == "R")
                                            {
                                                template_url = "https://www.trabau.com/emailers/xxddcca/interview-rejected-by-client-to-client.html";
                                            }


                                            WebRequest req = WebRequest.Create(template_url);
                                            WebResponse w_res = req.GetResponse();
                                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                            string html = sr.ReadToEnd();

                                            html = html.Replace("@Name", ClientName);
                                            html = html.Replace("@Freelancer_Name", Freelancer_Name);
                                            html = html.Replace("@JobTitle", JobTitle);

                                            string body = html;

                                            Emailer obj_email = new Emailer();
                                            string _val = obj_email.SendEmail(ClientEmailId, "", "", "Trabau Notification", "Maybe Next Time? – Interview Rejected", body, null);

                                            try
                                            {
                                                utility log = new utility();
                                                log.CreateEmailerLog(Convert.ToInt64(UserID), ClientEmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            try
                                            {
                                                utility log = new utility();
                                                log.CreateEmailerLog(Convert.ToInt64(UserID), ClientEmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                        }
                    }


                    detail.Add("action_response", response);
                    detail.Add("action_message", message);

                    detail.Add("response", "ok");
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("action_response", "");
                    detail.Add("action_message", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("action_response", "");
                detail.Add("action_message", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            val = serial.Serialize(details);

        }
        catch (Exception ex)
        {
            val = "";
        }
        return val;
    }
}