using System;
using System.Configuration;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
	public class LoginRequest : GatekeeperServiceRequest
    {
        public LoginRequest(string userName, string password)
        {
            Debug = false;
            Username = userName;
            HashedPassword = Utilities.HashPassword(password);
            CaseInsensitivePassword = password;
            //cannot validate on login because we don't know the member's pub code before login
            //ValidateSubscription = true;
            LoadRenewalOffers = true;
        }
	}
}

