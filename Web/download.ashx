<%@ WebHandler Language="C#" Class="download" %>

using System;
using System.Web;
using System.Data;
using TrabauClassLibrary.DLL.Projects;

public class download : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (HttpContext.Current.Request.QueryString.Count == 1)
        {
            if (HttpContext.Current.Request.QueryString["path"] != null)
            {
                string download_file_key = HttpContext.Current.Session["download_file_key"].ToString();
                DataTable dt_files = new DataTable();
                if (HttpContext.Current.Session["ApplyPost_Project_Files"] != null)
                {
                    dt_files = HttpContext.Current.Session["ApplyPost_Project_Files"] as DataTable;
                }
                else if (HttpContext.Current.Session["JobPost_Project_Files"] != null)
                {
                    dt_files = HttpContext.Current.Session["JobPost_Project_Files"] as DataTable;
                }
                else if (HttpContext.Current.Session["NewProject_Files"] != null)
                {
                    dt_files = HttpContext.Current.Session["NewProject_Files"] as DataTable;
                }
                else if (HttpContext.Current.Session["TabProject_Files"] != null)
                {
                    dt_files = HttpContext.Current.Session["TabProject_Files"] as DataTable;
                }
                DataView dv_files = dt_files.DefaultView;
                dv_files.RowFilter = "file_key='" + download_file_key + "'";
                if (dv_files.Count > 0)
                {
                    byte[] file_bytes = Convert.FromBase64String(dv_files.ToTable().Rows[0]["file_bytes"].ToString());
                    string file_name = dv_files.ToTable().Rows[0]["file_name"].ToString();


                    HttpResponse response = context.Response;
                    response.Clear();
                    file_name = file_name.Replace(" ", "_");
                    response.AddHeader("Content-Disposition", "attachment; filename=" + file_name);
                    // Add a HTTP header to the output stream that contains the
                    // content length(File Size). This lets the browser know how much data is being transfered
                    response.AddHeader("Content-Length", file_bytes.Length.ToString());
                    // Set the HTTP MIME type of the output stream
                    response.ContentType = "application/octet-stream";
                    // Write the data out to the client.
                    response.BinaryWrite(file_bytes);
                }
            }
            else if (HttpContext.Current.Request.QueryString["key"] != null)
            {
                string download_file_key = HttpContext.Current.Request.QueryString["key"];
                ProjectGenericForms obj = new ProjectGenericForms();
                string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                DataTable dt_files = obj.GetTabFile(Int64.Parse(UserID), download_file_key);

                if (dt_files.Rows.Count > 0)
                {
                    byte[] file_bytes = (dt_files.Rows[0]["FileBytes"] as byte[]);
                    string file_name = dt_files.Rows[0]["FileName"].ToString();


                    HttpResponse response = context.Response;
                    response.Clear();
                    file_name = file_name.Replace(" ", "_");
                    response.AddHeader("Content-Disposition", "attachment; filename=" + file_name);
                    // Add a HTTP header to the output stream that contains the
                    // content length(File Size). This lets the browser know how much data is being transfered
                    response.AddHeader("Content-Length", file_bytes.Length.ToString());
                    // Set the HTTP MIME type of the output stream
                    response.ContentType = "application/octet-stream";
                    // Write the data out to the client.
                    response.BinaryWrite(file_bytes);
                }
            }
        }
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}