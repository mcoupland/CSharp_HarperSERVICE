using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFGWrapper;
using SupportClasses;

namespace BusinessLogic
{
    public partial class MembershipLogic
    {
        public BaseResponse UpdateMemberProfile(string memberid,
            string salutation, string firstname, string middleinitial, string lastname, string suffix,
            string professionaltitle, string email, bool optin, string businessname, string address1, string address2, string address3,
            string city, string state, string postalcode, string country,
            string phone, string fax, string altcity,
            bool debug)
        {
            methodName = "UpdateMemberProfile";
            LogMethodEntry(methodName);
            try
            {
                #region  set member data
                Member memberData = new Member();
                memberData.MemberId = memberid;
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
                #endregion

                UpdateMemberRequest request = new UpdateMemberRequest(memberData, debug);
                baseResponse = CustomerUpdate.UpdateMember(request);
            }
            catch (InvalidInputException)
            {
                //need to add and handle input validation errors
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
            }
            LogMethodExit(methodName);
            return baseResponse;
        }        
        public BaseResponse GetMemberProfile(string memberId, bool debug)
        {
            methodName = "GetMemberProfile";
            LogMethodEntry(methodName);
            try
            {
                #region validate input
                // All params are required 
                if (memberId.Trim() == "")
                {
                    baseResponse.Messages.Add(new Message("ImproperValidationCriteriaException"));
                    return baseResponse;
                }
                #endregion

                GetMemberByMemberIdRequest request = new GetMemberByMemberIdRequest(memberId.Trim(), debug);
                baseResponse = Gatekeeper.GetMemberByMemberId(request);
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
            }
            LogMethodExit(methodName);
            return baseResponse;
        }                
        public BaseResponse GetSubscriptionOffers(string memberId, bool debug)
        {
            methodName = "GetSubscriptionOffers";
            LogMethodEntry(methodName);
            try
            {
                if (memberId.Trim() == "")
                {
                    baseResponse.Messages.Add(new Message("ImproperValidationCriteriaException"));
                    return baseResponse;
                }
                GetMemberByMemberIdRequest request = new GetMemberByMemberIdRequest(memberId.Trim(), debug);
                request.LoadRenewalOffers = true;
                request.OffersKeyCode = "E000764";
                baseResponse = Gatekeeper.GetMemberByMemberId(request);
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
            }
            LogMethodExit(methodName);
            return baseResponse;
        }       
        public BaseResponse GetCurrentSubscription(string memberId, bool debug){throw new NotImplementedException();}
        public BaseResponse GetSubscriptionHistory(string memberId, bool debug){throw new NotImplementedException();}
        public BaseResponse GetMemberContacts(string memberId, bool debug){throw new NotImplementedException();}
        public BaseResponse GetPrimaryMemberContact(string memberId, bool debug){throw new NotImplementedException();}
    }
}
