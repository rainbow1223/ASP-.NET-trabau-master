using ASPSnippets.FaceBookAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using TrabauClassLibrary.DLL.PropertyClass;

namespace TrabauClassLibrary.DLL.Facebook
{
    public class FB_Authentication
    {
        public pSocial_Response ValidateUser(string code, string IPAddress, string RedirectionURL, string AuthenticationType)
        {
            pSocial_Response _res = new pSocial_Response();
            try
            {
                string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
                _res = new JavaScriptSerializer().Deserialize<pSocial_Response>(data);
                _res.SocialAccount_Validated = true;

                DLL_Social_Authentication obj = new DLL_Social_Authentication();
                DataSet ds_response = obj.ValidateUser_Social("Facebook", IPAddress, _res.Id, _res.Email, "", _res.Name, "", "",
                    "", "", AuthenticationType);
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
            catch (Exception)
            {
            }

            return _res;
        }
    }
}
