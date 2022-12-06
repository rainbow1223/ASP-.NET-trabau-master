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

public partial class search_jobs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Trabau_UserId"] != null)
            {
                if (Session["Trabau_UserType"].ToString() == "W")
                {
                    Response.Redirect("jobs/searchjobs/index.aspx");
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
                
            }
        }
    }


    [WebMethod(EnableSession = true)]
    public static string SearchJobs()
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            string jobs_found = "";
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            var pageHolder = new Page();
            var form = new HtmlForm();
            var viewControl = (UserControl)pageHolder.LoadControl("~/UserControls/wucSearchJobs_Op.ascx");
            try
            {

            }
            catch (Exception)
            {
            }
            var scriptManager = new ScriptManager();
            form.Controls.Add(scriptManager);
            form.Controls.Add(viewControl);
            pageHolder.Controls.Add(form);
            HttpContext.Current.Server.Execute(pageHolder, output, false);


            detail.Add("response", "ok");
            detail.Add("jobs_html", output.ToString().Replace("\r\n", "").ToString());
            if (HttpContext.Current.Session["JobsResultFound"] != null)
            {
                jobs_found = (HttpContext.Current.Session["JobsResultFound"].ToString() == "1" ? "1" : "0");
            }
            HttpContext.Current.Session["JobsResultFound"] = null;

            detail.Add("jobs_found", jobs_found);

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