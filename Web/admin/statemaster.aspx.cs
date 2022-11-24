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

public partial class admin_statemaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetStateDetails(string data)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            StateMaster obj = new StateMaster();
            string StateId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(data), Trabau_Keys.Admin_Key);
            DataSet dsstate = obj.GetStateDetails(Int64.Parse(UserId), Int32.Parse(StateId));
            var lststate = dsstate.Tables[0].ToDynamic()
                .Select(x => new
                {
                    StateName = x.StateName,
                    StateId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.StateId.ToString(), Trabau_Keys.Admin_Key)),
                    CountryId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CountryId.ToString(), Trabau_Keys.Admin_Key)),
                    IsActive = x.IsActive
                }).ToList();

            var _result = new { response = "ok", data = lststate };

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


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveStateDetails(string StateID, string StateName, string CountryId, string IsActive)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            StateMaster obj = new StateMaster();
            string _StateID = string.Empty;
            try
            {
                _StateID = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(StateID), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _StateID = "0";
            }

            if (_StateID == "")
            {
                _StateID = "0";
            }

            string _CountryId = string.Empty;
            try
            {
                _CountryId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(CountryId), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _CountryId = "0";
            }

            if (_CountryId == "")
            {
                _CountryId = "0";
            }

            DataSet dsstate = obj.SaveStateDetails(Int64.Parse(UserId), StateName, Int32.Parse(_StateID), Int32.Parse(_CountryId), Convert.ToBoolean(IsActive));

            if (dsstate != null)
            {
                if (dsstate.Tables.Count > 0)
                {
                    if (dsstate.Tables[0].Rows.Count > 0)
                    {
                        string Response = dsstate.Tables[0].Rows[0]["Response"].ToString();
                        string Message = dsstate.Tables[0].Rows[0]["Message"].ToString();

                        var _result = new { response = Response, message = Message };

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        strJSON = js.Serialize(_result);
                    }
                }
            }

            if (strJSON == string.Empty)
            {
                var _result = new { response = "error", message = "Error while saving State details, please refresh and try again" };

                JavaScriptSerializer js = new JavaScriptSerializer();
                strJSON = js.Serialize(_result);
            }
        }
        catch (Exception ex)
        {
            var _result = new { response = "error", message = "Error while saving State details, please refresh and try again" };

            JavaScriptSerializer js = new JavaScriptSerializer();
            strJSON = js.Serialize(_result);
        }

        return strJSON;
    }
}