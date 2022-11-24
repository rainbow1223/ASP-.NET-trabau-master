using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabauClassLibrary.DLL.profile
{
    public class portfolio_changes : dbCon
    {
        public DataSet Get_Generic_Categories(string CategoryType)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_Get_Generic_Categories");
                dbc.Parameters.Add(new SqlParameter("CategoryType", CategoryType));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public string UpdatePortfolio(Int64 UserId, string Title, string CompletionDate, string Template, string Devices, string Mobile_P, string Mobile_PL,
            string Mobile_ADK, string Databases, string BSE, string ProjectURL, string ProjectDescription, DataTable dt_gallery, int PortfolioId)
        {
            string val = "error:Error while updating profile portfolio, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdatePortfolio");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Title", Title));
                dbc.Parameters.Add(new SqlParameter("CompletionDate", CompletionDate));
                dbc.Parameters.Add(new SqlParameter("Template", Template));
                dbc.Parameters.Add(new SqlParameter("Devices", Devices));
                dbc.Parameters.Add(new SqlParameter("Mobile_P", Mobile_P));
                dbc.Parameters.Add(new SqlParameter("Mobile_PL", Mobile_PL));
                dbc.Parameters.Add(new SqlParameter("Mobile_ADK", Mobile_ADK));
                dbc.Parameters.Add(new SqlParameter("Databases", Databases));
                dbc.Parameters.Add(new SqlParameter("BSE", BSE));
                dbc.Parameters.Add(new SqlParameter("ProjectURL", ProjectURL));
                dbc.Parameters.Add(new SqlParameter("ProjectDescription", ProjectDescription));
                dbc.Parameters.Add(new SqlParameter("XMLData_gallery", dt_gallery));
                dbc.Parameters.Add(new SqlParameter("PortfolioId", PortfolioId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating profile portfolio, please refresh and try again";
            }

            return val;
        }

        public DataSet GetPortfolioDetails(Int64 UserId, int PortfolioId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Profile_GetPortfolioDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("PortfolioId", PortfolioId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

    }
}
