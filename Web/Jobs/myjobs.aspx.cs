using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_myjobs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetMyJobs();
        }
    }

    public void GetMyJobs()
    {
        try
        {
            my_jobs obj = new my_jobs();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet dsjobs = obj.GetMyJobs(Int64.Parse(UserID));
            rMyJobs.DataSource = dsjobs;
            rMyJobs.DataBind();
        }
        catch (Exception)
        {
        }
    }
}