using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Projects;

public partial class projects_UserControls_wucNewProject : System.Web.UI.UserControl
{
    public static bool CanOpen_EditProject = false;
    public static bool AllowEdit = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["NewProject_Files"] = null;
                Session["Trabau_ProjectDetails"] = null;
                ltrlPageNo.Text = "1";
                div_step0.Visible = true;
                div_step1.Visible = false;
                div_step2.Visible = false;
                ltrlHeader.Text = "Project Type";
                btnExit.Visible = false;
                txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtEndDate.Text = DateTime.Now.AddDays(30).ToString("MM/dd/yyyy");
                txtManagerName.Text = Session["Trabau_FullName"].ToString();

                if (Request.RawUrl.Contains("projects/edit-project.aspx"))
                {
                    try
                    {
                        if (Request.QueryString.Count == 1)
                        {
                            if (Request.QueryString["projectid"] != null)
                            {
                                string projectid = Request.QueryString["projectid"];
                                if (projectid != string.Empty)
                                {
                                    projectid = MiscFunctions.Base64DecodingMethod(projectid);
                                    projectid = EncyptSalt.DecryptText(projectid, Trabau_Keys.Project_Key);

                                    AllowEdit = projectid.Split('~')[1] == "true" ? true : false;
                                    projectid = projectid.Split('~')[0];

                                    Page.Title = "Edit Project - Trabau";
                                    CanOpen_EditProject = true;

                                    GetProjectDetails(Int32.Parse(projectid), sender, e);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        RedirectToDefaultPage();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "FixTextboxClasses", "setTimeout(function () {FixTextboxClasses();}, 0);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();;}, 0);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlCompany_open_list", "setTimeout(function () {$('select[id*=" + ddlCompany.ID + "]').select2();}, 0);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlCommunicationFunction_open_list", "setTimeout(function () {$('select[id*=" + ddlCommunicationFunction.ID + "]').select2();}, 0);", true);

        if (!AllowEdit && CanOpen_EditProject)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "disable_description", "setTimeout(function () {$('.ql-editor').attr('contenteditable',false);$('.ql-editor').addClass('disabled');$('.dx-htmleditor-toolbar-wrapper').remove();$('.file-upload').remove();}, 100);", true);
        }
    }


    public void RedirectToDefaultPage()
    {
        string _url = "";
        string UserType = Session["Trabau_UserType"].ToString();
        if (UserType == "H")
        {

            _url = "http://" + Request.Url.Authority + "/jobs/posting/postedjobs.aspx";
        }
        else
        {
            _url = "http://" + Request.Url.Authority + "/jobs/searchjobs/index.aspx";
        }

        Response.Redirect(_url);
    }

    public void GetProjectDetails(int ProjectID, object sender, EventArgs e)
    {
        try
        {
            Project obj = new Project();
            string UserID = Session["Trabau_UserId"].ToString();

            var ds = obj.GetProjectDetails(Int64.Parse(UserID), ProjectID);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["Trabau_ProjectDetails"] = ds;
                        string ProjectType = ds.Tables[0].Rows[0]["ProjectType"].ToString();
                        rbtnlProjectType.SelectedValue = ProjectType;
                        rbtnlProjectType.Enabled = AllowEdit;
                        rbtnlOtherProject_Step1.Enabled = AllowEdit;
                        rbtnlOtherProject_Step2.Enabled = AllowEdit;

                        if (!AllowEdit && CanOpen_EditProject)
                        {
                            this.Page.Title = "Project Information (Read Only) - Trabau";
                        }

                        DisplayStep2(ProjectType, sender, e);
                    }
                    else
                    {
                        RedirectToDefaultPage();
                    }
                }
                else
                {
                    RedirectToDefaultPage();
                }
            }
            else
            {
                RedirectToDefaultPage();
            }
        }
        catch (Exception)
        {
        }
    }

    public void GetCompanyList()
    {
        try
        {
            Project obj = new Project();
            string UserID = Session["Trabau_UserId"].ToString();

            var lst = obj.GetCompanyList(Int64.Parse(UserID));
            ddlCompany.DataSource = (lst.Select(x => new { x.Text, x.Value })).ToList();
            ddlCompany.DataTextField = "Text";
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataBind();

        }
        catch (Exception)
        {

        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            string selected = rbtnlProjectType.SelectedValue;
            if (div_step0.Visible)
            {

                if (selected == "O")
                {
                    if (div_min_step2.Visible)
                    {
                        div_min_step1.Visible = true;
                        div_min_step2.Visible = false;
                        div_min_step3.Visible = false;
                        //div_min_step4.Visible = false;
                        btnExit.Visible = false;
                    }
                    else if (div_min_step3.Visible)
                    {
                        div_min_step1.Visible = false;
                        div_min_step2.Visible = true;
                        div_min_step3.Visible = false;
                        //div_min_step4.Visible = false;
                        btnExit.Visible = true;
                    }
                    //else if (div_min_step4.Visible)
                    //{
                    //    div_min_step1.Visible = false;
                    //    div_min_step2.Visible = false;
                    //    div_min_step3.Visible = true;
                    //    div_min_step4.Visible = false;
                    //    btnExit.Visible = true;
                    //}
                    btnNext.Text = "Next";
                }
            }
            else if (div_step1.Visible)
            {
                div_step0.Visible = true;
                div_step1.Visible = false;
                div_step2.Visible = false;
                btnExit.Visible = false;
                btnNext.Text = "Next";

                ltrlPageNo.Text = "1";
                ltrlHeader.Text = "Project Type";

                li_step0.Attributes.Add("class", "process");
                li_step1.Attributes.Add("class", "");
                li_step2.Attributes.Add("class", "");

                if (selected == "O")
                {
                    div_min_step1.Visible = false;
                    div_min_step2.Visible = false;
                    div_min_step3.Visible = true;
                    //div_min_step4.Visible = true;
                    btnExit.Visible = true;
                    btnNext.Text = "Next";

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "rbtnl_open_list", "setTimeout(function () {$('select[id*=" + rbtnlOtherProject_Step3.ID + "]').select2();}, 0);", true);
                }
            }
            else if (div_step2.Visible)
            {
                div_step0.Visible = false;
                div_step1.Visible = true;
                div_step2.Visible = false;
                btnExit.Visible = true;
                btnNext.Text = "Next";
                btnNext.Visible = true;

                ltrlPageNo.Text = "2";
                ltrlHeader.Text = "About the project";

                li_step0.Attributes.Add("class", "active");
                li_step1.Attributes.Add("class", "process");
                li_step2.Attributes.Add("class", "");

                if (selected != "O")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup_div_other_comm_func", "setTimeout(function () { ActivateOtherCommFunction_Default(); }, 100);", true);
                }

                if (CanOpen_EditProject)
                {
                    btnExit.Visible = false;
                }
                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlCompany_open_list", "setTimeout(function () {$('select[id*=" + ddlCompany.ID + "]').select2();}, 0);", true);
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlCommunicationFunction_open_list", "setTimeout(function () {$('select[id*=" + ddlCommunicationFunction.ID + "]').select2();}, 0);", true);
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (div_step0.Visible)
            {
                string selected = rbtnlProjectType.SelectedValue;
                if (selected == "O")
                {
                    if (div_min_step1.Visible)
                    {
                        try
                        {

                            List<dynamic> lst = GetProjectFunctions();

                            int OtherProjectItemID = lst.Where(x => x.ParentItemID == 0).FirstOrDefault().ItemID;

                            BindProjectFunctionItems<RadioButtonList>(rbtnlOtherProject_Step1, OtherProjectItemID, lst);

                            div_min_step1.Visible = false;
                            div_min_step2.Visible = true;
                            div_min_step3.Visible = false;
                            //div_min_step4.Visible = false;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (div_min_step2.Visible)
                    {
                        try
                        {
                            int ParentItemID = Int32.Parse(rbtnlOtherProject_Step1.SelectedValue);

                            BindProjectFunctionItems<RadioButtonList>(rbtnlOtherProject_Step2, ParentItemID);

                            div_min_step1.Visible = false;
                            div_min_step2.Visible = false;
                            div_min_step3.Visible = true;
                            //div_min_step4.Visible = false;

                            rfvOtherProject_Step3.ErrorMessage = "Select " + rbtnlOtherProject_Step1.SelectedItem.Text + " Type";
                            spn_otherproject_step2.InnerHtml = rbtnlOtherProject_Step1.SelectedItem.Text + " Type";
                        }
                        catch (Exception)
                        {
                        }
                    }
                    //else if (div_min_step3.Visible)
                    //{
                    //    try
                    //    {
                    //        int ParentItemID = Int32.Parse(rbtnlOtherProject_Step2.SelectedValue);

                    //        BindProjectFunctionItems<DropDownList>(rbtnlOtherProject_Step3, ParentItemID);

                    //        div_min_step1.Visible = false;
                    //        div_min_step2.Visible = false;
                    //        div_min_step3.Visible = false;
                    //        div_min_step4.Visible = true;

                    //        rfvOtherProject_Step4.ErrorMessage = "Select " + rbtnlOtherProject_Step2.SelectedItem.Text + " Type";
                    //        spn_otherproject_step3.InnerHtml = rbtnlOtherProject_Step2.SelectedItem.Text + " Type";
                    //    }
                    //    catch (Exception)
                    //    {
                    //    }
                    //}
                    else
                    {
                        DisplayStep2(selected, sender, e);
                    }
                }
                else
                {
                    DisplayStep2(selected, sender, e);
                }


                btnExit.Text = "Back";
                btnNext.Text = "Next";
                btnExit.Visible = true;
            }
            else if (div_step1.Visible)
            {
                div_step0.Visible = false;
                div_step1.Visible = false;
                div_step2.Visible = true;
                btnExit.Text = "Back";
                btnNext.Text = "Save Project";
                btnExit.Visible = true;
                if (Request.RawUrl.Contains("edit-project.aspx"))
                {
                    btnNext.Visible = AllowEdit;
                }
                ltrlPageNo.Text = "3";
                ltrlHeader.Text = "Project Schedule & Budget";

                li_step0.Attributes.Add("class", "active");
                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "process");


                if (Session["Trabau_ProjectDetails"] != null && CanOpen_EditProject && txtEndTime.Text == string.Empty)
                {
                    var edit_ds = Session["Trabau_ProjectDetails"] as DataSet;
                    string EndTime = edit_ds.Tables[0].Rows[0]["EndTime"].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "set_end_time", "setTimeout(function () {setTime('txtEndTime','" + EndTime + "');}, 100);", true);
                }
            }
            else if (div_step2.Visible)
            {
                SaveNewProject();
            }
        }
        catch (Exception)
        {
        }
    }

    private void DisplayStep2(string ProjectType, object sender, EventArgs e)
    {
        div_step0.Visible = false;
        div_step1.Visible = true;
        div_step2.Visible = false;


        ltrlPageNo.Text = "2";
        ltrlHeader.Text = "About the project";

        li_step0.Attributes.Add("class", "active");
        li_step1.Attributes.Add("class", "process");
        li_step2.Attributes.Add("class", "");

        if (ProjectType == "O")
        {
            div_comm_function.Visible = true;
            int ParentItemID = Int32.Parse(rbtnlOtherProject_Step2.SelectedValue);
            BindProjectFunctionItems<DropDownList>(ddlCommunicationFunction, ParentItemID);
        }
        else
        {
            ddlCommunicationFunction.Items.Clear();
            div_comm_function.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup_div_other_comm_func", "setTimeout(function () {ActivateOtherCommFunction_Default();}, 100);", true);
            //ddlCommunicationFunction.Items.Add(new ListItem { Text = "Other Communication Function", Value = "Other" });
        }
        GetCompanyList();


        if (Session["Trabau_ProjectDetails"] != null && CanOpen_EditProject)
        {

            try
            {
                var edit_ds = Session["Trabau_ProjectDetails"] as DataSet;

                string ProjectName = edit_ds.Tables[0].Rows[0]["ProjectName"].ToString();
                string CompanyName = edit_ds.Tables[0].Rows[0]["CompanyName"].ToString();
                string ApplicationName = edit_ds.Tables[0].Rows[0]["ApplicationName"].ToString();
                string ProjectCommFunction = edit_ds.Tables[0].Rows[0]["ProjectCommFunction"].ToString();
                string OtherCommFunction = edit_ds.Tables[0].Rows[0]["OtherCommFunction"].ToString();
                string ProjectDescription = edit_ds.Tables[0].Rows[0]["ProjectDescription"].ToString();

                DateTime dt_StartDate = Convert.ToDateTime(edit_ds.Tables[0].Rows[0]["StartDate"].ToString());
                DateTime dt_EndDate = Convert.ToDateTime(edit_ds.Tables[0].Rows[0]["EndDate"].ToString());
                string StartTime = edit_ds.Tables[0].Rows[0]["StartTime"].ToString();
                string EndTime = edit_ds.Tables[0].Rows[0]["EndTime"].ToString();
                string BudgetType = edit_ds.Tables[0].Rows[0]["BudgetType"].ToString();
                string TotalHours = edit_ds.Tables[0].Rows[0]["TotalHours"].ToString();
                string AdditionalInformation = edit_ds.Tables[0].Rows[0]["AdditionalInformation"].ToString();

                txtProjectName.Text = ProjectName;
                ddlCompany.SelectedValue = CompanyName;
                txtApplicationName.Text = ApplicationName;
                ddlCommunicationFunction.SelectedValue = ProjectCommFunction;
                txtOtherCommFunction.Text = OtherCommFunction;
                hfProjectDescription.Value = ProjectDescription;
                txtDescription.Text = ProjectDescription;
                txtStartDate.Text = dt_StartDate.ToShortDateString();
                txtStartDate.Enabled = dt_StartDate.Date < DateTime.Now.Date ? false : true;
                txtEndDate.Text = dt_EndDate.ToShortDateString();
                txtEndDate.Enabled = dt_EndDate.Date < DateTime.Now.Date ? false : true;
                hfStartTime.Value = StartTime;
                // txtEndTime.Text = EndTime;
                ddlBudgetType.SelectedValue = BudgetType;
                ddlBudgetType_SelectedIndexChanged(sender, e);
                if (BudgetType == "H")
                {
                    txtTotalHours.Text = TotalHours;
                }
                txtAdditionalInfo.Text = AdditionalInformation;

                DataTable dtfiles_db = edit_ds.Tables[1];
                DataTable dtfiles = GetProjectFilesStructure();
                for (int i = 0; i < dtfiles_db.Rows.Count; i++)
                {
                    string file_name = dtfiles_db.Rows[i]["file_name"].ToString();
                    byte[] file_bytes = dtfiles_db.Rows[i]["FileBytes"] as byte[];
                    string file_key = dtfiles_db.Rows[i]["file_key"].ToString();

                    AddProjectFilesItem(dtfiles, file_name, file_bytes, file_key);
                }
                Session["NewProject_Files"] = dtfiles;

                rProjectFiles.DataSource = dtfiles;
                rProjectFiles.DataBind();


            }
            catch (Exception)
            {
            }

            txtProjectName.Enabled = AllowEdit;
            ddlCompany.Enabled = AllowEdit;
            txtApplicationName.Enabled = AllowEdit;
            ddlCommunicationFunction.Enabled = AllowEdit;
            txtOtherCommFunction.Enabled = AllowEdit;
            //txtStartDate.Enabled = AllowEdit;
            txtEndDate.Enabled = AllowEdit;
            //txtStartTime.Enabled = AllowEdit;
            txtEndTime.Enabled = AllowEdit;
            ddlBudgetType.Enabled = AllowEdit;
            txtTotalHours.Enabled = AllowEdit;
            txtAdditionalInfo.Enabled = AllowEdit;
            rProjectFiles.Columns[rProjectFiles.Columns.Count - 1].Visible = AllowEdit;
        }

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlCompany_open_list", "setTimeout(function () {$('select[id*=" + ddlCompany.ID + "]').select2();}, 0);", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlCommunicationFunction_open_list", "setTimeout(function () {$('select[id*=" + ddlCommunicationFunction.ID + "]').select2();}, 0);", true);
    }

    public void SaveNewProject()
    {
        try
        {

            string ProjectName = txtProjectName.Text;

            DateTime StartDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
            string Status = "Ongoing";
            string IPAddress = "";
            string CompanyName = ddlCompany.SelectedValue;
            string ApplicationName = txtApplicationName.Text;
            string ManagerName = txtManagerName.Text;
            string ProjectDescription = hfProjectDescription.Value; //txtDescription.Text;
            DateTime StartTime = Convert.ToDateTime(txtStartTime.Text);
            DateTime EndTime = Convert.ToDateTime(txtEndTime.Text);
            string XML_ProjectFiles = GetProjectFiles();
            string ProjectType = rbtnlProjectType.SelectedValue;
            string OtherProjectType = (ProjectType == "O" ? rbtnlOtherProject_Step1.SelectedValue : "");
            string OtherProjectType_Child1 = (ProjectType == "O" ? rbtnlOtherProject_Step2.SelectedValue : "");
            string ProjectCommFunction = (ProjectType == "O" ? ddlCommunicationFunction.SelectedValue : "");
            string OtherCommFunction = txtOtherCommFunction.Text;

            DateTime time_from = DateTime.Parse(StartDate.ToShortDateString() + " " + StartTime.ToShortTimeString());
            DateTime time_to = DateTime.Parse(EndDate.ToShortDateString() + " " + EndTime.ToShortTimeString());


            string _response = "error";
            string _message = "Error while saving new project, please refresh and try again";

            int ProjectID = 0;
            try
            {
                if (Request.RawUrl.Contains("projects/edit-project.aspx"))
                {
                    if (Request.QueryString.Count == 1)
                    {
                        if (Request.QueryString["projectid"] != null)
                        {
                            string projectid = Request.QueryString["projectid"];
                            if (projectid != string.Empty)
                            {
                                projectid = MiscFunctions.Base64DecodingMethod(projectid);
                                projectid = EncyptSalt.DecryptText(projectid, Trabau_Keys.Project_Key);

                                ProjectID = Int32.Parse(projectid.Split('~')[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            if (time_to.TimeOfDay > time_from.TimeOfDay && EndDate.Date > StartDate.Date)
            {
                if (StartDate.Date >= DateTime.Now.Date || ProjectID > 0)
                {
                    string BudgetType = ddlBudgetType.SelectedValue;
                    int TotalHours = Int32.Parse(txtTotalHours.Text == string.Empty ? "0" : txtTotalHours.Text);
                    string AdditionalInformation = txtAdditionalInfo.Text;
                    string UserID = Session["Trabau_UserId"].ToString();


                    Project obj = new Project();
                    var response = obj.SaveNewProject(ProjectID, Int64.Parse(UserID), ProjectName, StartDate, EndDate, Status, IPAddress, CompanyName, ApplicationName, ManagerName, ProjectDescription,
                        StartTime, EndTime, BudgetType, TotalHours, AdditionalInformation, XML_ProjectFiles, ProjectType, OtherProjectType, OtherProjectType_Child1, ProjectCommFunction, OtherCommFunction);


                    if (response != null)
                    {
                        if (response.Count > 0)
                        {
                            _response = response[0].Response;
                            _message = response[0].Message;

                        }
                    }

                    if (_response == "success")
                    {
                        Clear();

                        string return_url = "index.aspx";
                        try
                        {
                            if (Session["Project_ReturnURL"] != null)
                            {
                                return_url = Session["Project_ReturnURL"].ToString();
                            }
                        }
                        catch (Exception)
                        {
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "NewProject_Redirection", "setTimeout(function () { window.location.href='" + return_url + "';}, 2000);", true);
                    }
                }
                else
                {
                    _response = "error";
                    _message = "Start Date should be greater than Today date, please check and try again";
                }
            }
            else
            {
                _response = "error";
                _message = "End Date and Time should be greater than Start Date and Time, please check and try again";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "NewProject_Message", "setTimeout(function () { toastr['" + _response + "']('" + _message + "');}, 200);", true);

        }
        catch (Exception ex)
        {
        }

    }
    public void Clear()
    {
        txtProjectName.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ddlCommunicationFunction.Items.Clear();
        ddlCompany.Items.Clear();
        txtApplicationName.Text = string.Empty;
        hfProjectDescription.Value = string.Empty;
        txtStartTime.Text = string.Empty;
        txtEndTime.Text = string.Empty;
        ddlBudgetType.SelectedIndex = 0;
        txtTotalHours.Text = string.Empty;
        txtAdditionalInfo.Text = string.Empty;
        txtOtherCommFunction.Text = string.Empty;

        div_step1.Visible = true;
        div_step2.Visible = false;
        btnExit.Visible = false;
        btnNext.Text = "Next";

        ltrlPageNo.Text = "1";

        li_step0.Attributes.Add("class", "process");
        li_step1.Attributes.Add("class", "");
        li_step2.Attributes.Add("class", "");
    }


    protected void chkWarningInlineConfig_CheckedChanged(object sender, EventArgs e)
    {
        if (chkWarningInlineConfig.Checked)
        {

            //   vsPostJobValidation_Summary.ShowMessageBox = false;
            //   vsPostJobValidation_Summary.ShowSummary = true;

            rfvProjectName.Text = "";
            rfvCompanyName.Text = "";
            rfvApplicationName.Text = "";
            rfvCommunicationFunction.Text = "";
            rfvManagerName.Text = "";
            rfvDescription.Text = "";
            rfvStartDate.Text = "";
            rfvEndDate.Text = "";
            rfvStartTime.Text = "";
            rfvEndTime.Text = "";
            efvBudgetType.Text = "";
            rfvHours.Text = "";
            rfvOtherCommFunction.Text = "";
            //rfvAdditionalInfo.Text = "";
        }
        else
        {

            //  vsPostJobValidation_Summary.ShowMessageBox = true;
            // vsPostJobValidation_Summary.ShowSummary = false;

            rfvProjectName.Text = "*";
            rfvCompanyName.Text = "*";
            rfvApplicationName.Text = "*";
            rfvCommunicationFunction.Text = "*";
            rfvManagerName.Text = "*";
            rfvDescription.Text = "*";
            rfvStartDate.Text = "*";
            rfvEndDate.Text = "*";
            rfvStartTime.Text = "*";
            rfvEndTime.Text = "*";
            efvBudgetType.Text = "*";
            rfvHours.Text = "*";
            rfvOtherCommFunction.Text = "*";
            //rfvAdditionalInfo.Text = "*";
        }

        string selected = rbtnlProjectType.SelectedValue;
        if (selected != "O")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup_div_other_comm_func", "setTimeout(function () { ActivateOtherCommFunction_Default(); }, 0);", true);
        }
    }

    protected void ddlBudgetType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string BudgetType = ddlBudgetType.SelectedValue;

            div_hourly.Visible = (BudgetType == "H" ? true : false);
        }
        catch (Exception)
        {

        }
    }

    protected void btnAddProjectFiles_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt_files = Session["NewProject_Files"] as DataTable;
            rProjectFiles.DataSource = dt_files;
            rProjectFiles.DataBind();
        }
        catch (Exception)
        {
        }
    }

    public string GetProjectFiles()
    {
        string XMLData = "";
        try
        {
            if (Session["NewProject_Files"] != null)
            {
                DataTable dt_files = Session["NewProject_Files"] as DataTable;

                XMLData = MiscFunctions.GetXMLString(dt_files, "ProjectFiles");
            }
        }
        catch (Exception)
        {
        }

        return XMLData;
    }

    protected void afuProjectFiles_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        try
        {
            if (afuProjectFiles.HasFile)
            {
                DataTable dt_files = new DataTable();
                if (Session["NewProject_Files"] != null)
                {
                    dt_files = Session["NewProject_Files"] as DataTable;
                }
                else
                {
                    dt_files = GetProjectFilesStructure();
                }
                byte[] attachment = afuProjectFiles.FileBytes;

                dt_files = AddProjectFilesItem(dt_files, afuProjectFiles.FileName, attachment);
                Session["NewProject_Files"] = dt_files;

            }
        }
        catch (Exception)
        {
        }
    }

    public DataTable GetProjectFilesStructure()
    {
        DataTable dt_files = new DataTable();
        dt_files.Columns.Add("file_key", typeof(string));
        dt_files.Columns.Add("file_name", typeof(string));
        dt_files.Columns.Add("file_bytes", typeof(string));
        dt_files.Columns.Add("file_size", typeof(string));
        return dt_files;
    }

    public DataTable AddProjectFilesItem(DataTable dt_files, string file_name, byte[] file_bytes, string _filekey = "")
    {
        try
        {
            DataRow dr = dt_files.NewRow();
            dr["file_key"] = (_filekey == "" ? MiscFunctions.RandomString(20).ToLower() : _filekey);
            dr["file_name"] = file_name;
            string base64_bytes = Convert.ToBase64String(file_bytes);
            dr["file_bytes"] = base64_bytes;
            dr["file_size"] = (file_bytes.Length / 1024).ToString() + " KB";
            dt_files.Rows.Add(dr);
        }
        catch (Exception ex)
        {
        }

        return dt_files;
    }

    protected void lbtnDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow item = (sender as LinkButton).Parent.Parent as GridViewRow;
            string FileKey = (item.FindControl("lblfilekey") as Label).Text;

            Session["download_file_key"] = FileKey;

            string URL = ResolveClientUrl("~") + "/download.ashx?path=" + MiscFunctions.RandomString(20).ToLower();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openURL_downloadfile", "openURL('" + URL + "');", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Move_Scroll", "setTimeout(function () { $('html, body').animate({ scrollTop: $('html, body').scrollTop() + 500 }, 800);}, 200);", true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void lbtnRemoveFile_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow item = (sender as LinkButton).Parent.Parent as GridViewRow;
            string filekey = (item.FindControl("lblfilekey") as Label).Text;

            DataTable dt_files = Session["NewProject_Files"] as DataTable;

            dt_files.Rows.Cast<DataRow>().Where(
                r => r.ItemArray[0].ToString() == filekey).ToList().ForEach(r => r.Delete());

            Session["NewProject_Files"] = dt_files;
            rProjectFiles.DataSource = dt_files;
            rProjectFiles.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Move_Scroll", "setTimeout(function () { $('html, body').animate({ scrollTop: $('html, body').scrollTop() + 500 }, 800);}, 200);", true);
        }
        catch (Exception)
        {
        }
    }

    public List<dynamic> GetProjectFunctions()
    {
        List<dynamic> lst = new List<dynamic>();
        Project obj = new Project();
        string UserID = Session["Trabau_UserId"].ToString();

        lst = obj.GetProjectItems(Int64.Parse(UserID));

        return lst;
    }

    public void BindProjectFunctionItems<T>(T rbtnl, int ParentItemID, List<dynamic> _lst = null) where T : ListControl
    {
        try
        {
            List<dynamic> lst = new List<dynamic>();
            if (_lst == null)
            {
                lst = GetProjectFunctions();
            }
            else
            {
                lst = _lst;
            }
            var filter = lst.Where(x => x.ParentItemID == ParentItemID || x.ParentItemID1 == ParentItemID).Select(y => new { y.ItemID, y.ItemName, y.OrderNo }).OrderBy(o => o.OrderNo).ToList<dynamic>();

            rbtnl.DataSource = filter;
            rbtnl.DataTextField = "ItemName";
            rbtnl.DataValueField = "ItemID";
            rbtnl.DataBind();

            if (Session["Trabau_ProjectDetails"] != null && CanOpen_EditProject)
            {
                var edit_ds = Session["Trabau_ProjectDetails"] as DataSet;
                if (rbtnl.ID == "rbtnlOtherProject_Step1")
                {
                    string OtherProjectType = edit_ds.Tables[0].Rows[0]["OtherProjectType"].ToString();
                    rbtnl.SelectedValue = OtherProjectType;
                }
                else if (rbtnl.ID == "rbtnlOtherProject_Step2")
                {
                    string OtherProjectType_Child1 = edit_ds.Tables[0].Rows[0]["OtherProjectType_Child1"].ToString();
                    rbtnl.SelectedValue = OtherProjectType_Child1;
                }
            }

            if (rbtnl is DropDownList)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "rbtnl_open_list", "setTimeout(function () {$('select[id*=" + rbtnl.ID + "]').select2();}, 0);", true);
            }
        }
        catch (Exception ex)
        {

        }

    }
}