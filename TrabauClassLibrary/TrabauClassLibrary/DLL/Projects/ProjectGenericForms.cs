using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using TrabauClassLibrary.DLL.Models;
using TrabauClassLibrary.DLL.Projects.Models;

namespace TrabauClassLibrary.DLL.Projects
{
    public class ProjectGenericForms : dbCon
    {
        public static List<propProjectData> Parse(string data)
        {
            var lstData = JsonConvert.DeserializeObject<List<propProjectData>>(data);
            lstData = lstData.Select(x => new propProjectData
            {
                key = x.key,
                value = MiscFunctions.Base64EncodingMethod(x.value)
            }).ToList();
            return lstData;
        }
        public List<dynamic> GetNavigationTabs(Int64 UserId, int NavigationId)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_ProjectNav_GetNavigationTabs");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("NavigationId", NavigationId));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }


        public NavigationTabFields_Model GetNavigationTabFields(Int64 UserId, int TabId, int ProjectID, int NavigationId, int DataIndex, string uniquePropertyKey)
        {
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_ProjectNav_GetNavigationTabFields");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("TabId", TabId));
                dbc.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                dbc.Parameters.Add(new SqlParameter("NavigationId", NavigationId));
                dbc.Parameters.Add(new SqlParameter("DataIndex", DataIndex));
                dbc.Parameters.Add(new SqlParameter("UniquePropertyKey", uniquePropertyKey));
                DataSet ds = db.ExecuteDataSet(dbc);

                return new NavigationTabFields_Model
                {
                    fields = ds.Tables[0].ToDynamic(),
                    fieldItems = ds.Tables[1].ToDynamic()
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<dynamic> GetNavigation_TabGridColumns(Int64 UserId, int TabDetailsId, int ProjectID, int NavigationId = 0, string dataParameter = "")
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_ProjectNav_GetNavigation_TabGridColumns");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("TabDetailsId", TabDetailsId));
                dbc.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                dbc.Parameters.Add(new SqlParameter("dataParameter", dataParameter));
                dbc.Parameters.Add(new SqlParameter("NavigationId", NavigationId));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }


        public List<dynamic> SaveTabData(Int64 UserId, string TabData, string TabData_Table, string ItemType, string TabFiles_XML, string Files_TabDetailsId,
            int ProjectID, int DataIndex, string uniquePropertyKey, string parentUniquePropertyKey)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_Projects_SaveTabData");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("TabData", TabData));
                dbc.Parameters.Add(new SqlParameter("TabData_Table", TabData_Table));
                dbc.Parameters.Add(new SqlParameter("ItemType", ItemType));
                dbc.Parameters.Add(new SqlParameter("TabFiles_XML", TabFiles_XML));
                dbc.Parameters.Add(new SqlParameter("Files_TabDetailsId", Files_TabDetailsId));
                dbc.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                dbc.Parameters.Add(new SqlParameter("DataIndex", DataIndex));
                dbc.Parameters.Add(new SqlParameter("UniquePropertyKey", uniquePropertyKey));
                dbc.Parameters.Add(new SqlParameter("ParentUniquePropertyKey", parentUniquePropertyKey));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }


        public DataTable GetTabFileDetails(Int64 UserId, int TabDetailsId, int ProjectID)
        {
            DataTable dt = new DataTable();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_Projects_GetTabFileDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("TabDetailsId", TabDetailsId));
                dbc.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                dt = db.ExecuteDataSet(dbc).Tables[0];
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }

        public DataTable GetTabFile(Int64 UserId, string file_key)
        {
            DataTable dt = new DataTable();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_Projects_GetTabFile");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("file_key", file_key));
                dt = db.ExecuteDataSet(dbc).Tables[0];
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }


        public List<dynamic> GetTabFieldFullHelpText(Int64 UserId, int TabDetailsId)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_ProjectNav_GetTabFieldFullHelpText");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("TabDetailsId", TabDetailsId));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }


        public List<dynamic> GetMoreFileDescription(Int64 UserId, string fileKey)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_ProjectNav_GetMoreFileDescription");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("fileKey", fileKey));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }

        public List<dynamic> GetAllNavFieldsDefinitions(Int64 UserId)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_ProjectNav_GetAllNavFieldsDefinitions");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }

        public List<dynamic> GetFieldIdForChangeEvent(Int64 UserId, int ProjectID, int TabDetailsId, string Param1 = null)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_ProjectNav_GetMultiFieldIdForChangeEvent");
                dbc.Parameters.Add(new SqlParameter("TabDetailsId", TabDetailsId));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                dbc.Parameters.Add(new SqlParameter("Param1", Param1));
                lst = db.ExecuteDataSet(dbc).Tables[0].ToDynamic();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }

        public List<dynamic> GetDynamicGridAdditionalFields(int TabDetailsId)
        {
            List<dynamic> lst = new List<dynamic>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_ProjectNav_GetDynamicGridAdditionalFields");
                dbc.Parameters.Add(new SqlParameter("TabDetailsId", TabDetailsId));
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
