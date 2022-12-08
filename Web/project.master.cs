using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.Authorization;
using TrabauClassLibrary.DLL.profile.settings;

public partial class project : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["Trabau_UserId"] == null)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
        else
        {
            CheckAccess();
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

    public void CheckAccess()
    {
        try
        {
            bool IsAccess = false;

            string UserType = Session["Trabau_UserType"].ToString();
            string CurrentURL = Request.RawUrl.ToString().ToLower();
            if (CurrentURL.IndexOf('/') == 0)
            {
                CurrentURL = CurrentURL.Substring(1, CurrentURL.Length - 1);
            }
            CurrentURL = Uri.UnescapeDataString(CurrentURL);

            bool EmailAddressVerfied = CheckEmailAddressVerificationStatus();


            if (UserType == "H")
            {
                a_logo.HRef = "jobs/posting/postedjobs.aspx";
            }
            else if (UserType == "W")
            {
                a_logo.HRef = "jobs/searchjobs/index.aspx";
            }
            else if (UserType == "A")
            {
                a_logo.HRef = "admin/citymaster.aspx";
            }

            IsAccess = ValidateAccess.Validate(CurrentURL, EmailAddressVerfied, UserType);

            if (!IsAccess)
            {
                Response.Redirect("~/error.aspx");
            }
            else
            {
                if (UserType == "W")
                {
                    li_menu_findwork.Visible = true;
                    //   li_menu_myjobs.Visible = true;
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Trabau_UserId"] == null)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
        if (!IsPostBack)
        {
            Page.Header.DataBind();

            try
            {
                if (Session["Temp_Preview_FilePath"] != null)
                {
                    File.Delete(Session["Temp_Preview_FilePath"].ToString());

                    if (!File.Exists(Session["Temp_Preview_FilePath"].ToString()))
                    {
                        Session["Temp_Preview_FilePath"] = null;
                    }
                }
            }
            catch (Exception)
            {

            }

            try
            {
                string UserType = Session["Trabau_UserType"].ToString();

                //div_menu_job.Visible = (UserType == "H" ? true : false);
                li_myjobs.Visible = (UserType == "H" ? true : false);
                li_admin.Visible = (UserType == "A" ? true : false);
                // li_domain.Visible = (UserType == "H" ? true : false);
                //li_projectmodel.Visible = (UserType == "H" ? true : false);

                string RawURL = Request.RawUrl;
                if (RawURL.Contains("signup/profile-updation") || RawURL.Contains("signup/company-details"))
                {
                    ltrlName.Text = Session["Trabau_FirstName"].ToString();
                    li_welcome.Visible = true;
                    li_logout.Visible = true;
                }
                else
                {
                    // ltrl_Name.Text = Session["Trabau_FirstName"].ToString();
                    li_menu.Visible = true;

                    string Primary_UserID = Session["Trabau_Primary_UserId"].ToString();
                    settings_changes obj = new settings_changes();
                    DataSet ds = obj.GetUserAccounts(Int64.Parse(Primary_UserID));

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["UserId"].ToString() == Session["Trabau_UserId"].ToString())
                        {
                            // ds.Tables[0].Rows[i]["AccStatus"] = "dropdown-item account-active";
                        }
                    }
                    rAccounts.DataSource = ds;
                    rAccounts.DataBind();
                    foreach (RepeaterItem item in rAccounts.Items)
                    {
                        string _UserId = (item.FindControl("lblUserId") as Label).Text;
                        string _user_pic = ImageProcessing.GetUserPicture(Int64.Parse(_UserId), 133, 100);
                        (item.FindControl("img_profile_pic") as HtmlImage).Src = _user_pic;
                        (item.FindControl("divUserType") as HtmlControl).Attributes.Add("class", _UserId == Session["Trabau_UserId"].ToString() ? "org-type ellipsis" : "ellipsis");
                        //  (item.FindControl("lbtnSwitchAccount") as LinkButton).Attributes.Add("class", (_UserId == Session["Trabau_UserId"].ToString() ? "dropdown-item account-active" : "dropdown-item"));
                        if (_UserId == Session["Trabau_UserId"].ToString())
                        {
                            img_profile_pic_main.Src = _user_pic;
                        }
                    }
                    //string UserID = Session["Trabau_UserId"].ToString();
                    //string user_pic = ImageProcessing.GetUserPicture(Int64.Parse(Primary_UserID), 133, 100);
                    //img_profile_pic_main.Src = user_pic;
                    //img_profile_pic.Src = user_pic;
                    //string UserType = Session["Trabau_UserType"].ToString();
                    //if (UserType == "W")
                    //{
                    //    div_usertype.InnerHtml = "Freelancer";
                    //}
                    //else if (UserType == "H")
                    //{
                    //    div_usertype.InnerHtml = "Client";
                    //}
                }

            }
            catch (Exception)
            {

            }

        }
    }

    protected void lbtnSwitchAccount_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem row = (sender as LinkButton).Parent as RepeaterItem;
            string UserId = (row.FindControl("lblUserId") as Label).Text;
            string UserType = (row.FindControl("lblUserType") as Label).Text;
            string FirstName = (row.FindControl("lblFirstName") as Label).Text;
            string FullName = (row.FindControl("lblFullName") as Label).Text;


            Session["Trabau_UserId"] = UserId;
            Session["Trabau_UserType"] = UserType;
            Session["Trabau_FirstName"] = FirstName;
            Session["Trabau_FullName"] = FullName;

            string _url = "";
            if (UserType == "H")
            {

                _url = "http://" + Request.Url.Authority + "/jobs/posting/postedjobs.aspx";
            }
            else
            {
                _url = "http://" + Request.Url.Authority + "/jobs/searchjobs/index.aspx";
            }

            Response.Redirect(_url);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "SwitchAccounts", "setTimeout(function () { window.location.href='" + _url + "';}, 50);", true);
        }
        catch (Exception ex)
        {
        }
    }
}
