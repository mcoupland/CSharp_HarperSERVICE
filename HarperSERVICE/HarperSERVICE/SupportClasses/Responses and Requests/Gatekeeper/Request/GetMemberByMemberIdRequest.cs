using System;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMemberByMemberIdRequest : GatekeeperServiceRequest
    {
        public GetMemberByMemberIdRequest(string memberId, bool debug)
        {
            base.Debug = debug;
            this.MemberId = Utilities.NullOrEmptyToString(memberId);
        }
    }
}

