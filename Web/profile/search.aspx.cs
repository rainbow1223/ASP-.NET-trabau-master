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

public partial class profile_search : System.Web.UI.Page
{
    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Trabau_UserId"] != null)
                {
                    this.MasterPageFile = "~/main.master";
                }
                else
                {
                    this.MasterPageFile = "~/index.master";
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["Trabau_UserId"] != null)
                {
                    div_cont_parent.Attributes.Add("class", "category-sec");
                    div_container.Attributes.Add("class", "");
                }
                else
                {
                    div_search_header.Visible = true;
                    div_container.Attributes.Add("class", "container");
                }

                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString["query"] != null)
                    {
                        string search_query = Request.QueryString["query"];
                        txtSearch.Text = search_query;
                    }
                }
            }
            catch (Exception)
            {
            }

        }
    }

    [WebMethod(EnableSession = true)]
    public static string Search_Freelancers(string PageNumber, string SearchText, string Category, string HourlyRate, string JobSuccess, string EarnedAmount,
        string Language, string ProfileType)
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            string profiles_found = "";
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            //if (HttpContext.Current.Session["Trabau_UserId"] != null)
            //{
            var pageHolder = new Page();
            var form = new HtmlForm();
            var viewControl = (UserControl)pageHolder.LoadControl("~/profile/UserControls/wucsearch_profiles.ascx");
            try
            {
                (viewControl.FindControl("lblPageNumber") as Label).Text = PageNumber;
                (viewControl.FindControl("lblSearchText") as Label).Text = SearchText;

                (viewControl.FindControl("lblCategory") as Label).Text = Category;
                (viewControl.FindControl("lblHourlyRate") as Label).Text = HourlyRate;
                (viewControl.FindControl("lblJobSuccess") as Label).Text = JobSuccess;
                (viewControl.FindControl("lblEarnedAmount") as Label).Text = EarnedAmount;
                (viewControl.FindControl("lblLanguage") as Label).Text = Language;
                (viewControl.FindControl("lblProfileType") as Label).Text = ProfileType;
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
            detail.Add("profiles_html", output.ToString().Replace("\r\n", "").ToString());

            if (HttpContext.Current.Session["ProfilesResultFound"] != null)
            {
                profiles_found = (HttpContext.Current.Session["ProfilesResultFound"].ToString() == "1" ? "1" : "0");
            }
            HttpContext.Current.Session["ProfilesResultFound"] = null;
            //}
            //else
            //{
            //    detail.Add("response", "");
            //    detail.Add("profiles_html", "");
            //    profiles_found = "0";
            //}

            detail.Add("profiles_found", profiles_found);

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
    public static string Get_SearchFilters()
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            //if (HttpContext.Current.Session["Trabau_UserId"] != null)
            //{
            var pageHolder = new Page();
            var form = new HtmlForm();
            var viewControl = (UserControl)pageHolder.LoadControl("~/profile/UserControls/wucsearch_profiles_left_filters.ascx");
            var scriptManager = new ScriptManager();
            form.Controls.Add(scriptManager);
            form.Controls.Add(viewControl);
            pageHolder.Controls.Add(form);
            HttpContext.Current.Server.Execute(pageHolder, output, false);


            detail.Add("response", "ok");
            detail.Add("filters_html", output.ToString().Replace("\r\n", "").ToString());

            //}
            //else
            //{
            //    detail.Add("response", "");
            //    detail.Add("filters_html", "");
            //}


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
    public static string Get_RecentSearchHistory()
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
                var viewControl = (UserControl)pageHolder.LoadControl("~/profile/UserControls/wucRecentSearchHistory.ascx");
                var scriptManager = new ScriptManager();
                form.Controls.Add(scriptManager);
                form.Controls.Add(viewControl);
                pageHolder.Controls.Add(form);
                HttpContext.Current.Server.Execute(pageHolder, output, false);


                detail.Add("response", "ok");
                detail.Add("history_html", output.ToString().Replace("\r\n", "").ToString());

            }
            else
            {
                detail.Add("response", "");
                detail.Add("history_html", "");
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