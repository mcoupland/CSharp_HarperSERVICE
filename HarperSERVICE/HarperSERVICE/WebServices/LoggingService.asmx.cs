using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using HarperLINQ;

namespace MemberServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class LoggingService : System.Web.Services.WebService
    {
        string connection_string = ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString;

        [WebMethod]
        public void LogAppEvent(string user, string app_name, string event_name, string message1, string message2, string message3,
            string severity, string section)
        {
            try
            {
                SupportClasses.EventLogger logger = new SupportClasses.EventLogger();
                
                tbl_AppEventLog log_entry = new tbl_AppEventLog(user, app_name, 
                        event_name, message1, 
                        message2, message3,
                        severity, section);
                log_entry.Save();
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        [WebMethod]
        public void LogWebEvent(string user, string event_name, string description)
        {
            try
            {
                tbl_WebEventLog log_entry = new tbl_WebEventLog
                {
                    welUserName = user,
                    welDateCreated = DateTime.Now,
                    welEvent = event_name,
                    welEventDesc = description
                };
                log_entry.Save();
            }
            catch { }
        }
    }
}
