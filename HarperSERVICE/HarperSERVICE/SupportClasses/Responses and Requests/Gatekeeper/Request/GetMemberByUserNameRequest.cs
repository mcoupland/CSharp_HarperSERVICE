using System;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMemberByUserNameRequest : GatekeeperServiceRequest
    {
        public GetMemberByUserNameRequest(string userName, bool debug)
        {
            base.Debug = debug;
            this.Username = Utilities.NullOrEmptyToString(userName);
        }
    }
}

