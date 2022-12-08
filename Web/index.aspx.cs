using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Trabau_UserId"] == null)
            {
                div_browsejobs.Visible = true;
                a_browse_jobs.HRef = "search-jobs.aspx";
            }
            else if (Session["Trabau_UserType"].ToString() == "W")
            {
                div_browsejobs.Visible = true;
                a_browse_jobs.HRef = "jobs/searchjobs/index.aspx";
            }
            else
            {
                div_browsejobs.Visible = false;
            }
        }
    }
}