using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFGWrapper.SFG.CustomerUpdateSvc;
using SupportClasses;

namespace SFGWrapper.Helpers
{
    public class CustomerUpdateTranslators
    {
        public static BaseResponse UpdateCustomer(returntype sfgReturn)
        {
            string className = "SFGWrapper.CustomerUpdateTranslators";
            BaseResponse baseResponse = new BaseResponse();
            foreach (var item in sfgReturn.error)
            {
                Message ahError = new Message(item.errno, MessageSources.CustomerUpdate);
                foreach (string message in item.errmsg)
                {
                    ahError.SfgMessages.Add(message);
                }
                baseResponse.Messages.Add(ahError);
            }

            UpdateMemberResponse updateMemberResponse = new UpdateMemberResponse();
            updateMemberResponse.MemberUpdated = (sfgReturn.response.CUSTOMER_UPDATED != "N");
            baseResponse.TypedResponse = updateMemberResponse;

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
        public static argtype TranslateToSfgRequest(CustomerUpdateServiceRequest ahRequest)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.CustomerUpdateTranslators.TranslateToSfgRequest()");
            argtype sfgRequest = new argtype();

            #region readonly properties
            sfgRequest.org = ahRequest.Org;
            sfgRequest.test_mode = ahRequest.TestMode ? "Y" : "N";
            sfgRequest.app_version = ahRequest.AppVersion;
            sfgRequest.program_type_id = ahRequest.ProgramTypeId;
            #endregion

            sfgRequest.customer_number = ahRequest.MemberToUpdate.MemberId;
            sfgRequest.title = ahRequest.MemberToUpdate.Salutation;
            sfgRequest.first = ahRequest.MemberToUpdate.FirstName;
            sfgRequest.mi = ahRequest.MemberToUpdate.MiddleInitial;
            sfgRequest.last = ahRequest.MemberToUpdate.LastName;
            sfgRequest.suffix = ahRequest.MemberToUpdate.Suffix;
            sfgRequest.professional_title = ahRequest.MemberToUpdate.ProfessionalTitle;
            sfgRequest.email = ahRequest.MemberToUpdate.Email;
            sfgRequest.optin = ahRequest.MemberToUpdate.OptIn ? "Y" : "N";

            sfgRequest.business_name = ahRequest.MemberToUpdate.Address.BusinessName;
            sfgRequest.add1 = ahRequest.MemberToUpdate.Address.Address1;
            sfgRequest.add2 = ahRequest.MemberToUpdate.Address.Address2;
            sfgRequest.add3 = ahRequest.MemberToUpdate.Address.Address3;
            sfgRequest.city = ahRequest.MemberToUpdate.Address.City;
            sfgRequest.st = ahRequest.MemberToUpdate.Address.State;
            sfgRequest.zip = ahRequest.MemberToUpdate.Address.PostalCode;
            sfgRequest.country = ahRequest.MemberToUpdate.Address.Country;
            sfgRequest.phone = ahRequest.MemberToUpdate.Address.Phone;
            sfgRequest.fax = ahRequest.MemberToUpdate.Address.Fax;
            sfgRequest.altcity = ahRequest.MemberToUpdate.Address.AltCity;

            EventLogger.LogEvent("LEAVING -> SFGWrapper.CustomerUpdateTranslators.TranslateToSfgRequest()");
            return sfgRequest;
        }
    }
}
