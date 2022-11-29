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

public partial class Jobs_interview_myinterviews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "W")
                {
                    var pageHolder = new Page();
                    var form = new HtmlForm();
                    var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/interview/UserControls/wucMyInterviews.ascx");
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
    public static string InterviewSchedule_Action(string Interviewdata, string action_type, string contact_number)
    {
        string val = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();

            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "W")
                {

                    Interviewdata = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(Interviewdata), Trabau_Keys.Job_Key);
                    string ScheduleId = Interviewdata;

                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                    string IPAddress = HttpContext.Current.Request.UserHostAddress;

                    Interview obj = new Interview();
                    DataSet ds_response = obj.Freelancer_Interview_Action(Int64.Parse(UserID), Int32.Parse(ScheduleId), action_type, contact_number);
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
                                        string ClientName = ds_response.Tables[0].Rows[0]["ClientName"].ToString();
                                        string JobTitle = ds_response.Tables[0].Rows[0]["JobTitle"].ToString();
                                        string ClientEmailId = ds_response.Tables[0].Rows[0]["ClientEmailId"].ToString();
                                        string Freelancer_Name = HttpContext.Current.Session["Trabau_FirstName"].ToString();
                                        string Freelancer_EmailId = HttpContext.Current.Session["Trabau_EmailId"].ToString();
                                        string Freelancer_UserId = UserID;


                                        string template_url = string.Empty;
                                        if (action_type == "A")
                                        {
                                            template_url = "https://www.trabau.com/emailers/xxddcca/interview-accepted-freelancer.html";
                                        }
                                        else
                                        {
                                            template_url = "https://www.trabau.com/emailers/xxddcca/interview-rejected-by-freelancer-to-freelancer.html";
                                        }
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
                                            string _val = obj_email.SendEmail(Freelancer_EmailId, "", "", "Trabau Notification",
                                                (action_type == "A" ? "One step closer – Interview accepted!" : "Maybe Next Time – Interview Rejected"), body, null);

                                            try
                                            {
                                                utility log = new utility();
                                                log.CreateEmailerLog(Convert.ToInt64(Freelancer_UserId), Freelancer_EmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
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
                                                log.CreateEmailerLog(Convert.ToInt64(Freelancer_UserId), Freelancer_EmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }



                                        string ClientUserId = UserID;
                                        try
                                        {
                                            if (action_type == "A")
                                            {
                                                template_url = "https://www.trabau.com/emailers/xxddcca/interview-accepted-client.html";
                                            }
                                            else
                                            {
                                                template_url = "https://www.trabau.com/emailers/xxddcca/interview-rejected-by-freelancer-to-client.html";
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
                                            string _val = obj_email.SendEmail(ClientEmailId, "", "", "Trabau Notification",
                                                (action_type == "A" ? "One step closer – Interview accepted!" : "Maybe Next Time – Interview Rejected"), body, null);

                                            try
                                            {
                                                utility log = new utility();
                                                log.CreateEmailerLog(Convert.ToInt64(ClientUserId), ClientEmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
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
                                                log.CreateEmailerLog(Convert.ToInt64(ClientUserId), ClientEmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
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
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "W")
                {
                    var pageHolder = new Page();
                    var form = new HtmlForm();
                    var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/interview/UserControls/wucMyInterviewDetails.ascx");
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
    public static string UpdateInterviewSchedule(string Interviewdata, string InterviewDate, string InterviewFromTime, string InterviewToTime)
    {
        string val = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();

            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "W")
                {
                    string response = "error";
                    string message = "Error while taking action on interview schedule, please refresh and try again";

                    DateTime time_from = DateTime.Parse("2012/12/12 " + InterviewFromTime);
                    DateTime time_to = DateTime.Parse("2012/12/12 " + InterviewToTime);

                    if (time_to.TimeOfDay > time_from.TimeOfDay)
                    {
                        Interviewdata = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(Interviewdata), Trabau_Keys.Job_Key);
                        string ScheduleId = Interviewdata;

                        string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                        string IPAddress = HttpContext.Current.Request.UserHostAddress;

                        Interview obj = new Interview();
                        DataSet ds_response = obj.UpdateInterviewSchedule(Int64.Parse(UserID), Int32.Parse(ScheduleId), InterviewDate, InterviewFromTime, InterviewToTime);

                        if (ds_response != null)
                        {
                            if (ds_response.Tables.Count > 0)
                            {
                                if (ds_response.Tables[0].Rows.Count > 0)
                                {
                                    response = ds_response.Tables[0].Rows[0]["Response"].ToString();
                                    message = ds_response.Tables[0].Rows[0]["Message"].ToString();
                                    string ClientName = ds_response.Tables[0].Rows[0]["ClientName"].ToString();
                                    string JobTitle = ds_response.Tables[0].Rows[0]["JobTitle"].ToString();
                                    string ClientEmailId = ds_response.Tables[0].Rows[0]["ClientEmailId"].ToString();
                                    string Freelancer_Name = HttpContext.Current.Session["Trabau_FirstName"].ToString();
                                    string Freelancer_EmailId = ds_response.Tables[0].Rows[0]["Freelancer_EmailId"].ToString();
                                    string Freelancer_UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
                                    string OldInt_ScheduleDetails = ds_response.Tables[0].Rows[0]["OldInt_ScheduleDetails"].ToString();
                                    string Schedule_Details = Convert.ToDateTime(InterviewDate).ToString("dd MMM yyyy") + " between " + InterviewFromTime + " and " + InterviewToTime;
                                    if (response == "success")
                                    {
                                        if (OldInt_ScheduleDetails != string.Empty && Freelancer_EmailId != string.Empty)
                                        {
                                            string template_url = "https://www.trabau.com/emailers/xxddcca/interview-schedule-datechange-freelancer.html";
                                            try
                                            {
                                                WebRequest req = WebRequest.Create(template_url);
                                                WebResponse w_res = req.GetResponse();
                                                StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                                string html = sr.ReadToEnd();

                                                html = html.Replace("@Name", Freelancer_Name);
                                                html = html.Replace("@ClientName", ClientName);
                                                html = html.Replace("@OldInt_ScheduleDate", OldInt_ScheduleDetails);
                                                html = html.Replace("@Schedule_Details", Schedule_Details);

                                                string body = html;

                                                Emailer obj_email = new Emailer();
                                                string _val = obj_email.SendEmail(Freelancer_EmailId, "", "", "Trabau Notification", "Request – Interview date change", body, null);

                                                try
                                                {
                                                    utility log = new utility();
                                                    log.CreateEmailerLog(Convert.ToInt64(Freelancer_UserId), Freelancer_EmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
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
                                                    log.CreateEmailerLog(Convert.ToInt64(Freelancer_UserId), Freelancer_EmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                                }
                                                catch (Exception)
                                                {
                                                }
                                            }



                                            string ClientUserId = UserID;
                                            try
                                            {



                                                template_url = "https://www.trabau.com/emailers/xxddcca/interview-schedule-datechange-client.html";

                                                WebRequest req = WebRequest.Create(template_url);
                                                WebResponse w_res = req.GetResponse();
                                                StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                                string html = sr.ReadToEnd();

                                                html = html.Replace("@Name", ClientName);
                                                html = html.Replace("@Freelancer_Name", Freelancer_Name);
                                                html = html.Replace("@OldInt_ScheduleDate", OldInt_ScheduleDetails);
                                                html = html.Replace("@NewInt_ScheduleDate", Schedule_Details);
                                                html = html.Replace("@Schedule_Details", Schedule_Details);

                                                string body = html;

                                                Emailer obj_email = new Emailer();
                                                string _val = obj_email.SendEmail(ClientEmailId, "", "", "Trabau Notification", "Request – Interview date change", body, null);

                                                try
                                                {
                                                    utility log = new utility();
                                                    log.CreateEmailerLog(Convert.ToInt64(ClientUserId), ClientEmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
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
                                                    log.CreateEmailerLog(Convert.ToInt64(ClientUserId), ClientEmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                                }
                                                catch (Exception)
                                                {
                                                }
                                            }
                                        }

                                    }
                                }
                            }
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
            val = "";
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
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "W")
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
}