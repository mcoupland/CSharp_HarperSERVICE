using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFGWrapper.SFG.GateKeeperSvc;
using SupportClasses;

namespace SFGWrapper.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class GateKeeperTranslators
    {
        public static BaseResponse GetMemberByMemberId(returntype sfgReturn)
        {
            string className = "SFGWrapper.GateKeeperTranslators.GetMemberByMemberId()";
            BaseResponse baseResponse = new BaseResponse();
            foreach (var item in sfgReturn.error)
            {
                Message ahError = new Message(item.errno, MessageSources.Gatekeeper);
                foreach (string message in item.errmsg)
                {
                    ahError.SfgMessages.Add(message);
                }
                baseResponse.Messages.Add(ahError);
            }

            GetMemberResponse getMemberResponse = new GetMemberResponse();
            getMemberResponse.Authenticated = (sfgReturn.response.AUTH == "Y");
            getMemberResponse.MemberFound = (sfgReturn.response.CUST_FOUND == "Y");
            if (getMemberResponse.MemberFound)
            {
                //    baseResponse.Messages.Add(new Message("MemberNotFoundException"));
                getMemberResponse.WebAccountFound = (sfgReturn.response.USER_FOUND == "Y");
                getMemberResponse.ShipToAddress = GetAddress(sfgReturn.response.SHIP_TO);
                getMemberResponse.SubscriptionValidationData = GetSubscriptionValidation(sfgReturn.response);
                getMemberResponse.MemberData = GetMember(sfgReturn.response.CUSTOMER_INFO);
                getMemberResponse.MemberData.UserName = sfgReturn.response.USERID;
                foreach (Subscription sub in GetSubscriptions(sfgReturn.response.ORDER_HISTORY))
                {
                    getMemberResponse.MemberData.Subscriptions.Add(sub);
                }
                foreach (RenewalOffer item in GetRenewalOffers(sfgReturn.response.SUB_OFFERS))
                {
                    getMemberResponse.RenewalOffers.Add(item);
                }
            }
            baseResponse.TypedResponse = getMemberResponse;

            baseResponse.TypedResponse.Success = sfgReturn.success;
            baseResponse.TypedResponse.Info = Utilities.GetInfo(sfgReturn.response.INFO);
            baseResponse.TypedResponse.MemoryUsed = sfgReturn.response.MEMORY_USED;
            baseResponse.TypedResponse.Protocol = sfgReturn.response.PROTOCOL;
            baseResponse.TypedResponse.RoundtripTime = sfgReturn.response.ROUNDTRIP_TIME;
            baseResponse.TypedResponse.Server = sfgReturn.response.SERVER;
            baseResponse.TypedResponse.TimeElapsed = sfgReturn.response.TIME_ELAPSED;
            baseResponse.TypedResponse.Version = sfgReturn.response.VERSION;

            return baseResponse;
        }
        public static BaseResponse GetMemberByUsername(returntype sfgReturn)
        {
            string className = "SFGWrapper.GateKeeperTranslators.GetMemberByUsername()";
            BaseResponse baseResponse = new BaseResponse();
            foreach (var item in sfgReturn.error)
            {
                Message ahError = new Message(item.errno, MessageSources.Gatekeeper);
                foreach (string message in item.errmsg)
                {
                    ahError.SfgMessages.Add(message);
                }
                baseResponse.Messages.Add(ahError);
            }
            GetMemberResponse getMemberByUserNameResponse = new GetMemberResponse();
            getMemberByUserNameResponse.Authenticated = (sfgReturn.response.AUTH == "Y");
            getMemberByUserNameResponse.MemberFound = (sfgReturn.response.CUST_FOUND == "Y");
            if (getMemberByUserNameResponse.MemberFound)
            {
                getMemberByUserNameResponse.WebAccountFound = (sfgReturn.response.USER_FOUND == "Y");
                getMemberByUserNameResponse.ShipToAddress = GetAddress(sfgReturn.response.SHIP_TO);
                getMemberByUserNameResponse.SubscriptionValidationData = GetSubscriptionValidation(sfgReturn.response);
                getMemberByUserNameResponse.MemberData = GetMember(sfgReturn.response.CUSTOMER_INFO);
                getMemberByUserNameResponse.MemberData.UserName = sfgReturn.response.USERID;
                foreach (Subscription sub in GetSubscriptions(sfgReturn.response.ORDER_HISTORY))
                {
                    getMemberByUserNameResponse.MemberData.Subscriptions.Add(sub);
                }
                foreach (RenewalOffer item in GetRenewalOffers(sfgReturn.response.SUB_OFFERS))
                {
                    getMemberByUserNameResponse.RenewalOffers.Add(item);
                }
            }   
            baseResponse.TypedResponse = getMemberByUserNameResponse;
            baseResponse.TypedResponse.Success = sfgReturn.success;
            baseResponse.TypedResponse.Info = Utilities.GetInfo(sfgReturn.response.INFO);
            baseResponse.TypedResponse.MemoryUsed = sfgReturn.response.MEMORY_USED;
            baseResponse.TypedResponse.Protocol = sfgReturn.response.PROTOCOL;
            baseResponse.TypedResponse.RoundtripTime = sfgReturn.response.ROUNDTRIP_TIME;
            baseResponse.TypedResponse.Server = sfgReturn.response.SERVER;
            baseResponse.TypedResponse.TimeElapsed = sfgReturn.response.TIME_ELAPSED;
            baseResponse.TypedResponse.Version = sfgReturn.response.VERSION;

            return baseResponse;
        }
        public static BaseResponse Login(returntype sfgReturn)
        {
            string className = "SFGWrapper.GateKeeperTranslators.Login()";
            BaseResponse baseResponse = new BaseResponse();
            foreach (var item in sfgReturn.error)
            {
                Message ahError = new Message(item.errno, MessageSources.Gatekeeper);
                foreach (string message in item.errmsg)
                {
                    ahError.SfgMessages.Add(message);
                }
                baseResponse.Messages.Add(ahError);
            }
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.Authenticated = (sfgReturn.response.AUTH == "Y");
            loginResponse.MemberFound = (sfgReturn.response.CUST_FOUND == "Y");
            loginResponse.WebAccountFound = (sfgReturn.response.USER_FOUND == "Y");
            loginResponse.ShipToAddress = GetAddress(sfgReturn.response.SHIP_TO);
            loginResponse.MemberData = GetMember(sfgReturn.response.CUSTOMER_INFO);
            loginResponse.MemberData.UserName = sfgReturn.response.USERID;
            if (loginResponse.Authenticated)
            {
                loginResponse.SubscriptionValidationData = GetSubscriptionValidation(sfgReturn.response);
                foreach (Subscription sub in GetSubscriptions(sfgReturn.response.ORDER_HISTORY))
                {
                    loginResponse.MemberData.Subscriptions.Add(sub);
                }
                foreach (RenewalOffer item in GetRenewalOffers(sfgReturn.response.SUB_OFFERS))
                {
                    loginResponse.RenewalOffers.Add(item);
                }
            }
            baseResponse.TypedResponse = loginResponse;

            baseResponse.TypedResponse.Success = sfgReturn.success;
            baseResponse.TypedResponse.Info = Utilities.GetInfo(sfgReturn.response.INFO);
            baseResponse.TypedResponse.MemoryUsed = sfgReturn.response.MEMORY_USED;
            baseResponse.TypedResponse.Protocol = sfgReturn.response.PROTOCOL;
            baseResponse.TypedResponse.RoundtripTime = sfgReturn.response.ROUNDTRIP_TIME;
            baseResponse.TypedResponse.Server = sfgReturn.response.SERVER;
            baseResponse.TypedResponse.TimeElapsed = sfgReturn.response.TIME_ELAPSED;
            baseResponse.TypedResponse.Version = sfgReturn.response.VERSION;

            return baseResponse;
        }
        public static BaseResponse GetOffer(returntype sfgReturn)
        {
            string className = "SFGWrapper.GateKeeperTranslators.GetOffer()";
            BaseResponse baseResponse = new BaseResponse();
            foreach (var item in sfgReturn.error)
            {
                Message ahError = new Message(item.errno, MessageSources.Gatekeeper);
                foreach (string message in item.errmsg)
                {
                    ahError.SfgMessages.Add(message);
                }
                baseResponse.Messages.Add(ahError);
            }
            GetMemberResponse getOfferResponse = new GetMemberResponse();            
            foreach (RenewalOffer item in GetRenewalOffers(sfgReturn.response.SUB_OFFERS))
            {
                getOfferResponse.RenewalOffers.Add(item);
            }
            baseResponse.TypedResponse = getOfferResponse;
            baseResponse.TypedResponse.Success = sfgReturn.success;
            baseResponse.TypedResponse.Info = Utilities.GetInfo(sfgReturn.response.INFO);
            baseResponse.TypedResponse.MemoryUsed = sfgReturn.response.MEMORY_USED;
            baseResponse.TypedResponse.Protocol = sfgReturn.response.PROTOCOL;
            baseResponse.TypedResponse.RoundtripTime = sfgReturn.response.ROUNDTRIP_TIME;
            baseResponse.TypedResponse.Server = sfgReturn.response.SERVER;
            baseResponse.TypedResponse.TimeElapsed = sfgReturn.response.TIME_ELAPSED;
            baseResponse.TypedResponse.Version = sfgReturn.response.VERSION;

            return baseResponse;
        }

        public static argtype TranslateToGetMemberByMemberIdRequest(GatekeeperServiceRequest ahRequest)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.GateKeeperTranslators.TranslateToGetMemberByMemberIdRequest()");
            argtype sfgRequest = new argtype();

            #region readonly properties            
            sfgRequest.org = ahRequest.Org;
            sfgRequest.program_type_id = ahRequest.ProgramTypeId;
            //no programid in login?
            sfgRequest.test_mode = ahRequest.TestMode ? "Y" : "N";
            sfgRequest.app_version = ahRequest.AppVersion;
            #endregion

            sfgRequest.load_renewal_offers = ahRequest.LoadRenewalOffers ? "Y" : "N";
            //sfgRequest.offers_key_code = "E000764";//ahRequest.OffersKeyCode;
           
            sfgRequest.customer_number = ahRequest.MemberId;
            sfgRequest.search_by_custno = ahRequest.SearchByCustno ? "Y" : "N";
            sfgRequest.load_customer = ahRequest.LoadCustomer ? "Y" : "N";
            sfgRequest.load_history = ahRequest.LoadHistory ? "Y" : "N";
            sfgRequest.days_history = ahRequest.DaysHistory;
            sfgRequest.days_history = ahRequest.DaysHistory;

            EventLogger.LogEvent("LEAVING -> SFGWrapper.GateKeeperTranslators.TranslateToGetMemberByMemberIdRequest()");
            return sfgRequest;
        }
        public static argtype TranslateToGetMemberByUsernameRequest(GatekeeperServiceRequest ahRequest)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.GateKeeperTranslators.TranslateToGetMemberByUsernameRequest()");
            argtype sfgRequest = new argtype();

            #region readonly properties
            sfgRequest.org = ahRequest.Org;
            sfgRequest.program_type_id = ahRequest.ProgramTypeId;
            //no programid in login?
            sfgRequest.test_mode = ahRequest.TestMode ? "Y" : "N";
            sfgRequest.app_version = ahRequest.AppVersion;
            #endregion

            sfgRequest.load_renewal_offers = ahRequest.LoadRenewalOffers ? "Y" : "N";
            //sfgRequest.offers_key_code = "E000764";//ahRequest.OffersKeyCode;

            sfgRequest.userid = ahRequest.Username;
            sfgRequest.load_customer = ahRequest.LoadCustomer ? "Y" : "N";
            sfgRequest.load_history = ahRequest.LoadHistory ? "Y" : "N";
            sfgRequest.days_history = ahRequest.DaysHistory;

            EventLogger.LogEvent("LEAVING -> SFGWrapper.GateKeeperTranslators.TranslateToGetMemberByUsernameRequest()");
            return sfgRequest;
        }
        public static argtype TranslateToGetOfferRequest(GatekeeperServiceRequest ahRequest)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.GateKeeperTranslators.TranslateToGetOfferRequest()");
            argtype sfgRequest = new argtype();

            #region readonly properties
            sfgRequest.org = ahRequest.Org;
            sfgRequest.program_type_id = ahRequest.ProgramTypeId;
            sfgRequest.test_mode = ahRequest.TestMode ? "Y" : "N";
            sfgRequest.app_version = ahRequest.AppVersion;
            #endregion

            sfgRequest.days_history = ahRequest.DaysHistory;
            sfgRequest.customer_number = ahRequest.MemberId;
            sfgRequest.search_by_custno = ahRequest.SearchByCustno ? "Y" : "N";
            sfgRequest.offers_key_code = ahRequest.OffersKeyCode;
            sfgRequest.check_pw = ahRequest.CheckPassword ? "Y" : "N";
            sfgRequest.load_customer = ahRequest.LoadCustomer ? "Y" : "N";
            sfgRequest.load_history = ahRequest.LoadHistory ? "Y" : "N";
            sfgRequest.load_renewal_offers = ahRequest.LoadRenewalOffers ? "Y" : "N";
            sfgRequest.validate_subscription = ahRequest.ValidateSubscription ? "Y" : "N";

            EventLogger.LogEvent("LEAVING -> SFGWrapper.GateKeeperTranslators.TranslateToGetOfferRequest()");
            return sfgRequest;
        }
        public static argtype TranslateToLoginRequest(GatekeeperServiceRequest ahRequest)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.GateKeeperTranslators.TranslateToLoginRequest()");
            argtype sfgRequest = new argtype();

            #region readonly properties
            sfgRequest.org = ahRequest.Org;
            sfgRequest.program_type_id = ahRequest.ProgramTypeId;
            sfgRequest.test_mode = ahRequest.TestMode ? "Y" : "N";
            sfgRequest.app_version = ahRequest.AppVersion;
            #endregion

            sfgRequest.validate_subscription = ahRequest.ValidateSubscription ? "Y" : "N";
            sfgRequest.load_renewal_offers = ahRequest.LoadRenewalOffers ? "Y" : "N";
            sfgRequest.offers_key_code = ahRequest.OffersKeyCode;//"E000764"

            sfgRequest.userid = ahRequest.Username;
            sfgRequest.hashed_pw = ahRequest.HashedPassword;
            sfgRequest.check_pw = ahRequest.CheckPassword ? "Y" : "N";
            sfgRequest.load_customer = "Y";
            sfgRequest.load_history = "Y";
            sfgRequest.days_history = ahRequest.DaysHistory;

            EventLogger.LogEvent("LEAVING -> SFGWrapper.GateKeeperTranslators.TranslateToLoginRequest()");
            return sfgRequest;
        }
        
        private static SubscriptionValidation GetSubscriptionValidation(responsetype sfgObject)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.GateKeeperTranslators.GetSubscriptionValidation()");
            SubscriptionValidation ahObject = new SubscriptionValidation();
            ahObject.Access = sfgObject.ACCESS == "Y";
            ahObject.StatusFlag = sfgObject.STATUS;
            EventLogger.LogEvent("LEAVING -> SFGWrapper.GateKeeperTranslators.GetSubscriptionValidation()");
            return ahObject;
        }
        private static Address GetAddress(shiptotype sfgObject)
        {
            Address ahObject = new Address();
            try
            {
                EventLogger.LogEvent("ENTERING -> SFGWrapper.GateKeeperTranslators.GetAddressInfo()");
                ahObject.Address1 = sfgObject.SADDR1;
                ahObject.Address2 = sfgObject.SADDR2;
                ahObject.Address3 = sfgObject.SADDR3;
                ahObject.City = sfgObject.SCITY;
                ahObject.Country = sfgObject.SCOUNTRY;
                ahObject.Phone = sfgObject.SPHONE;
                ahObject.StateCode = sfgObject.SST;
                ahObject.State = string.Empty; //TODO: where is this?
                ahObject.PostalCode = sfgObject.SZIP;
                EventLogger.LogEvent("LEAVING -> SFGWrapper.GateKeeperTranslators.GetAddressInfo()");
            }
            catch
            {
                //we dont have ship to addresses
            }
            return ahObject;
        }
        private static List<Subscription> GetSubscriptions(orderhistorytype[] orders)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.GateKeeperTranslators.GetSubscription()");
            List<Subscription> subs = new List<Subscription>();
            try
            {
                foreach (orderhistorytype order in orders)
                {
                    Subscription sub = new Subscription();
                    sub.Category = SubscriptionCategory.Publication;
                    if (order.CATEGORY == "Catalog")
                        sub.Category = SubscriptionCategory.Catalog;
                    sub.PublicationCode = order.PROD_CODE;
                    sub.Description = order.DESCRIPTION;
                    sub.Qty = Convert.ToInt32(order.QTY);
                    sub.DateEntered = order.DATE;
                    sub.ExpireDate = order.EXPIRE_DATE;
                    sub.IssuesRemaining = Convert.ToInt32(order.ISS_REM);
                    sub.RenewalKeycode = order.RENEWAL_KEYCODE;
                    sub.EPub = (order.EPUB == "Y");
                    sub.SubscribingMember = order.H_CUST;
                    sub.GiftRecipient = order.G_CUST;
                    sub.StatusFlag = order.STATUS_FLAG.Length > 0 ? order.STATUS_FLAG : "Y";
                    sub.IsGift = (order.IS_GIFT == "Y");
                    sub.IsDonor = (order.IS_DONOR == "Y");
                    subs.Add(sub);
                }
            }
            catch { }
            EventLogger.LogEvent("LEAVING -> SFGWrapper.GateKeeperTranslators.GetSubscription()");

            return subs;
        }
        private static Member GetMember(customerinfotype sfgObject)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.GateKeeperTranslators.GetMemberDetail()");
            Member ahObject = new Member();
            
            ahObject.Email = sfgObject.EMAIL;
            ahObject.Suffix = sfgObject.SUFFIX;
            ahObject.Salutation = sfgObject.TITLE;
            ahObject.FirstName = sfgObject.FIRST;
            ahObject.LastName = sfgObject.LAST;
            ahObject.MemberId = sfgObject.CUSTOMER_NUMBER;
            ahObject.MiddleInitial = (sfgObject.MI != null && sfgObject.MI.Length > 0) ? sfgObject.MI : null;
            ahObject.ProfessionalTitle = sfgObject.PROFESSIONAL_TITLE;
            ahObject.StatusFlag = sfgObject.STATUS;

            ahObject.Address.Address1 = sfgObject.ADD1;
            ahObject.Address.Address2 = sfgObject.ADD2;
            ahObject.Address.Address3 = sfgObject.ADD3;
            ahObject.Address.AltCity = sfgObject.ALTCITY;
            ahObject.Address.BusinessName = sfgObject.BUSINESS_NAME;
            ahObject.Address.City = sfgObject.CITY;
            ahObject.Address.Country = sfgObject.COUNTRY;
            ahObject.Address.Fax = ""; //TODO: NOT FOUND on sfgObject?
            ahObject.Address.Phone = sfgObject.PHONE;
            ahObject.Address.PostalCode = sfgObject.ZIP;
            ahObject.Address.State = sfgObject.STATE_NAME;
            ahObject.Address.StateCode = sfgObject.ST;

            ahObject.LoadHarperMemberData();

            EventLogger.LogEvent("LEAVING -> SFGWrapper.GateKeeperTranslators.GetMemberDetail()");
            return ahObject;
        }
        private static List<RenewalOffer> GetRenewalOffers(suboffertype[] offers)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.GateKeeperTranslators.GetRenewalOffers()");
            List<RenewalOffer> renewals = new List<RenewalOffer>();
            try
            {
                foreach (var offer in offers)
                {
                    RenewalOffer renewal = new RenewalOffer();
                    renewal.AgencyCode = offer.AGENCY_CODE;
                    if (offer.CASH_OR_CREDIT_OFFER.Length > 0)
                        renewal.CashOrCreditOffer = offer.CASH_OR_CREDIT_OFFER[0];
                    renewal.CountryCode = offer.COUNTRY_CODE;
                    renewal.GrossValue = Convert.ToDecimal(offer.GROSS_VALUE);
                    renewal.KeyCode = offer.KEY_CODE;
                    renewal.NumberFreeIssues = Convert.ToInt16(offer.NUMBER_FREE_ISSUES);
                    renewal.NumberGraceIssues = Convert.ToInt16(offer.NUMBER_GRACE_ISSUES);
                    renewal.NumberOfInstallments = Convert.ToInt16(offer.NUMBER_OF_INSTALLMENTS);
                    renewal.Postage = Convert.ToDecimal(offer.POSTAGE);
                    renewal.PublicationCode = offer.PUBLICATION_CODE;
                    renewal.StartBillingIssue = offer.START_BILLING_ISSUE;
                    renewal.StartDate = Convert.ToDateTime(offer.START_DATE);
                    renewal.StartIssue = offer.START_ISSUE;
                    if (offer.SUB_RENEWAL_FLAG.Length > 0)
                        renewal.SubRenewalFlag = offer.SUB_RENEWAL_FLAG[0];
                    renewal.SubscriptionTerm = offer.SUBSCRIPTION_TERM;
                    renewals.Add(renewal);
                }
            }
            catch { }
            EventLogger.LogEvent("LEAVING -> SFGWrapper.GateKeeperTranslators.GetRenewalOffers()");

            return renewals;
        }

    }
}