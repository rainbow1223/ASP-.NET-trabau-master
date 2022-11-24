using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabauClassLibrary.DLL
{
    class DLL_Social_Authentication : dbCon
    {
        public DataSet ValidateUser_Social(string SocialType, string IPAddress, string id, string email, string verified_email, string name,
            string given_name, string family_name, string picture, string locale, string AuthenticationType)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_Login_ValidateUser_Social");
                dbc.Parameters.Add(new SqlParameter("SocialType", SocialType));
                dbc.Parameters.Add(new SqlParameter("IPAddress", IPAddress));
                dbc.Parameters.Add(new SqlParameter("id", id));
                dbc.Parameters.Add(new SqlParameter("email", email));
                dbc.Parameters.Add(new SqlParameter("verified_email", verified_email));
                dbc.Parameters.Add(new SqlParameter("name", name));
                dbc.Parameters.Add(new SqlParameter("given_name", given_name));
                dbc.Parameters.Add(new SqlParameter("family_name", family_name));
                dbc.Parameters.Add(new SqlParameter("picture", picture));
                dbc.Parameters.Add(new SqlParameter("locale", locale));
                dbc.Parameters.Add(new SqlParameter("AuthenticationType", AuthenticationType));
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
