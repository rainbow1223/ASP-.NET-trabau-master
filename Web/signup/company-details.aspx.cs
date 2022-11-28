using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary.BLL.SignUp;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.SignUp;

public partial class Signup_company_details : System.Web.UI.Page
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
                    if (Session["Trabau_UserType"].ToString() == "H")
                    {
                        string step = CheckStep();
                        if (step == "1")
                        {
                            CompanyLoad();
                        }
                        else if (step == "2")
                        {
                            CompanyLoad_Step2();
                        }
                        else if (step == "3")
                        {
                            CompanyLoad_Step3();
                        }
                        else
                        {
                            Response.Redirect("../profile/settings/index.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("../login.aspx");
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

    public void CompanyLoad()
    {
        try
        {
            div_signup_step1.Visible = true;
            LoadNumberOfEmployeeDetails();
            LoadDepartments();
            BindCountries();
        }
        catch (Exception)
        {

        }
    }

    public void CompanyLoad_Step2()
    {
        try
        {
            div_signup_step2.Visible = true;
            DisplayCategories();
        }
        catch (Exception)
        {

        }
    }

    public void CompanyLoad_Step3()
    {
        try
        {
            div_signup_step3.Visible = true;
        }
        catch (Exception)
        {

        }
    }

    public void DisplayCategories()
    {
        try
        {
            DLL_Registration obj = new DLL_Registration();
            DataSet ds_cat = obj.GetServicesList(0);
            rCategories.DataSource = ds_cat;
            rCategories.DataBind();
        }
        catch (Exception)
        {

        }
    }

    public string CheckStep()
    {
        string val = "1";
        try
        {
            DLL_Registration obj = new DLL_Registration();
            string UserID = Session["Trabau_UserId"].ToString();
            val = obj.GetCompanyUpdateStep(Convert.ToInt64(UserID));
        }
        catch (Exception)
        {
        }

        return val;
    }

    public void LoadNumberOfEmployeeDetails()
    {
        try
        {
            BLL_Registration obj = new BLL_Registration();
            Tuple<List<dynamic>, string> data = obj.GetEmployeeNumberDetails();
            rbtnlNoOfEmployees.DataSource = data.Item1.Select(a => new { Text = a.Description, Value = a.Id }).ToList();
            rbtnlNoOfEmployees.DataTextField = "Text";
            rbtnlNoOfEmployees.DataValueField = "Value";
            rbtnlNoOfEmployees.DataBind();
        }
        catch (Exception)
        {

        }
    }

    public void LoadDepartments()
    {
        try
        {
            BLL_Registration obj = new BLL_Registration();
            Tuple<List<dynamic>, string> data = obj.GetDepartments();
            rbtnlDepartment.DataSource = data.Item1.Select(a => new { Text = a.DepartmentName, Value = a.Id }).ToList();
            rbtnlDepartment.DataTextField = "Text";
            rbtnlDepartment.DataValueField = "Value";
            rbtnlDepartment.DataBind();
        }
        catch (Exception)
        {

        }
    }

    public void BindCountries()
    {
        try
        {
            BLL_Registration obj = new BLL_Registration();
            Tuple<List<dynamic>, string> data = obj.GetCountryList();

            ddlCountryCode.DataSource = data.Item1.Select(a => new { Value = a.Value, Text = a.CountryPrefix + " " + a.CountryCode }).ToList();
            ddlCountryCode.DataTextField = "Text";
            ddlCountryCode.DataValueField = "Value";
            ddlCountryCode.DataBind();
        }
        catch (Exception ex)
        {
        }
    }


    protected void rbtnlNoOfEmployees_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnlNoOfEmployees.SelectedValue != "1")
            {
                div_department.Visible = true;

            }
            else
            {
                div_department.Visible = false;
                div_phonenumber.Visible = false;
                rbtnlDepartment.SelectedIndex = -1;
            }
            upSignUp.Update();
        }
        catch (Exception)
        {
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            string EmployeeNumbersId = rbtnlNoOfEmployees.SelectedValue;
            string DepartmentId = (rbtnlDepartment.SelectedIndex == -1 ? "0" : rbtnlDepartment.SelectedValue);
            string CountryId = ddlCountryCode.SelectedValue;
            string PhoneNumber = txtPhoneNumber.Text;
            DLL_Registration obj = new DLL_Registration();
            string UserID = Session["Trabau_UserId"].ToString();

            string data = obj.UpdateUserCompanyDetails(Int32.Parse(EmployeeNumbersId), Int32.Parse(DepartmentId), Int32.Parse(CountryId),
                PhoneNumber, Convert.ToInt64(UserID), 0, "", 1);
            string response = data.Split(':')[0];
            string message = data.Split(':')[1];
            if (response == "success")
            {
                div_signup_step1.Visible = false;
                CompanyLoad_Step2();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Company_Update_Message", "Swal.fire({type: '" + response + "',  title: '" + message + "',  showConfirmButton: true,  timer: 1500});", true);
        }
        catch (Exception ex)
        {

        }
    }

    protected void rbtnlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            div_phonenumber.Visible = true;
        }
        catch (Exception)
        {
        }
    }

    protected void btnNext_Step2_Click(object sender, EventArgs e)
    {
        try
        {
            string CategoryId = hfCategoryId.Value;
            if (CategoryId != string.Empty)
            {
                DLL_Registration obj = new DLL_Registration();
                string UserID = Session["Trabau_UserId"].ToString();

                string data = obj.UpdateUserCompanyDetails(0, 0, 0, "", Convert.ToInt64(UserID), Int32.Parse(CategoryId), "", 2);
                string response = data.Split(':')[0];
                string message = data.Split(':')[1];
                if (response == "success")
                {
                    div_signup_step2.Visible = false;
                    CompanyLoad_Step3();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Company_Cat_Update_Message", "Swal.fire({type: '" + response + "',  title: '" + message + "',  showConfirmButton: true,  timer: 1500});", true);
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnNext_Step3_Click(object sender, EventArgs e)
    {
        try
        {
            string MinutesCategory = hf_minutes.Value;
            if (MinutesCategory != string.Empty)
            {
                DLL_Registration obj = new DLL_Registration();
                string UserID = Session["Trabau_UserId"].ToString();
                Session["MinutesCategory"] = MinutesCategory;
                string data = obj.UpdateUserCompanyDetails(0, 0, 0, "", Convert.ToInt64(UserID), 0, MinutesCategory, 3);
                string response = data.Split(':')[0];
                string message = data.Split(':')[1];
                if (response == "success")
                {
                    div_signup_step3.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirectionToSettings", "setTimeout(function () { window.location.href='../profile/settings/index.aspx';}, 2000);", true);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Company_Cat_Update_Message", "Swal.fire({type: '" + response + "',  title: '" + message + "',  showConfirmButton: true,  timer: 1500});", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Maintain_Minutes_Category", "$('#" + MinutesCategory + "').addClass('minutes-selected');", true);
            }
        }
        catch (Exception)
        {

        }
    }
}