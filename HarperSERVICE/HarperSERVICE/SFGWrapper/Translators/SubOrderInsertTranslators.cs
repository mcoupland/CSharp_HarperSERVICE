using System;
using System.Collections.Generic;
using System.Linq;
using SFGWrapper.SFG.SubOrderInsertSvc;
using SupportClasses;

namespace SFGWrapper.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class SubOrderInsertTranslators
    {
        public static BaseResponse CreateSubscription(returntype sfgReturn)
        {
            string className = "SubOrderInsertTranslators.CreateSubscription";
            BaseResponse baseResponse = new BaseResponse();
            foreach (var item in sfgReturn.error)
            {
                Message ahError = new Message(item.errno, MessageSources.SubOrderInsert);
                foreach (string message in item.errmsg)
                {
                    ahError.SfgMessages.Add(message);
                }
                baseResponse.Messages.Add(ahError);
            }
            SubscriptionServiceResponse createSubscriptionResponse = new SubscriptionServiceResponse();
            foreach (var item in sfgReturn.response.C_EMAIL_RESULTS)
            {
                createSubscriptionResponse.MemberEmailResults.Add(GetEmailResultType(item));
            }
            foreach (var item in sfgReturn.response.C_OPTIN_RESULTS)
            {
                createSubscriptionResponse.MemberOptinResults.Add(GetOptionResults(item));
            }
            createSubscriptionResponse.MemberId = sfgReturn.response.CUSTOMER_NUMBER;
            createSubscriptionResponse.MemberUpdated = sfgReturn.response.CUSTOMER_UPDATED;
            foreach (var item in sfgReturn.response.G_EMAIL_RESULTS)
            {
                createSubscriptionResponse.GifteeEmailResults.Add(GetEmailResultType(item));
            }
            foreach (var item in sfgReturn.response.G_OPTIN_RESULTS)
            {
                createSubscriptionResponse.GifteeOptinResults.Add(GetOptionResults(item));
            }
            createSubscriptionResponse.OrderAdded = sfgReturn.response.ORDER_ADDED == "Y";
            baseResponse.TypedResponse = createSubscriptionResponse;

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
        
        public static argtype TranslateToSfgRequest(SubscriptionServiceRequest ahRequest)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.SubOrderInsertTranslators.TranslateToSfgRequest()");
            argtype sfgRequest = new argtype();

            #region readonly properties
            sfgRequest.org = ahRequest.Org;
            sfgRequest.test_mode = ahRequest.TestMode ? "Y" : "N";
            sfgRequest.app_version = ahRequest.AppVersion;
            sfgRequest.program_type_id = ahRequest.ProgramTypeId;
            #endregion

            sfgRequest.payment_type = ahRequest.CreditCardData.PaymentType;
            sfgRequest.program_id = ahRequest.PublicationCode;
            sfgRequest.term = ahRequest.SubscriptionLength;
            sfgRequest.price = ahRequest.CreditCardData.Price.ToString("F");
            sfgRequest.postage = ahRequest.CreditCardData.ShippingHandling.ToString("F");
            sfgRequest.tax = ahRequest.CreditCardData.Tax.ToString("F");
            sfgRequest.amount_paid = ahRequest.CreditCardData.AmountPaid.ToString("F");
            sfgRequest.key_code = ahRequest.KeyCode;
            sfgRequest.cc_route_no = ahRequest.CreditCardData.VerifoneRoutingId;
            sfgRequest.refresh_customer = ahRequest.RefreshCustomer ? "Y" : "N";

            #region member data
            sfgRequest.customer_number = ahRequest.SubscribingMember.MemberId;
            sfgRequest.c_title = ahRequest.SubscribingMember.Salutation;
            sfgRequest.c_first = ahRequest.SubscribingMember.FirstName;
            sfgRequest.c_mi = ahRequest.SubscribingMember.MiddleInitial;
            sfgRequest.c_last = ahRequest.SubscribingMember.LastName;
            sfgRequest.c_suffix = ahRequest.SubscribingMember.Suffix;
            sfgRequest.c_professional_title = ahRequest.SubscribingMember.ProfessionalTitle;
            sfgRequest.c_business_name = ahRequest.SubscribingMember.Address.BusinessName;
            sfgRequest.c_add1 = ahRequest.SubscribingMember.Address.Address1;
            sfgRequest.c_add2 = ahRequest.SubscribingMember.Address.Address2;
            sfgRequest.c_add3 = ahRequest.SubscribingMember.Address.Address3;
            sfgRequest.c_city = ahRequest.SubscribingMember.Address.City;
            sfgRequest.c_st = ahRequest.SubscribingMember.Address.State;
            sfgRequest.c_zip = ahRequest.SubscribingMember.Address.PostalCode;
            sfgRequest.c_country = ahRequest.SubscribingMember.Address.Country;
            sfgRequest.c_phone = ahRequest.SubscribingMember.Address.Phone;
            sfgRequest.c_fax = ahRequest.SubscribingMember.Address.Fax;
            sfgRequest.c_altcity = ahRequest.SubscribingMember.Address.AltCity;
            sfgRequest.c_email = ahRequest.SubscribingMember.Email;
            sfgRequest.c_optin = ahRequest.SubscribingMember.OptIn ? "Y" : "N";

            #endregion

            #region gift member data
            sfgRequest.gift_flag = ahRequest.GiftFlag ? "Y" : "N";
            sfgRequest.gift_number = ahRequest.GiftRecipient.MemberId;
            sfgRequest.g_title = ahRequest.GiftRecipient.Salutation;
            sfgRequest.g_first = ahRequest.GiftRecipient.FirstName;
            sfgRequest.g_mi = ahRequest.GiftRecipient.MiddleInitial;
            sfgRequest.g_last = ahRequest.GiftRecipient.LastName;
            sfgRequest.g_suffix = ahRequest.GiftRecipient.Suffix;
            sfgRequest.g_professional_title = ahRequest.GiftRecipient.ProfessionalTitle;
            sfgRequest.g_business_name = ahRequest.GiftRecipient.Address.BusinessName;
            sfgRequest.g_add1 = ahRequest.GiftRecipient.Address.Address1;
            sfgRequest.g_add2 = ahRequest.GiftRecipient.Address.Address2;
            sfgRequest.g_add3 = ahRequest.GiftRecipient.Address.Address3;
            sfgRequest.g_city = ahRequest.GiftRecipient.Address.City;
            sfgRequest.g_st = ahRequest.GiftRecipient.Address.State;
            sfgRequest.g_zip = ahRequest.GiftRecipient.Address.PostalCode;
            sfgRequest.g_country = ahRequest.GiftRecipient.Address.Country;
            sfgRequest.g_phone = ahRequest.GiftRecipient.Address.Phone;
            sfgRequest.g_email = ahRequest.GiftRecipient.Email;
            sfgRequest.g_optin = ahRequest.GiftRecipient.OptIn ? "Y" : "N";
            #endregion

            EventLogger.LogEvent("LEAVING -> SFGWrapper.SubOrderInsertTranslators.TranslateToSfgRequest()");
            return sfgRequest;
        }

        private static OptinResults GetOptionResults(optinresulttype optResult)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.SubOrderInsertTranslators.GetOptionResults()");
            OptinResults retVal = new OptinResults();
            try
            {
                retVal.MemberId = optResult.customer_number;
                retVal.Email = optResult.email;
                retVal.EmailId = optResult.email_id;
                retVal.EpTextId = optResult.eptextid;
                retVal.EpType = optResult.eptype;
                retVal.Errors = optResult.errors.ToList();
                retVal.Optin = optResult.optin;
                retVal.ProductLine = optResult.product_line;
                retVal.Source = optResult.source;
                retVal.Success = optResult.success;
                retVal.SuccessSpecified = optResult.successSpecified;
            }
            catch
            { }
            EventLogger.LogEvent("LEAVING -> SFGWrapper.SubOrderInsertTranslators.GetOptionResults()");

            return retVal;
        }
        private static EmailResultType GetEmailResultType(emailresulttype emailResult)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.SubOrderInsertTranslators.GetEmailResultType()");
            EmailResultType retVal = new EmailResultType();
            try
            {
                retVal.MemberId = emailResult.customer_number;
                retVal.EmailActive = emailResult.email_active;
                retVal.EmailId = emailResult.email_id;
                retVal.EmailStatus = emailResult.email_status;
                retVal.EmailType = emailResult.email_type;
                retVal.Email = emailResult.email;
                retVal.Errors = emailResult.errors.ToList();
                retVal.ProcessType = emailResult.process_type;
                retVal.Source = emailResult.source;
                retVal.Success = emailResult.success;
                retVal.SuccessSpecified = emailResult.successSpecified;
                retVal.UpdateAll = emailResult.update_all;
            }
            catch { }
            EventLogger.LogEvent("LEAVING -> SFGWrapper.SubOrderInsertTranslators.GetEmailResultType()");

            return retVal;
        }
    }
}
