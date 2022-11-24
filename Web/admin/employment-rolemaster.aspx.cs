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

public partial class admin_employment_rolemaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetRoleDetails(string data)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            RoleMaster obj = new RoleMaster();
            string RoleId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(data), Trabau_Keys.Admin_Key);
            DataSet dsstate = obj.GetRoleDetails(Int64.Parse(UserId), Int32.Parse(RoleId));
            var lststate = dsstate.Tables[0].ToDynamic()
                .Select(x => new
                {
                    RoleName = x.RoleName,
                    RoleId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.RoleId.ToString(), Trabau_Keys.Admin_Key)),
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
    public static string SaveRoleDetails(string RoleId, string RoleName, string IsActive)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            RoleMaster obj = new RoleMaster();
            string _RoleId = string.Empty;
            try
            {
                _RoleId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(RoleId), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _RoleId = "0";
            }

            if (_RoleId == "")
            {
                _RoleId = "0";
            }


            DataSet dsrole = obj.SaveRoleDetails(Int64.Parse(UserId), RoleName, Int32.Parse(_RoleId), Convert.ToBoolean(IsActive));

            if (dsrole != null)
            {
                if (dsrole.Tables.Count > 0)
                {
                    if (dsrole.Tables[0].Rows.Count > 0)
                    {
                        string Response = dsrole.Tables[0].Rows[0]["Response"].ToString();
                        string Message = dsrole.Tables[0].Rows[0]["Message"].ToString();

                        var _result = new { response = Response, message = Message };

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        strJSON = js.Serialize(_result);
                    }
                }
            }

            if (strJSON == string.Empty)
            {
                var _result = new { response = "error", message = "Error while saving Role details, please refresh and try again" };

                JavaScriptSerializer js = new JavaScriptSerializer();
                strJSON = js.Serialize(_result);
            }
        }
        catch (Exception ex)
        {
            var _result = new { response = "error", message = "Error while saving Role details, please refresh and try again" };

            JavaScriptSerializer js = new JavaScriptSerializer();
            strJSON = js.Serialize(_result);
        }

        return strJSON;
    }
}