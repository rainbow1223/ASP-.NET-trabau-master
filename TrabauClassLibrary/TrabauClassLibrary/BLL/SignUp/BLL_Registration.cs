using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TrabauClassLibrary.DLL.SignUp;

namespace TrabauClassLibrary.BLL.SignUp
{
    public class BLL_Registration
    {
        public Tuple<List<dynamic>, string> GetServicesList(int ServiceId)
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.GetServicesList(ServiceId).Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

        public Tuple<List<dynamic>, string> GetSkillsList()
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.GetSkillsList().Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

        public Tuple<List<dynamic>, string> CheckEmailId(string EmailId, int TimeOut)
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.CheckEmailId(EmailId, TimeOut).Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

        public Tuple<List<dynamic>, string> User_Pre_SignUp(string FirstName, string LastName, string EmailId, string UserId, string Password, int CountryId,
            string RegistrationType, string IPAddress)
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.User_Pre_SignUp(FirstName, LastName, EmailId, UserId, Password, CountryId, RegistrationType, IPAddress).Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

        public Tuple<List<dynamic>, string> User_SignUp(Int64 UserId, int ServiceId, string Services_Type_XML, string Skills, int ExperienceLevelId)
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.User_SignUp(UserId, ServiceId, Services_Type_XML, Skills, ExperienceLevelId).Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

        public string UpdateUserEducation(string SchoolName, int YearFrom, int YearTo, string Degree, string AreaOfStudy, string Description,
            Int64 UserId, int EducationId)
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    string data = obj.UpdateUserEducation(SchoolName, YearFrom, YearTo, Degree, AreaOfStudy, Description, UserId, EducationId);
                    return data;
                }
            }
            catch (Exception ex)
            {

                return "error:Error while updating Education details, please refresh and try again";
            }
        }


        public Tuple<List<dynamic>, string> GetUserEducationDetails(Int64 UserId, int EducationId)
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.GetUserEducationDetails(UserId, EducationId).Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }


        public string UpdateUserEmployment(string CompanyName, string CityName, int CountryId, string Title, int RoleId, int PeriodFrom_Month,
            int PeriodFrom_Year, int Period_To_Month, int Period_To_Year, bool WorkingHere, string Description, Int64 UserId, int EmploymentId)
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    string data = obj.UpdateUserEmployment(CompanyName, CityName, CountryId, Title, RoleId, PeriodFrom_Month, PeriodFrom_Year, Period_To_Month,
                        Period_To_Year, WorkingHere, Description, UserId, EmploymentId);
                    return data;
                }
            }
            catch (Exception ex)
            {

                return "error:Error while updating Employment details, please refresh and try again";
            }
        }

        public Tuple<List<dynamic>, string> GetUserEmploymentDetails(Int64 UserId, int EmploymentId)
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.GetUserEmploymentDetails(UserId, EmploymentId).Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

        public List<string> GetCities(string Prefix)
        {
            try
            {
                List<string> obList = new List<string>();
                if (HttpContext.Current.Session["Trabau_Cached_Cities"] != null)
                {
                    obList = HttpContext.Current.Session["Trabau_Cached_Cities"] as List<string>;

                    List<string> filtered_data = obList.Where(x => x.ToLower().Contains(Prefix.ToLower())).ToList();
                    if (filtered_data.Count > 0)
                    {
                        return filtered_data;
                    }
                    else
                    {
                        obList = GetCities_DB(Prefix);
                        return obList;
                    }
                }
                else
                {
                    obList = GetCities_DB(Prefix);
                    return obList;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<string> GetCities_DB(string Prefix)
        {
            try
            {
                List<string> obList = new List<string>();
                string key = "T_city";
                using (DLL_Registration obj = new DLL_Registration())
                {
                    obList = obj.GetCities(Prefix).Tables[0].AsEnumerable()
                            .Select(r => r.Field<string>("CityName") + "::" + EncyptSalt.EncryptText(r.Field<int>("Id").ToString(), key))
                            .ToList();

                    if (HttpContext.Current.Session["Trabau_Cached_Cities"] == null)
                    {
                        HttpContext.Current.Session["Trabau_Cached_Cities"] = obList;
                    }
                    else
                    {
                        List<string> cached_cities = HttpContext.Current.Session["Trabau_Cached_Cities"] as List<string>;

                        cached_cities = cached_cities.Concat(obList).ToList();
                        HttpContext.Current.Session["Trabau_Cached_Cities"] = cached_cities;

                        obList = cached_cities.Where(x => x.ToLower().Contains(Prefix.ToLower())).ToList();
                    }
                    return obList;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public string UpdateUserProfile_Step2(Int64 UserId, string Title, string Overview, string EnglishProficiency, string Address, int CityId,
            string PostalCode)
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    string data = obj.UpdateUserProfile_Step2(UserId, Title, Overview, EnglishProficiency, Address, CityId, PostalCode);
                    return data;
                }
            }
            catch (Exception ex)
            {

                return "error:Error while updating Profile details, please refresh and try again";
            }
        }

        public bool CheckProfile_Step(Int64 UserId)
        {
            bool val = false;
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    string data = obj.CheckProfile_Step(UserId);
                    val = (data == "1" ? true : false);
                }
            }
            catch (Exception ex)
            {
                val = false;
            }
            return val;
        }


        public Tuple<List<dynamic>, string> LoadProfileDetails(Int64 UserId)
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.LoadProfileDetails(UserId).Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

        public Tuple<List<dynamic>, string> GetEmployeeNumberDetails()
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.GetEmployeeNumberDetails().Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

        public Tuple<List<dynamic>, string> GetDepartments()
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.GetDepartments().Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

        public Tuple<List<dynamic>, string> GetCountryList()
        {
            try
            {
                using (DLL_Registration obj = new DLL_Registration())
                {
                    List<dynamic> data = obj.GetCountryList().Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

    }
}
