using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabauClassLibrary.DLL.profile.settings
{
    public class settings_changes : dbCon
    {
        public string CheckPreRegistrationStatus(Int64 UserId)
        {
            string val = "0:login.aspx";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_CheckPreRegistrationStatus");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "0:login.aspx";
            }

            return val;
        }

        public string UploadDocument(Int64 UserId, byte[] bytes, Int64 EntryBy, string IPAddress, int DocumentId, string DocumentType,
            string OtherDocType, string YouTubeVideoName)
        {
            string val = "error:Error while uploading document, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UploadDocument");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("bytes", bytes));
                dbc.Parameters.Add(new SqlParameter("EntryBy", EntryBy));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                dbc.Parameters.Add(new SqlParameter("DocumentId", DocumentId));
                dbc.Parameters.Add(new SqlParameter("DocumentType", DocumentType));
                dbc.Parameters.Add(new SqlParameter("OtherDocType", OtherDocType));
                dbc.Parameters.Add(new SqlParameter("YouTubeVideoName", YouTubeVideoName));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while uploading document, please refresh and try again";
            }

            return val;
        }

        public DataSet GetDocuments(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_GetDocumentsList");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetUploadedDocument(Int64 UserId, int DocumentId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_GetUploadedDocument");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("DocumentId", DocumentId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetIndvidualUploadedDocument(Int64 UserId, int UploadedDocId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_GetIndvidualUploadedDocument");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("UploadedDocId", UploadedDocId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string UploadDocumentVisibility(Int64 UserId, int UploadedDocId, string Visibility)
        {
            string val = "error:Error while changing document visibility, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_ChangeUploadedDocument");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Visibility", Visibility));
                dbc.Parameters.Add(new SqlParameter("UploadedDocId", UploadedDocId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while changing document visibility, please refresh and try again";
            }

            return val;
        }

        public DataSet GetContactInfo(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_GetContactInfo");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public string UpdateContactDetails(Int64 UserId, string _UserId, string FName, string LName, string EmailAddress)
        {
            string val = "error:Error while updating contact details, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdateContactDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("_UserId", _UserId));
                dbc.Parameters.Add(new SqlParameter("FName", FName));
                dbc.Parameters.Add(new SqlParameter("LName", LName));
                dbc.Parameters.Add(new SqlParameter("EmailAddress", EmailAddress));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating contact details, please refresh and try again";
            }

            return val;
        }


        public DataSet GetTimeZones(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_GetTimeZones");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string UpdateLocationDetails(Int64 UserId, int TimeZoneId, int CountryId, string Address, int CityId, string PostalCode,
            string CountryCode, string PhoneNo, string VATID, bool GoogleMapsLinkRequired)
        {
            string val = "error:Error while updating location details, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdateLocationDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("TimeZoneId", TimeZoneId));
                dbc.Parameters.Add(new SqlParameter("CountryId", CountryId));
                dbc.Parameters.Add(new SqlParameter("Address", Address));
                dbc.Parameters.Add(new SqlParameter("CityId", CityId));
                dbc.Parameters.Add(new SqlParameter("PostalCode", PostalCode));
                dbc.Parameters.Add(new SqlParameter("CountryCode", CountryCode));
                dbc.Parameters.Add(new SqlParameter("PhoneNo", PhoneNo));
                dbc.Parameters.Add(new SqlParameter("VATID", VATID));
                dbc.Parameters.Add(new SqlParameter("GoogleMapsLinkRequired", GoogleMapsLinkRequired));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating location details, please refresh and try again";
            }

            return val;
        }


        public DataSet LoadProfilePicture(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Settings_LoadProfilePicture");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet LoadCompanyLogo(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Settings_LoadCompanyLogo");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string RemoveUploadedDocument(Int64 UserId, int UploadedDocId)
        {
            string val = "error:Error while removing document, please refresh and try again";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_RemoveUploadedDocument");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("UploadedDocId", UploadedDocId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while removing document, please refresh and try again";
            }

            return val;
        }


        public DataSet GetSettingsNavigation(Int64 UserId, string UserType)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Settings_GetNavigation");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("UserType", UserType));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string UpdateCompanyLogo(Int64 UserId, byte[] CompanyLogo)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_UpdateCompanyLogo");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("CompanyLogo", CompanyLogo));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating Company Logo, please refresh and try again";
            }

            return val;
        }


        public string UpdateCompanyDetails(Int64 UserId, string CompanyName, string Website, string Tagline, string Description)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdateCompanyDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("CompanyName", CompanyName));
                dbc.Parameters.Add(new SqlParameter("Website", Website));
                dbc.Parameters.Add(new SqlParameter("Tagline", Tagline));
                dbc.Parameters.Add(new SqlParameter("Description", Description));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating Company Details, please refresh and try again";
            }

            return val;
        }


        public DataSet GetUserAccounts(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetUserAccounts");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet CreateNewSecondaryAccount(Int64 UserId, string Type, string IPAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_CreateNewSecondaryAccount");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Type", Type));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string UpdateProfileType(Int64 UserId, string ProfileType)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_UpdateProfileType");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ProfileType", ProfileType));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating Profile Type, please refresh and try again";
            }

            return val;
        }


        public string SetAsDefaultAccount(Int64 UserId)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_User_SetAsDefaultAccount");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating Profile Type, please refresh and try again";
            }

            return val;
        }
    }
}
