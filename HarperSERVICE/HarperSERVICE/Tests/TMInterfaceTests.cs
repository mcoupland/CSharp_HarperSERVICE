using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupportClasses;
using System;

namespace Tests
{
    [TestClass()]
    public class TMInterfaceTests
    {
        private CoreTests coreTests = new CoreTests();

        #region set test data
        public static bool debug = CoreTests.debug;
        public static string nosuboffermemberid = CoreTests.nosuboffermemberid;
        public static string suboffermemberid = CoreTests.suboffermemberid;
        public static string memberid = CoreTests.memberid;

        #region subscription values
        public static string pubcode = CoreTests.pubcode;
        public static bool giftflag = CoreTests.giftflag;
        public static bool refreshcustomer = CoreTests.refreshcustomer;
        public static int subscriptionlength = CoreTests.subscriptionlength;
        public static float price = CoreTests.price;
        public static float postage = CoreTests.postage;
        public static float tax = CoreTests.tax;
        public static float amountpaid = CoreTests.amountpaid;
        public static string keycode = CoreTests.keycode;
        public static string verifoneroutingid = CoreTests.verifoneroutingid;
        public static long giftid = CoreTests.giftid;
        public static string giftsalutation = CoreTests.giftsalutation;
        public static string giftfirstname = CoreTests.giftfirstname;
        public static string giftmiddleinitial = CoreTests.giftmiddleinitial;
        public static string giftlastname = CoreTests.giftlastname;
        public static string giftsuffix = CoreTests.giftsuffix;
        public static string giftprofessionaltitle = CoreTests.giftprofessionaltitle;
        public static string giftemail = CoreTests.giftemail;
        public static string giftfax = CoreTests.giftfax;
        public static bool giftoptin = CoreTests.giftoptin;
        public static string giftbusinessname = CoreTests.giftbusinessname;
        public static string giftaddress1 = CoreTests.giftaddress1;
        public static string giftaddress2 = CoreTests.giftaddress2;
        public static string giftaddress3 = CoreTests.giftaddress3;
        public static string giftcity = CoreTests.giftcity;
        public static string giftstate = CoreTests.giftstate;
        public static string giftpostalcode = CoreTests.giftpostalcode;
        public static string giftcountry = CoreTests.giftcountry;
        public static string giftphone = CoreTests.giftphone;
        public static string giftaltcity = CoreTests.giftaltcity;
        #endregion

        #region cc values
        public static string transactiontype = CoreTests.transactiontype;
        public static string ccaddress = CoreTests.ccaddress;
        public static string cccity = CoreTests.cccity;
        public static string ccpostalcode = CoreTests.ccpostalcode;
        public static string ccstate = CoreTests.ccstate;

        public static string ccexpmonth = CoreTests.ccexpmonth;
        public static string ccexpyear = CoreTests.ccexpyear;
        public static string ccname = CoreTests.ccname;
        public static string goodvisa = CoreTests.goodvisa;
        public static string goodmc = CoreTests.goodmc;
        public static string goodamex = CoreTests.goodamex;
        public static string gooddisc = CoreTests.gooddisc;

        public static string badvisa = CoreTests.badvisa;
        public static string badmc = CoreTests.badmc;
        public static string badamex = CoreTests.badamex;
        public static string baddisc = CoreTests.baddisc;
        #endregion

