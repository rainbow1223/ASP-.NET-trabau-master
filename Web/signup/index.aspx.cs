using ASPSnippets.FaceBookAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.BLL.SignUp;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.Facebook;
using TrabauClassLibrary.DLL.Google;
using TrabauClassLibrary.DLL.LinkedIn;
using TrabauClassLibrary.DLL.PropertyClass;
using TrabauClassLibrary.DLL.SignUp;

public partial class Signup_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnValidateProtection);
        if (!IsPostBack)
        {
            //if (Request.UserHostAddress == "101.0.49.111")
            //{
            //    div_btn_linkedin.Visible = true;
            //}
            //else
            //{

            if (CheckProtection())
            {
                div_btn_google.Visible = true;
                div_signup.Visible = true;
                div_btn_fb.Visible = true;
                div_btn_linkedin.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_Protection_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_Protection');}, 150);", true);
            }
            //}
            CheckSocialSignup();

            ViewState["Trabau_SignUp_EmailId_Validation"] = "1";
            div_signup_step1.Visible = true;
            div_signup_step2.Visible = false;

            CheckSignUpWithGoogle();
        }
    }

    public void CheckSocialSignup()
    {
        try
        {
            if (Request.QueryString["signup_code"] != null)
            {
                if (Request.QueryString["signup_code"] != string.Empty)
                {
                    string code = Request.QueryString["signup_code"];

                    code = MiscFunctions.Base64DecodingMethod(code);
                    code = EncyptSalt.DecryptString(code, Trabau_Keys.Misc_Key);

                    pSocial_Response _res = JsonConvert.DeserializeObject<pSocial_Response>(code);

                    String[] nameparts = _res.Name.Split(' ');

                    txtFirstName.Text = nameparts[0];
                    try
                    {
                        txtLastName.Text = nameparts[1];
                    }
                    catch (Exception)
                    {
                    }

                    txtEmailId.Text = _res.Email;

                    div_btn_google.Visible = false;
                    div_btn_fb.Visible = false;
                    div_btn_linkedin.Visible = false;
                }
            }
        }
        catch (Exception)
        {
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
                pSocial_Response res = obj.ValidateUser(Request.QueryString["code"], Request.UserHostAddress, "", "SignUp");
                //Response.Write(res.Item1);
                //Response.Write(res.Item2);
                // Response.Write(res.RedirectionURL.Trim());
                if (res.Trabau_Acc_Validated)
                {
                    if (res.RedirectionURL != string.Empty)
                    {
                        res.RedirectionURL = res.RedirectionURL.Replace("signup/", "");
                        //Response.Write("Ok");
                        //Response.Write(res.RedirectionURL);
                        //  Response.Write(Session["Trabau_UserId"].ToString());
                        //Response.Redirect(res.RedirectionURL);
                        //Response.Redirect("../" + res.RedirectionURL);
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
                            //res.RedirectionURL = res.RedirectionURL.Replace("/../", "/");
                            //res.RedirectionURL = res.RedirectionURL.Replace("signup/", "");

                            //string RedirectionURL = res.RedirectionURL;
                            Response.Redirect("../" + res.RedirectionURL);
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
        }
    }

    public void CheckSignUpWithGoogle()
    {
        try
        {
            if (Request.QueryString["code"] != null)
            {
                if (Request.QueryString["code"] != string.Empty)
                {
                    Authentication obj = new Authentication();
                    pSocial_Response res = obj.ValidateUser(Request.QueryString["code"], Request.UserHostAddress, "https://www.trabau.com/signup/index.aspx", "SignUp");
                    //    Response.Write(res.SocialAccount_Validated);
                    //    Response.Write(res.Trabau_Acc_Validated);
                    if (res.Trabau_Acc_Validated)
                    {
                        if (res.RedirectionURL != string.Empty)
                        {
                            res.RedirectionURL = res.RedirectionURL.Replace("/../", "/");
                            res.RedirectionURL = res.RedirectionURL.Replace("signup/", "");
                            Response.Redirect("../" + res.RedirectionURL);
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
                                RedirectionURL = RedirectionURL.Replace("signup/", "");

                                Response.Redirect(RedirectionURL);
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
        catch (Exception ex)
        {
            // Response.Write(ex.Message);
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
                    pSocial_Response res = obj.ValidateUser(Request.QueryString["code"], Request.UserHostAddress, "https://www.trabau.com/signup/index.aspx", "SignUp");

                    //Response.Write(res.SocialAccount_Validated);
                    //Response.Write(res.Trabau_Acc_Validated);
                    //Response.Write(res.Name);
                    //Response.Write(res.Email);
                    if (res.Trabau_Acc_Validated)
                    {
                        if (res.RedirectionURL != string.Empty)
                        {
                            res.RedirectionURL = res.RedirectionURL.Replace("signup/", "");
                            //Response.Write("Ok");
                            //Response.Write(res.RedirectionURL);
                            //  Response.Write(Session["Trabau_UserId"].ToString());
                            //Response.Redirect(res.RedirectionURL);
                            //Response.Redirect("../" + res.RedirectionURL);
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
                                RedirectionURL = RedirectionURL.Replace("signup/", "");
                                //Response.Redirect(res.RedirectionURL);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect_to", "setTimeout(function () {window.location.href='" + RedirectionURL + "';}, 100);", true);
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

    public bool CheckProtection()
    {
        bool val = false;
        try
        {
            if (Session["Trabau_Protection"] != null)
            {
                if (Session["Trabau_Protection"].ToString() == "ok")
                {
                    val = true;
                }
            }

            if (DateTime.Now.Year == 2021 && DateTime.Now.Month == 2 && (DateTime.Now.Day == 25 || DateTime.Now.Day == 26 || DateTime.Now.Day == 27 || DateTime.Now.Day == 28))
            {
                val = true;
            }
        }
        catch (Exception)
        {

        }
        return val;
    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Trabau_SignUp_EmailId_Validation"] == null)
            {
                ViewState["Trabau_SignUp_EmailId_Validation"] = "1";
            }
            string Step_No = ViewState["Trabau_SignUp_EmailId_Validation"].ToString();

            if (Step_No == "1")
            {
                Process_Step1();
            }
            else if (Step_No == "2")
            {
                hfRemarks.Value = "Step 0";
                Process_Step2();
            }


        }
        catch (Exception)
        {

        }
    }

    public void Process_Step1()
    {
        try
        {
            string FirstName = txtFirstName.Text;
            string LastName = txtLastName.Text;
            string EmailId = txtEmailId.Text;
            BLL_Registration obj = new BLL_Registration();
            Tuple<List<dynamic>, string> data = obj.CheckEmailId(EmailId, 120);
            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        if (res[0].Status == "success")
                        {
                            ViewState["Trabau_SignUp_EmailId_Validation"] = "2";
                            ViewState["Trabau_SignUp_FirstName"] = txtFirstName.Text;
                            ViewState["Trabau_SignUp_LastName"] = txtLastName.Text;
                            ViewState["Trabau_SignUp_EmailId"] = EmailId;

                            div_signup_step1.Visible = false;
                            div_signup_step2.Visible = true;
                            div_step2_header.InnerHtml = res[0].Message;

                            BindCountries();
                            div_response.InnerHtml = string.Empty;
                        }
                        else
                        {
                            div_response.InnerHtml = res[0].Message;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    public void Process_Step2()
    {
        try
        {
            //   hfRemarks.Value = "Step 1";
            string Password = txtPassword.Text;
            string Country = ddlCountry.SelectedValue;
            string RegType = ddlRegistrationType.SelectedValue;
            string FirstName = ViewState["Trabau_SignUp_FirstName"].ToString();
            string LastName = ViewState["Trabau_SignUp_LastName"].ToString();
            string EmailId = ViewState["Trabau_SignUp_EmailId"].ToString();
            string IPAddress = Request.UserHostAddress;
            string UserId = txtUserId.Text;

            BLL_Registration obj = new BLL_Registration();
            Tuple<List<dynamic>, string> data = obj.User_Pre_SignUp(FirstName, LastName, EmailId, UserId, Password, Int32.Parse(Country), RegType, IPAddress);

            if (data != null)
            {
                if (data.Item2 == "ok")
                {
                    if (data.Item1 != null)
                    {
                        List<dynamic> res = data.Item1;
                        if (res[0].Status == "success")
                        {
                            Session["Trabau_UserId"] = res[0].UserId;
                            Session["Trabau_UserName"] = UserId;
                            Session["Trabau_FirstName"] = FirstName;
                            Session["Trabau_LastName"] = LastName;
                            Session["Trabau_FullName"] = FirstName + " " + LastName;
                            Session["Trabau_EmailId"] = res[0].EmailId;
                            Session["Trabau_UserType"] = RegType;



                            string template_url = "https://www.trabau.com/emailers/xxddcca/registration.html";
                            string Freelancer_Name = FirstName;
                            string Freelancer_EmailId = res[0].EmailId;
                            string Freelancer_UserId = res[0].UserId.ToString();
                            try
                            {
                                string q1 = EncyptSalt.EncryptText(Freelancer_UserId + "^" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString()), "Trabau_EV");
                                q1 = HttpUtility.UrlEncode(q1);

                                string verificationlink = "https://www.trabau.com/verify-email-address.aspx?q1=" + q1;
                                WebRequest req = WebRequest.Create(template_url);
                                WebResponse w_res = req.GetResponse();
                                StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                string html = sr.ReadToEnd();

                                html = html.Replace("@Name", Freelancer_Name);
                                html = html.Replace("@Link", verificationlink);

                                string body = html;

                                Emailer obj_email = new Emailer();
                                string _val = obj_email.SendEmail(Freelancer_EmailId, "", "", "Trabau Notification", "Hello! Confirm your Trabau account?", body, null);

                                try
                                {
                                    utility log = new utility();
                                    log.CreateEmailerLog(Convert.ToInt64(Freelancer_UserId), Freelancer_EmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
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
                                    log.CreateEmailerLog(Convert.ToInt64(Freelancer_UserId), Freelancer_EmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                }
                                catch (Exception)
                                {
                                }
                            }


                            string redirect_url = "";
                            if (RegType == "W")
                            {
                                redirect_url = "profile-updation.aspx";
                            }
                            else
                            {
                                redirect_url = "company-details.aspx";
                            }

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "SignUp__Redirect", "setTimeout(function () {window.location.href='" + redirect_url + "';}, 1500); ", true);
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SignUp_Message", "Swal.fire({type: '" + res[0].Status + "',  title: '" + res[0].Message + "',  showConfirmButton: true,  timer: 500});", true);
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    public void BindCountries()
    {
        try
        {
            DLL_Registration obj = new DLL_Registration();
            DataSet ds_country = obj.GetCountryList();

            ddlCountry.DataSource = ds_country;
            ddlCountry.DataTextField = "Text";
            ddlCountry.DataValueField = "Value";
            ddlCountry.DataBind();
        }
        catch (Exception ex)
        {
        }
    }


    protected void lbtnContinueWithGoogle_Click(object sender, EventArgs e)
    {
        try
        {
            string clientid = Trabau_Keys.google_client_id;
            string redirection_url = "https://www.trabau.com/signup/index.aspx";
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
            string redirect_uri = "https://www.trabau.com/signup/index.aspx";
            string API_Request = "https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id=" + client_id + "&redirect_uri=" + redirect_uri + "&state=trabau&scope=r_liteprofile r_emailaddress";
            Response.Redirect(API_Request);

        }
        catch (Exception ex)
        {
        }
    }

    protected void btnValidateProtection_Click(object sender, EventArgs e)
    {
        DateTime startDate = Convert.ToDateTime("09/07/2022 12:00");
        DateTime todayDate = DateTime.Now;
        if (txtProtection_Password.Text == "admin_123@" || (txtProtection_Password.Text == "trabau@123!!.." && (todayDate - startDate).Days <= 180))
        {
            Session["Trabau_Protection"] = "ok";
            Response.Redirect(Request.Url.AbsoluteUri);
            //if (txtFirstName.Text == string.Empty)
            //{
            //    div_btn_google.Visible = true;
            //    div_signup.Visible = true;
            //    div_btn_fb.Visible = true;
            //    div_btn_linkedin.Visible = true;
            //}

            //div_signup_step1.Visible = true;
            //div_signup_step2.Visible = false;
        }
    }
}