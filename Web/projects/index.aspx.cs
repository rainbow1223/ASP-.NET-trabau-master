using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Projects;

public partial class projects_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Project_ReturnURL"] = null;
            try
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                btn_newproject.Visible = UserType == "H" ? true : false;
            }
            catch (Exception)
            {
            }
            
        }
    }



    [WebMethod(EnableSession = true)]
    public static string GetProjectCount()
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                if (UserType == "H" || UserType == "W")
                {
                    var pageHolder = new Page();
                    var form = new HtmlForm();
                    var viewControl = (UserControl)pageHolder.LoadControl("~/projects/UserControls/wucProjectsCount.ascx");
                    //try
                    //{
                    //    (viewControl.FindControl("hfInterviewFilter") as HiddenField).Value = InterviewFilter;
                    //}
                    //catch (Exception)
                    //{
                    //}
                    var scriptManager = new ScriptManager();
                    form.Controls.Add(scriptManager);
                    form.Controls.Add(viewControl);
                    pageHolder.Controls.Add(form);
                    HttpContext.Current.Server.Execute(pageHolder, output, false);


                    detail.Add("response", "ok");
                    detail.Add("project_html", output.ToString().Replace("\r\n", "").ToString());
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("project_html", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("project_html", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }


    [WebMethod(EnableSession = true)]
    public static string GetProjects(string Status)
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                if (UserType == "H" || UserType == "W")
                {
                    var pageHolder = new Page();
                    var form = new HtmlForm();
                    var viewControl = (UserControl)pageHolder.LoadControl("~/projects/UserControls/wucProjects.ascx");
                    try
                    {
                        (viewControl.FindControl("lblStatus") as Label).Text = Status;
                    }
                    catch (Exception)
                    {
                    }
                    var scriptManager = new ScriptManager();
                    form.Controls.Add(scriptManager);
                    form.Controls.Add(viewControl);
                    pageHolder.Controls.Add(form);
                    HttpContext.Current.Server.Execute(pageHolder, output, false);

                    ;

                    detail.Add("response", "ok");
                    detail.Add("project_html", output.ToString().Replace("\r\n", "").ToString());
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("project_html", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("project_html", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }
}