using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary.DLL.profile;

public partial class profile_usercontrols_wucRecentSearchHistory : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRecentSearchHistory();
        }
    }

    public void GetRecentSearchHistory()
    {
        try
        {
            string UserId = Session["Trabau_UserId"].ToString();
            freelancer_search obj = new freelancer_search();
            DataSet ds_history = obj.Get_RecentSearchHistory(Int64.Parse(UserId));
            rSearchHistory.DataSource = ds_history;
            rSearchHistory.DataBind();
        }
        catch (Exception)
        {
        }
    }
}