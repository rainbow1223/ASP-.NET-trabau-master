using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary.DLL.Job;

public partial class UserControls_wucSearchJobs_Op : System.Web.UI.UserControl
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
            string UserID = "0";
            try
            {
                if (Session["Trabau_UserId"] != null)
                {
                    UserID = Session["Trabau_UserId"].ToString();
                }
                else
                {
                    div_Register.Visible = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            DataSet ds_jobs = obj.SearchJobs_Op(Int64.Parse(UserID));
            rjobs.DataSource = ds_jobs.Tables[0];
            rjobs.DataBind();
            if (rjobs.Items.Count > 0)
            {
                DataTable dtExpertise = ds_jobs.Tables[1];

                foreach (RepeaterItem item in rjobs.Items)
                {
                    Repeater rExpertise = (item.FindControl("rExpertise") as Repeater);

                    string JobId = (item.FindControl("lblJobId") as Label).Text;
                    //  string Enc_JobId = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(JobId, Trabau_Keys.Job_Key));
                    //  (item.FindControl("lbl_JobId") as Label).Text = Enc_JobId;
                    DataView dvExp = dtExpertise.DefaultView;
                    dvExp.RowFilter = "JobId=" + JobId;
                    rExpertise.DataSource = dvExp;
                    rExpertise.DataBind();

                    //  (item.FindControl("a_save_job") as HtmlAnchor).Attributes.Add("onclick", "SaveJob_Main('" + Enc_JobId + "',this)");
                }
                Session["JobsResultFound"] = "1";
            }
            else
            {
                //div_nojobs.Visible = true;
                Session["JobsResultFound"] = "0";
            }
        }
        catch (Exception)
        {
        }
    }
}