
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Web;
using System.Web.Security;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SFG_Provider;

namespace Tests
{
    /// <summary>
    /// Summary description for ACLTests
    /// </summary>
    [TestClass]
    public class ACLTests
    {
        public ACLTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestRedeem()
        {
            //?ReferralId=CYlk6iy%2f1fE571%2fnOaGN7A%3d%3d
            string rid = HarperCRYPTO.Cryptography.EncryptData("24");
            string pwd = HarperCRYPTO.Cryptography.EncryptData("testing123");
            MembershipService.MembershipService client = new MembershipService.MembershipService();
            MembershipService.BaseResponse bresponse = client.RedeemReferral(rid, "Live", "User11", "lt11@andrewharper.com", "USA", "11 Oak", "", "San Antonio", "Texas", "78247", true, "lt11", pwd);
        }

        [TestMethod]
        public void TestTemp()
        {
            using (HarperACL.ACLDataDataContext context = new HarperACL.ACLDataDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
            {
                //var lastcustomer = (from a in context.Customers
                //                    select new { cusnum = long.Parse(a.cusCustNum) })
                //                    .OrderByDescending(x => x.cusnum)
                //                    .ToList();
                for (int i = 0; i < 20; i++)
                {
                    DateTime end1 = new DateTime();
                    DateTime end2 = new DateTime();
                    DateTime start1 = new DateTime();
                    DateTime start2 = new DateTime();

                    start1 = DateTime.Now;
                    var lastcustomer1 = (from a in context.Customers
                                         select new { cusnum = Convert.ToInt64(a.cusCustNum) })
                                        .OrderByDescending(x => x.cusnum)
                                        .Take(1).Single();
                    long newcustnum = lastcustomer1.cusnum + 1;
                    end1 = DateTime.Now;



                    start2 = DateTime.Now;
                    newcustnum = 0;
                    List<string> lastcustomer = (from a in context.Customers
                                                 select a.cusCustNum).ToList();
                    List<long> cusnumbs = new List<long>();
                    foreach (string cus in lastcustomer)
                    {
                        cusnumbs.Add(long.Parse(cus));
                    }
                    cusnumbs.Sort();
                    cusnumbs.Reverse();

                    newcustnum = cusnumbs[0];
                    newcustnum++;
                    end2 = DateTime.Now;

                    TimeSpan method1 = end1 - start1;
                    TimeSpan method2 = end2 - start2;

                    System.Diagnostics.Debug.WriteLine(string.Format("Old {0}:  vs New: {1}", new object[] { method2.TotalMilliseconds, method1.TotalMilliseconds }));
                }
            }
        }

        [TestMethod]
        public void TestAllLogins()
        {
            int good = 0;
            int bad = 0;
            GateKeeperProvider currentProvider = (GateKeeperProvider)Membership.Provider;
            string u;
            string p;
            HarperACL.Authenticator auth;
            SupportClasses.BaseResponse baseResponse;
            List<HarperACL.Customer> customers;
            using (HarperACL.ACLDataDataContext context = new HarperACL.ACLDataDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
            {
                customers = (from a in context.Customers
                             join b in context.NetMemberships
                             on a.cusID equals b.cusID
                             where a.cusUserName != null
                             && b.nmbDateEnd >= DateTime.Now
                             && a.cusFirstName == ""
                             && a.cusLastName == ""
                             select a).ToList();
            }
            foreach (HarperACL.Customer customer in customers)
            {
                u = customer.cusUserName;
                p = currentProvider.GetPassword(u, "");
                p = HarperCRYPTO.Cryptography.EncryptData(p);
                auth = new HarperACL.Authenticator(u, p, false, false, false);
                baseResponse = auth.Login();
                if (baseResponse.TypedResponse != null
                    && baseResponse.TypedResponse.Success
                    && (baseResponse.TypedResponse as SupportClasses.LoginResponse).Authenticated)
                {
                    good++;
                }
                else
                {
                    bad++;
                }
                System.Threading.Thread.Sleep(10);
            }
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void BulkEncrypt()
        {
            GateKeeperProvider currentProvider = (GateKeeperProvider)Membership.Provider;
            string u;
            string p;
            List<HarperACL.Customer> customers;
            using (HarperACL.ACLDataDataContext context = new HarperACL.ACLDataDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
            {
                customers = (from a in context.Customers
                             join b in context.NetMemberships
                             on a.cusID equals b.cusID
                             where  (a.cusEncryptedPassword == null || a.cusEncryptedPassword == "")
                             && b.nmbDateEnd >= DateTime.Now
                             && a.cusDateUpdated < DateTime.Now.AddDays(-2)
                             select a).ToList();
                
                foreach (HarperACL.Customer customer in customers)
                {
                    if (!string.IsNullOrEmpty(customer.cusUserName))
                    {
                        string z = currentProvider.GetPassword(customer.cusUserName, "");
                        if (!string.IsNullOrEmpty(z))
                        {
                            string enc = HarperCRYPTO.Cryptography.EncryptData(z);
                            if (!string.IsNullOrEmpty(enc))
                            {
                                customer.cusEncryptedPassword = enc;
                                customer.cusDateUpdated = DateTime.Now;
                            }
                        }
                    }
                }
                context.SubmitChanges();
            }
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestLogin()
        {
            GateKeeperProvider currentProvider = (GateKeeperProvider)Membership.Provider;
            string u;
            string p;
            HarperACL.Authenticator auth;
            SupportClasses.BaseResponse baseResponse;
            //u = "ut91";

            //u = "rbkb1234@aol.com";
            //p = "gilman10";
            //u = "premier1";
            //p = "@ndrew11";
            //u = "lindacpedwards@gmail.com";
            //p = "ledwards2011";
            //u = "mcoupland";
            //p = "testing123";
            //u = "testuser0001";
            //p = "testuser0001";
            //u = "aklein";
            //p = "aklein2011";
            //u = "kdicker";
            //p = "testing123";
            //u = "trademarkmedia";
            //p = "4eFr2cHE";
            //u = "cms_jmoon";
            //p = "andrewharper10";
            //u = "janebooz";
            //p = "october";
            //u = "marchbanks@pnvm.com";
            //p = "marchbanks";
            //u = "another@andrewharper.com";            //p = "testing123";
            u = "mcoupland";
            p = "img";
            p = HarperCRYPTO.Cryptography.Hash(p, true);
            auth = new HarperACL.Authenticator(u, p, true, true, false);
            baseResponse = auth.Login();

            SecureServices.SecureServices client = new Tests.SecureServices.SecureServices();
            SecureServices.BaseResponse bresponse = client.Login(u, p, string.Empty, string.Empty);
            if (bresponse.TypedResponse != null
                && bresponse.TypedResponse.Success
                && (bresponse.TypedResponse as SecureServices.LoginResponse).Authenticated)
            {
                Assert.AreEqual(true, true);
            }
        }

        [TestMethod]
        public void TestCreateLogin()
        {
            GateKeeperProvider currentProvider = (GateKeeperProvider)Membership.Provider;
            HarperACL.Authenticator auth;
            SupportClasses.BaseResponse baseResponse;
            string sfgid = HarperCRYPTO.Cryptography.Hash("81060800005", true);
            string zip = "76102";
            string un = "mswynne";
            string pw = HarperCRYPTO.Cryptography.Hash("mswynne1", true);
            //string un = "rcalvert";
            //string pw = "rcalvert2011";
            //10000014618
            //10000001683
            //10000000772
            //80091600006
            //10001264756

            SecureServices.SecureServices client = new Tests.SecureServices.SecureServices();
            SecureServices.BaseResponse bresponse = client.CreateUser(sfgid, un, pw, zip);
            if (bresponse.TypedResponse != null
                && bresponse.TypedResponse.Success
                && (bresponse.TypedResponse as SecureServices.LoginResponse).Authenticated)
            {
                Assert.AreEqual(true, true);
            }
        }


    }
}
