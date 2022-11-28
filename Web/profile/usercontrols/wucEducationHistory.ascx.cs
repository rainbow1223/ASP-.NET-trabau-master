using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary.BLL.SignUp;

public partial class profile_usercontrols_wucEducationHistory : System.Web.UI.UserControl
{
    public string _UserID { get; set; }
    public bool _EditMode { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Register_Education_Events", "setTimeout(function () { LoadEducationEvents();}, 200);", true);
    }

    public void GetUserEducationDetails(string UserID, bool EditMode)
    {
        try
        {
            _UserID = UserID;
            _EditMode = EditMode;
            lbtnAddEducationHistory.Visible = _EditMode;
            BLL_Registration obj = new BLL_Registration();
            Tuple<List<dynamic>, string> data = obj.GetUserEducationDetails(Int64.Parse(_UserID), 0);
            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        rEducation.DataSource = res.Select(a => new { FullDetails = a.FullDetails, YearDetails = a.YearDetails, EducationId = a.EducationId, EditMode = _EditMode }).ToList();
                        rEducation.DataBind();
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnAddEducationHistory_Click(object sender, EventArgs e)
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

                        //chkWorkingStatus_CheckedChanged(sender, e);
                        ViewState["EducationId"] = EducationId;
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Education_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_AddEducation');}, 150);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Add_Education_Year", "setTimeout(function () {$('select[id*=ddlEducationYear]').select2();}, 150);", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    protected void btnCloseEducation_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Education_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_AddEducation');LoadEducationEvents();}, 150);", true);

            divTrabau_AddEducation.Visible = false;
            GetUserEducationDetails(_UserID, true);
            try { (this.Parent.FindControl("upParent") as UpdatePanel).Update(); }
            catch (Exception) { }

            try { (this.Parent.FindControl("upSignUp") as UpdatePanel).Update(); }
            catch (Exception) { }
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    protected void btnSaveEducation_Click(object sender, EventArgs e)
    {
        try
        {
            string SchoolName = txtSchoolName.Text;
            string YearFrom = ddlEducationYearFrom.SelectedValue;
            string YearTo = ddlEducationYearTo.SelectedValue;
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

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];
            if (response == "success")
            {

                ClearEducation();

                string source = ((Button)(sender)).CommandName;
                //if (source == "save_close" || ViewState["EducationId"] != null)
                //{
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Education_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_AddEducation');}, 150);", true);
                divTrabau_AddEducation.Visible = false;
                //}

                GetUserEducationDetails(_UserID, true);
                try { (this.Parent.FindControl("upParent") as UpdatePanel).Update(); }
                catch (Exception) { }

                try { (this.Parent.FindControl("upSignUp") as UpdatePanel).Update(); }
                catch (Exception) { }

                if (ViewState["EducationId"] != null)
                {
                    ViewState["EducationId"] = null;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Education_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Add_Education_Popup", "setTimeout(function () {$('select[id*=ddlEducationYear]').select2();LoadEducationEvents();}, 150);", true);
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCity','hfCityId','','profile-updation.aspx/GetCities_List','::','');}, 300);", true);
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
}