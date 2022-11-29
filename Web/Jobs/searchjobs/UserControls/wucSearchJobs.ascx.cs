using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_searchjobs_UserControls_wucSearchJobs : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Search_Jobs();
        }
    }

    public void Search_Jobs()
    {
        try
        {
            searchjob obj = new searchjob();
            string UserID = Session["Trabau_UserId"].ToString();
            string PageNumber = lblPageNumber_Request.Text;
            string text = lblSearchText_Request.Text;
            string Type = lblSearchType_Request.Text;
            string SavedJobs = lblSavedJobs_Request.Text;

            string JobType = DecryptFilters(lblJobType.Text);
            string ExpLevel = DecryptFilters(lblExpLevel.Text);
            string ClientHistory = DecryptFilters(lblClientHistory.Text);
            string NoOfProposals = DecryptFilters(lblNoOfProposals.Text);
            string Budget = DecryptFilters(lblBudget.Text);
            string HoursPerWeek = DecryptFilters(lblHoursPerWeek.Text);
            string ProjectLength = DecryptFilters(lblProjectLength.Text);

            DataSet ds_jobs = obj.SearchJobs(Int64.Parse(UserID), Type, text, Int32.Parse(PageNumber), JobType, ExpLevel, ClientHistory, NoOfProposals, 
                Budget, HoursPerWeek, ProjectLength, SavedJobs);
            rjobs.DataSource = ds_jobs.Tables[0];
            rjobs.DataBind();
            if (rjobs.Items.Count > 0)
            {
                DataTable dtExpertise = ds_jobs.Tables[1];

                foreach (RepeaterItem item in rjobs.Items)
                {
                    Repeater rExpertise = (item.FindControl("rExpertise") as Repeater);

                    string JobId = (item.FindControl("lblJobId") as Label).Text;
                    string Enc_JobId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(JobId, Trabau_Keys.Job_Key));
                    (item.FindControl("lbl_JobId") as Label).Text = Enc_JobId;
                    DataView dvExp = dtExpertise.DefaultView;
                    dvExp.RowFilter = "JobId=" + JobId;
                    rExpertise.DataSource = dvExp;
                    rExpertise.DataBind();

                    (item.FindControl("a_save_job") as HtmlAnchor).Attributes.Add("onclick", "SaveJob_Main('" + Enc_JobId + "',this)");
                }
                Session["JobsResultFound"] = "1";
            }
            else
            {
                div_nojobs.Visible = true;
                Session["JobsResultFound"] = "0";
            }
        }
        catch (Exception)
        {
        }
    }

    public string DecryptFilters(string filter)
    {
        string _filter = "";
        try
        {
            for (int i = 0; i < filter.Split(',').Length; i++)
            {
                if (_filter == "")
                {
                    _filter = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(filter.Split(',')[i]), Trabau_Keys.Filter_Key);
                }
                else
                {
                    _filter = _filter + "," + EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(filter.Split(',')[i]), Trabau_Keys.Filter_Key);
                }
            }
        }
        catch (Exception ex)
        {
        }

        return _filter;
    }
}