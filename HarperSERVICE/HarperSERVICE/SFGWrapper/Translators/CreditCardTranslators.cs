using System;
using System.Collections.Generic;
using System.Linq;
using SFGWrapper.SFG.CreditCardProcessingSvc;
using SupportClasses;

namespace SFGWrapper
{
    /// <summary>
    /// 
    /// </summary>
    public class CreditCardTranslators
    {
        public static BaseResponse GetResponse(returntype sfgReturn)
        {
            string className = "SFGWrapper.CreditCardTranslators";
            BaseResponse baseResponse = new BaseResponse();
            foreach (var item in sfgReturn.error)
            {
                Message ahError = new Message(item.errno, MessageSources.CreditCard);
                foreach (string message in item.errmsg)
                {
                    ahError.SfgMessages.Add(message);
                }
                baseResponse.Messages.Add(ahError);
            }

            CreditCardServiceResponse chargeCardResponse = new CreditCardServiceResponse();
            chargeCardResponse.AuthorizationCode = sfgReturn.response.CC_AUTH_CODE;
            chargeCardResponse.ResponseCode = sfgReturn.response.CC_RESPONSE_CODE;
            chargeCardResponse.VerifoneRoutingId = sfgReturn.response.CC_ROUTE_NO;
            chargeCardResponse.TransactionSucceeded = sfgReturn.response.CC_TRANS_SUCCEEDED == "Y";
            chargeCardResponse.MemberId = sfgReturn.response.CUSTOMER_NUMBER;
            chargeCardResponse.MemberUpdated = sfgReturn.response.CUSTOMER_UPDATED == "Y";
            foreach (var item in sfgReturn.response.EMAIL_RESULTS)
            {
                chargeCardResponse.EmailResults.Add(GetEmailResults(item));
            }
            foreach (var item in sfgReturn.response.OPTIN_RESULTS)
            {
                chargeCardResponse.OptinResults.Add(GetOptinResults(item));
            }
            baseResponse.TypedResponse = chargeCardResponse;

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
        public static argtype TranslateToSfgRequest(CreditCardServiceRequest ahRequest)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.CreditCardTranslators.TranslateToSfgRequest()");
            argtype sfgRequest = new argtype();

            #region readonly properties
            sfgRequest.org = ahRequest.Org;
            sfgRequest.test_mode = ahRequest.TestMode ? "Y" : "N";
            sfgRequest.app_version = ahRequest.AppVersion;
            sfgRequest.program_id = ahRequest.ProgramId;
            sfgRequest.program_type_id = ahRequest.ProgramTypeId;
            #endregion
                        
            sfgRequest.cc_name = ahRequest.CreditCardData.CCName;
            sfgRequest.cc_number = ahRequest.CreditCardData.CCNumber;
            sfgRequest.cc_exp_mm = ahRequest.CreditCardData.CCExpMonth.ToString().PadLeft(2,'0');
            sfgRequest.cc_exp_yyyy = ahRequest.CreditCardData.CCExpYear.ToString().PadLeft(4,'0');
            sfgRequest.cc_amount = ahRequest.CreditCardData.AmountPaid;
            sfgRequest.cc_trans_type = ahRequest.CreditCardData.TransactionType;

            sfgRequest.cc_addr = ahRequest.CreditCardData.CCAddress;
            sfgRequest.cc_city = ahRequest.CreditCardData.CCCity;
            sfgRequest.cc_state = ahRequest.CreditCardData.CCState;
            sfgRequest.cc_zip = ahRequest.CreditCardData.CCPostalCode;
            sfgRequest.cc_country = ahRequest.CreditCardData.CCCountry;

            sfgRequest.business_name = ahRequest.MemberData.Address.BusinessName;
            sfgRequest.add1 = ahRequest.MemberData.Address.Address1;
            sfgRequest.add2 = ahRequest.MemberData.Address.Address2;
            sfgRequest.add3 = ahRequest.MemberData.Address.Address3;
            sfgRequest.city = ahRequest.MemberData.Address.City;
            sfgRequest.st = ahRequest.MemberData.Address.State;
            sfgRequest.zip = ahRequest.MemberData.Address.PostalCode;
            sfgRequest.phone = ahRequest.MemberData.Address.Phone;
            sfgRequest.fax = ahRequest.MemberData.Address.Fax;
            sfgRequest.altcity = ahRequest.MemberData.Address.AltCity;
            sfgRequest.country = ahRequest.MemberData.Address.Country;
            sfgRequest.title = ahRequest.MemberData.Salutation;
            sfgRequest.first = ahRequest.MemberData.FirstName;
            sfgRequest.mi = ahRequest.MemberData.MiddleInitial;
            sfgRequest.last = ahRequest.MemberData.LastName;
            sfgRequest.suffix = ahRequest.MemberData.Suffix;
            sfgRequest.professional_title = ahRequest.MemberData.ProfessionalTitle;
            sfgRequest.email = ahRequest.MemberData.Email;
            sfgRequest.optin = ahRequest.MemberData.OptIn ? "Y" : "N";

            sfgRequest.refresh_customer = "N";

            EventLogger.LogEvent("LEAVING -> SFGWrapper.CreditCardTranslators.TranslateToSfgRequest()");
            return sfgRequest;
        }

        private static OptinResults GetOptinResults(optinresulttype optResult)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.CreditCardTranslators.GetOptionResults()");
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
            EventLogger.LogEvent("LEAVING -> SFGWrapper.CreditCardTranslators.GetOptionResults()");
            return retVal;
        }
        private static EmailResultType GetEmailResults(emailresulttype emailResult)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.CreditCardTranslators.GetEmailResults()");
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
            EventLogger.LogEvent("LEAVING -> SFGWrapper.CreditCardTranslators.GetEmailResults()");
            return retVal;
        }
    }
}
