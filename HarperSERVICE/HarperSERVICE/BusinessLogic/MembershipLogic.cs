using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFGWrapper;
using SupportClasses;
using HarperLINQ;
using System.Configuration;
using HarperCRYPTO;
using HarperACL;

namespace BusinessLogic
{
    public partial class MembershipLogic
    {
        //TODO: discuss with sfg, are they checking their db for user/pass first? what if new user or pwd changed?
        BaseResponse baseResponse = new BaseResponse();
        string methodName = string.Empty;
        private void LogMethodError(string methodName, Exception exceptionCaught) { EventLogger.LogError("BusinessLogic.MembershipLogic.{0}()", string.Format("Message: {0} \r\nStackTrace: {1}", exceptionCaught.Message, exceptionCaught.StackTrace)); }
        
        public BaseResponse UpdateUsername(string newUserName, string oldUserName, string password, bool debug)
        {
            methodName = "UpdateUsername";
            
            try
            {
                #region validate input
                // All params are required
                if ((newUserName.Trim() == "") || (oldUserName.Trim() == "") || (password.Trim() == ""))
                {
                    baseResponse.Messages.Add(new Message("ImproperValidationCriteriaException"));
                    return baseResponse;
                }
                #endregion

                UpdateUsernameRequest request = new UpdateUsernameRequest(newUserName, oldUserName, password, debug);
                baseResponse = UserMaintenance.UpdateUsername(request);
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
            }
            
            return baseResponse;
        }
        public BaseResponse UpdatePassword(string userName, string newPassword, bool debug)
        {
            methodName = "UpdatePassword";
            
            try
            {
                #region validate input
                // All params are required.
                if ((userName.Trim() == "") || (newPassword.Trim() == ""))
                {
                    baseResponse.Messages.Add(new Message("ImproperValidationCriteriaException"));
                    return baseResponse;
                }
                #endregion

                UpdatePasswordRequest request = new UpdatePasswordRequest(userName, newPassword, debug);
                baseResponse = UserMaintenance.UpdatePassword(request);
                if (baseResponse != null
                    && baseResponse.TypedResponse != null
                    && baseResponse.TypedResponse.GetType().Name == "UpdatePasswordResponse"
                    && (baseResponse.TypedResponse as UpdatePasswordResponse).Success)
                {
                    HarperACL.Authenticator.UpdateAHPassword(userName, newPassword);
                }
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
            }
            
            return baseResponse;
        }        
        public BaseResponse GetMemberById(string memberId, bool issfgid)
        {
            methodName = "GetMemberByMemberId";
            
            if (!issfgid)
            {
                try
                {
                    using (SupportDataDataContext context = new SupportDataDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                    {
                        memberId = (from a in context.SFG_CustomerNumbers
                                    where a.cusID.ToString() == memberId
                                    select a.SFGCustNum).Single();
                    }
                }
                catch
                {
                    baseResponse.Messages.Add(new Message(MessageSources.AndrewHarper, 0, "ImproperValidationCriteriaException", "Unable to get SFG id for cusid", "", "", null));
                    baseResponse.TypedResponse = new GetMemberResponse();
                    baseResponse.TypedResponse.Success = false;
                    return baseResponse;
                }
            }            
            try
            {
                
                #region validate input
                // All params are required 
                if (memberId.Trim() == "")
                {
                    baseResponse.Messages.Add(new Message("ImproperValidationCriteriaException"));
                    baseResponse.TypedResponse = new GetMemberResponse();
                    baseResponse.TypedResponse.Success = false;
                    return baseResponse;
                }
                #endregion

                GetMemberByMemberIdRequest request = new GetMemberByMemberIdRequest(memberId.Trim(), false);
                baseResponse = Gatekeeper.GetMemberByMemberId(request);
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
            }
            
            return baseResponse;
        }
        public BaseResponse GetMemberByUserName(string userName)
        {
            methodName = "GetMemberByUserName";
            
            try
            {
                #region validate input
                // All params are required 
                if (userName.Trim() == "")
                {
                    baseResponse.Messages.Add(new Message("ImproperValidationCriteriaException"));
                    return baseResponse;
                }
                #endregion

                GetMemberByUserNameRequest request = new GetMemberByUserNameRequest(userName.Trim(), false);
                baseResponse = Gatekeeper.GetMemberByUserName(request);
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
            }
            
            return baseResponse;
        }   
        public BaseResponse CreateLogin(string memberId, string userName, string initialPassword, string postalCode)
        {
            methodName = "CreateLogin";
            
            List<Message> errors = new List<Message>();
            string errortext = string.Empty;
            try
            {
                #region validate input
                // All params are required 
                if ((memberId.Trim() == "") 
                    || (userName.Trim() == "") 
                    || (initialPassword.Trim() == "") 
                    || (postalCode.Trim() == ""))
                {
                    errors.Add(new Message("ImproperValidationCriteriaException"));
                }
                using (HarperLINQ.AHT_MainDataContext context = new AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                {
                    string exception = string.Empty;
                    bool exists = false;    
                    int existing = (from a in context.tbl_Customers
                                    join b in context.SFG_CustomerNumbers
                                    on a.cusID equals b.cusID
                                    where a.cusUserName == userName.Trim()
                                    && b.SFGCustNum != memberId.Trim()
                                    select a).Count();
                    if (existing > 0)
                    {
                        exists = true;
                        exception = "User name is in use.";
                    }
                    existing = 0;
                    existing = (from a in context.tbl_Customers
                                join b in context.SFG_CustomerNumbers
                                on a.cusID equals b.cusID
                                where a.cusDisplayName == userName.Trim()
                                && b.SFGCustNum != memberId.Trim()
                                select a).Count();
                    if (existing > 0)
                    {
                        exists = true;
                        exception += "  Screen name is in use.";
                    }
                    existing = 0;
                    existing = (from a in context.tbl_Customers
                                join b in context.SFG_CustomerNumbers
                                on a.cusID equals b.cusID
                                where a.cusEmail == userName.Trim()
                                && b.SFGCustNum != memberId.Trim()
                                select a).Count();
                    if (existing > 0)
                    {
                        exists = true;
                        exception += "Email address is in use.";
                    }
                    if (exists)
                    {
                        throw new Exception(exception);
                    }
                }
                #endregion

                CreateLoginRequest request = new CreateLoginRequest(memberId.Trim(), userName.Trim(), initialPassword.Trim(), postalCode.Trim(), false);
                baseResponse = UserMaintenance.CreateLogin(request);
                if (baseResponse != null
                    && baseResponse.TypedResponse != null
                    && baseResponse.TypedResponse.GetType().Name == "CreateLoginResponse"
                    && (baseResponse.TypedResponse as CreateLoginResponse).Success)
                {
                    HarperACL.Authenticator auth = new Authenticator(userName.Trim(), Cryptography.Hash(initialPassword.Trim(),true), true, true, false, memberId.Trim());
                    if (!auth.CreateUsername(userName.Trim(), initialPassword.Trim(), memberId.Trim()))
                    {
                        throw new Exception("User created at SFG, could not create user at AH");
                    }
                }
            }
            catch (Exception ex)
            {
                errortext = string.Format("Error creating login. Memberid: {0} Username: {1} Password: {2} PostalCode: {3} \r\nException message: {4}\r\nException stacktrace: {5}", new object[] { memberId, userName, initialPassword, postalCode, ex.Message, ex.StackTrace });
                errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateLoginException", errortext, "", "", null));
            }
            foreach (Message error in errors)
            {
                if (baseResponse == null)
                {
                    baseResponse = new BaseResponse();
                }
                baseResponse.Messages.Add(error);

                StringBuilder error_text = new StringBuilder();
                error_text.AppendLine("CREATELOGIN ERROR LOGGED");
                error_text.AppendLine(string.Format("MEMBERID {0}", memberId));
                baseResponse.Messages.Add(error);
                error_text.AppendLine(string.Format("ERROR: {0}", new object[] { error.AhMessage}));
                EventLogger.LogError("MembershipLogic.CreateLogin", error_text.ToString(), true);                
            }
            
            return baseResponse;
        }
        
        public BaseResponse UpdateMember(string memberid,
            string salutation, string firstname, string lastname, string suffix,
            string professionaltitle, string email, bool optin, string businessname, string address1, string address2, string address3,
            string city, string state, string postalcode, string country,
            string phone, string screenname,
            bool debug)
        {
            methodName = "UpdateMember";
            
            try
            {
                #region  set member data
                Member memberData = new Member();
                memberData.MemberId = memberid;
                memberData.Salutation = salutation;
                memberData.FirstName = firstname;
                memberData.LastName = lastname;
                memberData.Suffix = suffix;
                memberData.ProfessionalTitle = professionaltitle;
                memberData.OptIn = optin;
                memberData.Email = email;

                memberData.Address = new Address();
                memberData.Address.BusinessName = businessname;
                memberData.Address.Address1 = address1;
                memberData.Address.Address2 = address2;
                memberData.Address.Address3 = address3;
                memberData.Address.City = city;
                memberData.Address.State = state;
                memberData.Address.PostalCode = postalcode;
                memberData.Address.Country = country;
                memberData.Address.Phone = phone;
                #endregion

                //TODO :  Error handling for dupe email or screenname

                #region update ah db here
                using (HarperACL.ACLDataDataContext context = new ACLDataDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                {

                    HarperACL.Customer customer = (from a in context.Customers
                                                   join b in context.SFG_CustomerNumbers
                                                   on a.cusID equals b.cusID
                                                   where b.SFGCustNum == memberid
                                                   select a).Single();

                    int existing = (from a in context.Customers
                                    where
                                    (a.cusEmail == email || a.cusDisplayName == screenname)
                                    && a.cusID != customer.cusID
                                    select a).Count();
                    if (existing > 0)
                    {
                        throw new Exception("Unable to update member data, screen name or email already in use.");
                    }
                    customer.cusPrefix = salutation;
                    customer.cusFirstName = firstname;
                    customer.cusLastName = lastname;
                    customer.cusSuffix = suffix;
                    customer.cusTitle = professionaltitle;
                    customer.cusEmail = email;
                    customer.cusCompany = businessname;
                    customer.cusPhone1 = phone;
                    customer.cusDateUpdated = DateTime.Now;
                    customer.cusDisplayName = screenname;

                    HarperACL.AddressCustomer address = (from a in context.AddressCustomers
                                                         where a.addID == customer.addID
                                                         select a).Single();
                    address.addAddress1 = address1;
                    address.addAddress2 = address2;
                    address.addAddress3 = address3;
                    address.addCity = city;
                    address.addCountry = country;
                    address.addDateUpdated = DateTime.Now;
                    address.addPostalCode = postalcode;
                    address.addRegion = state;

                    context.SubmitChanges();
                }
                #endregion

                //update at SFG
                UpdateMemberRequest request = new UpdateMemberRequest(memberData, debug);
                baseResponse = CustomerUpdate.UpdateMember(request);
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
            }
            
            return baseResponse;
        }
 

        #region refer a friend
        public BaseResponse CreateReferral(string cusid, string membername, string memberemail, string keycode,
             string pubcode, string friendname, string friendemailaddress, bool ccmember)
        {
            methodName = "CreateReferral";
            
            List<Message> errors = new List<Message>();
            Referral referral = new Referral();
            try
            {
                tbl_Customer member = new tbl_Customer(int.Parse(cusid), false);

                #region validate input
                if (member == null)
                {
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralInputValidationException", BusinessLogicStrings.memberDoesNotExistError, cusid, "", null));
                }
                if (member.SfgId == null)
                {
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralInputValidationException", BusinessLogicStrings.invalidMemberIdError, "", "", null));
                }
                if (string.IsNullOrEmpty(membername))
                {
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralInputValidationException", BusinessLogicStrings.missingMemberNameError, "", "", null));
                }
                if (ccmember && string.IsNullOrEmpty(memberemail))
                {
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralInputValidationException", BusinessLogicStrings.missingMemberEmailError, "", "", null));
                }
                if (string.IsNullOrEmpty(keycode))
                {
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralInputValidationException", BusinessLogicStrings.missingKeycodeError, "", "", null));
                }
                if (string.IsNullOrEmpty(pubcode))
                {
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralInputValidationException", BusinessLogicStrings.missingPubcodeError, "", "", null));
                }
                if (string.IsNullOrEmpty(friendname))
                {
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralInputValidationException", BusinessLogicStrings.missingFriendNameError, "", "", null));
                }
                if (string.IsNullOrEmpty(friendemailaddress))
                {
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralInputValidationException", BusinessLogicStrings.missingFriendEmailError, "", "", null));
                }
                #endregion

                #region enforce business rules
                tbl_Customer friend = new tbl_Customer(friendemailaddress, false);
                try
                {
                    Referral existing_referral = new Referral(friendemailaddress);

                    if (memberemail == friendemailaddress)
                    {
                        errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralBusinessRuleException", BusinessLogicStrings.cannotReferSelfError, "", "", null));
                    }
                    else if (friend.cusID > 0)
                    {
                        errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralBusinessRuleException", BusinessLogicStrings.existingMemberError, "", "", null));
                    }
                    else if (existing_referral.dateredeemed == null)
                    {
                        if (existing_referral.id > 0 && existing_referral.dateexpires.CompareTo(DateTime.Now) >= 0)
                        {
                            errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralBusinessRuleException", BusinessLogicStrings.existingReferralError, "", "", null));
                        }
                    }
                    if (errors.Count <= 0)
                    {
                        GetMemberResponse checkFriend = (GetMemberByUserName(friendemailaddress).TypedResponse as GetMemberResponse);
                        if (checkFriend != null && (checkFriend.MemberFound || checkFriend.WebAccountFound))
                        {
                            errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralBusinessRuleException", BusinessLogicStrings.freindEmailInUseSFGError, "", "", null));
                        }
                    }
                }
                catch (HarperLINQ.DataLoadException dle)
                {
                    errors.Add(new Message(MessageSources.AndrewHarper, 0, "CreateReferralBusinessRuleException", BusinessLogicStrings.freindEmailInUseAHError, "", "", null));
                }
                if (errors.Count() > 0)
                {
                    string errstring = string.Empty;
                    foreach (Message msg in errors)
                    {
                        string sfgmessages = string.Empty;
                        if (msg.SfgMessages != null)
                        {
                            foreach (string sfgmsg in msg.SfgMessages)
                            {
                                sfgmessages += string.Format("SFGMessage: {0}", sfgmsg);
                            }
                        }
                        errstring += string.Format("AhMessage: {0}|| {1}", new object[] { msg.AhMessage, sfgmessages });
                    }
                    throw new Exception(string.Format("Error creating referral: [{0}]", errstring));
                }
                #endregion

                ReferralOffer offer = new ReferralOffer(keycode, pubcode);
                

                #region save referral
                referral = new Referral(int.Parse(cusid), membername, memberemail, keycode, pubcode, friendname, friendemailaddress, ccmember, offer.triallengthinmonths, offer.offerexpiresmonths);
                referral.Save();
                #endregion

                #region send email
                //create mailer and sent mail
                Mailer mailer = new Mailer();

                string ccEmail = memberemail; 

                if (!ccmember)
                {
                    ccEmail = string.Empty;
                }
                
                mailer.SendEmail(ConfigurationManager.AppSettings["mailserviceuser"], 
                    ConfigurationManager.AppSettings["mailservicepwd"],
                    string.Format("Membership Invitation from {0}", membername), 
                    ConfigurationManager.AppSettings["referemailfrom"],
                    friendemailaddress,
                    ccEmail, 
                    string.Empty, 
                    referral.GetReferralEmailBody(), 
                    true, 
                    ConfigurationManager.AppSettings["smtpserver"]);
                #endregion

            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
            }
            if (baseResponse != null && baseResponse.Messages != null)
            {
                foreach (Message error in errors)
                {
                    baseResponse.Messages.Add(error);
                }
            }
            if (baseResponse.Messages.Count() <= 0 && referral != null && referral.id >= 0)
            {
                #region create typed response
                baseResponse.TypedResponse = new ReferralResponse();
                baseResponse.TypedResponse.Success = true;
                (baseResponse.TypedResponse as ReferralResponse).referralid = referral.id;
                #endregion
            }
            else
            {
                baseResponse.TypedResponse = new ReferralResponse();
                baseResponse.TypedResponse.Success = false;
            }
            
            return baseResponse;
        }

        #endregion 

        /*
         * public BaseResponse RequestRate(string firstname, string lastname,
            string hashedmemberid, string hotelname, string location,
            string requestedroom, string checkin, string checkout,
            string altcheckin, string altcheckout, string requesting,
            string phone, string email, string comments)
        {
            
            string emailTo = System.Configuration.ConfigurationManager.AppSettings["RateRequestEmailTo"].ToString();
            string emailFrom = txtEmailAddress.Text.Trim().ToString();
            string emailServer = System.Configuration.ConfigurationManager.AppSettings["EmailServer"].ToString();

            MailMessage messMessage = null;
            SmtpClient smtpclient = null;

            string emailBody = @"<html><body><table>"
        + "<tr><td style='height: 70px' align='left'>Member Number: " + lblMemberNumber.Text.ToString() + "</td></tr>"
        + "<tr><td style='height: 70px' align='left'>" + txtFirstName.Text.ToString() + " " + txtLastName.Text.ToString()
        + " requests a rate. Please find the details as followings:</td></tr>"
        + "<tr><td>Hotel Name: " + txtHotelName.Text.ToString() + "</td></tr>"
        + "<tr><td>Location: " + txtLocation.Text.ToString() + "</td></tr>"
        + "<tr><td>Requested Room: " + txtReqRoom.Text.ToString() + "</td></tr>"
        + "<tr><td>Preferred Check In: " + txtCheckIn.Text.ToString() + "</td></tr>"
        + "<tr><td>Preferred Check Out: " + txtCheckOut.Text.ToString() + "</td></tr>"
        + "<tr><td>Alternate Check In: " + txtAltCheckIn.Text.ToString() + "</td></tr>"
        + "<tr><td>Alternate Check Out: " + txtAltCheckOut.Text.ToString() + "</td></tr>"
        + "<tr><td>Provide: " + ddlProvide.SelectedValue.ToString() + "</td></tr>"
        + "<tr><td>Phone: " + txtPhone.Text.ToString() + "</td></tr>"
        + "<tr><td>Email: " + txtEmailAddress.Text.ToString() + "</td></tr>"
        + "<tr><td>Comments: " + txtComments.Text.ToString() + "</td></tr>"
        + "</table></body></html>";

            messMessage = new MailMessage();
            messMessage.From = new MailAddress(emailFrom);
            messMessage.To.Add(new MailAddress(emailTo));
            messMessage.Subject = "New Rate Request";
            messMessage.IsBodyHtml = true;
            smtpclient = new SmtpClient(emailServer);

            AlternateView av1 = AlternateView.CreateAlternateViewFromString(emailBody, null, MediaTypeNames.Text.Html);
            messMessage.AlternateViews.Add(av1);

            smtpclient.Send(messMessage);
            
            return baseResponse;
        }*/

    }
}
