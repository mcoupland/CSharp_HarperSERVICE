using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// The base response container for all response objects returned from the AH_MemberService web service.
    /// </summary>
    [System.Xml.Serialization.XmlInclude(typeof(ReferralResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(BaseResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(FatalErrorResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(PingResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(SFGResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(CreditCardServiceResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(UpdateMemberResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(GetMemberResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(LoginResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(AHResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(CreateLoginResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(UpdatePasswordResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(UpdateUsernameResponse))]
    [System.Xml.Serialization.XmlInclude(typeof(SubscriptionServiceResponse))]
        
    [System.Xml.Serialization.XmlInclude(typeof(Address))]
    [System.Xml.Serialization.XmlInclude(typeof(EmailResultType))]
    [System.Xml.Serialization.XmlInclude(typeof(Message))]
    [System.Xml.Serialization.XmlInclude(typeof(Member))]
    [System.Xml.Serialization.XmlInclude(typeof(OptinResults))]
    [System.Xml.Serialization.XmlInclude(typeof(RenewalOffer))]
    [System.Xml.Serialization.XmlInclude(typeof(Subscription))]
    [System.Xml.Serialization.XmlInclude(typeof(SubscriptionValidation))]

    public class BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>        
        [XmlElement]
        public SFGResponse TypedResponse = null;

        /// <summary>
        /// 
        /// </summary>    
        [XmlElement]
        public List<Message> Messages = new List<Message>();

        /// <summary>
        /// 
        /// </summary>    
        [XmlElement]
        public List<Message> DebugLog = new List<Message>();

        /// <summary>
        /// 
        /// </summary>    
        [XmlElement]
        public List<string> DebugStringLog = new List<string>();
    }

}
