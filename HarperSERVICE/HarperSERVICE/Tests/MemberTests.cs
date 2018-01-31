using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarperCRYPTO;
using BusinessLogic;
using SupportClasses;

namespace Tests
{
    /// <summary>
    /// Summary description for MemberTests
    /// </summary>
    [TestClass]
    public class MemberTests
    {
        [TestMethod]
        public void TestAuction()
        {
            string s = Cryptography.Encrypt256("142325");
            string output = string.Empty;
            List<string> sfgids = new List<string>();
            sfgids.Add("10000015814");
            sfgids.Add("10000003238");
            sfgids.Add("10001211374");
            sfgids.Add("10001351624");
            sfgids.Add("10000990253");
            sfgids.Add("10001091308");
            sfgids.Add("10001347445");
            sfgids.Add("10001559261");
            sfgids.Add("10001606088");
            sfgids.Add("10001606787");
            sfgids.Add("10001607006");
            sfgids.Add("10001611776");
            sfgids.Add("10001632179");
            sfgids.Add("81021300001");
            sfgids.Add("81071400005");
            sfgids.Add("10001305999");
            foreach(string sfgid in sfgids)
            {
                try
                {
                    Tests.MembershipService.MembershipService client = new Tests.MembershipService.MembershipService();
                    Tests.MembershipService.BaseResponse response = client.GetMemberBySFGId(Cryptography.Hash(sfgid, true));
                    Tests.MembershipService.GatekeeperServiceResponse gk = (Tests.MembershipService.GatekeeperServiceResponse)response.TypedResponse;
                    System.Diagnostics.Debug.WriteLine(string.Format("Local: {0}, SFG: {1}", sfgid, gk.MemberData.MemberId));
                    output += string.Format("Local: {0}, SFGID: {1}, CusId: {2}, Email:{3}, CusCustNum: {4} \r\n", sfgid, gk.MemberData.MemberId, gk.MemberData.CusId, gk.MemberData.Email, gk.MemberData.CusCustNum);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Error: {0}", sfgid));
                }
            }
            
            List<string> usernames = new List<string>();            
            usernames.Add("cynthisize");
            usernames.Add("cooper");
            usernames.Add("dstern");
            usernames.Add("greenej");
            usernames.Add("thomas");
            usernames.Add("hbgianos");
            usernames.Add("drmjack");
            usernames.Add("premier1");
            usernames.Add("acbahrenburg");
            usernames.Add("fournet");
            usernames.Add("briodyhill");
            usernames.Add("lpetrasek");
            usernames.Add("premieronline");
            usernames.Add("pjpell");
            usernames.Add("aziegler");
            usernames.Add("maritadaly");
            foreach (string user in usernames)
            {
                try
                {
                    Tests.MembershipService.MembershipService client = new Tests.MembershipService.MembershipService();
                    Tests.MembershipService.BaseResponse response = client.GetMemberByUserName(user);
                    Tests.MembershipService.GatekeeperServiceResponse gk = (Tests.MembershipService.GatekeeperServiceResponse)response.TypedResponse;
                    System.Diagnostics.Debug.WriteLine(string.Format("User: {0}, SFGID: {1}", user, gk.MemberData.MemberId));
                    output += string.Format("User: {0}, SFGID: {1}, CusId: {2}, Email:{3}, CusCustNum: {4} \r\n", user, gk.MemberData.MemberId, gk.MemberData.CusId, gk.MemberData.Email, gk.MemberData.CusCustNum);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Error: {0}", user));
                }
            }
            System.Diagnostics.Debug.WriteLine(output);
        }

        #region set test data
        public static bool debug = true;
        public static string nosuboffermemberid = "10001112394";// does not have sub offer
        public static string suboffermemberid = "10001625840";// has sub offer
        public static string memberid = "10001625651";

        #region subscription values
        public static string pubcode = "DS";
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

