using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_searchjobs_apply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["JobPost_Project_Files"] = null;
            Session["ApplyPost_Project_Files"] = null;
            GetJobDetails();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();}, 0);", true);
        }
    }

    public void GetJobDetails()
    {
        try
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["job"] != null)
                {
                    string JobId = Request.QueryString["job"];
                    if (JobId != string.Empty)
                    {
                        string _JobId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(JobId), Trabau_Keys.Job_Key);
                        searchjob obj = new searchjob();
                        string UserID = Session["Trabau_UserId"].ToString();
                        DataSet ds = obj.GetJobDetails(Int64.Parse(UserID), Int32.Parse(_JobId));
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    string ProposalStatus = ds.Tables[0].Rows[0]["ProposalStatus"].ToString();
                                    if (ProposalStatus != string.Empty)
                                    {
                                        btnAlreadySubmitted.Visible = true;
                                        btnAlreadySubmitted.Text = ProposalStatus;
                                        btnAlreadySubmitted.CssClass = "cta-btn-md btn-disabled";
                                    }
                                    else
                                    {
                                        btnSubmitProposal.Visible = true;
                                    }
                                    string Title = ds.Tables[0].Rows[0]["JobTitle"].ToString();
                                    string JobCategory = ds.Tables[0].Rows[0]["JobCategory"].ToString();
                                    string JobCategoryText = ds.Tables[0].Rows[0]["JobCategoryText"].ToString();
                                    string Description = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                                    string PostedOn = ds.Tables[0].Rows[0]["PostedOn"].ToString();
                                    string JobNumberOfPeople = ds.Tables[0].Rows[0]["JobNumberOfPeople"].ToString();
                                    string JobBudgetTypeText = ds.Tables[0].Rows[0]["JobBudgetTypeText"].ToString();
                                    string JobBudgetValue = ds.Tables[0].Rows[0]["JobBudgetValue"].ToString();
                                    string JobLevelOfExperience = ds.Tables[0].Rows[0]["JobLevelOfExperience"].ToString();
                                    string JobExperienceInfo = ds.Tables[0].Rows[0]["JobExperienceInfo"].ToString();


                                    //string ProposalsCount = ds.Tables[0].Rows[0]["ProposalsCount"].ToString();
                                    //string InterviewingCount = ds.Tables[0].Rows[0]["InterviewingCount"].ToString();
                                    //string InvitesCount = ds.Tables[0].Rows[0]["InvitesCount"].ToString();
                                    //string UnanswererdInvitesCount = ds.Tables[0].Rows[0]["UnanswererdInvitesCount"].ToString();

                                    //string JobLocationInfo = ds.Tables[0].Rows[0]["JobLocationInfo"].ToString();
                                    string JobPaymentType = ds.Tables[0].Rows[0]["JobPaymentTypeText"].ToString();
                                    //string JobTypeText = ds.Tables[0].Rows[0]["JobTypeText"].ToString();

                                    //string ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
                                    //string ClientCityName = ds.Tables[0].Rows[0]["ClientCityName"].ToString();
                                    //string ClientCountryName = ds.Tables[0].Rows[0]["ClientCountryName"].ToString();
                                    //string ClientRegDate = ds.Tables[0].Rows[0]["ClientRegDate"].ToString();
                                    //string Client_TotalJobsPosted = ds.Tables[0].Rows[0]["TotalJobsPosted"].ToString();
                                    //string Client_TotalJobsOpen = ds.Tables[0].Rows[0]["TotalJobsOpen"].ToString();
                                    //string ClientHireRate = ds.Tables[0].Rows[0]["ClientHireRate"].ToString();
                                    string HourlyRate = ds.Tables[0].Rows[0]["HourlyRate"].ToString();

                                    ltrlHourlyRate.Text = "$" + HourlyRate + " /hr";
                                    ltrlJobTitle.Text = Title;
                                    ltrlJobDescription.Text = Description;
                                    ltrlJobPostedOn.Text = PostedOn;
                                    ltrlJobCategory.Text = JobCategoryText;
                                    ltrlExperienceLevel.Text = JobLevelOfExperience;
                                    ltrlPaymentType.Text = JobBudgetTypeText;
                                    ltrlBudgetValue.Text = "$" + JobBudgetValue + (JobBudgetTypeText == "Fixed Price" ? "" : " /hr");

                                    div_FixedPrice_terms.Visible = (JobBudgetTypeText == "Fixed Price" ? true : false);
                                    div_hourly_terms.Visible = (JobBudgetTypeText != "Fixed Price" ? true : false);

                                    double FixedPrice_Fee = double.Parse(JobBudgetValue) * 0.1;
                                    txtBid_FixedPrice.Text = JobBudgetValue;
                                    lblTrabauServiceFee_FixedPrice.Text = FixedPrice_Fee.ToString();
                                    txtReceive_FixedPrice.Text = (double.Parse(JobBudgetValue) - FixedPrice_Fee).ToString();

                                    double Hourly_Fee = double.Parse(HourlyRate) * 0.1;
                                    txtBid_HourlyRate.Text = HourlyRate;
                                    lblTrabauServiceFee_HourlyRate.Text = Hourly_Fee.ToString();
                                    txtReceive_HourlyRate.Text = (double.Parse(HourlyRate) - Hourly_Fee).ToString();


                                    if (JobBudgetTypeText == "Fixed Price")
                                    {
                                        div_project_length.Visible = true;
                                        List<ListItem> ls_project_length = new List<ListItem>();
                                        ls_project_length.Add(new ListItem { Text = "Less than 1 week", Value = "Less than 1 week" });
                                        ls_project_length.Add(new ListItem { Text = "Less than 1 month", Value = "Less than 1 month" });
                                        ls_project_length.Add(new ListItem { Text = "1 to 3 months", Value = "1 to 3 months" });
                                        ls_project_length.Add(new ListItem { Text = "3 to 6 months", Value = "3 to 6 months" });
                                        ls_project_length.Add(new ListItem { Text = "More than 6 months", Value = "More than 6 months" });

                                        ddlProjectLength.DataSource = ls_project_length;
                                        ddlProjectLength.DataTextField = "Text";
                                        ddlProjectLength.DataValueField = "Value";
                                        ddlProjectLength.DataBind();
                                    }

                                    try
                                    {
                                        var ls_AllSkills = ds.Tables[1].ToDynamic();
                                        var ls_skills = ls_AllSkills.Select(x => new
                                        {
                                            x.ExpertiseType,
                                            x.ExpertiseValue,
                                            x.ExpertiseSrNo
                                        }).ToList();

                                        rAllSkillsExpertise.DataSource = ls_skills.Select(x => new
                                        {
                                            x.ExpertiseValue,
                                            x.ExpertiseSrNo
                                        }).OrderBy(y => y.ExpertiseSrNo);
                                        rAllSkillsExpertise.DataBind();
                                    }
                                    catch { }

                                    try
                                    {
                                        rScreenQuestions.DataSource = ds.Tables[2];
                                        rScreenQuestions.DataBind();
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    aViewJobPosting.HRef = "viewjob.aspx?job=" + JobId;
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnAddProfileFiles_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt_files = Session["ApplyPost_Project_Files"] as DataTable;
            rProfileFiles.DataSource = dt_files;
            rProfileFiles.DataBind();

            upApplyJob.Update();
        }
        catch (Exception)
        {
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();}, 0);", true);
    }

    protected void afuProjectFiles_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        try
        {
            if (afuProjectFiles.HasFile)
            {
                DataTable dt_files = new DataTable();
                if (Session["ApplyPost_Project_Files"] != null)
                {
                    dt_files = Session["ApplyPost_Project_Files"] as DataTable;
                }
                else
                {
                    dt_files = GetProjectFilesStructure();
                }
                byte[] attachment = afuProjectFiles.FileBytes;
                dt_files = AddProjectFilesItem(dt_files, afuProjectFiles.FileName, attachment);
                Session["ApplyPost_Project_Files"] = dt_files;
            }
        }
        catch (Exception)
        {
        }
    }

    public DataTable GetProjectFilesStructure()
    {
        DataTable dt_files = new DataTable();
        dt_files.Columns.Add("file_key", typeof(string));
        dt_files.Columns.Add("file_name", typeof(string));
        dt_files.Columns.Add("file_bytes", typeof(byte[]));
        return dt_files;
    }

    public DataTable AddProjectFilesItem(DataTable dt_files, string file_name, byte[] file_bytes)
    {
        try
        {
            DataRow dr = dt_files.NewRow();
            dr["file_key"] = RandomString(20).ToLower();
            dr["file_name"] = file_name;
            //string base64_bytes = Convert.ToBase64String(file_bytes);
            dr["file_bytes"] = file_bytes;
            dt_files.Rows.Add(dr);
        }
        catch (Exception ex)
        {
        }

        return dt_files;
    }

    public string RandomString(int length)
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    protected void lbtnDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string FileKey = (item.FindControl("lblfilekey") as Label).Text;

            Session["download_file_key"] = FileKey;

            string URL = ResolveClientUrl("~") + "/download.ashx?path=" + RandomString(20).ToLower();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openURL_downloadfile", "openURL('" + URL + "');", true);

        }
        catch (Exception ex)
        {
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();}, 0);", true);
    }

    protected void lbtnRemoveFile_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string filekey = (item.FindControl("lblfilekey") as Label).Text;

            DataTable dt_files = Session["ApplyPost_Project_Files"] as DataTable;

            dt_files.Rows.Cast<DataRow>().Where(
                r => r.ItemArray[0].ToString() == filekey).ToList().ForEach(r => r.Delete());

            Session["ApplyPost_Project_Files"] = dt_files;
            rProfileFiles.DataSource = dt_files;
            rProfileFiles.DataBind();
        }
        catch (Exception)
        {
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();}, 0);", true);
    }

    protected void btnCancelProposal_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }

    protected void btnSubmitProposal_Click(object sender, EventArgs e)
    {
        try
        {
            string JobId = Request.QueryString["job"];
            JobId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(JobId), Trabau_Keys.Job_Key);

            string BIDValue = (div_FixedPrice_terms.Visible ? txtBid_FixedPrice.Text : txtBid_HourlyRate.Text);
            string BIDFinal_Value = (div_FixedPrice_terms.Visible ? txtReceive_FixedPrice.Text : txtReceive_HourlyRate.Text);
            string ProjectLength = ddlProjectLength.SelectedValue;
            string CoverLetter = hfCoverLetter.Value;//txtCoverLetter.Text;
            if (CoverLetter != string.Empty)
            {
                string Screen_Answers_XML = GetScreeningAnswers();
                DataTable Apply_Job_Files_XML = GetApplyJob_Files();
                string UserID = Session["Trabau_UserId"].ToString();

                searchjob obj = new searchjob();
                DataSet ds = obj.SubmitProposal(Int64.Parse(UserID), Int32.Parse(JobId), double.Parse(BIDValue), ProjectLength, CoverLetter, Request.UserHostAddress, Screen_Answers_XML,
                    Apply_Job_Files_XML);

                string response = "error";
                string message = "Error while submitting proposal, please refresh and try again";
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            response = ds.Tables[0].Rows[0]["Response"].ToString().ToLower();
                            message = ds.Tables[0].Rows[0]["Message"].ToString();
                            if (response == "success")
                            {
                                string template_url = "https://www.trabau.com/emailers/xxddcca/applyjob.html";
                                string Freelancer_Name = Session["Trabau_FirstName"].ToString();
                                string EmailId = Session["Trabau_EmailId"].ToString();
                                string JobTitle = ltrlJobTitle.Text;
                                string ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
                                string ClientEmailId = ds.Tables[0].Rows[0]["ClientEmailId"].ToString();
                                if (ClientEmailId != string.Empty)
                                {
                                    try
                                    {

                                        WebRequest req = WebRequest.Create(template_url);
                                        WebResponse w_res = req.GetResponse();
                                        StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                        string html = sr.ReadToEnd();

                                        html = html.Replace("@Name", Freelancer_Name);
                                        html = html.Replace("@JobTitle", JobTitle);

                                        string body = html;

                                        Emailer obj_email = new Emailer();
                                        string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Application Submitted – " + JobTitle, body, null);

                                        try
                                        {
                                            utility log = new utility();
                                            log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, _val, Request.UserHostAddress);
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
                                            log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, ex.Message, Request.UserHostAddress);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }

                                    try
                                    {
                                        template_url = "https://www.trabau.com/emailers/xxddcca/proposal-freelancer.html";

                                        WebRequest req = WebRequest.Create(template_url);
                                        WebResponse w_res = req.GetResponse();
                                        StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                        string html = sr.ReadToEnd();

                                        html = html.Replace("@Name", Freelancer_Name);
                                        html = html.Replace("@JobTitle", JobTitle);

                                        string body = html;

                                        Emailer obj_email = new Emailer();
                                        string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Will they hire you? – Proposal Sent", body, null);

                                        try
                                        {
                                            utility log = new utility();
                                            log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, _val, Request.UserHostAddress);
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
                                            log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, ex.Message, Request.UserHostAddress);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }

                                    try
                                    {
                                        template_url = "https://www.trabau.com/emailers/xxddcca/proposal-client.html";

                                        WebRequest req = WebRequest.Create(template_url);
                                        WebResponse w_res = req.GetResponse();
                                        StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                        string html = sr.ReadToEnd();

                                        html = html.Replace("@Name", ClientName);
                                        html = html.Replace("@JobTitle", JobTitle);
                                        html = html.Replace("@Freelancer_Name", Freelancer_Name);

                                        string body = html;

                                        Emailer obj_email = new Emailer();
                                        string _val = obj_email.SendEmail(ClientEmailId, "", "", "Trabau Notification", "Will you hire them? – New Proposal", body, null);

                                        try
                                        {
                                            utility log = new utility();
                                            log.CreateEmailerLog(Convert.ToInt64(UserID), ClientEmailId, template_url, _val, Request.UserHostAddress);
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
                                            log.CreateEmailerLog(Convert.ToInt64(UserID), ClientEmailId, template_url, ex.Message, Request.UserHostAddress);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "JobPost_Message_Success", "setTimeout(function () { window.location.href='index.aspx';}, 1000);", true);
                            }
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "SubmitProposal_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CoverLetterError_Message", "setTimeout(function () { toastr['error']('Enter Cover Letter');}, 200);", true);
            }


            
        }
        catch (Exception)
        {
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();}, 0);", true);
    }

    public string GetScreeningAnswers()
    {
        string Screen_Answers_XML = "";
        try
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("QuestionId", typeof(string));
            dtQuestions.Columns.Add("Answer", typeof(string));

            foreach (RepeaterItem item in rScreenQuestions.Items)
            {
                string QuestionId = (item.FindControl("lblQuestionId") as Label).Text;
                string Answer = (item.FindControl("txtScreeningQuestion") as TextBox).Text;

                DataRow dr = dtQuestions.NewRow();
                dr["QuestionId"] = QuestionId;
                dr["Answer"] = Answer;

                dtQuestions.Rows.Add(dr);
            }

            Screen_Answers_XML = MiscFunctions.GetXMLString(dtQuestions, "ScreeningQuestions");
        }
        catch (Exception)
        {
        }
        return Screen_Answers_XML;
    }


    public DataTable GetApplyJob_Files()
    {
        DataTable dt_files = new DataTable();
        try
        {
            if (Session["ApplyPost_Project_Files"] != null)
            {
                dt_files = Session["ApplyPost_Project_Files"] as DataTable;

                // XMLData = MiscFunctions.GetXMLString(dt_files, "ApplyJob_Files");
            }
            else
            {
                dt_files = GetProjectFilesStructure();
            }
        }
        catch (Exception)
        {
        }

        return dt_files;
    }
}