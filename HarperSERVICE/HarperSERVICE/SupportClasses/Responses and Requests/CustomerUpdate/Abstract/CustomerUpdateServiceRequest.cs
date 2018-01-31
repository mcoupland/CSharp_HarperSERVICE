using System;
using System.Configuration;
using System.Xml.Serialization;

namespace SupportClasses
{
    public abstract class CustomerUpdateServiceRequest : SFGRequest
    {
        #region readonly properties
        /// <summary>
        /// Readonly, defined and set in CustomerUpdateServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ProgramTypeId = ConfigurationManager.AppSettings["customerupdate-programtypeid"];
        
        /// <summary>
        /// Readonly, defined and set in CustomerUpdateServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServiceUsername = ConfigurationManager.AppSettings["customerupdate-username"];

        /// <summary>
        /// Readonly, defined and set in CustomerUpdateServiceRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServicePassword = ConfigurationManager.AppSettings["customerupdate-password"];
        #endregion

        /// <summary>
        /// Declared and initialized in CustomerUpdateServiceRequest
        /// </summary>
        [XmlElement]
        public Member MemberToUpdate = new Member();
        
        #region optional parameters
        /// <summary>
        /// Readonly, defined in CustomerUpdateServiceRequest not used, see SFG documentation
        /// </summary>
        [XmlElement]
        public readonly string EmailList = string.Empty;

        /// <summary>
        /// Readonly, defined in CustomerUpdateServiceRequest not used, see SFG documentation
        /// </summary>
        [XmlElement]
        public readonly string OptinList = string.Empty;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            System.Text.StringBuilder response = new System.Text.StringBuilder();
            response.Append("MemberToUpdate: ");
            response.Append(MemberToUpdate.ToString());
            response.Append("EmailList: ");
            response.Append(EmailList.ToString());
            response.Append("OptinList: ");
            response.Append(OptinList.ToString());
            return response.ToString();
        }
    }
}
