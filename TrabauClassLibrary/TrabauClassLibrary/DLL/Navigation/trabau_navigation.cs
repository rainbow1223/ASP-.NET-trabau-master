using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using TrabauClassLibrary.DLL.Projects;

namespace TrabauClassLibrary.DLL.Navigation
{
    public class trabau_navigation : dbCon
    {
        public List<dynamic> GetUserProjectNavigation(Int64 UserId, string Domain, int ProjectID, string ProjectID_Enc, string ProjectID_Enc_Edit,
            string ProjectID_Enc_View, int ParentId)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetUserProjectNavigation");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Domain", Domain));
                dbc.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                dbc.Parameters.Add(new SqlParameter("ProjectID_Enc", ProjectID_Enc));
                dbc.Parameters.Add(new SqlParameter("ProjectID_Enc_Edit", ProjectID_Enc_Edit));
                dbc.Parameters.Add(new SqlParameter("ProjectID_Enc_View", ProjectID_Enc_View));
                dbc.Parameters.Add(new SqlParameter("ParentId", ParentId));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }

        public List<dynamic> ExecuteProcedure(string ProcedureName, SqlParameter[] parameters)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand(ProcedureName);
                dbc.Parameters.AddRange(parameters);
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }
    }
}
