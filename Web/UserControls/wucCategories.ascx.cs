using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL;

public partial class UserControls_wucCategories : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayCategories();
        }
    }

    public void DisplayCategories()
    {
        try
        {
            utility obj = new utility();
            var data = obj.GetCategories().Tables[0].ToDynamic();
            var lst_cat = data.Select(x => new
            {
                ServiceCategoryId = x.ServiceCategoryId,
                ServiceCategoryName = x.ServiceCategoryName,
                CategoryParentId = x.CategoryParentId,
                CategoryIconPath = x.CategoryIconPath
            }).ToList();

            var lst_main_cat = lst_cat.Where(a => a.CategoryParentId == 0).OrderBy(o => o.ServiceCategoryName);

            rCategories.DataSource = lst_main_cat;
            rCategories.DataBind();

            foreach (RepeaterItem item in rCategories.Items)
            {
                Repeater rSubCategories = item.FindControl("rSubCategories") as Repeater;
                string CategoryID = (item.FindControl("lblCategoryId") as Label).Text;
                var sub_cat = lst_cat.Where(x => x.CategoryParentId == Int32.Parse(CategoryID)).Select(y => new
                {
                    EncCateoryId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(y.ServiceCategoryId.ToString() + "|" + y.ServiceCategoryName, Trabau_Keys.Category_Key)),
                    ServiceCategoryName = y.ServiceCategoryName
                }).ToList();
                rSubCategories.DataSource = sub_cat;
                rSubCategories.DataBind();

                if (rSubCategories.Items.Count > 10)
                {
                    (item.FindControl("btnMore") as HtmlInputButton).Visible = true;
                    (item.FindControl("btnLess") as HtmlInputButton).Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}