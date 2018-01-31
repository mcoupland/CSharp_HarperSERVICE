using MemberServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System;

namespace Tests
{
    /// <summary>
    ///This is a test class for CustomerServiceTest and is intended
    ///to contain all CustomerServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CustomerServiceTest
    {
        [TestMethod]
        public void CompMembershipTest()
        {
            CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
            string salutation = "Mr.";
            string firstname = "Michael"; 
            string middleinitial = "Q"; 
            string lastname = "Testcomp"; 
            string suffix = "";
            string professionaltitle = "";
            string email = "testcomp001@andrewharper.com";
            string businessname = ""; 
            string address1 = "6217 Test Ave."; 
            string address2 = ""; 
            string address3 = ""; 
            string city = "Austin"; 
            string state = "TX"; 
            string postalcode = "78704"; 
            string country = "US"; 
            string phone = "1231231234"; 
            string ccnum = ""; 
            string ccexpmonth = ""; 
            string ccexpyear = ""; 
            string amountpaid = "0"; 
            string ccname = ""; 
            string ccaddress = ""; 
            string cccity = ""; 
            string ccstate = ""; 
            string ccpostalcode = ""; 
            string pubcode = "PO"; 
            string username = "testcomp";
            string pwd = "testcomp"; 
            string keycode = "OLHCQ"; 
            string subscriptionlength = "12"; 
            string source = "9";
            string customertype = "PERSONAL"; 
            string expiredate = "7/1/2013"; 
            string startdate = "6/13/2012";
            string newsletters = "";
            string mobilephone = ""; 
            string secondemail = ""; 
            string methodofpayment = ""; 
            string cccountry = ""; 
            string ccaddress2 = "";
            string screenname = "testcomp";
            string iscomp = "true";
            CustomerService.ResponseObject response = client.SubscribeNewUser(
                salutation, firstname, middleinitial, lastname, suffix, professionaltitle, email,
                businessname, address1, address2, address3, city, state, postalcode, country, 
                phone, ccnum, ccexpmonth, ccexpyear, amountpaid, ccname, ccaddress, cccity, 
                ccstate, ccpostalcode, pubcode, username, pwd, keycode, subscriptionlength, source, 
                customertype, expiredate, startdate, newsletters, mobilephone, secondemail, methodofpayment, 
                cccountry, ccaddress2, screenname, iscomp);

            Assert.Equals(response.ResponseCode, 0);
        }
        
        
        [TestMethod]
        public void randomtest()
        {
            CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
            

            //string eu = HarperCRYPTO.Cryptography.Encrypt256("mcoupland");
            //string ep = HarperCRYPTO.Cryptography.Encrypt256("testing");
            //client.Login(eu, ep, false);
            Tests.CustomerService.ISO3166[] t = client.GetISORegions("US");

            //decrypt data for clay (auction info)
            //when have access to DB need to create service that does this for him
            // and tie it into the auction admin tool
            
            //CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
            // no pwd 
            //client.SubscribeNewUser("Mr", "JOSE", null, "MARTINEZ MARTINEZ", "", "", "cmartinezsanchez@corporacionhms.com", "AUTO JUNTAS, S.A.", "POLIGONO INDUSTRIAL CAMPOLLANO CALLE C NUMERO UNO", "", "", "ALBACETE", "", "02007", "SPAIN", "+0034 967 216 212", "", "5", "2014", "425.00", "JOSE MANUEL MARTINEZ MARTINEZ", "POLIGONO INDUSTRIAL CAMPOLLANO, C, 1", "ALBACETE", "", "02007", "27", "jmmartinezmartinez", "josemanuel", "WPR350A", "12", "DRUPAL", "PERSONAL", "2013-01-26", "2012-01-26", "", "", "", "visa", "SPAIN", "", "Sr. Martínez");
        }
        //[TestMethod]
        //public void SyncFromSFG() 
        //{
        //    string sfgid = "10001636220"; //mcoupland
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.ResponseObject response = client.SyncFromSFGBySFGid(sfgid);
        //}

        //[TestMethod]
        //public void SyncToSFGByCusId() 
        //{
        //    string cusid = "139930";
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.ResponseObject response = client.SyncToSFGByCusId(cusid);
        //}


        //[TestMethod]
        //public void SubscribeNewUser_SUCCESS()
        //{
        //    int expected = 0;
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();            
        //    Tests.CustomerService.ResponseObject actual = 
        //        client.SubscribeNewUser(
        //        "Ms.", "Test", "", "Moon", "", "", "jmoon@andrewharpertravel.com",
        //        "", "1601 Rio Grande", "", "", "Austin", "Texas", "78701", "US", "512-555-1212",
        //        "378507382661005",
        //        "9", "2013", "350", "Julie Moon", "1601 Rio Grande", "Austin", "TX", "78701", "PR", "jmoon11",
        //        "bozobozo", "WPR350A", "12", "DRUPAL", "UNKNOWN", "11-29-2012", "11-29-2011", "", "", "",
        //        "americanexpress", "US", "", "");
        //        /*
        //        updatedsalutation, updatedfirstname, updatedmiddleinitial, 
        //        updatedlastname, updatedsuffix, updatedprofessionaltitle, updatedemail, 
        //        updatedoptin.ToString(), updatedbusinessname, updatedaddress1, updatedaddress2, 
        //        updatedaddress3, "Vancouver", "'BC", "v6g2m7", "CANADA",
        //                                             updatedphone, updatedfax, updatedaltcity,
        //                                             goodvisa, ccexpmonth, ccexpyear, amountpaid.ToString(),
        //                                             ccname, ccaddress, "Austin",
        //                                             "BC", "v6g2m7", pubcode, updatedusername, updatedpwd, keycode, subscriptionlength.ToString(), "TEST", "PERSONAL",
        //                                             DateTime.Now.AddYears(1).ToShortDateString(), DateTime.Now.AddYears(1).ToShortDateString(), "", updatedphone, updatedemail, "", "CANADA", "", updatedusername);
        //         */
            
            
        //    Assert.AreEqual(expected, actual.ResponseCode);
        //}

        //#region ISO (country) tests
        //[TestMethod]
        //public void GetISOCountries()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.ISO3166[] actual = client.GetISOCountries();
        //}
        //[TestMethod]
        //public void GetISORegions()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.ISO3166[] actual = client.GetISORegions("US");
        //}
        //#endregion

        //[TestMethod]
        //public void GetPrefixes()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.Prefix[] actual = client.GetPrefixes();
        //}
        //[TestMethod]
        //public void GetSuffixes()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.Suffix[] actual = client.GetSuffixes();
        //}
        //[TestMethod]
        //public void GetOffer()
        //{            
        //    //userid in config is premier1 - isn't in sfg test db
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.OfferResponseObject response = client.GetOffer("E001505");//WPO350A");//WPR400A");//keycode);
        //}
        //[TestMethod]
        //public void GetUserNames()
        //{
        //    List<string> u = HarperLINQ.tbl_Customer.GetUserNames();
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    string[] users = client.GetUserNames();
        //}

        //[TestMethod]
        //public void TestNewMemberEmail()
        //{
        //    //change method in service to be public webmethod before running this test
        //    //CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    //client.SendNewMemberEmail("Mr.", "Michael", "Coupland", "Jr", "12345676890", "mcoupland@andrewharper.com");
        //}
        //#region APPEVENTLOG tests
        //[TestMethod]
        //public void APPEVENTLOG_GetAppNames()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    string[] objs = client.GetAppEventLogAppNames();
        //}
        //[TestMethod]
        //public void APPEVENTLOG_GetSeverities()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    string[] objs = client.GetAppEventLogSeverities();
        //}
        //[TestMethod]
        //public void APPEVENTLOG_GetEventNames()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    string[] objs = client.GetAppEventLogEventNames();
        //}
        //[TestMethod]
        //public void APPEVENTLOG_GetByUserName()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.tbl_AppEventLog[] objs = client.GetAppEventLogsByUserName("cms_intern", 0);
        //}
        //[TestMethod]
        //public void APPEVENTLOG_GetByAppName()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.tbl_AppEventLog[] objs = client.GetAppEventLogsByAppName("CustomerWebService", 0);
        //}
        //[TestMethod]
        //public void APPEVENTLOG_GetBySeverity()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.tbl_AppEventLog[] objs = client.GetAppEventLogsBySeverity("ERROR", 0);
        //}
        //[TestMethod]
        //public void APPEVENTLOG_GetByEventName()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.tbl_AppEventLog[] objs = client.GetAppEventLogsByEventName("LOGIN_FAILED", 0);
        //}
        //#endregion

        [TestMethod()]
        public void Login_SUCCESS()
        {

            HarperLINQ.tbl_NetMembership net = HarperLINQ.tbl_NetMembership.GetCurrentNetMembership(31180);

            //string enc_username = HarperCRYPTO.Cryptography.Encrypt256("rcoupland");
            //string enc_password = HarperCRYPTO.Cryptography.Encrypt256("rcoupland");
            //object[] actual = HarperLINQ.tbl_Customer.Login(enc_username, enc_password);
            
            //CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
            //client.Url = "http://192.168.2.55/customerservice.asmx";
            ////client.Url = "http://localhost:3105/CustomerService.asmx";
            //string enc_username = HarperCRYPTO.Cryptography.Encrypt256("duanewolff");
            //string enc_password = HarperCRYPTO.Cryptography.Encrypt256("hampton62985");

            //int expected = 0;
            //Tests.CustomerService.ResponseObject actual = client.Login(enc_username, enc_password, true);
            //Assert.AreEqual(expected, actual.ResponseCode);
        }
        //[TestMethod()]
        //public void Login_INVALID_PASSWORD()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();

        //    string enc_username = HarperCRYPTO.Cryptography.Encrypt256("mcoupland");
        //    string enc_password = HarperCRYPTO.Cryptography.Encrypt256("ig");

        //    int expected = 10;// PasswordMismatchError = 10
        //    Tests.CustomerService.ResponseObject actual = client.Login(enc_username, enc_password, true);
        //    Assert.AreEqual(expected, actual.ResponseCode);
        //}
        //[TestMethod()]
        //public void Login_NO_SUCH_USER_NAME()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();

        //    string enc_username = HarperCRYPTO.Cryptography.Encrypt256("++mcoupland");
        //    string enc_password = HarperCRYPTO.Cryptography.Encrypt256("img");

        //    int expected = 11;
        //    Tests.CustomerService.ResponseObject actual = client.Login(enc_username, enc_password, true);
        //    Assert.AreEqual(expected, actual.ResponseCode);
        //}
        //[TestMethod()]
        //public void Login_CANNOT_DECRYPT_INPUT()
        //{
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();

        //    string enc_username = HarperCRYPTO.Cryptography.Encrypt256("mcoupland");
        //    string enc_password = HarperCRYPTO.Cryptography.Encrypt256("img");

        //    int expected = 101;
        //    Tests.CustomerService.ResponseObject actual = client.Login(enc_username + "121", enc_password, true);
        //    Assert.AreEqual(expected, actual.ResponseCode);
        //}

        //[TestMethod]
        //public void ChangeLINQPassword()
        //{
        //    HarperLINQ.tbl_Customer cus = new HarperLINQ.tbl_Customer("cms_rfrisch", true);
        //    cus.cusPassword = HarperCRYPTO.Cryptography.Encrypt256("downtown1");
        //    cus.Save();
        //}

        //[TestMethod()]
        //public void DEBUG_GetStoredPassword()
        //{
        //    string user_name = "cms_mcarson";
        //    using (HarperLINQ.AHT_MainDataContext context = new HarperLINQ.AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
        //    {
        //        var customer = (from a in context.tbl_Customers
        //                         where a.cusUserName == user_name
        //                        select a).Single();
        //        string clear_p = HarperCRYPTO.Cryptography.Decrypt256FromHEX(customer.cusPassword);
        //        string clear_p2 = HarperCRYPTO.Cryptography.Decrypt256FromHEX(customer.cusEncryptedPassword);
        //    }
        //}
        //[TestMethod()]
        //public void DEBUG_TestLogin()
        //{
        //    string[] users = new string[] { "cms_mcoupland" };//{ "cms_alobrano", "cms_ekrosenthal", "cms_intern", "cms_jblack", "cms_jmoon", "cms_kmitchell", "cms_lcalvert", "cms_mcarson", "cms_mcoupland", "cms_rfrisch", "cms_sojourner", "cms_tgovaars" };
        //    int good = 0;
        //    int bad = 0;
        //    using (HarperLINQ.AHT_MainDataContext context = new HarperLINQ.AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
        //    {
        //        foreach (string user_name in users)
        //        {
        //            try
        //            {
        //                var customer = (from a in context.tbl_Customers
        //                                where a.cusUserName == user_name
        //                                select a).Single();
        //                string clear_p = HarperCRYPTO.Cryptography.Decrypt256FromHEX(customer.cusPassword);

        //                CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();

        //                string enc_username = HarperCRYPTO.Cryptography.Encrypt256(customer.cusUserName);
        //                string enc_password = HarperCRYPTO.Cryptography.Encrypt256(clear_p);

        //                Tests.CustomerService.ResponseObject actual = client.Login(enc_username, enc_password, true);
        //                if (actual.ResponseCode == 0)
        //                {
        //                    good++;
        //                }
        //                else
        //                {
        //                    bad++;
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                System.Diagnostics.Debug.WriteLine(string.Format(@"Dupe user: {0}", user_name));
        //                bad++;
        //            }
        //        }
        //    }

        //    System.Diagnostics.Debug.WriteLine(string.Format(@"Good logins: {0}", good));
        //    System.Diagnostics.Debug.WriteLine(string.Format(@"Bad logins: {0}", bad));
        //}
        
        //#region bulk operations
        //[TestMethod]
        //public void Bulk_256PwdInsert()
        //{
        //    // verify new pwd is same as old one
        //    //using (HarperLINQ.AHT_MainDataContext context = new HarperLINQ.AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
        //    //{
        //    //    var customers = (from a in context.tbl_Customers
        //    //                     where a.cusEncryptedPassword != null
        //    //                     && a.cusPassword != null
        //    //                     select a).Take(100);
        //    //    foreach (HarperLINQ.tbl_Customer customer in customers)
        //    //    {
        //    //        string old_p = customer.cusEncryptedPassword;
        //    //        string old_ph = customer.cusPassword;
        //    //        string clear_p = HarperCRYPTO.Cryptography.DecryptData(old_p);
        //    //        string clear_ph = HarperCRYPTO.Cryptography.Decrypt256FromHEX(old_ph);
        //    //        bool b = clear_p == clear_ph;
        //    //    }
        //    //}
        //    using (HarperLINQ.AHT_MainDataContext context = new HarperLINQ.AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
        //    {
        //        var customers = (from a in context.tbl_Customers
        //                         where a.cusEncryptedPassword != null
        //                         && a.cusPassword == null
        //                         && a.cusIsDeleted == false
        //                         select a);
        //        foreach (HarperLINQ.tbl_Customer customer in customers)
        //        {
        //            string old_p = customer.cusEncryptedPassword;
        //            string clear_p = HarperCRYPTO.Cryptography.DecryptData(old_p);
        //            string hex_p = HarperCRYPTO.Cryptography.Encrypt256(clear_p);
        //            customer.cusPassword = hex_p;
        //            context.SubmitChanges();
        //        }
        //    }

        //}

        //[TestMethod()]
        //public void BULK_Login()
        //{
        //    int good = 0;
        //    int bad = 0;

        //    using (HarperLINQ.AHT_MainDataContext context = new HarperLINQ.AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
        //    {
        //        var customers = (from a in context.tbl_Customers
        //                         where a.cusPassword != null
        //                         select a).Take(50);
        //        foreach (HarperLINQ.tbl_Customer customer in customers)
        //        {
        //            string clear_p = HarperCRYPTO.Cryptography.Decrypt256FromHEX(customer.cusPassword);

        //            CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();

        //            string enc_username = HarperCRYPTO.Cryptography.Encrypt256(customer.cusUserName);
        //            string enc_password = HarperCRYPTO.Cryptography.Encrypt256(clear_p);

        //            Tests.CustomerService.ResponseObject actual = client.Login(enc_username, enc_password, true);
        //            if (actual.ResponseCode == 0)
        //            {
        //                good++;
        //            }
        //            else
        //            {
        //                bad++;
        //            }
        //        }                
        //    }
            
        //    System.Diagnostics.Debug.WriteLine(string.Format(@"Good logins: {0}", good));
        //    System.Diagnostics.Debug.WriteLine(string.Format(@"Bad logins: {0}", bad));
        //}

        //[TestMethod()]
        //public void COMPARE_Login_Methods()
        //{
        //    int count = 100;
        //    DateTime start1 = new DateTime();
        //    TimeSpan stop1 = new TimeSpan();
        //    DateTime start2 = new DateTime();
        //    TimeSpan stop2 = new TimeSpan();
        //    string clear_p = string.Empty;
        //    string enc_username = string.Empty;
        //    string enc_password = string.Empty;
        //    int bad1 = 0;
        //    int bad2 = 0;

        //    List<HarperLINQ.tbl_Customer> customers = new List<HarperLINQ.tbl_Customer>();
        //    using (HarperLINQ.AHT_MainDataContext context = new HarperLINQ.AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
        //    {
        //        customers = (from a in context.tbl_Customers
        //                         where a.cusPassword != null
        //                         select a).Take(count).ToList<HarperLINQ.tbl_Customer>();
                

        //    }
        //    start1 = DateTime.Now;
        //    foreach (HarperLINQ.tbl_Customer customer in customers)
        //    {
        //        clear_p = HarperCRYPTO.Cryptography.Decrypt256FromHEX(customer.cusPassword);

        //        CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();

        //        enc_username = HarperCRYPTO.Cryptography.Encrypt256(customer.cusUserName);
        //        enc_password = HarperCRYPTO.Cryptography.Encrypt256(clear_p);

        //        Tests.CustomerService.ResponseObject actual = client.Login(enc_username, enc_password, true);
        //        if (actual.ResponseCode != 0)
        //        {
        //            bad1++;
        //        }
        //    }
        //    stop1 = DateTime.Now - start1;

        //    start2 = DateTime.Now;
        //    foreach (HarperLINQ.tbl_Customer customer in customers)
        //    {
        //        clear_p = HarperCRYPTO.Cryptography.DecryptData(customer.cusEncryptedPassword);

        //        SecureServices.SecureServices client = new Tests.SecureServices.SecureServices();
        //        SecureServices.BaseResponse bresponse = client.Login(customer.cusUserName, clear_p, string.Empty, string.Empty);
        //        if (bresponse.TypedResponse != null
        //            && bresponse.TypedResponse.Success
        //            && (bresponse.TypedResponse as SecureServices.LoginResponse).Authenticated)
        //        {
        //            bad2++;
        //        }
        //    }
        //    stop2 = DateTime.Now - start2;

        //    System.Diagnostics.Debug.WriteLine(string.Format(@"Old logins: Time: {0} Success {1}/{2}", new object[]{ stop2, bad2, count}));
        //    System.Diagnostics.Debug.WriteLine(string.Format(@"New logins: Time: {0} Success {1}/{2}", new object[] { stop1, bad1, count }));
        //}

        //#endregion

        #region set test data
        public static bool debug = true;
        public static string nosuboffermemberid = "10001112394";// does not have sub offer
        public static string suboffermemberid = "10001625840";// has sub offer
        public static string memberid = "10001625651";

        #region subscription values
        public static string pubcode = "PO";
        public static bool giftflag = false;
        public static bool refreshcustomer = false;
        public static int subscriptionlength = 36;
        public static float price = 12.0f;
        public static float postage = 0.0f;
        public static float tax = 0.0f;
        public static float amountpaid = 12.0f;
        public static string keycode = "E000697";
        public static string verifoneroutingid = "";
        public static long giftid;
        public static string giftsalutation = "mr.";
        public static string giftfirstname = "edwin";
        public static string giftmiddleinitial = "h";
        public static string giftlastname = "oldham";
        public static string giftsuffix = "jr.";
        public static string giftprofessionaltitle = "manager";
        public static string giftemail = "edwinh@testing123.com";
        public static string giftfax = "";
        public static bool giftoptin = false;
        public static string giftbusinessname = "oldtest comp";
        public static string giftaddress1 = "123 gift test";
        public static string giftaddress2 = "suite a";
        public static string giftaddress3 = "cubicle a";
        public static string giftcity = "austin";
        public static string giftstate = "tx";
        public static string giftpostalcode = "78704";
        public static string giftcountry = "usa";
        public static string giftphone = "5121231234";
        public static string giftaltcity = "buda";
        #endregion

        #region cc values
        public static string transactiontype = "charge";
        public static string ccaddress = "123 Test Ave.";
        public static string cccity = "Austin";
        public static string ccpostalcode = "78704";
        public static string ccstate = "TX";
        public static string cccountry = "US";

        public static string ccexpmonth = "1";
        public static string ccexpyear = "13";
        public static string ccname = "Edwin H. Lima";
        public static string goodvisa = "4222222222222"; //even = cc for sfg test
        public static string goodmc = "5555555555554444";
        public static string goodamex = "378734493671000";
        public static string gooddisc = "6011000990139424";

        public static string badvisa = "4111111111111111"; //odd = bad for sfg test
        public static string badmc = "5424180123456789";
        public static string badamex = "378282246310005";
        public static string baddisc = "6011111111111117";
        #endregion

        #region original values
        public static string originalusername = "newedwinh";
        public static string originalpwd = "soap";
        public static string originalsalutation = "mr.";
        public static string originalfirstname = "Edwin";
        public static string originalmiddleinitial = "h";
        public static string originallastname = "originalham";
        public static string originalsuffix = "jr.";
        public static string originalprofessionaltitle = "manager";
        public static string originalemail = "edwinh@testing123.com";
        public static bool originaloptin = false;
        public static string originalbusinessname = "originaltest comp";
        public static string originaladdress1 = "123 original test";
        public static string originaladdress2 = "suite a";
        public static string originaladdress3 = "cubicle a";
        public static string originalcity = "austin";
        public static string originalstate = "tx";
        public static string originalpostalcode = "78704";
        public static string originalcountry = "usa";
        public static string originalphone = "5121231234";
        public static string originalfax = "";
        public static string originalaltcity = "round Rock";
        #endregion

        #region updated values
        public static string updatedusername = GetTrimmedString(DateTime.Now.Ticks.ToString(), 80);
        public static string updatedpwd = GetTrimmedString("updpwd" + updatedusername, 32);
        public static string updatedsalutation = "Dr.";
        public static string updatedfirstname = GetTrimmedString("updf" + updatedusername, 15);
        public static string updatedmiddleinitial = "N";
        public static string updatedlastname = GetTrimmedString("updl" + updatedusername, 20);
        public static string updatedsuffix = "II";
        public static string updatedprofessionaltitle = "updpt";
        public static string updatedemail = GetTrimmedString("upd@" + updatedusername + ".com", 80);
        public static bool updatedoptin = true;
        public static string updatedbusinessname = GetTrimmedString("updbiz" + updatedusername, 40);
        public static string updatedaddress1 = GetTrimmedString(updatedusername + "test", 40);
        public static string updatedaddress2 = GetTrimmedString(updatedusername + "test", 40);
        public static string updatedaddress3 = GetTrimmedString(updatedusername + "test", 40);
        public static string updatedcity = "San Antonio";
        public static string updatedstate = "TX";
        public static string updatedpostalcode = "78247";
        public static string updatedcountry = "USA";
        public static string updatedphone = GetTrimmedString("210123" + updatedusername, 14);
        public static string updatedfax = "";
        public static string updatedaltcity = "Universal City";
        #endregion
        #endregion


        private static string GetTrimmedString(string input, int maxlen)
        {
            return input.Length > maxlen ? input.Substring(0, maxlen) : input;
        }
    }
}
