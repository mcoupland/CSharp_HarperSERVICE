using System;
using System.Configuration;
using System.Xml.Serialization;

namespace SupportClasses
{
    public abstract class UserMaintenanceServiceRequest : SFGRequest
    {    

        #region readonly properties
        /// <summary>
        /// Readonly, defined and set in GatekeeperServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ProgramTypeId = ConfigurationManager.AppSettings["usermaint-programtypeid"];

        /// <summary>
        /// Readonly, defined and set in GatekeeperServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServiceUsername = ConfigurationManager.AppSettings["usermaint-username"];

        /// <summary>
        /// Readonly, defined and set in GatekeeperServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServicePassword = ConfigurationManager.AppSettings["usermaint-password"];
        #endregion

        #region request parameters 
        /// <summary>
        /// Defined in UserMaintenanceServiceRequest
        /// </summary>
        [XmlElement]
        public bool NewUser = false;

        /// <summary>
        /// Defined in UserMaintenanceServiceRequest
        /// </summary>
        [XmlElement]
        public string UserName = string.Empty;

        /// <summary>
        /// Defined in UserMaintenanceServiceRequest
        /// </summary>
        [XmlElement]
        public string Password = string.Empty;

        /// <summary>
        /// Defined in UserMaintenanceServiceRequest
        /// </summary>
        [XmlElement]
        public string MemberId = string.Empty;

        /// <summary>
        /// Defined in UserMaintenanceServiceRequest
        /// </summary>
        [XmlElement]
        public string PostalCode = string.Empty;

        /// <summary>
        /// Defined in UserMaintenanceServiceRequest
        /// </summary>
        [XmlElement]
        public string ValidationUserName = string.Empty;
        #endregion

        public override string ToString()
        {
            System.Text.StringBuilder response = new System.Text.StringBuilder();
            response.Append("ProgramTypeID: ");
            response.Append(ProgramTypeId.ToString());
            response.Append("ServiceUsername: ");
            response.Append(ServiceUsername.ToString());
            response.Append("ServicePassword: ");
            response.Append(ServicePassword.ToString());
            response.Append("NewUser: ");
            response.Append(NewUser.ToString());
            response.Append("UserName: ");
            response.Append(UserName.ToString());
            response.Append("Password: ");
            response.Append(Password.ToString());
            response.Append("MemberId: ");
            response.Append(MemberId.ToString());
            response.Append("PostalCode: ");
            response.Append(PostalCode.ToString());
            response.Append("ValidationMemberId: ");
            response.Append(ValidationUserName.ToString());
            return response.ToString();
        }
    }
}
