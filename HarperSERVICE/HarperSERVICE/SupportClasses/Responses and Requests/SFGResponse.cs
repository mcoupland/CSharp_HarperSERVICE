using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
	public abstract class SFGResponse
	{
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public bool Success = false;

        /// <summary>
        /// Array of strings.
        /// </summary>        
        [XmlElement]
        public List<string> Info = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string MemoryUsed = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public List<OptinResults> OptinResults = new List<OptinResults>();

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string Protocol = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string RoundtripTime = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string Server = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string TimeElapsed = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string Version = string.Empty;

        /// <summary>
        /// 
        /// </summary>
		public SFGResponse(){}
	}
}
