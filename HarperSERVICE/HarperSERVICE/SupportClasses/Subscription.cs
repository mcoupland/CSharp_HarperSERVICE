using System;
using System.Text;

namespace SupportClasses
{
    /// <summary>
    /// Order history item class.  Used during serialization.
    /// </summary>
	public class Subscription
	{
        /// <summary>
        /// Order history item category enumeration item type.
        /// </summary>
		public SubscriptionCategory Category;
        /// <summary>
        /// Our Publication code or Product code (depending on category)
        /// </summary>
		public string PublicationCode; 
        /// <summary>
        /// Description
        /// </summary>
		public string Description;
        /// <summary>
        /// Quantity
        /// </summary>
		public int Qty;
        /// <summary>
        /// For publications, date entered; for catalog, order date
        /// </summary>
		public string DateEntered; 
        /// <summary>
        /// The date the publication expired.  MUST be greater than or equal to today minus “days_history” parameter
        /// </summary>
		public string ExpireDate; 
        /// <summary>
        /// The number of outstanding publications remaining
        /// </summary>
		public int IssuesRemaining; 
        /// <summary>
        /// This customer’s keycode to be used for renewal
        /// </summary>
		public string RenewalKeycode; 
        /// <summary>
        /// Electronic publication.
        /// </summary>
		public bool EPub;
        /// <summary>
        /// The customer number to whom this transaction belongs
        /// </summary>
		public string SubscribingMember; 
        /// <summary>
        /// The customer number of the recipient if this was a gift subscription
        /// </summary>
		public string GiftRecipient; 
        /// <summary>
        /// The outstanding balance of this record (if this customer was a gift recipient, the A/R balance will be reported as “0.00”.)
        /// </summary>
		public decimal OutstandingBalance; 
        /// <summary>
        /// One of several possible single-character values
        /// </summary>
		public string StatusFlag; 
        /// <summary>
        /// “Y/N” – Indicates if this transaction is a “gift” to CUSTOMER_NUMBER
        /// </summary>
		public bool IsGift; 
        /// <summary>
        /// “Y/N” – Indicates if CUSTOMER_NUMBER was a “donor” on this transaction
        /// </summary>
		public bool IsDonor; 
        /// <summary>
        /// Active status
        /// </summary>
		public bool IsActive
		{
			get
			{
				if (StatusFlag != null && "po".IndexOf(StatusFlag.ToString().ToLower()) != -1)
					return true;
				else
					return false;
			}
		}
        /// <summary>
        /// Active status description.
        /// </summary>
		public string StatusText
		{
			get
			{
                if (string.IsNullOrEmpty(StatusFlag))
                {
                    return "Unknown";
                }
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
					default:
						return "Unknown";
				}
			}
		}

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("Category:");
            result.Append(Category.ToString());
            result.Append("\r\n");

            result.Append("PublicationCode:");
            result.Append(PublicationCode);
            result.Append("\r\n");

            result.Append("Description:");
            result.Append(Description);
            result.Append("\r\n");

            result.Append("Qty:");
            result.Append(Qty);
            result.Append("\r\n");

            result.Append("DateEntered:");
            result.Append(DateEntered);
            result.Append("\r\n");

            result.Append("ExpireDate:");
            result.Append(ExpireDate);
            result.Append("\r\n");

            result.Append("IssuesRemaining:");
            result.Append(IssuesRemaining);
            result.Append("\r\n");

            result.Append("RenewalKeycode:");
            result.Append(RenewalKeycode);
            result.Append("\r\n");

            result.Append("EPub:");
            result.Append(EPub);
            result.Append("\r\n");

            result.Append("SubscribingMember:");
            result.Append(SubscribingMember);
            result.Append("\r\n");

            result.Append("GiftRecipient:");
            result.Append(GiftRecipient);
            result.Append("\r\n");

            result.Append("OutstandingBalance:");
            result.Append(OutstandingBalance);
            result.Append("\r\n");

            result.Append("StatusFlag:");
            result.Append(StatusFlag);
            result.Append("\r\n");

            result.Append("IsGift:");
            result.Append(IsGift);
            result.Append("\r\n");

            result.Append("IsDonor:");
            result.Append(IsDonor);
            result.Append("\r\n");

            result.Append("IsActive:");
            result.Append(IsActive);
            result.Append("\r\n");

            result.Append("StatusText:");
            result.Append(StatusText);
            result.Append("\r\n");

            return result.ToString();
        }
	}

    /// <summary>
    /// Categories available to subscriptions.
    /// </summary>
	public enum SubscriptionCategory 
    { 
        /// <summary>
        /// 
        /// </summary>
        Publication, 
        /// <summary>
        /// 
        /// </summary>
        Catalog 
    }
}
