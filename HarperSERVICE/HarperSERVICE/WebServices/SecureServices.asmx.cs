using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SupportClasses;
using SFGWrapper;
using BusinessLogic;
using System.Xml;
using HarperCRYPTO;
using HarperACL;

namespace MemberServices
{
    //ALWAYS HASH
    //  - MEMBERID
    //  - CUSID
    //  - PASSWORD
    //  - CC DATA
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public partial class SecureServices : System.Web.Services.WebService
    {
        #region core methods
        
        [WebMethod]
        public BaseResponse UpdateUserName(string newusername, string oldusername, string hashed_password)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = new MembershipLogic().UpdateUsername(newusername, oldusername, Cryptography.DeHash(hashed_password, true), false);
            }
            catch (Exception ex)
            {
                EventLogger.LogError("SecureServices.UpdateUsername",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }

        /// <summary>
        /// called by drupal ah_secure.module
        /// </summary>
        /// <param name="username"></param>
        /// <param name="enc_newpassword">Must be encrypted with Encrypt256</param>
        /// <returns></returns>
        [WebMethod]
        public object[] UpdatePassword(string username, string enc_newpassword)
        {
            object[] response = new object[2];
            try
            {
                HarperLINQ.tbl_Customer customer = new HarperLINQ.tbl_Customer(username, true);
                customer.cusPassword = enc_newpassword;
                customer.Save();
                response = new object[] { true, null };
            }
            catch (Exception ex)
            {
                EventLogger.LogError("SecureServices.UpdatePassword",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
                response = new object[] { false, ex.Message };
            }
            return response;
        }
        #endregion

    }
}
