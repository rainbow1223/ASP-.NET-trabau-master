using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Admin;

/// <summary>
/// Summary description for services
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class services : System.Web.Services.WebService
{

    public services()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetCityDataForDataTable()
    {
        HttpContext context = HttpContext.Current;
        try
        {
            //List of Column shown in the Table (user for finding the name of column on Sorting)  
            List<string> columns = new List<string>();
            columns.Add("CityName");
            columns.Add("StateName");
            columns.Add("CountryName");
            columns.Add("IsActive");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            //Get the Data from the Database  

            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CityMaster obj = new CityMaster();
            DataSet dscities = obj.GetCities(Int64.Parse(UserId), searchby, OffsetValue, PagingSize, sortColumn, sortDirection);

            var cities = dscities.Tables[0].ToList<propCityMaster>().Select(x => new propCityMaster
            {
                CityName = x.CityName,
                StateName = x.StateName,
                CountryName = x.CountryName,
                IsActive = x.IsActive,
                Id_Enc = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.Id.ToString(), Trabau_Keys.Admin_Key))
            }).ToList();
            Int32 recordTotal = Int32.Parse(dscities.Tables[1].Rows[0]["Total"].ToString());
            Int32 recordFiltered = recordTotal;

            var _result = new { draw = ajaxDraw, data = cities, recordsTotal = recordFiltered, recordsFiltered = recordFiltered };
            //string json = JsonConvert.SerializeObject(_result);

            //return json.ToString();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(_result);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(strJSON);
        }
        catch (Exception ex)
        {
        }
    }



    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetCountryDataForDataTable()
    {
        HttpContext context = HttpContext.Current;
        try
        {
            //List of Column shown in the Table (user for finding the name of column on Sorting)  
            List<string> columns = new List<string>();
            columns.Add("CountryName");
            columns.Add("CountryCode");
            columns.Add("CountryPrefix");
            columns.Add("TimeZone");
            columns.Add("IsActive");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            //Get the Data from the Database  

            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CountryMaster obj = new CountryMaster();
            DataSet ds_countries = obj.GetCountries(Int64.Parse(UserId), searchby, OffsetValue, PagingSize, sortColumn, sortDirection);

            var countries = ds_countries.Tables[0].ToList<propCountryMaster>().Select(x => new propCountryMaster
            {
                CountryName = x.CountryName,
                CountryCode = x.CountryCode,
                CountryPrefix = x.CountryPrefix,
                TimeZone = x.TimeZone,
                TimeZoneDetails = x.TimeZoneDetails,
                IsActive = x.IsActive,
                CountryId_Enc = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CountryId.ToString(), Trabau_Keys.Admin_Key))
            }).ToList();
            Int32 recordTotal = Int32.Parse(ds_countries.Tables[1].Rows[0]["Total"].ToString());
            Int32 recordFiltered = recordTotal;

            var _result = new { draw = ajaxDraw, data = countries, recordsTotal = recordFiltered, recordsFiltered = recordFiltered };
            //string json = JsonConvert.SerializeObject(_result);

            //return json.ToString();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(_result);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(strJSON);
        }
        catch (Exception ex)
        {
        }
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetStateDataForDataTable()
    {
        HttpContext context = HttpContext.Current;
        try
        {
            //List of Column shown in the Table (user for finding the name of column on Sorting)  
            List<string> columns = new List<string>();
            columns.Add("StateName");
            columns.Add("CountryName");
            columns.Add("IsActive");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            //Get the Data from the Database  

            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            StateMaster obj = new StateMaster();
            DataSet ds_countries = obj.GetStates(Int64.Parse(UserId), searchby, OffsetValue, PagingSize, sortColumn, sortDirection);

            var countries = ds_countries.Tables[0].ToList<propStateMaster>().Select(x => new propStateMaster
            {
                StateName = x.StateName,
                StateId = x.StateId,
                CountryName = x.CountryName,
                IsActive = x.IsActive,
                StateId_Enc = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.StateId.ToString(), Trabau_Keys.Admin_Key))
            }).ToList();
            Int32 recordTotal = Int32.Parse(ds_countries.Tables[1].Rows[0]["Total"].ToString());
            Int32 recordFiltered = recordTotal;

            var _result = new { draw = ajaxDraw, data = countries, recordsTotal = recordFiltered, recordsFiltered = recordFiltered };
            //string json = JsonConvert.SerializeObject(_result);

            //return json.ToString();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(_result);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(strJSON);
        }
        catch (Exception ex)
        {
        }
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetRoleDataForDataTable()
    {
        HttpContext context = HttpContext.Current;
        try
        {
            //List of Column shown in the Table (user for finding the name of column on Sorting)  
            List<string> columns = new List<string>();
            columns.Add("RoleName");
            columns.Add("IsActive");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            //Get the Data from the Database  

            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            RoleMaster obj = new RoleMaster();
            DataSet ds_roles = obj.GetRoles(Int64.Parse(UserId), searchby, OffsetValue, PagingSize, sortColumn, sortDirection);

            var roles = ds_roles.Tables[0].ToList<propRoleMaster>().Select(x => new propRoleMaster
            {
                RoleName = x.RoleName,
                RoleId = x.RoleId,
                IsActive = x.IsActive,
                RoleId_Enc = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.RoleId.ToString(), Trabau_Keys.Admin_Key))
            }).ToList();
            Int32 recordTotal = Int32.Parse(ds_roles.Tables[1].Rows[0]["Total"].ToString());
            Int32 recordFiltered = recordTotal;

            var _result = new { draw = ajaxDraw, data = roles, recordsTotal = recordFiltered, recordsFiltered = recordFiltered };
            //string json = JsonConvert.SerializeObject(_result);

            //return json.ToString();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(_result);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(strJSON);
        }
        catch (Exception ex)
        {
        }
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetSkillsDataForDataTable()
    {
        HttpContext context = HttpContext.Current;
        try
        {
            //List of Column shown in the Table (user for finding the name of column on Sorting)  
            List<string> columns = new List<string>();
            columns.Add("SkillName");
            columns.Add("SkillType");
            columns.Add("IsActive");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            //Get the Data from the Database  

            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            SkillMaster obj = new SkillMaster();
            DataSet ds_skills = obj.GetSkills(Int64.Parse(UserId), searchby, OffsetValue, PagingSize, sortColumn, sortDirection);

            var skills = ds_skills.Tables[0].ToList<propSkillMaster>().Select(x => new propSkillMaster
            {
                SkillName = x.SkillName,
                SkillId = x.SkillId,
                SkillType = x.SkillType,
                IsActive = x.IsActive,
                SkillId_Enc = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.SkillId.ToString(), Trabau_Keys.Admin_Key))
            }).ToList();
            Int32 recordTotal = Int32.Parse(ds_skills.Tables[1].Rows[0]["Total"].ToString());
            Int32 recordFiltered = recordTotal;

            var _result = new { draw = ajaxDraw, data = skills, recordsTotal = recordFiltered, recordsFiltered = recordFiltered };
            //string json = JsonConvert.SerializeObject(_result);

            //return json.ToString();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(_result);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(strJSON);
        }
        catch (Exception ex)
        {
        }
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDocumentDataForDataTable()
    {
        HttpContext context = HttpContext.Current;
        try
        {
            //List of Column shown in the Table (user for finding the name of column on Sorting)  
            List<string> columns = new List<string>();
            columns.Add("DocumentName");
            columns.Add("IsActive");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            //Get the Data from the Database  

            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            DocumentMaster obj = new DocumentMaster();
            DataSet ds_docs = obj.GetAllDocuments(Int64.Parse(UserId), searchby, OffsetValue, PagingSize, sortColumn, sortDirection);

            var docs = ds_docs.Tables[0].ToList<propDocumentMaster>().Select(x => new propDocumentMaster
            {
                DocumentName = x.DocumentName,
                DocumentId = x.DocumentId,
                IsActive = x.IsActive,
                DocumentId_Enc = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.DocumentId.ToString(), Trabau_Keys.Admin_Key))
            }).ToList();
            Int32 recordTotal = Int32.Parse(ds_docs.Tables[1].Rows[0]["Total"].ToString());
            Int32 recordFiltered = recordTotal;

            var _result = new { draw = ajaxDraw, data = docs, recordsTotal = recordFiltered, recordsFiltered = recordFiltered };
            //string json = JsonConvert.SerializeObject(_result);

            //return json.ToString();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(_result);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(strJSON);
        }
        catch (Exception ex)
        {
        }
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetServicesCategoryDataForDataTable()
    {
        HttpContext context = HttpContext.Current;
        try
        {
            //List of Column shown in the Table (user for finding the name of column on Sorting)  
            List<string> columns = new List<string>();
            columns.Add("ServiceCategoryName");
            columns.Add("CategoryType");
            columns.Add("IsActive");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            //Get the Data from the Database  

            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            CategoryMaster obj = new CategoryMaster();
            DataSet ds_categories = obj.GetCategories(Int64.Parse(UserId), searchby, OffsetValue, PagingSize, sortColumn, sortDirection);

            var categories = ds_categories.Tables[0].ToList<propCategoryMaster>().Select(x => new propCategoryMaster
            {
                ServiceCategoryName = x.ServiceCategoryName,
                ServiceCategoryId = x.ServiceCategoryId,
                CategoryType = x.CategoryType,
                IsActive = x.IsActive,
                ServiceCategoryId_Enc = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.ServiceCategoryId.ToString(), Trabau_Keys.Admin_Key))
            }).ToList();
            Int32 recordTotal = Int32.Parse(ds_categories.Tables[1].Rows[0]["Total"].ToString());
            Int32 recordFiltered = recordTotal;

            var _result = new { draw = ajaxDraw, data = categories, recordsTotal = recordFiltered, recordsFiltered = recordFiltered };
            //string json = JsonConvert.SerializeObject(_result);

            //return json.ToString();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(_result);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(strJSON);
        }
        catch (Exception ex)
        {
        }
    }



    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetGenericCatgeoryDataForDataTable()
    {
        HttpContext context = HttpContext.Current;
        try
        {
            //List of Column shown in the Table (user for finding the name of column on Sorting)  
            List<string> columns = new List<string>();
            columns.Add("CategoryName");
            columns.Add("CategoryType");
            columns.Add("IsActive");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            //Get the Data from the Database  

            string UserId = HttpContext.Current.Session["Trabau_UserId"].ToString();
            GenericCategoryMaster obj = new GenericCategoryMaster();
            DataSet ds_cat = obj.GetGenericCategories(Int64.Parse(UserId), searchby, OffsetValue, PagingSize, sortColumn, sortDirection);

            var categories = ds_cat.Tables[0].ToList<propGenericCategoryMaster>().Select(x => new propGenericCategoryMaster
            {
                CategoryName = x.CategoryName,
                CategoryId = x.CategoryId,
                CategoryType = x.CategoryType,
                IsActive = x.IsActive,
                CategoryId_Enc = EncyptSalt.Base64Encode(EncyptSalt.EncryptString(x.CategoryId.ToString(), Trabau_Keys.Admin_Key))
            }).ToList();
            Int32 recordTotal = Int32.Parse(ds_cat.Tables[1].Rows[0]["Total"].ToString());
            Int32 recordFiltered = recordTotal;

            var _result = new { draw = ajaxDraw, data = categories, recordsTotal = recordFiltered, recordsFiltered = recordFiltered };
            //string json = JsonConvert.SerializeObject(_result);

            //return json.ToString();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(_result);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(strJSON);
        }
        catch (Exception ex)
        {
        }
    }
}

