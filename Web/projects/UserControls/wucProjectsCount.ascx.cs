using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary.DLL.Projects;

public partial class projects_UserControls_wucProjectsCount : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProjectCount();
        }
    }

    public void LoadProjectCount()
    {
        try
        {
            Project obj = new Project();
            string UserID = Session["Trabau_UserId"].ToString();
            List<dynamic> lst_projects = obj.GetUserProjectsCount(Int64.Parse(UserID));

            rProjects.DataSource = lst_projects.Select(x => new { x.Status, x.Tooltip, x.Total }).ToList();
            rProjects.DataBind();

            //foreach (RepeaterItem item in rProjects.Items)
            //{
            //    Repeater rProjectDetails = item.FindControl("rProjectDetails") as Repeater;
            //    string Status = (item.FindControl("lblStatus") as Label).Text;

            //    var projectdetails = lst_projeccts.Where(x => x.Status == Status).Select(y => new
            //    {
            //        y.ProjectName,
            //        y.ProjectID,
            //        ProjectURL = "../projects/view-project.aspx?domain=MkhQU25RSSt5bldQR3JYSkRKUDRhQT09&id="+ MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(y.ProjectID.ToString(), Trabau_Keys.Misc_Key))
            //    }).ToList();

            //    rProjectDetails.DataSource = projectdetails;
            //    rProjectDetails.DataBind();
            //}
        }
        catch (Exception ex)
        {
        }
    }
}