        #region original values
        public static string originalusername = CoreTests.originalusername;
        public static string originalpwd = CoreTests.originalpwd;
        public static string originalsalutation = CoreTests.originalsalutation;
        public static string originalfirstname = CoreTests.originalfirstname;
        public static string originalmiddleinitial = CoreTests.originalmiddleinitial;
        public static string originallastname = CoreTests.originallastname;
        public static string originalsuffix = CoreTests.originalsuffix;
        public static string originalprofessionaltitle = CoreTests.originalprofessionaltitle;
        public static string originalemail = CoreTests.originalemail;
        public static bool originaloptin = CoreTests.originaloptin;
        public static string originalbusinessname = CoreTests.originalbusinessname;
        public static string originaladdress1 = CoreTests.originaladdress1;
        public static string originaladdress2 = CoreTests.originaladdress2;
        public static string originaladdress3 = CoreTests.originaladdress3;
        public static string originalcity = CoreTests.originalcity;
        public static string originalstate = CoreTests.originalstate;
        public static string originalpostalcode = CoreTests.originalpostalcode;
        public static string originalcountry = CoreTests.originalcountry;
        public static string originalphone = CoreTests.originalphone;
        public static string originalfax = CoreTests.originalfax;
        public static string originalaltcity = CoreTests.originalaltcity;
        #endregion

        #region updated values
        public static string updatedusername = CoreTests.updatedusername;
        public static string updatedpwd = CoreTests.updatedpwd;
        public static string updatedsalutation = CoreTests.updatedsalutation;
        public static string updatedfirstname = CoreTests.updatedfirstname;
        public static string updatedmiddleinitial = CoreTests.updatedmiddleinitial;
        public static string updatedlastname = CoreTests.updatedlastname;
        public static string updatedsuffix = CoreTests.updatedsuffix;
        public static string updatedprofessionaltitle = CoreTests.updatedprofessionaltitle;
        public static string updatedemail = CoreTests.updatedemail;
        public static bool updatedoptin = CoreTests.updatedoptin;
        public static string updatedbusinessname = CoreTests.updatedbusinessname;
        public static string updatedaddress1 = CoreTests.updatedaddress1;
        public static string updatedaddress2 = CoreTests.updatedaddress2;
        public static string updatedaddress3 = CoreTests.updatedaddress3;
        public static string updatedcity = CoreTests.updatedcity;
        public static string updatedstate = CoreTests.updatedstate;
        public static string updatedpostalcode = CoreTests.updatedpostalcode;
        public static string updatedcountry = CoreTests.updatedcountry;
        public static string updatedphone = CoreTests.updatedphone;
        public static string updatedfax = CoreTests.updatedfax;
        public static string updatedaltcity = CoreTests.updatedaltcity;
        #endregion
        #endregion

        #region VERSION 1 METHODS - Test with good data
        [TestMethod]
        public void SaveMemberProfile()
        {
            BaseResponse original = new MembershipLogic().GetMemberByMemberId(memberid);
            BaseResponse actual = new MembershipLogic().UpdateMemberProfile(memberid,
                updatedsalutation, updatedfirstname, updatedmiddleinitial, updatedlastname, updatedsuffix,
                updatedprofessionaltitle, updatedemail, updatedoptin, updatedbusinessname, updatedaddress1, updatedaddress2, updatedaddress3,
                updatedcity, updatedstate, updatedpostalcode, updatedcountry,
                updatedphone, updatedfax, updatedaltcity,
                debug);
            BaseResponse updated = new MembershipLogic().GetMemberByMemberId(memberid);
            coreTests.MyTestCleanup();
            if (actual != null && actual.TypedResponse != null && actual.TypedResponse.Success)
            {
                Assert.AreEqual((actual.TypedResponse as UpdateMemberResponse).MemberUpdated, true);
                Assert.AreEqual((original.TypedResponse as GetMemberResponse).MemberData.FirstName ==
                                (updated.TypedResponse as GetMemberResponse).MemberData.FirstName, false);
            }
            else { Assert.AreEqual(1, 0); };
        }

        [TestMethod]
        public void GetMemberProfile()
        {
            BaseResponse actual = new MembershipLogic().GetMemberProfile(memberid, debug);
            if (actual != null && actual.TypedResponse != null && actual.TypedResponse.Success)
            {
                Assert.AreEqual((actual.TypedResponse as GetMemberResponse).MemberData.FirstName == originalfirstname, true);
            }
            else { Assert.AreEqual(1, 0); };
        }

