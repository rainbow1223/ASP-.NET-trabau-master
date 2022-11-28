using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.profile;

public partial class profile_usercontrols_wucsearch_profiles : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Search_Freelancers();
        }
    }

    public void Search_Freelancers()
    {
        try
        {
            int page_size = 5;
            freelancer_search obj = new freelancer_search();
            string UserID = "0";
            try
            {
                UserID = Session["Trabau_UserId"].ToString();
            }
            catch (Exception)
            {
            }
            string PageNumber = lblPageNumber.Text;
            string SearchText = lblSearchText.Text;
            string Category = DecryptFilters(lblCategory.Text);
            string HourlyRate = DecryptFilters(lblHourlyRate.Text);
            string JobSuccess = DecryptFilters(lblJobSuccess.Text);
            string EarnedAmount = DecryptFilters(lblEarnedAmount.Text);
            string Language = DecryptFilters(lblLanguage.Text);
            string ProfileType = DecryptFilters(lblProfileType.Text);
            string IPAddress = Request.UserHostAddress;

            DataSet ds_profiles = obj.Search_Freelancers(Int64.Parse(UserID), Int32.Parse(PageNumber), SearchText, Category, HourlyRate, JobSuccess, EarnedAmount, 
                Language, IPAddress, ProfileType);
            var profiles = ds_profiles.Tables[0].ToDynamic().Select(x => new
            {
                x.UserId,
                x.Title,
                x.EmailId,
                x.Name,
                x.CountryName,
                x.TotalEarning,
                x.JobSuccessRate,
                x.HourlyRate,
                x.Overview,
                profile_id = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.UserId.ToString(), Trabau_Keys.Profile_Key)),
                x.Preferred_List,
                x.Preferred_ListClass,
                x.CanAdd_PreferList,
                x.Preferred_ListTooltip
            }).ToList();

            rFreelancer_Profiles.DataSource = profiles;
            rFreelancer_Profiles.DataBind();

            div_no_result.Visible = (rFreelancer_Profiles.Items.Count == 0 && PageNumber == "1" ? true : false);
            if (rFreelancer_Profiles.Items.Count > 0)
            {
                int prev_items_count = page_size * (Int32.Parse(PageNumber) - 1);
                foreach (RepeaterItem item in rFreelancer_Profiles.Items)
                {
                    string _UserId = (item.FindControl("lblUserId") as Label).Text;
                    DataView dv_skills = ds_profiles.Tables[1].DefaultView;
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
                    (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data-key", ran + "_userpic_" + (prev_items_count + item.ItemIndex + 1000).ToString());
                    (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data", _UserId);
                    (item.FindControl("imgFL_ProfilePic") as HtmlImage).Src = "../../assets/uploads/loading_pic.gif";
                }

                Session["ProfilesResultFound"] = "1";
            }
            else
            {
                //div_more_profiles.Visible = true;
                Session["ProfilesResultFound"] = "0";
            }
        }
        catch (Exception ex)
        {
        }
    }


    public string DecryptFilters(string filter)
    {
        string _filter = "";
        try
        {
            for (int i = 0; i < filter.Split(',').Length; i++)
            {
                if (_filter == "")
                {
                    _filter = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(filter.Split(',')[i]), Trabau_Keys.Filter_Key);
                }
                else
                {
                    _filter = _filter + "," + EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(filter.Split(',')[i]), Trabau_Keys.Filter_Key);
                }
            }
        }
        catch (Exception ex)
        {
        }

        return _filter;
    }
}