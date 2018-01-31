using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFGWrapper;
using SupportClasses;
using HarperCRYPTO;
using HarperLINQ;
using System.Configuration;

//TODO: update solution to log errors like RedeemReferralSubscription
namespace BusinessLogic
{
    public class SubscriptionLogic
    {
        BaseResponse baseResponse = new BaseResponse();

        public BaseResponse CreateSubscription(int subscriptionlength, 
            float amountpaid, string verifoneroutingid, 
            string publicationcode, string keycode, 
            string renewingmemberid, string salutation, string firstname, string middleinitial, string lastname, string suffix,
            string professionaltitle, string email, bool optin, string businessname, string address1, string address2, string address3,
            string city, string state, string postalcode, string country,
            string phone, string fax, string altcity, 
            bool giftflag, 
            string renewinggiftmemberid, string giftsalutation, string giftfirstname, string giftmiddleinitial, string giftlastname, string giftsuffix,
            string giftprofessionaltitle, string giftemail, bool giftoptin, string giftbusinessname, string giftaddress1, string giftaddress2, string giftaddress3,
            string giftcity, string giftstate, string giftpostalcode, string giftcountry,
            string giftphone, string giftfax, string giftaltcity)
        {
            try
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
                memberData.OptIn = optin;
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
                memberData.Address.Fax = fax;
                memberData.Address.AltCity = altcity;

                Member giftData = new Member();
                //giftData.MemberId = renewinggiftmemberid;
                //giftData.Salutation = salutation;
                //giftData.FirstName = firstname;
                //giftData.MiddleInitial = middleinitial;
                //giftData.LastName = lastname;
                //giftData.Suffix = suffix;
                //giftData.ProfessionalTitle = professionaltitle;
                //giftData.OptIn = optin;
                //giftData.Email = email;

                //giftData.Address = new Address();
                //giftData.Address.BusinessName = businessname;
                //giftData.Address.Address1 = address1;
                //giftData.Address.Address2 = address2;
                //giftData.Address.Address3 = address3;
                //giftData.Address.City = city;
                //giftData.Address.State = state;
                //giftData.Address.PostalCode = postalcode;
                //giftData.Address.Country = country;
                //giftData.Address.Phone = phone;
                //giftData.Address.Fax = fax;
                //giftData.Address.AltCity = altcity;
                #endregion

                #region set cc data
                CreditCard creditCardData = new CreditCard();
                creditCardData.Price = amountpaid;
                creditCardData.AmountPaid = amountpaid;
                creditCardData.VerifoneRoutingId = verifoneroutingid;
                #endregion

                SubscriptionServiceRequest request = new SubscriptionServiceRequest(memberData, giftData, creditCardData, 
                        publicationcode, keycode, giftflag, subscriptionlength);
                baseResponse = SubOrderInsert.CreateSubscription(request);       
            }
            catch (Exception ex)
            {
                EventLogger.LogError("SubscriptionLogic.CreateSubscription", ex.Message);
            }
            return baseResponse;
        }

