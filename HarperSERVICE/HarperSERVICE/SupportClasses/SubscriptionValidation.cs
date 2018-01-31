using System;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
	public class SubscriptionValidation
	{
        /// <summary>
        /// “Y/N” – if subscription has status of “(P)aid” or “(O)pen A/R”, will be “Y” 
        /// </summary>
        [XmlElement]
        public bool Access; 
        /// <summary>
        /// One of several possible single-character values
        /// </summary>
        [XmlElement]
		public string StatusFlag; 
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
		public string StatusText
		{
			get
			{
				switch (StatusFlag.ToString().ToLower())
                {
                    case "b":
                        return "Bad debt";
					case "e":
						return "Expired";
					case "o":
						return "Open A/R active";
					case "p":
						return "Paid active";
					case "s":
						return "Credit Suspend";
					case "i":
						return "Inactive";
                    case "z":
                        return "zero dollar membership";
					default:
						return "Unknown";
				}
			}
		}
	}
}
