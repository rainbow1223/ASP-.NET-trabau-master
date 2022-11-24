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

public partial class admin_generic_categories : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetCategoryDetails(string data)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            GenericCategoryMaster obj = new GenericCategoryMaster();
            string StateId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(data), Trabau_Keys.Admin_Key);
            DataSet dscat = obj.GetCategoryDetails(Int64.Parse(UserId), Int32.Parse(StateId));
            var lstcat = dscat.Tables[0].ToDynamic()
                .Select(x => new
                {
                    CategoryName = x.CategoryName,
                    CategoryId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CategoryId.ToString(), Trabau_Keys.Admin_Key)),
                    CategoryType = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CategoryType, Trabau_Keys.Admin_Key)),
                    IsActive = x.IsActive
                }).ToList();

            var _result = new { response = "ok", data = lstcat };

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
    public static string GetCategoryTypes()
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            GenericCategoryMaster obj = new GenericCategoryMaster();
            DataSet ds_cat_types = obj.GetCategoryTypes(Int64.Parse(UserId));
            var lst_cat_types = ds_cat_types.Tables[0].ToDynamic()
                .Select(x => new
                {
                    CategoryType = x.CategoryType,
                    CategoryId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CategoryType, Trabau_Keys.Admin_Key))
                }).ToList();
            var _result = new { response = "ok", data = lst_cat_types };

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
    public static string SaveCategoryDetails(string CategoryId, string CategoryName, string CategoryType, string IsActive)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            GenericCategoryMaster obj = new GenericCategoryMaster();
            string _CategoryId = string.Empty;
            try
            {
                _CategoryId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(CategoryId), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _CategoryId = "0";
            }

            if (_CategoryId == "")
            {
                _CategoryId = "0";
            }

            string _CategoryType = string.Empty;
            try
            {
                _CategoryType = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(CategoryType), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _CategoryType = "";
            }
            DataSet ds_cat = obj.SaveCategoryDetails(Int64.Parse(UserId), CategoryName, Int32.Parse(_CategoryId), _CategoryType, Convert.ToBoolean(IsActive));

            if (ds_cat != null)
            {
                if (ds_cat.Tables.Count > 0)
                {
                    if (ds_cat.Tables[0].Rows.Count > 0)
                    {
                        string Response = ds_cat.Tables[0].Rows[0]["Response"].ToString();
                        string Message = ds_cat.Tables[0].Rows[0]["Message"].ToString();

                        var _result = new { response = Response, message = Message };

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        strJSON = js.Serialize(_result);
                    }
                }
            }

            if (strJSON == string.Empty)
            {
                var _result = new { response = "error", message = "Error while saving Generic Category details, please refresh and try again" };

                JavaScriptSerializer js = new JavaScriptSerializer();
                strJSON = js.Serialize(_result);
            }
        }
        catch (Exception ex)
        {
            var _result = new { response = "error", message = "Error while saving Generic Category details, please refresh and try again" };

            JavaScriptSerializer js = new JavaScriptSerializer();
            strJSON = js.Serialize(_result);
        }

        return strJSON;
    }
}