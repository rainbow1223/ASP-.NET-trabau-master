using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Job;

public partial class Jobs_searchjobs_UserControls_wucJobAdditionalFiles : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["JobPost_Project_Files"] = null;
            GetAdditionalFiles();
        }
    }

    public void GetAdditionalFiles()
    {
        try
        {
            string JobId = hfAdditionalFiles_JobId.Value;
            JobId = EncyptSalt.DecryptString(EncyptSalt.Base64Decode(JobId), Trabau_Keys.Job_Key);
            searchjob obj = new searchjob();
            string UserID = Session["Trabau_UserId"].ToString();
            DataSet ds = obj.GetAdditionalFileDetails(Int32.Parse(UserID), Int32.Parse(JobId));

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

                        if (Session["JobPost_Project_Files"] == null)
                        {
                            Session["JobPost_Project_Files"] = dt_files;
                        }
                        else
                        {
                            DataTable dtAll = Session["JobPost_Project_Files"] as DataTable;
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