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

public partial class admin_citymaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetCityDetails(string data)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CityMaster obj = new CityMaster();
            string CityId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(data), Trabau_Keys.Admin_Key);
            DataSet dscity = obj.GetCityDetails(Int64.Parse(UserId), Int32.Parse(CityId));
            var lstcity = dscity.Tables[0].ToDynamic()
                .Select(x => new
                {
                    CityName = x.CityName,
                    StateID = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.StateID.ToString(), Trabau_Keys.Admin_Key)),
                    CountryId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CountryId.ToString(), Trabau_Keys.Admin_Key)),
                    IsActive = x.IsActive
                }).ToList();

            var lstcountry = dscity.Tables[1].ToDynamic()
                .Select(x => new
                {
                    CountryId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CountryId.ToString(), Trabau_Keys.Admin_Key)),
                    CountryName = x.CountryName
                }).ToList();

            var _result = new { response = "ok", data = lstcity, countries = lstcountry };

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
    public static string SaveCityDetails(string CityID, string CityName, string StateID, string IsActive)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CityMaster obj = new CityMaster();
            string _CityID = string.Empty;
            try
            {
                _CityID = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(CityID), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _CityID = "0";
            }

            if (_CityID == "")
            {
                _CityID = "0";
            }

            string _StateID = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(StateID), Trabau_Keys.Admin_Key);

            DataSet dscity = obj.SaveCityDetails(Int64.Parse(UserId), CityName, Int32.Parse(_CityID), Int32.Parse(_StateID), Convert.ToBoolean(IsActive));

            if (dscity != null)
            {
                if (dscity.Tables.Count > 0)
                {
                    if (dscity.Tables[0].Rows.Count > 0)
                    {
                        string Response = dscity.Tables[0].Rows[0]["Response"].ToString();
                        string Message = dscity.Tables[0].Rows[0]["Message"].ToString();

                        var _result = new { response = Response, message = Message };

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        strJSON = js.Serialize(_result);
                    }
                }
            }

            if (strJSON == string.Empty)
            {
                var _result = new { response = "error", message = "Error while saving City details, please refresh and try again" };

                JavaScriptSerializer js = new JavaScriptSerializer();
                strJSON = js.Serialize(_result);
            }
        }
        catch (Exception ex)
        {
            var _result = new { response = "error", message = "Error while saving City details, please refresh and try again" };

            JavaScriptSerializer js = new JavaScriptSerializer();
            strJSON = js.Serialize(_result);
        }

        return strJSON;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetStates(string data)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CityMaster obj = new CityMaster();
            string CountryId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(data), Trabau_Keys.Admin_Key);
            DataSet dscity = obj.GetStates(Int64.Parse(UserId), Int32.Parse(CountryId));
            var lst_states = dscity.Tables[0].ToDynamic()
                .Select(x => new
                {
                    StateName = x.StateName,
                    StateId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.StateId.ToString(), Trabau_Keys.Admin_Key))
                }).ToList();
            var _result = new { response = "ok", data = lst_states };

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
    public static string GetCountries()
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CityMaster obj = new CityMaster();
            DataSet ds_country = obj.GetCountries(Int64.Parse(UserId));
            var lst_country = ds_country.Tables[0].ToDynamic()
                .Select(x => new
                {
                    CountryName = x.CountryName,
                    CountryId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CountryId.ToString(), Trabau_Keys.Admin_Key))
                }).ToList();
            var _result = new { response = "ok", data = lst_country };

            JavaScriptSerializer js = new JavaScriptSerializer();
            strJSON = js.Serialize(_result);

        }
        catch (Exception)
        {
        }

        return strJSON;
    }
}