using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Configuration;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class Message
    {
        #region static list of pre-defined messages
        private static List<Message> predefinedMessages = new List<Message>();
        public static List<Message> PredefinedMessages
        {
            get
            {
                if (Message.predefinedMessages == null ||
                    Message.predefinedMessages.Count <= 0)
                {
                    Message.predefinedMessages = LoadPredefinedMessages();
                }
                return Message.predefinedMessages;
            }
            set { Message.predefinedMessages = value; }
        }
        #endregion

        #region sfg message data
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string SfgCode;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string SfgName;

        /// <summary>
        /// This is provided at runtime, it is not included in the pre-defined error
        /// </summary>
        [XmlElement]
        public List<string> SfgMessages = new List<string>();
        #endregion

        #region ah message data
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public short AhCode;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string AhName;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string AhMessage;
        #endregion

        public string MethodName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public MessageSources MessageSource;

        #region constructors
        public Message() { }
        //public Message(string ahMessageName, string text)
        //{
        //    AhName = ahMessageName;
        //    MessageSource = MessageSources.AndrewHarper;

        //    // get the error from alldefinederrors
        //    Message predefinedMessage = PredefinedMessages.Find(delegate(Message er) { return er.AhName == ahMessageName; });
        //    if (predefinedMessage != null)
        //    {
        //        SfgCode = predefinedMessage.SfgCode;
        //        SfgName = predefinedMessage.SfgName;
        //        AhCode = predefinedMessage.AhCode;
        //        AhMessage = predefinedMessage.AhMessage + " [" + text + "]";
        //    }
        //}

        /// <summary>
        /// Used for AndrewHarper messages ONLY.
        /// </summary>
        /// <param name="ahMessageName">AndrewHarper specific message name as defined on messaging XML document.</param>
        public Message(string ahMessageName)
        {
            AhName = ahMessageName;
            MessageSource = MessageSources.AndrewHarper;

            // get the error from alldefinederrors
            Message predefinedMessage = PredefinedMessages.Find(delegate(Message er) { return er.AhName == ahMessageName; });
            if (predefinedMessage != null)
            {
                SfgCode = predefinedMessage.SfgCode;
                SfgName = predefinedMessage.SfgName;
                AhCode = predefinedMessage.AhCode;
                AhMessage = predefinedMessage.AhMessage;
            }
            else
            {
                SfgName = "";
                AhCode = 0;
                AhName = "";
                AhMessage = "Undefined message.";
            }
        }

        /// <summary>
        /// Ad-hoc message
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ahcode"></param>
        /// <param name="ahname"></param>
        /// <param name="ahmessage"></param>
        /// <param name="sfgcode"></param>
        /// <param name="sfgmessages"></param>
        public Message(MessageSources source, short ahcode, string ahname, string ahmessage, string sfgcode, string sfgname, List<string> sfgmessages )
        {
            MessageSource = source;

            AhCode = ahcode;
            AhName = ahname;
            AhMessage = ahmessage;

            SfgCode = sfgcode;
            SfgName = sfgname;
            SfgMessages = sfgmessages;
        }

        /// <summary>
        /// Used for all SFG messages.
        /// </summary>
        /// <param name="sfgCode">SFG generated message code.  Ignored if messageSource is AndrewHarper</param>
        /// <param name="messageSource">Enumeration of available message sources.</param>
        public Message(string sfgCode, MessageSources messageSource)
        {
            //error numbers < 100 are common messages
            //error numbers >= 100 are service specific messages 
            //error numbers > 9000 are Andrew Harper specific
            try
            {
                SfgCode = sfgCode;

                if (Convert.ToInt16(sfgCode) < 100)
                    messageSource = MessageSources.SfgCommon;
                else
                    MessageSource = messageSource;

                List<Message> predefinedMessageList = new List<Message>();
                Message predefinedMessage = new Message();
                if (predefinedMessage != null)
                {
                    predefinedMessageList = PredefinedMessages.FindAll(delegate(Message msg) { return msg.SfgCode == sfgCode; });
                    predefinedMessage = predefinedMessageList.Find(delegate(Message msg) { return msg.MessageSource == messageSource; });
                    if (predefinedMessage != null)
                    {
                        SfgName = predefinedMessage.SfgName;
                        AhCode = predefinedMessage.AhCode;
                        AhName = predefinedMessage.AhName;
                        AhMessage = predefinedMessage.AhMessage;
                        if (MessageSource == MessageSources.AndrewHarper)
                            SfgCode = "";
                    }
                    else
                    {
                        SfgName = "";
                        AhCode = 0;
                        AhName = "";
                        AhMessage = "Undefined message.";
                    }
                }
            }
            catch
            {
                SfgName = "";
                AhCode = 0;
                AhName = "";
                AhMessage = "Undefined message.";
            }
        }
        #endregion

        /// <summary>
        /// Loads the messaging XML file into a generic list of messages.  This is accessed from the Message 
        /// class constructor to translate messages from SFG messages description to AH specific error messages.  
        /// </summary>
        /// <returns>List of Message objects.</returns>
        private static List<Message> LoadPredefinedMessages()
        {
            List<Message> rtMessageList = new List<Message>();
            MessageSources messageSource = MessageSources.Unspecified;
            String SfgCode = String.Empty;
            String SfgName = String.Empty;
            String AhCode = String.Empty;
            String AhName = String.Empty;
            String AhMessage = String.Empty;

            XmlTextReader rdr = new XmlTextReader(ConfigurationManager.AppSettings["messagelist-filename"]);
            string currentService = string.Empty;

            while (rdr.Read())
            {
                if (rdr.NodeType == XmlNodeType.Element)
                {
                    switch (rdr.Name)
                    {
                        case "service":
                            {
                                while (rdr.MoveToNextAttribute())
                                    switch (rdr.Value)
                                    {
                                        case "ahmessages":
                                            messageSource = MessageSources.AndrewHarper;
                                            break;
                                        case "common":
                                            messageSource = MessageSources.SfgCommon;
                                            break;
                                        case "ccproc":
                                            messageSource = MessageSources.CreditCard;
                                            break;
                                        case "customerupdate":
                                            messageSource = MessageSources.CustomerUpdate;
                                            break;
                                        case "gatekeeper":
                                            messageSource = MessageSources.Gatekeeper;
                                            break;
                                        case "suborder":
                                            messageSource = MessageSources.SubOrderInsert;
                                            break;
                                        case "usermaintenance":
                                            messageSource = MessageSources.UserMaint;
                                            break;
                                        default:
                                            break;
                                    }
                                break;
                            }
                        case "message":
                            {
                                while (rdr.MoveToNextAttribute())
                                    switch (rdr.Name)
                                    {
                                        case "sfgcode":
                                            SfgCode = rdr.GetAttribute(rdr.Name);
                                            break;
                                        case "sfgname":
                                            SfgName = rdr.GetAttribute(rdr.Name);
                                            break;
                                        case "sfgmessage":
                                            break;
                                        case "ahcode":
                                            AhCode = rdr.GetAttribute(rdr.Name);
                                            break;
                                        case "ahname":
                                            AhName = rdr.GetAttribute(rdr.Name);
                                            break;
                                        case "ahmessage":
                                            AhMessage = rdr.GetAttribute(rdr.Name);
                                            break;
                                        default:
                                            break;
                                    }
                                Message msg = new Message();
                                msg.MessageSource = messageSource;
                                msg.SfgCode = SfgCode;
                                msg.SfgName = SfgName;
                                msg.AhCode = Convert.ToInt16(AhCode);
                                msg.AhName = AhName;
                                msg.AhMessage = AhMessage;
                                rtMessageList.Add(msg);
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
            rdr.Close();
            return rtMessageList;
        }

        public override string ToString()
        {
            System.Text.StringBuilder value = new System.Text.StringBuilder();
            value.AppendLine(string.Format("MESSAGE SOURCE: {0}", MessageSource.ToString()));
            value.AppendLine(string.Format("AH CODE: {0}\tAH NAME: {1}\tAH MESSAGE: {2}", new object[]{AhCode, AhName, AhMessage}));
            value.AppendLine(string.Format("SFG CODE: {0}\tSFG NAME: {1}", new object[]{SfgCode, SfgName}));
            foreach (string message in SfgMessages)
            {
                value.AppendLine(string.Format("SFG MESSAGE: {0}", message));
            }
            return value.ToString();
        }
    }
}

/// <summary>
/// Supported messaging sources.
/// </summary>
public enum MessageSources { AndrewHarper, SfgCommon, Unspecified, CreditCard, CustomerUpdate, Gatekeeper, SubOrderInsert, UserMaint, Heartbeat }