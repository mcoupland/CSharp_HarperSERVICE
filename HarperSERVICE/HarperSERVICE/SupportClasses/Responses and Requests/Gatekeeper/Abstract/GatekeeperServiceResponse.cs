using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SupportClasses
{
    public abstract class GatekeeperServiceResponse : SFGResponse
    {
        /// <summary>
        /// 'Y/N'
        /// </summary>
        [XmlElement]
        public bool WebAccountFound = false;

        /// <summary>
        /// 'Y/N'
        /// </summary>
        [XmlElement]
        public bool MemberFound = false;

        /// <summary>
        /// only set if “check_pw” is “Y”
        /// </summary>
        [XmlElement]
        public bool Authenticated = false;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public SubscriptionValidation SubscriptionValidationData = new SubscriptionValidation();

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public Member MemberData = new Member();

        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
        public List<RenewalOffer> RenewalOffers = new List<RenewalOffer>();

        /// <summary>
        /// 
        /// </summary>
        /// Causes error in SFG response XML documents because invalid ref to customerinfotype.
        [XmlElement]
        public Address ShipToAddress = new Address();

        public GatekeeperServiceResponse() { }
    }
}
