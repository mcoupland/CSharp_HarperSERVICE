using System;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateUsernameRequest : UserMaintenanceServiceRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="debug"></param>
        public UpdateUsernameRequest(string newUsername, string oldUsername, string password, bool debug)
        {
            ValidationUserName = oldUsername;
            NewUser = false;
            UserName = newUsername;
            Password = password;

            Debug = debug;
        }

        public UpdateUsernameRequest(string newUsername, string oldUsername, string password, string zipcode, string sfgid, bool debug)
        {
            PostalCode = zipcode;
            NewUser = false;
            UserName = newUsername;
            Password = password;
            MemberId = sfgid;
            Debug = debug;
        }
    }
}

