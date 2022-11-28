using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.profile.settings;

public partial class profile_usercontrols_wucCompanyLogo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void OpenCompanyLogo_Popup()
    {
        try
        {
            div_company_logo_popup.Visible = true;
            string UserID = Session["Trabau_UserId"].ToString();
            string company_logo = ImageProcessing.GetCompanyLogo(Int64.Parse(UserID), 700, 600);
            imgCompanyLogo_Upload.ImageUrl = company_logo;

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OpenCompanyLogo_Popup", "setTimeout(function () {HandlePopUp('1','div_company_logo_popup');}, 500);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCloseCompanyLogo_popup_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseCompanyLogo_Popup", "setTimeout(function () {HandlePopUp('0','div_company_logo_popup');}, 0);", true);
            div_company_logo_popup.Visible = false;
        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveCompanyLogo_Click(object sender, EventArgs e)
    {
        try
        {
            string cropped_pic = hf_CompanyLogo_Cropped.Value;
            imgCompanyLogo_Upload.ImageUrl = cropped_pic;
            byte[] photo = Convert.FromBase64String(cropped_pic.Replace("data:image/png;base64,", ""));
            photo = ImageProcessing.ScaleFreeHeight(photo, 400, 600);
            string UserID = Session["Trabau_UserId"].ToString();

            settings_changes obj = new settings_changes();
            string data = obj.UpdateCompanyLogo(Int64.Parse(UserID), photo);

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];
            if (response == "success")
            {
                hf_CompanyLogo_Cropped.Value = string.Empty;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseCompanyLogo_Popup", "setTimeout(function () {HandlePopUp('0','div_company_logo_popup');}, 0);", true);
                div_company_logo_popup.Visible = false;

                try
                {
                    (this.Parent.FindControl("imgCompanyLogo") as ImageButton).ImageUrl = "data:image;base64," + Convert.ToBase64String(photo);
                }
                catch (Exception)
                {

                }

                imgCompanyLogo_Upload.ImageUrl = "data:image;base64," + Convert.ToBase64String(photo);

                try
                {
                    (this.Parent.FindControl("upCompanyDetails") as UpdatePanel).Update();
                }
                catch (Exception)
                {
                }

            }

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CompanyLogo_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
        }
        catch (Exception)
        {
        }
    }
}