        public MemberTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        //sfg staging
        //trademarkmedia, 138859, 10001112394, 4eFr2cHE
        //string hashedu = Cryptography.Hash("cms_mcoupland");
        //string hashedp = Cryptography.Hash("");
        //string clearu = Cryptography.DeHash(hashedu);
        //string clearp = Cryptography.DeHash(hashedp);
        [TestMethod]
        public void LoginSfg()
        {
            CustomerService.CustomerService svc = new Tests.CustomerService.CustomerService();
            string hen = Cryptography.Encrypt256("craux");
            string hep = Cryptography.Encrypt256("andrewharper");
            CustomerService.ResponseObject actual = svc.Login(hen, hep, false);
            Assert.AreEqual(true, actual != null);
        }
        //    MembershipLogic target = new MembershipLogic();
        //    //string hen = Cryptography.Hash("trademarkmedia");
        //    //string hep = Cryptography.Hash("4eFr2cHE");
        //    string hen = Cryptography.Hash("cms_erosenthal");
        //    string hep = Cryptography.Hash("andrewharper10");
        //    string uen = Cryptography.DeHash(hen);
        //    string uep = Cryptography.DeHash(hep);
        //    BaseResponse actual = target.Login(uen, uep, "", "");                         
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("jdoe014", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("jdoetesting134@andrewharper.com", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
            
        //    actual = target.Login("634322602048750410", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("10001625666", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("10001625840", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("10001472061", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("10001360523", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("10001366020", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("10001608185", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("10001019264", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("10001032830", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //    actual = target.Login("10001106471", "testing123", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success && (actual.TypedResponse as LoginResponse).Authenticated);
        //}
        //[TestMethod]
        //public void LoginAH()
        //{
        //    MembershipLogic target = new MembershipLogic();
        //    BaseResponse actual = target.Login("janebooz", "october", "", "");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        //    Assert.AreEqual((actual.TypedResponse as LoginResponse).Authenticated, true);
        //}
        [TestMethod]
        public void GetReferredMembers()
        {
            List<string> referred = new List<string>();
            referred.Add("FGillick@aol.com");
            referred.Add("jlwinter39@sbcglobal.net");
            referred.Add("lisamiller817@gmail.com");
            referred.Add("mjgoldsmith315@aol.com");
            referred.Add("johnsadler@earthlink.net");
            referred.Add("johnwsadler@earthlink.net");
            referred.Add("djajohnston@gmail.com");
            referred.Add("quitoallen@aol.com");
            referred.Add("dwhea@aol.com");
            referred.Add("pes@yahoo.com");
            referred.Add("glenn.davis@mac.com");
            referred.Add("melhirsch@att.net");
            referred.Add("shannonoryan@sbcglobal.net");
            referred.Add("eadaniels@aol.com");
            referred.Add("bernard.bedon@smvgroup.com");
            referred.Add("BChurch@hardenhealthcare.com");
            referred.Add("misse@honeybeeweddings.com");
            referred.Add("nathaliedarley@aol.com");
            referred.Add("victorinemerriman@me.com");
            referred.Add("alan.durlester@wellsfargoadvisors.com");
            referred.Add("lokeyjoy@yahoo.com");
            referred.Add("jennifer.munro@starwoodhotels.com");
            referred.Add("redzjr@hotmail.com");
            referred.Add("salterwest@comcast.net");
            referred.Add("lhcorson@aol.com");
            referred.Add("marnick@optonline.net");
            referred.Add("BAG28DDS@AOL.com");
            referred.Add("doberman@embarqmail.com");
            referred.Add("walt.wood@hotmail.com");
            referred.Add("ugakim@aol.com");
            referred.Add("rjohnson@ui.urban.org");
            referred.Add("sueqrock@gmail.com");
            referred.Add("alexandra.thome@citi.com");
            referred.Add("ekimmm@aol.com");
            referred.Add("csaladino@saladinos.com");
            referred.Add("kevin@pavilionp.com");
            referred.Add("michael.baron@gmail.com");
            referred.Add("pkg100@aol.com");
            referred.Add("jean.anderson@weil.com");
            referred.Add("kathy@mmxmail.com");
            referred.Add("lauriesalvo@yahoo.com");
            referred.Add("rdusman@comcast.net");
            referred.Add("Susecornw@gmail.com");
            referred.Add("hb@thebrody.com");
            referred.Add("david.butterly@dm.duke.edu");
            referred.Add("jocelynrefo@gmail.com");
            referred.Add("jim@ayrshire.cc");
            referred.Add("jeribrock@yahoo.com");
            referred.Add("darbydenison@gmail.com");
            referred.Add("kgreiner1116@gmail.com");
            referred.Add("poohbear4209@gmail.com");
            referred.Add("james.weir@gmail.com");
            referred.Add("kradelb@aol.com");
            referred.Add("mojo1954@yahoo.com");
            referred.Add("Luz3@knology.net");
            referred.Add("williams.keithmary@gmail.com");
            referred.Add("N53049@hotmail.com");
            referred.Add("sreaves@omegafi.com");
            referred.Add("rutilio@charter.net");
            referred.Add("ronroth@mindspring.com");
            referred.Add("loria03@gmail.com");
            referred.Add("araddison@yahoo.com");
            referred.Add("jessiegilbert@gmail.com");
            referred.Add("alan.gilbert@snrcenton.com");
            referred.Add("krilander@gmail.com");
            referred.Add("sara.dibonaventura@gmail.com");
            referred.Add("johnpower1441@gmail.com");
            referred.Add("rchestnut@tampabay.rr.com");
            referred.Add("htobin@cppib.ca");
            referred.Add("coffee0624@hotmail.com");

            MembershipLogic target = new MembershipLogic();
            List<string> found = new List<string>();
            int notfound = 0;
            foreach(string referral in referred)
            {
                BaseResponse actual = target.GetMemberByUserName(referral);
                if (actual != null && actual.TypedResponse != null && actual.TypedResponse.Success)
                {                    
                    try
                    {
                        GetMemberResponse res = actual.TypedResponse as GetMemberResponse;
                        if (res.MemberFound)
                        {
                            if (res.MemberData != null
                                && res.MemberData.Subscriptions != null
                                && res.MemberData.Subscriptions.Count > 0)
                            {
                                System.Diagnostics.Debug.WriteLine(string.Format("SFG ID {0} Subscription count: {1}", res.MemberData.MemberId, res.MemberData.Subscriptions.Count));
                                found.Add(string.Format("{0}, {1}, {2}", referral, res.MemberData.MemberId, res.MemberData.Subscriptions.Count));
                            }
                            else
                            {
                                notfound++;
                            }
                        }
                        else
                        {
                            found.Add(string.Format("{0}, {1}, {2}", referral, string.Empty, null));
                            notfound++;
                        }
                    }
                    catch
                    {
                        notfound++;
                    }
                }
                else
                {
                    notfound++;
                }
                System.Diagnostics.Debug.WriteLine(string.Format("Found: {0}", found.Count));
                System.Diagnostics.Debug.WriteLine(string.Format("NOT Found: {0}", notfound));
            }
        }

        [TestMethod]
        public void GetUP()
        {
            int i = 0;
            List<HarperLINQ.tbl_Customer> customers = HarperLINQ.tbl_Customer.GetAllCustomers();
            foreach (HarperLINQ.tbl_Customer customer in customers)
            {                
                string pwd = string.IsNullOrEmpty(customer.cusEncryptedPassword) ? "" : HarperCRYPTO.Cryptography.DecryptData(customer.cusEncryptedPassword);
                string un = customer.cusUserName;
                string cusid = customer.cusID.ToString();
                string cuscustnum = customer.cusCustNum;
                i++;
                System.Diagnostics.Debug.WriteLine(i.ToString() + "," + un + "," + pwd + "," + cusid + "," + cuscustnum);
            }
        }

        //[TestMethod]
        //public void GetMemberByEmail()
        //{
        //    string s = HarperCRYPTO.Cryptography.Hash("Hell0Ted", true);
        //    //If you pass emailaddress as Username, SFG returns the user with that email address
        //    //oddly enough, it also reports the emailaddress as the username instead of the actual username
            
        //    MembershipLogic target = new MembershipLogic();
        //    BaseResponse actual = target.GetMemberByUserName("michellec@cavco.com");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        //    Assert.AreEqual(true, (actual.TypedResponse as GetMemberResponse).MemberFound);
        //}

        //[TestMethod]
        //public void GetMemberByMemberId()
        //{
        //    //MembershipLogic target = new MembershipLogic();
        //    ////string hashedu = Cryptography.Hash("10001625651");//.Hash("10001112394");
        //    string hashedu = Cryptography.Hash("81071300007", true);
        //    string clearu = Cryptography.DeHash(hashedu);
        //    //BaseResponse actual = new MembershipLogic().GetMemberById(clearu, true);
        //    //Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        //    //Assert.AreEqual(true, (actual.TypedResponse as GetMemberResponse).MemberFound);

        //    MembershipService.MembershipService client = new Tests.MembershipService.MembershipService();
        //    MembershipService.BaseResponse actual = client.GetMemberBySFGId(hashedu);
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
            
        //}
        //[TestMethod]
        //public void GetMemberByUsername()
        //{
        //    MembershipLogic target = new MembershipLogic();
        //    BaseResponse actual = target.GetMemberByUserName("mcoupland");//"shelley@shelleymorrisinteriors.com");
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        //    Assert.AreEqual(true, (actual.TypedResponse as GetMemberResponse).MemberFound);
        //}
        //[TestMethod]
        //public void UpdateUsername()
        //{
        //    MembershipLogic target = new MembershipLogic();
        //    BaseResponse actual = target.UpdateUsername("mswynne_old", "mswynne", "mswynne1", debug);
        //    BaseResponse updated = target.GetMemberByUserName(updatedusername);
        //    //resetUsername();
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        //    Assert.AreEqual((updated.TypedResponse as GetMemberResponse).MemberData.MemberId == memberid, true);
        //}

        //[TestMethod]
        //public void GetPwd()
        //{
        //    //test data copied from error log       
        //    //string cu = HarperCRYPTO.Cryptography.Decrypt256FromHEX("782235c49c7a75f8913a6740e0cf4704");
        //    //string cp = HarperCRYPTO.Cryptography.Decrypt256FromHEX("26fe4e3456434157c86d63cdd7ba073b");

        //    //CustomerService.CustomerService client2 = new Tests.CustomerService.CustomerService();
        //    //Tests.CustomerService.ResponseObject ro2 = client2.Login("782235c49c7a75f8913a6740e0cf4704", "26fe4e3456434157c86d63cdd7ba073b", false);

        //    HarperLINQ.tbl_Customer cus2 = new HarperLINQ.tbl_Customer("premier1", true);
        //    string pwd2 = HarperCRYPTO.Cryptography.DecryptData(cus2.cusEncryptedPassword);
        //    //string sdfsd2 = HarperCRYPTO.Cryptography.Decrypt256FromHEX(cus2.cusPassword);
        //    //CustomerService.CustomerService client1 = new Tests.CustomerService.CustomerService();
            
        //    //69835e4e4826be87769ab83335a6e5fa-c45c11c35bade79bbb932a2ee51d4168-dGVzdGluZzEyMw==
        //   // Tests.CustomerService.ResponseObject ro1 = client1.Login(HarperCRYPTO.Cryptography.Encrypt256("mcoupland"), cus2.cusPassword, false);

        //    Tests.SecureServices.SecureServices c = new Tests.SecureServices.SecureServices();
        //    Tests.SecureServices.BaseResponse r = c.Login("mcoupland", cus2.cusEncryptedPassword, null, null);


        //    HarperLINQ.tbl_Customer cus = new HarperLINQ.tbl_Customer("cms_mcoupland", true);
        //    string pwd = HarperCRYPTO.Cryptography.DecryptData(cus.cusEncryptedPassword);
        //    string sdfsd = HarperCRYPTO.Cryptography.Decrypt256FromHEX(cus.cusPassword);
        //    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();
        //    Tests.CustomerService.ResponseObject ro = client.Login(HarperCRYPTO.Cryptography.Encrypt256("cms_mcoupland"), cus.cusPassword, false);
        //}

        //[TestMethod]
        //public void VerifyAndUpdatePassword()
        //{
        //    MembershipLogic target = new MembershipLogic();
        //    string hashedu = Cryptography.Hash("mcoupland");
        //    string clearu = Cryptography.DeHash(hashedu);
        //    SecureServices.SecureServices client = new Tests.SecureServices.SecureServices();
        //    SecureServices.BaseResponse actual = client.VerifyAndUpdatePassword(clearu, Cryptography.Hash("testing12345", true), Cryptography.Hash("testing123", true)); 
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        //}


        //[TestMethod]
        //public void UpdatePassword()
        //{
        //    MembershipLogic target = new MembershipLogic();
        //    string hashedu = Cryptography.Hash("mcoupland");
        //    string clearu = Cryptography.DeHash(hashedu);
        //    SecureServices.SecureServices client = new Tests.SecureServices.SecureServices();
        //    SecureServices.BaseResponse actual = client.UpdatePassword(clearu, Cryptography.Hash("testing12345", true)); //target.UpdatePassword(clearu, "testing1234", debug);
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        //}
        //[TestMethod]
        //public void UpdateMember()
        //{
        //   // BaseResponse before = new MembershipLogic().GetMemberById("81102500006", true);
            
        //    //MembershipLogic c = new MembershipLogic();
        //    //BaseResponse actual = c.UpdateMember("81102500006","", "Robert", "Coupland", "", "", "namaesm@ayhoo.com", false, "", "Lioba Dr.", "", "", "San Antonio", "TX", "78247", "US", "", "rcoupland", false);
                
        //    //BaseResponse after = new MembershipLogic().GetMemberById(memberid, true);
        //    //Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        //    //Assert.AreEqual((before.TypedResponse as GetMemberResponse).MemberData.FirstName !=
        //    //    (after.TypedResponse as GetMemberResponse).MemberData.FirstName, true);


        //     Tests.MembershipService.MembershipService c = new Tests.MembershipService.MembershipService();
        //    //69835e4e4826be87769ab83335a6e5fa-553ef0af2124e2f32a3b77084b85e091-ODExMDI1MDAwMDY=
        //    //81102500006
        //    string id = Cryptography.DeHash("69835e4e4826be87769ab83335a6e5fa-553ef0af2124e2f32a3b77084b85e091-ODExMDI1MDAwMDY=", true);
        //    Tests.MembershipService.BaseResponse actual = c.UpdateMember("69835e4e4826be87769ab83335a6e5fa-553ef0af2124e2f32a3b77084b85e091-ODExMDI1MDAwMDY=", "", "Robert", "Coupland", "", "", "namaesm@hoo.com", false, "", "16404 Spruce Leaf", "", "", "San Antonio", "TX", "78247", "US", "", "rcoupland");

        //    BaseResponse after = new MembershipLogic().GetMemberById("81102500006", true);
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);         
        //}

        //[TestMethod]
        //public void UpdateMember2()
        //{
        //    memberid = Cryptography.DeHash("69835e4e4826be87769ab83335a6e5fa-cb2b20e85bb5b83efb31f995122f42c7-MTAwMDE1NjkzNTY=", true);// "10001569356";

        //  //  BaseResponse before = new MembershipLogic().GetMemberById(memberid, true);
        //    BaseResponse actual = new MembershipLogic().UpdateMember(memberid,"", "Julie", "Moon", "", "", "", true, "", "9809 Spanish Wells Drive", 
        //        "","","Austin","TX", "78717","USA","1231234","screenname",
        //        false);
        //    BaseResponse after = new MembershipLogic().GetMemberById(memberid, true);
        //    Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        //    //Assert.AreEqual((before.TypedResponse as GetMemberResponse).MemberData.FirstName !=
        //  //      (after.TypedResponse as GetMemberResponse).MemberData.FirstName, true);
        //}


        private static string GetTrimmedString(string input, int maxlen)
        {
            return input.Length > maxlen ? input.Substring(0, maxlen) : input;
        }
    }
}
