using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace SupportClasses
{
    public class EventLogger
    {
        #region properties
        public static bool logErrorsOnly { get; set; }
        public static string eventSource { get; set; }
        #endregion

        public static void LogAppEvent(string user, string app_name, string event_name, string message1,
            string message2, string message3, string severity, string section)
        {
            //AppEventLog log_entry = new AppEventLog();
            //log_entry.DateCreated = DateTime.Now;
            //log_entry.UserName = user;
            //log_entry.AppName = app_name;
            //log_entry.Event = event_name;
            //log_entry.Message1 = message1;
            //log_entry.Message2 = message2;
            //log_entry.Message3 = message3;
            //log_entry.Severity = severity;
            //log_entry.Section = section;
            //using (SupportDataDataContext context = new SupportDataDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
            //{
            //    context.AppEventLogs.InsertOnSubmit(log_entry);
            //    context.SubmitChanges();
            //}
        }
        public static void LogError(string method, string message, bool sendemail)
        {
            try
            {
                //EventLog.WriteEntry(eventSource, string.Format("Error in: {0}\r\nMessage:{1}", new object[] { method, message }), EventLogEntryType.Error);
                
                AppEventLog appLog = new AppEventLog();
                appLog.AppName = "HarperSERVICE";
                appLog.DateCreated = DateTime.Now;
                appLog.Event = "ERROR_LOGGED";
                appLog.Message1 = message;
                appLog.Section = method;
                using (SupportDataDataContext context = new SupportDataDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                {
                    context.AppEventLogs.InsertOnSubmit(appLog);
                    context.SubmitChanges();
                }

                Mailer emailer = new Mailer();
                emailer.SendEmail(ConfigurationManager.AppSettings["mailserviceuser"],
                    ConfigurationManager.AppSettings["mailservicepwd"],
                    "SERVICE ERROR LOGGED",
                    ConfigurationManager.AppSettings["erroremailsfrom"],
                    ConfigurationManager.AppSettings["erroremailsto"],
                    string.Empty, string.Empty,
                    message,
                    false, ConfigurationManager.AppSettings["erroremailsmtpserver"]);                
            }
            catch { }
        }
        public static void LogEvent(string message)
        {
            try
            {
                if (!logErrorsOnly)
                {
                    //EventLog.WriteEntry(eventSource, message, EventLogEntryType.Information);
                    
                    //AppEventLog appLog = new AppEventLog();
                    //appLog.AppName = "HarperSERVICE";
                    //appLog.DateCreated = DateTime.Now;
                    //appLog.Event = "EVENT_LOGGED";
                    //appLog.Severity = "INFO";
                    //appLog.Message1 = message;
                    //using (SupportDataDataContext context = new SupportDataDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                    //{
                    //    context.AppEventLogs.InsertOnSubmit(appLog);
                    //    context.SubmitChanges();
                    //}
                }
            }
            catch { }
        }
        
        public static void LogError(string method, string message)
        {
            LogError(method, message, false);
        }
    }
}
