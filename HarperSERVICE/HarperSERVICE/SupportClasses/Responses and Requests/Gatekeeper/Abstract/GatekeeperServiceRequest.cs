using System;
using System.Configuration;
using System.Xml.Serialization;

namespace SupportClasses
{
    public abstract class GatekeeperServiceRequest : SFGRequest
    {
        #region readonly properties
        /// <summary>
        /// Readonly, defined and set in GatekeeperServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ProgramTypeId = ConfigurationManager.AppSettings["gatekeeper-programtypeid"];

        /// <summary>
        /// Readonly, defined and set in GatekeeperServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServiceUsername = ConfigurationManager.AppSettings["gatekeeper-username"];

        /// <summary>
        /// Readonly, defined and set in GatekeeperServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServicePassword = ConfigurationManager.AppSettings["gatekeeper-password"];
        #endregion

        #region from login
        /// <summary>
        /// Defined and initialized to "" in LoginRequest
        /// </summary>
        [XmlElement]
        public string Username = string.Empty;

        /// <summary>
        /// Defined and initialized to "" in LoginRequest
        /// </summary>
        [XmlElement]
        public string HashedPassword = string.Empty;

        /// <summary>
        /// False if getting offer
        /// </summary>
        [XmlElement]
        public bool CheckPassword = true;

        /// <summary>
        /// Readonly, defined in LoginRequest, not initialized since we never use it
        /// </summary>
        [XmlElement]
        public readonly string LogInAs;
        #endregion

        #region from getmember
        /// <summary>
        /// Defined and initialized to "" in GetMemberRequest
        /// 11-digit customer account number at SFG
        /// </summary> 
        [XmlElement]
        public string MemberId = string.Empty;

        ///// <summary>
        ///// Readonly, defined in GetMemberRequest, not initialized since 
        ///// we should only be getting members by memberid
        ///// </summary>
        //[XmlElement]
        //public readonly string Username;

        /// <summary>
        /// Readonly, defined in GetMemberRequest, not initialized since 
        /// we should only be getting members by memberid
        /// </summary>
        [XmlElement]
        public readonly string PostalCode;

        /// <summary>
        /// Readonly, defined in GetMemberRequest, not initialized since 
        /// we should only be getting members by memberid
        /// </summary>
        [XmlElement]
        public readonly string FirstName;

        /// <summary>
        /// Readonly, defined in GetMemberRequest, not initialized since 
        /// we should only be getting members by memberid
        /// </summary>
        [XmlElement]
        public readonly string LastName;

        /// <summary>
        /// Readonly, defined in GetMemberRequest, not initialized since 
        /// we should only be getting members by memberid
        /// </summary>
        [XmlElement]
        public readonly string Address;

        /// <summary>
        /// False if GetOffer
        /// </summary>
        [XmlElement]
        public bool SearchByCustno = true;
        #endregion

        #region options
        /// <summary>
        /// False if getting offer
        /// </summary>
        [XmlElement]
        public bool LoadCustomer = true;

        /// <summary>
        /// False if getting offer
        /// </summary>
        [XmlElement]
        public bool LoadHistory = true;

        /// <summary>
        /// </summary>
        [XmlElement]
        public short DaysHistory = 5000;

        /// <summary>
        /// Defined and initialized to false in GatekeeperServiceRequest
        /// If true, all “cash” offers tied to this customer’s most recent renewal key codes will be 
        /// returned (by publication code). 
        /// </summary>
        [XmlElement]
        public bool LoadRenewalOffers = false;

        /// <summary>
        /// Defined and initialized to "" in GatekeeperServiceRequest
        /// (7-alpha) The specific key code for which to return offers.
        /// </summary>
        [XmlElement]
        public string OffersKeyCode = string.Empty;

        /// <summary>
        /// Defined and initialized to false in GatekeeperServiceRequest
        /// </summary>
        [XmlElement]
        public bool ValidateSubscription = false;

        /// <summary>
        /// Readonly, defined in GatekeeperServiceRequest 
        /// not initialized because it should never be used 
        /// Convert uppercase PWs to incoming PW (sends pw in the clear and bypasses the hashed pw check)
        /// </summary>
        [XmlElement]
        public string CaseInsensitivePassword;

        #endregion       

        public override string ToString()
        {
            System.Text.StringBuilder response = new System.Text.StringBuilder();
            return response.ToString();
        }
    }
}
