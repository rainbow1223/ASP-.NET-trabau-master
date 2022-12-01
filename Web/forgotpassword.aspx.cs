using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;

public partial class forgotpassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCaptcha();
        }
    }

    public void LoadCaptcha()
    {
        Bitmap objBitmap = new Bitmap(130, 60);
        Graphics objGraphics = Graphics.FromImage(objBitmap);
        objGraphics.Clear(Color.White);
        Random objRandom = new Random();
        objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
        objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
        objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
        Brush objBrush =
            default(Brush);
        //create background style  
        HatchStyle[] aHatchStyles = new HatchStyle[]
        {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                    HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
        };
        ////create rectangular area  
        RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
        objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
        objGraphics.FillRectangle(objBrush, oRectangleF);
        //Generate the image for captcha  
        string captchaText = string.Format("{0:X}", objRandom.Next(1000000, 9999999));
        //add the captcha value in session  
        Session["Trabau_FP_CaptchaVerify"] = captchaText;
        Font objFont = new Font("Courier New", 15, FontStyle.Bold);
        //Draw the image for captcha  
        objGraphics.DrawString(captchaText, objFont, Brushes.Black, 20, 20);
        //  objBitmap.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Gif);
        byte[] _bytes;
        using (MemoryStream ms = new MemoryStream())
        {
            objBitmap.Save(ms, ImageFormat.Bmp);
            _bytes = ms.ToArray();
        }
        imgcaptcha.ImageUrl = "data:image;base64," + Convert.ToBase64String(_bytes);
    }

    protected void btnSendResetEmail_Click(object sender, EventArgs e)
    {
        try
        {
            FailureText.Text = string.Empty;
            string _captcha = txtCaptcha.Text;
            string _actual_captcha = Session["Trabau_FP_CaptchaVerify"].ToString();
            if (_captcha == _actual_captcha)
            {
                string _UserName = UserName.Text;
                BLL_UserValidation obj = new BLL_UserValidation();
                Tuple<List<dynamic>, string> data = obj.RequestSendEmail(_UserName, Request.UserHostAddress);
                if (data != null)
                {
                    if (data.Item2 == "ok")
                    {
                        if (data.Item1 != null)
                        {
                            List<dynamic> res = data.Item1;
                            if (res[0].Message == "1")
                            {
                                string EmailId = res[0].EmailId;
                                string UserId = res[0].UserId.ToString();
                                string UserId_Name = res[0].UserId_Name;
                                string body = "";
                                try
                                {
                                    string q1 = EncyptSalt.EncryptText(UserId + "^" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString()), "Trabau_FP");
                                    q1 = HttpUtility.UrlEncode(q1);

                                    string template_url = "https://www.trabau.com/emailers/xxddcca/forgot-password.html";
                                    WebRequest req = WebRequest.Create(template_url);
                                    WebResponse w_res = req.GetResponse();
                                    StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                    string html = sr.ReadToEnd();

                                    html = html.Replace("@Name", UserId_Name);
                                    html = html.Replace("@Link", "http://www.trabau.com/resetpassword.aspx?q1=" + q1);

                                    body = html;

                                    Emailer obj_email = new Emailer();
                                    string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Your Trabau Account – Forgot your password?", body, null);


                                }
                                catch (Exception)
                                {

                                }
                                div_reset_request_message.Visible = true;
                                div_reset_request.Visible = false;
                                div_message.InnerHtml = "An email has been sent your registered email address for reset password";
                            }
                            else
                            {
                                FailureText.Text = res[0].Message;
                            }
                        }
                    }
                }
            }
            else
            {
                FailureText.Text = "Captcha not valid";
            }
        }
        catch (Exception ex)
        {

        }
    }
}