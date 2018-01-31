using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SupportClasses
{
    public class CreditCardServiceResponse : SFGResponse
    {
        /// <summary>
        /// Result status of the transaction.
        /// </summary>
        [XmlElement]
        public bool TransactionSucceeded = false;

        /// <summary>
        /// 15-digit VeriFone Routing ID.  Required to be passed with any order insert 
        /// transaction for proper credit to be applied.
        /// </summary>
        [XmlElement]
        public string VerifoneRoutingId = string.Empty;

        /// <summary>
        /// 6-character transaction authorization code.
        /// </summary>
        [XmlElement]
        public string AuthorizationCode = string.Empty;

        /// <summary>
        /// 3-character response code.
        /// </summary>
        [XmlElement]
        public string ResponseCode = string.Empty;

        /// <summary>
        /// 11-digit customer number that this credit card transaction was applied to.
        /// </summary>
        [XmlElement]
        public string MemberId = string.Empty;

        /// <summary>
        /// this field is only returned if "RefreshCustomer" was 'Y'.
        /// </summary>
        [XmlElement]
        public bool MemberUpdated = false;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public List<EmailResultType> EmailResults = new List<EmailResultType>();

    }
}
