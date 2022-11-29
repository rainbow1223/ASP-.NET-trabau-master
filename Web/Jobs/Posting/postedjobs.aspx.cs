using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_Posting_postedjobs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayPostedJobs();
        }
    }

    public void DisplayPostedJobs()
    {
        try
        {
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds_jobs = obj.GetPostedJobs(Int32.Parse(UserID));
            rPostedJobs.DataSource = ds_jobs.Tables[0];
            rPostedJobs.DataBind();

            try
            {
                DataView dv_menu = ds_jobs.Tables[1].DefaultView;

                foreach (RepeaterItem item in rPostedJobs.Items)
                {
                    string JobId = (item.FindControl("lblJobId") as Label).Text;
                    dv_menu.RowFilter = "JobId=" + JobId;

                    var menu = dv_menu.ToTable().ToDynamic().Select(x => new
                    {
                        JobId = x.JobId,
                        Name = x.Name,
                        MenuCode = x.MenuCode
                    }).Where(r => r.JobId.ToString() == JobId).ToList();

                    //dv_menu.ToTable().Select(string.Format("[JobId] = {0} and Name = 'Edit posting'", JobId))
                    // .ToList<DataRow>()
                    // .ForEach(r =>
                    // {
                    //     r["RedirectURL"] = Enc_JobId;
                    // });


                    Repeater rPostedJobMenu = (item.FindControl("rPostedJobMenu") as Repeater);
                    rPostedJobMenu.DataSource = menu;
                    rPostedJobMenu.DataBind();

                }
            }
            catch (Exception ex)
            {
            }

        }
        catch (Exception)
        {
        }
    }

    protected void lbtnMenuLink_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string JobId = (item.FindControl("lblJobId") as Label).Text;
            string MenuCode = (item.FindControl("lblMenuCode") as Label).Text;

            if (MenuCode == "EP")
            {
                Session["PostedJob_JobId"] = JobId;
                Response.Redirect("postjob.aspx");
            }
            else if (MenuCode == "VJP")
            {
                Session["PostedJob_JobId"] = JobId;
                Response.Redirect("jobposting.aspx");
            }
        }
        catch (Exception)
        {

        }
    }
}