using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_hires_UserControls_wucHires : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayMyHires();
        }
    }

    public void DisplayMyHires()
    {
        try
        {
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();

            DataSet ds_hires = obj.GetMyHires(Int64.Parse(UserID));
            var hires = ds_hires.Tables[0].ToDynamic().Select(x => new
            {
                x.JobTitle,
                x.UserId,
                x.Title,
                x.Name,
                x.LocalTime,
                x.TotalEarning,
                x.HourlyRate,
                x.Overview,
                profile_id = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.UserId.ToString(), Trabau_Keys.Profile_Key)),
                x.Preferred_List,
                x.Preferred_ListClass
            }).ToList();

            rHires.DataSource = hires;
            rHires.DataBind();

            foreach (RepeaterItem item in rHires.Items)
            {
                string _UserId = (item.FindControl("lblUserId") as Label).Text;

                _UserId = _UserId + "~" + DateTime.Now.ToString();
                _UserId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(_UserId, Trabau_Keys.Profile_Key));

                string ran = Guid.NewGuid().ToString();
                (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data-key", ran + "_userpic_" + (item.ItemIndex + 1000).ToString());
                (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data", _UserId);
                (item.FindControl("imgFL_ProfilePic") as HtmlImage).Src = "../../assets/uploads/loading_pic.gif";
            }
        }
        catch (Exception)
        {
        }
    }
}