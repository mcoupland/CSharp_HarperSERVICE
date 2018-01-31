using System;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class ChargeCardRequest : CreditCardServiceRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberCreditCard"></param>
        /// <param name="transactionType"></param>
        /// <param name="amount"></param>
        /// <param name="debug"></param>
        public ChargeCardRequest(CreditCard creditCardData, Member memberData, float amount, bool debug)
        {
            base.Debug = debug;
            this.CreditCardData = creditCardData;
            this.MemberData = memberData;
            this.Amount = amount;
        }
    }
}
