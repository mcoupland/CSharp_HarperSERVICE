using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Configuration;

namespace MemberServices
{
    /// <summary>
    /// Summary description for NewsletterService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class NewsletterService : System.Web.Services.WebService
    {
        [WebMethod]
        public NewsletterResponseObject GetSubscriber(string enccusId, ushort? position)
        {

            NewsletterResponseObject response = new NewsletterResponseObject();
            HarperLINQ.tbl_Customer customer;
            HarperLINQ.AHT_MainDataContext dc;
            int cusId = 0;

            try
            {
                string strCusId = HarperCRYPTO.Cryptography.DeHash(enccusId, true);
                cusId = Int32.Parse(strCusId);
                response.PrimarySubscriber = null;
                response.SecondarySubscriber = null;

                dc = new HarperLINQ.AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString);
                customer = dc.tbl_Customers.Where(c => c.cusID == cusId)
                                           .Single();

                if (position.HasValue && position.Value != 1 && position.Value != 2)
                    throw new ApplicationException("Position Value is invalid. Use 1 for Primary, 2 for Secondary.");

                if (!position.HasValue || position.Value == 1)
                {
                    if (customer.cusStormPostPrimaryID.HasValue && customer.cusStormPostPrimaryID.Value > 0 )
                    {
                        response.PrimarySubscriber = retrieveSubscriber(customer.cusStormPostPrimaryID.Value);
                        response.PrimarySubscriber.Primary_Email = true;
                        response.PrimarySubscriber.CusID = cusId;
                    }
                }

                if (!position.HasValue || position.Value == 2)
                {
                    if (customer.cusStormPostSecondaryID.HasValue && customer.cusStormPostSecondaryID.Value > 0)
                    {
                        response.SecondarySubscriber = retrieveSubscriber(customer.cusStormPostSecondaryID.Value);
                        response.SecondarySubscriber.CusID = cusId;
                    }

                }



                response.Success = true;
                return response;
            }
            catch(Exception ex)
            {
                string argString = String.Format("enccusId: {0}; position: {1}", enccusId, position);
                new SupportClasses.Mailer().SendEmail(ConfigurationManager.AppSettings["mailserviceuser"],
                    ConfigurationManager.AppSettings["mailservicepwd"],
                    "NewsletterService ERROR - " + ex.GetType().Name,
                    "noreply@andrewharper.com",
                    ConfigurationManager.AppSettings["newsletter-erroremailto"],
                    "", "", argString + ex.ToString(), false,
                    ConfigurationManager.AppSettings["erroremailsmtpserver"]);
                throw;
            }
            
        }

        /// <summary>
        /// Updates the subscription status for a subscriber on a collection of newsletters. 
        /// </summary>
        /// <param name="subscriber">The Subscriber we wish to update</param>
        /// <param name="listIds">The StormPost ListIds we will be updating.</param>
        /// <param name="subscribe">The new subscription status; true = subscribe, false = unsubscribe</param>
        /// <returns></returns>
        [WebMethod]
        public bool UpdateSubscription(NewsletterSubscriber subscriber, int[] listIds, bool subscribe)
        {
            stormpost.api.SoapRequestProcessorService api;
            int listId = 0;

            try
            {
                api = new MemberServices.stormpost.api.SoapRequestProcessorService();
                api.authenticationValue = new MemberServices.stormpost.api.authentication();
                api.authenticationValue.username = ConfigurationManager.AppSettings["newsletter-api-username"];
                api.authenticationValue.password = ConfigurationManager.AppSettings["newsletter-api-password"];

                for(int i = 0; i < listIds.Length; i++)
                {
                    listId = listIds[i];
                    if (subscribe)
                        api.subscribeToList(listId, subscriber.UniqueID, true, "", 0);
                    else
                        api.unsubscribeFromList(listId, subscriber.UniqueID, 0);
                }

            }
            catch(Exception ex)
            {
                string argString = String.Format("cusID: {0}; uniqueId: {1}; listId {2}; subscribe: {3}\r\n\r\n", subscriber.CusID, subscriber.UniqueID, listId.ToString(), subscribe.ToString());
                new SupportClasses.Mailer().SendEmail(ConfigurationManager.AppSettings["mailserviceuser"],
                    ConfigurationManager.AppSettings["mailservicepwd"],
                    "NewsletterService ERROR - " + ex.GetType().Name,
                    "noreply@andrewharper.com",
                    ConfigurationManager.AppSettings["newsletter-erroremailto"],
                    "", "", argString + ex.ToString(), false,
                    ConfigurationManager.AppSettings["erroremailsmtpserver"]);
                throw;
            }

            return true;
        }

