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

public partial class Jobs_hires_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod(EnableSession = true)]
    public static string GetMyHires()
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                var pageHolder = new Page();
                var form = new HtmlForm();
                var viewControl = (UserControl)pageHolder.LoadControl("~/Jobs/hires/UserControls/wucHires.ascx");

                var scriptManager = new ScriptManager();
                form.Controls.Add(scriptManager);
                form.Controls.Add(viewControl);
                pageHolder.Controls.Add(form);
                HttpContext.Current.Server.Execute(pageHolder, output, false);


                detail.Add("response", "ok");
                detail.Add("myhires_html", output.ToString().Replace("\r\n", "").ToString());

            }
            else
            {
                detail.Add("response", "");
                detail.Add("myhires_html", "");
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