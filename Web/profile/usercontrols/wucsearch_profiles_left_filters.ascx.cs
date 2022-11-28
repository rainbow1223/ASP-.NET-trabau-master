using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.profile;

public partial class profile_usercontrols_wucsearch_profiles_left_filters : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetFilters();
        }
    }

    public void GetFilters()
    {
        try
        {
            freelancer_search obj = new freelancer_search();
            string UserID = "0";
            try
            {
                UserID = Session["Trabau_UserId"].ToString();
            }
            catch (Exception)
            {
            }
            DataSet ds_filters = obj.GetFilters(Int64.Parse(UserID));
            DataView dv_Filters = ds_filters.Tables[0].DefaultView;
            DataTable dvFilters = dv_Filters.ToTable(true, "Type", "TypeFilter");

            rFilters.DataSource = dvFilters;
            rFilters.DataBind();


            foreach (RepeaterItem item in rFilters.Items)
            {
                try
                {
                    string Type = (item.FindControl("lblType") as Label).Text;
                    Repeater rInnerFilter = (item.FindControl("rInnerFilters") as Repeater);
                    DataView dv_inner = ds_filters.Tables[0].DefaultView;
                    dv_inner.RowFilter = "Type='" + Type + "'";

                    var lst_inner = dv_inner.ToTable().ToDynamic().Select(
                        x => new
                        {
                            x.Text,
                            Value = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.Value, Trabau_Keys.Filter_Key)),
                            x.SrNo
                        }).ToList().OrderBy(y => y.SrNo);

                    rInnerFilter.DataSource = lst_inner;
                    rInnerFilter.DataBind();
                }
                catch (Exception)
                {
                }
            }



        }
        catch (Exception)
        {
        }
    }
}