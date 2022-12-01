using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL;

public partial class hire_categorywise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["Trabau_UserId"] != null)
                {
                    div_signup.Visible = false;
                }

                if (Request.QueryString.Count == 1)
                {
                    if (Request.QueryString["category"] != null)
                    {
                        string category = Request.QueryString["category"];
                        if (category != string.Empty)
                        {
                            category = MiscFunctions.Base64DecodingMethod(category);
                            category = EncyptSalt.DecryptString(category, Trabau_Keys.Category_Key);

                            string CategoryId = category.Split('|')[0];
                            string CategoryName = category.Split('|')[1];
                            ltrlCategoryName.Text = CategoryName;
                            ltrlCat_Name.Text = CategoryName;
                            this.Title = "Highest Rated Freelancers for " + CategoryName + " - Trabau";
                            GetHourlyRateFilter();
                            GetJobSuccessFilter();


                            DisplayHighestRated_Freelancers(Int32.Parse(CategoryId), 0, "0", "0");
                        }
                        else
                        {
                            Response.Redirect("hire.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("hire.aspx");
                    }
                }
                else
                {
                    Response.Redirect("hire.aspx");
                }
            }
            catch (Exception)
            {
                Response.Redirect("hire.aspx");
            }
        }
    }

    public void GetHourlyRateFilter()
    {
        try
        {
            utility obj = new utility();
            DataSet ds_hr = obj.GetHourlyRateFilter();
            ddlHourlyRate.DataSource = ds_hr;
            ddlHourlyRate.DataTextField = "Text";
            ddlHourlyRate.DataValueField = "Value";
            ddlHourlyRate.DataBind();
        }
        catch (Exception)
        {
        }
    }

    public void GetJobSuccessFilter()
    {
        try
        {
            utility obj = new utility();
            DataSet ds_hr = obj.GetJobSuccessFilter();
            ddlJobSuccess.DataSource = ds_hr;
            ddlJobSuccess.DataTextField = "Text";
            ddlJobSuccess.DataValueField = "Value";
            ddlJobSuccess.DataBind();
        }
        catch (Exception)
        {
        }
    }

    public void DisplayHighestRated_Freelancers(int CategoryId, int CountryId, string HourlyRate_Order, string JobSuccess)
    {
        try
        {
            string UserId = "";
            try
            {
                UserId = Session["Trabau_UserId"].ToString();
            }
            catch (Exception)
            {
                UserId = "0";
            }


            utility obj = new utility();
            DataSet ds_freelancers = obj.GetHighestRated_Freelancers(Int64.Parse(UserId), CategoryId, 0, CountryId, HourlyRate_Order, JobSuccess);

            var ls_freelancers = ds_freelancers.Tables[0].ToDynamic().Select(x => new
            {
                x.UserId,
                x.Name,
                x.Title,
                x.CountryName,
                x.CountryId,
                x.JobSuccessRate,
                x.HourlyRate,
                profile_id = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.UserId.ToString(), Trabau_Keys.Profile_Key)),
                x.Preferred_List,
                x.Preferred_ListClass,
                x.CanAdd
            }).ToList().Where(y => y.CountryId == CountryId || CountryId == 0);
            rFreelancers.DataSource = ls_freelancers;
            rFreelancers.DataBind();


            if (HourlyRate_Order == "LH")
            {
                ls_freelancers = ls_freelancers.OrderBy(x => x.HourlyRate);
            }
            else if (HourlyRate_Order == "HL")
            {
                ls_freelancers = ls_freelancers.OrderByDescending(x => x.HourlyRate);
            }
            if (ddlCountry.Items.Count == 0)
            {
                var country_list = ls_freelancers.Select(x => new { x.CountryName, x.CountryId }).Distinct();

                ddlCountry.DataSource = country_list;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem { Text = "Any Country", Value = "0" });
            }

            foreach (RepeaterItem item in rFreelancers.Items)
            {
                string _UserId = (item.FindControl("lblUserId") as Label).Text;
                DataView dv_skills = ds_freelancers.Tables[1].DefaultView;
                dv_skills.RowFilter = "UserId=" + _UserId;
                var skills = dv_skills.ToTable().ToDynamic().Select(x => new
                {
                    x.SkillName,
                    EncSkillId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.SkillId.ToString() + "|" + x.SkillName, Trabau_Keys.Skill_Key))
                }).ToList();

                Repeater rFreelancers_Skills = item.FindControl("rFreelancers_Skills") as Repeater;
                rFreelancers_Skills.DataSource = skills;
                rFreelancers_Skills.DataBind();


                if (_UserId != "")
                {
                    _UserId = _UserId + "~" + DateTime.Now.ToString();
                    _UserId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(_UserId, Trabau_Keys.Profile_Key));
                }

                string ran = Guid.NewGuid().ToString();
                (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data-key", ran + "_userpic_" + (item.ItemIndex + 1000).ToString());
                (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data", _UserId);
                (item.FindControl("imgFL_ProfilePic") as HtmlImage).Src = "assets/uploads/loading_pic.gif";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GetTrabauPic_Info", "setTimeout(function () { GetTrabau_PicInfo('1000','" + (rFreelancers.Items.Count + 1000).ToString() + "');}, 0);", true);
        }
        catch (Exception ex)
        {
        }
    }


    public void Filter_Freelancers()
    {
        try
        {
            string CountryId = ddlCountry.SelectedValue;
            string HourlyRate = ddlHourlyRate.SelectedValue;
            string JobSuccess = ddlJobSuccess.SelectedValue;
            DisplayHighestRated_Freelancers(0, Int32.Parse(CountryId), HourlyRate, JobSuccess);
        }
        catch (Exception)
        {
        }
    }

    protected void ddlHourlyRate_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filter_Freelancers();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filter_Freelancers();
    }

    protected void ddlJobSuccess_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filter_Freelancers();
    }
}