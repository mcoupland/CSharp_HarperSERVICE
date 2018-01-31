using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HarperLINQ;

namespace MemberServices
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            tbl_Customer.CreateCustomer("5924 Contra Costa Rd", "", "", "Oakland", "CA", "USA", "94618-2137", 
                "Z1", "Harper79", "PERSONAL", "", "Kellie", "", "McElhaney", "", 
                "kmack@haas.berkeley.edu", "kmcelhaney",  "81063000008", "PO", "01/05/2013", 
                "07/05/2013", "kmcelhaney", "", "", "POTRIAL");
            /*
            string rand = GetTrimmedString(DateTime.Now.Ticks.ToString(), 6);
            CustomerService client = new CustomerService();
            string salutation = "Mr.";
            string firstname = "Michael" + rand;
            string middleinitial = null;
            string lastname = "Testcomp" + rand;
            string suffix = "";
            string professionaltitle = "";
            string email = "testcomp" + rand + "@andrewharper.com";
            string businessname = "";
            string address1 = rand + " Test Ave.";
            string address2 = "";
            string address3 = null;
            string city = "Austin";
            string state = "TX";
            string postalcode = "78704";
            string country = "US";
            string phone = "1231231234";
            string ccnum = "";
            string ccexpmonth = "";
            string ccexpyear = "";
            string amountpaid = "";
            string ccname = "";
            string ccaddress = "";
            string cccity = "";
            string ccstate = "";
            string ccpostalcode = "";
            string pubcode = "PO";
            string username = "testcomp" + rand;
            string pwd = "testcomp";
            string keycode = "MDSPUBP";
            string subscriptionlength = "12";
            string source = "9";
            string customertype = "PERSONAL";
            string expiredate = "7/1/2013";
            string startdate = "6/13/2012";
            string newsletters = "";
            string mobilephone = null;
            string secondemail = null;
            string methodofpayment = "";
            string cccountry = "";
            string ccaddress2 = "";
            string screenname = "testcomp" + rand;
            string iscomp = "false";
            ResponseObject response = client.SubscribeNewUser(
                salutation, firstname, middleinitial, lastname, suffix, professionaltitle, email,
                businessname, address1, address2, address3, city, state, postalcode, country,
                phone, ccnum, ccexpmonth, ccexpyear, amountpaid, ccname, ccaddress, cccity,
                ccstate, ccpostalcode, pubcode, username, pwd, keycode, subscriptionlength, source,
                customertype, expiredate, startdate, newsletters, mobilephone, secondemail, methodofpayment,
                cccountry, ccaddress2, screenname, iscomp);

            TextBox1.Text = response.ResponseCode.ToString();
            */
        }


        private static string GetTrimmedString(string input, int maxlen)
        {
            return input.Length > maxlen ? input.Substring(0, maxlen) : input;
        }
    }
}
