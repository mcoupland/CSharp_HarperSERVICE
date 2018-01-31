using System;
using System.Configuration;
using System.Xml.Serialization;

namespace SupportClasses
{
    public class CreditCardServiceRequest : SFGRequest
    {
        #region readonly properties 
        /// <summary>
        /// Readonly, defined and set in CreditCardServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ProgramTypeId = ConfigurationManager.AppSettings["creditcard-programtypeid"]; 


        /// <summary>
        /// Readonly, defined and set in CreditCardServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServiceUsername = ConfigurationManager.AppSettings["creditcard-username"];

        /// <summary>
        /// Readonly, defined and set in CreditCardServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServicePassword = ConfigurationManager.AppSettings["creditcard-password"];
        #endregion

        public string ProgramId = string.Empty; 
        public CreditCard CreditCardData = new CreditCard();
        public Member MemberData = new Member();
        public bool RefreshCustomer = false;
        public string UserName = string.Empty;
        public string Password = string.Empty;

        public CreditCardServiceRequest(CreditCard creditcarddata, Member memberdata, string pubcode, string username, string password, bool refreshcustomer)
        {
            CreditCardData = creditcarddata;
            MemberData = memberdata;
            ProgramId = pubcode;
            UserName = username;
            Password = password;
            RefreshCustomer = refreshcustomer;
        }

        public override string ToString()
        {
            System.Text.StringBuilder response = new System.Text.StringBuilder();
            //TODO: Implement ToString() 
            return response.ToString();
        }

    }
}
