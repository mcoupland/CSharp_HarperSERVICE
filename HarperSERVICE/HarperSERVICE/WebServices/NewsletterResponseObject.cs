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
    [System.Xml.Serialization.XmlInclude(typeof(NewsletterSubscriber))]
    public class NewsletterResponseObject
    {
        public bool Success { get; set; }
        public NewsletterSubscriber PrimarySubscriber { get; set; }
        public NewsletterSubscriber SecondarySubscriber { get; set; }
    }


}