public class propCityMaster
{
    public string Id_Enc { get; set; }
    public int Id { get; set; }
    public string CityName { get; set; }
    public string StateName { get; set; }
    public string CountryName { get; set; }
    public bool IsActive { get; set; }
}

public class propCountryMaster
{
    public string CountryId_Enc { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public string CountryCode { get; set; }
    public string CountryPrefix { get; set; }
    public string TimeZone { get; set; }
    public string TimeZoneDetails { get; set; }
    public bool IsActive { get; set; }
}

public class propStateMaster
{
    public string StateId_Enc { get; set; }
    public int StateId { get; set; }
    public string CountryName { get; set; }
    public string StateName { get; set; }
    public bool IsActive { get; set; }
}

public class propRoleMaster
{
    public string RoleId_Enc { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public bool IsActive { get; set; }
}

public class propDocumentMaster
{
    public string DocumentId_Enc { get; set; }
    public int DocumentId { get; set; }
    public string DocumentName { get; set; }
    public bool IsActive { get; set; }
}

public class propSkillMaster
{
    public string SkillId_Enc { get; set; }
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    public string SkillType { get; set; }
    public bool IsActive { get; set; }
}

public class propCategoryMaster
{
    public string ServiceCategoryId_Enc { get; set; }
    public int ServiceCategoryId { get; set; }
    public string ServiceCategoryName { get; set; }
    public string CategoryType { get; set; }
    public bool IsActive { get; set; }
}


public class propGenericCategoryMaster
{
    public string CategoryId_Enc { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryType { get; set; }
    public bool IsActive { get; set; }
}
