using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using TrabauClassLibrary.DLL.PropertyClass;

namespace TrabauClassLibrary.DLL.LinkedIn
{
    public class LinkedIn_Authentication
    {
        public pSocial_Response ValidateUser(string code, string IPAddress, string RedirectionURL, string AuthenticationType)
        {
            pSocial_Response _res = new pSocial_Response();
            try
            {
                LinkedIn_UserInfo info = GetToken(code, RedirectionURL);

                if (info != null)
                {
                    if (info.emailaddress != string.Empty && info.emailaddress != null)
                    {
                        _res.SocialAccount_Validated = true;

                        info.localizedFirstName = (info.localizedFirstName == null ? string.Empty : info.localizedFirstName);
                        info.localizedLastName = (info.localizedLastName == null ? string.Empty : info.localizedLastName);
                        info.profilePicture.displayImage = (info.profilePicture.displayImage == null ? string.Empty : info.profilePicture.displayImage);
                        info.id = (info.id == null ? string.Empty : info.id);

                        _res.Name = info.localizedFirstName;
                        _res.Email = info.emailaddress;
                        _res.Id = info.id;

                        DLL_Social_Authentication obj = new DLL_Social_Authentication();
                        DataSet ds_response = obj.ValidateUser_Social("LinkedIn", IPAddress, info.id, info.emailaddress, "",
                            info.localizedFirstName + " " + info.localizedLastName, "", "", info.profilePicture.displayImage, "", AuthenticationType);
                        if (ds_response != null)
                        {
                            if (ds_response.Tables.Count > 0)
                            {
                                if (ds_response.Tables[0].Rows.Count > 0)
                                {
                                    List<dynamic> res = ds_response.Tables[0].ToDynamic();

                                    if (res[0].Response == "1")
                                    {
                                        _res.Trabau_Acc_Validated = true;
                                        //FormsAuthenticationTicket ticket;
                                        //ticket = GetAuthenticationTicket(res[0].UserId, "C");
                                        //string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                                        //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                                        //HttpContext.Current.Response.Cookies.Add(cookie);

                                        HttpContext.Current.Session["Trabau_Primary_UserId"] = res[0].UserId;
                                        HttpContext.Current.Session["Trabau_UserId"] = res[0].UserId;
                                        HttpContext.Current.Session["Trabau_UserType"] = res[0].UserType;
                                        HttpContext.Current.Session["Trabau_FirstName"] = res[0].Name;
                                        HttpContext.Current.Session["Trabau_FullName"] = res[0].FullName;
                                        HttpContext.Current.Session["Trabau_EmailId"] = res[0].EmailId;

                                        _res.RedirectionURL = res[0].RedirectURL;
                                    }
                                    else
                                    {
                                        _res.Trabau_Acc_Validated = false;
                                        string enc_code = JsonConvert.SerializeObject(_res);
                                        enc_code = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(enc_code, Trabau_Keys.Misc_Key));
                                        _res.RedirectionURL = "signup/index.aspx?signup_code=" + enc_code;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _res.Email = ex.Message;
            }

            return _res;
        }
        public LinkedIn_UserInfo GetToken(string code, string RedirectionURL)
        {
            LinkedIn_UserInfo info = new LinkedIn_UserInfo();
            try
            {
                string redirection_url = RedirectionURL;
                string url = "https://api.linkedin.com/oauth/v2/accessToken?";

                string getstring = "grant_type=authorization_code&code=" + code + "&client_id=" + Trabau_Keys.linkedin_client_id + "&client_secret=" + Trabau_Keys.linkedin_clientsecret + "&redirect_uri=" + redirection_url + "";
                url = url + getstring;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(url);
                // request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";
                //UTF8Encoding utfenc = new UTF8Encoding();
                //byte[] bytes = utfenc.GetBytes(poststring);
                //Stream outputstream = null;
                //try
                //{
                //    request.ContentLength = bytes.Length;
                //    outputstream = request.GetRequestStream();
                //    outputstream.Write(bytes, 0, bytes.Length);
                //}
                //catch
                //{ }
                var response = (HttpWebResponse)request.GetResponse();
                var streamReader = new StreamReader(response.GetResponseStream());
                string responseFromServer = streamReader.ReadToEnd();


                JavaScriptSerializer js = new JavaScriptSerializer();
                LinkedIn_Token obj = js.Deserialize<LinkedIn_Token>(responseFromServer);
                LinkedIn_Response_Email email = GetUserEmail(obj.access_token);


                string EmailAddress = string.Empty;
                try
                {
                    if (email.elements[0].handle1.emailAddress != null)
                    {
                        EmailAddress = email.elements[0].handle1.emailAddress;
                    }
                }
                catch (Exception)
                {
                }

                if (EmailAddress != string.Empty)
                {
                    info = GetuserProfile(obj.access_token);
                    info.emailaddress = EmailAddress;
                }

                //info = GetuserProfile(obj.access_token);
            }
            catch (Exception ex)
            {
                info.emailaddress = ex.Message;
            }

            return info;
        }


        public LinkedIn_UserInfo GetuserProfile(string accesstoken)
        {
            LinkedIn_UserInfo info = new LinkedIn_UserInfo();
            try
            {
                string url = "https://api.linkedin.com/v2/me";
                WebRequest request = WebRequest.Create(url);
                request.Headers.Add("Authorization", "Bearer " + accesstoken);
                request.Credentials = CredentialCache.DefaultCredentials;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                response.Close();

                info = JsonConvert.DeserializeObject<LinkedIn_UserInfo>(responseFromServer);
                // info.emailaddress = responseFromServer;
            }
            catch (Exception ex)
            {
                info = new LinkedIn_UserInfo();
                info.emailaddress = ex.Message;
            }
            return info;
        }


        public LinkedIn_Response_Email GetUserEmail(string accesstoken)
        {
            LinkedIn_Response_Email email_response = new LinkedIn_Response_Email();
            try
            {
                string url = "https://api.linkedin.com/v2/emailAddress?q=members&projection=(elements*(handle~))";
                WebRequest request = WebRequest.Create(url);
                request.Headers.Add("Authorization", "Bearer " + accesstoken);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //JavaScriptSerializer js = new JavaScriptSerializer();
                //  email_response = js.Deserialize<LinkedIn_Response_Email>(responseFromServer);
                email_response = JsonConvert.DeserializeObject<LinkedIn_Response_Email>(responseFromServer);
            }
            catch (Exception ex)
            {
                //email_response = new LinkedIn_Response_Email();
                //email_response.elements.Add(new LinkedIn_Response_Email_handle { handle1 = new LinkedIn_Response_Email_handle_1 { emailAddress = ex.Message } });
            }
            return email_response;
        }


        //public pSocial_Response ValidateUser(string code, string IPAddress, string RedirectionURL, string AuthenticationType)
        //{
        //    pSocial_Response _res = new pSocial_Response();
        //    try
        //    {
        //        string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
        //        _res = new JavaScriptSerializer().Deserialize<pSocial_Response>(data);
        //        _res.SocialAccount_Validated = true;

        //        DLL_Social_Authentication obj = new DLL_Social_Authentication();
        //        DataSet ds_response = obj.ValidateUser_Social("Facebook", IPAddress, _res.Id, _res.Email, "", _res.Name, "", "",
        //            "", "", AuthenticationType);
        //        if (ds_response != null)
        //        {
        //            if (ds_response.Tables.Count > 0)
        //            {
        //                if (ds_response.Tables[0].Rows.Count > 0)
        //                {
        //                    List<dynamic> res = ds_response.Tables[0].ToDynamic();

        //                    if (res[0].Response == "1")
        //                    {
        //                        _res.Trabau_Acc_Validated = true;

        //                        HttpContext.Current.Session["Trabau_Primary_UserId"] = res[0].UserId;
        //                        HttpContext.Current.Session["Trabau_UserId"] = res[0].UserId;
        //                        HttpContext.Current.Session["Trabau_UserType"] = res[0].UserType;
        //                        HttpContext.Current.Session["Trabau_FirstName"] = res[0].Name;
        //                        HttpContext.Current.Session["Trabau_EmailId"] = res[0].EmailId;

        //                        _res.RedirectionURL = res[0].RedirectURL;
        //                    }
        //                    else
        //                    {
        //                        _res.Trabau_Acc_Validated = false;
        //                        string enc_code = JsonConvert.SerializeObject(_res);
        //                        enc_code = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(enc_code, Trabau_Keys.Misc_Key));
        //                        _res.RedirectionURL = "signup/index.aspx?signup_code=" + enc_code;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    return _res;
        //}
        //}
    }

    public class LinkedIn_Token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

    public class LinkedIn_Response_Email
    {
        public List<LinkedIn_Response_Email_handle> elements { get; set; }
    }

    public class LinkedIn_Response_Email_handle
    {
        [JsonProperty(PropertyName = "handle~")]
        public LinkedIn_Response_Email_handle_1 handle1 { get; set; }
        public string handle { get; set; }
    }

    public class LinkedIn_Response_Email_handle_1
    {
        public string emailAddress { get; set; }
    }

    public class LinkedIn_UserInfo
    {
        public string localizedLastName { get; set; }
        public string localizedFirstName { get; set; }
        public string id { get; set; }
        public LinkedIn_UserInfo_ProfilePicture profilePicture { get; set; }
        public string emailaddress { get; set; }
        public string Response { get; set; }
    }
    public class LinkedIn_UserInfo_ProfilePicture
    {
        public string displayImage { get; set; }
    }

}
