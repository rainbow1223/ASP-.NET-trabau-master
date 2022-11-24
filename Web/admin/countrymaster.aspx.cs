using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Admin;

public partial class admin_countrymaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetCountryDetails(string data)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CountryMaster obj = new CountryMaster();
            string CountryId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(data), Trabau_Keys.Admin_Key);
            DataSet dscountry = obj.GetCountryDetails(Int64.Parse(UserId), Int32.Parse(CountryId));
            var lstcountry = dscountry.Tables[0].ToDynamic()
                .Select(x => new
                {
                    CountryName = x.CountryName,
                    CountryCode = x.CountryCode,
                    CountryPrefix = x.CountryPrefix,
                    TimeZone = x.TimeZone,
                    TimeZoneDetails = x.TimeZoneDetails,
                    CountryId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CountryId.ToString(), Trabau_Keys.Admin_Key)),
                    IsActive = x.IsActive
                }).ToList();

            var _result = new { response = "ok", data = lstcountry };

            JavaScriptSerializer js = new JavaScriptSerializer();
            strJSON = js.Serialize(_result);

        }
        catch (Exception)
        {
        }

        return strJSON;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveCountryDetails(string CountryID, string CountryName, string CountryCode, string CountryPrefix, string TimeZone, string TimeZoneDetails, string IsActive)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CountryMaster obj = new CountryMaster();
            string _CountryID = string.Empty;
            try
            {
                _CountryID = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(CountryID), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _CountryID = "0";
            }

            if (_CountryID == "")
            {
                _CountryID = "0";
            }

            DataSet dscountry = obj.SaveCountryDetails(Int64.Parse(UserId), CountryName, CountryCode, CountryPrefix, TimeZone, TimeZoneDetails, Int32.Parse(_CountryID), Convert.ToBoolean(IsActive));

            if (dscountry != null)
            {
                if (dscountry.Tables.Count > 0)
                {
                    if (dscountry.Tables[0].Rows.Count > 0)
                    {
                        string Response = dscountry.Tables[0].Rows[0]["Response"].ToString();
                        string Message = dscountry.Tables[0].Rows[0]["Message"].ToString();

                        var _result = new { response = Response, message = Message };

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        strJSON = js.Serialize(_result);
                    }
                }
            }

            if (strJSON == string.Empty)
            {
                var _result = new { response = "error", message = "Error while saving Country details, please refresh and try again" };

                JavaScriptSerializer js = new JavaScriptSerializer();
                strJSON = js.Serialize(_result);
            }
        }
        catch (Exception ex)
        {
            var _result = new { response = "error", message = "Error while saving Country details, please refresh and try again" };

            JavaScriptSerializer js = new JavaScriptSerializer();
            strJSON = js.Serialize(_result);
        }

        return strJSON;
    }
}