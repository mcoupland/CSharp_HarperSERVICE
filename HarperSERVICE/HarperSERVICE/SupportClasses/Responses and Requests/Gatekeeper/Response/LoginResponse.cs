using System;
using System.Collections.Generic;

namespace SupportClasses
{
    public class LoginResponse : GatekeeperServiceResponse
    {
        public static LoginResponse GetBaseLoginResponse(bool authenticated)
        {
            LoginResponse value = new LoginResponse();
            value.Authenticated = authenticated;
            value.MemberFound = authenticated;
            value.Success = authenticated;
            value.WebAccountFound = authenticated;
            return value;
        }
    }
}
