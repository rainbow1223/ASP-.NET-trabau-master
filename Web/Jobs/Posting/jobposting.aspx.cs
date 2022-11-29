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

public partial class Jobs_Posting_jobposting : System.Web.UI.Page
{
    public static string _JobId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["download_file_key"] = null;
            if (Session["PostedJob_JobId"] != null)
            {
                string JobId = Session["PostedJob_JobId"].ToString();
                ViewState["PostedJob_JobId"] = JobId;
                Session["PostedJob_JobId"] = null;
                _JobId = JobId;
                DisplayJobDetails(Int32.Parse(JobId));
            }
            else
            {
                Response.Redirect("postedjobs.aspx");
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "MenuChangeEvent", "setTimeout(function () {RegisterMenuChangeEvent();}, 0);", true);
    }

    public void DisplayJobDetails(int JobId)
    {
        try
        {
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds = obj.GetPostedJobDetails(Int32.Parse(UserID), JobId);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
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

                        string Deliverable = ds.Tables[0].Rows[0]["Deliverable"].ToString();
                        string Languages = ds.Tables[0].Rows[0]["Languages"].ToString();
                        string Skills = ds.Tables[0].Rows[0]["Skills"].ToString();
                        string AdditionalSkills = ds.Tables[0].Rows[0]["AdditionalSkills"].ToString();

                        string ProposalsCount = ds.Tables[0].Rows[0]["ProposalsCount"].ToString();
                        string InterviewingCount = ds.Tables[0].Rows[0]["InterviewingCount"].ToString();
                        string InvitesCount = ds.Tables[0].Rows[0]["InvitesCount"].ToString();
                        string UnanswererdInvitesCount = ds.Tables[0].Rows[0]["UnanswererdInvitesCount"].ToString();

                        string JobLocationInfo = ds.Tables[0].Rows[0]["JobLocationInfo"].ToString();
                        string JobPaymentType = ds.Tables[0].Rows[0]["JobPaymentTypeText"].ToString();
                        string JobTypeText = ds.Tables[0].Rows[0]["JobTypeText"].ToString();

                        ltrlLocationInfo.Text = JobLocationInfo;
                        ltrlPaymentType.Text = JobPaymentType;
                        ltrlTypeOfWork.Text = JobTypeText;

                        DataSet ds_skills = new DataSet();
                        if (ViewState["Trabau_Skills"] != null)
                        {
                            ds_skills = ViewState["Trabau_Skills"] as DataSet;
                        }
                        else
                        {
                            ds_skills = obj.GetSkillsList("");
                            ViewState["Trabau_Skills"] = ds_skills;
                        }

                        lblNoOfPeople.Text = JobNumberOfPeople;
                        BindDataForReview(rFE_Deliverables, Deliverable, ds_skills);
                        BindDataForReview(rFE_Languages, Languages, ds_skills);
                        BindDataForReview(rAdditionalSkills, AdditionalSkills, ds_skills);

                        ltrlJobTitle.Text = Title;
                        ltrlJobDescription.Text = Description;
                        ltrlJobCategory.Text = JobCategoryText;
                        ltrlJobPostedOn.Text = PostedOn;
                        ltrlBudgetType.Text = JobBudgetTypeText;
                        ltrlBudgetValue.Text = "$" + JobBudgetValue;
                        ltrlExperienceLevel.Text = JobLevelOfExperience;
                        ltrlExperienceInfo.Text = JobExperienceInfo;

                        ltrlProposalsCount.Text = ProposalsCount;
                        ltrlInterviewingCount.Text = InterviewingCount;
                        ltrlInvitesCount.Text = InvitesCount;
                        ltrlUnanswererdInvitesCount.Text = UnanswererdInvitesCount;

                        DataSet ds_menu = obj.GetPostedJobMenu(Int32.Parse(UserID), JobId);
                        rPostedJobMenu.DataSource = ds_menu;
                        rPostedJobMenu.DataBind();


                        DataTable dt_files = GetProjectFilesStructure();
                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            DataRow dr = dt_files.NewRow();
                            dr["file_name"] = ds.Tables[2].Rows[i]["file_name"].ToString();
                            dr["file_key"] = ds.Tables[2].Rows[i]["file_key"].ToString();
                            dr["file_bytes"] = Convert.ToBase64String((byte[])ds.Tables[2].Rows[i]["file_bytes"]);

                            dt_files.Rows.Add(dr);
                        }
                        Session["JobPost_Project_Files"] = dt_files;

                        rProfileFiles.DataSource = dt_files;
                        rProfileFiles.DataBind();

                        if (rProfileFiles.Items.Count == 0)
                        {
                            div_profile_files_empty.Visible = true;
                        }

                        try
                        {
                            rNoOfPeople.DataSource = ds.Tables[4];
                            rNoOfPeople.DataBind();

                            tr_Empty_People.Visible = (rNoOfPeople.Items.Count == 0 ? true : false);
                            //int total = 0;
                            //foreach (RepeaterItem item in rNoOfPeople.Items)
                            //{
                            //    string Budget = (item.FindControl("lblPeopleBudgetValue") as Label).Text;
                            //    total = total + Int32.Parse(Budget);
                            //}
                            //lblTotalPeopleBudget.Text = total.ToString() + "$";
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }
        catch { }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeTab", "setTimeout(function () {ActivateInviteTab('JP');}, 0);", true);
    }

    public DataTable GetProjectFilesStructure()
    {
        DataTable dt_files = new DataTable();
        dt_files.Columns.Add("file_key", typeof(string));
        dt_files.Columns.Add("file_name", typeof(string));
        dt_files.Columns.Add("file_bytes", typeof(string));
        return dt_files;
    }

    public void BindDataForReview(Repeater rep, string selected, DataSet data)
    {
        try
        {
            if (selected != string.Empty)
            {

                string Filter = "";
                for (int i = 0; i < selected.Split(',').Length; i++)
                {
                    if (Filter == string.Empty)
                    {
                        Filter = "Value='" + selected.Split(',')[i] + "'";
                    }
                    else
                    {
                        Filter = Filter + " or Value='" + selected.Split(',')[i] + "'";
                    }
                }

                DataView dv_data = data.Tables[0].DefaultView;
                dv_data.RowFilter = Filter;

                rep.DataSource = dv_data;
                rep.DataBind();
            }
            else
            {
                rep.DataSource = null;
                rep.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnMenuLink_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string JobId = (item.FindControl("lblJobId") as Label).Text;
            string MenuCode = (item.FindControl("lblMenuCode") as Label).Text;

            if (MenuCode == "EP")
            {
                Session["PostedJob_JobId"] = JobId;
                Response.Redirect("postjob.aspx");
            }
            else if (MenuCode == "RP")
            {
                jobposting obj = new jobposting();
                string UserID = Session["Trabau_UserId"].ToString();

                string data = obj.RemoveJob(Int32.Parse(UserID), Int32.Parse(JobId), Request.UserHostAddress);

                string _response = data.Split(':')[0];
                string _message = data.Split(':')[1];
                if (_response == "success")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "JobUpdate_Redirection", "setTimeout(function () { window.location.href='postedjobs.aspx';}, 800);", true);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JobUpdate_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);
            }
            else if (MenuCode == "CL")
            {
                Response.Redirect("postedjobs.aspx");
            }
            else if (MenuCode == "ReP")
            {
                Session["PostedJob_JobId"] = JobId;
                Session["PostedJob_Reuse"] = "1";
                Response.Redirect("postjob.aspx");
            }
            else if (MenuCode == "M_Priv" || MenuCode == "M_Pub")
            {
                jobposting obj = new jobposting();
                string UserID = Session["Trabau_UserId"].ToString();

                string data = obj.UpdateVisibility(Int32.Parse(UserID), Int32.Parse(JobId), Request.UserHostAddress, MenuCode);

                string _response = data.Split(':')[0];
                string _message = data.Split(':')[1];
                if (_response == "success")
                {
                    (item.FindControl("lblMenuCode") as Label).Text = (MenuCode == "M_Priv" ? "M_Pub" : "M_Priv");
                    (item.FindControl("lbtnConfirmation_MenuLink") as LinkButton).Text = "<i class='flaticon-null-10'></i>" + (MenuCode == "M_Priv" ? "Make Public" : "Make Private");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "JobUpdate_Redirection", "setTimeout(function () { window.location.href='postedjobs.aspx';}, 800);", true);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JobUpdate_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);
            }
        }
        catch (Exception)
        {

        }
    }

    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string SearchText = txtSearch_FL_JobPosting.Text;
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();
            string JobId = ViewState["PostedJob_JobId"].ToString();
            DataSet ds_freelancers = obj.SearchFreelancers(Int32.Parse(UserID), Int32.Parse(JobId), SearchText, "");

            BindFreelancers(ds_freelancers);
        }
        catch (Exception)
        {
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Invite_Tab", "setTimeout(function () {$('#profile-tab').click();}, 0);", true);
    }

    protected void lbtnHire_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent.Parent as RepeaterItem;
            string AddUserId = (item.FindControl("lblUserId") as Label).Text;
            string Name = (item.FindControl("lblName") as Label).Text;
            string EmailId = (item.FindControl("lblEmailId") as Label).Text;
            string JobId = ViewState["PostedJob_JobId"].ToString();
            string UserID = Session["Trabau_UserId"].ToString();

            jobposting obj = new jobposting();
            string data = obj.SaveJobDetails(Int64.Parse(UserID), Int32.Parse(JobId), Int64.Parse(AddUserId), "H", true, Request.UserHostAddress);
            //string data = "";
            string _response = data.Split(':')[0];
            string _message = data.Split(':')[1];
            if (_response == "success")
            {
                try
                {
                    Emailer obj_email = new Emailer();
                    string ClientName = Session["Trabau_FirstName"].ToString();
                    string body = "Dear " + Name + ",<br/><br/>You are hired by " + ClientName + " for the job " + ltrlJobTitle.Text;

                    obj_email.SendEmail(EmailId, "", "", "Trabau Hiring", "You are hired - Trabau", body, null);



                    string client_body = "Dear " + ClientName + ",<br/><br/>You have hired " + Name + " (freelancer) for the job " + ltrlJobTitle.Text;
                    string ClientEmailId = Session["Trabau_EmailId"].ToString();
                    obj_email.SendEmail(ClientEmailId, "", "", "Trabau Hiring", "You have hired - Trabau", client_body, null);

                }
                catch (Exception)
                {

                }

                (item.FindControl("lbtnHire") as LinkButton).Visible = false;
                (item.FindControl("lblHiredText") as Label).Visible = true;
                (item.FindControl("lblHiredText") as Label).Text = "<i class='fa fa-check' aria-hidden='true'></i> Hired";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "JobUpdate_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);
        }
        catch (Exception)
        {
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "GetTrabauPic_Info", "setTimeout(function () { GetTrabau_PicInfo('1000','" + (rFreelancers.Items.Count + 1000).ToString() + "');RegisterPreferEvent();}, 0);", true);
    }

    protected void lbtnInvite_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent.Parent as RepeaterItem;
            string AddUserId = (item.FindControl("lblUserId") as Label).Text;
            string Name = (item.FindControl("lblName") as Label).Text;
            string EmailId = (item.FindControl("lblEmailId") as Label).Text;
            string JobId = ViewState["PostedJob_JobId"].ToString();
            string UserID = Session["Trabau_UserId"].ToString();

            jobposting obj = new jobposting();
            string data = obj.SaveJobDetails(Int64.Parse(UserID), Int32.Parse(JobId), Int64.Parse(AddUserId), "I", true, Request.UserHostAddress);
            string _response = data.Split(':')[0];
            string _message = data.Split(':')[1];
            if (_response == "success")
            {
                try
                {
                    Emailer obj_email = new Emailer();
                    string ClientName = Session["Trabau_FirstName"].ToString();
                    string body = "Dear " + Name + ",<br/><br/>You are invited by " + ClientName + " for the job " + ltrlJobTitle.Text;

                    obj_email.SendEmail(EmailId, "", "", "Trabau Invite", "You are invited - Trabau", body, null);



                    string client_body = "Dear " + ClientName + ",<br/><br/>You have invited " + Name + " (freelancer) for the job " + ltrlJobTitle.Text;
                    string ClientEmailId = Session["Trabau_EmailId"].ToString();
                    obj_email.SendEmail(ClientEmailId, "", "", "Trabau Invite Sent", "You have invited - Trabau", client_body, null);


                }
                catch (Exception)
                {

                }
                (item.FindControl("lbtnInvite") as LinkButton).Visible = false;
                (item.FindControl("lblInvitedText") as Label).Visible = true;
                (item.FindControl("lblInvitedText") as Label).Text = "<i class='fa fa-check' aria-hidden='true'></i> Invited";
                //ClearUploadControls();
                //LoadUploadedDocuments();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "JobUpdate_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);
        }
        catch (Exception)
        {
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "GetTrabauPic_Info", "setTimeout(function () { GetTrabau_PicInfo('1000','" + (rFreelancers.Items.Count + 1000).ToString() + "');RegisterPreferEvent();ActivateTooltip();}, 0);", true);
    }

    protected void lbtnTabInvite_Click(object sender, EventArgs e)
    {
        try
        {
            if (rFreelancers.Items.Count == 0)
            {
                lbtnSearch_Click(lbtnSearch, new EventArgs());
            }
            div_search_FL.Visible = true;
            upSearch_Freelancers.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeTab", "setTimeout(function () {ActivateInviteTab('I');ActivateChild_InviteTab('S');setTimeout(function () { GetTrabau_PicInfo('1000','" + (rFreelancers.Items.Count + 1000).ToString() + "');RegisterPreferEvent();ActivateTooltip();}, 200);}, 0);", true);
        }
        catch (Exception)
        {
        }
    }


    protected void lbtnTabViewJobPost_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeTab", "setTimeout(function () {ActivateInviteTab('JP');}, 0);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnTabHire_Click(object sender, EventArgs e)
    {
        try
        {
            lbtnChildHireTab_Click(sender, e);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeTab_Hire", "setTimeout(function () {ActivateInviteTab('H');ActivateChild_InviteTab('H');}, 0);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string AddUserId = (item.FindControl("lblUserId") as Label).Text;
            string Saved = (item.FindControl("lblSaved") as Label).Text;
            string JobId = ViewState["PostedJob_JobId"].ToString();
            string UserID = Session["Trabau_UserId"].ToString();
            bool Value = (Saved.ToLower() == "true" ? false : true);
            jobposting obj = new jobposting();
            string data = obj.SaveJobDetails(Int64.Parse(UserID), Int32.Parse(JobId), Int64.Parse(AddUserId), "S", Value, Request.UserHostAddress);

            string _response = data.Split(':')[0];
            string _message = data.Split(':')[1];
            if (_response == "success")
            {
                (item.FindControl("lblSaved") as Label).Text = Value.ToString();
                (item.FindControl("lbtnSave") as LinkButton).Text = "<i class='fa fa-heart" + (Value == false ? "-o" : "") + "' aria-hidden='true'></i>";

                //(item.FindControl("lblInvitedText") as Label).Visible = true;
                //(item.FindControl("lblInvitedText") as Label).Text = "<i class='fa fa-check' aria-hidden='true'></i> Invited";
                //ClearUploadControls();
                //LoadUploadedDocuments();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "JobUpdate_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnChildSearchTab_Click(object sender, EventArgs e)
    {
        try
        {
            string SearchText = "";
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();
            string JobId = ViewState["PostedJob_JobId"].ToString();
            string Type = "";
            DataSet ds_freelancers = obj.SearchFreelancers(Int32.Parse(UserID), Int32.Parse(JobId), SearchText, Type);

            BindFreelancers(ds_freelancers);

            div_search_FL.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeTab", "setTimeout(function () {ActivateInviteTab('I');ActivateChild_InviteTab('S');}, 0);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnChildInviteTab_Click(object sender, EventArgs e)
    {
        try
        {
            string SearchText = "";
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();
            string JobId = ViewState["PostedJob_JobId"].ToString();
            string Type = "I";
            DataSet ds_freelancers = obj.SearchFreelancers(Int32.Parse(UserID), Int32.Parse(JobId), SearchText, Type);

            BindFreelancers(ds_freelancers);

            div_search_FL.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeTab", "setTimeout(function () {ActivateInviteTab('I');ActivateChild_InviteTab('I');}, 0);", true);
        }
        catch (Exception)
        {
        }
    }

    public void BindFreelancers(DataSet ds_freelancers)
    {
        try
        {
            rFreelancers.DataSource = ds_freelancers.Tables[0];
            rFreelancers.DataBind();

            foreach (RepeaterItem item in rFreelancers.Items)
            {
                string _UserId = (item.FindControl("lblUserId") as Label).Text;
                DataView dv_skills = ds_freelancers.Tables[1].DefaultView;
                dv_skills.RowFilter = "UserId=" + _UserId;
                Repeater rFreelancers_Skills = item.FindControl("rFreelancers_Skills") as Repeater;
                rFreelancers_Skills.DataSource = dv_skills;
                rFreelancers_Skills.DataBind();


                string UserId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(_UserId, Trabau_Keys.Profile_Key));
                (item.FindControl("a_profile") as HtmlAnchor).HRef = "~/profile/user/userprofile.aspx?profile=" + UserId;


                string prefer_action_method = "P";
                prefer_action_method = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(prefer_action_method, Trabau_Keys.Misc_Key));
                (item.FindControl("lbtnAddToPrefer") as HtmlAnchor).Attributes.Add("action-method", prefer_action_method);

                (item.FindControl("lbtnAddToPrefer") as HtmlAnchor).Attributes.Add("action-name", "AddtoPreferList");
                // (item.FindControl("lbtnHire") as HtmlAnchor).Attributes.Add("action-name", "Hire_Action");



                (item.FindControl("div_actions") as HtmlControl).Attributes.Add("data", UserId);

                string ran = Guid.NewGuid().ToString();
                (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data-key", ran + "_userpic_" + (item.ItemIndex + 1000).ToString());

                string Enc_UserId = _UserId + "~" + DateTime.Now.ToString();
                Enc_UserId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(Enc_UserId, Trabau_Keys.Profile_Key));

                (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data", Enc_UserId);
                (item.FindControl("imgFL_ProfilePic") as HtmlImage).Src = "~/assets/uploads/loading_pic.gif";
                //  (item.FindControl("imgFL_ProfilePic") as HtmlImage).Src = ImageProcessing.GetUserPicture(Int64.Parse(_UserId), 100, 100);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Activate_Tooltip", "setTimeout(function () {ActivateTooltip();RegisterPreferEvent();}, 0);", true);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GetTrabauPic_Info", "setTimeout(function () { GetTrabau_PicInfo('1000','" + (rFreelancers.Items.Count + 1000).ToString() + "');}, 0);", true);
        }
        catch (Exception)
        {
        }
    }



    protected void lbtnChildHireTab_Click(object sender, EventArgs e)
    {
        try
        {
            string SearchText = "";
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();
            string JobId = ViewState["PostedJob_JobId"].ToString();
            string Type = "H";
            DataSet ds_freelancers = obj.SearchFreelancers(Int32.Parse(UserID), Int32.Parse(JobId), SearchText, Type);

            BindFreelancers(ds_freelancers);

            div_search_FL.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeTab", "setTimeout(function () {ActivateInviteTab('I');ActivateChild_InviteTab('H');}, 0);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnChildSavedTab_Click(object sender, EventArgs e)
    {
        try
        {
            string SearchText = "";
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();
            string JobId = ViewState["PostedJob_JobId"].ToString();
            string Type = "S";
            DataSet ds_freelancers = obj.SearchFreelancers(Int32.Parse(UserID), Int32.Parse(JobId), SearchText, Type);

            BindFreelancers(ds_freelancers);

            div_search_FL.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeTab", "setTimeout(function () {ActivateInviteTab('I');ActivateChild_InviteTab('Saved');}, 0);", true);
        }
        catch (Exception)
        {
        }
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeTab", "setTimeout(function () {ActivateInviteTab('JP');}, 0);", true);
    }




    [WebMethod(EnableSession = true)]
    public static string ViewProposals()
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
                var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/posting/UserControls/wucSearchFreelancer.ascx");
                try
                {
                    (viewControl.FindControl("lblJobId") as Label).Text = _JobId;
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
                detail.Add("Proposals_html", output.ToString().Replace("\r\n", "").ToString());

            }
            else
            {
                detail.Add("response", "");
                detail.Add("Proposals_html", "");
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
    public static string GetUserPicture(string data)
    {
        string val = "";
        try
        {
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                data = MiscFunctions.Base64DecodingMethod(data);
                data = EncyptSalt.DecryptString(data, Trabau_Keys.Profile_Key);
                val = data.Split('~')[0];
                string dd = data.Split('~')[1];
                val = ImageProcessing.GetUserPicture(Int64.Parse(val), 120, 100);
            }

        }
        catch (Exception ex)
        {
            val = "";
        }
        return val;
    }


    [WebMethod(EnableSession = true)]
    public static string ViewProposalDetails(string ProposalId)
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
                var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/posting/UserControls/wucProposalDetails.ascx");
                try
                {
                    (viewControl.FindControl("lblProposalId") as Label).Text = ProposalId;
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
                detail.Add("Proposal_details_html", output.ToString().Replace("\r\n", "").ToString());

            }
            else
            {
                detail.Add("response", "");
                detail.Add("Proposal_details_html", "");
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
    public static string Proposal_Action(string proposalId, string action)
    {
        string val = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();

            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string data = proposalId;
                data = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(data), Trabau_Keys.Job_Key);
                string ProposalId = data.Split('-')[0];
                string JobId = data.Split('-')[1];
                string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                string IPAddress = HttpContext.Current.Request.UserHostAddress;

                jobposting obj = new jobposting();
                DataSet ds_response = obj.SaveJobProposalAction(Int32.Parse(JobId), Int32.Parse(ProposalId), Int64.Parse(UserID), action, IPAddress);
                string response = "error";
                string message = "Error while taking action on proposal, please refresh and try again";
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
                            string EmailId = ds_response.Tables[0].Rows[0]["EmailId"].ToString();
                            string Freelancer_UserId = ds_response.Tables[0].Rows[0]["Freelancer_UserId"].ToString();

                            if (response == "success")
                            {
                                string template_url = string.Empty;

                                if (action == "H")
                                {
                                    template_url = "https://www.trabau.com/emailers/xxddcca/freelancer-hired-to-freelancer.html";
                                }
                                else
                                {
                                    template_url = "https://www.trabau.com/emailers/xxddcca/proposal-declined-freelancer.html";
                                }


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
                                    string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification",
                                        (action == "H" ? "Congratulations – You are hired!!!" : "Not a fit…"), body, null);

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



                                string ClientEmailId = HttpContext.Current.Session["Trabau_EmailId"].ToString();
                                string ClientUserId = UserID;
                                try
                                {
                                    string ClientName = HttpContext.Current.Session["Trabau_FirstName"].ToString();

                                    if (action == "H")
                                    {
                                        template_url = "https://www.trabau.com/emailers/xxddcca/freelancer-hired-to-client.html";
                                    }
                                    else
                                    {
                                        template_url = "https://www.trabau.com/emailers/xxddcca/proposal-declined-client.html";
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
                                        (action == "H" ? "Yay! Freelancer Hired!!" : "Not a fit? – Proposal Declined"), body, null);

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
        catch (Exception ex)
        {
            val = "";
        }
        return val;
    }


    [WebMethod(EnableSession = true)]
    public static string GetProposalAdditionalFiles(string ProposalId)
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
                var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/Posting/UserControls/wucProposalAdditionalFiles.ascx");
                try
                {
                    (viewControl.FindControl("hfAdditionalFiles_ProposalId") as HiddenField).Value = ProposalId;
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
    public static string FlagForInterview(string ProposalId)
    {
        string val = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();

            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string data = ProposalId;
                data = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(data), Trabau_Keys.Job_Key);
                string _ProposalId = data.Split('-')[0];
                string JobId = data.Split('-')[1];
                string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                string IPAddress = HttpContext.Current.Request.UserHostAddress;

                jobposting obj = new jobposting();
                DataSet ds_response = obj.FlagForInterview(Int32.Parse(JobId), Int32.Parse(_ProposalId), IPAddress, Int64.Parse(UserID), "A");
                string response = "error";
                string message = "Error while taking action on interview flag, please refresh and try again";
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
                            string Freelancer_EmailId = ds_response.Tables[0].Rows[0]["Freelancer_EmailId"].ToString();
                            string Freelancer_UserId = ds_response.Tables[0].Rows[0]["Freelancer_UserId"].ToString();
                            string ClientName = HttpContext.Current.Session["Trabau_FirstName"].ToString();
                            if (response == "success")
                            {
                                string template_url = "https://www.trabau.com/emailers/xxddcca/identified-for-interview-to-freelancer.html";

                                if (Freelancer_EmailId != string.Empty)
                                {
                                    try
                                    {
                                        WebRequest req = WebRequest.Create(template_url);
                                        WebResponse w_res = req.GetResponse();
                                        StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                        string html = sr.ReadToEnd();

                                        html = html.Replace("@Name", Freelancer_Name);
                                        html = html.Replace("@Client_Name", ClientName);
                                        html = html.Replace("@JobTitle", JobTitle);

                                        string body = html;

                                        Emailer obj_email = new Emailer();
                                        string _val = obj_email.SendEmail(Freelancer_EmailId, "", "", "Trabau Notification", "You’ve been identified! What’s next?", body, null);

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



                                    string ClientEmailId = HttpContext.Current.Session["Trabau_EmailId"].ToString();
                                    string ClientUserId = UserID;
                                    try
                                    {



                                        template_url = "https://www.trabau.com/emailers/xxddcca/identified-for-interview-to-client.html";

                                        WebRequest req = WebRequest.Create(template_url);
                                        WebResponse w_res = req.GetResponse();
                                        StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                        string html = sr.ReadToEnd();

                                        html = html.Replace("@Name", ClientName);
                                        html = html.Replace("@Freelancer_Name", Freelancer_Name);
                                        html = html.Replace("@JobTitle", JobTitle);

                                        string body = html;

                                        Emailer obj_email = new Emailer();
                                        string _val = obj_email.SendEmail(ClientEmailId, "", "", "Trabau Notification", "Freelancer Identified! What’s next?", body, null);

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
        catch (Exception ex)
        {
            val = "";
        }
        return val;
    }



    [WebMethod(EnableSession = true)]
    public static string SendJobToFriend(string Name, string EmailAddress, string userid)
    {
        string val = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();

            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string JobId = _JobId;
                string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                string IPAddress = HttpContext.Current.Request.UserHostAddress;

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



    //    protected void lbtnAddToPrefer_Click(object sender, EventArgs e)
    //    {
    //        try
    //        {
    //            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
    //            string AddUserId = (item.FindControl("lblUserId") as Label).Text;

    //            LinkButton lbtn = (sender as LinkButton);

    //            utility obj = new utility();
    //            string _UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();

    //            DataSet ds_response = obj.AddToPreferList(Int64.Parse(_UserId), Int64.Parse(AddUserId), "A", HttpContext.Current.Request.UserHostAddress);

    //            if (ds_response != null)
    //            {
    //                if (ds_response.Tables.Count > 0)
    //                {
    //                    if (ds_response.Tables[0].Rows.Count > 0)
    //                    {
    //                        string _response = ds_response.Tables[0].Rows[0]["Response"].ToString();
    //                        string _message = ds_response.Tables[0].Rows[0]["ResponseMessage"].ToString();
    //                        if (_response == "success")
    //                        {
    //                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "JobUpdate_Redirection", "setTimeout(function () { window.location.href='postedjobs.aspx';}, 800);", true);
    //                        }
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Prefer_Action_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception)
    //        {
    //        }
    //    }
}