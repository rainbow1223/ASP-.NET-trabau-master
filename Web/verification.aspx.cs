using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.DLL;

public partial class verification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Trabau_UserId"] == null)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
        if (!IsPostBack)
        {
            try
            {
                if (!CheckEmailAddressVerificationStatus())
                {
                    // SendVerificationEmail();

                    string EmailId = Session["Trabau_EmailId"].ToString();
                    EmailId = EmailId.Substring(0, 3) + "*******@" + EmailId.Split('@')[1];
                    ltrlEmailAddress.Text = EmailId;
                }
                else
                {
                    Response.Redirect("signup/profile-updation.aspx");
                }
            }
            catch (Exception)
            {

            }
        }
    }

    public bool CheckEmailAddressVerificationStatus()
    {
        bool val = false;
        try
        {
            DLL_UserValidation obj = new DLL_UserValidation();
            string UserID = Session["Trabau_UserId"].ToString();
            string _response = obj.CheckEmailAddressVerificationStatus(Int32.Parse(UserID));
            if (_response == "1")
            {
                val = true;
            }
        }
        catch (Exception)
        {
            val = false;
        }
        return val;
    }

    public void SendVerificationEmail()
    {
        string UserID = Session["Trabau_UserId"].ToString();
        string EmailId = Session["Trabau_EmailId"].ToString();
        string Freelancer_Name = Session["Trabau_FirstName"].ToString();

        string q1 = EncyptSalt.EncryptText(UserID + "^" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString()), "Trabau_EV");
        q1 = HttpUtility.UrlEncode(q1);


        string template_url = "https://www.trabau.com/emailers/xxddcca/verification-email-resend.html";
        try
        {
            WebRequest req = WebRequest.Create(template_url);
            WebResponse w_res = req.GetResponse();
            StreamReader sr = new StreamReader(w_res.GetResponseStream());
            string html = sr.ReadToEnd();

            html = html.Replace("@Name", Freelancer_Name);
            html = html.Replace("@Link", "http://www.trabau.com/verify-email-address.aspx?q1=" + q1);

            string body = html;

            Emailer obj_email = new Emailer();
            string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Here’s your verification!", body, null);

            try
            {
                utility log = new utility();
                log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, _val, Request.UserHostAddress);
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
                log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, ex.Message, Request.UserHostAddress);
            }
            catch (Exception)
            {
            }
        }

        //Emailer obj_email = new Emailer();
        //string body = "Dear " + FirstName + ",<br/><br/>Please click <a href='http://www.trabau.com/verify-email-address.aspx?q1=" + q1 + "'>Verify Now</a> to verify your email ID.<br/><br/>";
        //body = body + "Post verification, your email ID will be updated in our records for future updates related to your Trabau account.<br/>";
        //body = body + "This link will be valid for 48 hours only";
        //obj_email.SendEmail(EmailId, "", "", "Trabau Account", "Verify your email ID for Trabau account", body, null);


    }

    protected void btnResendVerificationLink_Click(object sender, EventArgs e)
    {
        try
        {
            SendVerificationEmail();
        }
        catch (Exception)
        {

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Email_Resend_Message", "setTimeout(function () {Swal.fire({type: 'success',  title: 'Trabau account Verification email send your registered email address',  showConfirmButton: true,  timer: 1500});}, 500);", true);
    }
}