using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_searchjobs_UserControls_wucJobDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetJobDetails();
        }
    }

    public void GetJobDetails()
    {
        try
        {
            string JobId = hfJobId.Value;
            string _JobId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(JobId), Trabau_Keys.Job_Key);
            searchjob obj = new searchjob();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds = obj.GetJobDetails(Int32.Parse(UserID), Int32.Parse(_JobId));

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string UserType = Session["Trabau_UserType"].ToString();
                        div_proposal_submission.Visible = (UserType == "W" ? true : false);
                        string ProposalId = ds.Tables[0].Rows[0]["ProposalId"].ToString();
                        string ProposalStatus = ds.Tables[0].Rows[0]["ProposalStatus"].ToString();
                        string SavedStatus = ds.Tables[0].Rows[0]["SavedStatus"].ToString();

                        string Title = ds.Tables[0].Rows[0]["JobTitle"].ToString();
                        string JobCategory = ds.Tables[0].Rows[0]["JobCategory"].ToString();
                        string JobCategoryText = ds.Tables[0].Rows[0]["JobCategoryText"].ToString();
                        string Description = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                        string PostedOn = ds.Tables[0].Rows[0]["PostedOn"].ToString();
                        string JobNumberOfPeople = ds.Tables[0].Rows[0]["JobNumberOfPeople"].ToString();
                        string JobBudgetTypeText = ds.Tables[0].Rows[0]["JobBudgetTypeText"].ToString();
                        string JobBudgetValue = ds.Tables[0].Rows[0]["JobBudgetValue"].ToString();
                        string JobLevelOfExperience = ds.Tables[0].Rows[0]["JobLevelOfExperience"].ToString();
                        string JobExperienceInfo = ds.Tables[0].Rows[0]["JobExperienceInfo"].ToString();


                        string ProposalsCount = ds.Tables[0].Rows[0]["ProposalsCount"].ToString();
                        string InterviewingCount = ds.Tables[0].Rows[0]["InterviewingCount"].ToString();
                        string InvitesCount = ds.Tables[0].Rows[0]["InvitesCount"].ToString();
                        string UnanswererdInvitesCount = ds.Tables[0].Rows[0]["UnanswererdInvitesCount"].ToString();

                        string JobLocationInfo = ds.Tables[0].Rows[0]["JobLocationInfo"].ToString();
                        string JobPaymentType = ds.Tables[0].Rows[0]["JobPaymentTypeText"].ToString();
                        string JobTypeText = ds.Tables[0].Rows[0]["JobTypeText"].ToString();

                        string ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
                        string ClientCityName = ds.Tables[0].Rows[0]["ClientCityName"].ToString();
                        string ClientCountryName = ds.Tables[0].Rows[0]["ClientCountryName"].ToString();
                        string ClientRegDate = ds.Tables[0].Rows[0]["ClientRegDate"].ToString();
                        string Client_TotalJobsPosted = ds.Tables[0].Rows[0]["TotalJobsPosted"].ToString();
                        string Client_TotalJobsOpen = ds.Tables[0].Rows[0]["TotalJobsOpen"].ToString();
                        string ClientHireRate = ds.Tables[0].Rows[0]["ClientHireRate"].ToString();
                        string ClientUserId = ds.Tables[0].Rows[0]["ClientUserId"].ToString();
                        string PreferList_Status = ds.Tables[0].Rows[0]["PreferList_Status"].ToString();
                        if (PreferList_Status == "Y")
                        {
                            aAddToPreferList.Attributes.Add("class", "save-job disabled");
                            aAddToPreferList.InnerHtml = "<i class='fa fa-check' aria-hidden='true'></i> Prefer List";
                            aAddToPreferList.Attributes.Add("data-content", "Remove Client from preferred list");
                        }

                        ltrlClientCityName.Text = ClientCityName;
                        ltrlClientCountryName.Text = ClientCountryName;
                        ltrlClientRegDate.Text = ClientRegDate;
                        ltrlClientJobPostedCount.Text = Client_TotalJobsPosted;
                        ltrlClientJobOpenCount.Text = Client_TotalJobsOpen;
                        ltrlClientHireRate.Text = ClientHireRate + "%";

                        ltrlLocationInfo.Text = JobLocationInfo;
                        ltrlPaymentType.Text = JobPaymentType;
                        ltrlTypeOfWork.Text = JobTypeText;



                        //lblNoOfPeople.Text = JobNumberOfPeople;

                        ltrlJobTitle.Text = Title;
                        ltrlJobDescription.Text = Description;
                        ltrlJobCategory.Text = JobCategoryText;
                        ltrlJobPostedOn.Text = PostedOn;
                        ltrlBudgetType.Text = JobBudgetTypeText;
                        ltrlBudgetValue.Text = "$" + JobBudgetValue;
                        ltrlExperienceLevel.Text = JobLevelOfExperience;
                        ltrlExperienceInfo.Text = JobExperienceInfo;

                        try
                        {
                            var ls_AllSkills = ds.Tables[1].ToDynamic();
                            var ls_skills = ls_AllSkills.Select(x => new
                            {
                                x.ExpertiseType,
                                x.ExpertiseValue,
                                x.ExpertiseSrNo
                            }).ToList();

                            rSkills.DataSource = ls_skills.Select(x => new { x.ExpertiseType, x.ExpertiseSrNo }).Distinct().OrderBy(y => y.ExpertiseSrNo);
                            rSkills.DataBind();

                            foreach (RepeaterItem item in rSkills.Items)
                            {
                                string ExpertiseType = (item.FindControl("ltrlExpertiseType") as Literal).Text;

                                Repeater rFE = (item.FindControl("rFE") as Repeater);
                                try
                                {
                                    rFE.DataSource = ls_skills.Where(x => x.ExpertiseType == ExpertiseType).ToList();
                                    rFE.DataBind();
                                }
                                catch (Exception)
                                {
                                }

                            }
                        }
                        catch (Exception)
                        {
                        }

                        ltrlProposalsCount.Text = ProposalsCount;
                        ltrlInterviewingCount.Text = InterviewingCount;
                        ltrlInvitesCount.Text = InvitesCount;
                        ltrlUnanswererdInvitesCount.Text = UnanswererdInvitesCount;

                        if (ProposalStatus != string.Empty)
                        {
                            aApply.InnerText = ProposalStatus;
                            aApply.Attributes.Add("class", "btn-disabled");
                            lblAlreadyApplied.Text = "You have already submitted a proposal";
                            div_alreadyapplied.Visible = true;
                            aApply.Attributes.Add("data-content", "Already Applied");
                        }
                        else
                        {
                            aApply.HRef = "../../jobs/searchjobs/apply.aspx?job=" + JobId;
                        }

                        aSaveJob.Attributes.Add("onclick", "SaveJob('" + JobId + "')");
                        aSaveJob.InnerHtml = SavedStatus;
                        if (SavedStatus.Contains("Saved"))
                        {
                            aSaveJob.Attributes.Add("data-content", "Saved");
                        }

                        ProposalId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(ProposalId, Trabau_Keys.Job_Key));

                        a_view_proposal.HRef = "../../jobs/proposals/viewproposal.aspx?proposal=" + ProposalId;

                        ClientUserId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(ClientUserId, Trabau_Keys.Profile_Key));
                        aAddToPreferList.Attributes.Add("data", ClientUserId);

                        aSendJob.Attributes.Add("data", JobId);

                        try
                        {
                            var lst_other_jobs = ds.Tables[3].ToDynamic().Select(x => new
                            {
                                JobId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.JobId.ToString(), Trabau_Keys.Job_Key)),
                                x.JobTitle,
                                x.JobBudgetType,
                                x.JobLevelOfExperience,
                                x.JobBudgetValue,
                                x.PostedOn,
                                x.JobDescription,
                                x.ProposalsCount,
                                x.JobLocationInfo
                            });
                            rotherjobs.DataSource = lst_other_jobs;
                            rotherjobs.DataBind();
                        }
                        catch (Exception)
                        {
                        }
                        //DataSet ds_menu = obj.GetPostedJobMenu(Int32.Parse(UserID), JobId);
                        //rPostedJobMenu.DataSource = ds_menu;
                        //rPostedJobMenu.DataBind();


                        //DataTable dt_files = GetProjectFilesStructure();
                        //for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        //{
                        //    DataRow dr = dt_files.NewRow();
                        //    dr["file_name"] = ds.Tables[2].Rows[i]["file_name"].ToString();
                        //    dr["file_key"] = ds.Tables[2].Rows[i]["file_key"].ToString();
                        //    dr["file_bytes"] = Convert.ToBase64String((byte[])ds.Tables[2].Rows[i]["file_bytes"]);

                        //    dt_files.Rows.Add(dr);
                        //}
                        //Session["JobPost_Project_Files"] = dt_files;

                        //rProfileFiles.DataSource = dt_files;
                        //rProfileFiles.DataBind();

                        //if (rProfileFiles.Items.Count == 0)
                        //{
                        //    div_profile_files_empty.Visible = true;
                        //}

                        //try
                        //{
                        //    rNoOfPeople.DataSource = ds.Tables[4];
                        //    rNoOfPeople.DataBind();

                        //    tr_Empty_People.Visible = (rNoOfPeople.Items.Count == 0 ? true : false);
                        //    //int total = 0;
                        //    //foreach (RepeaterItem item in rNoOfPeople.Items)
                        //    //{
                        //    //    string Budget = (item.FindControl("lblPeopleBudgetValue") as Label).Text;
                        //    //    total = total + Int32.Parse(Budget);
                        //    //}
                        //    //lblTotalPeopleBudget.Text = total.ToString() + "$";
                        //}
                        //catch (Exception)
                        //{
                        //}
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}