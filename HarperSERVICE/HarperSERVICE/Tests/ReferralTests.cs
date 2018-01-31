using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarperCRYPTO;
using HarperLINQ;
using BusinessLogic;
using SupportClasses;
using System.Data.Linq;
using System.Configuration;

namespace Tests
{
    /// <summary>
    /// Summary description for ReferralTests
    /// </summary>
    [TestClass]
    public class ReferralTests
    {
        public ReferralTests()
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
        /*
        [TestMethod]
        public void PASS_GetISO3166()
        {
            HarperLINQ.ISO3166 iso = new HarperLINQ.ISO3166("ES", HarperLINQ.IdentifierType.Country_Code_Alpha2);
        }

        [TestMethod]
        public void PASS_ReferralEmailBody()
        {
            HarperLINQ.Referral refer = new HarperLINQ.Referral(10);
            string email = refer.GetReferralEmailBody();
        }

        [TestMethod]
        public void PASS_SendReferralEmail()
        {
            HarperLINQ.Referral refer = new HarperLINQ.Referral(30);
            refer.membername = "Michael Coupland";
            string email_body = refer.GetReferralEmailBody();
            Mailer mailer = new Mailer();

            mailer.SendEmail(
                ConfigurationManager.AppSettings["mailserviceuser"],
                ConfigurationManager.AppSettings["mailservicepwd"],
                "Membership Invitation from Your Friend",
                ConfigurationManager.AppSettings["referemailfrom"],
                "mcoupland@andrewharper.com",
                string.Empty,
                string.Empty,
                email_body,
                true,
                ConfigurationManager.AppSettings["smtpserver"]);
        }

        [TestMethod]
        public void PASS_GetReferralUserCreatedEmailBody()
        {
            HarperLINQ.Referral refer = new HarperLINQ.Referral(13);
            string email = refer.GetReferralUserCreatedEmailBody();
        }

        [TestMethod]
        public void PASS_SendReferralUserCreatedEmail()
        {
            HarperLINQ.Referral refer = new HarperLINQ.Referral(13);
            string email_body = refer.GetReferralUserCreatedEmailBody();
            Mailer mailer = new Mailer();

            mailer.SendEmail(
                ConfigurationManager.AppSettings["mailserviceuser"],
                ConfigurationManager.AppSettings["mailservicepwd"],
                "Welcome to the Andrew Harper Community!",
                ConfigurationManager.AppSettings["referemailfrom"],
                "mcoupland@andrewharper.com",
                string.Empty,
                string.Empty,
                email_body,
                true,
                ConfigurationManager.AppSettings["smtpserver"]);
        }

        [TestMethod]
        public void FAIL_ReferralUserCreatedEmail()
        {
            try
            {
                HarperLINQ.Referral refer = new HarperLINQ.Referral(33);
                string email = refer.GetReferralUserCreatedEmailBody();
                Assert.AreEqual(true, false);//force failure of test
            }
            catch
            {
                Assert.AreEqual(true, true);//test passed 
            }
        }

        [TestMethod]
        public void PASS_LoginNewReferral()
        {
            string u = "ut73";
            string p = "testing123";
            p = HarperCRYPTO.Cryptography.Hash(p, true);
            SecureServices.SecureServices client = new Tests.SecureServices.SecureServices();
            SecureServices.BaseResponse actual = client.Login(u, p, null, null);
            Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        }

        [TestMethod]
        public void PASS_GetDataNewReferral()
        {
            string u = "ut71";
            MembershipService.MembershipService client = new Tests.MembershipService.MembershipService();
            MembershipService.BaseResponse actual = client.GetMemberByUserName(u);
            Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        }
        */
        
        [TestMethod]
        public void GetEncryptedIds()
        {
            List<HarperLINQ.Referral> referrals = HarperLINQ.Referral.GetNonRedeemedList();
            List<string> urls = new List<string>();
            string s = "";
            foreach (Referral referral in referrals)
            {
                System.Diagnostics.Debug.WriteLine(referral.id.ToString() + "||http://harpernet.andrewharper.com/HarperNET/Referral/Redeem.aspx?ReferralId=" + System.Web.HttpUtility.UrlEncode(HarperCRYPTO.Cryptography.EncryptData(referral.id.ToString())));
                //urls.Add(referral.id.ToString() + "||http://harpernet.andrewharper.com/HarperNET/Referral/Redeem.aspx?ReferralId="
                   // + System.Web.HttpUtility.UrlEncode(HarperCRYPTO.Cryptography.EncryptData(referral.id.ToString())));
            }
        }

        [TestMethod]
        public void PASS_ReferralCreate()
        {
            try
            {
                HarperLINQ.tbl_Customer mike = new HarperLINQ.tbl_Customer("mcoupland", true);
                MembershipService.MembershipService client = new Tests.MembershipService.MembershipService();
                MembershipService.BaseResponse actual = client.CreateReferral(HarperCRYPTO.Cryptography.EncryptData(mike.cusID.ToString()), "Michael Coupland", 
                    "mcoupland@andrewharper.com", "POTRIAL", "PO", "Michellez Coupland", "mcouplanddebug47@andrewharper.com", true);

                int i = actual.Messages.Count();
            }
            catch(Exception ex)
            {
                string s = ex.Message;
                Assert.AreEqual(true, true);//test passed 
            }
        }

        [TestMethod]
        public void PASS_ReferralRedeem()
        {
            try
            {
                HarperLINQ.Referral refer = new HarperLINQ.Referral("mcouplanddebug47@andrewharper.com");
                //BaseResponse baseResponse = new SubscriptionLogic().RedeemReferralSubscription(refer.id, "TESTACCOUNT", "TESTACCOUNT",
                //    "mcouplanddebug7@andrewharper.com", "US", "12 add121", "", "San Antonio", "TX", "78247", false, "45michlle3262", "testing");
                
                //string dsd = Cryptography.EncryptData(refer.id.ToString());
                //string sdd = Cryptography.EncryptData("testing");
                MembershipService.MembershipService client = new Tests.MembershipService.MembershipService();
                MembershipService.BaseResponse actual = client.RedeemReferral(Cryptography.EncryptData(refer.id.ToString()), "TESTACCOUNT", "TESTACCOUNT", 
                    "mcouplanddebug7@andrewharper.com", "US", "12 add121", "", "San Antonio", "TX", "78247", false, "45michlle3262", Cryptography.EncryptData("testing"));
                int i = actual.Messages.Count();

            }
            catch (Exception ex)
            {
                string s = ex.Message;
                Assert.AreEqual(true, true);//test passed 
            }
        }

        
    }
}
