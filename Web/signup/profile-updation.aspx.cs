using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL.SignUp;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.SignUp;
using System.Web.UI.HtmlControls;

public partial class Signup_profile_updation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Trabau_UserId"] == null)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
        if (!IsPostBack)
        {
            try
            {
                if (CheckEmailAddressVerificationStatus())
                {
                    if (Session["Trabau_UserType"].ToString() == "W")
                    {
                        ProfileLoad();
                    }
                    else
                    {
                        Response.Redirect("index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("../verification.aspx");
                }
            }
            catch (Exception)
            {

            }
        }
    }

    public bool CheckEmailAddressVerificationStatus()
    {
        bool val = false;
        try
        {
            DLL_UserValidation obj = new DLL_UserValidation();
            string UserID = Session["Trabau_UserId"].ToString();
            string _response = obj.CheckEmailAddressVerificationStatus(Int32.Parse(UserID));
            if (_response == "1")
            {
                val = true;
            }
        }
        catch (Exception)
        {
            val = false;
        }
        return val;
    }

    public void ProfileLoad()
    {
        if (!CheckProfileStatus())
        {
            div_signup_step1.Visible = true;
            div_signup_step2.Visible = false;
            BindServices();
        }
        else
        {
            div_signup_step1.Visible = false;
            div_signup_step2.Visible = true;
            GetUserEducationDetails();
            GetUserEmploymentDetails();

            LoadProfileDetails();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
    }

    public void LoadProfileDetails()
    {
        try
        {
            BLL_Registration obj = new BLL_Registration();
            string UserID = Session["Trabau_UserId"].ToString();
            Tuple<List<dynamic>, string> data = obj.LoadProfileDetails(Int64.Parse(UserID));
            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        txtTitle.Text = res[0].Title;
                        txtOverview.Text = res[0].Overview;
                        try { ddlEnglishProficiency.SelectedValue = res[0].EnglishProficiency; }
                        catch (Exception) { }
                        txtAddress.Text = res[0].Address;
                        txtCity.Text = res[0].CityName;
                        string CityId = res[0].CityId.ToString();
                        if (CityId != string.Empty && CityId != "0")
                        {
                            string key = "T_city";
                            CityId = EncyptSalt.EncryptText(CityId, key);
                        }
                        hfCityId.Value = CityId;
                        txtPostalCode.Text = res[0].PostalCode;
                        if (txtPostalCode.Text != string.Empty)
                        {
                            btnGoToSettings.Visible = true;
                        }
                        try
                        {
                            //byte[] pic_bytes = (byte[])(res[0].ProfilePicture);
                            string user_pic = ImageProcessing.GetUserPicture(Int64.Parse(UserID), 250, 200);
                            imgProfilePic.ImageUrl = user_pic;
                            imgProfilePic_Upload.ImageUrl = user_pic;

                        }
                        catch (Exception ex)
                        {
                        }

                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    public void GetUserEducationDetails()
    {
        try
        {
            BLL_Registration obj = new BLL_Registration();
            string UserID = Session["Trabau_UserId"].ToString();
            Tuple<List<dynamic>, string> data = obj.GetUserEducationDetails(Int64.Parse(UserID), 0);
            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        rEducation.DataSource = res.Select(a => new { FullDetails = a.FullDetails, YearDetails = a.YearDetails, EducationId = a.EducationId }).ToList();
                        rEducation.DataBind();
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    public bool CheckProfileStatus()
    {
        bool val = false;

        try
        {
            BLL_Registration obj = new BLL_Registration();
            string UserID = Session["Trabau_UserId"].ToString();
            val = obj.CheckProfile_Step(Int64.Parse(UserID));
        }
        catch (Exception)
        {

        }

        return val;
    }
    public void BindServices()
    {
        try
        {
            DLL_Registration obj = new DLL_Registration();
            DataSet ds_services = obj.GetServicesList(0);

            ddlServices.DataSource = ds_services;
            ddlServices.DataTextField = "Text";
            ddlServices.DataValueField = "Value";
            ddlServices.DataBind();

            ddlServices.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SignUp_Services", "setTimeout(function () {$('select[id*=ddlServices]').select2();}, 0);", true);
        }
        catch (Exception)
        {

        }
    }

    public void ExperienceLevels()
    {
        try
        {
            DLL_Registration obj = new DLL_Registration();
            DataSet ds_exp_levels = obj.GetExperienceLevels();

            ddlExperienceLevel.DataSource = ds_exp_levels;
            ddlExperienceLevel.DataTextField = "ExperienceName";
            ddlExperienceLevel.DataValueField = "Id";
            ddlExperienceLevel.DataBind();

            ddlExperienceLevel.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SignUp_Services", "setTimeout(function () {$('select[id*=ddlServices]').select2();}, 0);", true);
        }
        catch (Exception)
        {

        }
    }

    protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ServiceId = ddlServices.SelectedValue;
            if (ServiceId != "0" && ServiceId != "")
            {
                div_service_type.Visible = true;
                BLL_Registration obj = new BLL_Registration();
                Tuple<List<dynamic>, string> data = obj.GetServicesList(Int32.Parse(ServiceId));

                List<dynamic> services = data.Item1;
                var other_service = services.Where(a => a.Text == "Others").ToList();
                if (other_service != null)
                {
                    services.Remove(services.Where(a => a.Text == "Others"));
                    services.Add(other_service[0]);
                }
                rServiceType.DataSource = services.Select(a => new { Text = a.Text, Value = a.Value }).ToList();
                rServiceType.DataBind();

                ExperienceLevels();

                BindSkills();
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Skills_AutoComplete", "setTimeout(function () { LoadKeywords('txtSkills', 'hfSkills');AutoCompleteTextBox_Keywords('txtSkills', 'hfSkills', '', 'profile-updation.aspx/GetSkills', '::', '');}, 300);", true);
            }
            else
            {
                div_service_type.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SignUp_Services", "setTimeout(function () {$('select[id*=ddlServices]').select2();}, 0);", true);
        }
        catch (Exception)
        {
            div_service_type.Visible = false;
        }
    }

    public void BindSkills()
    {
        try
        {
            BLL_Registration obj = new BLL_Registration();
            Tuple<List<dynamic>, string> data = obj.GetSkillsList();
            List<dynamic> skills = data.Item1;
            if (skills != null)
            {
                ddlSkills.DataSource = skills.Select(a => new { Text = a.Text, Value = a.Value }).ToList();
                ddlSkills.DataTextField = "Text";
                ddlSkills.DataValueField = "Value";
                ddlSkills.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "open_skills_list", "setTimeout(function () {RegisterSelect2('ddlSkills', 'Select skills from the list', 'hfSkills','0');}, 100);", true);
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        try
        {
            string ServiceId = ddlServices.SelectedValue;
            DataTable dt_service_types = GetServiceTypes();
            if (dt_service_types.Rows.Count <= 4 && dt_service_types.Rows.Count > 0)
            {
                string services_type_xml = "";
                dt_service_types.TableName = "ServicesType";
                using (StringWriter sw = new StringWriter())
                {
                    dt_service_types.WriteXml(sw, false);
                    services_type_xml = ParseXpathString(sw.ToString());
                }
                string Skills = hfSkills.Value;
                if (Skills != string.Empty)
                {
                    string ExperienceLevel = ddlExperienceLevel.SelectedValue;
                    string UserId = Session["Trabau_UserId"].ToString();

                    BLL_Registration obj = new BLL_Registration();
                    Tuple<List<dynamic>, string> data = obj.User_SignUp(Int64.Parse(UserId), Int32.Parse(ServiceId), services_type_xml, Skills, Int32.Parse(ExperienceLevel));
                    if (data != null)
                    {
                        if (data.Item2 == "ok")
                        {
                            if (data.Item1 != null)
                            {
                                List<dynamic> res = data.Item1;

                                ProfileLoad();
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SignUp_Error_Message", "Swal.fire({type: 'error',  title: 'Please Enter your skills',  showConfirmButton: true,  timer: 1500}); ", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SignUp_Error_Message", "Swal.fire({type: 'error',  title: 'Please select upto 4 Service Type to continue',  showConfirmButton: true,  timer: 1500}); ", true);

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SignUp_Services", "setTimeout(function () {$('select[id*=ddlServices]').select2();}, 0);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_skills_list", "setTimeout(function () {RegisterSelect2('ddlSkills', 'Select skills from the list', 'hfSkills','0');}, 100);", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Skills_AutoComplete", "setTimeout(function () {LoadKeywords('txtSkills', 'hfSkills');AutoCompleteTextBox_Keywords('txtSkills', 'hfSkills', '', 'profile-updation.aspx/GetSkills', '::', '');}, 0);", true);
        }
        catch (Exception)
        {

        }
    }

    private string ParseXpathString(string input)
    {

        if (input.Contains("'"))
        {
            int myindex = input.IndexOf("'");
            input = input.Insert(myindex, "'");
        }
        return input;
    }

    public DataTable GetServiceTypes()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ServiceType", typeof(string));
        try
        {
            foreach (RepeaterItem item in rServiceType.Items)
            {
                if ((item.FindControl("chkServiceType") as CheckBox).Checked)
                {
                    DataRow dr = dt.NewRow();
                    dr["ServiceType"] = (item.FindControl("chkServiceType") as CheckBox).Text;

                    dt.Rows.Add(dr);
                }
            }
        }
        catch (Exception)
        {
        }
        return dt;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string[] GetSkills(string Prefix)
    {
        try
        {
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                DLL_Registration obj = new DLL_Registration();
                return obj.GetSkills(Prefix).ToArray();
            }
        }
        catch (Exception)
        {
            return null;
        }
        return null;
    }

    protected void btnUpdateProfile_Click(object sender, EventArgs e)
    {
        try
        {
            string Title = txtTitle.Text;
            string Overview = txtOverview.Text;
            string EnglishProficiency = ddlEnglishProficiency.SelectedValue;
            string Address = txtAddress.Text;
            string CityId = hfCityId.Value;
            if (CityId != string.Empty)
            {
                string key = "T_city";
                CityId = EncyptSalt.DecryptText(CityId, key);
                string PostalCode = txtPostalCode.Text;
                BLL_Registration obj = new BLL_Registration();
                string UserID = Session["Trabau_UserId"].ToString();
                string data = obj.UpdateUserProfile_Step2(Int64.Parse(UserID), Title, Overview, EnglishProficiency, Address, Int32.Parse(CityId), PostalCode);

                string response = data.Split(':')[0];
                string message = data.Split(':')[1];
                if (response == "success")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Profile_Update_Redirect", "setTimeout(function () {window.location.href='../profile/settings/'}, 3000);", true);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Profile_Update_Message", "Swal.fire({type: '" + response + "',  title: '" + message + "',  showConfirmButton: true,  timer: 1500});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Profile_Update_Message", "Swal.fire({type: 'error',  title: 'Please type City name and select from the list',  showConfirmButton: true,  timer: 1500});", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnAddEducation_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["EducationId"] = null;
            BindEducationYears();
            lblAddEducation_Header.Text = "Add Education";
            divTrabau_AddEducation.Visible = true;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Add_Education_Popup", "setTimeout(function () {$('select[id*=ddlEducationYear]').select2();HandlePopUp('1','divTrabau_AddEducation');}, 150);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    public void BindEducationYears()
    {
        ddlEducationYearFrom.Items.Clear();
        ddlEducationYearFrom.Items.Add(new ListItem { Text = "From", Value = "0" });
        for (int i = DateTime.Now.Year + 30; i >= DateTime.Now.Year - 75; i--)
        {
            ddlEducationYearFrom.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
        }

        ddlEducationYearTo.Items.Clear();
        ddlEducationYearTo.Items.Add(new ListItem { Text = "To", Value = "0" });
        for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 75; i--)
        {
            ddlEducationYearTo.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
        }
    }

    protected void btnSaveEducation_Click(object sender, EventArgs e)
    {
        try
        {
            string response = "error";
            string message = "Error while adding / saving education details, please refresh and try again";


            string SchoolName = txtSchoolName.Text;
            string YearFrom = ddlEducationYearFrom.SelectedValue;
            string YearTo = ddlEducationYearTo.SelectedValue;
            if (Int32.Parse(YearTo) >= Int32.Parse(YearFrom))
            {
                string Degree = txtEducationDegree.Text;
                string AreaOfStudy = txtEducationAreaOfStudy.Text;
                string Description = txtEducationDescription.Text;
                string UserID = Session["Trabau_UserId"].ToString();
                string EducationId = "0";
                try
                {
                    if (lblAddEducation_Header.Text.Contains("Edit Education"))
                    {
                        EducationId = ViewState["EducationId"].ToString();
                    }
                }
                catch (Exception)
                {
                }

                BLL_Registration obj = new BLL_Registration();
                string data = obj.UpdateUserEducation(SchoolName, Int32.Parse(YearFrom), Int32.Parse(YearTo), Degree, AreaOfStudy, Description, Int64.Parse(UserID), Int32.Parse(EducationId));

                response = data.Split(':')[0];
                message = data.Split(':')[1];
                if (response == "success")
                {

                    ClearEducation();

                    string source = ((Button)(sender)).CommandName;
                    if (source == "save_close" || ViewState["EducationId"] != null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Education_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_AddEducation');}, 150);", true);
                        divTrabau_AddEducation.Visible = false;
                    }

                    if (source == "save_close")
                    {
                        GetUserEducationDetails();
                        upSignUp.Update();
                    }

                    if (ViewState["EducationId"] != null)
                    {
                        ViewState["EducationId"] = null;
                    }
                }
            }
            else
            {
                response = "error";
                message = "Year From should be less than or equal to Year To";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Education_Update_Message", "Swal.fire({type: '" + response + "',  title: '" + message + "',  showConfirmButton: true,  timer: 1500});", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Add_Education_Popup", "setTimeout(function () {$('select[id*=ddlEducationYear]').select2();}, 150);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    public void ClearEducation()
    {
        txtSchoolName.Text = string.Empty;
        ddlEducationYearFrom.SelectedIndex = 0;
        ddlEducationYearTo.SelectedIndex = 0;
        txtEducationDegree.Text = string.Empty;
        txtEducationAreaOfStudy.Text = string.Empty;
        txtEducationDescription.Text = string.Empty;
    }


    protected void lbtnEditEducation_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_AddEducation.Visible = true;
            lblAddEducation_Header.Text = "Edit Education";

            BLL_Registration obj = new BLL_Registration();
            string UserID = Session["Trabau_UserId"].ToString();
            RepeaterItem item = ((LinkButton)(sender)).Parent as RepeaterItem;

            string EducationId = (item.FindControl("lblEducationId") as Label).Text;
            Tuple<List<dynamic>, string> data = obj.GetUserEducationDetails(Int64.Parse(UserID), Int32.Parse(EducationId));
            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        txtSchoolName.Text = res[0].SchoolName;
                        BindEducationYears();
                        ddlEducationYearFrom.SelectedValue = res[0].YearFrom.ToString();
                        ddlEducationYearTo.SelectedValue = res[0].YearTo.ToString();
                        txtEducationDegree.Text = res[0].Degree;
                        txtEducationAreaOfStudy.Text = res[0].AreaOfStudy;
                        txtEducationDescription.Text = res[0].Description;

                        chkWorkingStatus_CheckedChanged(sender, e);
                        ViewState["EducationId"] = EducationId;
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Education_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_AddEducation');}, 150);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Add_Education_Year", "setTimeout(function () {$('select[id*=ddlEducationYear]').select2();}, 150);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    public void BindCountries()
    {
        try
        {
            DLL_Registration obj = new DLL_Registration();
            DataSet ds_country = obj.GetCountryList();

            ddlCountry.DataSource = ds_country;
            ddlCountry.DataTextField = "Text";
            ddlCountry.DataValueField = "Value";
            ddlCountry.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    public void BindEmploymentYears()
    {
        ddlPeriodFrom_Year.Items.Clear();
        ddlPeriodFrom_Year.Items.Add(new ListItem { Text = "Year", Value = "0" });

        ddlPeriodTo_Year.Items.Clear();
        ddlPeriodTo_Year.Items.Add(new ListItem { Text = "Year", Value = "0" });

        for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 75; i--)
        {
            ddlPeriodFrom_Year.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
            ddlPeriodTo_Year.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
        }


        ddlPeriodFrom_Month.Items.Clear();
        ddlPeriodFrom_Month.Items.Add(new ListItem { Text = "Month", Value = "0" });

        ddlPeriodTo_Month.Items.Clear();
        ddlPeriodTo_Month.Items.Add(new ListItem { Text = "Month", Value = "0" });
        for (int i = 1; i <= 12; i++)
        {
            ddlPeriodFrom_Month.Items.Add(new ListItem { Text = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i), Value = i.ToString() });
            ddlPeriodTo_Month.Items.Add(new ListItem { Text = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i), Value = i.ToString() });
        }
    }

    public void BindEmploymentRoles()
    {
        try
        {
            DLL_Registration obj = new DLL_Registration();
            DataSet ds_Roles = obj.GetEmploymentRoles();
            ddlRole.DataSource = ds_Roles;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "Id";
            ddlRole.DataBind();

            ddlRole.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
        }
        catch (Exception)
        {

        }
    }

    protected void lbtnAddEmploymentHistory_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["EmploymentId"] = null;
            ltrlEmployment_Header.Text = "Add Employment";
            divTrabau_AddEmployment.Visible = true;

            BindCountries();
            BindEmploymentYears();
            BindEmploymentRoles();

            ClearEmployment();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Employment_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_AddEmployment');}, 150);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Employment_Select2", "setTimeout(function () {$('select[id*=ddlPeriod]').select2();}, 150);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    protected void btnSaveEmployment_Click(object sender, EventArgs e)
    {
        try
        {
            string CompanyName = txtCompanyName.Text;
            string CityName = txtCityName.Text;
            string CountryId = ddlCountry.SelectedValue;
            string Title = txtEmploymentTitle.Text;
            string RoleId = ddlRole.SelectedValue;
            string PeriodFrom_Month = ddlPeriodFrom_Month.SelectedValue;
            string PeriodFrom_Year = ddlPeriodFrom_Year.SelectedValue;
            string PeriodTo_Month = ddlPeriodTo_Month.SelectedValue;
            string PeriodTo_Year = ddlPeriodTo_Year.SelectedValue;
            bool WorkingHere = chkWorkingStatus.Checked;
            string Description = txtEmploymentDescription.Text;
            string EmploymentId = "0";
            try
            {
                if (ltrlEmployment_Header.Text.Contains("Edit Employment"))
                {
                    EmploymentId = ViewState["EmploymentId"].ToString();
                }
            }
            catch (Exception)
            {
            }
            string UserID = Session["Trabau_UserId"].ToString();

            BLL_Registration obj = new BLL_Registration();
            string data = obj.UpdateUserEmployment(CompanyName, CityName, Int32.Parse(CountryId), Title, Int32.Parse(RoleId), Int32.Parse(PeriodFrom_Month),
                Int32.Parse(PeriodFrom_Year), Int32.Parse(PeriodTo_Month), Int32.Parse(PeriodTo_Year), WorkingHere, Description, Int64.Parse(UserID),
                Int32.Parse(EmploymentId));

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];
            string source = ((Button)(sender)).CommandName;
            if (response == "success")
            {

                ClearEmployment();


                if (source == "save_close" || ViewState["EmploymentId"] != null)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Employment_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_AddEmployment');}, 150);", true);
                    divTrabau_AddEmployment.Visible = false;
                }

                if (source == "save_close")
                {
                    GetUserEmploymentDetails();
                    upSignUp.Update();
                }

                if (ViewState["EmploymentId"] != null)
                {

                    ViewState["EmploymentId"] = null;
                }

            }

            if (source != "save_close")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Employment_Select2", "setTimeout(function () {$('select[id*=ddlPeriod]').select2();}, 0);", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    public void ClearEmployment()
    {
        txtCompanyName.Text = string.Empty;
        txtCityName.Text = string.Empty;
        ddlCountry.SelectedIndex = 0;
        txtEmploymentTitle.Text = string.Empty;
        ddlRole.SelectedIndex = 0;
        ddlPeriodFrom_Month.SelectedIndex = 0;
        ddlPeriodFrom_Year.SelectedIndex = 0;
        ddlPeriodTo_Month.SelectedIndex = 0;
        ddlPeriodTo_Year.SelectedIndex = 0;
        div_Period_To_Month.Visible = true;
        div_Period_To_Year.Visible = true;
        chkWorkingStatus.Checked = false;
        txtEmploymentDescription.Text = string.Empty;
    }

    protected void chkWorkingStatus_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            div_Period_To_Month.Visible = !chkWorkingStatus.Checked;
            div_Period_To_Year.Visible = !chkWorkingStatus.Checked;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Employment_Select2", "setTimeout(function () {$('select[id*=ddlPeriod]').select2();}, 0);", true);
        }
        catch (Exception)
        {
        }
    }

    public void GetUserEmploymentDetails()
    {
        try
        {
            BLL_Registration obj = new BLL_Registration();
            string UserID = Session["Trabau_UserId"].ToString();
            Tuple<List<dynamic>, string> data = obj.GetUserEmploymentDetails(Int64.Parse(UserID), 0);
            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        rEmployment.DataSource = res.Select(a => new { FullDetails = a.FullDetails, YearDetails = a.YearDetails, EmploymentId = a.EmploymentId }).ToList();
                        rEmployment.DataBind();
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnEditEmployment_Click(object sender, EventArgs e)
    {
        try
        {
            divTrabau_AddEmployment.Visible = true;
            ltrlEmployment_Header.Text = "Edit Employment";

            BLL_Registration obj = new BLL_Registration();
            string UserID = Session["Trabau_UserId"].ToString();
            RepeaterItem item = ((LinkButton)(sender)).Parent as RepeaterItem;

            string EmploymentId = (item.FindControl("lblEmploymentId") as Label).Text;
            Tuple<List<dynamic>, string> data = obj.GetUserEmploymentDetails(Int64.Parse(UserID), Int32.Parse(EmploymentId));
            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        txtCompanyName.Text = res[0].CompanyName;
                        txtEmploymentTitle.Text = res[0].Title;
                        txtCityName.Text = res[0].CityName;
                        BindCountries();
                        ddlCountry.SelectedValue = res[0].CountryId.ToString();
                        BindEmploymentRoles();
                        ddlRole.SelectedValue = res[0].RoleId.ToString();
                        BindEmploymentYears();
                        ddlPeriodFrom_Month.SelectedValue = res[0].PeriodFrom_Month.ToString();
                        ddlPeriodFrom_Year.SelectedValue = res[0].PeriodFrom_Year.ToString();
                        ddlPeriodTo_Month.SelectedValue = res[0].PeriodTo_Month.ToString();
                        ddlPeriodTo_Year.SelectedValue = res[0].PeriodTo_Year.ToString();
                        txtEmploymentDescription.Text = res[0].Description;
                        chkWorkingStatus.Checked = res[0].WorkingHere;

                        ViewState["EmploymentId"] = EmploymentId;
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Employment_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_AddEmployment');}, 150);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Employment_Select2", "setTimeout(function () {$('select[id*=ddlPeriod]').select2();}, 150);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string[] GetCities_List(string Prefix)
    {
        try
        {
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                BLL_Registration obj = new BLL_Registration();
                List<string> data = obj.GetCities(Prefix);
                return data.ToArray();
            }
            else
            {
                return null;
            }
        }
        catch (Exception)
        {
            return null;
        }

    }

    protected void btnCloseEmployment_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Employment_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_AddEmployment');}, 150);", true);

            divTrabau_AddEmployment.Visible = false;
            GetUserEmploymentDetails();
            upSignUp.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    protected void btnCloseEducation_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Education_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_AddEducation');}, 150);", true);

            divTrabau_AddEducation.Visible = false;
            GetUserEducationDetails();
            upSignUp.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    protected void lbtnAddProfilePic_Click(object sender, EventArgs e)
    {
        try
        {
            div_profile_pic_popup.Visible = true;
            upSignUp.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenProfilePic_Popup", "setTimeout(function () {HandlePopUp('1','div_profile_pic_popup');}, 150);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCloseProfilePic_popup_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseProfilePic_Popup", "setTimeout(function () {HandlePopUp('0','div_profile_pic_popup');}, 0);", true);
            div_profile_pic_popup.Visible = false;
            upSignUp.Update();
        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveProfilePicture_Click(object sender, EventArgs e)
    {
        try
        {
            string cropped_pic = hfCropped_Picture.Value;
            imgProfilePic_Upload.ImageUrl = cropped_pic;
            byte[] photo = Convert.FromBase64String(cropped_pic.Replace("data:image/png;base64,", ""));
            photo = ScaleFreeHeight(photo, 400, 600);
            string UserID = Session["Trabau_UserId"].ToString();

            DLL_Registration obj = new DLL_Registration();
            string data = obj.UpdateUserProfilePic(Int64.Parse(UserID), photo);

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];
            if (response == "success")
            {
                hfCropped_Picture.Value = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseProfilePic_Popup", "setTimeout(function () {HandlePopUp('0','div_profile_pic_popup');}, 0);", true);
                div_profile_pic_popup.Visible = false;
                imgProfilePic.ImageUrl = "data:image;base64," + Convert.ToBase64String(photo);
                imgProfilePic_Upload.ImageUrl = "data:image;base64," + Convert.ToBase64String(photo);
                upSignUp.Update();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ProfilePic_Update_Message", "Swal.fire({type: '" + response + "',  title: '" + message + "',  showConfirmButton: true,  timer: 1500});", true);
        }
        catch (Exception)
        {
        }
    }

    public byte[] ScaleFreeHeight(byte[] bytes, int newWidth, int newHeight)
    {
        var newHeight2 = 0;
        System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
        System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);

        var image = returnImage;
        if (newHeight == 0)
        {
            newHeight2 = Convert.ToInt32(newWidth * (1.0000000 * image.Height / image.Width));
        }
        else
        {
            newHeight2 = newHeight;
        }
        newWidth = Convert.ToInt32(newHeight * (1.0000000 * image.Width / image.Height));
        var thumbnail = new Bitmap(newWidth, newHeight2);
        var graphic = Graphics.FromImage(thumbnail);
        graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphic.SmoothingMode = SmoothingMode.HighQuality;
        graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphic.CompositingQuality = CompositingQuality.HighQuality;

        graphic.DrawImage(image, 0, 0, newWidth, newHeight2);


        System.IO.MemoryStream objMemoryStream = new System.IO.MemoryStream();
        thumbnail.Save(objMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        byte[] imageContent = new byte[objMemoryStream.Length];
        objMemoryStream.Position = 0;
        objMemoryStream.Read(imageContent, 0, (int)objMemoryStream.Length);
        return imageContent;
    }

    protected void lbtnUploadProfilePicture_Click(object sender, EventArgs e)
    {

    }

    protected void btnGoToSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("../profile/settings/");
    }

}