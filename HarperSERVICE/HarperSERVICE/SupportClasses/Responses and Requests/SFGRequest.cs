using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Configuration;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
	public abstract class SFGRequest
	{
        /// <summary>
        /// Must set in concrete class, Defined in SFGRequest class
        /// </summary>
        [XmlElement]
        public bool Debug = false;       

        /// <summary>
        /// Readonly, defined and set in SFGRequest class
        /// </summary>
        [XmlElement]
		public readonly string Org = ConfigurationManager.AppSettings["org"];

        /// <summary>
        /// Readonly, defined and set in SFGRequest class
        /// </summary>
        [XmlElement]
		public readonly bool TestMode = Utilities.StringToBoolean(ConfigurationManager.AppSettings["test-mode"]);

        /// <summary>
        /// Readonly, defined and set in SFGRequest class
        /// </summary>
        [XmlElement]
		public readonly string AppVersion = ConfigurationManager.AppSettings["app-version"];
	}
}
