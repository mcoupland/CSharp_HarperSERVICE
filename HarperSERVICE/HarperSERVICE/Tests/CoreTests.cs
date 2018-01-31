using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupportClasses;
using System;
using System.Collections.Generic;
using HarperCRYPTO;

namespace Tests
{   
    /// <summary>
    ///This is a test class for MembershipLogicTest and is intended
    ///to contain all MembershipLogicTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CoreTests
    {
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


        [TestMethod()]
        public void EncryptId()
        {
            List<string> users = new List<string>();
            users.Add("tmrdoe");
            users.Add("discovery");
            users.Add("premier1");
            users.Add("standard");
            users.Add("premieronline");
            users.Add("jmoon");
            foreach (string user in users)
            {
                HarperLINQ.tbl_Customer cus = new HarperLINQ.tbl_Customer(user, true);
                System.Diagnostics.Debug.WriteLine(string.Format("USER: {0}, AUCTION URL: https://harpernet.andrewharper.com/HarperNET/Auctions/Default.aspx?MemberId={1}", cus.cusUserName, HarperCRYPTO.Cryptography.Encrypt256(cus.cusID.ToString())));
            }
            Assert.AreEqual(true, true);
        }

        [TestMethod()]
        public void DecryptValue()
        {
            //string decrypted = HarperCRYPTO.Cryptography.DecryptData("8018cbeb766e58aee801223b6514f4da");
            string decrypted2 = HarperCRYPTO.Cryptography.Decrypt256FromHEX("eafe9fecd06be83454848dda1acb5770");
        }

        [TestMethod()]
        public void HashId()
        {
            string hashid = HarperCRYPTO.Cryptography.Hash("10000948869");
            Assert.AreEqual(true, true);
        }

        #region standard best use-case scenario tests

        //[TestMethod]//working 2/7/2011
        //public void ChargeCard()
        //{
        //    BaseResponse actual = new CreditCardLogic().ChargeCard(null, updatedsalutation, 
        //        updatedfirstname, updatedmiddleinitial, updatedlastname, updatedsuffix, updatedprofessionaltitle,
        //        updatedemail, true, updatedbusinessname, updatedaddress1, updatedaddress2, updatedaddress3,
        //        updatedcity, updatedstate, updatedpostalcode, updatedcountry, updatedphone, updatedfax,
        //        updatedaltcity, goodamex, short.Parse(ccexpmonth), short.Parse(ccexpyear), amountpaid, ccname, ccaddress, cccity,
        //        ccstate, ccpostalcode, pubcode, updatedusername, updatedpwd, false);
        //    Assert.AreEqual((actual != null && actual.TypedResponse != null && !string.IsNullOrEmpty((actual.TypedResponse as CreditCardServiceResponse).VerifoneRoutingId)), true);            
        //}

        //[TestMethod()]//working 2/7/2011
        //public void CreateSubscription()
        //{
        //    BaseResponse ccresponse = new CreditCardLogic().ChargeCard(null, updatedsalutation,
        //        updatedfirstname, updatedmiddleinitial, updatedlastname, updatedsuffix, updatedprofessionaltitle,
        //        updatedemail, true, updatedbusinessname, updatedaddress1, updatedaddress2, updatedaddress3,
        //        updatedcity, updatedstate, updatedpostalcode, updatedcountry, updatedphone, updatedfax,
        //        updatedaltcity, goodamex, short.Parse(ccexpmonth), short.Parse(ccexpyear), amountpaid, ccname, ccaddress, cccity,
        //        ccstate, ccpostalcode, pubcode, updatedusername, updatedpwd, false);

        //    if (ccresponse != null && ccresponse.TypedResponse != null && !string.IsNullOrEmpty((ccresponse.TypedResponse as CreditCardServiceResponse).VerifoneRoutingId))
        //    {
        //        verifoneroutingid = (ccresponse.TypedResponse as CreditCardServiceResponse).VerifoneRoutingId;
        //        BaseResponse actual = new SubscriptionLogic().CreateSubscription(subscriptionlength,
        //            amountpaid, verifoneroutingid, pubcode, keycode, null, updatedsalutation,
        //            updatedfirstname, updatedmiddleinitial, updatedlastname, updatedsuffix, updatedprofessionaltitle,
        //            updatedemail, true, updatedbusinessname, updatedaddress1, updatedaddress2, updatedaddress3,
        //            updatedcity, updatedstate, updatedpostalcode, updatedcountry, updatedphone, updatedfax,
        //            updatedaltcity, giftflag, null, giftsalutation,
        //            giftfirstname, giftmiddleinitial, giftlastname, giftsuffix, giftprofessionaltitle,
        //            giftemail, true, giftbusinessname, giftaddress1, giftaddress2, giftaddress3,
        //            giftcity, giftstate, giftpostalcode, giftcountry, giftphone, giftfax,
        //            giftaltcity);
        //        Assert.AreEqual((actual != null && actual.TypedResponse != null && !string.IsNullOrEmpty((actual.TypedResponse as SubscriptionServiceResponse).MemberId)), true);
        //    }
        //}

        //[TestMethod()]//working 2/7/2011
        //public void CreateLogin()
        //{            
        //    BaseResponse ccresponse = new CreditCardLogic().ChargeCard(null, updatedsalutation,
        //        updatedfirstname, updatedmiddleinitial, updatedlastname, updatedsuffix, updatedprofessionaltitle,
        //        "edwinh@testing123.com", true, updatedbusinessname, updatedaddress1, updatedaddress2, updatedaddress3,
        //        updatedcity, updatedstate, updatedpostalcode, updatedcountry, updatedphone, updatedfax,
        //        updatedaltcity, goodamex, short.Parse(ccexpmonth), short.Parse(ccexpyear), amountpaid, ccname, ccaddress, cccity,
        //        ccstate, ccpostalcode, pubcode, updatedusername, updatedpwd, false);

        //    if (ccresponse != null && ccresponse.TypedResponse != null && !string.IsNullOrEmpty((ccresponse.TypedResponse as CreditCardServiceResponse).VerifoneRoutingId))
        //    {
        //        verifoneroutingid = (ccresponse.TypedResponse as CreditCardServiceResponse).VerifoneRoutingId;
        //        BaseResponse createsubresponse = new SubscriptionLogic().CreateSubscription(subscriptionlength,
        //            amountpaid, verifoneroutingid, pubcode, keycode, null, updatedsalutation,
        //            updatedfirstname, updatedmiddleinitial, updatedlastname, updatedsuffix, updatedprofessionaltitle,
        //            "edwinh@testing123.com", true, updatedbusinessname, updatedaddress1, updatedaddress2, updatedaddress3,
        //            updatedcity, updatedstate, updatedpostalcode, updatedcountry, updatedphone, updatedfax,
        //            updatedaltcity, giftflag, null, giftsalutation,
        //            giftfirstname, giftmiddleinitial, giftlastname, giftsuffix, giftprofessionaltitle,
        //            giftemail, true, giftbusinessname, giftaddress1, giftaddress2, giftaddress3,
        //            giftcity, giftstate, giftpostalcode, giftcountry, giftphone, giftfax,
        //            giftaltcity);
        //        if (createsubresponse != null && createsubresponse.TypedResponse != null
        //            && !string.IsNullOrEmpty((createsubresponse.TypedResponse as SubscriptionServiceResponse).MemberId))
        //        {
        //            memberid = (createsubresponse.TypedResponse as SubscriptionServiceResponse).MemberId;
        //            BaseResponse actual = new MembershipLogic().CreateLogin(memberid, updatedusername, updatedpwd, updatedpostalcode);                
        //            Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
        //            Assert.AreEqual((actual.TypedResponse as CreateLoginResponse).Success, true);                
        //        }
        //    }
        //}
        #endregion

        #region optional scenarios with good data
        [TestMethod()]
        public void ApproveCC()
        {
            throw new NotImplementedException();
            //CreditCardLogic target = new CreditCardLogic();

            #region set cc data
            CreditCard creditCardData = new CreditCard();
            creditCardData.CCAddress = ccaddress;
            creditCardData.CCCity = cccity;
            creditCardData.CCExpMonth = short.Parse(ccexpmonth);
            creditCardData.CCExpYear = short.Parse(ccexpyear);
            creditCardData.CCName = ccname;
            creditCardData.CCPostalCode = ccpostalcode;
            creditCardData.CCState = ccstate;
            #endregion

            #region set member data
            Member memberData = new Member();
            memberData.Salutation = updatedsalutation;
            memberData.FirstName = updatedfirstname;
            memberData.MiddleInitial = updatedmiddleinitial;
            memberData.LastName = updatedlastname;
            memberData.Suffix = updatedsuffix;
            memberData.ProfessionalTitle = updatedprofessionaltitle;
            memberData.Email = updatedemail;
            memberData.OptIn = updatedoptin;
            memberData.Address.BusinessName = updatedbusinessname;
            memberData.Address.Address1 = updatedaddress1;
            memberData.Address.Address2 = updatedaddress2;
            memberData.Address.Address3 = updatedaddress3;
            memberData.Address.City = updatedcity;
            memberData.Address.State = updatedstate;
            memberData.Address.PostalCode = updatedpostalcode;
            memberData.Address.Country = updatedcountry;
            memberData.Address.Phone = updatedphone;
            memberData.Address.Fax = updatedfax;
            memberData.Address.AltCity = updatedaltcity;
            #endregion

            //creditCardData.CCNumber = goodamex;
            //BaseResponse actual = target.ChargeCard(creditCardData, memberData, amountpaid, debug);
            //Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
            //verifoneroutingid = ((ChargeCardResponse)actual.TypedResponse).VerifoneRoutingId;

            //creditCardData.CCNumber = gooddisc;
            //actual = target.ChargeCard(creditCardData, memberData, amountpaid, debug);
            //Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
            //verifoneroutingid = ((ChargeCardResponse)actual.TypedResponse).VerifoneRoutingId;

            //creditCardData.CCNumber = goodmc;
            //actual = target.ChargeCard(creditCardData, memberData, amountpaid, debug);
            //Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
            //verifoneroutingid = ((ChargeCardResponse)actual.TypedResponse).VerifoneRoutingId;

            //creditCardData.CCNumber = goodvisa;
            //actual = target.ChargeCard(creditCardData, memberData, amountpaid, debug);
            //Assert.AreEqual(true, actual != null && actual.TypedResponse != null && actual.TypedResponse.Success);
            //verifoneroutingid = ((ChargeCardResponse)actual.TypedResponse).VerifoneRoutingId;
        }

        //[TestMethod()]
        //public void DeclineCC()
        //{
        //    CreditCardLogic target = new CreditCardLogic();

        //    #region set cc data
        //    CreditCard creditCardData = new CreditCard();
        //    creditCardData.CCAddress = ccaddress;
        //    creditCardData.CCCity = cccity;
        //    creditCardData.CCExpMonth = short.Parse(ccexpmonth);
        //    creditCardData.CCExpYear = short.Parse(ccexpyear);
        //    creditCardData.CCName = ccname;
        //    creditCardData.CCPostalCode = ccpostalcode;
        //    creditCardData.CCState = ccstate;
        //    #endregion

        //    #region set member data
        //    Member memberData = new Member();
        //    memberData.Salutation = updatedsalutation;
        //    memberData.FirstName = updatedfirstname;
        //    memberData.MiddleInitial = updatedmiddleinitial;
        //    memberData.LastName = updatedlastname;
        //    memberData.Suffix = updatedsuffix;
        //    memberData.ProfessionalTitle = updatedprofessionaltitle;
        //    memberData.Email = updatedemail;
        //    memberData.OptIn = updatedoptin;
        //    memberData.Address.BusinessName = updatedbusinessname;
        //    memberData.Address.Address1 = updatedaddress1;
        //    memberData.Address.Address2 = updatedaddress2;
        //    memberData.Address.Address3 = updatedaddress3;
        //    memberData.Address.City = updatedcity;
        //    memberData.Address.State = updatedstate;
        //    memberData.Address.PostalCode = updatedpostalcode;
        //    memberData.Address.Country = updatedcountry;
        //    memberData.Address.Phone = updatedphone;
        //    memberData.Address.Fax = updatedfax;
        //    memberData.Address.AltCity = updatedaltcity;
        //    #endregion

        //    creditCardData.CCNumber = badamex;

        //    throw new NotImplementedException(); 
        //    //BaseResponse actual = target.ChargeCard(creditCardData, memberData, amountpaid, debug);
        //    //Assert.AreEqual(true, actual != null && actual.TypedResponse != null
        //    //    && !actual.TypedResponse.Success
        //    //    && actual.Messages != null
        //    //    && actual.Messages.Count >= 1
        //    //    && actual.Messages[0].SfgCode == "135");


        //    //creditCardData.CCNumber = baddisc;
        //    //actual = target.ChargeCard(creditCardData, memberData, amountpaid, debug);
        //    //Assert.AreEqual(true, actual != null && actual.TypedResponse != null
        //    //    && !actual.TypedResponse.Success
        //    //    && actual.Messages != null
        //    //    && actual.Messages.Count >= 1
        //    //    && actual.Messages[0].SfgCode == "135");


        //    //creditCardData.CCNumber = badmc;
        //    //actual = target.ChargeCard(creditCardData, memberData, amountpaid, debug);
        //    //Assert.AreEqual(true, actual != null && actual.TypedResponse != null
        //    //    && !actual.TypedResponse.Success
        //    //    && actual.Messages != null
        //    //    && actual.Messages.Count >= 1
        //    //    && actual.Messages[0].SfgCode == "135");


        //    //creditCardData.CCNumber = badvisa;
        //    //actual = target.ChargeCard(creditCardData, memberData, amountpaid, debug);
        //    //Assert.AreEqual(true, actual != null && actual.TypedResponse != null
        //    //    && !actual.TypedResponse.Success
        //    //    && actual.Messages != null
        //    //    && actual.Messages.Count >= 1
        //    //    && actual.Messages[0].SfgCode == "135");
        //}
        #endregion

        #region New order end to end test
        //[TestMethod]
        //public void TestNewOrderComplete()
        //{
        //    memberid = null;
        //    string[] ret = ApproveCC(goodamex);
        //    string verifoneid = ret[0];
        //    string authid = ret[1];
        //    if (verifoneid != null && authid != null)
        //    {
        //        string fname = verifoneid.ToString();
        //        string lname = verifoneid.ToString();
        //        string add1 = verifoneid.ToString();
        //        string mbrid = CreateSubscription(fname, lname, add1, verifoneid);
        //        if (mbrid != null)
        //        {
        //            BaseResponse mbrresponse = GetMemberByMemberId(mbrid);
        //            if (mbrresponse != null && mbrresponse.TypedResponse != null && mbrresponse.TypedResponse.Success)
        //            {
        //                GetMemberResponse mbr = (GetMemberResponse)mbrresponse.TypedResponse;

        //                if (CreateLogin(updatedaddress1, updatedaddress2, updatedaddress3, updatedcity, updatedstate, updatedcountry, updatedpostalcode, 
        //                        "TEST", updatedpwd, "PERSONAL", updatedsalutation, updatedfirstname, updatedmiddleinitial, updatedlastname, updatedsuffix,
        //                        updatedemail, updatedusername, mbr.MemberData.MemberId, pubcode, DateTime.Now.AddYears(1), DateTime.Now) 
        //                    > 0)
        //                {
        //                    CustomerService.CustomerService client = new Tests.CustomerService.CustomerService();

        //                    string enc_username = HarperCRYPTO.Cryptography.Encrypt256(updatedusername);
        //                    string enc_password = HarperCRYPTO.Cryptography.Encrypt256(updatedpwd);

        //                    int expected = 0;
        //                    Tests.CustomerService.ResponseObject actual = client.Login(enc_username, enc_password, true, false);
        //                    Assert.AreEqual(expected, actual.ResponseCode);
        //                }
        //            }
        //        }
        //    }
        //}

        #region methods used by TestNewOrderComplete
        public bool doPing()
        {
            HeartbeatLogic target = new HeartbeatLogic();
            BaseResponse actual = target.Ping();
            return actual != null && actual.TypedResponse != null && actual.TypedResponse.Success;
        }
        //public string[] ApproveCC(string goodcc)
        //{
        //    CreditCardLogic target = new CreditCardLogic();
        //    BaseResponse actual = target.ChargeCard(memberid, updatedsalutation, updatedfirstname, updatedmiddleinitial, updatedlastname, updatedsuffix,
        //                                             updatedprofessionaltitle, updatedemail, updatedoptin, updatedbusinessname, updatedaddress1, updatedaddress2, updatedaddress3,
        //                                             updatedcity, updatedstate, updatedpostalcode, updatedcountry,
        //                                             updatedphone, updatedfax, updatedaltcity,
        //                                             goodcc, short.Parse(ccexpmonth), short.Parse(ccexpyear), amountpaid, 
        //                                             ccname,  ccaddress,  cccity,
        //                                             ccstate,  ccpostalcode,  pubcode, updatedusername, updatedpwd, false);
        //    if (actual != null && actual.TypedResponse != null && actual.TypedResponse.Success)
        //    {
        //        string[] returnData = new string[]{
        //                    ((CreditCardServiceResponse)actual.TypedResponse).VerifoneRoutingId,
        //                    ((CreditCardServiceResponse)actual.TypedResponse).AuthorizationCode
        //                };
        //        return returnData;
        //    }
        //    return null;
        //}
        public string CreateSubscription(string fname, string lname, string add1, string verifoneroutingid)
        {
            SubscriptionLogic target = new SubscriptionLogic();
            BaseResponse actual = target.CreateSubscription(subscriptionlength, 
                                                             amountpaid,  verifoneroutingid, 
                                                             pubcode,  keycode,
                                                             null, updatedsalutation, fname, updatedmiddleinitial, lname, updatedsuffix,
                                                             updatedprofessionaltitle, updatedemail, updatedoptin, updatedbusinessname, updatedaddress1, updatedaddress2, updatedaddress3,
                                                             updatedcity, updatedstate, updatedpostalcode, updatedcountry,
                                                             updatedphone, updatedfax, updatedaltcity,  
                                                             false,
                                                             null, null, null, null, null, null,
                                                             null, null, false, null, null, null, null,
                                                             null, null, null, null,
                                                             null, null, null);
            /*  giftflag, 
                renewinggiftmemberid,  giftsalutation,  giftfirstname,  giftmiddleinitial,  giftlastname,  giftsuffix,
                giftprofessionaltitle,  giftemail, giftoptin,  giftbusinessname,  giftaddress1,  giftaddress2,  giftaddress3,
                giftcity,  giftstate,  giftpostalcode,  giftcountry,
                giftphone,  giftfax,  giftaltcity
             */

            if (actual != null && actual.TypedResponse != null && actual.TypedResponse.Success)
            {
                return ((SubscriptionServiceResponse)actual.TypedResponse).MemberId;
            }
            return null;
        }
        public BaseResponse GetMemberByMemberId(string mbrid)
        {
            MembershipLogic target = new MembershipLogic();
            return target.GetMemberById(mbrid, true);
            //return actual != null && actual.TypedResponse != null && actual.TypedResponse.Success;
        }
        public object[] CreateLogin(string address1, string address2, string address3, string city, string region, string country, string postal, string source,
            string password, string customertype, string prefix, string firstname, string middleinitial, string lastname, string suffix, string emailaddress, string username, string newmemberid, 
            string pubcode, DateTime expiredate, DateTime startdate)
        {
            return HarperLINQ.tbl_Customer.CreateCustomer(address1, address2, address3, city, region, country, postal, source,
                password, customertype, prefix, firstname, middleinitial, lastname, suffix, emailaddress, username, newmemberid, 
                pubcode, expiredate.ToShortDateString(), startdate.ToShortDateString(), username, "");
        }
        public BaseResponse GetMemberByUsername(string user)
        {
            MembershipLogic target = new MembershipLogic();
            BaseResponse actual = target.GetMemberByUserName(user);
            return actual;
        }
        //public bool Login(string user, string pwd)
        //{
        //    MembershipLogic target = new MembershipLogic();
        //    BaseResponse actual = target.Login(user, pwd, "", "");
        //    return actual != null && actual.TypedResponse != null && actual.TypedResponse.Success;
        //}
        public bool UpdateUsername(string nu, string iu, string ip)
        {
            MembershipLogic target = new MembershipLogic();
            BaseResponse actual = target.UpdateUsername(nu, iu, ip, debug);
            return actual != null && actual.TypedResponse != null && actual.TypedResponse.Success;
        }
        public bool UpdatePassword(string nu, string np)
        {
            MembershipLogic target = new MembershipLogic();
            BaseResponse actual = target.UpdatePassword(nu, np, debug);
            return actual != null && actual.TypedResponse != null && actual.TypedResponse.Success;
        }
        #endregion

        #endregion

        #region cleanup tasks 
        [TestCleanup()]
        public void MyTestCleanup()
        {
            //resetUsername();
            //resetPassword();
            //resetMember();
        }
        private static void resetUsername()
        {
            //MembershipLogic target = new MembershipLogic();
            //target.UpdateUsername(updatedusername, originalusername, originalpwd, debug);
        }
        private static void resetPassword()
        {
            //MembershipLogic target = new MembershipLogic();
            //target.UpdatePassword(updatedusername, updatedpwd, debug);            
        }
        private static void resetMember()
        {
            //MembershipLogic target = new MembershipLogic(); // TODO: Initialize to an appropriate value
            //Member memberToUpdate = new Member();
            //memberToUpdate.MemberId = memberid;
            //memberToUpdate.Salutation = updatedsalutation;
            //memberToUpdate.FirstName = updatedfirstname;
            //memberToUpdate.MiddleInitial = updatedmiddleinitial;
            //memberToUpdate.LastName = updatedlastname;
            //memberToUpdate.Suffix = updatedsuffix;
            //memberToUpdate.ProfessionalTitle = updatedprofessionaltitle;
            //memberToUpdate.Email = updatedemail;
            //memberToUpdate.OptIn = updatedoptin;
            //memberToUpdate.Address.BusinessName = updatedbusinessname;
            //memberToUpdate.Address.Address1 = updatedaddress1;
            //memberToUpdate.Address.Address2 = updatedaddress2;
            //memberToUpdate.Address.Address3 = updatedaddress3;
            //memberToUpdate.Address.City = updatedcity;
            //memberToUpdate.Address.State = updatedstate;
            //memberToUpdate.Address.PostalCode = updatedpostalcode;
            //memberToUpdate.Address.Country = updatedcountry;
            //memberToUpdate.Address.Phone = updatedphone;
            //memberToUpdate.Address.Fax = updatedfax;
            //memberToUpdate.Address.AltCity = updatedaltcity;
            //bool optIn = updatedoptin;
            //target.UpdateMember(memberid, originalsalutation, originalfirstname, originalmiddleinitial, originallastname, originalsuffix,
            //    originalprofessionaltitle, originalemail, originaloptin,
            //    originalbusinessname, originaladdress1, originaladdress2, originaladdress3, originalcity, originalstate, originalpostalcode, originalcountry,
            //    originalphone, originalfax, originalaltcity,
            //    debug);
        }
        #endregion

        private static string GetTrimmedString(string input, int maxlen)
        {
            return input.Length > maxlen ? input.Substring(0, maxlen) : input;
        }
    }
}
