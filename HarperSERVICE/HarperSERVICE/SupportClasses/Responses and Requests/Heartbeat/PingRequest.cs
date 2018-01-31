using System;
using System.Xml.Serialization;
using System.Configuration;

namespace SupportClasses
{
    /// <summary>
    /// Builds the request object for the SFG Heartbeat service call.
    /// </summary>
    public class PingRequest : SFGRequest
    {
        #region readonly properties
        /// <summary>
        /// Readonly, defined and set in PingRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ProgramTypeId = ConfigurationManager.AppSettings["heartbeat-programtypeid"];

        /// <summary>
        /// Readonly, defined and set in PingRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServiceUsername = ConfigurationManager.AppSettings["heartbeat-username"];

        /// <summary>
        /// Readonly, defined and set in PingRequest (from web.config)
        /// </summary>
        [XmlElement]
        public readonly string ServicePassword = ConfigurationManager.AppSettings["heartbeat-password"];
        
        /// <summary>
        /// Readonly, defined and set to "" in PingRequest
        /// </summary>
        [XmlElement]
        public readonly string org = String.Empty;
        #endregion
        
        public PingRequest()
        {
            base.Debug = false;
        }

        public override string ToString()
        {
            System.Text.StringBuilder response = new System.Text.StringBuilder();
            return response.ToString();
        }
    }
}
