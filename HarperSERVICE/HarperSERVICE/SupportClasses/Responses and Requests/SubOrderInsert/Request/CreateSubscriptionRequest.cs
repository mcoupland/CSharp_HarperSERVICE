using System;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSubscriptionRequest : SubscriptionServiceRequest
    {
        public CreateSubscriptionRequest(Member memberData, Member giftData, string verifoneRoutingId, int subscriptionLength,
            float amountPaid, string keyCode, string pubCode, bool refreshCustomer, bool giftflag, bool debug)
        {
            Debug = debug;
            VerifoneRoutingId = verifoneRoutingId; 
            SubscriptionLength = subscriptionLength;
            Price = amountPaid;
            AmountPaid = amountPaid;
            KeyCode =  keyCode;
            RefreshCustomer = refreshCustomer;
            GiftFlag = giftflag;
            PublicationCode = pubCode;

            #region member data
            SubscribingMember.MemberId = memberData.MemberId;
            SubscribingMember.Salutation = memberData.Salutation;
            SubscribingMember.FirstName = memberData.FirstName;
            SubscribingMember.MiddleInitial = memberData.MiddleInitial;
            SubscribingMember.LastName = memberData.LastName;
            SubscribingMember.Suffix = memberData.Suffix;
            SubscribingMember.ProfessionalTitle = memberData.ProfessionalTitle;
            SubscribingMember.Email = memberData.Email;
            SubscribingMember.OptIn = memberData.OptIn;

            SubscribingMember.Address = new Address();
            SubscribingMember.Address.BusinessName = memberData.Address.BusinessName;
            SubscribingMember.Address.Address1 = memberData.Address.Address1;
            SubscribingMember.Address.Address2 = memberData.Address.Address2;
            SubscribingMember.Address.Address3 = memberData.Address.Address3;
            SubscribingMember.Address.City = memberData.Address.City;
            SubscribingMember.Address.State = memberData.Address.State;
            SubscribingMember.Address.PostalCode = memberData.Address.PostalCode;
            SubscribingMember.Address.Country = memberData.Address.Country;
            SubscribingMember.Address.Phone = memberData.Address.Phone;
            SubscribingMember.Address.AltCity = memberData.Address.AltCity;
            #endregion

            #region gift member data
            GiftRecipient.MemberId = memberData.MemberId;
            GiftRecipient.Salutation = memberData.Salutation;
            GiftRecipient.FirstName = memberData.FirstName;
            GiftRecipient.MiddleInitial = memberData.MiddleInitial;
            GiftRecipient.LastName = memberData.LastName;
            GiftRecipient.Suffix = memberData.Suffix;
            GiftRecipient.ProfessionalTitle = memberData.ProfessionalTitle;
            GiftRecipient.Email = memberData.Email;
            GiftRecipient.OptIn = memberData.OptIn;

            GiftRecipient.Address = new Address();
            GiftRecipient.Address.BusinessName = memberData.Address.BusinessName;
            GiftRecipient.Address.Address1 = memberData.Address.Address1;
            GiftRecipient.Address.Address2 = memberData.Address.Address2;
            GiftRecipient.Address.Address3 = memberData.Address.Address3;
            GiftRecipient.Address.City = memberData.Address.City;
            GiftRecipient.Address.State = memberData.Address.State;
            GiftRecipient.Address.PostalCode = memberData.Address.PostalCode;
            GiftRecipient.Address.Country = memberData.Address.Country;
            GiftRecipient.Address.Phone = memberData.Address.Phone;
            GiftRecipient.Address.AltCity = memberData.Address.AltCity;
            #endregion

        }
    }
}

