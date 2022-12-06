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

public partial class resetpassword : System.Web.UI.Page
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
                    q1 = EncyptSalt.DecryptText(q1, "Trabau_FP");

                    string UserId = q1.Split('^')[0];
                    string timestamp = q1.Split('^')[1];

                    TimeSpan span = DateTime.Now - Convert.ToDateTime(timestamp);
                    if (span.Minutes <= 60)
                    {

                    }
                    else
                    {
                        div_reset_request.Visible = false;
                        div_reset_password_message.Visible = true;

                        lblMessage.Text = "Reset password link expired";
                    }
                }
                else
                {
                    div_reset_request.Visible = false;
                    div_reset_password_message.Visible = true;

                    lblMessage.Text = "Reset password link expired or not valid";
                }
            }
            else
            {
                div_reset_request.Visible = false;
                div_reset_password_message.Visible = true;

                lblMessage.Text = "Reset password link expired or not valid";
            }
        }
        catch (Exception)
        {
            div_reset_request.Visible = false;
            div_reset_password_message.Visible = true;

            lblMessage.Text = "Reset password link expired or not valid";
        }
    }

    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Count == 1)
            {
                if (Request.QueryString["q1"] != null)
                {
                    string q1 = Request.QueryString["q1"];
                    q1 = EncyptSalt.DecryptText(q1, "Trabau_FP");

                    string UserId = q1.Split('^')[0];
                    string timestamp = q1.Split('^')[1];

                    TimeSpan span = DateTime.Now - Convert.ToDateTime(timestamp);
                    if (span.TotalHours <= 24)
                    {
                        DLL_UserValidation obj = new DLL_UserValidation();
                        string Password = txtNewPassword.Text;
                        DataSet ds = obj.ResetPassword(Convert.ToInt64(UserId), Password, Request.UserHostAddress);
                        string response = "error";
                        string message = "Error while reseting your password, please refresh and try again";
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    response = ds.Tables[0].Rows[0]["Response"].ToString().ToLower();
                                    message = ds.Tables[0].Rows[0]["Message"].ToString();
                                    if (response == "success")
                                    {
                                        string UserId_Name = ds.Tables[0].Rows[0]["Name"].ToString();
                                        string EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();

                                        string template_url = "https://www.trabau.com/emailers/xxddcca/after-password-reset.html";

                                        try
                                        {
                                            WebRequest req = WebRequest.Create(template_url);
                                            WebResponse w_res = req.GetResponse();
                                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                            string html = sr.ReadToEnd();

                                            html = html.Replace("@Name", UserId_Name);

                                            string body = html;

                                            Emailer obj_email = new Emailer();
                                            string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "We did it –Trabau Password Reset Complete!", body, null);

                                            try
                                            {
                                                utility log = new utility();
                                                log.CreateEmailerLog(Convert.ToInt64(UserId), EmailId, template_url, _val, Request.UserHostAddress);
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
                                                log.CreateEmailerLog(Convert.ToInt64(UserId), EmailId, template_url, ex.Message, Request.UserHostAddress);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ResetPassword_Redirect", "setTimeout(function () {window.location.href='login.aspx';}, 2000);", true);
                                    }
                                }
                            }
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ResetPassword_Message", "Swal.fire({type: '" + response + "',  title: '" + message + "',  showConfirmButton: true,  timer: 1500});", true);
                    }
                    else
                    {
                        div_reset_request.Visible = false;
                        div_reset_password_message.Visible = true;

                        lblMessage.Text = "Reset password link expired";
                    }
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception)
        {
        }
    }
}