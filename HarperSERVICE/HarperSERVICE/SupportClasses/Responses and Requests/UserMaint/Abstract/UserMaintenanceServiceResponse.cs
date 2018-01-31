using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SupportClasses
{
    public abstract class UserMaintenanceServiceResponse : SFGResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public bool UpdateSucceeded = false;
    }
}
