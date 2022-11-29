using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_proposals_viewproposal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayProposal_Details();
        }
    }

    public void DisplayProposal_Details()
    {
        try
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["proposal"] != null)
                {
                    string ProposalId = Request.QueryString["proposal"];
                    if (ProposalId != string.Empty)
                    {
                        string UserID = Session["Trabau_UserId"].ToString();
                        string _ProposalId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(ProposalId), Trabau_Keys.Job_Key);
                        searchjob obj = new searchjob();
                        DataSet ds = obj.GetSubmittedProposalDetails(Int64.Parse(UserID), Int32.Parse(_ProposalId));

                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    string JobId = ds.Tables[0].Rows[0]["JobId"].ToString();
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
                                    string Prosposed_Bid = ds.Tables[0].Rows[0]["Prosposed_Bid"].ToString();
                                    string Final_Amount = ds.Tables[0].Rows[0]["Final_Amount"].ToString();
                                    string CoverLetter = ds.Tables[0].Rows[0]["CoverLetter"].ToString();
                                    string ClientUserId = ds.Tables[0].Rows[0]["ClientUserId"].ToString();

                                    string JobPaymentType = ds.Tables[0].Rows[0]["JobPaymentTypeText"].ToString();
                                    string PreferList_Status = ds.Tables[0].Rows[0]["PreferList_Status"].ToString();

                                    if (PreferList_Status == "Y")
                                    {
                                        aAddToPreferList.Attributes.Add("class", "save-job disabled");
                                        aAddToPreferList.InnerHtml = "<i class='fa fa-check' aria-hidden='true'></i> Preferred List";
                                        aAddToPreferList.Attributes.Add("data-content", "Remove Client from preferred list");
                                    }

                                    //  ltrlHourlyRate.Text = "$" + HourlyRate + " /hr";
                                    ltrlJobTitle.Text = Title;
                                    ltrlJobDescription.Text = Description;
                                    ltrlJobPostedOn.Text = PostedOn;
                                    ltrlJobCategory.Text = JobCategoryText;
                                    ltrlExperienceLevel.Text = JobLevelOfExperience;
                                    ltrlPaymentType.Text = JobBudgetTypeText;
                                    ltrlBudgetValue.Text = "$" + JobBudgetValue + (JobBudgetTypeText == "Fixed Price" ? "" : " /hr");
                                    ltrlProposedBID.Text = "$" + Prosposed_Bid;
                                    ltrlFinalAmount.Text = "$" + Final_Amount;
                                    ltrlCoverLetter.Text = CoverLetter;




                                    string ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
                                    string ClientCityName = ds.Tables[0].Rows[0]["ClientCityName"].ToString();
                                    string ClientCountryName = ds.Tables[0].Rows[0]["ClientCountryName"].ToString();
                                    string ClientRegDate = ds.Tables[0].Rows[0]["ClientRegDate"].ToString();
                                    string Client_TotalJobsPosted = ds.Tables[0].Rows[0]["TotalJobsPosted"].ToString();
                                    string Client_TotalJobsOpen = ds.Tables[0].Rows[0]["TotalJobsOpen"].ToString();
                                    string ClientHireRate = ds.Tables[0].Rows[0]["ClientHireRate"].ToString();

                                    ltrlClientCityName.Text = ClientCityName;
                                    ltrlClientCountryName.Text = ClientCountryName;
                                    ltrlClientRegDate.Text = ClientRegDate;
                                    ltrlClientJobPostedCount.Text = Client_TotalJobsPosted;
                                    ltrlClientJobOpenCount.Text = Client_TotalJobsOpen;
                                    ltrlClientHireRate.Text = ClientHireRate + "%";


                                    try
                                    {
                                        var ls_AllSkills = ds.Tables[1].ToDynamic();
                                        var ls_skills = ls_AllSkills.Select(x => new
                                        {
                                            x.ExpertiseType,
                                            x.ExpertiseValue,
                                            x.ExpertiseSrNo
                                        }).ToList();

                                        rAllSkillsExpertise.DataSource = ls_skills.Select(x => new
                                        {
                                            x.ExpertiseValue,
                                            x.ExpertiseSrNo
                                        }).OrderBy(y => y.ExpertiseSrNo);
                                        rAllSkillsExpertise.DataBind();
                                    }
                                    catch { }


                                    JobId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(JobId, Trabau_Keys.Job_Key));
                                    aViewJobPosting.HRef = "~/jobs/searchjobs/viewjob.aspx?job=" + JobId;

                                    ClientUserId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(ClientUserId, Trabau_Keys.Profile_Key));
                                    aAddToPreferList.Attributes.Add("data", ClientUserId);
                                }
                            }
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Activate_Tooltip", "setTimeout(function () {Activate_TooltipNow();}, 100);", true);
    }
}