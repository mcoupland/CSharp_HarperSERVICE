using System;
using System.Configuration;
using System.Xml.Serialization;

namespace SupportClasses
{
    public class SubscriptionServiceRequest : SFGRequest
    {
        #region readonly properties
        /// <summary>
        /// Readonly, defined and set in SubscriptionServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ProgramTypeId = ConfigurationManager.AppSettings["subscription-programtypeid"];

        /// <summary>
        /// Readonly, defined and set in SubscriptionServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServiceUsername = ConfigurationManager.AppSettings["subscription-username"];

        /// <summary>
        /// Readonly, defined and set in SubscriptionServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServicePassword = ConfigurationManager.AppSettings["subscription-password"];
        #endregion

        public CreditCard CreditCardData = new CreditCard();

        /// <summary>
        /// Defined and initialized to new member in SubscriptionServiceRequest
        /// </summary>
        [XmlElement]
        public Member SubscribingMember = new Member();


        /// <summary>
        /// Defined and initialized to new member in SubscriptionServiceRequest
        /// </summary>
        [XmlElement]
        public Member GiftRecipient = new Member();

        /// <summary>
        /// Defined and initialized to 0 in SubscriptionServiceRequest
        /// SFG's "term"
        /// </summary>
        [XmlElement]
        public int SubscriptionLength = 0;


        private string _KeyCode = string.Empty;
        /// <summary>
        /// Defined and initialized to "" in SubscriptionServiceRequest
        /// </summary>
        [XmlElement]
        public string KeyCode
        {
            get { return _KeyCode; }
            set
            {
                if (value.Length > 7)
                {
                    throw new InvalidInputException("KeyCode must be less than 7 characters");
                }
                _KeyCode = value;
            }
        }
        
        /// <summary>
        /// Defined and initialized to "" in SubscriptionServiceRequest
        /// Passed in to the web service
        /// SFG's "program_id"
        /// </summary>
        [XmlElement]
        public string PublicationCode = string.Empty;

        /// <summary>
        /// Defined in SubscriptionServiceRequest
        /// Not initialized
        /// TODO: check this
        /// </summary>
        [XmlElement]
        public bool RefreshCustomer;

        [XmlElement]
        public bool GiftFlag;

        #region unused parameters
        /// <summary>
        /// Readonly, defined in SubscriptionServiceRequest
        /// Not initialized
        /// TODO: check this
        /// </summary>
        [XmlElement]
        public readonly int Copies;

        /// <summary>
        /// Readonly, defined in SubscriptionServiceRequest
        /// Not initialized
        /// TODO: check this
        /// </summary>
        [XmlElement]
        public readonly string Premium1;

        /// <summary>
        /// Readonly, defined in SubscriptionServiceRequest
        /// Not initialized
        /// TODO: check this
        /// </summary>
        [XmlElement]
        public readonly string Premium2;

        /// <summary>
        /// Readonly, defined in SubscriptionServiceRequest
        /// Not initialized
        /// TODO: check this
        /// </summary>
        [XmlElement]
        public readonly string Premium3;

        /// <summary>
        /// Readonly, defined in SubscriptionServiceRequest
        /// Not initialized
        /// TODO: check this
        /// </summary>
        [XmlElement]
        public readonly string StartIssue;

        /// <summary>
        /// Readonly, defined in SubscriptionServiceRequest
        /// Not initialized
        /// TODO: check this
        /// </summary>
        [XmlElement]
        public readonly bool AutoRenew;

        /// <summary>
        /// Not used, see SFG documentation
        /// </summary>
        [XmlElement]
        public string MemberEmailList;

        /// <summary>
        /// Not used, see SFG documentation
        /// </summary>
        [XmlElement]
        public readonly string MemberOptinList;

        /// <summary>
        /// Not used, see SFG documentation
        /// </summary>
        [XmlElement]
        public readonly string GiftRecipientEmailList;

        /// <summary>
        /// Not used, see SFG documentation
        /// </summary>
        [XmlElement]
        public readonly string GiftRecipientOptinList;
        #endregion

        public SubscriptionServiceRequest(Member memberData, Member giftData, CreditCard creditCardData, 
            string publicationcode, string keycode, bool giftflag, int subscriptionlength)
        {
            RefreshCustomer = false;
            if (
                    (memberData != null 
                    && memberData.MemberId != null 
                    &&!string.IsNullOrEmpty(memberData.MemberId.Trim())
                ) 
                || 
                (
                    giftData != null 
                    && giftData.MemberId != null 
                    && !string.IsNullOrEmpty(giftData.MemberId.Trim()))
                )
            {
                RefreshCustomer = true;                
            }
            
            SubscribingMember = memberData;
            GiftRecipient = giftData;
            CreditCardData = creditCardData;
            PublicationCode = publicationcode;
            KeyCode = keycode;
            GiftFlag = giftflag;
            SubscriptionLength = subscriptionlength;
        }

        /// <summary>
        /// Used for redeeming referrals only
        /// </summary>
        /// <param name="referral"></param>
        /// <param name="giftData"></param>
        public SubscriptionServiceRequest(HarperLINQ.Referral referral, Member giftData)
        {
            CreditCardData.PaymentType = "F";
            CreditCardData.Price = 0;
            CreditCardData.AmountPaid = 0;
            CreditCardData.VerifoneRoutingId = string.Empty;
            RefreshCustomer = false;
            HarperLINQ.tbl_Customer referrer = new HarperLINQ.tbl_Customer(referral.memberid, false);
            SubscribingMember.MemberId = referrer.SfgId.ToString();
            GiftRecipient = giftData;
            PublicationCode = referral.pubcode;
            KeyCode = referral.keycode;
            GiftFlag = true;
            SubscriptionLength = referral.subscriptionlength;
        }

        public override string ToString()
        {
            System.Text.StringBuilder response = new System.Text.StringBuilder();
            //TODO: Implement ToString() 
            return response.ToString();
        }
    }
}