        public BaseResponse RedeemReferralSubscription(int referralid, string firstname, string lastname,
            string emailaddress, string countrycode, string address1, string address2,
            string city, string region, string postal, bool optin, string username, string password)
        {
            List<Message> errors = new List<Message>();
            string errortext = string.Empty;
            try
            {
                HarperLINQ.Referral referral;

                #region input validation
                using (AHT_MainDataContext context = new AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                {
                    referral = context.Referrals.SingleOrDefault(r => r.id == referralid);
                }
                if (referral == null)
                {
                    errortext = string.Format(BusinessLogicStrings.invalidReferralIdError, referralid);
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "RedeemReferralException", errortext, "", "", null));
                }
                else if (referral.dateredeemed != null || referral.friendid > 0)
                {
                    errortext = string.Format(BusinessLogicStrings.RedeemedReferralError, referralid);
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "RedeemReferralException", errortext, "", "", null));
                }
                else if (referral.dateexpires <= DateTime.Now)
                {
                    errortext = string.Format(BusinessLogicStrings.expiredReferralError, referralid);
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "RedeemReferralException", errortext, "", "", null));
                }
                #endregion
                
                else
                {
                    #region sub order insert
                    Member giftData = new Member();
                    giftData.FirstName = firstname;
                    giftData.LastName = lastname;
                    giftData.OptIn = optin;
                    giftData.Email = emailaddress;

                    giftData.Address = new Address();
                    giftData.Address.Address1 = address1;
                    giftData.Address.Address2 = address2;
                    giftData.Address.City = city;
                    giftData.Address.State = region;
                    giftData.Address.PostalCode = postal;
                    giftData.Address.Country = countrycode;

                    SubscriptionServiceRequest request = new SubscriptionServiceRequest(referral, giftData);
                    baseResponse = SubOrderInsert.RedeemReferralSubscription(request);
                    foreach (Message err in baseResponse.Messages)
                    {
                        errors.Add(err);
                    }
                    #endregion

                    MembershipLogic memberlogic = new MembershipLogic();
                    BaseResponse memberResponse = null;
                    if (errors.Count <= 0)
                    {
                        memberResponse = memberlogic.GetMemberByUserName(emailaddress);
                    }

                    if (!(errors.Count > 0 
                        || memberResponse == null
                        || memberResponse.TypedResponse == null
                        || memberResponse.TypedResponse.Success == false))
                    {
                        using (AHT_MainDataContext context = new AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                        {
                            GetMemberResponse getMemberResponse = memberResponse.TypedResponse as GetMemberResponse;
                            if (getMemberResponse.MemberFound && getMemberResponse.MemberData != null)
                            {
                                string newMemberId = getMemberResponse.MemberData.MemberId;

                                #region create the user at AH
                                object[] create_response = tbl_Customer.CreateCustomer(address1, address2, "", city, region, 
                                    countrycode, postal, "Z1", password, "PERSONAL", "", 
                                    firstname, "", lastname, "", emailaddress, username, newMemberId, referral.pubcode, DateTime.Now.AddMonths(referral.subscriptionlength).ToShortDateString(), DateTime.Now.ToShortDateString(), username, "");
                                tbl_Customer customer = (tbl_Customer)create_response[1];
                                #endregion

                                #region referral data at AH
                                referral = context.Referrals.SingleOrDefault(r => r.id == referralid);
                                referral.dateredeemed = DateTime.Now;
                                referral.friendid = customer.cusID;
                                context.SubmitChanges();
                                #endregion

                                #region send email
                                Mailer mailer = new Mailer();

                                mailer.SendEmail(
                                    ConfigurationManager.AppSettings["mailserviceuser"], 
                                    ConfigurationManager.AppSettings["mailservicepwd"],
                                    "Welcome to the Andrew Harper Community!", 
                                    ConfigurationManager.AppSettings["referemailfrom"],
                                    referral.friendemail,
                                    string.Empty, 
                                    string.Empty,
                                    referral.GetReferralUserCreatedEmailBody(), 
                                    true, 
                                    ConfigurationManager.AppSettings["smtpserver"]);
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        errortext = string.Format(BusinessLogicStrings.RetrieveMemeberError, new object[] { referralid, emailaddress });
                        errors.Add(new Message(MessageSources.AndrewHarper, 0, "RedeemReferralException", errortext, "", "", null));
                    }
                }

                baseResponse.TypedResponse = new AHResponse();
                if (errors.Count == 0)
                {
                    baseResponse.TypedResponse.Success = true;
                }
                else
                {
                    baseResponse.TypedResponse.Success = false;
                }
            }
            
            catch (Exception ex)
            {
                baseResponse.TypedResponse.Success = false;
                errortext = string.Format(BusinessLogicStrings.UnknownReferralError, ex.Message);
                errors.Add(new Message(MessageSources.AndrewHarper, 0, "RedeemReferralException", errortext, "", "", null));
            }
            foreach (Message error in errors)
            {
                if (baseResponse == null)
                {
                    baseResponse = new BaseResponse();
                }
                baseResponse.Messages.Add(error);

                StringBuilder error_text = new StringBuilder();
                error_text.AppendLine("REFERRAL ERROR LOGGED");
                error_text.AppendLine(string.Format("REFERRALID {0}", referralid));
                baseResponse.Messages.Add(error);
                error_text.AppendLine(string.Format("ERROR: ", new object[] { }));
                EventLogger.LogError("SubscriptionLogic.RedeemReferralSubscription", error_text.ToString(), true);
            }
            return baseResponse;
        }        
    }
}
