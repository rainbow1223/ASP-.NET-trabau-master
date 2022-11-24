using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabauClassLibrary.DLL.SignUp
{
    public class DLL_Registration : dbCon
    {
        public DataSet CheckEmailId(string EmailId, int TimeOut)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_CheckEmailId");
                dbc.Parameters.Add(new SqlParameter("EmailId", EmailId));
                dbc.CommandTimeout = TimeOut;
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetCountryList()
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_GetCountryList");
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public List<string> GetSkills(string Name)
        {
            List<string> skills = new List<string>();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_Registration_GetSkills");
                dbc.Parameters.Add(new SqlParameter("Name", Name));
                skills = db.ExecuteDataSet(dbc).Tables[0].AsEnumerable()
                            .Select(r => r.Field<string>(0))
                            .ToList();
            }
            catch (Exception ex)
            {
                skills = null;
            }

            return skills;
        }


        public DataSet User_Pre_SignUp(string FirstName, string LastName, string EmailId, string UserId, string Password, int CountryId,
            string RegistrationType, string IPAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_Pre_Registration");
                dbc.Parameters.Add(new SqlParameter("FirstName", FirstName));
                dbc.Parameters.Add(new SqlParameter("LastName", LastName));
                dbc.Parameters.Add(new SqlParameter("EmailId", EmailId));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Password", Password));
                dbc.Parameters.Add(new SqlParameter("CountryId", CountryId));
                dbc.Parameters.Add(new SqlParameter("RegistrationType", RegistrationType));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetServicesList(int ServiceId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_GetServicesList");
                dbc.Parameters.Add(new SqlParameter("ServiceId", ServiceId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetSkillsList(string SkillType = "Empty")
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_GetSkillsList");
                dbc.Parameters.Add(new SqlParameter("SkillType", SkillType));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet GetExperienceLevels()
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_Registration_GetExperienceLevels");
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet User_SignUp(Int64 UserId, int ServiceId, string Services_Type_XML, string Skills, int ExperienceLevelId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_Registration");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ServiceId", ServiceId));
                dbc.Parameters.Add(new SqlParameter("Services_Type_XML", Services_Type_XML));
                dbc.Parameters.Add(new SqlParameter("Skills", Skills));
                dbc.Parameters.Add(new SqlParameter("ExperienceLevelId", ExperienceLevelId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string UpdateUserEducation(string SchoolName, int YearFrom, int YearTo, string Degree, string AreaOfStudy, string Description,
            Int64 UserId, int EducationId)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_UpdateUserEducation");
                dbc.Parameters.Add(new SqlParameter("SchoolName", SchoolName));
                dbc.Parameters.Add(new SqlParameter("YearFrom", YearFrom));
                dbc.Parameters.Add(new SqlParameter("YearTo", YearTo));
                dbc.Parameters.Add(new SqlParameter("Degree", Degree));
                dbc.Parameters.Add(new SqlParameter("AreaOfStudy", AreaOfStudy));
                dbc.Parameters.Add(new SqlParameter("Description", Description));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("EducationId", EducationId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating Education details, please refresh and try again";
            }

            return val;
        }


        public DataSet GetUserEducationDetails(Int64 UserId, int EducationId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetUserEducationDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("EducationId", EducationId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetEmploymentRoles()
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Employment_GetRoles");
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string UpdateUserEmployment(string CompanyName, string CityName, int CountryId, string Title, int RoleId, int PeriodFrom_Month,
            int PeriodFrom_Year, int Period_To_Month, int Period_To_Year, bool WorkingHere, string Description, Int64 UserId, int EmploymentId)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_UpdateUserEmployment");
                dbc.Parameters.Add(new SqlParameter("CompanyName", CompanyName));
                dbc.Parameters.Add(new SqlParameter("CityName", CityName));
                dbc.Parameters.Add(new SqlParameter("CountryId", CountryId));
                dbc.Parameters.Add(new SqlParameter("Title", Title));
                dbc.Parameters.Add(new SqlParameter("RoleId", RoleId));
                dbc.Parameters.Add(new SqlParameter("PeriodFrom_Month", PeriodFrom_Month));
                dbc.Parameters.Add(new SqlParameter("PeriodFrom_Year", PeriodFrom_Year));
                dbc.Parameters.Add(new SqlParameter("Period_To_Month", Period_To_Month));
                dbc.Parameters.Add(new SqlParameter("Period_To_Year", Period_To_Year));
                dbc.Parameters.Add(new SqlParameter("WorkingHere", WorkingHere));
                dbc.Parameters.Add(new SqlParameter("Description", Description));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("EmploymentId", EmploymentId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating Education details, please refresh and try again";
            }

            return val;
        }


        public DataSet GetUserEmploymentDetails(Int64 UserId, int EmploymentId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetUserEmploymentDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("EmploymentId", EmploymentId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetCities(string Prefix)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetCities");
                dbc.Parameters.Add(new SqlParameter("Prefix", Prefix));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public string UpdateUserProfile_Step2(Int64 UserId, string Title, string Overview, string EnglishProficiency, string Address, int CityId,
            string PostalCode)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_UpdateUserProfile_Step2");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("Title", Title));
                dbc.Parameters.Add(new SqlParameter("Overview", Overview));
                dbc.Parameters.Add(new SqlParameter("EnglishProficiency", EnglishProficiency));
                dbc.Parameters.Add(new SqlParameter("Address", Address));
                dbc.Parameters.Add(new SqlParameter("CityId", CityId));
                dbc.Parameters.Add(new SqlParameter("PostalCode", PostalCode));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating Profile details, please refresh and try again";
            }

            return val;
        }


        public string CheckProfile_Step(Int64 UserId)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_CheckProfile_Step");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "0";
            }

            return val;
        }


        public DataSet LoadProfileDetails(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_LoadProfileDetails");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }



        public DataSet GetEmployeeNumberDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_GetEmployeeNumberDetails");
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet GetDepartments()
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_SignUp_GetDepartments");
                ds = db.ExecuteDataSet(dbc);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public string UpdateUserCompanyDetails(int EmployeeNumbersId, int DepartmentId, int CountryId, string PhoneNumber
            , Int64 UserId, int CategoryId, string MinutesCategory, int Step)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_UpdateUserCompanyDetails");
                dbc.Parameters.Add(new SqlParameter("EmployeeNumbersId", EmployeeNumbersId));
                dbc.Parameters.Add(new SqlParameter("DepartmentId", DepartmentId));
                dbc.Parameters.Add(new SqlParameter("CountryId", CountryId));
                dbc.Parameters.Add(new SqlParameter("PhoneNumber", PhoneNumber));
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("CategoryId", CategoryId));
                dbc.Parameters.Add(new SqlParameter("MinutesCategory", MinutesCategory));
                dbc.Parameters.Add(new SqlParameter("Step", Step));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating company details, please refresh and try again";
            }

            return val;
        }


        public string GetCompanyUpdateStep(Int64 UserId)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("SP_Trabau_Check_CompanyUpdateStep");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "1";
            }

            return val;
        }


        public string UpdateUserProfilePic(Int64 UserId, byte[] ProfilePicture)
        {
            string val = "";
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_UpdateUserProfilePicture");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
                dbc.Parameters.Add(new SqlParameter("ProfilePicture", ProfilePicture));
                val = db.ExecuteScalar(dbc).ToString();
            }
            catch (Exception ex)
            {
                val = "error:Error while updating Profile Picture, please refresh and try again";
            }

            return val;
        }


    }
}
