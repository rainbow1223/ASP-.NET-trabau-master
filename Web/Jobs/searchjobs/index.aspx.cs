using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_searchjobs_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["query"] != null)
                {
                    string search_query = Request.QueryString["query"];
                    txtSearchJobs.Text = search_query;
                }
            }
        }
    }



    [WebMethod(EnableSession = true)]
    public static string SearchJobs(string page_number, string category, string text, string type, string JobType, string ExpLevel,
        string ClientHistory, string NoOfProposals, string Budget, string HoursPerWeek, string ProjectLength, string savedjobs)
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            string jobs_found = "";
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                var pageHolder = new Page();
                var form = new HtmlForm();
                var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/SearchJobs/UserControls/wucSearchJobs.ascx");
                try
                {
                    (viewControl.FindControl("lblPageNumber_Request") as Label).Text = page_number;
                    (viewControl.FindControl("lblSearchText_Request") as Label).Text = text;
                    (viewControl.FindControl("lblSearchType_Request") as Label).Text = type;
                    (viewControl.FindControl("lblSavedJobs_Request") as Label).Text = savedjobs;


                    (viewControl.FindControl("lblJobType") as Label).Text = JobType;
                    (viewControl.FindControl("lblExpLevel") as Label).Text = ExpLevel;
                    (viewControl.FindControl("lblClientHistory") as Label).Text = ClientHistory;
                    (viewControl.FindControl("lblNoOfProposals") as Label).Text = NoOfProposals;
                    (viewControl.FindControl("lblBudget") as Label).Text = Budget;
                    (viewControl.FindControl("lblHoursPerWeek") as Label).Text = HoursPerWeek;
                    (viewControl.FindControl("lblProjectLength") as Label).Text = ProjectLength;
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
                detail.Add("jobs_html", output.ToString().Replace("\r\n", "").ToString());
                if (HttpContext.Current.Session["JobsResultFound"] != null)
                {
                    jobs_found = (HttpContext.Current.Session["JobsResultFound"].ToString() == "1" ? "1" : "0");
                }
                HttpContext.Current.Session["JobsResultFound"] = null;


            }
            else
            {
                detail.Add("response", "");
                detail.Add("jobs_html", "");
                jobs_found = "0";
            }
            detail.Add("jobs_found", jobs_found);

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
    public static string GetJobDetails(string JobId)
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                var pageHolder = new Page();
                var form = new HtmlForm();
                var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/SearchJobs/UserControls/wucJobDetails.ascx");
                try
                {
                    (viewControl.FindControl("hfJobId") as HiddenField).Value = JobId;
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
                detail.Add("jobdetails_html", output.ToString().Replace("\r\n", "").ToString());

            }
            else
            {
                detail.Add("response", "");
                detail.Add("jobdetails_html", "");
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
    public static string GetJobAdditionalFiles(string JobId)
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                var pageHolder = new Page();
                var form = new HtmlForm();
                var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/SearchJobs/UserControls/wucJobAdditionalFiles.ascx");
                try
                {
                    (viewControl.FindControl("hfAdditionalFiles_JobId") as HiddenField).Value = JobId;
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
                detail.Add("jobAF_html", output.ToString().Replace("\r\n", "").ToString());

            }
            else
            {
                detail.Add("response", "");
                detail.Add("jobAF_html", "");
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
    public static string DownloadAdditionalFile(string URL)
    {
        string response = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                HttpContext.Current.Session["download_file_key"] = URL;

                string Download_URL = "../../download.ashx?path=" + RandomString(20).ToLower();

                detail.Add("response", "ok");
                detail.Add("DownloadURL", Download_URL);

            }
            else
            {
                detail.Add("response", "");
                detail.Add("DownloadURL", "");
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

    public static string RandomString(int length)
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }



    [WebMethod(EnableSession = true)]
    public static string SaveJob(string JobId)
    {
        string response = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "W")
                {
                    searchjob obj = new searchjob();
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                    JobId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(JobId), Trabau_Keys.Job_Key);
                    string data = obj.SaveJob(Int64.Parse(UserID), Int32.Parse(JobId));
                    //string data = "success:Saved";

                    string _response = data.Split(':')[0];
                    string _message = data.Split(':')[1];

                    detail.Add("response", "ok");
                    detail.Add("action_response", _response);
                    detail.Add("action_message", _message);
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
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }



    [WebMethod(EnableSession = true)]
    public static string GetUserCategories()
    {
        string response = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                searchjob obj = new searchjob();
                string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                DataSet ds_cat = obj.GetUserCategories(Int64.Parse(UserID));
                string cat = "";
                for (int i = 0; i < ds_cat.Tables[0].Rows.Count; i++)
                {
                    string cat_id = ds_cat.Tables[0].Rows[i]["Value"].ToString();
                    cat_id = EncyptSalt.EncryptString(EncyptSalt.Base64Encode(cat_id), Trabau_Keys.Category_Key);
                    string cat_text = ds_cat.Tables[0].Rows[i]["Text"].ToString();
                    cat = cat + "<li id='" + cat_id + "' onclick='FilterJobs(this);'><a>" + cat_text + "</a></li>";
                }
                detail.Add("response", "ok");
                detail.Add("categories", cat);

            }
            else
            {
                detail.Add("response", "");
                detail.Add("categories", "");
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
    public static string GetAdvanceControlFilters()
    {
        string response = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                searchjob obj = new searchjob();
                string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                DataSet ds_filters = obj.GetAdvanceControlFilters(Int64.Parse(UserID));
                DataView dv_filters = ds_filters.Tables[0].DefaultView;
                DataTable distinct_filters = dv_filters.ToTable(true, "FilterType");
                for (int i = 0; i < distinct_filters.Rows.Count; i++)
                {
                    string FilterType = distinct_filters.Rows[i]["FilterType"].ToString();
                    DataView dv_filter = dv_filters;
                    dv_filter.RowFilter = "FilterType='" + FilterType + "'";
                    string options = "";
                    for (int j = 0; j < dv_filter.Count; j++)
                    {
                        string Text = dv_filter.ToTable().Rows[j]["Text"].ToString();
                        string Value = dv_filter.ToTable().Rows[j]["Value"].ToString();
                        Value = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(Value, Trabau_Keys.Filter_Key));
                        options = options + "<tr><td><input type='checkbox' value='" + Value + "' /><label>" + Text + "</label></td></tr>";
                    }

                    detail.Add(FilterType, options);
                }
                detail.Add("response", "ok");

            }
            else
            {
                detail.Add("response", "");

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
    public static string GetRightNavDetails()
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                var pageHolder = new Page();
                var form = new HtmlForm();
                var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/SearchJobs/UserControls/wucSearchJobRightSide.ascx");

                var scriptManager = new ScriptManager();
                form.Controls.Add(scriptManager);
                form.Controls.Add(viewControl);
                pageHolder.Controls.Add(form);
                HttpContext.Current.Server.Execute(pageHolder, output, false);


                detail.Add("response", "ok");
                detail.Add("navigation_html", output.ToString().Replace("\r\n", "").ToString());

            }
            else
            {
                detail.Add("response", "");
                detail.Add("navigation_html", "");
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
    public static string SendJobToFriend(string Name, string EmailAddress, string data, string userid)
    {
        string val = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();

            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string JobId = data;
                JobId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(JobId), Trabau_Keys.Job_Key);
                try
                {
                    userid = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(userid), Trabau_Keys.Profile_Key);
                }
                catch (Exception)
                {
                    userid = "0";
                }

                if (userid == string.Empty || userid == "undefined")
                {
                    userid = "0";
                }


                string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                string IPAddress = HttpContext.Current.Request.UserHostAddress;

                jobposting obj = new jobposting();
                DataSet ds_response = obj.SendJobToFriend(Int64.Parse(UserID), Int32.Parse(JobId), EmailAddress, Name, IPAddress, Int64.Parse(userid));
                string response = "error";
                string message = "Error while sending job to " + EmailAddress + ", please refresh and try again";
                if (ds_response != null)
                {
                    if (ds_response.Tables.Count > 0)
                    {
                        if (ds_response.Tables[0].Rows.Count > 0)
                        {
                            response = ds_response.Tables[0].Rows[0]["Response"].ToString();
                            message = ds_response.Tables[0].Rows[0]["Message"].ToString();
                            string Freelancer_Name = ds_response.Tables[0].Rows[0]["Freelancer_Name"].ToString();
                            string JobTitle = ds_response.Tables[0].Rows[0]["JobTitle"].ToString();
                            string Freelancer_EmailId = EmailAddress;
                            string Freelancer_UserId = ds_response.Tables[0].Rows[0]["Freelancer_UserId"].ToString();
                            string ClientName = HttpContext.Current.Session["Trabau_FirstName"].ToString();
                            if (response == "success")
                            {
                                string template_url = "https://www.trabau.com/emailers/xxddcca/send-job-to-friend-freelancer.html";

                                if (Freelancer_EmailId != string.Empty)
                                {
                                    try
                                    {
                                        WebRequest req = WebRequest.Create(template_url);
                                        WebResponse w_res = req.GetResponse();
                                        StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                        string html = sr.ReadToEnd();

                                        JobId = EncyptSalt.EncryptString(EncyptSalt.Base64Encode(JobId), Trabau_Keys.Job_Key);
                                        string job_link = "https://www.trabau.com/jobs/searchjobs/apply.aspx?job=" + JobId;
                                        html = html.Replace("@Name", Freelancer_Name);
                                        html = html.Replace("@JobLink", job_link);
                                        html = html.Replace("@JobTitle", JobTitle);

                                        string body = html;

                                        Emailer obj_email = new Emailer();
                                        string _val = obj_email.SendEmail(Freelancer_EmailId, "", "", "Trabau Notification", "Got someone for a gig?", body, null);

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

                details.Add(detail);

                JavaScriptSerializer serial = new JavaScriptSerializer();
                val = serial.Serialize(details);

            }
        }
        catch (Exception ex)
        {
            val = "";
        }
        return val;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string[] GetUsers(string Prefix)
    {
        try
        {
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                searchjob obj = new searchjob();
                return obj.GetUsersList(Prefix, Int64.Parse(UserID)).ToArray();
            }
        }
        catch (Exception)
        {
            return null;
        }
        return null;
    }
}