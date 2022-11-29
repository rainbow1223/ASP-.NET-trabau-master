using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_proposals_UserControls_wucProposals : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllProposals();
        }
    }

    public void GetAllProposals()
    {
        try
        {
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds_proposals = obj.GetJobSubmittedAllProposals(Int64.Parse(UserID));

            var proposals = ds_proposals.Tables[0].ToDynamic().Select(x => new
            {
                JobId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(x.JobId.ToString(), Trabau_Keys.Job_Key)),
                x.UserId,
                x.Title,
                x.EmailId,
                x.Name,
                x.CountryName,
                x.CoverLetter,
                x.BIDAmount,
                x.TotalEarning,
                x.JobSuccessRate,
                x.Hired,
                x.HiredText,
                x.Invited,
                x.InvitedText,
                x.Saved,
                x.SavedText,
                HiredClass = x.Hired == true ? "disabled" : "",
                DeclinedClass = x.Declined == true ? "disabled" : "",
                x.Declined,
                x.DeclinedText,
                x.JobTitle
            }).ToList();

            rProposals.DataSource = proposals;
            rProposals.DataBind();



            foreach (RepeaterItem item in rProposals.Items)
            {
                string _UserId = (item.FindControl("lblUserId") as Label).Text;
                DataView dv_skills = ds_proposals.Tables[1].DefaultView;
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
                (item.FindControl("imgFL_ProfilePic") as HtmlImage).Src = "../../assets/uploads/loading_pic.gif";
            }
        }
        catch (Exception)
        {
        }
    }
}