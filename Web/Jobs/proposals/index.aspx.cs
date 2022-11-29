using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_proposals_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetSubmittedProposals();
        }
    }

    public void GetSubmittedProposals()
    {
        try
        {
            string UserID = Session["Trabau_UserId"].ToString();
            searchjob obj = new searchjob();
            DataSet ds_prop = obj.GetSubmittedProposals(Int64.Parse(UserID));
            //var lst_prop = ds_prop.Tables[0].ToDynamic().Select(x => new
            //{
            //    x.ProposalId
            //});
            rProposals.DataSource = ds_prop;
            rProposals.DataBind();

            foreach (RepeaterItem item in rProposals.Items)
            {
                string ProposalId = (item.FindControl("lblProposalId") as Label).Text;
                ProposalId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptString(ProposalId, Trabau_Keys.Job_Key));
                (item.FindControl("a_viewjob") as HtmlAnchor).HRef = "viewproposal.aspx?proposal=" + ProposalId;
                (item.FindControl("btnRequestForInterview") as HtmlAnchor).Attributes.Add("data", ProposalId);
            }
        }
        catch (Exception)
        {
        }
    }


    [WebMethod(EnableSession = true)]
    public static string InterviewRequest(string InterviewData)
    {
        string val = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();

            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                if (HttpContext.Current.Session["Trabau_UserType"].ToString() == "W")
                {
                    InterviewData = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(InterviewData), Trabau_Keys.Job_Key);
                    string ProposalId = InterviewData;
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                    string IPAddress = HttpContext.Current.Request.UserHostAddress;

                    jobposting obj = new jobposting();
                    DataSet ds_response = obj.InterviewRequest(Int32.Parse(ProposalId), Int64.Parse(UserID), IPAddress);
                    string response = "error";
                    string message = "Error while generating interview request, please refresh and try again";
                    if (ds_response != null)
                    {
                        if (ds_response.Tables.Count > 0)
                        {
                            if (ds_response.Tables[0].Rows.Count > 0)
                            {
                                response = ds_response.Tables[0].Rows[0]["Response"].ToString();
                                message = ds_response.Tables[0].Rows[0]["Message"].ToString();
                                //string Freelancer_Name = ds_response.Tables[0].Rows[0]["Name"].ToString();
                                //string JobTitle = ds_response.Tables[0].Rows[0]["JobTitle"].ToString();
                                //string EmailId = ds_response.Tables[0].Rows[0]["EmailId"].ToString();

                                if (response == "success")
                                {
                                    try
                                    {
                                        //Emailer obj_email = new Emailer();
                                        //string ClientName = HttpContext.Current.Session["Trabau_FirstName"].ToString();
                                        //string body = "Dear " + Freelancer_Name + ",<br/><br/>You are hired by " + ClientName + " for the job " + JobTitle;

                                        //obj_email.SendEmail(EmailId, "", "", "Trabau Hiring", "You are hired - Trabau", body, null);



                                        //string client_body = "Dear " + ClientName + ",<br/><br/>You have hired " + Freelancer_Name + " (freelancer) for the job " + JobTitle;
                                        //string ClientEmailId = HttpContext.Current.Session["Trabau_EmailId"].ToString();
                                        //obj_email.SendEmail(ClientEmailId, "", "", "Trabau Hiring", "You have hired - Trabau", client_body, null);

                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                        }
                    }


                    detail.Add("action_response", response);
                    detail.Add("action_message", message);

                    detail.Add("response", "ok");
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("action_response", "");
                    detail.Add("action_message", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("action_response", "");
                detail.Add("action_message", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            val = serial.Serialize(details);

        }
        catch (Exception ex)
        {
            val = "";
        }
        return val;
    }
}