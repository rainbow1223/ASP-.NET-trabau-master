using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_searchjobs_search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string UserType = Session["Trabau_UserType"].ToString();
            div_saved_content.Visible = (UserType == "W" ? true : false);
            li_savedtab.Visible = (UserType == "W" ? true : false);
        }
    }



}