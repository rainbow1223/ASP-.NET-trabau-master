using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.profile;

public partial class profile_user_portfolios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Devices_data"] = null;
            Session["Mobile_P_data"] = null;
            Session["Mobile_PL_data"] = null;
            Session["Mobile_ADK_data"] = null;
            Session["Databases_data"] = null;
            Session["BSE_data"] = null;
            Session["PE_PortfolioId"] = null;

            Session["Portfolio_gallery"] = null;
            ltrlPortfoilio_Header.Text = "Add portfolio project";
            li1.Attributes.Add("class", "active");
            this.Page.Title = "Add Portfolio";

            try
            {
                if (Request.QueryString.Count == 1)
                {
                    if (Request.QueryString["portfolio"] != null)
                    {
                        string PortfolioId = Request.QueryString["portfolio"];
                        PortfolioId = EncyptSalt.Base64Decode(PortfolioId);
                        PortfolioId = EncyptSalt.DecryptText(PortfolioId, "Trabau_Port_Update");

                        string UserID = Session["Trabau_UserId"].ToString();

                        portfolio_changes obj = new portfolio_changes();
                        DataSet ds_details = obj.GetPortfolioDetails(Int64.Parse(UserID), Int32.Parse(PortfolioId));
                        if (ds_details != null)
                        {
                            if (ds_details.Tables.Count > 0)
                            {
                                if (ds_details.Tables[0].Rows.Count > 0)
                                {
                                    Session["PE_PortfolioId"] = PortfolioId;
                                    ltrlPortfoilio_Header.Text = "Portfolio Editor";
                                    this.Page.Title = "Portfolio Editor";
                                    txtProjectTitle.Text = ds_details.Tables[0].Rows[0]["PortfolioTitle"].ToString();
                                    txtCompletiondate.Text = ds_details.Tables[0].Rows[0]["PortfolioCompletionDate"].ToString();
                                    hftemplate_selected.Value = ds_details.Tables[0].Rows[0]["Template"].ToString();
                                    txtProjectDescription.Text = ds_details.Tables[0].Rows[0]["PortfolioProjectDescription"].ToString();
                                    txtProjectURL.Text = ds_details.Tables[0].Rows[0]["PortfolioProjectURL"].ToString();

                                    SetCateoryValue(ds_details.Tables[2].Copy(), "Devices", hfDevices);
                                    SetCateoryValue(ds_details.Tables[2].Copy(), "Mobile_P", hfMobilePlatforms);
                                    SetCateoryValue(ds_details.Tables[2].Copy(), "Mobile_PL", hfMobileProgLanguages);
                                    SetCateoryValue(ds_details.Tables[2].Copy(), "Mobile_ADK", hfMobileAppDevSkills);
                                    SetCateoryValue(ds_details.Tables[2].Copy(), "Databases", hfDatabases);
                                    SetCateoryValue(ds_details.Tables[2].Copy(), "BSE", hfBusinessSizeExperiences);
                                    //DisplayStep3Details();

                                    DataTable dt_gallery = GetGalleryStructure();
                                    for (int i = 0; i < ds_details.Tables[1].Rows.Count; i++)
                                    {
                                        byte[] file_bytes = (byte[])ds_details.Tables[1].Rows[i]["GalleryItem"];
                                        dt_gallery = AddGalleryItem(dt_gallery, file_bytes);
                                    }
                                    Session["Portfolio_gallery"] = dt_gallery;
                                    rGallery.DataSource = dt_gallery;
                                    rGallery.DataBind();
                                }
                                else
                                {
                                    Response.Redirect("profile.aspx");
                                }
                            }
                            else
                            {
                                Response.Redirect("profile.aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect("profile.aspx");
                        }
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("profile.aspx");
            }
        }
    }

    public void SetCateoryValue(DataTable dt, string filter, HiddenField hf)
    {
        try
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "CategoryName='" + filter + "'";
            hf.Value = dv.ToTable().Rows[0]["CategoryValue"].ToString();
            hf.Value = EncryptValues(hf.Value);
        }
        catch (Exception)
        {
            hf.Value = "";
        }
    }

    protected void btnCancelPortfolio_Click(object sender, EventArgs e)
    {
        try
        {
            if (div_step1.Visible)
            {
                Response.Redirect("profile.aspx");
            }
            else if (div_step2.Visible)
            {
                div_step1.Visible = true;
                div_step2.Visible = false;
                div_step3.Visible = false;

                li1.Attributes.Add("class", "active");
                li2.Attributes.Add("class", "");
                li3.Attributes.Add("class", "");
                li4.Attributes.Add("class", "");


                ltrlPortfoilio_Header.Text = "Add portfolio project";
                btnCancelPortfolio.Text = "Cancel";
                lbtnSelectTemplate.Text = "Proceed to Select Template";
                lbtnSelectTemplate.ValidationGroup = "Portfolio_Step1";

                upParent.Update();
            }
            else if (div_step3.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = true;
                div_step3.Visible = false;

                li1.Attributes.Add("class", "");
                li2.Attributes.Add("class", "active");
                li3.Attributes.Add("class", "");
                li4.Attributes.Add("class", "");


                ltrlPortfoilio_Header.Text = "Select template";
                btnCancelPortfolio.Text = "Back";
                lbtnSelectTemplate.Text = "Proceed to Add Details";

                upParent.Update();
            }
            else if (div_step4.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = false;
                div_step3.Visible = true;
                div_step4.Visible = false;

                li1.Attributes.Add("class", "");
                li2.Attributes.Add("class", "");
                li3.Attributes.Add("class", "active");
                li4.Attributes.Add("class", "");


                ltrlPortfoilio_Header.Text = "Add Details";
                btnCancelPortfolio.Text = "Back";
                lbtnSelectTemplate.Text = "Proceed to Preview";
                lbtnSelectTemplate.ValidationGroup = "Portfolio_Step3";
                RegisterSelect2();
                upParent.Update();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnSelectTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            if (div_step1.Visible)
            {
                li1.Attributes.Add("class", "completed");
                li2.Attributes.Add("class", "active");

                ltrlPortfoilio_Header.Text = "Select template";
                div_step1.Visible = false;
                div_step2.Visible = true;
                div_step3.Visible = false;
                btnCancelPortfolio.Text = "Back";
                lbtnSelectTemplate.Text = "Proceed to Add Details";
                lbtnSelectTemplate.ValidationGroup = "Portfolio_Step1";
            }
            else if (div_step2.Visible)
            {
                string selected_template = hftemplate_selected.Value;
                if (selected_template == "1" || selected_template == "2" || selected_template == "3")
                {
                    li1.Attributes.Add("class", "completed");
                    li2.Attributes.Add("class", "completed");
                    li3.Attributes.Add("class", "active");

                    ltrlPortfoilio_Header.Text = "Add details";
                    div_step1.Visible = false;
                    div_step2.Visible = false;
                    div_step3.Visible = true;
                    btnCancelPortfolio.Text = "Back";
                    lbtnSelectTemplate.Text = "Proceed to Preview";
                    lbtnSelectTemplate.ValidationGroup = "Portfolio_Step3";

                    DisplayStep3Details();
                }
            }
            else if (div_step3.Visible)
            {
                string selected_template = hftemplate_selected.Value;
                if (selected_template == "1" || selected_template == "2" || selected_template == "3")
                {
                    li1.Attributes.Add("class", "completed");
                    li2.Attributes.Add("class", "completed");
                    li3.Attributes.Add("class", "completed");
                    li4.Attributes.Add("class", "active");

                    ltrlPortfoilio_Header.Text = "Preview";
                    div_step1.Visible = false;
                    div_step2.Visible = false;
                    div_step3.Visible = false;
                    div_step4.Visible = true;
                    btnCancelPortfolio.Text = "Back";
                    lbtnSelectTemplate.Text = "Publish";

                    DisplayStep4Preview();
                }
            }
            else if (div_step4.Visible)
            {
                string _Title = txtProjectTitle.Text;
                string _CompletionDate = txtCompletiondate.Text;
                string _Template = hftemplate_selected.Value;


                string _Devices = DecryptValues(hfDevices.Value);

                string _Mobile_P = DecryptValues(hfMobilePlatforms.Value);
                string _Mobile_PL = DecryptValues(hfMobileProgLanguages.Value);
                string _Mobile_ADK = DecryptValues(hfMobileAppDevSkills.Value);
                string _Databases = DecryptValues(hfDatabases.Value);
                string _BSE = DecryptValues(hfBusinessSizeExperiences.Value);
                string _ProjectURL = txtProjectURL.Text;
                string _ProjectDescription = txtProjectDescription.Text;
                DataTable dt_gallery = new DataTable();
                string XMLData_gallery = "";
                if (Session["Portfolio_gallery"] != null)
                {
                    var xmlstream = new StringWriter();
                    dt_gallery = Session["Portfolio_gallery"] as DataTable;
                    dt_gallery.Columns.Remove("file_bytes_preview");
                    dt_gallery.Columns.Remove("file_key");
                    //dt_gallery.TableName = "GalleryItem";
                    //dt_gallery.WriteXml(xmlstream, XmlWriteMode.WriteSchema);
                    //XMLData_gallery = xmlstream.ToString();
                    //using (StringWriter sw = new StringWriter())
                    //{
                    //    dt_gallery.WriteXml(sw, false);
                    //    XMLData_gallery = ParseXpathString(sw.ToString());
                    //}
                }
                string UserID = Session["Trabau_UserId"].ToString();
                string PortfolioId = "0";
                try
                {
                    PortfolioId = Session["PE_PortfolioId"].ToString();
                }
                catch (Exception)
                {
                }
                portfolio_changes obj = new portfolio_changes();
                string data = obj.UpdatePortfolio(Int64.Parse(UserID), _Title, _CompletionDate, _Template, _Devices, _Mobile_P, _Mobile_PL, _Mobile_ADK, _Databases, _BSE, _ProjectURL,
                    _ProjectDescription, dt_gallery, Int32.Parse(PortfolioId));
                string response = data.Split(':')[0];
                string message = data.Split(':')[1];
                if (response == "success")
                {
                    if (PortfolioId == "0" || PortfolioId == "")
                    {
                        try
                        {
                            string Freelancer_Name = Session["Trabau_FirstName"].ToString();
                            string EmailId = Session["Trabau_EmailId"].ToString();
                            string Freelancer_UserId = UserID;

                            string template_url = "https://www.trabau.com/emailers/xxddcca/new-portfolio.html";

                            try
                            {
                                WebRequest req = WebRequest.Create(template_url);
                                WebResponse w_res = req.GetResponse();
                                StreamReader sr = new StreamReader(w_res.GetResponseStream());
                                string html = sr.ReadToEnd();

                                html = html.Replace("@Name", Freelancer_Name);

                                string body = html;

                                Emailer obj_email = new Emailer();
                                string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "New Portfolio? Well Done!", body, null);

                                try
                                {
                                    utility log = new utility();
                                    log.CreateEmailerLog(Convert.ToInt64(Freelancer_UserId), EmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
                                }
                                catch (Exception)
                                {
                                }
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    utility log = new utility();
                                    log.CreateEmailerLog(Convert.ToInt64(Freelancer_UserId), EmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Portfolio_Redirect", "setTimeout(function () { window.location.href='profile.aspx'}, 1000);", true);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Portfolio_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
            }
            upParent.Update();
        }
        catch (Exception ex)
        {

        }
    }

    private string ParseXpathString(string input)
    {

        if (input.Contains("'"))
        {
            int myindex = input.IndexOf("'");
            input = input.Insert(myindex, "'");
        }
        return input;
    }

    public string DecryptValues(string _value)
    {
        string value = "";
        try
        {
            for (int i = 0; i < _value.Split(',').Length; i++)
            {
                if (value == "")
                {
                    value = EncyptSalt.DecryptText(_value.Split(',')[i], "TrabauCategory");
                }
                else
                {
                    value = value + "," + EncyptSalt.DecryptText(_value.Split(',')[i], "TrabauCategory");
                }
            }
        }
        catch (Exception)
        {
        }
        return value;
    }

    public string EncryptValues(string _value)
    {
        string value = "";
        try
        {
            for (int i = 0; i < _value.Split(',').Length; i++)
            {
                if (value == "")
                {
                    value = EncyptSalt.EncryptText(_value.Split(',')[i], "TrabauCategory");
                }
                else
                {
                    value = value + "," + EncyptSalt.EncryptText(_value.Split(',')[i], "TrabauCategory");
                }
            }
        }
        catch (Exception)
        {
        }
        return value;
    }

    public void DisplayStep4Preview()
    {
        try
        {
            ltrlTitle.Text = txtProjectTitle.Text;
            BindCategoryPreview(rDevices, "Devices", hfDevices, div_Devices);
            BindCategoryPreview(rMobilePlatforms, "Mobile_P", hfMobilePlatforms, div_MP);
            BindCategoryPreview(rMobileProgrammingLanguages, "Mobile_PL", hfMobileProgLanguages, div_MPL);
            BindCategoryPreview(rMobileAppDevelopmentSkills, "Mobile_ADK", hfMobileAppDevSkills, div_MADS);
            BindCategoryPreview(rDatabases, "Databases", hfDatabases, div_Databases);
            BindCategoryPreview(rBusinessSizeExperience, "BSE", hfBusinessSizeExperiences, div_BSE);
            ltrlProjectDescription.Text = txtProjectDescription.Text;
            string Template = "";
            if (hftemplate_selected.Value == "1")
            {
                Template = "Gallery";
            }
            else if (hftemplate_selected.Value == "2")
            {
                Template = "Case Study";
            }
            else if (hftemplate_selected.Value == "3")
            {
                Template = "Classic";
            }
            ltrlTemplate.Text = Template;
        }
        catch (Exception)
        {
        }
    }

    public void BindCategoryPreview(Repeater rItem, string Category, HiddenField hfData, HtmlGenericControl crtl)
    {
        try
        {
            crtl.Visible = false;
            DataSet ds = Session[Category + "_data"] as DataSet;
            DataView dv = ds.Tables[0].Copy().DefaultView;
            string value = hfData.Value;
            string filter = "";
            for (int i = 0; i < value.Split(',').Length; i++)
            {
                if (filter == "")
                {
                    filter = "CategoryId='" + value.Split(',')[i] + "'";
                }
                else
                {
                    filter = filter + " or CategoryId='" + value.Split(',')[i] + "'";
                }
            }
            dv.RowFilter = filter;
            rItem.DataSource = dv;
            rItem.DataBind();

            crtl.Visible = (dv.Count > 0 ? true : false);
        }
        catch (Exception)
        {
        }
    }

    public void DisplayStep3Details()
    {
        try
        {
            BindCategoryDropDown("Devices", ddlDevices, "hfDevices", "Select Devices");
            BindCategoryDropDown("Mobile_P", ddlMobilePlatforms, "hfMobilePlatforms", "Select Mobile Platforms");
            BindCategoryDropDown("Mobile_PL", ddlMobileProgLanguages, "hfMobileProgLanguages", "Select Mobile Programming Languages");
            BindCategoryDropDown("Mobile_ADK", ddlMobileAppDevSkills, "hfMobileAppDevSkills", "Select Mobile App Dev Skills");
            BindCategoryDropDown("Databases", ddlDatabases, "hfDatabases", "Select Databases");
            BindCategoryDropDown("BSE", ddlBusinessSizeExperiences, "hfBusinessSizeExperiences", "Select Business Size Experiences");

        }
        catch (Exception)
        {
        }
    }

    public void BindCategoryDropDown(string CategoryType, DropDownList ddl, string hiddenfield, string select_message)
    {
        try
        {
            DataSet ds = new DataSet();
            if (Session[CategoryType + "_data"] != null)
            {
                ds = Session[CategoryType + "_data"] as DataSet;
            }
            else
            {
                portfolio_changes obj = new portfolio_changes();
                ds = obj.Get_Generic_Categories(CategoryType);
                Session[CategoryType + "_data"] = ds;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["CategoryId"] = EncyptSalt.EncryptText(ds.Tables[0].Rows[i]["CategoryId"].ToString(), "TrabauCategory");
                }
            }
            ddl.DataSource = ds;
            ddl.DataTextField = "CategoryName";
            ddl.DataValueField = "CategoryId";
            ddl.DataBind();


            ScriptManager.RegisterStartupScript(this, this.GetType(), ddl.ID + "_open_list", "setTimeout(function () {RegisterSelect2('" + ddl.ID + "', '" + select_message + "', '" + hiddenfield + "','0');}, 100);", true);
            //if (ddl.ID != "ddlDevices")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), ddl.ID + "_Dropdown_Select", "setTimeout(function () { $('[Id*=" + ddl.ID + "]').select2();}, 50);", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "open_devices_list", "setTimeout(function () {RegisterSelect2('ddlDevices', 'Select devices from the list', 'hfDevices','0');}, 100);", true);
            //}
        }
        catch (Exception)
        {
        }
    }

    protected void afuAttachments_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        try
        {
            // System.Threading.Thread.Sleep(5000);
            if (afuAttachments.HasFile)
            {
                DataTable dt_gallery = new DataTable();
                if (Session["Portfolio_gallery"] != null)
                {
                    dt_gallery = Session["Portfolio_gallery"] as DataTable;
                }
                else
                {
                    dt_gallery = GetGalleryStructure();
                }
                byte[] attachment = afuAttachments.FileBytes;
                //  attachment = ImageProcessing.ScaleFreeHeight(attachment, 400, 300);
                dt_gallery = AddGalleryItem(dt_gallery, attachment);
                Session["Portfolio_gallery"] = dt_gallery;
            }
        }
        catch (Exception)
        {
        }
    }

    public DataTable GetGalleryStructure()
    {
        DataTable dt_gallery = new DataTable();
        dt_gallery.Columns.Add("file_key", typeof(string));
        dt_gallery.Columns.Add("file_bytes", typeof(byte[]));
        dt_gallery.Columns.Add("file_bytes_preview", typeof(string));
        return dt_gallery;
    }

    public DataTable AddGalleryItem(DataTable dt_gallery, byte[] file_bytes)
    {
        try
        {
            DataRow dr = dt_gallery.NewRow();
            dr["file_key"] = RandomString(20).ToLower();
            dr["file_bytes"] = file_bytes;
            dr["file_bytes_preview"] = "data:image;base64," + Convert.ToBase64String(ImageProcessing.ScaleFreeHeight(file_bytes, 300, 400));
            dt_gallery.Rows.Add(dr);
        }
        catch (Exception)
        {
        }

        return dt_gallery;
    }

    protected void btnRefreshGalleryItems_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt_gallery = new DataTable();
            if (Session["Portfolio_gallery"] != null)
            {
                dt_gallery = Session["Portfolio_gallery"] as DataTable;
                rGallery.DataSource = dt_gallery;
                rGallery.DataBind();
            }
            RegisterSelect2();
        }
        catch (Exception)
        {

        }
    }

    public void RegisterSelect2()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlDevices_open_list", "setTimeout(function () {RegisterSelect2('ddlDevices', 'Select Devices', 'hfDevices','0');}, 100);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlMobilePlatforms_open_list", "setTimeout(function () {RegisterSelect2('ddlMobilePlatforms', 'Select Mobile Platforms', 'hfMobilePlatforms','0');}, 100);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlMobileProgLanguages_open_list", "setTimeout(function () {RegisterSelect2('ddlMobileProgLanguages', 'Select Mobile Programming Languages', 'hfMobileProgLanguages','0');}, 100);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlMobileAppDevSkills_open_list", "setTimeout(function () {RegisterSelect2('ddlMobileAppDevSkills', 'Select Mobile App Dev Skills', 'hfMobileAppDevSkills','0');}, 100);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlDatabases_open_list", "setTimeout(function () {RegisterSelect2('ddlDatabases', 'Select Databases', 'hfDatabases','0');}, 100);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlBusinessSizeExperiences_open_list", "setTimeout(function () {RegisterSelect2('ddlBusinessSizeExperiences', 'Select Business Size Experiences', 'hfBusinessSizeExperiences','0');}, 100);", true);
    }

    protected void lbtnRemoveMedia_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = ((LinkButton)(sender)).Parent as RepeaterItem;
            string file_key = (item.FindControl("lbl_file_key") as Label).Text;
            hfRemoveItemKey.Value = file_key;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Remove_Media_Item_Confirmation", "setTimeout(function () { Swal.fire({title: 'Are you sure to remove this?',text: 'You wont be able to revert this!',icon: 'warning',showCancelButton: true,confirmButtonColor: '#3085d6',cancelButtonColor: '#d33',confirmButtonText: 'Yes, delete it!'}).then((result) => {if (result.value){$('#btnRemoveGalleryItem').click();}else {$('#hfRemoveItemKey').val('');}})}, 50);", true);
            RegisterSelect2();
        }
        catch (Exception)
        {

        }
    }

    protected void btnRemoveGalleryItem_Click(object sender, EventArgs e)
    {
        try
        {
            string file_key = hfRemoveItemKey.Value;
            hfRemoveItemKey.Value = string.Empty;
            DataTable dt_gallery = new DataTable();
            if (Session["Portfolio_gallery"] != null)
            {
                dt_gallery = Session["Portfolio_gallery"] as DataTable;
                dt_gallery.Select("file_key='" + file_key + "'").Delete();
                //foreach (DataRow row in dt_gallery.Rows)
                //{
                //    if (row["file_key"].ToString().Trim().Contains(file_key))
                //        dt_gallery.Rows.Remove(row);
                //}
                Session["Portfolio_gallery"] = dt_gallery;
                rGallery.DataSource = dt_gallery;
                rGallery.DataBind();

                upPortfolioSteps.Update();
            }
            RegisterSelect2();
        }
        catch (Exception ex)
        {
        }
    }


    public string RandomString(int length)
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}