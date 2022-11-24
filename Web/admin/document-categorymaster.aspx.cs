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

public partial class admin_document_categorymaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetDocumentCategoryDetails(string data)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            DocumentMaster obj = new DocumentMaster();
            string RoleId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(data), Trabau_Keys.Admin_Key);
            DataSet ds_cat = obj.GetDocumentDetails(Int64.Parse(UserId), Int32.Parse(RoleId));
            var lststate = ds_cat.Tables[0].ToDynamic()
                .Select(x => new
                {
                    DocumentName = x.DocumentName,
                    DocumentId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.DocumentId.ToString(), Trabau_Keys.Admin_Key)),
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
    public static string SaveDocumentDetails(string DocId, string DocName, string IsActive)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            DocumentMaster obj = new DocumentMaster();
            string _DocId = string.Empty;
            try
            {
                _DocId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(DocId), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _DocId = "0";
            }

            if (_DocId == "")
            {
                _DocId = "0";
            }


            DataSet dsdoc = obj.SaveDocDetails(Int64.Parse(UserId), DocName, Int32.Parse(_DocId), Convert.ToBoolean(IsActive));

            if (dsdoc != null)
            {
                if (dsdoc.Tables.Count > 0)
                {
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        string Response = dsdoc.Tables[0].Rows[0]["Response"].ToString();
                        string Message = dsdoc.Tables[0].Rows[0]["Message"].ToString();

                        var _result = new { response = Response, message = Message };

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        strJSON = js.Serialize(_result);
                    }
                }
            }

            if (strJSON == string.Empty)
            {
                var _result = new { response = "error", message = "Error while saving Document details, please refresh and try again" };

                JavaScriptSerializer js = new JavaScriptSerializer();
                strJSON = js.Serialize(_result);
            }
        }
        catch (Exception ex)
        {
            var _result = new { response = "error", message = "Error while saving Document details, please refresh and try again" };

            JavaScriptSerializer js = new JavaScriptSerializer();
            strJSON = js.Serialize(_result);
        }

        return strJSON;
    }
}