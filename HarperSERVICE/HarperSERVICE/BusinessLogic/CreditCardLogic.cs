using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFGWrapper;
using SupportClasses;

namespace BusinessLogic
{
    public class CreditCardLogic
    {
        BaseResponse baseResponse = new BaseResponse();
        string methodName = string.Empty;

        private void LogMethodError(string methodName, Exception exceptionCaught) { EventLogger.LogError("BusinessLogic.SubscriptionLogic.{0}()", string.Format("Message: {0} \r\nStackTrace: {1}", exceptionCaught.Message, exceptionCaught.StackTrace)); }

        //public BaseResponse ChargeCard(string memberid, string salutation, string firstname, string middleinitial, string lastname, string suffix,
        //    string professionaltitle, string email, string optin, string businessname, string address1, string address2, string address3,
        //    string city, string state, string postalcode, string country,
        //    string phone, string fax, string altcity,
        //    string ccnumber, short expmonth, short expyear, float amount, 
        //    string ccname, string ccaddr, string cccity,
        //    string ccstate, string cczip, string pubcode, string username, string password, bool refreshcustomer)
        //{
        //    methodName = "ChargeCard";

        //    bool b_optin = false;

        //    if (!bool.TryParse(optin, out b_optin))
        //    {
        //        response.ResponseCode = 203;
        //    }

        //    try
        //    {
        //        #region validate input
        //        Member memberData = new Member();
        //        memberData.MemberId = memberid;
        //        memberData.Salutation = salutation;
        //        memberData.FirstName = firstname;
        //        memberData.MiddleInitial = middleinitial;
        //        memberData.LastName = lastname;
        //        memberData.Suffix = suffix;
        //        memberData.ProfessionalTitle = professionaltitle;
        //        memberData.OptIn = optin;
        //        memberData.Email = email;

        //        memberData.Address = new Address();
        //        memberData.Address.BusinessName = businessname;
        //        memberData.Address.Address1 = address1;
        //        memberData.Address.Address2 = address2;
        //        memberData.Address.Address3 = address3;
        //        memberData.Address.City = city;
        //        memberData.Address.State = state;
        //        memberData.Address.PostalCode = postalcode;
        //        memberData.Address.Country = country;
        //        memberData.Address.Phone = phone;
        //        memberData.Address.Fax = fax;
        //        memberData.Address.AltCity = altcity;

        //        CreditCard ccData = new CreditCard();
        //        ccData.CCNumber = ccnumber;
        //        ccData.CCExpMonth = expmonth;
        //        ccData.CCExpYear = expyear;
        //        ccData.AmountPaid = amount;
        //        ccData.CCName = ccname;
        //        ccData.CCAddress = ccaddr;
        //        ccData.CCCity = cccity;
        //        ccData.CCState = ccstate;
        //        ccData.CCPostalCode = cczip;
        //        #endregion

        //        CreditCardServiceRequest request = new CreditCardServiceRequest(ccData, memberData, pubcode, username, password, refreshcustomer);
        //        baseResponse = CreditCardProcessing.GetResponse(request);

        //        #region custom error handling
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        LogMethodError(methodName, ex);
        //    }
            
        //    return baseResponse;
        //}
    }
}
