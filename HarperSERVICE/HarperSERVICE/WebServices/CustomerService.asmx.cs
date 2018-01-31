using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Configuration;

using SupportClasses;
using SFGWrapper;
using BusinessLogic;
//using System.Xml;
using HarperCRYPTO;
using HarperLINQ;
//using HarperACL;
using System.Collections.Generic;

namespace MemberServices
{
    /// <summary>
    /// Summary description for CustomerService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CustomerService : System.Web.Services.WebService
    {
        [WebMethod]
        public ResponseObject LoginOnly(string enc_username, string enc_password, bool log_attempt)
        {
            ResponseObject response = new ResponseObject();
            try
            {
                response = DoLogin(enc_username, enc_password);

                if (log_attempt)
                {
                    RecordLoginAttempt(enc_username, enc_password, response, (CustomerResponseCode)response.ResponseCode);
                }
            }
            catch(Exception e)
            {
                response.ResponseCode = 100;
                string message = string.Format("ENC_USERNAME:{0} ENC_PASSWORD:{1} LOG_ATTEMPT:{2}", new object[] { enc_username, enc_password, log_attempt });
                RecordError("Login", message, e);
            }
            return response;
        }

        [WebMethod]
        public int SyncMembershipWithSFG(string enc_username)
        {
            try
            {
                return SyncMembership(enc_username) ? 1 : 0;
            }
            catch 
            { 
                return -1;
            }
            
        }

        /// <summary>Gets user name and compares password.
        /// Possible response codes
        /// SUCCESS = 0, 
        /// INVALID_PASSWORD = 101,
        /// NO_SUCH_USER_NAME = 102, 
        /// NO_SUB = 104,
        /// DUPLICATE_USER_NAME = 105, 
        /// CANNOT_DECRYPT_STORED_PWD = 106,
        /// CANNOT_DECRYPT_INPUT = 107
        /// </summary>
        /// <param name="enc_username">Encrypted user name</param>
        /// <param name="enc_password">Encrypted password</param>
        /// <param name="log_attempt">Whether or not to record this attempt in the tbl_AppEventLog</param>
        /// <param name="sync_data">Whether or not to sync data after login</param>
        /// <returns>int</returns> 
        [WebMethod]
        public ResponseObject Login(string enc_username, string enc_password, bool log_attempt)
        {
            ResponseObject response = new ResponseObject();
            try
            {
                SyncMembership(enc_username);                
            }
            catch(Exception ex){}
            try
            {
                response = DoLogin(enc_username, enc_password);

                if (log_attempt)
                {
                    RecordLoginAttempt(enc_username, enc_password, response, (CustomerResponseCode)response.ResponseCode);
                }
            }
            catch (Exception e)
            {
                response.ResponseCode = 100;
                string message = string.Format("ENC_USERNAME:{0} ENC_PASSWORD:{1} LOG_ATTEMPT:{2}", new object[] { enc_username, enc_password, log_attempt });
                RecordError("Login", message, e);
            }
            return response;
        }


        private ResponseObject DoLogin(string enc_username, string enc_password)
        {
            ResponseObject response = new ResponseObject();
            object[] login_response = tbl_Customer.Login(enc_username, enc_password);
            response.ResponseCode = (int)login_response[0];
            response.Customer = (tbl_Customer)login_response[1];
            response.Address = (tbl_AddressCustomer)login_response[2];
            response.NetMembership = (tbl_NetMembership)login_response[3];
            return response;
        }

        private bool SyncMembership(string enc_username)
        {
            tbl_Customer customer = new tbl_Customer(Cryptography.Decrypt256FromHEX(enc_username), true);
            GetMemberByMemberIdRequest req = new GetMemberByMemberIdRequest(customer.SfgId.ToString(), false);
            BaseResponse res = Gatekeeper.GetMemberByMemberId(req);
            if (res.Messages.Count() <= 0 && res.TypedResponse != null)
            {
                GetMemberResponse tres = (GetMemberResponse)res.TypedResponse;
                if (tres.MemberData != null
                    && tres.MemberData.Subscriptions != null
                    && tres.MemberData.Subscriptions.Count() > 0)
                {
                    Subscription current = null;
                    foreach (Subscription sub in tres.MemberData.Subscriptions)
                    {
                        if (current == null)
                        {
                            current = sub;
                        }
                        else if ((sub.StatusFlag == "P" || sub.StatusFlag == "O") && !sub.IsDonor)
                        {
                            try
                            {
                                if (DateTime.Parse(sub.ExpireDate).CompareTo(DateTime.Parse(current.ExpireDate)) > 0)
                                {
                                    current = sub;
                                }
                            }
                            catch { }
                        }
                    }
                    //update netmembership, with latest from sfg
                    tbl_NetMembership currentmembership = tbl_NetMembership.GetCurrentNetMembership(customer.cusID);
                    if (currentmembership == null) { currentmembership = new tbl_NetMembership(); }
                    currentmembership.cusID = customer.cusID;
                    currentmembership.mtyCode = HarperLINQ.SFG_ProdCode.GetFromExtCode(current.PublicationCode).IntCode;
                    currentmembership.nmbDateCreated = DateTime.Now;
                    currentmembership.nmbDateEnd = DateTime.Parse(current.ExpireDate);
                    currentmembership.nmbDateStart = DateTime.Parse(current.DateEntered);
                    currentmembership.Save();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<Prefix> GetPrefixes()
        {
            return Prefix.GetPrefixes();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<Suffix> GetSuffixes() 
        {
            return Suffix.GetSuffixes();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<ISO3166> GetISOCountries()
        {
            return ISO3166.GetCountries();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentAlpha2"></param>
        /// <returns></returns>
        [WebMethod]
        public List<ISO3166> GetISORegions(string ParentAlpha2) 
        {
            return ISO3166.GetRegions(ParentAlpha2);
        }

        /// <summary>
        /// 
        /// </summary>
        [WebMethod]
        public OfferResponseObject GetOffer(string keycode)
        {
            GetOfferRequest request = new GetOfferRequest(keycode, false);
            OfferResponseObject response = new OfferResponseObject();
            BaseResponse base_response = Gatekeeper.GetOffer(request);

            if (base_response.Messages.Count > 0)
            {
                response.ResponseCode = 100;
            }
            if (response.ResponseCode == 0)
            {
                GetMemberResponse member_response = base_response.TypedResponse as GetMemberResponse;
                response.RenewalOffers = member_response.RenewalOffers;
            }
            return response;
        }
        
        [WebMethod]
        public bool IsUserNameAvailable(string username)
        {
            return HarperLINQ.tbl_Customer.IsUserNameAvailable(username);
        }

        #region App event log methods
        [WebMethod]
        public List<string> GetAppEventLogAppNames()
        {
            return HarperLINQ.tbl_AppEventLog.GetAppNames();
        }
        [WebMethod]
        public List<string> GetAppEventLogSeverities()
        {
            return HarperLINQ.tbl_AppEventLog.GetSeverities();
        }
        [WebMethod]
        public List<string> GetAppEventLogEventNames()
        {
            return HarperLINQ.tbl_AppEventLog.GetEventNames();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="limit">If zero, defaults to 100</param>
        /// <returns></returns>
        [WebMethod]
        public List<tbl_AppEventLog> GetAppEventLogsByUserName(string username, int limit)
        {
            return HarperLINQ.tbl_AppEventLog.GetByUserName(username, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appname"></param>
        /// <param name="limit">If zero, defaults to 100</param>
        /// <returns></returns>
        [WebMethod]
        public List<tbl_AppEventLog> GetAppEventLogsByAppName(string appname, int limit)
        {
            return HarperLINQ.tbl_AppEventLog.GetByAppName(appname, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="limit">If zero, defaults to 100</param>
        /// <returns></returns>
        [WebMethod]
        public List<tbl_AppEventLog> GetAppEventLogsBySeverity(string severity, int limit)
        {
            return HarperLINQ.tbl_AppEventLog.GetBySeverity(severity, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="limit">If zero, defaults to 100</param>
        /// <returns></returns>
        [WebMethod]
        public List<tbl_AppEventLog> GetAppEventLogsByEventName(string eventname, int limit)
        {
            return HarperLINQ.tbl_AppEventLog.GetByEventName(eventname, limit);
        }
        #endregion

        #region New order methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="salutation"></param>
        /// <param name="firstname"></param>
        /// <param name="middleinitial"></param>
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
        /// <param name="fax"></param>
        /// <param name="altcity"></param>
        /// <param name="ccnum"></param>
        /// <param name="ccexpmonth"></param>
        /// <param name="ccexpyear"></param>
        /// <param name="amountpaid"></param>
        /// <param name="ccname"></param>
        /// <param name="ccaddress"></param>
        /// <param name="cccity"></param>
        /// <param name="ccstate"></param>
        /// <param name="ccpostalcode"></param>
        /// <param name="pubcode"></param>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <param name="keycode"></param>
        /// <param name="subscriptionlength"></param>
        /// <param name="source"></param>
        /// <param name="customertype"></param>
        /// <param name="expiredate"></param>
        /// <param name="startdate"></param>
        /// <param name="newsletters"></param>
        /// <param name="mobilephone"></param>
        /// <param name="secondemail"></param>
        /// <param name="methodofpayment"></param>
        /// <param name="cccountry"></param>
        /// <param name="ccaddress2"></param>
        /// <param name="screenname"></param>
        /// <param name="iscomp"></param>
        /// <returns></returns>
        [WebMethod]
        public ResponseObject SubscribeNewUser(
            string salutation, 
            string firstname, 
            string middleinitial, 
            string lastname, 
            string suffix,
            string professionaltitle,
            string email,
            //string optin, //not sure if we will need this since we are accepting an array of newsletters now
            string businessname, 
            string address1, 
            string address2, 
            string address3, 
            string city, 
            string state, 
            string postalcode, 
            string country, 
            string phone, 
            string ccnum, 
            string ccexpmonth, 
            string ccexpyear, 
            string amountpaid, 
            string ccname, 
            string ccaddress, 
            string cccity, 
            string ccstate, 
            string ccpostalcode, 
            string pubcode, 
            string username, 
            string pwd, 
            string keycode, 
            string subscriptionlength, 
            string source, 
            string customertype, 
            string expiredate, 
            string startdate,
            string newsletters,
            string mobilephone, 
            string secondemail, 
            string methodofpayment, 
            string cccountry, 
            string ccaddress2,
            string screenname,
            string iscomp)
        {
            bool is_comp = false;
            if (!bool.TryParse(iscomp, out is_comp))
            {
                is_comp = false;
            }

            #region store all request data
            HarperLINQ.tbl_DrupalCartRequest cart = new tbl_DrupalCartRequest();
            cart.salutation = salutation;
            cart.firstname = firstname;
            cart.middleinitial = middleinitial;
            cart.lastname = lastname;
            cart.suffix = suffix;
            cart.protitle = professionaltitle;
            cart.email = email;
            //cart.optin = optin;
            cart.business = businessname;
            cart.add1 = address1;
            cart.add2 = address2;
            cart.add3 = address3;
            cart.city = city;
            cart.state = state;
            cart.zip = postalcode;
            cart.country = country;
            cart.phone = phone;
            //cart.fax = fax;
            //cart.altcity = altcity;
            if (!is_comp)
            {
                cart.ccnum = Cryptography.Encrypt256(ccnum);
                cart.ccmonth = ccexpmonth;
                cart.ccyear = ccexpyear;
                cart.amtpaid = amountpaid;
                cart.ccname = ccname;
                cart.ccadd = ccaddress;
                cart.cccity = cccity;
                cart.ccstate = ccstate;
                cart.cczip = ccpostalcode;
                cart.methodofpayment = methodofpayment;
                cart.cccountry = cccountry;
                cart.ccaddress2 = ccaddress2;
            }
            cart.pubcode = pubcode;
            cart.keycode = keycode;
            cart.username = username;
            cart.pwd = pwd;
            cart.sublen = subscriptionlength;
            cart.source = source;
            cart.customertype = customertype;
            cart.expiredate = expiredate;
            cart.startdate = startdate;
            cart.newsletters = newsletters;
            cart.mobilephone = mobilephone;
            cart.secondemail = secondemail;
            cart.screenname = screenname;
            cart.Save();
            #endregion

            ResponseObject response = new ResponseObject();
            response.ResponseCode = 0;

            try
            {
                #region check user name availability
                switch (tbl_Customer.CheckUserName(username))
                {
                    case 0:
                        response.ResponseCode = 0;
                        break;
                    case 1:
                        response.ResponseCode = (int)CustomerResponseCode.DUPLICATE_USER_NAME;
                        break;
                    case 2:
                        response.ResponseCode = (int)CustomerResponseCode.DUPLICATE_EMAIL_ADDRESS;
                        break;
                    case 3:
                        response.ResponseCode = (int)CustomerResponseCode.DUPLICATE_USER_NAME;
                        break;
                }
                #endregion
                
                if (response.ResponseCode == 0)
                {
                    object[] cc_response = null;
                    if(!is_comp)
                    {
                        #region charge credit card
                        cc_response = ChargeCard(null, salutation, firstname, middleinitial, lastname, suffix, professionaltitle, email, businessname, address1, address2, address3, city, state, postalcode, country, phone, ccnum, ccexpmonth, ccexpyear, amountpaid, ccname, ccaddress, cccity, ccstate, ccpostalcode, cccountry, pubcode, username, pwd, "false");
                        response.ResponseCode = (int)cc_response[0];
                        #endregion
                    }


                    if (response.ResponseCode == 0)
                    {
                        #region create subscription at SFG
                        string routingid = string.Empty;
                        string authcode = string.Empty;
                        if (!is_comp)
                        {
                            routingid = (string)cc_response[1];
                            authcode = (string)cc_response[2];
                        }
                        // optin, fax, altcity, 
                        object[] sub_response = CreateSubscription(subscriptionlength, amountpaid, routingid, 
                            pubcode, keycode, null, salutation, firstname, 
                            middleinitial, lastname, suffix, professionaltitle, 
                            email,businessname, address1, address2, address3, 
                            city, state, postalcode, country, phone, 
                            "false", null, null, null, null, null, null, null, null,null, 
                            null, null, null, null, null, null, null, null, iscomp);
                        response.ResponseCode = (int)sub_response[0];
                        #endregion

                        if (response.ResponseCode == 0)
                        {
                            #region create account at AH
                            string newmemberid = (string)sub_response[1];
                            object[] create_response = 
                                tbl_Customer.CreateCustomer(address1, address2, address3, city, 
                                state, country, postalcode, source, pwd, 
                                customertype, salutation, firstname, middleinitial, 
                                lastname, suffix, email, username, newmemberid, 
                                pubcode, expiredate, startdate, screenname, mobilephone, 
                                secondemail, keycode);
                            response.ResponseCode = (int)create_response[0];
                            response.Customer = (tbl_Customer)create_response[1];
                            response.Address = (tbl_AddressCustomer)create_response[2];
                            response.NetMembership = (tbl_NetMembership)create_response[3];
                            #endregion

                            #region send new user email - no longer sent by this service
                            /*
                            try
                            {
                                Prefix sfgp = HarperLINQ.Prefix.GetPrefixBySFGCode(salutation);
                                Suffix sfgs = HarperLINQ.Suffix.GetSuffixBySFGCode(suffix);
                                string Prefix = sfgp == null ? "" : sfgp.displayname;
                                string Suffix = sfgs == null ? "" : sfgs.displayname;
                                SendNewMemberEmail(Prefix, firstname, lastname, Suffix, newmemberid, email);
                                foreach (string cc in ConfigurationManager.AppSettings["subscribeccaddresses"].Split(','))
                                {
                                    SendNewMemberEmail(Prefix, firstname, lastname, Suffix, newmemberid, cc);
                                }
                            }
                            catch { }
                             */
                            #endregion
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.ResponseCode = 200;
                response.Customer.cusEncryptedPassword = e.Message;
                string message = string.Format("", new object[] { string.Empty });
                RecordError("CreateCustomer", message, e);
                cart.errors += message;
            }
            return response;

        }                  
        
        //string optin,string fax,string altcity,
        private object[] ChargeCard(string memberid, string salutation, string firstname, string middleinitial, string lastname, string suffix,
            string professionaltitle, string email, string businessname, string address1, string address2, string address3,
            string city, string state, string postalcode, string country,
            string phone,  
            string ccnumber, string expmonth, string expyear, string amount,
            string ccname, string ccaddr, string cccity,
            string ccstate, string cczip, string cccountry, string pubcode, string username, string password, string refreshcustomer)
        {
            int response_code = 0;
            string routing_id = string.Empty;
            string auth_code = string.Empty;

            #region convert string input to correct types
            bool b_optin = false;
            short s_expmonth = 0;
            short s_expyear = 0;
            float f_amount = 0.0f;
            bool b_refreshcustomer = false;

            /*if (!bool.TryParse(optin, out b_optin))
            {
                response_code = 301;
            }*/
            if (!short.TryParse(expmonth, out s_expmonth))
            {
                response_code = 302;
            }
            if (!short.TryParse(expyear, out s_expyear))
            {
                response_code = 303;
            }
            if (!float.TryParse(amount, out f_amount))
            {
                response_code = 304;
            }
            if (!bool.TryParse(refreshcustomer, out b_refreshcustomer))
            {
                response_code = 305;
            }
            #endregion
            
            if(response_code == 0)
            {
                #region validate input
                Member memberData = new Member();
                memberData.MemberId = memberid;
                memberData.Salutation = salutation;
                memberData.FirstName = firstname;
                memberData.MiddleInitial = middleinitial;
                memberData.LastName = lastname;
                memberData.Suffix = suffix;
                memberData.ProfessionalTitle = professionaltitle;
                //memberData.OptIn = b_optin;
                memberData.Email = email;

                memberData.Address = new Address();
                memberData.Address.BusinessName = businessname;
                memberData.Address.Address1 = address1;
                memberData.Address.Address2 = address2;
                memberData.Address.Address3 = address3;
                memberData.Address.City = city;
                memberData.Address.State = state;
                memberData.Address.PostalCode = postalcode;
                memberData.Address.Country = country;
                memberData.Address.Phone = phone;
                //memberData.Address.Fax = fax;
                //memberData.Address.AltCity = altcity;

                CreditCard ccData = new CreditCard();
                ccData.CCNumber = ccnumber;
                ccData.CCExpMonth = s_expmonth;
                ccData.CCExpYear = s_expyear;
                ccData.AmountPaid = f_amount;
                ccData.CCName = ccname;
                ccData.CCAddress = ccaddr;
                ccData.CCCity = cccity;
                ccData.CCState = ccstate;
                ccData.CCPostalCode = cczip;
                ccData.CCCountry = cccountry;
                #endregion

                CreditCardServiceRequest request = new CreditCardServiceRequest(ccData, memberData, pubcode, username, password, b_refreshcustomer);
                BaseResponse cc_response = CreditCardProcessing.GetResponse(request);

                if (cc_response == null || cc_response.TypedResponse == null || cc_response.TypedResponse.Success == false)
                {
                    string msgs = string.Empty;
                    foreach (Message s in cc_response.Messages)
                    {
                        msgs += "[" + s.ToString() + "]";
                    }
                    tbl_AppEventLog logmsg = new tbl_AppEventLog();
                    logmsg.aelAppName = "HarperSERVICE";
                    logmsg.aelDateCreated = DateTime.Now;
                    logmsg.aelEvent = "306";
                    logmsg.aelMessage1 = "cc_response messages:" + msgs;
                    logmsg.aelMessage2 = "typed response:" + cc_response.TypedResponse.ToString();
                    logmsg.aelMessage3 = "success:" + cc_response.TypedResponse.Success;
                    logmsg.Save();
                    response_code = 306;
                }
                else
                {
                    routing_id = ((CreditCardServiceResponse)cc_response.TypedResponse).VerifoneRoutingId;
                    auth_code = ((CreditCardServiceResponse)cc_response.TypedResponse).AuthorizationCode;
                }
            }

            return new object[]{response_code, routing_id, auth_code};
        }

        //string optin, string fax, string altcity,string giftoptin, string giftfax, string giftaltcity
        private object[] CreateSubscription(string subscriptionlength,
            string amountpaid, string verifoneroutingid,
            string publicationcode, string keycode,
            string renewingmemberid, string salutation, string firstname, string middleinitial, string lastname, string suffix,
            string professionaltitle, string email, string businessname, string address1, string address2, string address3,
            string city, string state, string postalcode, string country,
            string phone, 
            string giftflag,
            string renewinggiftmemberid, string giftsalutation, string giftfirstname, string giftmiddleinitial, string giftlastname, string giftsuffix,
            string giftprofessionaltitle, string giftemail, string giftbusinessname, string giftaddress1, string giftaddress2, string giftaddress3,
            string giftcity, string giftstate, string giftpostalcode, string giftcountry,
            string giftphone,
            string iscomp)
        {
            int response_code = 0;
            string member_id = string.Empty;

            #region convert string input to correct types

            bool is_comp = false;
            if (!bool.TryParse(iscomp, out is_comp))
            {
                is_comp = false;
            }
            int i_subscriptionlength = 0;
            float f_amountpaid = 0.0f;
            bool b_optin = false;
            bool b_giftflag = false;
            bool b_giftoptin = false;

            if (!int.TryParse(subscriptionlength, out i_subscriptionlength))
            {
                response_code = 401;
            }
            if (!float.TryParse(amountpaid, out f_amountpaid))
            {
                response_code = 402;
            }
            /*if (!bool.TryParse(optin, out b_optin))
            {
                response_code = 403;
            }*/
            if (!string.IsNullOrEmpty(giftflag))
            {
                if (!bool.TryParse(giftflag, out b_giftflag))
                {
                    response_code = 404;
                }
                /*if (!bool.TryParse(giftoptin, out b_giftoptin))
                {
                    response_code = 405;
                }*/
            }
            #endregion

            if (response_code == 0)
            {
                #region set member data
                Member memberData = new Member();
                memberData.MemberId = renewingmemberid;
                memberData.Salutation = salutation;
                memberData.FirstName = firstname;
                memberData.MiddleInitial = middleinitial;
                memberData.LastName = lastname;
                memberData.Suffix = suffix;
                memberData.ProfessionalTitle = professionaltitle;
                memberData.OptIn = b_optin;
                memberData.Email = email;

                memberData.Address = new Address();
                memberData.Address.BusinessName = businessname;
                memberData.Address.Address1 = address1;
                memberData.Address.Address2 = address2;
                memberData.Address.Address3 = address3;
                memberData.Address.City = city;
                memberData.Address.State = state;
                memberData.Address.PostalCode = postalcode;
                memberData.Address.Country = country;
                memberData.Address.Phone = phone;
                //memberData.Address.Fax = fax;
                //memberData.Address.AltCity = altcity;

                Member giftData = new Member();
                if (b_giftflag)
                {
                    giftData.MemberId = renewinggiftmemberid;
                    giftData.Salutation = salutation;
                    giftData.FirstName = firstname;
                    giftData.MiddleInitial = middleinitial;
                    giftData.LastName = lastname;
                    giftData.Suffix = suffix;
                    giftData.ProfessionalTitle = professionaltitle;
                    giftData.OptIn = b_giftoptin;
                    giftData.Email = email;

                    giftData.Address = new Address();
                    giftData.Address.BusinessName = businessname;
                    giftData.Address.Address1 = address1;
                    giftData.Address.Address2 = address2;
                    giftData.Address.Address3 = address3;
                    giftData.Address.City = city;
                    giftData.Address.State = state;
                    giftData.Address.PostalCode = postalcode;
                    giftData.Address.Country = country;
                    giftData.Address.Phone = phone;
                    //giftData.Address.Fax = fax;
                    //giftData.Address.AltCity = altcity;
                }
                #endregion

                #region set cc data
                CreditCard creditCardData = new CreditCard();
                creditCardData.Price = f_amountpaid;
                creditCardData.AmountPaid = f_amountpaid;
                creditCardData.VerifoneRoutingId = verifoneroutingid;
                
                if(is_comp)
                {
                    creditCardData.PaymentType = "F";
                }
                #endregion

                SubscriptionServiceRequest request = new SubscriptionServiceRequest(memberData, giftData, 
                    creditCardData, publicationcode, keycode, b_giftflag, i_subscriptionlength);
                BaseResponse sub_response = SubOrderInsert.CreateSubscription(request);
                if (sub_response == null || sub_response.TypedResponse == null || sub_response.TypedResponse.Success == false)
                {
                    string msgs = string.Empty;
                    foreach (Message s in sub_response.Messages)
                    {
                        msgs += "[" + s.ToString() + "]";
                    }
                    tbl_AppEventLog logmsg = new tbl_AppEventLog();
                    logmsg.aelAppName = "HarperSERVICE";
                    logmsg.aelDateCreated = DateTime.Now;
                    logmsg.aelEvent = "406";
                    logmsg.aelMessage1 = "sub_response messages:" + msgs;
                    logmsg.aelMessage2 = "typed response:" + sub_response.TypedResponse.ToString();
                    logmsg.aelMessage3 = "success:" + sub_response.TypedResponse.Success;
                    logmsg.Save();
                    response_code = 406;
                }
                else
                {
                    member_id = ((SubscriptionServiceResponse)sub_response.TypedResponse).MemberId;
                }
            }
            return new object[]{response_code, member_id};
        }
        #endregion

        
        private void SendNewMemberEmail(string salutation, string firstname, string lastname, string suffix, string sfgid, string email)
        {
            string EmailBody = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["newuseremailtemplate"]);
            string ThankYou = ", ";
            ThankYou += string.IsNullOrEmpty(salutation) ? "" : salutation + " ";
            ThankYou += string.IsNullOrEmpty(firstname) ? "" : firstname + " ";
            ThankYou += string.IsNullOrEmpty(lastname) ? "" : lastname + " ";
            ThankYou += string.IsNullOrEmpty(suffix) ? "" : suffix;
            if (ThankYou.Length > 0 && ThankYou.LastIndexOf(", ") == ThankYou.Length - 2)
            {
                ThankYou = ThankYou.Substring(0, ThankYou.LastIndexOf(", "));
            }
            if (ThankYou.Length > 0 && ThankYou.LastIndexOf(" ") == ThankYou.Length - 1)
            {
                ThankYou = ThankYou.Substring(0, ThankYou.LastIndexOf(" "));
            }
            EmailBody = EmailBody.Replace("[[sfgid]]", sfgid);
            EmailBody = EmailBody.Replace("[[thankyou]]", ThankYou);
            EmailBody = EmailBody.Replace("[[baseurl]]", ConfigurationManager.AppSettings["baseurl"]);
            string EmailSubject = ConfigurationManager.AppSettings["newuseremailsubject"];
            string EmailFrom = ConfigurationManager.AppSettings["newuseremailfrom"];

            SupportClasses.Mailer Emailer = new SupportClasses.Mailer();
            Emailer.SendEmail(ConfigurationManager.AppSettings["mailserviceuser"],
                ConfigurationManager.AppSettings["mailservicepwd"],
                EmailSubject, EmailFrom, email,
                string.Empty, string.Empty,
                EmailBody, true,
                ConfigurationManager.AppSettings["EmailServer"]);
        }
        private void RecordLoginAttempt(string enc_username, string enc_password, ResponseObject response, CustomerResponseCode response_code)
        {
            string u = response.ResponseCode == (int)CustomerResponseCode.CANNOT_DECRYPT_INPUT ? string.Empty : Cryptography.Decrypt256FromHEX(enc_username);
            using (AHT_MainDataContext context = new AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
            {
                tbl_AppEventLog ael = new tbl_AppEventLog();
                ael.aelUserName = u;
                ael.aelSection = "Login";
                ael.aelAppName = "CustomerWebService";
                ael.aelSeverity = response.ResponseCode > 0 ? "ERROR" : "INFO";
                ael.aelEvent = response.ResponseCode == (int)CustomerResponseCode.SUCCESS ? "LOGIN_SUCCEEDED" : "LOGIN_FAILED";
                ael.aelMessage1 = string.Format("U:{0} P:{1}", new object[] { enc_username, enc_password });
                ael.aelMessage2 = string.Format(response_code.ToString());
                ael.aelDateCreated = DateTime.Now;
                context.tbl_AppEventLogs.InsertOnSubmit(ael);
                context.SubmitChanges();
            }
        }
        private void RecordError(string method, string message1, Exception ex)
        {
            using (AHT_MainDataContext context = new AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
            {
                int col_len = 2500;
                string msg = ex.Message;
                if (msg.Length > col_len)
                {
                    msg = msg.Substring(0, col_len);
                }
                string stack_trace = ex.StackTrace;
                if (stack_trace.Length > col_len)
                {
                    stack_trace = stack_trace.Substring(0, col_len);
                }
                if (message1.Length > col_len)
                {
                    message1 = message1.Substring(0, col_len);
                }
                string message2 = string.Format("Exception Message: {0}", msg);
                string message3 = string.Format("Exception Stack Trace: {0}", stack_trace);
                tbl_AppEventLog ael = new tbl_AppEventLog();
                ael.aelAppName = "CustomerWebService";
                ael.aelSection = method;
                ael.aelSeverity = "ERROR";
                ael.aelEvent = "Unknown Error";
                ael.aelMessage1 = message1.Length > 2500 ? message1.Substring(0, 2499) : message1;
                ael.aelMessage2 = message2.Length > 2500 ? message2.Substring(0,2499) : message2;
                ael.aelMessage3 = message3.Length > 2500 ? message3.Substring(0, 2499) : message3;
                ael.aelDateCreated = DateTime.Now;
                context.tbl_AppEventLogs.InsertOnSubmit(ael);
                context.SubmitChanges();
            }
        }
    }
}
