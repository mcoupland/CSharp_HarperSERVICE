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
using HarperLINQ;

namespace MemberServices
{
    [System.Xml.Serialization.XmlInclude(typeof(HarperLINQ.tbl_Customer))]
    [System.Xml.Serialization.XmlInclude(typeof(HarperLINQ.tbl_AddressCustomer))]
    [System.Xml.Serialization.XmlInclude(typeof(HarperLINQ.tbl_NetMembership))]

    public class ResponseObject
    {
        public int ResponseCode = 0;
        public HarperLINQ.tbl_Customer Customer = new tbl_Customer();
        public HarperLINQ.tbl_AddressCustomer Address = new tbl_AddressCustomer();
        public HarperLINQ.tbl_NetMembership NetMembership = new tbl_NetMembership();
    }
}