        /// <summary>
        /// Create a new Subscriber record based on the existing tbl_Customer record. If the customer already has
        /// a StormPost recipient set up, the function will throw an exception.
        /// </summary>
        /// <param name="encCusId">Encrypted cusId for the customer.</param>
        /// <param name="position">Email address position; 1 = primary email, 2 = secondary email</param>
        /// <returns></returns>
        [WebMethod]
        public NewsletterResponseObject CreateSubscriber(string encCusId, ushort position)
        {
            stormpost.api.SoapRequestProcessorService api;
            stormpost.api.Recipient recipient, existing;
            HarperLINQ.AHT_MainDataContext dc;
            HarperLINQ.tbl_Customer customer;
            HarperLINQ.tbl_NetMembership maxMembership;
            HarperLINQ.tbl_MembershipType maxMembershipType;
            HarperLINQ.tbl_StormPostRecipient newRecipient;
            NewsletterResponseObject response;
            int cusID = 0;

            try
            {
                cusID = Int32.Parse(HarperCRYPTO.Cryptography.DeHash(encCusId, true));
                api = new MemberServices.stormpost.api.SoapRequestProcessorService();
                dc = new HarperLINQ.AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString);
                response = new NewsletterResponseObject();
                response.Success = false;
                response.PrimarySubscriber = new NewsletterSubscriber();

                // Set the authentication on the SOAP object
                api.authenticationValue = new MemberServices.stormpost.api.authentication();
                api.authenticationValue.username = ConfigurationManager.AppSettings["newsletter-api-username"];
                api.authenticationValue.password = ConfigurationManager.AppSettings["newsletter-api-password"];


                // Get the customer and his associated NetMembership record
                customer = dc.tbl_Customers.Where(c => c.cusID == cusID)
                                           .Single();
                try
                {
                    maxMembership = dc.tbl_NetMemberships.Where(nm => nm.cusID == customer.cusID)
                                                         .OrderByDescending(nm => nm.nmbDateEnd)
                                                         .First();
                }
                catch
                {
                    maxMembership = null;
                }

                try
                {
                    maxMembershipType = dc.tbl_MembershipTypes.Where(mt => mt.mtyCode == maxMembership.mtyCode)
                                          .Single();
                }
                catch
                {
                    maxMembershipType = null;
                }


                // "Lazy" coding: look for an excuse not to do any work...
                if (position != 1 && position != 2)
                    throw new ApplicationException("Postion specified is unsupported. Currently this operation only selects position 1 or 2.");
                else if (position == 1 && customer.cusStormPostPrimaryID.HasValue && customer.cusStormPostPrimaryID > 0)
                    throw new ApplicationException(String.Format("The customer selected already has a StormPulse account ({0})", customer.cusStormPostPrimaryID));
                else if (position == 2 && customer.cusStormPostSecondaryID.HasValue && customer.cusStormPostSecondaryID > 0)
                    throw new ApplicationException(String.Format("The customer selected already has a StormPulse account ({0})", customer.cusStormPostSecondaryID));
                

                // Set up the recipient object
                recipient = new MemberServices.stormpost.api.Recipient();
                recipient.dateJoined = DateTime.Now;
                recipient.externalID = customer.cusCustNum;
                recipient.SetDemographic("FirstName", customer.cusFirstName);
                recipient.SetDemographic("LastName", customer.cusLastName);
                recipient.SetDemographic("_Custom01", (maxMembershipType != null) ? maxMembershipType.mtyName : "");
                recipient.SetDemographic("_Custom02", customer.csoCode);
                recipient.SetDemographic("_Custom03", (maxMembership != null)  ? maxMembership.nmbDateEnd.ToShortDateString() : "");
                recipient.SetDemographic("_Custom04", position == 1 ? "True" : "False");
                if (position == 1)
                    recipient.address = customer.cusEmail;
                else if (position == 2)
                    recipient.address = customer.cusSecondEmail;


                // Check whether the recipient already exists at stormpost
                existing = null;
                try
                {
                    existing = api.getRecipientByAddress(recipient.address);
                }
                catch { }
                if (existing != null)
                {
                    if (!String.IsNullOrEmpty(existing.address) && existing.recipID.HasValue)
                    {
                        return updateCustomerStormPostId(cusID, position, existing.recipID.Value);
                    }
                }

                // Make the createRecipient call, and update Nucleus with the new value
                response.PrimarySubscriber.UniqueID = api.createRecipientAndReturnRecipID(recipient);
                if (position == 1)
                    customer.cusStormPostPrimaryID = response.PrimarySubscriber.UniqueID;
                else if (position == 2)
                    customer.cusStormPostSecondaryID = response.PrimarySubscriber.UniqueID;

                // Create the StormPostRecipient object
                DateTime tmpDateTime = new DateTime();
                newRecipient = new HarperLINQ.tbl_StormPostRecipient()
                {
                    sprEmail = recipient.address,
                    sprFirstName = recipient.GetDemographics()["FirstName"],
                    sprLastName = recipient.GetDemographics()["LastName"],
                    sprMemberType = recipient.GetDemographics()["_Custom01"],
                    sprMemberSource = recipient.GetDemographics()["_Custom02"],
                    sprExpireDate = (DateTime.TryParse(recipient.GetDemographics()["_Custom03"], out tmpDateTime) ? (DateTime?)tmpDateTime : (DateTime?)null),
                    sprPrimaryEmail = recipient.GetDemographics()["_Custom04"] == "True",
                    sprRecipId = response.PrimarySubscriber.UniqueID,
                    timestmp = DateTime.Now,
                    sprJoinDate = DateTime.Now
                };
                dc.tbl_StormPostRecipients.InsertOnSubmit(newRecipient);
                dc.SubmitChanges();


                // Construct the response object
                response.PrimarySubscriber.CusID = cusID;
                response.PrimarySubscriber.EmailAddress = customer.cusEmail;
                response.PrimarySubscriber.Subscriptions = new List<Newsletter>();
                response.PrimarySubscriber.First_Name = recipient.GetDemographics()["FirstName"];
                response.PrimarySubscriber.Last_Name = recipient.GetDemographics()["LastName"];
                response.PrimarySubscriber.Member_Source = recipient.GetDemographics()["_Custom02"];
                response.PrimarySubscriber.Member_Type = recipient.GetDemographics()["_Custom01"];
                response.PrimarySubscriber.Primary_Email = (recipient.GetDemographics()["_Custom04"] == "True");
                // Parse the Expire Date
                string temp = recipient.GetDemographics()["_Custom03"];
                DateTime tmpDate = new DateTime();
                response.PrimarySubscriber.Member_Expire_Date = (DateTime.TryParse(temp, out tmpDate)) ? (DateTime?)tmpDate : null;
                response.Success = true;
                return response;

            }
            catch(Exception ex)
            {
                string argString = String.Format("cusId: {0}\r\n\r\n", cusID.ToString());
                new SupportClasses.Mailer().SendEmail(ConfigurationManager.AppSettings["mailserviceuser"],
                    ConfigurationManager.AppSettings["mailservicepwd"],
                    "NewsletterService ERROR - " + ex.GetType().Name,
                    "noreply@andrewharper.com",
                    ConfigurationManager.AppSettings["newsletter-erroremailto"],
                    "", "", argString + ex.ToString(), false,
                    ConfigurationManager.AppSettings["erroremailsmtpserver"]);
                throw;
            }

        }

        [WebMethod]
        public bool UpdateSubscriber(NewsletterSubscriber subscriber)
        {
            stormpost.api.SoapRequestProcessorService api;
            stormpost.api.Recipient recipient;

            try
            {
                api = new MemberServices.stormpost.api.SoapRequestProcessorService();
                api.authenticationValue = new MemberServices.stormpost.api.authentication();
                api.authenticationValue.username = ConfigurationManager.AppSettings["newsletter-api-username"];
                api.authenticationValue.password = ConfigurationManager.AppSettings["newsletter-api-password"];

                recipient = api.getRecipient(subscriber.UniqueID);
                recipient.address = subscriber.EmailAddress;
                recipient.SetDemographic("FirstName", subscriber.First_Name);
                recipient.SetDemographic("LastName", subscriber.Last_Name);
                recipient.SetDemographic("_Custom01", subscriber.Member_Type);
                recipient.SetDemographic("_Custom02", subscriber.Member_Source);
                recipient.SetDemographic("_Custom03", subscriber.Member_Expire_Date.GetValueOrDefault(new DateTime(1900, 1,1)).ToString("yyyy-MM-dd"));
                
                api.updateRecipient(recipient);
                return true;
            }
            catch (Exception ex)
            {
                string argString = String.Format("Unique Id: {0}\r\n\r\n", subscriber.UniqueID.ToString());
                new SupportClasses.Mailer().SendEmail(ConfigurationManager.AppSettings["mailserviceuser"],
                    ConfigurationManager.AppSettings["mailservicepwd"],
                    "NewsletterService ERROR - " + ex.GetType().Name,
                    "noreply@andrewharper.com",
                    ConfigurationManager.AppSettings["newsletter-erroremailto"],
                    "", "", argString + ex.ToString(), false,
                    ConfigurationManager.AppSettings["erroremailsmtpserver"]);
                throw;
            }

        }


        [WebMethod]
        public List<Newsletter> GetAllNewsletters()
        {
            List<Newsletter> output;
            stormpost.api.SoapRequestProcessorService api;
            stormpost.api.List[] lists;

            try
            {
                api = new MemberServices.stormpost.api.SoapRequestProcessorService();
                api.authenticationValue = new MemberServices.stormpost.api.authentication();
                api.authenticationValue.username = ConfigurationManager.AppSettings["newsletter-api-username"];
                api.authenticationValue.password = ConfigurationManager.AppSettings["newsletter-api-password"];
                output = new List<Newsletter>();

                lists = api.getLists(new stormpost.api.List());
                foreach (stormpost.api.List list in lists)
                    output.Add(new Newsletter() { ListId = list.listID.Value, Name = list.listTitle });

                return output;
            }
            catch (Exception ex)
            {
                new SupportClasses.Mailer().SendEmail(ConfigurationManager.AppSettings["mailserviceuser"],
                    ConfigurationManager.AppSettings["mailservicepwd"],
                    "NewsletterService ERROR - " + ex.GetType().Name,
                    "noreply@andrewharper.com",
                    ConfigurationManager.AppSettings["newsletter-erroremailto"],
                    "", "", ex.ToString(), false,
                    ConfigurationManager.AppSettings["erroremailsmtpserver"]);
                throw;
            }

        }


        /// <summary>
        /// Makes a call to the StormPost API to retrieve the Recipient record, by uniqueId
        /// </summary>
        /// <param name="uniqueId">StormPost Unique ID</param>
        /// <returns></returns>
        private NewsletterSubscriber retrieveSubscriber(int uniqueId)
        {
            NewsletterSubscriber output;
            Dictionary<string, string> demographics;
            MemberServices.stormpost.api.Recipient recipient;
            stormpost.api.SoapRequestProcessorService api;
            stormpost.api.ListSubscription[] subscriptions;

            output = new NewsletterSubscriber();
            output.Subscriptions = new List<Newsletter>();

            // Retrieve the StormPost Subscriber info
            api = new MemberServices.stormpost.api.SoapRequestProcessorService();
            api.authenticationValue = new MemberServices.stormpost.api.authentication();
            api.authenticationValue.username = ConfigurationManager.AppSettings["newsletter-api-username"];
            api.authenticationValue.password = ConfigurationManager.AppSettings["newsletter-api-password"];
            recipient = api.getRecipient(uniqueId);


            // Retrieve the Subscription data
            output.UniqueID = recipient.recipID.Value;
            output.EmailAddress = recipient.address;
            subscriptions = api.getRecipientSubscriptions(recipient.recipID.Value);
            subscriptions = subscriptions.Where(s => s.subscribed)
                                         .ToArray();

            // Populate the subscriptions collection on the output object
            foreach (stormpost.api.ListSubscription sub in subscriptions)
            {
                output.Subscriptions.Add(new Newsletter()
                {
                    ListId = sub.listID.Value,
                    Name = sub.listTitle
                });
            }

            // Retrive Demographics; StormPost refers to custom demographic fields by _Custom01, _Custom02, etc.
            demographics = recipient.GetDemographics();
            output.First_Name = demographics.Keys.Contains("FirstName") ? demographics["FirstName"] : "";
            output.Last_Name = demographics.Keys.Contains("LastName") ? demographics["LastName"] : "";
            output.Member_Type = demographics.Keys.Contains("_Custom01") ? demographics["_Custom01"] : "";
            output.Member_Source = demographics.Keys.Contains("_Custom02") ? demographics["_Custom02"] : "";

            // Parse Expire date from the value in _Custom03
            string temp = demographics.Keys.Contains("_Custom03") ? demographics["_Custom03"] : "";
            DateTime tmpDate = new DateTime();
            output.Member_Expire_Date = DateTime.TryParse(temp, out tmpDate) ? (DateTime?)tmpDate : (DateTime?)null;


            return output;


        }

        private NewsletterResponseObject updateCustomerStormPostId(int cusId, ushort position, int stormPostId)
        {
            HarperLINQ.AHT_MainDataContext dc;
            HarperLINQ.tbl_Customer customer;
            string enc_cusId;
            List<HarperLINQ.tbl_Customer> existing1st, existing2nd;
            int existingMembers = 0;

            if(position != 1 && position != 2)
                throw new ArgumentException(String.Format("{0} is not a valid position (Only 1 or 2 accepted).", position.ToString()));

            dc = new HarperLINQ.AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString);
            customer = dc.tbl_Customers.Single(c => c.cusID == cusId);

            // Locate customer records that already have the StormPostId
            existing1st = dc.tbl_Customers.Where(c => c.cusStormPostPrimaryID == stormPostId)
                                          .ToList();
            existing2nd = dc.tbl_Customers.Where(c => c.cusStormPostSecondaryID == stormPostId)
                                          .ToList();

            // Check whether the StormPostId already belongs to an existing *Member*; if so, throw an error
            existingMembers += existing1st.Where(c => 1 == 1
                                                // Check that the member isn't deleted
                                                && !c.cusIsDeleted
                                                // Check that the member has an SFG number
                                                && dc.SFG_CustomerNumbers.Any(sfg => sfg.cusID == c.cusID)
                                                // Check that the member has an active subscription
                                                && dc.tbl_NetMemberships.Any(nm => nm.nmbDateEnd > DateTime.Now && nm.cusID == c.cusID)
                                            ).Count();
            existingMembers += existing2nd.Where(c => 1 == 1
                                                // Check that the member isn't deleted
                                                && !c.cusIsDeleted
                                                // Check that the member has an SFG number
                                                && dc.SFG_CustomerNumbers.Any(sfg => sfg.cusID == c.cusID)
                                                // Check that the member has an active subscription
                                                && dc.tbl_NetMemberships.Any(nm => nm.nmbDateEnd > DateTime.Now && nm.cusID == c.cusID)
                                            ).Count();
            if (existingMembers > 0)
                throw new ApplicationException(String.Format("The requested StormPostId {0} is already assigned to another member. Cannot assign to cusId {1}", stormPostId, cusId));

            // Remove the StormPostId from any existing non-active customers 
            existing1st.ForEach(e1 => e1.cusStormPostPrimaryID = null);
            existing2nd.ForEach(e2 => e2.cusStormPostSecondaryID = null);

            if (position == 1)
                customer.cusStormPostPrimaryID = stormPostId;
            else if (position == 2)
                customer.cusStormPostSecondaryID = stormPostId;


            dc.SubmitChanges();
            enc_cusId = HarperCRYPTO.Cryptography.Hash(cusId.ToString(), true);
            return GetSubscriber(enc_cusId, position);
        }
        
    }
}
