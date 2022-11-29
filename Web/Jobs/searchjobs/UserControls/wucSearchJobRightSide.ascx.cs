using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary.BLL.SignUp;

public partial class Jobs_searchjobs_UserControls_wucSearchJobRightSide : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUserDetails();
        }
    }

    public void LoadUserDetails()
    {
        try
        {
            string UserType = Session["Trabau_UserType"].ToString();
            div_right_nav.Visible = (UserType == "W" ? true : false);
            if (div_right_nav.Visible)
            {
                BLL_Registration obj = new BLL_Registration();
                string UserID = Session["Trabau_UserId"].ToString();
                Tuple<List<dynamic>, string> data = obj.LoadProfileDetails(Int64.Parse(UserID));
                if (data != null)
                {
                    if (data.Item2 == "ok")
                    {
                        if (data.Item1 != null)
                        {

                            List<dynamic> res = data.Item1;
                            ltrlProfileVisibility.Text = res[0].VisibilityText;
                            lblProfileVisibility.Text = res[0].Visibility;
                            ltrlProfileAvailability.Text = res[0].Availability;
                            lblProfileAvailability.Text = res[0].AvailabilityText;
                            profile_visibility_icon_class.Attributes.Add("class", res[0].VisibilityIcon);

                            aProfile.HRef = "../../profile/user/profile.aspx";
                            aProposals.HRef = "../../jobs/proposals/index.aspx";
                            aProposals.InnerText = res[0].TotalProposals.ToString() + " submitted proposal(s)";
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }
}