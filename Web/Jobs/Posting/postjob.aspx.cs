using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.BLL;
using TrabauClassLibrary.DLL;
using TrabauClassLibrary.DLL.Job;
using TrabauClassLibrary.DLL.SignUp;

public partial class Jobs_Posting_postjob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form.Count > 0)
                {
                    if (Request.Form[0] != null)
                    {

                        string file_data = Request.Form[0];
                        var request = JsonConvert.DeserializeObject<dynamic>(file_data.Substring(file_data.IndexOf("{"), file_data.IndexOf("}")).Replace("\r\n-------", ""));
                        //string[] files = file_data.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                        //var file = Convert.FromBase64String(files[2].Replace("\"",""));
                        var file = Convert.FromBase64String(request.contentAsBase64String.Value);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            ltrlPageNo.Text = "1";
            div_step1.Visible = true;
            ltrlHeader.Text = "Title";
            Session["JobPost_Project_Files"] = null;
            Session["JobPost_Project_ScreeningQuestions"] = null;
            Session["JobPost_Talent_ScreeningQuestions"] = null;
            Session["download_file_key"] = null;

            if (Session["PostedJob_JobId"] != null)
            {
                string JobId = Session["PostedJob_JobId"].ToString();

                ViewState["PostedJob_JobId"] = JobId;
                Session["PostedJob_JobId"] = null;

                try
                {
                    if (Session["PostedJob_Reuse"] != null)
                    {
                        ViewState["PostedJob_JobId"] = null;
                        Session["PostedJob_Reuse"] = null;
                    }
                }
                catch (Exception)
                {
                }

                DisplayJobDetails(Int32.Parse(JobId));
            }
        }
    }


    public void DisplayJobDetails(int JobId)
    {
        try
        {
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds = obj.GetPostedJobDetails(Int32.Parse(UserID), JobId);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string Title = ds.Tables[0].Rows[0]["JobTitle"].ToString();
                        string JobCategory = ds.Tables[0].Rows[0]["JobCategory"].ToString();
                        string Description = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                        string JobType = ds.Tables[0].Rows[0]["JobType"].ToString();
                        string JobPaymentType = ds.Tables[0].Rows[0]["JobPaymentType"].ToString();
                        string BusinessSize = ds.Tables[0].Rows[0]["BusinessSize"].ToString();
                        string Deliverable = ds.Tables[0].Rows[0]["Deliverable"].ToString();
                        string Languages = ds.Tables[0].Rows[0]["Languages"].ToString();
                        string Skills = ds.Tables[0].Rows[0]["Skills"].ToString();
                        string AdditionalSkills = ds.Tables[0].Rows[0]["AdditionalSkills"].ToString();
                        string JobVisibility = ds.Tables[0].Rows[0]["JobVisibility"].ToString();
                        string JobNumberOfPeople = ds.Tables[0].Rows[0]["JobNumberOfPeople"].ToString();
                        string JobLocation = ds.Tables[0].Rows[0]["JobLocation"].ToString();
                        string LocationDistanceType = ds.Tables[0].Rows[0]["LocationDistanceType"].ToString();
                        string LocationZipCode = ds.Tables[0].Rows[0]["LocationZipCode"].ToString();
                        string LocationDistance = ds.Tables[0].Rows[0]["LocationDistance"].ToString();
                        string LocationDistanceUnits = ds.Tables[0].Rows[0]["LocationDistanceUnits"].ToString();
                        string JobBudgetType = ds.Tables[0].Rows[0]["JobBudgetType"].ToString();
                        string JobBudgetValue = ds.Tables[0].Rows[0]["JobBudgetValue"].ToString();
                        string JobBudgetBonusValue = ds.Tables[0].Rows[0]["JobBudgetBonusValue"].ToString();
                        //  string JobLevelOfExperience = ds.Tables[0].Rows[0]["JobLevelOfExperience"].ToString();

                        txtJobTitle.Text = Title;
                        //txtJobDescription.Text = Description;
                        hfJobDescription.Value = Description;
                        rbtnlJobCategory.SelectedValue = JobCategory;
                        rbtnlTypeOfWork.SelectedValue = JobType;
                        rbtnlPaymentType.SelectedValue = JobPaymentType;
                        hfProjectSkills_Deliverable.Value = Deliverable;
                        hfProjectSkills_Languages.Value = Languages;
                        hfProjectSkills_Skills.Value = Skills;

                        if (BusinessSize != string.Empty)
                        {
                            rbtnlBusinessSize.SelectedValue = BusinessSize;
                        }
                        hfAdditionalSkills.Value = AdditionalSkills;
                        rbtnlJobVisibility.SelectedValue = JobVisibility;
                        if (JobNumberOfPeople == "1")
                        {
                            rbtnlNoOfFreelancers.SelectedValue = "One";
                        }
                        else
                        {
                            rbtnlNoOfFreelancers.SelectedIndex = 1;
                            div_More_Freelancers.Visible = true;
                            txtNumberOfFreelancers.Text = JobNumberOfPeople;
                        }

                        BindLocations();
                        ddlJobLocation.SelectedValue = JobLocation;
                        ddlJobLocation_SelectedIndexChanged(ddlJobLocation, new EventArgs());
                        if (ddlJobLocation.SelectedValue == "USA")
                        {
                            ddlLocationDistance.SelectedValue = LocationDistanceType;
                            ddlLocationDistance_SelectedIndexChanged(ddlLocationDistance, new EventArgs());
                            if (ddlLocationDistance.SelectedValue == "Local Only")
                            {
                                txtLocalLocation_ZipCode.Text = LocationZipCode;
                                txtLocalLocation_Distance.Text = LocationDistance;
                            }
                        }
                        rbtnlBudgetType.SelectedValue = JobBudgetType;
                        txtBudgetValue.Text = JobBudgetValue;
                        if (JobBudgetBonusValue == "0" || JobBudgetBonusValue == "")
                        {
                            JobBudgetBonusValue = "";
                        }
                        txtBudgetBonusValue.Text = JobBudgetBonusValue;
                        //  rbtnlExperienceLevel.SelectedValue = JobLevelOfExperience;
                        try
                        {
                            foreach (ListItem item in rbtnlExperienceLevel.Items)
                            {
                                string ExpValue = item.Value;
                                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                                {
                                    if (ExpValue == ds.Tables[3].Rows[i]["ExpLevel"].ToString())
                                    {
                                        item.Selected = true;
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }


                        string[] selectedColumns = new[] { "question_key", "question" };
                        try
                        {
                            DataView dv_sq = ds.Tables[1].DefaultView;
                            dv_sq.RowFilter = "QuestionType='ScreeningQuestions'";

                            DataTable dt_sq = dv_sq.ToTable(false, selectedColumns);

                            Session["JobPost_Project_ScreeningQuestions"] = dt_sq;

                            rScreeningQuestions.DataSource = dt_sq;
                            rScreeningQuestions.DataBind();
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            DataView dv_sq = ds.Tables[1].DefaultView;
                            dv_sq.RowFilter = "QuestionType='TalentScreeningQuestions'";

                            DataTable dt_sq = dv_sq.ToTable(false, selectedColumns);

                            Session["JobPost_Talent_ScreeningQuestions"] = dt_sq;

                            rTalentPreferences.DataSource = dt_sq;
                            rTalentPreferences.DataBind();
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            DataTable dt_files = GetProjectFilesStructure();
                            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                            {
                                DataRow dr = dt_files.NewRow();
                                dr["file_name"] = ds.Tables[2].Rows[i]["file_name"].ToString();
                                dr["file_key"] = ds.Tables[2].Rows[i]["file_key"].ToString();
                                dr["file_bytes"] = Convert.ToBase64String((byte[])ds.Tables[2].Rows[i]["file_bytes"]);

                                dt_files.Rows.Add(dr);
                            }
                            Session["JobPost_Project_Files"] = dt_files;
                            rProfileFiles.DataSource = dt_files;
                            rProfileFiles.DataBind();
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            DataTable dt_people = ds.Tables[4];
                            rNoOfPeople.DataSource = dt_people;
                            rNoOfPeople.DataBind();

                            if (rNoOfPeople.Items.Count > 0)
                            {
                                //  DataSet ds_title = obj.GetPeopleTitle(Int64.Parse(UserID));
                                foreach (RepeaterItem item in rNoOfPeople.Items)
                                {
                                    //DropDownList ddlPeopleTitle = (item.FindControl("ddlPeopleTitle") as DropDownList);
                                    //ddlPeopleTitle.DataSource = ds_title;
                                    //ddlPeopleTitle.DataValueField = "Id";
                                    //ddlPeopleTitle.DataTextField = "Title";
                                    //ddlPeopleTitle.DataBind();

                                    //ddlPeopleTitle.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                                    //try
                                    //{
                                    //    ddlPeopleTitle.SelectedValue = (item.FindControl("lblPeopleTitle") as Label).Text;
                                    //}
                                    //catch (Exception)
                                    //{
                                    //}

                                    try
                                    {
                                        DropDownList ddlPaymentFrequency = (item.FindControl("ddlPaymentFrequency") as DropDownList);
                                        ddlPaymentFrequency.SelectedValue = (item.FindControl("lblPaymentFrequency") as Label).Text;
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }


                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlPeopleTitle_open_list", "setTimeout(function () {$('.people_row select').select2();}, 0);", true);
                            }
                        }
                        catch (Exception)
                        {
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlJobLocation.ID + "]').select2();}, 0);", true);
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            if (div_step1.Visible)
            {
                // Exit
                Response.Redirect("postedjobs.aspx");
            }
            else if (div_step2.Visible)
            {
                div_step1.Visible = true;
                div_step2.Visible = false;
                btnExit.Text = "Exit";

                ltrlPageNo.Text = "1";

                li_step1.Attributes.Add("class", "process");
                li_step2.Attributes.Add("class", "");
            }
            else if (div_step3.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = true;
                div_step3.Visible = false;
                btnExit.Text = "Back";

                ltrlPageNo.Text = "2";
                ltrlHeader.Text = "Description";

                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "process");
                li_step3.Attributes.Add("class", "");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Count_Characters", "setTimeout(function () { CountCharacters();}, 50);", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();;}, 0);", true);
            }
            else if (div_step4.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = false;
                div_step3.Visible = true;
                div_step4.Visible = false;
                btnExit.Text = "Back";

                ltrlPageNo.Text = "3";
                ltrlHeader.Text = "Details";

                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "active");
                li_step3.Attributes.Add("class", "process");
                li_step4.Attributes.Add("class", "");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Radio_button_selection", "setTimeout(function () { RadioButtonSelection('rbtnlTypeOfWork');RadioButtonSelection_Override();CheckRadioButton();ActivateToolTip();}, 0);", true);
            }
            else if (div_step5.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = false;
                div_step3.Visible = false;
                div_step4.Visible = true;
                div_step5.Visible = false;
                btnExit.Text = "Back";

                ltrlPageNo.Text = "4";
                ltrlHeader.Text = "Expertise";

                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "active");
                li_step3.Attributes.Add("class", "active");
                li_step4.Attributes.Add("class", "process");
                li_step5.Attributes.Add("class", "");


                GetSkillsList(ddlProjectSkills_Deliverable, hfProjectSkills_Deliverable, "Front End Deliverables", "Select Deliverable");
                GetSkillsList(ddlProjectSkills_Languages, hfProjectSkills_Languages, "Front End Languages", "Select Languages");
                GetSkillsList(ddlProjectSkills_Skills, hfProjectSkills_Skills, "Front End Skills", "Select Skills");
                GetSkillsList(ddlAdditionalSkills, hfAdditionalSkills, "", "Select Additional Skills");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "CheckBSE_RadioButton", "setTimeout(function () { CheckBSE_RadioButton();ActivateToolTip_AdditionalSkills(); }, 0);", true);
            }
            else if (div_step6.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = false;
                div_step3.Visible = false;
                div_step4.Visible = false;
                div_step5.Visible = true;
                div_step6.Visible = false;
                btnExit.Text = "Back";

                ltrlPageNo.Text = "5";
                ltrlHeader.Text = "Visibility";

                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "active");
                li_step3.Attributes.Add("class", "active");
                li_step4.Attributes.Add("class", "active");
                li_step5.Attributes.Add("class", "process");
                li_step6.Attributes.Add("class", "");

                //string select_message = "Select Location";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {RegisterSelect2('" + ddlJobLocation.ID + "', '" + select_message + "', '" + hfJobLocation.ID + "','0');}, 20);", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlJobLocation.ID + "]').select2();}, 0);", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Check_RadioButton", "setTimeout(function () { CheckRadioButton();}, 0);", true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), ddlLocationDistance.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlLocationDistance.ID + "]').select2();}, 0);", true);
            }
            else if (div_step7.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = false;
                div_step3.Visible = false;
                div_step4.Visible = false;
                div_step5.Visible = false;
                div_step6.Visible = true;
                div_step7.Visible = false;
                btnExit.Text = "Back";

                ltrlPageNo.Text = "6";
                ltrlHeader.Text = "Budget";

                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "active");
                li_step3.Attributes.Add("class", "active");
                li_step4.Attributes.Add("class", "active");
                li_step5.Attributes.Add("class", "active");
                li_step6.Attributes.Add("class", "process");
                li_step7.Attributes.Add("class", "");

                btnNext.Text = "Next";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlPeopleTitle_open_list", "setTimeout(function () {$('.people_row select').select2();}, 0);", true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Radio_button_selection", "setTimeout(function () { RadioButtonSelection_Override();CheckRadioButton();CheckCheckboxButton();}, 0);", true);
            }

        }
        catch (Exception)
        {
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Move_Scroll", "setTimeout(function () { $('html, body').animate({ scrollTop: 0 }, 800);}, 200);", true);
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (div_step1.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = true;
                btnExit.Text = "Back";

                ltrlPageNo.Text = "2";
                ltrlHeader.Text = "Description";

                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "process");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Count_Characters", "setTimeout(function () { CountCharacters();}, 50);", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {$('.textEditor').jqte();}, 0);", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();}, 0);", true);
            }
            else if (div_step2.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = false;
                div_step3.Visible = true;
                btnExit.Text = "Back";

                ltrlPageNo.Text = "3";
                ltrlHeader.Text = "Details";

                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "active");
                li_step3.Attributes.Add("class", "process");

                //foreach (ListItem item in rbtnlTypeOfWork.Items)
                //{
                //   // item.Attributes.Add("customtooltip", "sfsdjhfsd sdjfgsdf");
                //}
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Radio_button_selection", "setTimeout(function () { RadioButtonSelection('rbtnlTypeOfWork');RadioButtonSelection_Override();CheckRadioButton();ActivateToolTip();}, 0);", true);
            }
            else if (div_step3.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = false;
                div_step3.Visible = false;
                div_step4.Visible = true;
                btnExit.Text = "Back";

                ltrlPageNo.Text = "4";
                ltrlHeader.Text = "Expertise";

                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "active");
                li_step3.Attributes.Add("class", "active");
                li_step4.Attributes.Add("class", "process");

                GetSkillsList(ddlProjectSkills_Deliverable, hfProjectSkills_Deliverable, "Front End Deliverables", "Select Deliverable");
                GetSkillsList(ddlProjectSkills_Languages, hfProjectSkills_Languages, "Front End Languages", "Select Languages");
                GetSkillsList(ddlProjectSkills_Skills, hfProjectSkills_Skills, "Front End Skills", "Select Skills");
                GetSkillsList(ddlAdditionalSkills, hfAdditionalSkills, "", "Select Additional Skills");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "CheckBSE_RadioButton", "setTimeout(function () { CheckBSE_RadioButton();ActivateToolTip_AdditionalSkills(); }, 0);", true);
            }
            else if (div_step4.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = false;
                div_step3.Visible = false;
                div_step4.Visible = false;
                div_step5.Visible = true;
                btnExit.Text = "Back";

                ltrlPageNo.Text = "5";
                ltrlHeader.Text = "Visibility";

                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "active");
                li_step3.Attributes.Add("class", "active");
                li_step4.Attributes.Add("class", "active");
                li_step5.Attributes.Add("class", "process");


                BindLocations();
                //string select_message = "Select Location";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {RegisterSelect2('" + ddlJobLocation.ID + "', '" + select_message + "', '" + hfJobLocation.ID + "','0');}, 20);", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlJobLocation.ID + "]').select2();}, 0);", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), ddlLocationDistance.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlLocationDistance.ID + "]').select2();}, 0);", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Check_RadioButton", "setTimeout(function () { CheckRadioButton();}, 0);", true);

                if (divLocalLocation.Visible)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), ddlLocationDistance.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlLocationDistance.ID + "]').select2();}, 0);", true);
                }
            }
            else if (div_step5.Visible)
            {
                div_step1.Visible = false;
                div_step2.Visible = false;
                div_step3.Visible = false;
                div_step4.Visible = false;
                div_step5.Visible = false;
                div_step6.Visible = true;
                btnExit.Text = "Back";

                ltrlPageNo.Text = "6";
                ltrlHeader.Text = "Budget";

                li_step1.Attributes.Add("class", "active");
                li_step2.Attributes.Add("class", "active");
                li_step3.Attributes.Add("class", "active");
                li_step4.Attributes.Add("class", "active");
                li_step5.Attributes.Add("class", "active");
                li_step6.Attributes.Add("class", "process");

                DisplayNoOfPeople();


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Radio_button_selection", "setTimeout(function () { RadioButtonSelection_Override();CheckRadioButton();CheckCheckboxButton();}, 0);", true);
            }
            else if (div_step6.Visible)
            {
                if (CheckExperienceSelection())
                {
                    div_step1.Visible = false;
                    div_step2.Visible = false;
                    div_step3.Visible = false;
                    div_step4.Visible = false;
                    div_step5.Visible = false;
                    div_step6.Visible = false;
                    div_step7.Visible = true;
                    btnExit.Text = "Back";

                    ltrlPageNo.Text = "7";
                    ltrlHeader.Text = "Review";

                    li_step1.Attributes.Add("class", "active");
                    li_step2.Attributes.Add("class", "active");
                    li_step3.Attributes.Add("class", "active");
                    li_step4.Attributes.Add("class", "active");
                    li_step5.Attributes.Add("class", "active");
                    li_step6.Attributes.Add("class", "active");
                    li_step7.Attributes.Add("class", "process");

                    btnNext.Text = "Post Job";
                    DisplayReview();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Radio_button_selection", "setTimeout(function () { RadioButtonSelection_Override();CheckRadioButton();CheckCheckboxButton();}, 0);", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CheckExperienceSelection_Message", "setTimeout(function () { toastr['error']('Please select Job Experience Level');}, 200);", true);
                }
            }
            else if (div_step7.Visible)
            {
                PostJob();
            }

        }
        catch (Exception)
        {
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Move_Scroll", "setTimeout(function () { $('html, body').animate({ scrollTop: 0 }, 800);}, 200);", true);
    }

    public void DisplayNoOfPeople()
    {
        try
        {
            DataTable dtPeople = GetNoOfPeople();

            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();

            int total = Int32.Parse((rbtnlNoOfFreelancers.SelectedValue == "More than one" ? txtNumberOfFreelancers.Text : "1"));
            if (total != rNoOfPeople.Items.Count)
            {
                if (total > dtPeople.Rows.Count)
                {
                    for (int i = dtPeople.Rows.Count; i < total; i++)
                    {
                        DataRow dr = dtPeople.NewRow();
                        dr["Title"] = txtJobTitle.Text;
                        dr["Budget"] = "";
                        dr["PaymentFrequency"] = (rbtnlBudgetType.SelectedValue == "Fixed" ? "Once" : "0");

                        dtPeople.Rows.Add(dr);
                    }
                }
                else
                {
                    while (dtPeople.Rows.Count > total)
                    {
                        dtPeople.Rows[dtPeople.Rows.Count - 1].Delete();
                        dtPeople.AcceptChanges();
                    }
                }

                rNoOfPeople.DataSource = dtPeople;
                rNoOfPeople.DataBind();
                if (rNoOfPeople.Items.Count > 0)
                {
                    // DataSet ds_title = obj.GetPeopleTitle(Int64.Parse(UserID));
                    foreach (RepeaterItem item in rNoOfPeople.Items)
                    {
                        //DropDownList ddlPeopleTitle = (item.FindControl("ddlPeopleTitle") as DropDownList);
                        //ddlPeopleTitle.DataSource = ds_title;
                        //ddlPeopleTitle.DataValueField = "Id";
                        //ddlPeopleTitle.DataTextField = "Title";
                        //ddlPeopleTitle.DataBind();

                        //ddlPeopleTitle.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                        //try
                        //{
                        //    ddlPeopleTitle.SelectedValue = (item.FindControl("lblPeopleTitle") as Label).Text;
                        //}
                        //catch (Exception)
                        //{
                        //}

                        try
                        {
                            DropDownList ddlPaymentFrequency = (item.FindControl("ddlPaymentFrequency") as DropDownList);
                            ddlPaymentFrequency.SelectedValue = (item.FindControl("lblPaymentFrequency") as Label).Text;
                        }
                        catch (Exception)
                        {
                        }
                    }

                    CalcBudget();


                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlPeopleTitle_open_list", "setTimeout(function () {$('.people_row select').select2();}, 0);", true);
        }
        catch (Exception)
        {
        }
    }

    public DataTable GetNoOfPeople()
    {
        DataTable dtPeople = new DataTable();
        dtPeople.Columns.Add("Title", typeof(string));
        dtPeople.Columns.Add("Budget", typeof(string));
        dtPeople.Columns.Add("PaymentFrequency", typeof(string));
        try
        {
            foreach (RepeaterItem item in rNoOfPeople.Items)
            {
                //string Title = (item.FindControl("ddlPeopleTitle") as DropDownList).SelectedValue;
                string Title = (item.FindControl("txtPeopleTitle") as TextBox).Text;
                string Budget = (item.FindControl("txtPeopleBudget") as TextBox).Text;
                string PaymentFrequency = (item.FindControl("ddlPaymentFrequency") as DropDownList).SelectedValue;
                if (Budget != string.Empty || PaymentFrequency != "0")
                {
                    DataRow dr = dtPeople.NewRow();
                    dr["Title"] = Title;
                    dr["Budget"] = Budget;
                    dr["PaymentFrequency"] = PaymentFrequency;
                    dtPeople.Rows.Add(dr);
                }
            }
        }
        catch (Exception)
        {
        }

        return dtPeople;
    }

    public string GetNoOfPeople_XML()
    {
        string XMLData = string.Empty;
        try
        {
            DataTable dtPeople = GetNoOfPeople();
            XMLData = MiscFunctions.GetXMLString(dtPeople, "ProjectPeople");
        }
        catch (Exception)
        {
        }
        return XMLData;
    }

    public bool CheckExperienceSelection()
    {
        bool val = false;
        try
        {
            foreach (ListItem item in rbtnlExperienceLevel.Items)
            {
                if (item.Selected)
                {
                    val = true;
                    break;
                }
            }
        }
        catch (Exception)
        {
        }

        return val;
    }

    public Tuple<string, DataTable> GetExperienceSelection()
    {
        string Exp = "";
        DataTable dt = new DataTable();
        dt.Columns.Add("Text", typeof(string));
        try
        {
            foreach (ListItem item in rbtnlExperienceLevel.Items)
            {
                if (item.Selected)
                {
                    DataRow dr = dt.NewRow();
                    if (Exp == string.Empty)
                    {
                        Exp = item.Value;
                        dr["Text"] = item.Value;
                    }
                    else
                    {
                        Exp = Exp + "," + item.Value;
                        dr["Text"] = item.Value;
                    }
                    dt.Rows.Add(dr);
                }
            }
        }
        catch (Exception)
        {
        }

        return new Tuple<string, DataTable>(Exp, dt);
    }

    public void PostJob()
    {
        try
        {
            string Title = txtJobTitle.Text;
            string Category = rbtnlJobCategory.SelectedValue;
            //string Description = txtJobDescription.Text;
            string Description = hfJobDescription.Value;
            string XML_ProjectFiles = GetProjectFiles();
            string TypeOfWork = rbtnlTypeOfWork.SelectedValue;
            string PaymentType = rbtnlPaymentType.SelectedValue;
            string XML_ScreeningQuestions = GetScreeningQuestions();
            string FE_ProjectSkills_Deliverable = hfProjectSkills_Deliverable.Value;
            string FE_ProjectSkills_Languages = hfProjectSkills_Languages.Value;
            string FE_ProjectSkills = hfProjectSkills_Skills.Value;
            string BusinessSize = rbtnlBusinessSize.SelectedValue;
            string AdditionalSkills = hfAdditionalSkills.Value;
            string Visibility = rbtnlJobVisibility.SelectedValue;
            string NoOfFreelancers = (rbtnlNoOfFreelancers.SelectedValue == "More than one" ? txtNumberOfFreelancers.Text : "1");
            string Location = ddlJobLocation.SelectedValue;
            string LocationDistanceType = ddlLocationDistance.SelectedValue;
            string LocationZipCode = txtLocalLocation_ZipCode.Text;
            string LocationDistance = (txtLocalLocation_Distance.Text == string.Empty ? "0" : txtLocalLocation_Distance.Text);
            string LocationDistanceUnits = "miles";
            string XML_TalentScreeningQuestions = GetTalentScreeningQuestions();
            string BudgetType = rbtnlBudgetType.SelectedValue;
            string BudgetValue = txtBudgetValue.Text;
            string Bonus_BudgetValue = (txtBudgetBonusValue.Text == string.Empty ? "0" : txtBudgetBonusValue.Text);
            string LevelOfExperience = GetExperienceSelection().Item1;
            string IPAddress = Request.UserHostAddress;
            string XML_People = GetNoOfPeople_XML();
            string UserID = Session["Trabau_UserId"].ToString();
            string JobId = "0";
            try
            {
                JobId = ViewState["PostedJob_JobId"].ToString();
            }
            catch (Exception)
            {
            }
            jobposting obj = new jobposting();
            string data = obj.SaveJob(Title, Category, Description, TypeOfWork, PaymentType, XML_ScreeningQuestions, FE_ProjectSkills_Deliverable, FE_ProjectSkills_Languages, FE_ProjectSkills,
                BusinessSize, AdditionalSkills, Visibility, Int32.Parse(NoOfFreelancers), Location, LocationDistanceType, LocationZipCode, Int32.Parse(LocationDistance),
                LocationDistanceUnits, XML_TalentScreeningQuestions, BudgetType, Int32.Parse(BudgetValue), Int32.Parse(Bonus_BudgetValue), LevelOfExperience, Int32.Parse(JobId), IPAddress,
                Int32.Parse(UserID), XML_ProjectFiles, XML_People);

            string response = data.Split(':')[0];
            string message = data.Split(':')[1];

            if (response == "success")
            {
                string Name = Session["Trabau_FirstName"].ToString();
                string EmailId = Session["Trabau_EmailId"].ToString();

                if (JobId == "" || JobId == "0")
                {
                    try
                    {
                        string template_url = "https://www.trabau.com/emailers/xxddcca/post-job.html";

                        try
                        {
                            WebRequest req = WebRequest.Create(template_url);
                            WebResponse w_res = req.GetResponse();
                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                            string html = sr.ReadToEnd();

                            html = html.Replace("@Name", Name);
                            html = html.Replace("@JobTitle", Title);

                            string body = html;

                            Emailer obj_email = new Emailer();
                            string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "New Job posting – " + Title, body, null);

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


                        template_url = "https://www.trabau.com/emailers/xxddcca/hire-freelancer-after-jobposting.html";
                        try
                        {
                            WebRequest req = WebRequest.Create(template_url);
                            WebResponse w_res = req.GetResponse();
                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                            string html = sr.ReadToEnd();

                            html = html.Replace("@Name", Name);
                            html = html.Replace("@JobTitle", Title);

                            string body = html;

                            Emailer obj_email = new Emailer();
                            string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Are you searching for an expert?", body, null);

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
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    try
                    {
                        string template_url = "https://www.trabau.com/emailers/xxddcca/post-job-changes.html";

                        try
                        {
                            WebRequest req = WebRequest.Create(template_url);
                            WebResponse w_res = req.GetResponse();
                            StreamReader sr = new StreamReader(w_res.GetResponseStream());
                            string html = sr.ReadToEnd();

                            html = html.Replace("@Name", Name);
                            html = html.Replace("@JobTitle", Title);

                            string body = html;

                            Emailer obj_email = new Emailer();
                            string _val = obj_email.SendEmail(EmailId, "", "", "Trabau Notification", "Posted Job Changes for – " + Title, body, null);

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
                    catch (Exception)
                    {
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JobPost_Message_Success", "setTimeout(function () { window.location.href='postedjobs.aspx';}, 1000);", true);
                //
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "JobPost_Message", "setTimeout(function () { toastr['" + response + "']('" + message + "');}, 200);", true);
        }
        catch (Exception ex)
        {
        }
    }

    public string GetProjectFiles()
    {
        string XMLData = "";
        try
        {
            if (Session["JobPost_Project_Files"] != null)
            {
                DataTable dt_files = Session["JobPost_Project_Files"] as DataTable;

                XMLData = MiscFunctions.GetXMLString(dt_files, "ProjectFiles");
            }
        }
        catch (Exception)
        {
        }

        return XMLData;
    }

    public string GetScreeningQuestions()
    {
        string XMLData = "";
        try
        {
            if (Session["JobPost_Project_ScreeningQuestions"] != null)
            {
                DataTable dt_questions = Session["JobPost_Project_ScreeningQuestions"] as DataTable;

                XMLData = MiscFunctions.GetXMLString(dt_questions, "ScreeningQuestions");
            }
        }
        catch (Exception)
        {
        }

        return XMLData;
    }

    public string GetTalentScreeningQuestions()
    {
        string XMLData = "";
        try
        {
            if (Session["JobPost_Talent_ScreeningQuestions"] != null)
            {
                DataTable dt_questions = Session["JobPost_Talent_ScreeningQuestions"] as DataTable;

                XMLData = MiscFunctions.GetXMLString(dt_questions, "Talent_ScreeningQuestions");
            }
        }
        catch (Exception)
        {
        }

        return XMLData;
    }



    public void DisplayReview()
    {
        try
        {
            ltrlJobTitle_Review.Text = txtJobTitle.Text;
            ltrlJobCategory_Review.Text = rbtnlJobCategory.SelectedItem.Text;
            //ltrlDescription_Review.Text = txtJobDescription.Text;
            ltrlDescription_Review.Text = hfJobDescription.Value;
            string TypeOfWork = rbtnlTypeOfWork.SelectedValue;
            TypeOfWork = (TypeOfWork == "OT" ? "One Time Work" : (TypeOfWork == "ON" ? "Ongoing Work" : "Work as Needed"));
            ltrlTypeOfWork_Review.Text = TypeOfWork;

            string PaymentThrough = rbtnlPaymentType.SelectedValue;
            PaymentThrough = (PaymentThrough == "With_Trabau" ? "Pay through Trabau" : "Pay outside Trabau");
            ltrlPaymentThrough_Review.Text = PaymentThrough;

            try
            {
                if (Session["JobPost_Project_ScreeningQuestions"] != null)
                {
                    DataTable dt_questions = new DataTable();
                    dt_questions = Session["JobPost_Project_ScreeningQuestions"] as DataTable;

                    rScreeningQuestions_Review.DataSource = dt_questions;
                    rScreeningQuestions_Review.DataBind();
                }
            }
            catch (Exception)
            {
            }

            div_screening_ques.Visible = (rScreeningQuestions_Review.Items.Count > 0 ? true : false);


            ltrlRequireCoverLetter_Review.Text = (rScreeningQuestions_Review.Items.Count == 0 ? "Yes" : "No");

            DataSet ds_skills = ViewState["Trabau_Skills"] as DataSet;
            BindDataForReview(rFE_Deliverables_Preview, hfProjectSkills_Deliverable, ds_skills);
            div_FE_Deliverables.Visible = (rFE_Deliverables_Preview.Items.Count > 0 ? true : false);

            BindDataForReview(rFE_Languages_Preview, hfProjectSkills_Languages, ds_skills);
            div_FE_Languages.Visible = (rFE_Languages_Preview.Items.Count > 0 ? true : false);

            BindDataForReview(rFE_DevSkills_Preview, hfProjectSkills_Skills, ds_skills);
            div_FE_DevSkills.Visible = (rFE_DevSkills_Preview.Items.Count > 0 ? true : false);

            string BusinessSize = rbtnlBusinessSize.SelectedValue;
            if (BusinessSize == "VerySmall")
            {
                BusinessSize = "Very Small (1-9 employee)";
            }
            else if (BusinessSize == "Small")
            {
                BusinessSize = "Small (10-100 employee)";
            }
            else if (BusinessSize == "Mid")
            {
                BusinessSize = "Mid (100-999 employee)";
            }
            else if (BusinessSize == "Large")
            {
                BusinessSize = "Large (1000+ employee)";
            }
            else if (BusinessSize == "Startup")
            {
                BusinessSize = "Startup";
            }
            else if (BusinessSize == "Fortune500")
            {
                BusinessSize = "Fortune 500";
            }
            else
            {
                BusinessSize = "No Preference";
            }
            ltrlBusinessSize_Preview.Text = BusinessSize;

            BindDataForReview(rAdditionalSkills_Preview, hfAdditionalSkills, ds_skills);

            string JobVisibility = rbtnlJobVisibility.SelectedValue;
            ltrlJobVisibility_Review.Text = JobVisibility;

            string NoOfPeople = rbtnlNoOfFreelancers.SelectedValue;
            NoOfPeople = (NoOfPeople == "More than one" ? txtNumberOfFreelancers.Text : "1");
            ltrlJobNoOfPeople_Review.Text = NoOfPeople;

            //  DataSet ds_Locations = ViewState["JobPost_Project_Locations"] as DataSet;
            // BindDataForReview(rJobLocation_Preview, hfJobLocation, ds_Locations);
            string JobLocation = ddlJobLocation.SelectedValue;
            ltrlJobLocation_Review.Text = JobLocation;
            divLocationDistance_Review.Visible = false;
            divLocationZipCode_Review.Visible = false;
            divLocationMiles_Review.Visible = false;

            try
            {
                if (JobLocation == "USA")
                {
                    string LocalDistance = ddlLocationDistance.SelectedValue;

                    ltrlLocationDistance.Text = LocalDistance;
                    divLocationDistance_Review.Visible = true;

                    if (LocalDistance == "Local Only")
                    {
                        divLocationZipCode_Review.Visible = true;
                        divLocationMiles_Review.Visible = true;

                        ltrlLocationZipCode_Review.Text = txtLocalLocation_ZipCode.Text;
                        ltrlLocationDistance_Review.Text = txtLocalLocation_Distance.Text + " miles";
                    }
                }
            }
            catch (Exception)
            {

            }

            try
            {
                if (Session["JobPost_Talent_ScreeningQuestions"] != null)
                {
                    DataTable dt_questions = new DataTable();
                    dt_questions = Session["JobPost_Talent_ScreeningQuestions"] as DataTable;

                    rTP_ScreeningQuestions_Review.DataSource = dt_questions;
                    rTP_ScreeningQuestions_Review.DataBind();
                }
            }
            catch (Exception)
            {
            }

            div_TP_ScreeningQues.Visible = (rTP_ScreeningQuestions_Review.Items.Count > 0 ? true : false);

            string BudgetType = rbtnlBudgetType.SelectedValue;
            BudgetType = (BudgetType == "Hour" ? "Pay by the hour" : "Pay a fixed Price");
            ltrlBudgetType_Review.Text = BudgetType;

            ltrlBudgetValue_Review.Text = "$" + txtBudgetValue.Text;
            ltrlBudgetBonusValue_Review.Text = (txtBudgetBonusValue.Text != string.Empty ? "$" + txtBudgetBonusValue.Text : "-");

            DataTable dtExp = GetExperienceSelection().Item2;
            rLevelOfExperience.DataSource = dtExp;
            rLevelOfExperience.DataBind();
        }
        catch (Exception)
        {
        }
    }

    public void BindDataForReview(Repeater rep, HiddenField hfSelected, DataSet data)
    {
        try
        {
            if (hfSelected.Value != string.Empty)
            {

                string Filter = "";
                for (int i = 0; i < hfSelected.Value.Split(',').Length; i++)
                {
                    if (Filter == string.Empty)
                    {
                        Filter = "Value='" + hfSelected.Value.Split(',')[i] + "'";
                    }
                    else
                    {
                        Filter = Filter + " or Value='" + hfSelected.Value.Split(',')[i] + "'";
                    }
                }

                DataView dv_data = data.Tables[0].DefaultView;
                dv_data.RowFilter = Filter;

                rep.DataSource = dv_data;
                rep.DataBind();
            }
            else
            {
                rep.DataSource = null;
                rep.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }

    public void GetSkillsList(DropDownList ddl, HiddenField hidden_field, string SkillType, string select_message)
    {
        try
        {
            jobposting obj = new jobposting();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds_skills = new DataSet();
            if (ViewState["Trabau_Skills"] != null)
            {
                ds_skills = ViewState["Trabau_Skills"] as DataSet;
            }
            else
            {
                ds_skills = obj.GetSkillsList("");
                ViewState["Trabau_Skills"] = ds_skills;
            }

            List<dynamic> skills = ds_skills.Tables[0].ToDynamic();
            var filtered_skills = skills.Where(y => y.SkillType.ToString() == SkillType).Select(x => new { Text = x.Text, Value = x.Value }).ToList();
            //skills = skills.Select(x => new { Text = x.Text, Value = x.Value }).ToList();
            ddl.DataSource = filtered_skills;
            ddl.DataTextField = "Text";
            ddl.DataValueField = "Value";
            ddl.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), ddl.ID + "_open_list", "setTimeout(function () {RegisterSelect2('" + ddl.ID + "', '" + select_message + "', '" + hidden_field.ID + "','0');}, 20);", true);
        }
        catch (Exception ex)
        {

        }

    }


    [WebMethod(EnableSession = true)]
    public static string PostFileData()
    {
        string response = "";
        try
        {
            var output = new StringWriter();
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {



                detail.Add("response", "ok");
                detail.Add("navigation_html", output.ToString().Replace("\r\n", "").ToString());

            }
            else
            {
                detail.Add("response", "");
                detail.Add("navigation_html", "");
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

    protected void afuProjectFiles_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        try
        {
            if (afuProjectFiles.HasFile)
            {
                DataTable dt_files = new DataTable();
                if (Session["JobPost_Project_Files"] != null)
                {
                    dt_files = Session["JobPost_Project_Files"] as DataTable;
                }
                else
                {
                    dt_files = GetProjectFilesStructure();
                }
                byte[] attachment = afuProjectFiles.FileBytes;
                dt_files = AddProjectFilesItem(dt_files, afuProjectFiles.FileName, attachment);
                Session["JobPost_Project_Files"] = dt_files;
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
        return dt_files;
    }

    public DataTable AddProjectFilesItem(DataTable dt_files, string file_name, byte[] file_bytes)
    {
        try
        {
            DataRow dr = dt_files.NewRow();
            dr["file_key"] = MiscFunctions.RandomString(20).ToLower();
            dr["file_name"] = file_name;
            string base64_bytes = Convert.ToBase64String(file_bytes);
            dr["file_bytes"] = base64_bytes;
            dt_files.Rows.Add(dr);
        }
        catch (Exception ex)
        {
        }

        return dt_files;
    }

    protected void btnAddProfileFiles_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt_files = Session["JobPost_Project_Files"] as DataTable;
            rProfileFiles.DataSource = dt_files;
            rProfileFiles.DataBind();

            // upNewJobPost.Update();
        }
        catch (Exception)
        {
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Count_Characters", "setTimeout(function () { CountCharacters();}, 0);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();;}, 0);", true);
    }

    protected void lbtnRemoveFile_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string filekey = (item.FindControl("lblfilekey") as Label).Text;

            DataTable dt_files = Session["JobPost_Project_Files"] as DataTable;

            dt_files.Rows.Cast<DataRow>().Where(
                r => r.ItemArray[0].ToString() == filekey).ToList().ForEach(r => r.Delete());

            Session["JobPost_Project_Files"] = dt_files;
            rProfileFiles.DataSource = dt_files;
            rProfileFiles.DataBind();
        }
        catch (Exception)
        {
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Count_Characters", "setTimeout(function () { CountCharacters();}, 0);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();;}, 0);", true);
    }

    protected void lbtnAddScreeningQuestion_Click(object sender, EventArgs e)
    {
        try
        {
            string ScreeningQuestion = txtScreeningQuestion.Text;
            DataTable dt_questions = new DataTable();
            if (Session["JobPost_Project_ScreeningQuestions"] != null)
            {
                dt_questions = Session["JobPost_Project_ScreeningQuestions"] as DataTable;
            }
            else
            {
                dt_questions = GetProjectScreeningQuestionsStructure();
            }

            dt_questions = AddProjectScreeningQuestionsItem(dt_questions, ScreeningQuestion);
            Session["JobPost_Project_ScreeningQuestions"] = dt_questions;

            txtScreeningQuestion.Text = string.Empty;
            rScreeningQuestions.DataSource = dt_questions;
            rScreeningQuestions.DataBind();
        }
        catch (Exception)
        {
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Radio_button_selection", "setTimeout(function () { RadioButtonSelection('rbtnlTypeOfWork');RadioButtonSelection_Override();CheckRadioButton();}, 0);", true);
    }


    public DataTable GetProjectScreeningQuestionsStructure()
    {
        DataTable dt_questions = new DataTable();
        dt_questions.Columns.Add("question_key", typeof(string));
        dt_questions.Columns.Add("question", typeof(string));
        return dt_questions;
    }

    public DataTable AddProjectScreeningQuestionsItem(DataTable dt_questions, string question)
    {
        try
        {
            DataRow dr = dt_questions.NewRow();
            dr["question_key"] = MiscFunctions.RandomString(20).ToLower();
            dr["question"] = question;
            dt_questions.Rows.Add(dr);
        }
        catch (Exception)
        {
        }

        return dt_questions;
    }

    protected void lbtnRemoveQuestion_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string questionkey = (item.FindControl("lblquestionkey") as Label).Text;

            DataTable dt_questions = Session["JobPost_Project_ScreeningQuestions"] as DataTable;

            dt_questions.Rows.Cast<DataRow>().Where(
                r => r.ItemArray[0].ToString() == questionkey).ToList().ForEach(r => r.Delete());

            Session["JobPost_Project_ScreeningQuestions"] = dt_questions;
            rScreeningQuestions.DataSource = dt_questions;
            rScreeningQuestions.DataBind();
        }
        catch (Exception)
        {
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Radio_button_selection", "setTimeout(function () { RadioButtonSelection('rbtnlTypeOfWork');RadioButtonSelection_Override();CheckRadioButton();}, 0);", true);
    }

    protected void rbtnlNoOfFreelancers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string NoOf_FL = rbtnlNoOfFreelancers.SelectedValue;
            div_More_Freelancers.Visible = false;
            txtNumberOfFreelancers.Text = string.Empty;
            if (NoOf_FL == "More than one")
            {
                div_More_Freelancers.Visible = true;
            }
        }
        catch (Exception)
        {
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Select_RadioButton", "setTimeout(function () { CheckRadioButton();}, 0);", true);

        BindLocations();
        //string select_message = "Select Location";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {RegisterSelect2('" + ddlJobLocation.ID + "', '" + select_message + "', '" + hfJobLocation.ID + "','0');}, 20);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlJobLocation.ID + "]').select2();}, 0);", true);

        if (divLocalLocation.Visible)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), ddlLocationDistance.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlLocationDistance.ID + "]').select2();}, 0);", true);
        }
    }

    public void BindLocations()
    {
        try
        {
            if (ddlJobLocation.Items.Count == 0)
            {
                jobposting obj = new jobposting();
                DataSet ds_country = new DataSet();
                if (ViewState["JobPost_Project_Locations"] != null)
                {
                    ds_country = ViewState["JobPost_Project_Locations"] as DataSet;
                }
                else
                {
                    ds_country = obj.GetLocationList();
                    ViewState["JobPost_Project_Locations"] = ds_country;
                }


                ddlJobLocation.DataSource = ds_country;
                ddlJobLocation.DataTextField = "Text";
                ddlJobLocation.DataValueField = "Value";
                ddlJobLocation.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void lbtnAddTalentPreferencesQuestions_Click(object sender, EventArgs e)
    {
        try
        {
            string ScreeningQuestion = txtTalentPreferences.Text;
            DataTable dt_questions = new DataTable();
            if (Session["JobPost_Talent_ScreeningQuestions"] != null)
            {
                dt_questions = Session["JobPost_Talent_ScreeningQuestions"] as DataTable;
            }
            else
            {
                dt_questions = GetProjectScreeningQuestionsStructure();
            }

            dt_questions = AddProjectScreeningQuestionsItem(dt_questions, ScreeningQuestion);
            Session["JobPost_Talent_ScreeningQuestions"] = dt_questions;

            txtTalentPreferences.Text = string.Empty;
            rTalentPreferences.DataSource = dt_questions;
            rTalentPreferences.DataBind();
        }
        catch (Exception)
        {
        }

        //string select_message = "Select Location";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {RegisterSelect2('" + ddlJobLocation.ID + "', '" + select_message + "', '" + hfJobLocation.ID + "','0');}, 20);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlJobLocation.ID + "]').select2();}, 0);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Check_RadioButton", "setTimeout(function () { CheckRadioButton();}, 0);", true);
    }


    protected void lbtnRemoveTalentPreferences_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string questionkey = (item.FindControl("lblquestionkey") as Label).Text;

            DataTable dt_questions = Session["JobPost_Talent_ScreeningQuestions"] as DataTable;

            dt_questions.Rows.Cast<DataRow>().Where(
                r => r.ItemArray[0].ToString() == questionkey).ToList().ForEach(r => r.Delete());

            Session["JobPost_Talent_ScreeningQuestions"] = dt_questions;
            rTalentPreferences.DataSource = dt_questions;
            rTalentPreferences.DataBind();
        }
        catch (Exception)
        {
        }

        //string select_message = "Select Location";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {RegisterSelect2('" + ddlJobLocation.ID + "', '" + select_message + "', '" + hfJobLocation.ID + "','0');}, 20);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlJobLocation.ID + "]').select2();}, 0);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Check_RadioButton", "setTimeout(function () { CheckRadioButton();}, 0);", true);
    }

    protected void ddlJobLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divLocationDistance.Visible = false;
            divLocalLocation.Visible = false;
            ddlLocationDistance.SelectedIndex = 0;
            txtLocalLocation_ZipCode.Text = string.Empty;
            txtLocalLocation_Distance.Text = string.Empty;

            if (ddlJobLocation.SelectedValue == "USA")
            {
                divLocationDistance.Visible = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), ddlLocationDistance.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlLocationDistance.ID + "]').select2();}, 0);", true);
            }
        }
        catch (Exception)
        {
        }
        if (div_step5.Visible)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Select_RadioButton", "setTimeout(function () { CheckRadioButton();}, 0);", true);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlJobLocation.ID + "]').select2();}, 0);", true);
    }

    protected void ddlLocationDistance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divLocalLocation.Visible = false;
            txtLocalLocation_ZipCode.Text = string.Empty;
            txtLocalLocation_Distance.Text = string.Empty;

            if (ddlLocationDistance.SelectedValue == "Local Only")
            {
                divLocalLocation.Visible = true;
            }
        }
        catch (Exception)
        {
        }
        if (div_step5.Visible)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Select_RadioButton", "setTimeout(function () { CheckRadioButton();}, 0);", true);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), ddlJobLocation.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlJobLocation.ID + "]').select2();}, 0);", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), ddlLocationDistance.ID + "_open_list", "setTimeout(function () {$('select[id*=" + ddlLocationDistance.ID + "]').select2();}, 0);", true);
    }

    protected void lbtnDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string FileKey = (item.FindControl("lblfilekey") as Label).Text;

            Session["download_file_key"] = FileKey;

            string URL = ResolveClientUrl("~") + "/download.ashx?path=" + MiscFunctions.RandomString(20).ToLower();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openURL_downloadfile", "openURL('" + URL + "');", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Count_Characters", "setTimeout(function () { CountCharacters();}, 50);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "text_editor", "setTimeout(function () {LoadEditor();;}, 0);", true);
        }
        catch (Exception ex)
        {
        }

    }

    protected void txtPeopleBudget_TextChanged(object sender, EventArgs e)
    {
        CalcBudget();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlPeopleTitle_open_list", "setTimeout(function () {$('.people_row select').select2();CheckCheckboxButton();}, 0);", true);
    }


    public void CalcBudget()
    {
        try
        {
            int TotalBudget = 0;
            foreach (RepeaterItem item in rNoOfPeople.Items)
            {
                string Budget = (item.FindControl("txtPeopleBudget") as TextBox).Text;
                if (Budget != string.Empty)
                {
                    TotalBudget = TotalBudget + Int32.Parse(Budget);
                }
            }

            txtBudgetValue.Text = TotalBudget.ToString();
        }
        catch (Exception)
        {
        }
    }

    protected void rbtnlBudgetType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem item in rNoOfPeople.Items)
            {
                DropDownList ddlPaymentFrequency = item.FindControl("ddlPaymentFrequency") as DropDownList;
                if (ddlPaymentFrequency.SelectedValue == "0" && rbtnlBudgetType.SelectedValue == "Fixed")
                {
                    ddlPaymentFrequency.SelectedValue = "Once";
                }
            }
        }
        catch (Exception)
        {
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlPeopleTitle_open_list", "setTimeout(function () {$('.people_row select').select2();}, 0);", true);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Radio_button_selection", "setTimeout(function () { RadioButtonSelection_Override();CheckRadioButton();CheckCheckboxButton();}, 0);", true);
    }
}