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

public partial class admin_skillmaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetSkillDetails(string data)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            SkillMaster obj = new SkillMaster();
            string SkillId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(data), Trabau_Keys.Admin_Key);
            DataSet dsstate = obj.GetSkillDetails(Int64.Parse(UserId), Int32.Parse(SkillId));
            var lststate = dsstate.Tables[0].ToDynamic()
                .Select(x => new
                {
                    SkillName = x.SkillName,
                    SkillId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.SkillId.ToString(), Trabau_Keys.Admin_Key)),
                    SkillType = x.SkillType,
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
    public static string SaveSkillDetails(string SkillId, string SkillName, string SkillType, string IsActive)
    {
        string strJSON = "";
        try
        {
            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            SkillMaster obj = new SkillMaster();
            string _SkillId = string.Empty;
            try
            {
                _SkillId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(SkillId), Trabau_Keys.Admin_Key);
            }
            catch (Exception)
            {
                _SkillId = "0";
            }

            if (_SkillId == "")
            {
                _SkillId = "0";
            }


            DataSet ds_skill = obj.SaveSkillDetails(Int64.Parse(UserId), SkillName, Int32.Parse(_SkillId), SkillType, Convert.ToBoolean(IsActive));

            if (ds_skill != null)
            {
                if (ds_skill.Tables.Count > 0)
                {
                    if (ds_skill.Tables[0].Rows.Count > 0)
                    {
                        string Response = ds_skill.Tables[0].Rows[0]["Response"].ToString();
                        string Message = ds_skill.Tables[0].Rows[0]["Message"].ToString();

                        var _result = new { response = Response, message = Message };

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        strJSON = js.Serialize(_result);
                    }
                }
            }

            if (strJSON == string.Empty)
            {
                var _result = new { response = "error", message = "Error while saving Skill details, please refresh and try again" };

                JavaScriptSerializer js = new JavaScriptSerializer();
                strJSON = js.Serialize(_result);
            }
        }
        catch (Exception ex)
        {
            var _result = new { response = "error", message = "Error while saving Skill details, please refresh and try again" };

            JavaScriptSerializer js = new JavaScriptSerializer();
            strJSON = js.Serialize(_result);
        }

        return strJSON;
    }
}