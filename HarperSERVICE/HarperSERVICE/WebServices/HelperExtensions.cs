using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace MemberServices
{
    public static class HelperExtensions
    {
        public static Dictionary<string, string> GetDemographics(this stormpost.api.Recipient recp)
        {
            Dictionary<string, string> output = new Dictionary<string, string>();
            output = new Dictionary<string, string>();
            foreach (string str in recp.demographics)
            {
                string[] kv = str.Split(new char[] { '=' });
                output.Add(kv[0], kv[1]);
            }
            return output;
        }

        public static void SetDemographic(this stormpost.api.Recipient recp, string name, string value)
        {

            Dictionary<string, string> demographics;
            int idx;

            if (recp.demographics == null)
                recp.demographics = new string[0];
            
            demographics = recp.GetDemographics();

            if (!demographics.Keys.Contains(name))
            {
                string[] demos = recp.demographics;
                idx= recp.demographics.Length;
                Array.Resize(ref demos, idx + 1);
                recp.demographics = demos;
            }
            else
            {
                idx = Array.IndexOf(demographics.Keys.ToArray(), name);
            }
            recp.demographics[idx] = String.Format("{0}={1}", name, value);
        }

    }
}