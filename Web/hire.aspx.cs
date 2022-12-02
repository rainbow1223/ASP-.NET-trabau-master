using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL;

public partial class hire : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayCategories();

            DisplayTopTrending_Freelancers();



        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Categories_Slick", "setTimeout(function () { CategoriesSlick();TrendingFreelancersSlick(); }, 0);", true);
    }

    public void DisplayCategories()
    {
        try
        {
            utility obj = new utility();
            var data = obj.GetCategories().Tables[0].ToDynamic();
            var lst_cat = data.Select(x => new
            {
                x.ServiceCategoryId,
                x.ServiceCategoryName,
                x.CategoryParentId,
                x.CategoryIconPath,
                x.SrNo
            }).ToList();

            var lst_main_cat = lst_cat.Where(a => a.CategoryParentId == 0).OrderBy(o => o.SrNo);

            rCategories.DataSource = lst_main_cat;
            rCategories.DataBind();

            foreach (RepeaterItem item in rCategories.Items)
            {
                Repeater rSubCategories = item.FindControl("rSubCategories") as Repeater;
                string CategoryID = (item.FindControl("lblCategoryId") as Label).Text;
                var sub_cat = lst_cat.Where(x => x.CategoryParentId == Int32.Parse(CategoryID)).Select(y => new
                {
                    EncCateoryId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(y.ServiceCategoryId.ToString() + "|" + y.ServiceCategoryName, Trabau_Keys.Category_Key)),
                    y.ServiceCategoryName,
                    y.SrNo
                }).ToList();
                rSubCategories.DataSource = sub_cat.OrderBy(o => o.SrNo);
                rSubCategories.DataBind();

                if (rSubCategories.Items.Count > 10)
                {
                    (item.FindControl("btnMore") as HtmlInputButton).Visible = true;
                    (item.FindControl("btnLess") as HtmlInputButton).Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void DisplayTopTrending_Freelancers()
    {
        try
        {
            utility obj = new utility();
            string UserId = "";
            try
            {
                UserId = Session["Trabau_UserId"].ToString();
            }
            catch (Exception)
            {
                UserId = "0";
            }
            DataSet ds_freelancers = obj.GetTopTrending_Freelancers(Int64.Parse(UserId));
            var ls_freelancers = ds_freelancers.Tables[0].ToDynamic().Select(x => new
            {
                x.UserId,
                x.Name,
                x.Title,
                x.Overview,
                x.CountryName,
                x.JobSuccessRate,
                profile_id = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.UserId.ToString(), Trabau_Keys.Profile_Key)),
                x.Preferred_List,
                x.Preferred_ListClass,
                x.CanAdd
            }).ToList();
            rFreelancers.DataSource = ls_freelancers;
            rFreelancers.DataBind();

            var all_skills = ds_freelancers.Tables[1].ToDynamic();
            foreach (RepeaterItem item in rFreelancers.Items)
            {
                string _UserId = (item.FindControl("lblUserId") as Label).Text;

                var skills = all_skills.Where(y => y.UserId.ToString() == _UserId).Select(x => new
                {
                    SkillName = x.SkillName,
                    EncSkillId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(x.SkillId.ToString() + "|" + x.SkillName, Trabau_Keys.Skill_Key))
                }).ToList();

                Repeater rFreelancers_Skills = item.FindControl("rFreelancers_Skills") as Repeater;
                rFreelancers_Skills.DataSource = skills;
                rFreelancers_Skills.DataBind();


                if (_UserId != "")
                {
                    _UserId = _UserId + "~" + DateTime.Now.ToString();
                    _UserId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(_UserId, Trabau_Keys.Profile_Key));
                }

                string ran = Guid.NewGuid().ToString();
                (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data-key", ran + "_userpic_" + (item.ItemIndex + 1000).ToString());
                (item.FindControl("div_profile_photo") as HtmlGenericControl).Attributes.Add("data", _UserId);
                (item.FindControl("imgFL_ProfilePic") as HtmlImage).Src = "assets/uploads/loading_pic.gif";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GetTrabauPic_Info", "setTimeout(function () { GetTrabau_PicInfo('1000','" + (rFreelancers.Items.Count + 1000).ToString() + "');}, 0);", true);

        }
        catch (Exception ex)
        {
        }
    }

    //[WebMethod]
    //public static string DisplayCategories()
    //{
    //    var output = new StringWriter();
    //    try
    //    {
    //        var pageHolder = new Page();
    //        var form = new HtmlForm();
    //        var viewControl = (UserControl)pageHolder.LoadControl("~/UserControls/wucCategories.ascx");
    //        var scriptManager = new ScriptManager();
    //        form.Controls.Add(scriptManager);
    //        form.Controls.Add(viewControl);
    //        pageHolder.Controls.Add(form);
    //        HttpContext.Current.Server.Execute(pageHolder, output, false);
    //    }
    //    catch (Exception)
    //    {
    //        return "0";
    //    }
    //    return output.ToString();
    //}


    [WebMethod(EnableSession = true)]
    public static string GetUserPicture(string data)
    {
        string val = "";
        try
        {
            data = MiscFunctions.Base64DecodingMethod(data);
            data = EncyptSalt.DecryptString(data, Trabau_Keys.Profile_Key);
            val = data.Split('~')[0];
            string dd = data.Split('~')[1];
            val = ImageProcessing.GetUserPicture(Int64.Parse(val), 120, 100);

        }
        catch (Exception ex)
        {
            val = "";
        }
        return val;
    }


    [WebMethod(EnableSession = true)]
    public static string AddtoPreferList(string UserId, string Type)
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                utility obj = new utility();
                string _UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();

                UserId = MiscFunctions.Base64DecodingMethod(UserId);
                UserId = EncyptSalt.DecryptString(UserId, Trabau_Keys.Profile_Key);

                DataSet ds_response = obj.AddToPreferList(Int64.Parse(_UserId), Int64.Parse(UserId), Type, HttpContext.Current.Request.UserHostAddress);

                if (ds_response != null)
                {
                    if (ds_response.Tables.Count > 0)
                    {
                        if (ds_response.Tables[0].Rows.Count > 0)
                        {
                            detail.Add("response", "ok");
                            detail.Add("message_response", ds_response.Tables[0].Rows[0]["Response"].ToString());
                            detail.Add("message", ds_response.Tables[0].Rows[0]["ResponseMessage"].ToString());
                            detail.Add("redirect_url", "");
                        }
                        else
                        {
                            detail.Add("response", "ok");
                            detail.Add("message_response", "warning");
                            detail.Add("message", "Error while adding to preferred list, please refresh and try again");
                            detail.Add("redirect_url", "");
                        }
                    }
                    else
                    {
                        detail.Add("response", "ok");
                        detail.Add("message_response", "warning");
                        detail.Add("message", "Error while adding to preferred list, please refresh and try again");
                        detail.Add("redirect_url", "");
                    }
                }
                else
                {
                    detail.Add("response", "ok");
                    detail.Add("message_response", "warning");
                    detail.Add("message", "Error while adding to preferred list, please refresh and try again");
                    detail.Add("redirect_url", "");
                }

            }
            else
            {
                detail.Add("response", "");
                detail.Add("message_response", "warning");
                detail.Add("message", "Please login first for adding to prefer list");
                detail.Add("redirect_url", "login.aspx");
            }


            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }
}