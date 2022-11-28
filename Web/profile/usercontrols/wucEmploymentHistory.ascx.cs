using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary.BLL.SignUp;
using TrabauClassLibrary.DLL.SignUp;

public partial class profile_usercontrols_wucEmploymentHistory : System.Web.UI.UserControl
{
    public string _UserID { get; set; }
    public bool _EditMode { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Register_Employment_Events", "setTimeout(function () { LoadEmploymentEvents();}, 200);", true);
    }

    public void GetUserEmploymentDetails(string UserID, bool EditMode)
    {
        try
        {
            lbtnAddEmploymentHistory.Visible = EditMode;
            _UserID = UserID;
            _EditMode = EditMode;
            BLL_Registration obj = new BLL_Registration();
            Tuple<List<dynamic>, string> data = obj.GetUserEmploymentDetails(Int64.Parse(_UserID), 0);
            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        rEmployment.DataSource = res.Select(a => new { FullDetails = a.FullDetails, YearDetails = a.YearDetails, EmploymentId = a.EmploymentId, EditMode = _EditMode }).ToList();
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
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Open_Employment_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_AddEmployment');}, 150);", true);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Employment_Select2", "setTimeout(function () {$('select[id*=ddlPeriod]').select2();}, 150);", true);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
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

    protected void btnCloseEmployment_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Employment_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_AddEmployment');}, 150);", true);

            divTrabau_AddEmployment.Visible = false;
            GetUserEmploymentDetails(_UserID, true);
            try { (this.Parent.FindControl("upParent") as UpdatePanel).Update(); }
            catch (Exception) { }

            try { (this.Parent.FindControl("upSignUp") as UpdatePanel).Update(); }
            catch (Exception) { }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
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


                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Close_Employment_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_AddEmployment');}, 150);", true);
                divTrabau_AddEmployment.Visible = false;

                if (source == "save_close")
                {


                    //  upSignUp.Update();
                }

                GetUserEmploymentDetails(_UserID, true);
                try { (this.Parent.FindControl("upParent") as UpdatePanel).Update(); }
                catch (Exception) { }

                try { (this.Parent.FindControl("upSignUp") as UpdatePanel).Update(); }
                catch (Exception) { }

                if (ViewState["EmploymentId"] != null)
                {

                    ViewState["EmploymentId"] = null;
                }

            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Employment_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);

            if (source != "save_close")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Employment_Select2", "setTimeout(function () {$('select[id*=ddlPeriod]').select2();}, 0);", true);
            }
            //comment ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    protected void chkWorkingStatus_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            div_Period_To_Month.Visible = !chkWorkingStatus.Checked;
            div_Period_To_Year.Visible = !chkWorkingStatus.Checked;

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Employment_Select2", "setTimeout(function () {$('select[id*=ddlPeriod]').select2();}, 0);", true);
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
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }
}