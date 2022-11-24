using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TrabauClassLibrary.BLL
{
    public class Emailer
    {
        public string SendEmail(string EmailId, string EmailCC, string EmailBCC, string SenderName, string Subject, string Body, List<dynamic> attach)
        {
            string val = "failed";
            if (HttpContext.Current.Request.UserHostAddress != "::1")
            {
                string _val = ValidateEmailer(EmailId, EmailCC, EmailBCC, "", SenderName, "", Subject, Body);
                if (_val == string.Empty)
                {
                    try
                    {
                        var fromAddress = new MailAddress("testing.email.services1@gmail.com", SenderName);
                        var toAddress = new MailAddress(EmailId, EmailId);
                        string fromPassword = "hjxkcwxcqpxtnopb";
                        string subject = Subject;
                        string body = Body;

                        var smtp = new SmtpClient
                        {
                            TargetName = "STARTTLS/smtp.gmail.com",
                            Host = "relay-hosting.secureserver.net",
                            UseDefaultCredentials = false,
                            Port = 25,
                            EnableSsl = false,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                            Timeout = 20000,


                        };


                        var message = new MailMessage(fromAddress, toAddress);

                        if (EmailCC != string.Empty)
                        {
                            message.CC.Add(EmailCC);
                        }

                        if (EmailBCC != string.Empty)
                        {
                            message.Bcc.Add(EmailBCC);
                        }

                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;
                        //for (int i = 0; i < attach.Count; i++)
                        //{
                        //    MemoryStream ms = new MemoryStream(attach[i].Attachment);
                        //    message.Attachments.Add(new Attachment(ms, attach[i].AttachmentName));
                        //}


                        smtp.Send(message);
                        val = "success";

                    }
                    catch (Exception ex)
                    {
                        val = ex.Message;
                    }
                }
                else
                {
                    val = _val;
                }
            }
            return val;
        }

        public string ValidateEmailer(string EmailTo, string EmailCC, string EmailBCC, string EmailFormat,
            string SenderName, string EmailPriority, string Subject, string Content)
        {
            string val = "";
            try
            {
                if (!IsValidEmailId(EmailTo))
                {
                    val = "Email To:" + EmailTo + " not valid";
                }

                if (val == string.Empty)
                {
                    if (!IsValidEmailId(EmailCC) && EmailCC != string.Empty)
                    {
                        val = "EmailCC:" + EmailCC + " not valid";
                    }
                }

                if (val == string.Empty)
                {
                    if (!IsValidEmailId(EmailBCC) && EmailBCC != string.Empty)
                    {
                        val = "EmailBCC:" + EmailBCC + " not valid";
                    }
                }

                if (val == string.Empty)
                {
                    if (SenderName == string.Empty)
                    {
                        val = "Sender Name cannot be empty";
                    }
                }

                if (val == string.Empty)
                {
                    if (Subject == string.Empty)
                    {
                        val = "Subject cannot be empty";
                    }
                }

                if (val == string.Empty)
                {
                    if (Content == string.Empty)
                    {
                        val = "Email Content cannot be empty";
                    }
                }
            }
            catch (Exception)
            {

            }
            return val;
        }

        public bool IsValidEmailId(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
