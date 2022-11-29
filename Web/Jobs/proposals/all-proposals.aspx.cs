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

public partial class Jobs_proposals_all_proposals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod(EnableSession = true)]
    public static string GetAllProposals()
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
                if (UserType == "H")
                {
                    var pageHolder = new Page();
                    var form = new HtmlForm();
                    var viewControl = (UserControl)pageHolder.LoadControl("~/jobs/proposals/UserControls/wucProposals.ascx");
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
                    detail.Add("proposals_html", output.ToString().Replace("\r\n", "").ToString());
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("proposals_html", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("proposals_html", "");
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
    public static string OpenJobPostingLink(string id)
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
                if (UserType == "H")
                {
                    id = MiscFunctions.Base64DecodingMethod(id);
                    id = EncyptSalt.DecryptText(id, Trabau_Keys.Job_Key);
                    string JobId = id;

                    HttpContext.Current.Session["PostedJob_JobId"] = JobId;

                    detail.Add("response", "ok");
                    detail.Add("redirecturl", "../posting/jobposting.aspx");
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("redirecturl", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("redirecturl", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception)
        {
        }
        return response;
    }
}