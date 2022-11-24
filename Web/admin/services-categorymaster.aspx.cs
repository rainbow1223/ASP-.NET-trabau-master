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

public partial class admin_services_categorymaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetServiceCategoryDetails(string data)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CategoryMaster obj = new CategoryMaster();
            string StateId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(data), Trabau_Keys.Admin_Key);
            DataSet dsstate = obj.GetServiceCategoryDetails(Int64.Parse(UserId), Int32.Parse(StateId));
            var lststate = dsstate.Tables[0].ToDynamic()
                .Select(x => new
                {
                    ServiceCategoryName = x.ServiceCategoryName,
                    ServiceCategoryId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.ServiceCategoryId.ToString(), Trabau_Keys.Admin_Key)),
                    CategoryParentId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CategoryParentId.ToString(), Trabau_Keys.Admin_Key)),
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
    public static string GetCategoryTypes()
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CategoryMaster obj = new CategoryMaster();
            DataSet ds_services = obj.GetCategoryTypes(Int64.Parse(UserId));
            var lst_services = ds_services.Tables[0].ToDynamic()
                .Select(x => new
                {
                    ServiceCategoryName = x.ServiceCategoryName,
                    CategoryTypeId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CategoryTypeId.ToString(), Trabau_Keys.Admin_Key))
                }).ToList();
            var _result = new { response = "ok", data = lst_services };

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
    public static string SaveServiceCategory(string ServiceCategoryId, string ServiceCategoryName, string CategoryTypeId, string IsActive)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CategoryMaster obj = new CategoryMaster();
            string _ServiceCategoryId = string.Empty;
            try
            {
                _ServiceCategoryId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(ServiceCategoryId), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _ServiceCategoryId = "0";
            }

            if (_ServiceCategoryId == "")
            {
                _ServiceCategoryId = "0";
            }

            string _CategoryTypeId = string.Empty;
            try
            {
                _CategoryTypeId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(CategoryTypeId), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _CategoryTypeId = "0";
            }

            if (_CategoryTypeId == "")
            {
                _CategoryTypeId = "0";
            }

            DataSet ds_services = obj.SaveServiceCategory(Int64.Parse(UserId), ServiceCategoryName, Int32.Parse(_ServiceCategoryId), Int32.Parse(_CategoryTypeId), Convert.ToBoolean(IsActive));

            if (ds_services != null)
            {
                if (ds_services.Tables.Count > 0)
                {
                    if (ds_services.Tables[0].Rows.Count > 0)
                    {
                        string Response = ds_services.Tables[0].Rows[0]["Response"].ToString();
                        string Message = ds_services.Tables[0].Rows[0]["Message"].ToString();

                        var _result = new { response = Response, message = Message };

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        strJSON = js.Serialize(_result);
                    }
                }
            }

            if (strJSON == string.Empty)
            {
                var _result = new { response = "error", message = "Error while saving Service Category, please refresh and try again" };

                JavaScriptSerializer js = new JavaScriptSerializer();
                strJSON = js.Serialize(_result);
            }
        }
        catch (Exception ex)
        {
            var _result = new { response = "error", message = "Error while saving Service Category, please refresh and try again" };

            JavaScriptSerializer js = new JavaScriptSerializer();
            strJSON = js.Serialize(_result);
        }

        return strJSON;
    }
}