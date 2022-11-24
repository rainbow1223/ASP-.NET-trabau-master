using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabauClassLibrary.DLL.Projects
{
    public class Project : dbCon
    {
        public DataSet GetUserProjects(Int64 UserId, string Status)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Project_GetUserProjects");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Status", Status));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public List<dynamic> GetUserProjectsCount(Int64 UserId)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Project_GetUserProjectsCount");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }


        public List<dynamic> SaveNewProject(int ProjectID, Int64 UserId, string ProjectName, DateTime StartDate, DateTime EndDate, string Status, string IPAddress,
            string CompanyName, string ApplicationName, string ManagerName, string ProjectDescription, DateTime StartTime, DateTime EndTime, string BudgetType, int TotalHours,
            string AdditionalInformation, string XML_ProjectFiles, string ProjectType, string OtherProjectType, string OtherProjectType_Child1, string ProjectCommFunction, string OtherCommFunction)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_Projects_SaveNewProject");
                dbc.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ProjectName", ProjectName));
                dbc.Parameters.Add(new SqlParameter("StartDate", StartDate));
                dbc.Parameters.Add(new SqlParameter("EndDate", EndDate));
                dbc.Parameters.Add(new SqlParameter("Status", Status));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                dbc.Parameters.Add(new SqlParameter("CompanyName", CompanyName));
                dbc.Parameters.Add(new SqlParameter("ApplicationName", ApplicationName));
                dbc.Parameters.Add(new SqlParameter("ManagerName", ManagerName));
                dbc.Parameters.Add(new SqlParameter("ProjectDescription", ProjectDescription));
                dbc.Parameters.Add(new SqlParameter("StartTime", StartTime));
                dbc.Parameters.Add(new SqlParameter("EndTime", EndTime));
                dbc.Parameters.Add(new SqlParameter("BudgetType", BudgetType));
                dbc.Parameters.Add(new SqlParameter("TotalHours", TotalHours));
                dbc.Parameters.Add(new SqlParameter("AdditionalInformation", AdditionalInformation));
                dbc.Parameters.Add(new SqlParameter("XML_ProjectFiles", XML_ProjectFiles));
                dbc.Parameters.Add(new SqlParameter("ProjectType", ProjectType));
                dbc.Parameters.Add(new SqlParameter("OtherProjectType", OtherProjectType));
                dbc.Parameters.Add(new SqlParameter("OtherProjectType_Child1", OtherProjectType_Child1));
                dbc.Parameters.Add(new SqlParameter("ProjectCommFunction", ProjectCommFunction));
                dbc.Parameters.Add(new SqlParameter("OtherCommFunction", OtherCommFunction));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }


        public List<dynamic> GetCompanyList(Int64 UserId)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Project_GetCompanyList");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }

        public List<dynamic> GetProjectItems(Int64 UserId)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Project_GetProjectItems");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }


        public DataSet GetProjectDetails(Int64 UserId, int ProjectID)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Project_GetProjectDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public List<dynamic> GetActionDetails(Int64 UserId, int ProjectID)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Project_GetActionDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }


        public List<dynamic> GetControlDataSource(Int64 UserId, int ProjectID, string DataSource)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand(DataSource);
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }

        public void UpdateMenuEnc(Int64 UserId, string MenuXML)
        {
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Projects_UpdateMenuEnc");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("MenuXML", MenuXML));
                db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
