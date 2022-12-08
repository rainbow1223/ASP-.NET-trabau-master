using ASPSnippets.FaceBookAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.DLL.Facebook;
using TrabauClassLibrary.DLL.Google;
using TrabauClassLibrary.DLL.LinkedIn;
using TrabauClassLibrary.DLL.PropertyClass;

public partial class Login : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            div_btn_google.Visible = true;
            //if (Request.UserHostAddress == "103.223.8.80" || Request.UserHostAddress == "::1")
            //{

            div_btn_fb.Visible = true;
            //}

            //if (Request.UserHostAddress == "180.188.224.104" || Request.UserHostAddress == "::1")
            //{
            div_btn_linkedin.Visible = true;
            //}
            CheckSignInWithGoogle();

            //Response.Write(Request.UserHostAddress);
        }
    }

    private void CheckSignInWithFB()
    {
        try
        {
            //FaceBookConnect.API_Key = "1081902605599458";
            //FaceBookConnect.API_Secret = "e81763693cb912fe26a436a697146971";
            string code = Request.QueryString["code"];
            if (!string.IsNullOrEmpty(code))
            {
                FB_Authentication obj = new FB_Authentication();
                pSocial_Response res = obj.ValidateUser(Request.QueryString["code"], Request.UserHostAddress, "", "Login");
                //Response.Write(res.Item1);
                //Response.Write(res.Item2);
                // Response.Write(res.RedirectionURL.Trim());
                if (res.Trabau_Acc_Validated)
                {
                    if (res.RedirectionURL != string.Empty)
                    {
                        //Response.Redirect(res.RedirectionURL);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect_to", "setTimeout(function () {window.location.href='" + res.RedirectionURL + "';}, 100);", true);
                    }
                    else
                    {
                        Session.Clear();
                        Session.Abandon();
                        FormsAuthentication.SignOut();
                    }
                }
                else
                {
                    if (res.SocialAccount_Validated)
                    {
                        if (res.RedirectionURL != string.Empty)
                        {
                            string RedirectionURL = res.RedirectionURL;
                            //Response.Redirect(res.RedirectionURL);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect_to", "setTimeout(function () {window.location.href='" + res.RedirectionURL + "';}, 100);", true);
                        }
                        else
                        {
                            Session.Clear();
                            Session.Abandon();
                            FormsAuthentication.SignOut();
                        }
                    }
                    else
                    {
                        CheckSignInWithLinkedIn();
                    }
                }
                //string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
                //pSocial_Response faceBookUser = new JavaScriptSerializer().Deserialize<pSocial_Response>(data);
                // faceBookUser.PictureUrl = String.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);
            }
            else
            {
                CheckSignInWithLinkedIn();
            }
        }
        catch (Exception ex)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
        }
    }

    public void CheckSignInWithGoogle()
    {
        try
        {
            if (Request.QueryString["code"] != null)
            {
                if (Request.QueryString["code"] != string.Empty)
                {
                    Authentication obj = new Authentication();
                    pSocial_Response res = obj.ValidateUser(Request.QueryString["code"], Request.UserHostAddress, "https://www.trabau.com/login.aspx", "Login");
                    //Response.Write(res.Item1);
                    //Response.Write(res.Item2);
                    if (res.Trabau_Acc_Validated)
                    {
                        if (res.RedirectionURL != string.Empty)
                        {

                            //Response.Write(res.RedirectionURL);
                            //Response.Redirect(res.RedirectionURL);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect_to", "setTimeout(function () {window.location.href='" + res.RedirectionURL + "';}, 100);", true);
                        }
                        else
                        {
                            Session.Clear();
                            Session.Abandon();
                            FormsAuthentication.SignOut();
                        }
                    }
                    else
                    {
                        if (res.SocialAccount_Validated)
                        {
                            if (res.RedirectionURL != string.Empty)
                            {
                                string RedirectionURL = res.RedirectionURL;
                                //Response.Redirect(res.RedirectionURL);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect_to", "setTimeout(function () {window.location.href='" + res.RedirectionURL + "';}, 100);", true);
                            }
                            else
                            {
                                Session.Clear();
                                Session.Abandon();
                                FormsAuthentication.SignOut();
                            }
                        }
                        else
                        {
                            CheckSignInWithFB();
                        }
                    }
                }
                else
                {
                    CheckSignInWithFB();
                }
            }
            else
            {
                CheckSignInWithFB();
            }
        }
        catch (Exception)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
        }
    }

    public void CheckSignInWithLinkedIn()
    {
        try
        {
            if (Request.QueryString["code"] != null)
            {
                if (Request.QueryString["code"] != string.Empty)
                {
                    LinkedIn_Authentication obj = new LinkedIn_Authentication();
                    pSocial_Response res = obj.ValidateUser(Request.QueryString["code"], Request.UserHostAddress, "https://www.trabau.com/login.aspx", "Login");

                    //Response.Write(res.SocialAccount_Validated);
                    //Response.Write(res.Trabau_Acc_Validated);
                    //Response.Write(res.Name);
                    //Response.Write(res.Email);
                    if (res.Trabau_Acc_Validated)
                    {
                        if (res.RedirectionURL != string.Empty)
                        {
                            //Response.Write("Ok");
                            //Response.Write(res.RedirectionURL);
                            //  Response.Write(Session["Trabau_UserId"].ToString());
                            //Response.Redirect(res.RedirectionURL);

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect_to", "setTimeout(function () {window.location.href='" + res.RedirectionURL + "';}, 200);", true);
                        }
                        else
                        {
                            Session.Clear();
                            Session.Abandon();
                            FormsAuthentication.SignOut();
                        }
                    }
                    else
                    {
                        if (res.SocialAccount_Validated)
                        {
                            if (res.RedirectionURL != string.Empty)
                            {
                                string RedirectionURL = res.RedirectionURL;
                                //Response.Write(res.RedirectionURL);
                                //  Response.Redirect(res.RedirectionURL);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect_to", "setTimeout(function () {window.location.href='" + res.RedirectionURL + "';}, 100);", true);
                            }
                            else
                            {
                                Session.Clear();
                                Session.Abandon();
                                FormsAuthentication.SignOut();
                            }
                        }
                        else
                        {
                            Session.Clear();
                            Session.Abandon();
                            FormsAuthentication.SignOut();
                        }
                    }
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                }
            }
            else
            {
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
            }
        }
        catch (Exception ex)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();

            //  Response.Write(ex.Message);
        }
    }

    protected void UserAuthLogin_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            e.Authenticated = false;

            string UserName = UserAuthLogin.UserName;
            string Password = UserAuthLogin.Password;
            string IPAddress = Request.UserHostAddress;
            BLL_UserValidation obj = new BLL_UserValidation();
            Tuple<List<dynamic>, string> data = obj.ValidateUser(UserName, Password, IPAddress);
            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        if (res[0].Response == "1")
                        {
                            FormsAuthenticationTicket ticket;
                            ticket = GetAuthenticationTicket(UserAuthLogin.UserName, "C");
                            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            Response.Cookies.Add(cookie);
                            e.Authenticated = true;

                            Session["Trabau_Primary_UserId"] = res[0].UserId;
                            Session["Trabau_UserId"] = res[0].UserId;
                            Session["Trabau_UserType"] = res[0].UserType;
                            Session["Trabau_FirstName"] = res[0].Name;
                            Session["Trabau_FullName"] = res[0].FullName;
                            Session["Trabau_EmailId"] = res[0].EmailId;
                            Response.Redirect(res[0].RedirectURL);
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private FormsAuthenticationTicket GetAuthenticationTicket(string userName, string roles)
    {
        FormsAuthenticationTicket tempTicket = new
                                                FormsAuthenticationTicket
                                                (1,
                                                userName,
                                                DateTime.Now,
                                                DateTime.Now.AddMinutes(200),
                                                false,
                                                roles);

        return tempTicket;
    }

    protected void lbtnSignInWithGoogle_Click(object sender, EventArgs e)
    {
        try
        {
            string clientid = Trabau_Keys.google_client_id;
            string redirection_url = "https://www.trabau.com/login.aspx";
            string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile email&include_granted_scopes=true&redirect_uri=" + redirection_url + "&response_type=code&client_id=" + clientid + "";
            Response.Redirect(url);
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnSignInWithFacebook_Click(object sender, EventArgs e)
    {
        try
        {
            FaceBookConnect.API_Key = Trabau_Keys.facebook_api_key;
            FaceBookConnect.API_Secret = Trabau_Keys.facebook_api_secret;
            FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
        }
        catch (Exception ex)
        {
        }
    }

    protected void lbtnLinkedIn_Click(object sender, EventArgs e)
    {
        try
        {
            string client_id = Trabau_Keys.linkedin_client_id;
            string redirect_uri = "https://www.trabau.com/login.aspx";
            string API_Request = "https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id=" + client_id + "&redirect_uri=" + redirect_uri + "&state=trabau&scope=r_liteprofile r_emailaddress";
            Response.Redirect(API_Request);

        }
        catch (Exception ex)
        {
        }
    }
}

