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
    public class preferlist : dbCon
    {
        public DataSet GetPreferList(Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("pTrabau_GetPreferList");
                dbc.Parameters.Add(new SqlParameter("UserId", UserId));
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