        [TestMethod]
        public void GetSubscriptionOffers()
        {
            BaseResponse actual = new MembershipLogic().GetSubscriptionOffers(suboffermemberid, debug);
            if (actual != null && actual.TypedResponse != null && actual.TypedResponse.Success)
            {
                Assert.AreEqual((actual.TypedResponse as GetMemberResponse).RenewalOffers == null, false);
            }
            else { Assert.AreEqual(1, 0); };
        }
        
        [TestMethod]
        public void GetCurrentSubscription()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetSubscriptionHistory()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetMemberContactData()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetPrimaryMemberContactData()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PlaceNewOrder()
        {
            throw new NotImplementedException();
            //SubscriptionLogic target = new SubscriptionLogic();
            //string uniqueusername = "";
            //BaseResponse actual = target.CreateSubscription();

            //Assert.AreEqual(actual == null, false);
            //Assert.AreEqual(actual.TypedResponse == null, false);
            //Assert.AreEqual(actual.TypedResponse.Success, true);
            //Assert.AreEqual(coreTests.GetMemberByUsername(updatedusername), true);
            //Assert.AreEqual(string.IsNullOrEmpty((actual.TypedResponse as CreateSubscriptionResponse).MemberId), false);
        }

        [TestMethod]
        public void PlaceOnlineRenewal()
        {
            throw new NotImplementedException();
            //SubscriptionLogic target = new SubscriptionLogic();
            //BaseResponse original = new MembershipLogic().GetMemberByMemberId(suboffermemberid, debug);
            //BaseResponse actual = target.CreateSubscription();
            //BaseResponse updated = new MembershipLogic().GetMemberByMemberId(suboffermemberid, debug);
            //if (actual != null && actual.TypedResponse != null && actual.TypedResponse.Success)
            //{
            //    Assert.AreEqual(string.IsNullOrEmpty((actual.TypedResponse as CreateSubscriptionResponse).MemberId), false);
            //}
        }

        [TestMethod]
        public void ForgotPassword()
        {
            throw new NotImplementedException();
        }               

        [TestMethod]
        public void CreateProspectMember()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateCompMember()
        {
            throw new NotImplementedException();
        }
        
        [TestMethod]
        public void PlaceNewGiftOrder()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region METHODS THAT WILL NOT IMPLEMENTED IN VERSION 1
        public void PlaceOnlineGiftRenewalOrder()
        {
            throw new NotImplementedException();
        }

        public void ProcessEmailRenewal() 
        { 
            throw new NotImplementedException();
        }
        #endregion

        #region optional scenarios with good data
        [TestMethod]
        public void OnlineRenewalDoNotUpdateMemberData()
        {
            throw new NotImplementedException();
            //SubscriptionLogic target = new SubscriptionLogic();
            //BaseResponse original = new MembershipLogic().GetMemberByMemberId(suboffermemberid, debug);
            //BaseResponse actual = target.CreateSubscription();
            //BaseResponse updated = new MembershipLogic().GetMemberByMemberId(suboffermemberid, debug);
            //if (actual != null && actual.TypedResponse != null && actual.TypedResponse.Success)
            //{
            //    Assert.AreEqual(string.IsNullOrEmpty((actual.TypedResponse as CreateSubscriptionResponse).MemberId), false);
            //}
        }

        [TestMethod]
        public void OnlineRenewalUpdateMemberData()
        {
            throw new NotImplementedException();
            //BaseResponse original = new MembershipLogic().GetMemberByMemberId(suboffermemberid, debug);
            //BaseResponse actual = new SubscriptionLogic().CreateSubscription();
            //BaseResponse updated = new MembershipLogic().GetMemberByMemberId(suboffermemberid, debug);
            //if (actual != null && actual.TypedResponse != null && actual.TypedResponse.Success)
            //{
            //    Assert.AreEqual(string.IsNullOrEmpty((actual.TypedResponse as CreateSubscriptionResponse).MemberId), false);

            //}
        }
        #endregion
    }
}
