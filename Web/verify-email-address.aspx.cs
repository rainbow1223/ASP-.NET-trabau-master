using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.DLL;

public partial class verify_email_address : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Count == 1)
            {
                if (Request.QueryString["q1"] != null)
                {
                    string q1 = Request.QueryString["q1"];
                    q1 = EncyptSalt.DecryptText(q1, "Trabau_EV");

                    string UserId = q1.Split('^')[0];
                    string timestamp = q1.Split('^')[1];

                    TimeSpan span = DateTime.Now - Convert.ToDateTime(timestamp);
                    if (span.TotalHours <= 48)
                    {
                        DLL_UserValidation obj = new DLL_UserValidation();
                        DataSet ds_response = obj.VerifyEmailAddress(Int32.Parse(UserId), Request.UserHostAddress);
                        string response = "Error while verifying your email address, please refresh and try again";
                        if (ds_response != null)
                        {
                            if (ds_response.Tables.Count > 0)
                            {
                                if (ds_response.Tables[0].Rows.Count > 0)
                                {
                                    response = ds_response.Tables[0].Rows[0]["Response"].ToString();
                                    string Name = ds_response.Tables[0].Rows[0]["Name"].ToString();
                                    string EmailId = ds_response.Tables[0].Rows[0]["EmailId"].ToString();
                                    string UserType = ds_response.Tables[0].Rows[0]["UserType"].ToString();
                                    string UserAction = ds_response.Tables[0].Rows[0]["UserAction"].ToString();
                                    string _UserId = ds_response.Tables[0].Rows[0]["UserId"].ToString();
                                    string _Password = ds_response.Tables[0].Rows[0]["Password"].ToString();
                                    if (response == "success")
                                    {
                                        string template_url = "https://www.trabau.com/emailers/xxddcca/verification-success.html";
                                        try
                                        {
                                            WebRequest req = WebRequest.Create(template_url);
                                            WebResponse w_res = req.GetResponse();
                                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                            string html = sr.ReadToEnd();

                                            html = html.Replace("@Name", Name);
                                            html = html.Replace("@UserType", UserType);
                                            html = html.Replace("@UserAction", UserAction);
                                            html = html.Replace("@UserId", _UserId);
                                            html = html.Replace("@Password", _Password);

                                            string body = html;

                                            Emailer obj_email = new Emailer();
                                            string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Hello and Welcome to Trabau!", body, null);

                                            try
                                            {
                                                utility log = new utility();
                                                log.CreateEmailerLog(Convert.ToInt64(UserId), EmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
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
                                                log.CreateEmailerLog(Convert.ToInt64(UserId), EmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }


                                        div_thanks.Visible = true;
                                        div_message.Visible = false;
                                    }
                                    else
                                    {
                                        div_thanks.Visible = false;
                                        div_message.Visible = true;

                                        ltrlMessage.Text = response;
                                    }
                                }
                                else
                                {
                                    div_thanks.Visible = false;
                                    div_message.Visible = true;

                                    ltrlMessage.Text = response;
                                }
                            }
                            else
                            {
                                div_thanks.Visible = false;
                                div_message.Visible = true;

                                ltrlMessage.Text = response;
                            }
                        }
                        else
                        {
                            div_thanks.Visible = false;
                            div_message.Visible = true;

                            ltrlMessage.Text = response;
                        }
                    }
                    else
                    {
                        div_thanks.Visible = false;
                        div_message.Visible = true;

                        ltrlMessage.Text = "Email address verification link expired";
                    }
                }
                else
                {
                    div_thanks.Visible = false;
                    div_message.Visible = true;

                    ltrlMessage.Text = "Email address verification link expired or not valid";
                }
            }
            else
            {
                div_thanks.Visible = false;
                div_message.Visible = true;

                ltrlMessage.Text = "Email address verification link expired or not valid";
            }
        }
        catch (Exception)
        {
            div_thanks.Visible = false;
            div_message.Visible = true;

            ltrlMessage.Text = "Email address verification link expired or not valid";
        }
    }
}