using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// Corresponds to the customer's most recent "renewal keycode(s)," if load_renewal_offers is "Y", 
    /// OR to the offers_key_code if that is supplied, or both.
    /// </summary>
	public class RenewalOffer
	{
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public string KeyCode;
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public string PublicationCode;
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public string AgencyCode;
        /// <summary>
        /// “USA,” “CAN,” or “INT”
        /// </summary>
        [XmlElement]
		public string CountryCode; 
        /// <summary>
        /// “R” for renewal, “S” for sub
        /// </summary>
        [XmlElement]
		public char SubRenewalFlag; 
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public string SubscriptionTerm;
        /// <summary>
        /// “C” for cash, “R” for credit
        /// </summary>
        [XmlElement]
		public char CashOrCreditOffer; 
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public decimal GrossValue;
        /// <summary>
        /// “0001-01-01” means “unassigned”
        /// </summary>
        [XmlElement]
		public DateTime StartDate;
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public string StartIssue;
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public short NumberFreeIssues;
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public string StartBillingIssue;
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public short NumberGraceIssues;
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public short NumberOfInstallments;
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public decimal Postage;
	}
}
