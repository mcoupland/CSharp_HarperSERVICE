using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace SupportClasses
{
    public static class Utilities
    {
        public static string GetStringFromDB(object value)
        {
            return value.GetType().Name == "DBNull" ? string.Empty : value.ToString();
        }
        public static string NullOrEmptyToString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            else
                return text;
        }
        public static bool StringToBoolean(string value)
        {
            if (value != null)
                switch (value.ToUpper().Trim())
                {
                    case "TRUE": return true;
                    case "FALSE": return false;
                    case "N": return false;
                    case "Y": return true;
                    case "NO": return false;
                    case "YES": return true;
                    case "0": return false;
                    case "1": return true;
                    default:
                        return false;
                }
            return false;
        }
        public static string GetPasswordSalt()
        {
            byte[] charBuffer = new byte[16];
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 16; i++)
            {
                charBuffer[i] = Convert.ToByte(Convert.ToInt32(Math.Floor(26 * rand.NextDouble() + 65)));
            }
            return Encoding.ASCII.GetString(charBuffer);
        }
        public static string HashPassword(string password)
        {
            string salt = GetPasswordSalt();            
            string saltpass = salt + password;
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] hash = sha1.ComputeHash(Encoding.ASCII.GetBytes(saltpass));
            string hashstr = "";
            for (int i = 0; i < hash.Length; i++)
            {
                string part = hash[i].ToString("X").ToLower();
                if (part.Length == 1)
                {
                    part = "0" + part;
                }
                hashstr += part;
            }
            return salt + (hashstr);          
        }
        public static List<string> GetInfo(string[] sfgObject)
        {
            List<string> ahObject = new List<string>();
            foreach (string item in sfgObject)
            {
                ahObject.Add(item);
            }
            return ahObject;
        }        
    }
}
