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

public partial class profile_preferlist_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["Trabau_UserType"].ToString() == "W")
                {
                    this.Title = "Prefer List - Trabau";
                    h4_heading.InnerHtml = "My Preferred Clients";
                }
                else
                {
                    h4_heading.InnerHtml = "My Preferred Freelancers, Contractors, & Agencies";
                }

                GetPreferList();
            }
            catch (Exception)
            {
            }


        }
    }

    public void GetPreferList()
    {
        try
        {
            string UserType = Session["Trabau_UserType"].ToString();
            string UserID = Session["Trabau_UserId"].ToString();
            preferlist obj = new preferlist();
            DataSet ds_profiles = obj.GetPreferList(Int64.Parse(UserID));
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
                CanView = (UserType == "H" ? true : false),
                x.CompanyWebsite,
                x.Preferred_ListTooltip
            }).ToList();

            rFreelancer_Profiles.DataSource = profiles;
            rFreelancer_Profiles.DataBind();

            div_no_result.Visible = (rFreelancer_Profiles.Items.Count == 0 ? true : false);
            if (rFreelancer_Profiles.Items.Count > 0)
            {
                foreach (RepeaterItem item in rFreelancer_Profiles.Items)
                {
                    string _UserId = (item.FindControl("lblUserId") as Label).Text;
                    if (UserType == "H")
                    {
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
                    }

                    if (_UserId != "")
                    {
                        _UserId = _UserId + "~" + DateTime.Now.ToString();
                        _UserId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(_UserId, Trabau_Keys.Profile_Key));
                    }

                    string ran = Guid.NewGuid().ToString();
                    (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data-key", ran + "_userpic_" + (item.ItemIndex + 1000).ToString());
                    (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data", _UserId);
                    (item.FindControl("imgFL_ProfilePic") as HtmlImage).Src = "../../assets/uploads/loading_pic.gif";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "GetTrabauPic_Info", "setTimeout(function () { GetTrabau_PicInfo('1000','" + (rFreelancer_Profiles.Items.Count + 1000).ToString() + "');}, 0);", true);
            }
        }
        catch (Exception)
        {
        }
    }
}