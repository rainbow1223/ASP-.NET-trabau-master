using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using TrabauClassLibrary.DLL.PropertyClass;

namespace TrabauClassLibrary.DLL.Google
{
    public class Authentication
    {
        public pSocial_Response ValidateUser(string code, string IPAddress, string RedirectionURL, string AuthenticationType)
        {
            pSocial_Response _res = new pSocial_Response();
            try
            {
                Google_UserDetails info = GetToken(code, RedirectionURL);
                if (info != null)
                {
                    if (info.email != null)
                    {
                        if (info.email != string.Empty)
                        {
                            _res.SocialAccount_Validated = true;

                            info.name = (info.name == null ? string.Empty : info.name);
                            info.given_name = (info.given_name == null ? string.Empty : info.given_name);
                            info.family_name = (info.family_name == null ? string.Empty : info.family_name);
                            info.locale = (info.locale == null ? string.Empty : info.locale);

                            _res.Name = info.name;
                            _res.Email = info.email;
                            DLL_Social_Authentication obj = new DLL_Social_Authentication();
                            DataSet ds_response = obj.ValidateUser_Social("Google", IPAddress, info.id, info.email, info.verified_email, info.name, info.given_name, info.family_name,
                                info.picture, info.locale, AuthenticationType);
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
            }
            catch (Exception)
            {
            }

            return _res;
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
        public Google_UserDetails GetToken(string code, string RedirectionURL)
        {
            Google_UserDetails info = new Google_UserDetails();
            try
            {
                string redirection_url = RedirectionURL;
                string url = "https://accounts.google.com/o/oauth2/token";

                string poststring = "grant_type=authorization_code&code=" + code + "&client_id=" + Trabau_Keys.google_client_id + "&client_secret=" + Trabau_Keys.google_clientsecret + "&redirect_uri=" + redirection_url + "";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                UTF8Encoding utfenc = new UTF8Encoding();
                byte[] bytes = utfenc.GetBytes(poststring);
                Stream outputstream = null;
                try
                {
                    request.ContentLength = bytes.Length;
                    outputstream = request.GetRequestStream();
                    outputstream.Write(bytes, 0, bytes.Length);
                }
                catch
                { }
                var response = (HttpWebResponse)request.GetResponse();
                var streamReader = new StreamReader(response.GetResponseStream());
                string responseFromServer = streamReader.ReadToEnd();
                // Response.Write(responseFromServer);
                JavaScriptSerializer js = new JavaScriptSerializer();
                Google_Tokenclass obj = js.Deserialize<Google_Tokenclass>(responseFromServer);
                info = GetuserProfile(obj.access_token);
            }
            catch (Exception ex)
            {

            }

            return info;
        }


        public Google_UserDetails GetuserProfile(string accesstoken)
        {
            string url = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + accesstoken + "";
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            Google_UserDetails userinfo = js.Deserialize<Google_UserDetails>(responseFromServer);
            return userinfo;
        }



    }

    public class Google_Tokenclass
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    public class Google_UserDetails
    {
        public string id { get; set; }
        public string email { get; set; }
        public string verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string picture { get; set; }
        public string locale { get; set; }
    }
}
