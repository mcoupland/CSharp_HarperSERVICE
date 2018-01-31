using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SupportClasses
{
    public class SubscriptionServiceResponse : SFGResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public List<EmailResultType> MemberEmailResults = new List<EmailResultType>();

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public List<OptinResults> MemberOptinResults = new List<OptinResults>();


        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public List<EmailResultType> GifteeEmailResults;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public List<OptinResults> GifteeOptinResults;

        /// <summary>
        /// Whether or not the order was successfully added.
        /// </summary>
        [XmlElement]
        public bool OrderAdded;
        
        /// <summary>
        /// 11-digit account number for this order.
        /// </summary>
        [XmlElement]
        public string MemberId;
        /// <summary>
        /// 'Y/N' - This field is only returned if "refresh-customer" was 'Y'.
        /// </summary>
        [XmlElement]
        public string MemberUpdated;   
    }
}
