using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.profile.settings;
using System.Web.UI.HtmlControls;
using TrabauClassLibrary.DLL.SignUp;
using System.Net;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.DLL;

public partial class profile_settings_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnUpload);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(lbtnNewFreelancerAccount);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(lbtnNewClientAccount);
        if (!IsPostBack)
        {
            try
            {
                CheckPreRegistrationStatus();
            }
            catch (Exception)
            {

            }
        }
    }

    public void LoadNavigation()
    {
        try
        {
            DataTable dt_nav = new DataTable();
            //dt_nav.Columns.Add("NavigationId", typeof(int));
            //dt_nav.Columns.Add("NavigationName", typeof(string));
            //dt_nav.Columns.Add("ParentId", typeof(int));
            //dt_nav.Columns.Add("Action", typeof(string));
            //dt_nav.Columns.Add("NavigationClass", typeof(string));

            //dt_nav = AddNavigation(dt_nav, 1, "User Settings", 0, "", "");
            //dt_nav = AddNavigation(dt_nav, 2, "Contact Info", 1, "contact_info", "active");
            //dt_nav = AddNavigation(dt_nav, 3, "Upload Documents", 1, "upload_docs", "");
            //dt_nav = AddNavigation(dt_nav, 4, "My Profile", 1, "my_profile", "");
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds_nav = obj.GetSettingsNavigation(Int64.Parse(UserID), "");
            if (ds_nav != null)
            {
                if (ds_nav.Tables.Count > 0)
                {
                    dt_nav = ds_nav.Tables[0];


                    DataView dv_main_nav = dt_nav.Copy().DefaultView;
                    dv_main_nav.RowFilter = "ParentId=0";

                    rNavigation_main.DataSource = dv_main_nav;
                    rNavigation_main.DataBind();

                    foreach (RepeaterItem item in rNavigation_main.Items)
                    {
                        Repeater rNavigation = (item.FindControl("rNavigation") as Repeater);
                        string Nav_Id_main = (item.FindControl("lblNavigationId_main") as Label).Text;
                        DataView dv_nav = dt_nav.Copy().DefaultView;
                        dv_nav.RowFilter = "ParentId=" + Nav_Id_main;

                        rNavigation.DataSource = dv_nav;
                        rNavigation.DataBind();
                    }


                    ddlNavigation.DataSource = dt_nav.ToDynamic().Where(x => x.ParentId > 0).Select(a => new { NavigationName = a.NavigationName, Action = a.Action }).ToList();
                    ddlNavigation.DataTextField = "NavigationName";
                    ddlNavigation.DataValueField = "Action";
                    ddlNavigation.DataBind();

                    ddlNavigation.SelectedValue = "contact_info";

                    ChangeNavigation("contact_info");
                }
            }




        }
        catch (Exception)
        {
        }
    }

    public DataTable AddNavigation(DataTable dt_nav, int NavigationId, string NavigationName, int ParentId, string Action, string NavigationClass)
    {
        try
        {
            DataRow dr = dt_nav.NewRow();
            dr["NavigationId"] = NavigationId;
            dr["NavigationName"] = NavigationName;
            dr["ParentId"] = ParentId;
            dr["Action"] = Action;
            dr["NavigationClass"] = NavigationClass;

            dt_nav.Rows.Add(dr);
        }
        catch (Exception)
        {

        }
        return dt_nav;
    }

    public void CheckPreRegistrationStatus()
    {
        Tuple<bool, string> val = new Tuple<bool, string>(false, "");
        try
        {
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();
            string data = obj.CheckPreRegistrationStatus(Int64.Parse(UserID));
            string _response = data.Split(':')[0];
            string _redirecturl = data.Split(':')[1];
            if (_response == "0")
            {
                Response.Redirect(_redirecturl);
            }
            else
            {
                LoadNavigation();
                //LoadUploadedDocuments();// Tab data for documents upload of profile
            }
        }
        catch (Exception)
        {
            val = new Tuple<bool, string>(false, "");
        }
    }

    public void LoadUploadedDocuments()
    {
        try
        {
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds_docs = obj.GetUploadedDocument(Int64.Parse(UserID), 0);
            rDocuments.DataSource = ds_docs;
            rDocuments.DataBind();

            foreach (RepeaterItem item in rDocuments.Items)
            {
                try
                {
                    (item.FindControl("rbtnlVisibility") as RadioButtonList).SelectedValue = (item.FindControl("lblDocumentVisibility") as Label).Text;
                }
                catch (Exception)
                {
                }

            }

            DataSet ds_all_docs = obj.GetDocuments(Int64.Parse(UserID));
            ddlDocumentType.DataSource = ds_all_docs;
            ddlDocumentType.DataTextField = "DocumentName";
            ddlDocumentType.DataValueField = "Id";
            ddlDocumentType.DataBind();

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "DocumentType_Select", "setTimeout(function () { $('[Id*=ddlDocumentType]').select2();}, 50);", true);
        }
        catch (Exception)
        {
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string valid_types = ltrlValidTypes.Text;
            string DocId = ddlDocumentType.SelectedValue;
            string DocName = ddlDocumentType.SelectedItem.Text;
            if (fu_profile_document.HasFile || (DocName == "Youtube Video"))
            {
                byte[] file_bytes = fu_profile_document.FileBytes;
                Tuple<bool, string> _filestatus = MimeType_Validation.CheckMimeType(file_bytes, "image/jpeg,image/jpg,image/png,image/gif,application/pdf,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/msword,image/tiff");
                if (_filestatus.Item1 || DocName == "Youtube Video")
                {

                    settings_changes obj = new settings_changes();
                    string UserID = Session["Trabau_UserId"].ToString();
                    if (DocName == "Youtube Video")
                    {
                        file_bytes = null;
                    }
                    string OtherDocType = txtOtherDocumentType.Text;
                    bool YoutubeVideoStatus = false;
                    if (DocName == "Youtube Video")
                    {
                        OtherDocType = OtherDocType.Replace("youtu.be/", "www.youtube.com/embed/");
                        if (OtherDocType.Contains("www.youtube.com/watch"))
                        {
                            try
                            {
                                string[] stringSeparators = new string[] { "?v=" };
                                string[] result = OtherDocType.Split(stringSeparators, StringSplitOptions.None);
                                if (result[1] != string.Empty)
                                {
                                    OtherDocType = "https://www.youtube.com/embed/" + result[1];
                                    YoutubeVideoStatus = true;
                                }
                            }
                            catch (Exception)
                            {
                            }

                        }
                        else if (OtherDocType.Contains("www.youtube.com/embed"))
                        {
                            YoutubeVideoStatus = true;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Document_YouTube_Message", "setTimeout(function () { toastr['error']('Please Enter valid youtube embed URL (e.g. https://www.youtube.com/embed/********* or https://www.youtube.com/watch?v=*********)');}, 200);", true);
                        }
                    }
                    if (DocName != "Youtube Video" || YoutubeVideoStatus)
                    {
                        string YouTubeVideoName = txtYouTubeVideoName.Text;
                        string data = obj.UploadDocument(Int64.Parse(UserID), file_bytes, Int64.Parse(UserID), Request.UserHostAddress, Int32.Parse(DocId), _filestatus.Item2, OtherDocType,
                            YouTubeVideoName);
                        string _response = data.Split(':')[0];
                        string _message = data.Split(':')[1];
                        if (_response == "success")
                        {
                            ClearUploadControls();
                            LoadUploadedDocuments();
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Document_Update_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Document_Update_Message", "setTimeout(function () { toastr['error']('File type not valid, please upload " + valid_types + " file only');}, 200);", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Document_Update_Message", "setTimeout(function () { toastr['error']('Please select a file and upload " + valid_types + " file only');}, 200);", true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Document_Update_Message", "setTimeout(function () { toastr['error']('Error while uploading document, please refresh and try again');}, 200);", true);
        }

        this.Title = "Documents";
    }

    protected void lbtnViewDocument_Click(object sender, EventArgs e)
    {
        string _path = "";
        try
        {
            RepeaterItem row = (sender as LinkButton).Parent as RepeaterItem;
            string Doc_Id = (row.FindControl("lblDocumentId") as Label).Text;
            string DocumentExtension = (row.FindControl("lblDocumentExtension") as Label).Text;
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds_docs = obj.GetIndvidualUploadedDocument(Int64.Parse(UserID), Int32.Parse(Doc_Id));

            if (ds_docs != null)
            {
                if (ds_docs.Tables.Count > 0)
                {
                    if (ds_docs.Tables[0].Rows.Count > 0)
                    {
                        byte[] doc_bytes = (byte[])(ds_docs.Tables[0].Rows[0]["Document_bytes"]);

                        if (DocumentExtension == ".pdf")
                        {
                            // Load the document from disk.


                            Guid g = Guid.NewGuid();
                            string extension = DocumentExtension;
                            string filename = g.ToString() + extension;
                            //Save the Byte Array as File.
                            string filePath = "~/offline_files/" + Path.GetFileName(filename);

                            _path = Server.MapPath(filePath);
                            Session["Temp_Preview_FilePath"] = _path;
                            try
                            {
                                File.WriteAllBytes(_path, doc_bytes);
                            }
                            catch (Exception ex1)
                            {
                                hfError.Value = ex1.Message;
                            }

                            //Document doc = new Document(_path);

                            //HtmlSaveOptions options = new HtmlSaveOptions();

                            //options.ExportRoundtripInformation = true;

                            //String html = doc.FirstSection.Body.ToString(options);
                            //html = html.Replace("Evaluation Only. Created with Aspose.Words. Copyright 2003-2020 Aspose Pty Ltd.", "");

                            //   hfError.Value = _path;
                            // string base64PDF = System.Convert.ToBase64String(doc_bytes, 0, doc_bytes.Length);
                            //  string path = "http://trabau.com/offline_files/" + filename;
                            // div_doc_data.InnerHtml = "<object data='data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64," + base64PDF + "' type='application/vnd.openxmlformats-officedocument.wordprocessingml.document' width='100%'><embed src = 'data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64," + base64PDF + "' type='application/vnd.openxmlformats-officedocument.wordprocessingml.document' /></object>";
                            // div_doc_data.InnerHtml = "<iframe src='http://docs.google.com/gview?url=" + path + "&embedded=true' style='width:100%; height:500px;' frameborder='0'></iframe>";
                            // div_doc_data.InnerHtml = html;
                        }
                        else
                        {
                            string base64PDF = System.Convert.ToBase64String(doc_bytes, 0, doc_bytes.Length);
                            div_doc_data.InnerHtml = "<object data='application/pdf;base64," + base64PDF + "' type='application/pdf' width='100%'><embed style='width: 100%;height: 450px;' src='data:application/pdf;base64," + base64PDF + "' type='application/pdf' /></object>";
                        }
                    }
                }
            }
            divTrabau_DocumentView.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_DocumentView_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_DocumentView');}, 150);", true);
        }
        catch (Exception ex)
        {
            //  hfError.Value = _path;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Document_Preview_Message", "setTimeout(function () { toastr['error']('Error while previewing document, please refresh and try again');}, 200);", true);
        }
        this.Title = "Documents";
    }

    protected void btnClose_document_top_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                File.Delete(Session["Temp_Preview_FilePath"].ToString());

                if (!File.Exists(Session["Temp_Preview_FilePath"].ToString()))
                {
                    Session["Temp_Preview_FilePath"] = null;
                }
            }
            catch (Exception)
            {

            }
            divTrabau_DocumentView.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_DocumentView_Popup", "setTimeout(function () {HandlePopUp('0','divTrabau_DocumentView');}, 150);", true);
        }
        catch (Exception)
        {
        }
        this.Title = "Documents";
    }

    protected void rbtnlVisibility_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem row = (sender as RadioButtonList).Parent as RepeaterItem;
            string UploadedDocId = (row.FindControl("lblUploadedDocId") as Label).Text;
            string Visibility = (row.FindControl("rbtnlVisibility") as RadioButtonList).SelectedValue;

            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();
            string data = obj.UploadDocumentVisibility(Int64.Parse(UserID), Int32.Parse(UploadedDocId), Visibility);

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];

            //if (response != "success")
            //{
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Document_Visibility_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
            //}
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnNavigation_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = ((LinkButton)sender).Parent.Parent as RepeaterItem;
            ClearNavigationActive();
            (item.FindControl("li_nav") as HtmlGenericControl).Attributes.Add("class", "active");

            string nav_action = (item.FindControl("lblNavigation_Action") as Label).Text;
            ChangeNavigation(nav_action);
        }
        catch (Exception ex)
        {
        }
    }

    public void ChangeNavigation(string nav_action)
    {
        try
        {
            div_contact_info.Visible = false;
            div_Docs_Upload.Visible = false;
            div_newaccount.Visible = false;

            AccountEditableChange(false);
            LocationEditableChange(false);
            CompanyEditChange(false);
            ClearUploadControls();
            switch (nav_action)
            {
                case "contact_info":
                    div_contact_info.Visible = true;

                    DisplayContactInfo();
                    break;
                case "upload_docs":
                    div_Docs_Upload.Visible = true;
                    h1_Header.InnerText = "Documents";
                    this.Page.Title = "Documents";
                    LoadUploadedDocuments();
                    break;
                case "my_profile":
                    Response.Redirect("../user/profile.aspx");
                    break;
                case "new_freelancer_account":
                    h1_Header.InnerText = "Create a freelancer profile";
                    this.Page.Title = "Become a Freelancer";
                    h2_newacc_header.InnerText = "Create a profile to find jobs and earn money as a freelancer";
                    div_newacc_info.InnerHtml = "Don't worry, you'll still be a client. We'll create a separate freelancer account that you can access with the same login.";
                    btnCreateNewAccount.Text = "Become a Freelancer";
                    div_newaccount.Visible = true;
                    break;
                case "new_client_account":
                    h1_Header.InnerText = "Create a Client profile";
                    this.Page.Title = "Become a Client";
                    h2_newacc_header.InnerText = "Create a profile to Hire, manage and pay as a different company. Each client company has its own freelancers, payment methods and reports ";
                    div_newacc_info.InnerHtml = "Don't worry, you'll still be a freelancer. We'll create a separate client account that you can access with the same login.";
                    btnCreateNewAccount.Text = "Become a Client";
                    div_newaccount.Visible = true;
                    break;
                default:
                    break;
            }

        }
        catch (Exception)
        {
        }
    }

    public void ClearNavigationActive()
    {
        try
        {
            foreach (RepeaterItem item in rNavigation_main.Items)
            {
                Repeater rNavigation = (item.FindControl("rNavigation") as Repeater);
                foreach (RepeaterItem item_child in rNavigation.Items)
                {
                    (item_child.FindControl("li_nav") as HtmlGenericControl).Attributes.Clear();
                }
            }
        }
        catch (Exception)
        {
        }
    }

    #region Contact Info

    public void AccountEditableChange(bool Editable)
    {
        ltrlUserType.Visible = !Editable;
        ltrlUserId.Visible = !Editable;
        txtUserId.Visible = Editable;

        ltrlFName.Visible = !Editable;
        txtFName.Visible = Editable;

        ltrlLName.Visible = !Editable;
        txtLName.Visible = Editable;

        ltrlEmailId.Visible = !Editable;
        txtEmailAddress.Visible = Editable;

        div_save_contact_details.Visible = Editable;
        lbtnEdit_ContactInfo.Visible = !Editable;
    }
    public void DisplayContactInfo()
    {
        try
        {
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds_contact_info = obj.GetContactInfo(Int64.Parse(UserID));
            if (ds_contact_info != null)
            {
                if (ds_contact_info.Tables.Count > 0)
                {
                    if (ds_contact_info.Tables[0].Rows.Count > 0)
                    {
                        ViewState["User_Contact_Details"] = ds_contact_info;
                        string _UserId = ds_contact_info.Tables[0].Rows[0]["UserId"].ToString();
                        string _FirstName = ds_contact_info.Tables[0].Rows[0]["FirstName"].ToString();
                        string _LastName = ds_contact_info.Tables[0].Rows[0]["LastName"].ToString();
                        string _EmailId = ds_contact_info.Tables[0].Rows[0]["EmailId"].ToString();
                        _EmailId = _EmailId.Substring(0, 3) + "*******@" + _EmailId.Split('@')[1];
                        string _TimeZone = ds_contact_info.Tables[0].Rows[0]["TimeZone"].ToString();
                        string _Address = ds_contact_info.Tables[0].Rows[0]["Address"].ToString();
                        string _PhoneNo = (ds_contact_info.Tables[0].Rows[0]["Mobile"].ToString() != "" ? (ds_contact_info.Tables[0].Rows[0]["CountryCode"].ToString() + " " + ds_contact_info.Tables[0].Rows[0]["Mobile"].ToString()) : "-");
                        string _CityName = ds_contact_info.Tables[0].Rows[0]["CityName"].ToString();
                        string _State = ds_contact_info.Tables[0].Rows[0]["State"].ToString();
                        string _PinCode = ds_contact_info.Tables[0].Rows[0]["PinCode"].ToString();
                        string _VATID = ds_contact_info.Tables[0].Rows[0]["VATID_View"].ToString();
                        string _CompanyName = ds_contact_info.Tables[0].Rows[0]["CompanyName"].ToString();
                        string _CompanyWebsite = ds_contact_info.Tables[0].Rows[0]["CompanyWebsite"].ToString();
                        string _CompanyTagLine = ds_contact_info.Tables[0].Rows[0]["CompanyTagLine"].ToString();
                        string _CompanyDescription = ds_contact_info.Tables[0].Rows[0]["CompanyDescription"].ToString();
                        string UserType = Session["Trabau_UserType"].ToString();
                        string GoogleMapsLinkRequired = ds_contact_info.Tables[0].Rows[0]["GoogleMapsLinkRequired"].ToString();
                        string GoogleMapsLink = ds_contact_info.Tables[0].Rows[0]["GoogleMapsLink"].ToString();
                        string ProfileType = ds_contact_info.Tables[0].Rows[0]["ProfileType"].ToString();
                        bool DefaultAccount = Convert.ToBoolean(ds_contact_info.Tables[0].Rows[0]["DefaultAccount"].ToString());

                        ltrlGoogleMapsLink.Text = GoogleMapsLink;
                        chkGoogleMapsLink.Checked = (GoogleMapsLinkRequired == "1" ? true : false);
                        div_company_details.Visible = (UserType == "H" ? true : false);
                        divVATID.Visible = (UserType == "H" ? true : false);
                        div_account_picture.Visible = (UserType == "H" ? true : false);
                        divNewAccount.Visible = true;
                        // divNewAccount.Visible = (UserType == "H" ? true : false);
                        ltrlUserType.Text = " - " + (UserType == "H" ? "Client" : "Freelancer");
                        ltrlVATID.Text = _VATID;
                        ltrlUserId.Text = _UserId;
                        ltrlFName.Text = _FirstName;
                        ltrlLName.Text = _LastName;
                        ltrlEmailId.Text = _EmailId;
                        ltrlTimeZone.Text = _TimeZone;
                        ltrlAddress.Text = _Address;
                        ltrlCityName.Text = _CityName;
                        ltrlState.Text = _State;
                        ltrlPostalCode.Text = _PinCode;
                        ltrlPhoneNo.Text = "<a class='trabau-link' href='tel:" + _PhoneNo + "'>" + _PhoneNo + "</a>";
                        ltrlCompanyName.Text = _CompanyName;
                        ltrlCompanyWebsite.Text = "<a class='trabau-link' href='" + _CompanyWebsite + "' target='_blank'>" + _CompanyWebsite + "</a>";
                        ltrlCompanyTagline.Text = _CompanyTagLine;
                        ltrlCompanyDescription.Text = _CompanyDescription;

                        txtCompanyName.Text = _CompanyName;
                        txtCompanyWebsite.Text = _CompanyWebsite;
                        txtCompanyTagline.Text = _CompanyTagLine;
                        txtCompanyDescription.Text = _CompanyDescription;
                        //txtUserId.Text = _UserId;
                        //txtName.Text = _FullName;
                        //txtEmailAddress.Text = _EmailId;



                        if (UserType == "W")
                        {
                            div_profiletype.Visible = true;
                            ltrlProfileType.Text = ProfileType;
                            ddlProfileType.SelectedValue = ProfileType;

                            h2_location_header.InnerText = "Location";
                            h1_Header.InnerText = "Contact Info";
                            this.Page.Title = "Contact Info";
                        }
                        else if (UserType == "H")
                        {
                            h2_location_header.InnerText = "Company Contact";
                            h1_Header.InnerText = "My Info";
                            this.Page.Title = "My Info";
                        }


                        if (div_account_picture.Visible)
                        {
                            string user_pic = ImageProcessing.GetUserPicture(Int64.Parse(UserID), 133, 100);
                            imgProfilePic.ImageUrl = user_pic;
                        }

                        if (div_company_details.Visible)
                        {
                            string company_logo = ImageProcessing.GetCompanyLogo(Int64.Parse(UserID), 133, 100);
                            imgCompanyLogo.ImageUrl = company_logo;
                        }

                        CheckAccounts();

                        lblAccountType.Text = (UserType == "H" ? "Client" : "Freelancer");
                        div_userid.Visible = (Session["Trabau_Primary_UserId"].ToString() == Session["Trabau_UserId"].ToString() ? true : false);
                        //  div_email.Visible = (Session["Trabau_Primary_UserId"].ToString() == Session["Trabau_UserId"].ToString() ? true : false);

                        lblDefaultAccountStatus.Text = (DefaultAccount ? " (Default Account)" : "");
                        btnSetAsDefaultAccount.Visible = !DefaultAccount;
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }


    public void CheckAccounts()
    {
        try
        {
            string Primary_UserID = Session["Trabau_Primary_UserId"].ToString();
            string UserType = Session["Trabau_UserType"].ToString();
            settings_changes obj = new settings_changes();
            DataSet ds = obj.GetUserAccounts(Int64.Parse(Primary_UserID));

            if (ds.Tables[0].Rows.Count >= 2)
            {
                btnNewAccount.Visible = false; ;
            }


        }
        catch (Exception)
        {
        }
    }
    protected void lbtnEdit_ContactInfo_Click(object sender, EventArgs e)
    {
        try
        {
            string Primary_UserID = Session["Trabau_Primary_UserId"].ToString();
            string UserID = Session["Trabau_UserId"].ToString();
            bool IsSecondaryAccount = false;

            if (Primary_UserID != UserID)
            {
                IsSecondaryAccount = true;
            }

            ltrlUserId.Visible = false;
            ltrlUserType.Visible = false;
            txtUserId.Visible = true;
            txtUserId.Text = ltrlUserId.Text;

            ltrlFName.Visible = false;
            txtFName.Visible = true;
            txtFName.Text = ltrlFName.Text;

            ltrlLName.Visible = false;
            txtLName.Visible = true;
            txtLName.Text = ltrlLName.Text;

            ltrlEmailId.Visible = IsSecondaryAccount;
            txtEmailAddress.Visible = !IsSecondaryAccount;
            txtEmailAddress.Text = Session["Trabau_EmailId"].ToString();

            div_save_contact_details.Visible = true;
            lbtnEdit_ContactInfo.Visible = false;
            LocationEditableChange(false);
            CompanyEditChange(false);
        }
        catch (Exception)
        {

        }
    }

    protected void btnSubmitContactDetails_Click(object sender, EventArgs e)
    {
        try
        {
            string _UserId = txtUserId.Text;
            string FName = txtFName.Text;
            string LName = txtLName.Text;
            string EmailAddress = txtEmailAddress.Text;
            if (_UserId != string.Empty)
            {
                if (FName != string.Empty)
                {
                    if (EmailAddress != string.Empty)
                    {
                        string UserID = Session["Trabau_UserId"].ToString();
                        settings_changes obj = new settings_changes();
                        string data = obj.UpdateContactDetails(Int64.Parse(UserID), _UserId, FName, LName, EmailAddress);
                        string response = data.Split(':')[0];
                        string message = data.Split(':')[1];

                        if (response == "success")
                        {
                            Session["Trabau_FirstName"] = FName;
                            Session["Trabau_FullName"] = FName + ' ' + LName;
                            Session["Trabau_EmailId"] = EmailAddress;

                            ChangeNavigation("contact_info");
                        }
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ContactDetails_Update_Message", "setTimeout(function () { Swal.fire({type: '" + response + "',  title: '" + message + "',  showConfirmButton: true,  timer: 1500});}, 200);", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ContactDetails_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ContactDetails_Error_Message", "setTimeout(function () { toastr['error']('Enter Email Address');}, 200);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ContactDetails_Error_Message", "setTimeout(function () { toastr['error']('Enter First Name');}, 200);", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ContactDetails_Error_Message", "setTimeout(function () { toastr['error']('Enter User Id');}, 200);", true);
            }

        }
        catch (Exception)
        {

        }
    }


    protected void lbtnCancelContactDetails_Click(object sender, EventArgs e)
    {
        try
        {
            AccountEditableChange(false);
        }
        catch (Exception)
        {

        }
    }

    #endregion

    //#region Location
    protected void lbtnEditLocation_Click(object sender, EventArgs e)
    {
        try
        {
            AccountEditableChange(false);
            LocationEditableChange(true);
            DataSet ds_contactinfo = new DataSet();
            if (ViewState["User_Contact_Details"] != null)
            {
                ds_contactinfo = ViewState["User_Contact_Details"] as DataSet;
            }

            string TimeZoneId = ds_contactinfo.Tables[0].Rows[0]["TimeZoneId"].ToString();
            string CountryId = ds_contactinfo.Tables[0].Rows[0]["CountryId"].ToString();
            string Address = ds_contactinfo.Tables[0].Rows[0]["Address"].ToString();
            string CityName = ds_contactinfo.Tables[0].Rows[0]["CityName"].ToString();
            string CityId = ds_contactinfo.Tables[0].Rows[0]["CityId"].ToString();
            string PhoneNo = ds_contactinfo.Tables[0].Rows[0]["Mobile"].ToString();
            string CountryCode = ds_contactinfo.Tables[0].Rows[0]["CountryCode"].ToString();
            string VATID = ds_contactinfo.Tables[0].Rows[0]["VATID"].ToString();
            string GoogleMapsLinkRequired = "0";

            chkGoogleMapsLink.Visible = true;
            chkGoogleMapsLink.Text = (GoogleMapsLinkRequired == "1" ? "ON" : "OFF");
            string key = "T_city";
            CityId = EncyptSalt.EncryptText(CityId, key);
            string PinCode = ds_contactinfo.Tables[0].Rows[0]["PinCode"].ToString();

            BindCountryCodes(CountryCode);
            DisplayTimeZones(TimeZoneId);
            DisplayCountry(CountryId);
            txtAddress.Text = Address;
            txtCityName.Text = CityName;
            hfCityId.Value = CityId;
            txtPostalCode.Text = PinCode;
            txtPhoneNo.Text = PhoneNo;
            txtVATID.Text = VATID;

            chkGoogleMapsLink_CheckedChanged(sender, e);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "open_city_autocomplete", "setTimeout(function () {AutoCompleteTextBox('txtCityName','hfCityId','','../../signup/profile-updation.aspx/GetCities_List','::','');}, 300);", true);
        }
        catch (Exception)
        {

        }
    }

    public void DisplayTimeZones(string TimeZoneId)
    {
        try
        {
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();

            DataSet ds_time_zones = obj.GetTimeZones(Int64.Parse(UserID));
            ddlTimeZone.DataSource = ds_time_zones;
            ddlTimeZone.DataTextField = "Text";
            ddlTimeZone.DataValueField = "Value";
            ddlTimeZone.DataBind();

            try
            {
                ddlTimeZone.SelectedValue = TimeZoneId;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "TimeZone_Select", "setTimeout(function () { $('[Id*=ddlTimeZone]').select2();}, 50);", true);
            }
            catch (Exception)
            {
            }
        }
        catch (Exception)
        {
        }
    }


    public void DisplayCountry(string CountryId)
    {
        try
        {
            DLL_Registration obj = new DLL_Registration();

            DataSet ds_country = obj.GetCountryList();
            ddlCountry.DataSource = ds_country;
            ddlCountry.DataTextField = "Text";
            ddlCountry.DataValueField = "Value";
            ddlCountry.DataBind();

            try
            {
                ddlCountry.SelectedValue = CountryId;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Country_Select", "setTimeout(function () { $('[Id*=ddlCountry]').select2();}, 50);", true);
            }
            catch (Exception)
            {
            }
        }
        catch (Exception)
        {
        }
    }

    public void LocationEditableChange(bool Editable)
    {
        chkGoogleMapsLink.Visible = Editable;
        ltrlGoogleMapsLink.Visible = !Editable;

        txtVATID.Visible = Editable;
        ltrlVATID.Visible = !Editable;

        ddlTimeZone.Visible = Editable;
        ltrlTimeZone.Visible = !Editable;

        div_Phone.Visible = Editable;
        div_Phone_View.Visible = !Editable;

        div_Country.Visible = Editable;
        div_address.Visible = Editable;
        div_address_view.Visible = !Editable;
        divCity.Visible = Editable;
        div_location_details.Visible = Editable;
        lbtnEditLocation.Visible = !Editable;
    }


    //#endregion

    protected void btnUpdateLocationDetails_Click(object sender, EventArgs e)
    {
        try
        {
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();
            string TimeZoneId = ddlTimeZone.SelectedValue;
            string CountryId = ddlCountry.SelectedValue;
            string Address = txtAddress.Text;
            string CityId = hfCityId.Value;
            string key = "T_city";
            CityId = EncyptSalt.DecryptText(CityId, key);
            string PostalCode = txtPostalCode.Text;
            string CountryCode = ddlCountryCode.SelectedValue;
            string PhoneNo = txtPhoneNo.Text;
            string VATID = txtVATID.Text;
            bool GoogleMapsLinkRequired = chkGoogleMapsLink.Checked;
            string data = obj.UpdateLocationDetails(Int64.Parse(UserID), Int32.Parse(TimeZoneId), Int32.Parse(CountryId), Address, Int32.Parse(CityId), PostalCode,
                CountryCode, PhoneNo, VATID, GoogleMapsLinkRequired);

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];

            if (response == "success")
            {
                LocationEditableChange(false);
                ChangeNavigation("contact_info");
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "LocationDetails_Update_Message", "setTimeout(function () { Swal.fire({type: '" + response + "',  title: '" + message + "',  showConfirmButton: true,  timer: 1500});}, 200);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LocationDetails_Update_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);

        }
        catch (Exception)
        {

        }
    }

    protected void btnCancelLocationDetails_Click(object sender, EventArgs e)
    {
        try
        {
            LocationEditableChange(false);
        }
        catch (Exception)
        {
        }
    }

    protected void ddlNavigation_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selected_nav = ddlNavigation.SelectedValue;
        ChangeNavigation(selected_nav);
    }

    public void BindCountryCodes(string CountryCode)
    {
        try
        {
            DLL_Registration obj = new DLL_Registration();
            DataSet ds_country = obj.GetCountryList();

            ddlCountryCode.DataSource = ds_country;
            ddlCountryCode.DataTextField = "CountryCode";
            ddlCountryCode.DataValueField = "CountryCode";
            ddlCountryCode.DataBind();

            try
            {
                ddlCountryCode.SelectedValue = CountryCode;
            }
            catch (Exception)
            {
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void lbtnDownloadDocument_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem row = (sender as LinkButton).Parent as RepeaterItem;
            string UploadedDocId = (row.FindControl("lblUploadedDocId") as Label).Text;
            string DocumentExtension = (row.FindControl("lblDocumentExtension") as Label).Text;
            string DocumentName = (row.FindControl("lblDocumentName") as Label).Text;
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds_docs = obj.GetIndvidualUploadedDocument(Int64.Parse(UserID), Int32.Parse(UploadedDocId));

            if (ds_docs != null)
            {
                if (ds_docs.Tables.Count > 0)
                {
                    if (ds_docs.Tables[0].Rows.Count > 0)
                    {
                        byte[] doc_bytes = (byte[])(ds_docs.Tables[0].Rows[0]["Document_bytes"]);


                        Guid g = Guid.NewGuid();
                        string extension = DocumentExtension;
                        string filename = DocumentName.Replace(" ", "_") + "_" + g.ToString() + extension;
                        Response.ClearContent();
                        // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                        // Add the file size into the response header
                        Response.AddHeader("Content-Length", doc_bytes.Length.ToString());
                        // Set the ContentType
                        Response.ContentType = "application/octet-stream";
                        // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
                        Response.BinaryWrite(doc_bytes);
                        Response.Flush();
                        // System.IO.File.Delete(FilePath);
                        // End the response
                        Response.End();
                    }
                }
            }
        }
        catch
        {

        }
    }

    protected void lbtnRemoveDocument_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem row = (sender as LinkButton).Parent as RepeaterItem;
            string UploadedDocId = (row.FindControl("lblUploadedDocId") as Label).Text;
            string UserID = Session["Trabau_UserId"].ToString();

            settings_changes obj = new settings_changes();
            string data = obj.RemoveUploadedDocument(Int64.Parse(UserID), Int32.Parse(UploadedDocId));

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];

            if (response == "success")
            {
                LoadUploadedDocuments();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "RemoveDocument_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void lbtnViewYouTubeVideo_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem row = (sender as LinkButton).Parent as RepeaterItem;
            string YouTubeVideoURL = (row.FindControl("lblOtherDocType") as Label).Text;

            divTrabau_DocumentView.Visible = true;
            div_doc_data.InnerHtml = "<iframe height='480' src='" + YouTubeVideoURL + "' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open_DocumentView_Popup", "setTimeout(function () {HandlePopUp('1','divTrabau_DocumentView');}, 150);", true);

        }
        catch (Exception)
        {
        }
    }

    public void ClearUploadControls()
    {
        div_other_document.Visible = false;
        rfvOtherDocType.Enabled = false;
        div_youtube_video.Visible = false;
        rfvYoutubeVideoName.Enabled = false;
        btnUploadFile.Visible = true;
        rfvDocumentFile.Enabled = true;
        txtOtherDocumentType.Text = string.Empty;
        txtYouTubeVideoName.Text = string.Empty;
        pYoutubeVideoExample.Visible = false;
    }

    protected void ddlDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearUploadControls();

            string DocType = ddlDocumentType.SelectedValue;
            string DocType_Name = ddlDocumentType.SelectedItem.Text;
            if (DocType_Name == "Other")
            {
                div_other_document.Visible = true;
                rfvOtherDocType.Enabled = true;
                txtOtherDocumentType.Attributes.Add("placeholder", "Enter Other Document Name");
                ltrlOtherDocLabel.Text = "Enter Other Document Name";
                rfvOtherDocType.ErrorMessage = "Enter Other Document Name";
            }
            else if (DocType_Name == "Youtube Video")
            {
                div_other_document.Visible = true;
                rfvOtherDocType.Enabled = true;
                div_youtube_video.Visible = true;
                rfvYoutubeVideoName.Enabled = true;

                txtOtherDocumentType.Attributes.Add("placeholder", "Enter Youtube Video Embed URL");
                ltrlOtherDocLabel.Text = "Enter Youtube Video Embed URL";
                rfvOtherDocType.ErrorMessage = "Enter Youtube Video Embed URL";
                btnUploadFile.Visible = false;
                rfvDocumentFile.Enabled = false;
                pYoutubeVideoExample.Visible = true;
            }
        }
        catch (Exception)
        {
        }
    }

    protected void imgProfilePic_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            wucProfilePicture1.OpenProfilePicture_Popup();
        }
        catch (Exception)
        {
        }
    }

    public void CompanyEditChange(bool required)
    {
        company_logo.Visible = !required;
        ltrlCompanyName.Visible = !required;
        ltrlCompanyWebsite.Visible = !required;
        ltrlCompanyTagline.Visible = !required;
        ltrlCompanyDescription.Visible = !required;

        txtCompanyName.Visible = required;
        txtCompanyWebsite.Visible = required;
        txtCompanyTagline.Visible = required;
        txtCompanyDescription.Visible = required;
        div_update_company_details.Visible = required;
    }

    protected void lbtnEditCompanyDetails_Click(object sender, EventArgs e)
    {
        try
        {
            CompanyEditChange(true);
        }
        catch (Exception)
        {

        }
    }

    protected void imgCompanyLogo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            wucCompanyLogo1.OpenCompanyLogo_Popup();
        }
        catch (Exception)
        {
        }
    }

    protected void btnUpdateCompanyDetails_Click(object sender, EventArgs e)
    {
        try
        {
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();
            string CompanyName = txtCompanyName.Text;
            string Website = txtCompanyWebsite.Text;
            string Tagline = txtCompanyTagline.Text;
            string Description = txtCompanyDescription.Text;

            string data = obj.UpdateCompanyDetails(Int64.Parse(UserID), CompanyName, Website, Tagline, Description);

            string _response = data.Split(':')[0];
            string _message = data.Split(':')[1];
            if (_response == "success")
            {
                ltrlCompanyName.Text = CompanyName;
                ltrlCompanyWebsite.Text = "<a class='trabau-link' href='" + Website + "' target='_blank'>" + Website + "</a>";
                ltrlCompanyTagline.Text = Tagline;
                ltrlCompanyDescription.Text = Description;
                CompanyEditChange(false);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CompanyDetails_Update_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);
        }
        catch (Exception)
        {

        }
    }

    protected void lbtnCancelCompanyDetails_Click(object sender, EventArgs e)
    {
        try
        {
            CompanyEditChange(false);
        }
        catch (Exception)
        {

        }
    }

    protected void btnNewAccount_Click(object sender, EventArgs e)
    {
        try
        {
            string Primary_UserID = Session["Trabau_Primary_UserId"].ToString();
            settings_changes obj = new settings_changes();
            DataSet ds = obj.GetUserAccounts(Int64.Parse(Primary_UserID));

            if (ds.Tables[0].Rows.Count >= 2)
            {
                btnNewAccount.Visible = false;
            }
            else
            {
                string UserType = Session["Trabau_UserType"].ToString();
                divNewClientAccount.Visible = (UserType == "H" ? false : true);
                divNewFreelancerAccount.Visible = (UserType == "W" ? false : true);
                div_newaccount_popup.Visible = true;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "NewAccount_Popup", "setTimeout(function () {HandlePopUp('1','div_newaccount_popup');}, 500);", true);
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnCloseNewAccount_popup_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseNewAccount_Popup", "setTimeout(function () {HandlePopUp('0','div_newaccount_popup');}, 0);", true);
            div_newaccount_popup.Visible = false;
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnNewFreelancerAccount_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseNewAccount_Popup", "setTimeout(function () {HandlePopUp('0','div_newaccount_popup');}, 0);", true);
            div_newaccount_popup.Visible = false;

            ChangeNavigation("new_freelancer_account");


        }
        catch (Exception)
        {

        }
    }


    protected void lbtnNewClientAccount_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseNewAccount_Popup", "setTimeout(function () {HandlePopUp('0','div_newaccount_popup');}, 0);", true);
            div_newaccount_popup.Visible = false;

            ChangeNavigation("new_client_account");


        }
        catch (Exception)
        {

        }
    }

    protected void btnCreateNewAccount_Click(object sender, EventArgs e)
    {
        try
        {
            settings_changes obj = new settings_changes();
            string Primary_UserID = Session["Trabau_Primary_UserId"].ToString();
            string UserType = Session["Trabau_UserType"].ToString();
            string New_UserType = (UserType == "W" ? "H" : "W");
            DataSet ds = obj.CreateNewSecondaryAccount(Int64.Parse(Primary_UserID), New_UserType, Request.UserHostAddress);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                        if (Int64.Parse(UserId) > 0)
                        {
                            string Message = ds.Tables[0].Rows[0]["Message"].ToString();

                            Session["Trabau_UserId"] = UserId;
                            Session["Trabau_UserType"] = New_UserType;

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "NewAccount_Message", "setTimeout(function () { toastr['success']('" + Message + "');}, 200);", true);

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "SwitchAccounts", "setTimeout(function () { window.location.href=window.location.href;}, 300);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "NewAccount_Message", "setTimeout(function () { toastr['error']('Error while creating new Freelancer account, please refresh and try again');}, 200);", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "NewAccount_Message", "setTimeout(function () { toastr['error']('Error while creating new Freelancer account, please refresh and try again');}, 200);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NewAccount_Message", "setTimeout(function () { toastr['error']('Error while creating new Freelancer account, please refresh and try again');}, 200);", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NewAccount_Message", "setTimeout(function () { toastr['error']('Error while creating new Freelancer account, please refresh and try again');}, 200);", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "NewAccount_Message", "setTimeout(function () { toastr['error']('Error while creating new Freelancer account, please refresh and try again');}, 200);", true);
        }
    }

    protected void chkGoogleMapsLink_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkGoogleMapsLink.Text = chkGoogleMapsLink.Checked ? "ON" : "OFF";
            ltrlGoogleMapsLink.Visible = chkGoogleMapsLink.Checked;
            DataSet ds_contact_info = ViewState["User_Contact_Details"] as DataSet;

            string _CityName = ds_contact_info.Tables[0].Rows[0]["CityName"].ToString();
            string _State = ds_contact_info.Tables[0].Rows[0]["State"].ToString();
            string _PinCode = ds_contact_info.Tables[0].Rows[0]["PinCode"].ToString();
            string _Address = ds_contact_info.Tables[0].Rows[0]["Address"].ToString();
            ltrlGoogleMapsLink.Text = "<i class='fa fa-map-marker' aria-hidden='true'></i><a class='gmaps-link' target='_blank' href='https://www.google.com/maps/place/"
                + _Address + "+" + _CityName + "+" + _State + "'>" + _CityName + " ," + _State + " ," + _PinCode + "</a>";
        }
        catch (Exception)
        {
        }
    }


    protected void lbtnEditProfileType_Click(object sender, EventArgs e)
    {
        try
        {
            ProfileTypeChange(true);
        }
        catch (Exception)
        {

        }
    }

    public void ProfileTypeChange(bool val)
    {
        try
        {
            ddlProfileType.Visible = val;
            div_update_profiletype.Visible = val;

            ltrlProfileType.Visible = !val;
            lbtnEditProfileType.Visible = !val;
        }
        catch (Exception)
        {
        }
    }

    protected void btnUpdateProfileType_Click(object sender, EventArgs e)
    {
        try
        {
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();
            string ProfileType = ddlProfileType.SelectedValue;

            string data = obj.UpdateProfileType(Int64.Parse(UserID), ProfileType);

            string _response = data.Split(':')[0];
            string _message = data.Split(':')[1];
            if (_response == "success")
            {
                string OldProfileType = ltrlProfileType.Text;
                ltrlProfileType.Text = ProfileType;
                ProfileTypeChange(false);

                try
                {
                    Repeater rAccounts = this.Page.Master.FindControl("rAccounts") as Repeater;
                    foreach (RepeaterItem item in rAccounts.Items)
                    {
                        string Acc_UserId = (item.FindControl("lblUserId") as Label).Text;
                        if (Acc_UserId == UserID)
                        {
                            HtmlGenericControl divUserType = (item.FindControl("divUserType") as HtmlGenericControl);
                            divUserType.InnerHtml = ddlProfileType.SelectedItem.Text;

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ProfileType_Update", "setTimeout(function () { $('#" + divUserType.ClientID + "').html('" + ddlProfileType.SelectedItem.Text + "');}, 200);", true);
                            break;
                        }
                    }

                }
                catch (Exception)
                {
                }


                try
                {
                    if (OldProfileType != ProfileType)
                    {
                        string Name = Session["Trabau_FirstName"].ToString();
                        string EmailId = Session["Trabau_EmailId"].ToString();
                        string template_url = "https://www.trabau.com/emailers/xxddcca/profile-type-update.html";

                        try
                        {
                            WebRequest req = WebRequest.Create(template_url);
                            WebResponse w_res = req.GetResponse();
                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                            string html = sr.ReadToEnd();

                            html = html.Replace("@Name", Name);
                            html = html.Replace("@actiontype", OldProfileType + " to " + ProfileType);

                            string body = html;

                            Emailer obj_email = new Emailer();
                            string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Profile Change – updated from " + OldProfileType + " to " + ProfileType, body, null);

                            try
                            {
                                utility log = new utility();
                                log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, _val, HttpContext.Current.Request.UserHostAddress);
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
                                log.CreateEmailerLog(Convert.ToInt64(UserID), EmailId, template_url, ex.Message, HttpContext.Current.Request.UserHostAddress);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ProfileType_Update_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);
        }
        catch (Exception)
        {

        }
    }

    protected void lbtnCancelProfileType_Click(object sender, EventArgs e)
    {
        try
        {
            ProfileTypeChange(false);
        }
        catch (Exception)
        {

        }
    }

    protected void btnSetAsDefaultAccount_Click(object sender, EventArgs e)
    {
        try
        {
            settings_changes obj = new settings_changes();
            string UserID = Session["Trabau_UserId"].ToString();

            string data = obj.SetAsDefaultAccount(Int64.Parse(UserID));

            string _response = data.Split(':')[0];
            string _message = data.Split(':')[1];
            if (_response == "success")
            {
                lblDefaultAccountStatus.Text = " (Default Account)";
                btnSetAsDefaultAccount.Visible = false;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "DefaultAccount_Update_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);
        }
        catch (Exception)
        {
        }
    }
}