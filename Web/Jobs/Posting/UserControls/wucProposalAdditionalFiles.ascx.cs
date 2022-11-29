using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_Posting_UserControls_wucProposalAdditionalFiles : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ApplyPost_Project_Files"] = null;
            GetAdditionalFiles();
        }
    }

    public void GetAdditionalFiles()
    {
        try
        {
            string data = hfAdditionalFiles_ProposalId.Value;
            data = EncyptSalt.DecryptString(MiscFunctions.Base64DecodingMethod(data), Trabau_Keys.Job_Key);
            string ProposalId = data.Split('-')[0];
            string JobId = data.Split('-')[1];
            string UserID = Session["Trabau_UserId"].ToString();
            searchjob obj = new searchjob();
            DataSet ds = obj.GetProposal_AdditionalFileDetails(Int32.Parse(UserID), Int32.Parse(ProposalId), Int32.Parse(JobId));

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt_files = GetProjectFilesStructure();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dt_files.NewRow();
                            dr["file_name"] = ds.Tables[0].Rows[i]["file_name"].ToString();
                            dr["file_key"] = ds.Tables[0].Rows[i]["file_key"].ToString();
                            dr["file_bytes"] = Convert.ToBase64String((byte[])ds.Tables[0].Rows[i]["file_bytes"]);

                            dt_files.Rows.Add(dr);
                        }

                        if (Session["ApplyPost_Project_Files"] == null)
                        {
                            Session["ApplyPost_Project_Files"] = dt_files;
                        }
                        else
                        {
                            DataTable dtAll = Session["ApplyPost_Project_Files"] as DataTable;
                            for (int i = 0; i < dt_files.Rows.Count; i++)
                            {
                                string file_key = dt_files.Rows[i]["file_key"].ToString();
                                DataView dv_files = dtAll.DefaultView;
                                dv_files.RowFilter = "file_key='" + file_key + "'";
                                if (dv_files.Count == 0)
                                {
                                    dtAll.ImportRow(dt_files.Rows[i]);
                                }
                            }
                        }
                        rAdditionalFiles.DataSource = dt_files;
                        rAdditionalFiles.DataBind();

                        div_additional_files.Visible = true;
                    }
                }
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
}