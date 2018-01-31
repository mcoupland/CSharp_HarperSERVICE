using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace MemberServices
{

    [System.Xml.Serialization.XmlInclude(typeof(List<Newsletter>))]
    public class NewsletterSubscriber
    {
        public int CusID { get; set; }
        public int UniqueID { get; set; }
        public string EmailAddress { get; set; }
        public List<Newsletter> Subscriptions { get; set; }
        public DateTime? Member_Expire_Date { get; set; }
        public string Member_Type { get; set; }
        public string Member_Source { get; set; }
        public bool Primary_Email { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }

    }
}
