using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using SupportClasses;
using SFGWrapper;
using BusinessLogic;
using System.Xml;
using HarperCRYPTO;

namespace MemberServices
{
    //ALWAYS HASH
    //  - MEMBERID
    //  - CUSID
    //  - PASSWORD
    //  - CC DATA
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MembershipService : System.Web.Services.WebService
    {    
        [WebMethod]
        public BaseResponse Ping()
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = new HeartbeatLogic().Ping();                              

            }
            catch (Exception ex)
            {
                EventLogger.LogError("MembershipService.Ping",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }

        #region member login and mgmt
        /// <summary>
        /// Used by harpernet create username and password (at least)
        /// </summary>
        /// <param name="hashed_sfgid"></param>
        /// <returns></returns>
        [WebMethod]
        public BaseResponse GetMemberBySFGId(string hashed_sfgid)
        {
            bool debug = false;
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = new MembershipLogic().GetMemberById(Cryptography.DeHash(hashed_sfgid, true), true);

            }
            catch (Exception ex)
            {
                EventLogger.LogError("MembershipService.GetMemberBySFGId",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }

        /// <summary>
        /// not used in harpernet 
        /// </summary>
        /// <param name="hashed_cusid"></param>
        /// <returns></returns>
        [WebMethod]
        public BaseResponse GetMemberByCusId(string hashed_cusid)
        {
            bool debug = false;
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = new MembershipLogic().GetMemberById(Cryptography.DeHash(hashed_cusid, true), false);

            }
            catch (Exception ex)
            {
                EventLogger.LogError("MembershipService.GetMemberByCusId",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }

        /// <summary>
        /// used all over the place
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [WebMethod]
        public BaseResponse GetMemberByUserName(string username)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = new MembershipLogic().GetMemberByUserName(username);                      

            }
            catch (Exception ex)
            {
                EventLogger.LogError("MembershipService.GetMemberByUserName",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }
        
        /// <summary>
        /// used all over the place
        /// </summary>
        /// <param name="hashed_memberid"></param>
        /// <param name="salutation"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="suffix"></param>
        /// <param name="professionaltitle"></param>
        /// <param name="email"></param>
        /// <param name="optin"></param>
        /// <param name="businessname"></param>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="address3"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="postalcode"></param>
        /// <param name="country"></param>
        /// <param name="phone"></param>
        /// <param name="screenname"></param>
        /// <returns></returns>
        [WebMethod]
        public BaseResponse UpdateMember(string hashed_memberid,
            string salutation, string firstname, string lastname, string suffix,
            string professionaltitle, string email, bool optin, string businessname, string address1, 
            string address2, string address3,
            string city, string state, string postalcode, string country,
            string phone, string screenname)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = new MembershipLogic().UpdateMember(Cryptography.DeHash(hashed_memberid, true),
                    salutation, firstname, lastname, suffix,
                    professionaltitle, email, optin, businessname, address1, address2, address3,
                    city, state, postalcode, country,
                    phone, screenname,
                    false);
            }
            catch (Exception ex)
            {
                EventLogger.LogError("MembershipService.UpdateMember",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }
        #endregion


        #region refer a friend
        /// <summary>
        /// used in harpernet referral refer.aspx.cs
        /// </summary>
        /// <param name="enccusid"></param>
        /// <param name="membername"></param>
        /// <param name="memeberemail"></param>
        /// <param name="keycode"></param>
        /// <param name="pubcode"></param>
        /// <param name="friendname"></param>
        /// <param name="emailaddress"></param>
        /// <param name="ccmember"></param>
        /// <returns></returns>
        [WebMethod]
        public BaseResponse CreateReferral(string enccusid, string membername, string memeberemail, string keycode, string pubcode, string friendname, string emailaddress, bool ccmember)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                string cusid = Cryptography.DecryptData(enccusid);
                baseResponse = new MembershipLogic().CreateReferral(cusid, membername, memeberemail, keycode, pubcode, friendname, emailaddress, ccmember);
            }
            catch (Exception ex)
            {
                EventLogger.LogError("MembershipService.CreateReferral",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;            
        }

        /// <summary>
        /// used HarperNET\Referral\Redeem.aspx.cs
        /// </summary>
        /// <param name="encreferralid"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="emailaddress"></param>
        /// <param name="countrycode"></param>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="city"></param>
        /// <param name="region"></param>
        /// <param name="postal"></param>
        /// <param name="optin"></param>
        /// <param name="username"></param>
        /// <param name="encpassword"></param>
        /// <returns></returns>
        [WebMethod]
        public BaseResponse RedeemReferral(string encreferralid, string firstname, string lastname, 
            string emailaddress, string countrycode, string address1, string address2, string city, 
            string region, string postal, bool optin, string username, string encpassword)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                int referralid = int.Parse(Cryptography.DecryptData(encreferralid));
                string password = Cryptography.DecryptData(encpassword);
                baseResponse = new SubscriptionLogic().RedeemReferralSubscription(referralid, firstname, lastname, emailaddress, countrycode, address1, address2, city, region, postal, optin, username, password);
            }
            catch (Exception ex)
            {
                EventLogger.LogError("MembershipService.RedeemReferral",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }
        #endregion 

        [WebMethod]
        public BaseResponse StoreCompHRData()
        {
            throw new NotImplementedException();            
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                //baseResponse = new MembershipLogic().UpdateMember(memberid,
                //    salutation, firstname, middleinitial, lastname, suffix,
                //    professionaltitle, email, optin, businessname, address1, address2, address3,
                //    city, state, postalcode, country,
                //    phone, fax, altcity,
                //    debug);
            }
            catch (Exception ex)
            {
                EventLogger.LogError("MembershipService.StoreCompHRData",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }

        /*
        [WebMethod]
        public BaseResponse RequestRate(string firstname, string lastname,
            string hashedmemberid, string hotelname, string location,
            string requestedroom, string checkin, string checkout,
            string altcheckin, string altcheckout, string requesting,
            string phone, string email, string comments) 
        {

            BaseResponse baseResponse = new BaseResponse();
            try
            {
                string cusid = Cryptography.DeHash(hashedmemberid, true);
                baseResponse = new MembershipLogic().RequestRate(
                    firstname, lastname,
                    hashedmemberid, hotelname, location,
                    requestedroom, checkin, checkout,
                    altcheckin, altcheckout, requesting,
                    phone, email, comments);
            }
            catch (Exception ex)
            {
                EventLogger.LogError("MembershipService.RequestRate",
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;   
        }*/
    }
}