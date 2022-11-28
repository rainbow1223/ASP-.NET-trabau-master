using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Projects;

public partial class projects_UserControls_wucProjects : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProjects();
        }
    }


    public void LoadProjects()
    {
        try
        {
            Project obj = new Project();
            string UserID = Session["Trabau_UserId"].ToString();
            string Status = lblStatus.Text;
            DataSet ds_projects = obj.GetUserProjects(Int64.Parse(UserID), Status);

            List<dynamic> lst_projects = ds_projects.Tables[0].ToDynamic();

            rProjects.DataSource = lst_projects.Select(x => new
            {
                x.ProjectID,
                x.ProjectName,
                x.ProjectFunction,
                x.StartDate,
                x.EndDate,
                x.ProjectBudget,
                x.ProjectStatus,
                x.NoOfPeople,
                ViewProject_Visibility = (Status == "Ongoing" ? false : true),
                ProjectURL = "../projects/view-project.aspx?domain=MkhQU25RSSt5bldQR3JYSkRKUDRhQT09&id=" + MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(x.ProjectID.ToString(), Trabau_Keys.Project_Key))
            }).ToList();
            rProjects.DataBind();

            div_noprojects.Visible = (rProjects.Items.Count == 0 ? true : false);

            List<dynamic> lst_project_menu = ds_projects.Tables[1].ToDynamic();

            foreach (RepeaterItem item in rProjects.Items)
            {
                Repeater rProjectMenu = (item.FindControl("rProjectMenu") as Repeater);
                string ProjectID = (item.FindControl("lblProjectID") as Label).Text;
                string ProjectURL = "../projects/view-project.aspx?domain=MkhQU25RSSt5bldQR3JYSkRKUDRhQT09&id=" + MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(ProjectID, Trabau_Keys.Project_Key));

                var lst_menu = lst_project_menu.Select(x => new
                {
                    x.MenuName,
                    ProjectURL = (x.MenuName == "Open Project" ? ProjectURL : "javascript:void(0)")
                }).ToList();

                if (lst_menu.Count() > 0)
                {
                    rProjectMenu.DataSource = lst_menu;
                    rProjectMenu.DataBind();
                }
                else
                {
                    (item.FindControl("aproject_menu") as HtmlAnchor).Visible = false;
                    (item.FindControl("ul_project_menu") as HtmlElement).Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
        }
    }
}