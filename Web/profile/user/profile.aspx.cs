using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.BLL.SignUp;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.profile;
using TrabauClassLibrary.DLL.SignUp;

public partial class profile_user_profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count == 1)
            {
                if (Request.QueryString["profile"] != null)
                {
                    if (Request.QueryString["profile"] != string.Empty)
                    {
                        string profile_id = Request.QueryString["profile"];
                        profile_id = MiscFunctions.Base64DecodingMethod(profile_id);
                        profile_id = EncyptSalt.DecryptText(profile_id, Trabau_Keys.Profile_Key);
                        LoadProfile(profile_id, false);
                    }
                    else
                    {
                        LoadOwnProfile();
                    }
                }
                else
                {
                    LoadOwnProfile();
                }
            }
            else
            {
                LoadOwnProfile();
            }

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Register_Load_Events", "setTimeout(function () { ProfileLoad_Events();}, 200);", true);
    }

    public void LoadOwnProfile()
    {
        try
        {
            string UserID = Session["Trabau_UserId"].ToString();
            LoadProfile(UserID, true);
        }
        catch (Exception)
        {
        }

    }

    public void LoadProfile(string UserID, bool EditMode)
    {
        try
        {
            BLL_Registration obj = new BLL_Registration();

            Tuple<List<dynamic>, string> data = obj.LoadProfileDetails(Int64.Parse(UserID));
            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        try
                        {
                            lbtnEditProfilePicture.Visible = EditMode;

                            ltrl_profile_name.Text = res[0].Name;
                            string user_pic = ImageProcessing.GetUserPicture(Int64.Parse(UserID), 133, 100);
                            img_profile_picture.Src = user_pic;
                            ltrl_profile_cityname.Text = res[0].CityFullName;
                            ltrl_profile_title.Text = res[0].Title;
                            ltrl_profile_overview.Text = res[0].Overview;
                            ltrlLocalTime.Text = res[0].LocalTime;
                            ltrlProgessPercentage.Text = res[0].ProgessPercentage;
                            divProgessBar.Attributes.Add("style", "width:" + res[0].ProgessPercentage);

                            ltrlProgessPercentageMobile.Text = res[0].ProgessPercentage;
                            divProgessBarMobile.Attributes.Add("style", "width:" + res[0].ProgessPercentage);

                            ltrlHourlyRate.Text = res[0].HourlyRate;
                            ltrlTotalEarned.Text = res[0].TotalEarned;
                            ltrlTotalJobs.Text = res[0].TotalJobs;
                            ltrlProfileVisibility.Text = res[0].VisibilityText;
                            lblProfileVisibility.Text = res[0].Visibility;
                            ltrlProfileAvailability.Text = res[0].Availability;
                            lblProfileAvailability.Text = res[0].AvailabilityText;
                            profile_visibility_icon_class.Attributes.Add("class", res[0].VisibilityIcon);
                            DateTime VacationDate = res[0].VacationTillDated;

                            ltrlVacationDate.Text = String.Format("{0:MM/dd/yyyy}", VacationDate).Replace("-", "/");
                        }
                        catch (Exception)
                        {
                        }
                        
                        try
                        {
                            DisplayProfileSkills(UserID);
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            wucEmploymentHistory.GetUserEmploymentDetails(UserID, EditMode);
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            wucEducationHistory.GetUserEducationDetails(UserID, EditMode);
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            DisplayPortfolios(UserID);
                        }
                        catch (Exception)
                        {
                        }


                        this.Page.Title = ltrl_profile_name.Text + " - " + res[0].Title + " - Trabau Freelancer from " + res[0].CityFullName;
                    }
                }
            }

        }
        catch (Exception)
        {
        }
    }

    public void DisplayPortfolios(string UserID)
    {
        try
        {
            profile_updates obj = new profile_updates();
            DataSet ds_portfoilios = obj.GetPortfolios(Int64.Parse(UserID));
            for (int i = 0; i < ds_portfoilios.Tables[0].Rows.Count; i++)
            {
                byte[] file_bytes = (byte[])ds_portfoilios.Tables[0].Rows[i]["GalleryItem"];
                string file_bytes_preview = "data:image;base64," + Convert.ToBase64String(ImageProcessing.ScaleFreeHeight(file_bytes, 300, 400));
                ds_portfoilios.Tables[0].Rows[i]["GalleryItemPreview"] = file_bytes_preview;
            }
            rPortfolio.DataSource = ds_portfoilios;
            rPortfolio.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    public void DisplayProfileSkills(string UserID)
    {
        try
        {
            profile_updates obj = new profile_updates();
            DataSet ds_skills = obj.GetSkillsName(Int64.Parse(UserID));
            rSkills.DataSource = ds_skills;
            rSkills.DataBind();
        }
        catch (Exception)
        {
        }
    }

    public void DisplaySkills()
    {
        try
        {
            DLL_Registration reg = new DLL_Registration();
            DataSet ds_skills = reg.GetSkillsList();
            ddlSkills.DataSource = ds_skills;
            ddlSkills.DataTextField = "Text";
            ddlSkills.DataValueField = "Value";
            ddlSkills.DataBind();

            profile_updates obj = new profile_updates();
            string UserID = Session["Trabau_UserId"].ToString();
            string skills = obj.GetSkills(Int64.Parse(UserID));
            hfSkills.Value = skills;
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnEditTitle_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_Popup_EditTitle.Visible = true;
            txtProfileTitle.Text = ltrl_profile_title.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Edit_Title_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_Popup_EditTitle');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCloseEditTitlePopup_top_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_Popup_EditTitle.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Edit_Title_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_Popup_EditTitle');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveTitle_Click(object sender, EventArgs e)
    {
        try
        {
            string Title = txtProfileTitle.Text;
            string UserID = Session["Trabau_UserId"].ToString();
            profile_updates obj = new profile_updates();

            string data = obj.UpdateProfileTitle(Int64.Parse(UserID), Title, Int64.Parse(UserID));

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];

            if (response == "success")
            {
                try
                {
                    string Name = Session["Trabau_FirstName"].ToString();
                    string EmailId = Session["Trabau_EmailId"].ToString();
                    string template_url = "https://www.trabau.com/emailers/xxddcca/profile-update.html";

                    try
                    {
                        WebRequest req = WebRequest.Create(template_url);
                        WebResponse w_res = req.GetResponse();
                        StreamReader sr = new StreamReader(w_res.GetResponseStream());
                        string html = sr.ReadToEnd();

                        html = html.Replace("@Name", Name);
                        html = html.Replace("@ProfileAction", "profile title changes");

                        string body = html;

                        Emailer obj_email = new Emailer();
                        string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Profile Updated – Profile Title Changes", body, null);

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
                }
                catch (Exception)
                {
                }

                ltrl_profile_title.Text = Title;
                upParent.Update();
                btnCloseEditTitlePopup_top_Click(sender, e);
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "ProfileTitle_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnEditOverview_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_Popup_EditOverview.Visible = true;
            txtProfileOverview.Text = ltrl_profile_overview.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Edit_Overview_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_Popup_EditOverview');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCloseEditOverviewPopup_top_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_Popup_EditOverview.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Edit_Overview_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_Popup_EditOverview');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveOverview_Click(object sender, EventArgs e)
    {
        try
        {
            string OverView = txtProfileOverview.Text;
            string UserID = Session["Trabau_UserId"].ToString();
            profile_updates obj = new profile_updates();

            string data = obj.UpdateProfileOverview(Int64.Parse(UserID), OverView, Int64.Parse(UserID));

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];

            if (response == "success")
            {
                try
                {
                    string Name = Session["Trabau_FirstName"].ToString();
                    string EmailId = Session["Trabau_EmailId"].ToString();
                    string template_url = "https://www.trabau.com/emailers/xxddcca/profile-update.html";

                    try
                    {
                        WebRequest req = WebRequest.Create(template_url);
                        WebResponse w_res = req.GetResponse();
                        StreamReader sr = new StreamReader(w_res.GetResponseStream());
                        string html = sr.ReadToEnd();

                        html = html.Replace("@Name", Name);
                        html = html.Replace("@ProfileAction", "profile overview changes");

                        string body = html;

                        Emailer obj_email = new Emailer();
                        string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Profile Updated – Profile Overview Changes", body, null);

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
                }
                catch (Exception)
                {
                }

                ltrl_profile_overview.Text = OverView;
                upParent.Update();
                btnCloseEditOverviewPopup_top_Click(sender, e);
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "ProfileOverview_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnEditSkills_Click(object sender, EventArgs e)
    {
        try
        {
            DisplaySkills();
            divTrabau_Popup_EditSkills.Visible = true;
            //txtSkills.Text = ltrl_profile_overview.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Edit_Skills_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_Popup_EditSkills');}, 150);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_skills_list", "setTimeout(function () {RegisterSelect2('ddlSkills', 'Select skills from the list', 'hfSkills','0');}, 100);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCloseEditSkillsPopup_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_Popup_EditSkills.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Edit_Skills_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_Popup_EditSkills');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveSkills_Click(object sender, EventArgs e)
    {
        try
        {
            string Skills = hfSkills.Value;
            if (Skills != string.Empty && Skills != ",")
            {
                string UserID = Session["Trabau_UserId"].ToString();
                profile_updates obj = new profile_updates();

                string data = obj.UpdateProfileSkills(Int64.Parse(UserID), Skills, Int64.Parse(UserID));

                string response = data.Split(':')[0];
                string message = data.Split(':')[1];

                if (response == "success")
                {
                    try
                    {
                        string Name = Session["Trabau_FirstName"].ToString();
                        string EmailId = Session["Trabau_EmailId"].ToString();
                        string template_url = "https://www.trabau.com/emailers/xxddcca/profile-update.html";

                        try
                        {
                            WebRequest req = WebRequest.Create(template_url);
                            WebResponse w_res = req.GetResponse();
                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                            string html = sr.ReadToEnd();

                            html = html.Replace("@Name", Name);
                            html = html.Replace("@ProfileAction", "skill changes");

                            string body = html;

                            Emailer obj_email = new Emailer();
                            string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Profile Updated – Skill Changes", body, null);

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
                    }
                    catch (Exception)
                    {
                    }

                    DisplayProfileSkills(UserID);
                    upParent.Update();
                    btnCloseEditSkillsPopup_Click(sender, e);
                }


                ScriptManager.RegisterStartupScript(this, this.GetType(), "ProfileSkills_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
            }
        }
        catch (Exception)
        {
        }
    }


    protected void lbtnEditPortfolio_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = ((LinkButton)(sender)).Parent as RepeaterItem;
            string PortfolioId = (item.FindControl("lblPortfolioId") as Label).Text;

            PortfolioId = EncyptSalt.EncryptText(PortfolioId, "Trabau_Port_Update");
            Response.Redirect("portfolios.aspx?portfolio=" + EncyptSalt.Base64Encode(PortfolioId));


        }
        catch (Exception)
        {
        }
    }

    protected void lbtnAddPortfolio_Click(object sender, EventArgs e)
    {
        Response.Redirect("portfolios.aspx");
    }

    protected void lbtnEditHourlyRate_Click(object sender, EventArgs e)
    {
        try
        {
            ltrlCurrentProfileRate.Text = ltrlHourlyRate.Text + "/hr";
            divTrabau_Popup_HourlyRate.Visible = true;
            txtHourlyRate.Text = ltrlHourlyRate.Text.Replace("$", "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Edit_HourlyRate_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_Popup_HourlyRate');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCloseHourlyRatePopup_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_Popup_HourlyRate.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_HourlyRate_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_Popup_HourlyRate');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveHourlyRate_Click(object sender, EventArgs e)
    {
        try
        {
            string HourlyRate = txtHourlyRate.Text;
            string UserID = Session["Trabau_UserId"].ToString();
            profile_updates obj = new profile_updates();

            string data = obj.UpdateProfileHourlyRate(Int64.Parse(UserID), Convert.ToDecimal(HourlyRate), Int64.Parse(UserID));

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];

            if (response == "success")
            {
                string Freelancer_EmailId = Session["Trabau_EmailId"].ToString();
                string Freelancer_UserId = UserID;
                string Freelancer_Name = Session["Trabau_FirstName"].ToString();

                string template_url = "https://www.trabau.com/emailers/xxddcca/hourly-rate-changes-freelancer.html";
                try
                {
                    WebRequest req = WebRequest.Create(template_url);
                    WebResponse w_res = req.GetResponse();
                    StreamReader sr = new StreamReader(w_res.GetResponseStream());
                    string html = sr.ReadToEnd();

                    html = html.Replace("@Name", Freelancer_Name);

                    string body = html;

                    Emailer obj_email = new Emailer();
                    string _val = obj_email.SendEmail(Freelancer_EmailId, "", "", "Trabau Notification", "Hourly Rate Changes", body, null);

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

                ltrlHourlyRate.Text = "$" + HourlyRate;
                upParent.Update();
                btnCloseHourlyRatePopup_Click(sender, e);
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "ProfileHourlyRate_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void lbtnEditProfilePicture_Click(object sender, EventArgs e)
    {
        try
        {
            (wucProfilePicture.FindControl("div_profile_pic_popup") as HtmlControl).Visible = true;
            string UserID = Session["Trabau_UserId"].ToString();
            string user_pic = ImageProcessing.GetUserPicture(Int64.Parse(UserID), 700, 600);
            (wucProfilePicture.FindControl("imgProfilePic_Upload") as Image).ImageUrl = user_pic;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenProfilePic_Popup", "setTimeout(function () {HandlePopUp('1','div_profile_pic_popup');}, 500);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnEditProfileVisibility_Click(object sender, EventArgs e)
    {
        try
        {
            // ltrlCurrentProfileRate.Text = ltrlHourlyRate.Text + "/hr";
            divTrabau_Profile_Visibility.Visible = true;
            ddlProfileVisibility.SelectedValue = lblProfileVisibility.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Edit_ProfileVisibility_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_Profile_Visibility');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCloseProfileVisibilityPopup_top_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_Profile_Visibility.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_ProfileVisibility_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_Profile_Visibility');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnUpdateProfileVisibility_Click(object sender, EventArgs e)
    {
        try
        {
            string Visibility = ddlProfileVisibility.SelectedValue;
            string UserID = Session["Trabau_UserId"].ToString();
            profile_updates obj = new profile_updates();

            string data = obj.UpdateProfileVisibility(Int64.Parse(UserID), Visibility, Int64.Parse(UserID));

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];

            if (response == "success")
            {
                string IconClass = "fa fa-globe";
                if (Visibility == "Trabau")
                {
                    IconClass = "fa fa-id-badge";
                }
                else if (Visibility == "Private")
                {
                    IconClass = "fa fa-key";
                }

                string template_url = "https://www.trabau.com/emailers/xxddcca/profile-visibility-changes.html";
                string Freelancer_Name = Session["Trabau_FirstName"].ToString();
                string Freelancer_EmailId = Session["Trabau_EmailId"].ToString();
                string Freelancer_UserId = UserID;
                try
                {
                    WebRequest req = WebRequest.Create(template_url);
                    WebResponse w_res = req.GetResponse();
                    StreamReader sr = new StreamReader(w_res.GetResponseStream());
                    string html = sr.ReadToEnd();

                    html = html.Replace("@Name", Freelancer_Name);
                    html = html.Replace("@visibiliy_status", (Visibility == "Private" ? "off" : "on"));
                    html = html.Replace("@visibility_type", (Visibility == "Private" ? "not" : "now"));

                    string body = html;

                    Emailer obj_email = new Emailer();
                    string _val = obj_email.SendEmail(Freelancer_EmailId, "", "", "Trabau Notification", "Now We " + (Visibility == "Private" ? "don't" : "") + " see you – Profile Visibility", body, null);

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


                ltrlProfileVisibility.Text = ddlProfileVisibility.SelectedItem.Text;
                lblProfileVisibility.Text = Visibility;
                profile_visibility_icon_class.Attributes.Add("class", IconClass);
                upParent.Update();
                btnCloseProfileVisibilityPopup_top_Click(sender, e);
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "ProfileVisibility_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void lbtnEditAvailability_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_Profile_Availability.Visible = true;
            rbtnProfileAvailability.SelectedValue = ltrlProfileAvailability.Text;
            txtVacationTillDated.Text = ltrlVacationDate.Text;
            rbtnProfileAvailability_SelectedIndexChanged(sender, e);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Edit_ProfileAvailability_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_Profile_Availability');RadioButtonSelection_Override();ProfileAvailabilityEvents();CheckRadioButton();}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCloseProfile_Availability_popup_top_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_Profile_Availability.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_ProfileAvailability_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_Profile_Availability');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnUpdateProfileAvailability_Click(object sender, EventArgs e)
    {
        try
        {
            string Availability = rbtnProfileAvailability.SelectedValue;
            string VacationDate = "";

            if (Availability == "Vacation")
            {
                VacationDate = txtVacationTillDated.Text;
            }
            string UserID = Session["Trabau_UserId"].ToString();
            profile_updates obj = new profile_updates();

            string data = obj.UpdateProfileAvailability(Int64.Parse(UserID), Availability, Int64.Parse(UserID), VacationDate);

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];

            if (response == "success")
            {

                ltrlProfileAvailability.Text = Availability;
                string VacationDateText = "";
                if (Availability == "Vacation")
                {
                    string Month = VacationDate.Split('/')[0];
                    string Day = VacationDate.Split('/')[1];
                    string Year = VacationDate.Split('/')[2];
                    VacationDateText = Convert.ToDateTime(Day + "/" + Month + "/" + Year).ToString("dd MMM yyyy");

                    ltrlVacationDate.Text = VacationDate;
                }
                lblProfileAvailability.Text = (Availability == "Vacation" ? ("Vacation till " + VacationDateText) : Availability);

                try
                {
                    string Name = Session["Trabau_FirstName"].ToString();
                    string EmailId = Session["Trabau_EmailId"].ToString();
                    string template_url = "https://www.trabau.com/emailers/xxddcca/profile-availability-changes.html";

                    try
                    {
                        WebRequest req = WebRequest.Create(template_url);
                        WebResponse w_res = req.GetResponse();
                        StreamReader sr = new StreamReader(w_res.GetResponseStream());
                        string html = sr.ReadToEnd();

                        html = html.Replace("@Name", Name);
                        html = html.Replace("@availability_type", (Availability == "Available" ? "now" : "not"));
                        html = html.Replace("@availability", (Availability == "Available" ? "on" : "off"));

                        string body = html;

                        Emailer obj_email = new Emailer();
                        string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Ready (or not)?  – Profile Availability", body, null);

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
                }
                catch (Exception)
                {
                }


                upParent.Update();
                btnCloseProfile_Availability_popup_top_Click(sender, e);
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "ProfileAvailability_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void rbtnProfileAvailability_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Availability = rbtnProfileAvailability.SelectedValue;
            div_vacation_date.Visible = false;
            if (Availability == "Vacation")
            {
                div_vacation_date.Visible = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Edit_VacationDatePicker", "setTimeout(function () {ActivateDatePicker();}, 0);", true);
            }
        }
        catch (Exception)
        {
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Edit_ProfileAvailability", "setTimeout(function () {CheckRadioButton();}, 0);", true);
    }
}