using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL.SignUp;
using TrabauClassLibrary.DLL.profile;
using TrabauClassLibrary.DLL.SignUp;

public partial class profile_user_userprofile : System.Web.UI.Page
{

    protected void Page_PreInit(Object sender, EventArgs e)
    {
        try
        {
            if (Session["Trabau_UserId"] != null)
            {
                this.MasterPageFile = "~/main.master";
            }
        }
        catch (Exception)
        {
        }
       
        if (!IsPostBack)
        {
            
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                if (Request.QueryString.Count == 1)
                {
                    if (Request.QueryString["profile"] != null)
                    {
                        if (Request.QueryString["profile"] != string.Empty)
                        {
                            string profile_id = Request.QueryString["profile"];
                            profile_id = MiscFunctions.Base64DecodingMethod(profile_id);
                            profile_id = EncyptSalt.DecryptString(profile_id, Trabau_Keys.Profile_Key);
                            LoadProfile(profile_id, false);
                        }
                        else
                        {
                            upParent.Visible = false;
                            div_profile_not_avail.Visible = true;
                            div_profile_not_avail.InnerHtml = "Trabau profile link is invalid or doesn't exists";
                            this.Page.Title = "Trabau Freelancer";
                        }
                    }
                    else
                    {
                        upParent.Visible = false;
                        div_profile_not_avail.Visible = true;
                        div_profile_not_avail.InnerHtml = "Trabau profile link is invalid or doesn't exists";
                        this.Page.Title = "Trabau Freelancer";
                    }
                }
                else
                {
                    upParent.Visible = false;
                    div_profile_not_avail.Visible = true;
                    div_profile_not_avail.InnerHtml = "Trabau profile link is invalid or doesn't exists";
                    this.Page.Title = "Trabau Freelancer";
                }

            }
            catch (Exception)
            {
                upParent.Visible = false;
                div_profile_not_avail.Visible = true;
                div_profile_not_avail.InnerHtml = "Trabau profile link is invalid or doesn't exists";
                this.Page.Title = "Trabau Freelancer";
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Register_Load_Events", "setTimeout(function () { ProfileLoad_Events();}, 200);", true);
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

                        string Visibility = res[0].Visibility;
                        string Visibility_AllowedText = string.Empty;
                        if (Visibility == "Trabau" && Session["Trabau_UserId"] == null)
                        {
                            Visibility_AllowedText = "This freelancer's profile is only available to Trabau users. Please <a href='../../login.aspx'>login</a> or <a href='../../signup/index.aspx'>sign up</a> to view their profile.";
                        }
                        else if (Visibility == "Private")
                        {
                            Visibility_AllowedText = "This freelancer's profile is not available publicly.";
                        }

                        if (Visibility_AllowedText == string.Empty)
                        {
                            try
                            {
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

                                lblProfileAvailability.Text = res[0].AvailabilityText;
                            }
                            catch (Exception)
                            {
                            }
                            DisplayProfileSkills(UserID);

                            wucEmploymentHistory.GetUserEmploymentDetails(UserID, EditMode);
                            wucEducationHistory.GetUserEducationDetails(UserID, EditMode);

                            DisplayPortfolios(UserID);

                            this.Page.Title = ltrl_profile_name.Text + " - " + res[0].Title + " - Trabau Freelancer from " + res[0].CityFullName;
                        }
                        else
                        {
                            upParent.Visible = false;
                            div_profile_not_avail.Visible = true;
                            div_profile_not_avail.InnerHtml = Visibility_AllowedText;
                            this.Page.Title = "Trabau Freelancer";
                        }
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


}