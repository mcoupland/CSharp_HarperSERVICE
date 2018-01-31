using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupportClasses
{
    public class CreateLoginRequest : UserMaintenanceServiceRequest
    {
        /// <summary>
        /// Creates a new user and set an initial password.        
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="userName"></param>
        /// <param name="initialPassword"></param>
        /// <param name="postalCode"></param>
        /// <param name="debug"></param>
        public CreateLoginRequest(string memberId, string userName, string initialPassword, string postalCode, bool debug)
        {
            NewUser = true;
            MemberId = memberId;
            UserName = userName;
            PostalCode = postalCode;
            Password = initialPassword;

            Debug = debug;
        }
    }
}
