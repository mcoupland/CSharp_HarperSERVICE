using System;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateCustomerProfileRequest : CreditCardServiceRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileMemberDetail"></param>
        /// <param name="refreshCustomer"></param>
        /// <param name="optIn"></param>
        /// <param name="debug"></param>
        public UpdateCustomerProfileRequest(Member profileMember, bool refreshCustomer, bool optIn, bool debug)
        {
            base.Debug = debug;
            this.MemberData = profileMember;
            this.RefreshCustomer = refreshCustomer;
            this.OptIn = optIn;
        }
    }
}
