using System;
using System.Xml.Serialization;
using System.Configuration;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdatePasswordRequest : UserMaintenanceServiceRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newPassword"></param>
        /// <param name="debug"></param>
        public UpdatePasswordRequest(string userName, string newPassword, bool debug)
        {
            UserName = userName;            //Set usename to MemberId when changing the password.
            ValidationUserName = userName;  //ValidateMemberId and UserName are required to match by SFG req's.
            NewUser = false;
            Password = newPassword;

            Debug = debug;
        }
    }
}

