using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.profile.settings;

public partial class index_master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Header.DataBind();
            if (CheckProtection())
            {
                div_home.Visible = true;

                try
                {
                    string RawURL = Request.RawUrl;

                    string CurrentURL = Request.RawUrl.ToString().ToLower();
                    if (CurrentURL.IndexOf('/') == 0)
                    {
                        CurrentURL = CurrentURL.Substring(1, CurrentURL.Length - 1);
                    }
                    CurrentURL = Uri.UnescapeDataString(CurrentURL);

                    if (CurrentURL.Contains("userprofile.aspx?profile=") || CurrentURL == "hire.aspx" || CurrentURL.Contains("hire-categorywise.aspx?category=")
                        || CurrentURL.Contains("hire-skillwise.aspx?skill=") || CurrentURL == "how-it-works.aspx" || CurrentURL == "index.aspx"
                        || CurrentURL.Contains("profile/search.aspx") || CurrentURL.Contains("profile/search-jobs.aspx"))
                    {
                        if (Session["Trabau_UserId"] != null)
                        {
                            li_menu.Visible = true;
                            li_login.Visible = false;
                            li_signup.Visible = false;

                            string UserType = Session["Trabau_UserType"].ToString();

                            if (UserType != "H")
                            {
                                li_postjob.Visible = false;
                            }

                            string Primary_UserID = Session["Trabau_Primary_UserId"].ToString();
                            settings_changes obj = new settings_changes();
                            DataSet ds = obj.GetUserAccounts(Int64.Parse(Primary_UserID));

                            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            //{
                            //    if (ds.Tables[0].Rows[i]["UserId"].ToString() == Session["Trabau_UserId"].ToString())
                            //    {
                            //        // ds.Tables[0].Rows[i]["AccStatus"] = "dropdown-item account-active";
                            //    }
                            //}
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
                        }
                    }
                }
                catch (Exception)
                {
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Protection_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_Protection');}, 150);", true);
            }
        }
    }

    public bool CheckProtection()
    {
        bool val = false;
        try
        {
            if (Session["Trabau_Protection"] != null)
            {
                if (Session["Trabau_Protection"].ToString() == "ok")
                {
                    val = true;
                }
            }
            //val = true;
        }
        catch (Exception)
        {

        }
        return val;
    }

    protected void btnValidateProtection_Click(object sender, EventArgs e)
    {
        DateTime startDate = Convert.ToDateTime("09/07/2022 12:00");
        DateTime todayDate = DateTime.Now;
        if (txtProtection_Password.Text == "admin_123@" || (txtProtection_Password.Text == "trabau@123!!.." && (todayDate - startDate).Days <= 180))
        {
            Session["Trabau_Protection"] = "ok";
            Response.Redirect(Request.RawUrl);
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
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "SwitchAccounts", "setTimeout(function () { window.location.href=window.location.href;}, 50);", true);
            Response.Redirect(_url);
        }
        catch (Exception ex)
        {
        }
    }
}
