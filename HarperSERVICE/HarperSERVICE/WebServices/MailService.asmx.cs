using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using HarperCRYPTO;
using SupportClasses;

namespace MemberServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]

    public class MailService : System.Web.Services.WebService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject">max len 128</param>
        /// <param name="from">max len 320 == 64 chars for name, 1 for "@", 255 for domain</param>
        /// <param name="to">no max len, allows unlimited recipients (comma separated list)</param>
        /// <param name="cc">max len 3200, allows up to 10 cc recipients (comma separated list)</param>
        /// <param name="bcc">max len 3200, allows up to 10 bcc recipients (comma separated list)</param>
        /// <param name="body">no max len</param>
        /// <returns></returns>
        [WebMethod]
        public bool SendMail(string hashedusername, string hashedpassword, string subject, string fromaddress, string toaddress, string cc, string bcc, string body, bool ishtml, string smtp)
        {
            //TODO: add calling server authentication
            string user = Cryptography.DeHash(hashedusername);
            string pwd = Cryptography.DeHash(hashedpassword);
            Mailer emailer = new Mailer();
            return emailer.SendEmail(user, pwd, subject, fromaddress, toaddress, cc, bcc, body, ishtml, smtp);
        }
    }
}
