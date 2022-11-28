using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.SignUp;

public partial class profile_usercontrols_wucProfilePicture : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void OpenProfilePicture_Popup()
    {
        try
        {
            div_profile_pic_popup.Visible = true;
            string UserID = Session["Trabau_UserId"].ToString();
            string user_pic = ImageProcessing.GetUserPicture(Int64.Parse(UserID), 700, 600);
            imgProfilePic_Upload.ImageUrl = user_pic;

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OpenProfilePic_Popup", "setTimeout(function () {HandlePopUp('1','div_profile_pic_popup');}, 500);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCloseProfilePic_popup_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseProfilePic_Popup", "setTimeout(function () {HandlePopUp('0','div_profile_pic_popup');}, 0);", true);
            div_profile_pic_popup.Visible = false;
        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveProfilePicture_Click(object sender, EventArgs e)
    {
        try
        {
            string cropped_pic = hfCropped_Picture.Value;
            imgProfilePic_Upload.ImageUrl = cropped_pic;
            byte[] photo = Convert.FromBase64String(cropped_pic.Replace("data:image/png;base64,", ""));
            photo = ImageProcessing.ScaleFreeHeight(photo, 400, 600);
            string UserID = Session["Trabau_UserId"].ToString();

            DLL_Registration obj = new DLL_Registration();
            string data = obj.UpdateUserProfilePic(Int64.Parse(UserID), photo);

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];
            string image_data = "";
            string profilepic_id = "";
            if (response == "success")
            {
                hfCropped_Picture.Value = string.Empty;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseProfilePic_Popup", "setTimeout(function () {HandlePopUp('0','div_profile_pic_popup');}, 0);", true);
                div_profile_pic_popup.Visible = false;
                image_data = "data:image;base64," + Convert.ToBase64String(photo);
                try
                {
                    (this.Parent.FindControl("img_profile_picture") as HtmlImage).Src = image_data;
                }
                catch (Exception)
                {

                }

                try
                {
                    (this.Parent.FindControl("imgProfilePic") as ImageButton).ImageUrl = image_data;
                }
                catch (Exception)
                {

                }

                try
                {
                    Repeater rAccounts = this.Page.Master.FindControl("rAccounts") as Repeater;
                    foreach (RepeaterItem item in rAccounts.Items)
                    {
                        string Acc_UserId = (item.FindControl("lblUserId") as Label).Text;
                        if (Acc_UserId == UserID)
                        {
                            HtmlImage img_profile_pic = (item.FindControl("img_profile_pic") as HtmlImage);
                            profilepic_id = img_profile_pic.ClientID;
                            img_profile_pic.Src = image_data;
                            break;
                        }
                    }

                    HtmlImage img_profile_pic_main = (this.Page.Master.FindControl("img_profile_pic_main") as HtmlImage);
                    img_profile_pic_main.Src = image_data;

                }
                catch (Exception)
                {
                }

                imgProfilePic_Upload.ImageUrl = image_data;
                try
                {
                    (this.Parent.FindControl("upParent") as UpdatePanel).Update();
                }
                catch (Exception)
                {
                }

                try
                {
                    (this.Parent.FindControl("upContactDetails") as UpdatePanel).Update();
                }
                catch (Exception)
                {
                }


                try
                {
                    string Name = Session["Trabau_FirstName"].ToString();
                    string EmailId = Session["Trabau_EmailId"].ToString();
                    string template_url = "https://www.trabau.com/emailers/xxddcca/profile-update.html";

                    try
                    {
                        WebRequest req = WebRequest.Create(template_url);
                        WebResponse w_res = req.GetResponse();
                        StreamReader sr = new StreamReader(w_res.GetResponseStream());
                        string html = sr.ReadToEnd();

                        html = html.Replace("@Name", Name);
                        html = html.Replace("@ProfileAction", "profile picture changes");

                        string body = html;

                        Emailer obj_email = new Emailer();
                        string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Profile Updated – Profile Picture", body, null);

                        try
                        {
                            utility log = new utility();
                            log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            utility log = new utility();
                            log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
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
            if (image_data != string.Empty)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Update_ProfilePic", "setTimeout(function () { $('#img_profile_pic_main').attr('src','" + image_data + "');$('#"+ profilepic_id + "').attr('src','" + image_data + "');}, 200);", true);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ProfilePic_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
        }
        catch (Exception)
        {
        }
    }

}