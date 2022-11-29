using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_Posting_UserControls_wucProposalDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetProposalDetails();
        }
    }

    public void GetProposalDetails()
    {
        try
        {
            string data = lblProposalId.Text;
            data = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(data), Trabau_Keys.Job_Key);
            string ProposalId = data.Split('-')[0];
            string JobId = data.Split('-')[1];
            string UserID = Session["Trabau_UserId"].ToString();
            jobposting obj = new jobposting();
            DataSet ds_pd = obj.GetJobSubmittedProposalDetails(Int32.Parse(JobId), Int32.Parse(ProposalId), Int64.Parse(UserID));
            if (ds_pd != null)
            {
                if (ds_pd.Tables.Count > 0)
                {
                    if (ds_pd.Tables[0].Rows.Count > 0)
                    {
                        bool Hired = Convert.ToBoolean(ds_pd.Tables[0].Rows[0]["Hired"].ToString());
                        string HiredText = ds_pd.Tables[0].Rows[0]["HiredText"].ToString();
                        string DeclinedText = ds_pd.Tables[0].Rows[0]["DeclinedText"].ToString();
                        string DisabledClass = "cta-btn-sm disabled";
                        bool Declined = Convert.ToBoolean(ds_pd.Tables[0].Rows[0]["Declined"].ToString());
                        string BIDAmount = ds_pd.Tables[0].Rows[0]["BIDAmount"].ToString();
                        string JobBudgetValue = ds_pd.Tables[0].Rows[0]["JobBudgetValue"].ToString();
                        string Interview = ds_pd.Tables[0].Rows[0]["Interview"].ToString();

                        lbtnFlagForInterview.Visible = true;

                        if (Interview == "1")
                        {
                            lbtnFlagForInterview.InnerHtml = "<i class='fa fa-check' aria-hidden='true'></i> Identify For Interview";
                            lbtnFlagForInterview.Attributes.Add("class", DisabledClass);
                        }

                        lbtnHire.Visible = !Declined;
                        lbtnDecline.Visible = !Hired;
                        lbtnHire.InnerHtml = HiredText;
                        lbtnDecline.InnerHtml = DeclinedText;
                        if (Hired)
                        {
                            lbtnHire.Attributes.Add("class", DisabledClass);
                        }
                        else if (Declined)
                        {
                            lbtnDecline.Attributes.Add("class", DisabledClass);
                        }
                        ltrl_profile_name.Text = ds_pd.Tables[0].Rows[0]["Name"].ToString();
                        ltrl_profile_cityname.Text = ds_pd.Tables[0].Rows[0]["CityName"].ToString();
                        sCoverLetter.InnerHtml = ds_pd.Tables[0].Rows[0]["CoverLetter"].ToString();
                        divActions.Attributes.Add("data", lblProposalId.Text);

                        ltrlBudgetValue.Text = "$" + JobBudgetValue;
                        ltrlProposedBID.Text = "$" + BIDAmount;

                        try
                        {
                            rScreenQuestions.DataSource = ds_pd.Tables[1];
                            rScreenQuestions.DataBind();

                            if (rScreenQuestions.Items.Count > 0)
                            {
                                div_screening.Visible = true;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}