using System;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class CreditCard
    {

        public readonly string TransactionType = "charge";

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string CCNumber = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public short CCExpMonth = 0;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public short CCExpYear = 0;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string CCName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string CCAddress = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string CCCity = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string CCState = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string CCPostalCode = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string CCCountry = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string VerifoneRoutingId = string.Empty;



        private string _Currency = "US";
        /// <summary>
        /// Defined and initialized to "US" in SubscriptionServiceRequest
        /// </summary>
        [XmlElement]
        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (value != "US" && value != "CN")
                {
                    throw new InvalidInputException("Currency must be US or CN");
                }
                _Currency = value;
            }
        }

        /// <summary>
        /// Defined and initialized to 0.0 in SubscriptionServiceRequest
        /// Price before tax
        /// </summary>
        [XmlElement]
        public float Price = 0.0f;

        /// <summary>
        /// Defined and initialized to 0.0 in SubscriptionServiceRequest
        /// SFG's "postage"
        /// </summary>
        [XmlElement]
        public readonly float ShippingHandling = 0.0f;

        /// <summary>
        /// Defined and initialized to 0.0 in SubscriptionServiceRequest
        /// </summary>
        [XmlElement]
        public readonly float Tax = 0.0f;

        /// <summary>
        /// Defined and initialized to 0.0 in SubscriptionServiceRequest
        /// </summary>
        [XmlElement]
        public float AmountPaid = 0.0f;


        /// <summary>
        /// Defined and initialized to "C" in SubscriptionServiceRequest
        /// Must be set to "F" for comp orders
        /// </summary>
        [XmlElement]
        public string PaymentType = "C"; 


        public CreditCard() { }
    }
}
