using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabauClassLibrary.DLL;

namespace TrabauClassLibrary.BLL
{
    public class BLL_UserValidation
    {
        public Tuple<List<dynamic>, string> ValidateUser(string UserName, string Password, string IPAddress)
        {
            try
            {
                using (DLL_UserValidation obj = new DLL_UserValidation())
                {
                    List<dynamic> data = obj.ValidateUser(UserName, Password, IPAddress).Tables[0].ToDynamic();
                    return new Tuple<List<dynamic>, string>(data, "ok");
                }
            }
            catch (Exception ex)
            {

                return new Tuple<List<dynamic>, string>(null, ex.Message);
            }
        }

        public Tuple<List<dynamic>, string> RequestSendEmail(string UserName, string IPAddress)
        {
            try
            {
                using (DLL_UserValidation obj = new DLL_UserValidation())
                {
                    List<dynamic> data = obj.RequestSendEmail(UserName, IPAddress).Tables[0].ToDynamic();
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
