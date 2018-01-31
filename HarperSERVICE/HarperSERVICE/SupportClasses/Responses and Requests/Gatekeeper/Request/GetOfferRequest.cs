using System;
using System.Xml.Serialization;
using System.Configuration;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class GetOfferRequest : GatekeeperServiceRequest
    {
        public GetOfferRequest(string keycode, bool debug)
        {
            base.Debug = debug;
            this.OffersKeyCode = keycode;
            this.MemberId = ConfigurationManager.AppSettings["getoffer-memberid"];
            this.CheckPassword = false;
            this.LoadCustomer = false;
            this.LoadHistory = false;
            this.LoadRenewalOffers = false;
            this.ValidateSubscription = false;
            this.SearchByCustno = true;
        }
    }
}

