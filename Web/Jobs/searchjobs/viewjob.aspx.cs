using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;

public partial class Jobs_searchjobs_viewjob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                GetJobDetails();
            }
            catch (Exception)
            {
            }
        }
    }

    public void GetJobDetails()
    {
        try
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["job"] != null)
                {
                    string JobId = Request.QueryString["job"];
                    if (JobId != string.Empty)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "GetJob_Details", "GetJobDetails('" + JobId + "');", true);
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }
}