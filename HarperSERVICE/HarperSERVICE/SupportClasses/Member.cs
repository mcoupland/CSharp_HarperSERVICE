using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Configuration;
using HarperLINQ;
using HarperCRYPTO;

//TODO: must update [svcs_CreateUser] on prod database 
namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
	public class Member
    {
        #region AH Data
        private string _CusId = string.Empty;
        [XmlElement]
        public string CusId
        {
            get { return _CusId; }
            set { _CusId = value; }
        }
        private string _CusCustNum = string.Empty;
        [XmlElement]
        public string CusCustNum
        {
            get { return _CusCustNum; }
            set { _CusCustNum = value; }
        }
        private string _ScreenName = string.Empty;
        [XmlElement]
        public string ScreenName
        {
            get { return _ScreenName; }
            set { _ScreenName = value; }
        }
        #endregion

        #region other data
        private string _UserName = string.Empty;
        [XmlElement]
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (value != null && value.Length > 80)
                {
                    throw new InvalidInputException("UserName must be 80 characters or less.");
                }
                _UserName = value;
            }
        }

        private string _Password = string.Empty;
        [XmlElement]
        public string Password
        {
            get { return _Password; }
            set
            {
                if (value != null && value.Length > 32)
                {
                    throw new InvalidInputException("Password must be 32 characters or less.");
                }
                _Password = value;
            }
        }

        private string _Email = string.Empty;
        [XmlElement]
        public string Email
        {
            get { return _Email; }
            set
            {
                if (value != null && value.Length > 80)
                {
                    throw new InvalidInputException("Email must be 80 characters or less.");
                }
                _Email = value;
            }
        }

        private string _MemberId = string.Empty;
        [XmlElement]
        public string MemberId
        {
            get { return _MemberId; }
            set
            {
                _MemberId = value;
            }
        }

        private string _Salutation = string.Empty;
        [XmlElement]
        public string Salutation
        {
            get { return _Salutation; }
            set
            {
                if (value != null && value.Length > 15)
                {
                    throw new InvalidInputException("Salutation must be 15 characters or less.");
                }
                _Salutation = value;
            }
        }

        private string _FirstName = string.Empty;
        [XmlElement]
        public string FirstName
        {
            get { return _FirstName; }
            set 
            {
                if (value != null && value.Length > 15)
                {
                    throw new InvalidInputException("FirstName must be 15 characters or less.");
                }
                _FirstName = value; 
            }
        }

        private string _MiddleInitial = string.Empty;
        [XmlElement]
        public string MiddleInitial
        {
            get { return _MiddleInitial; }
            set
            {
                if (value != null && value != null && value.Length > 1)
                {
                    throw new InvalidInputException("MiddleInitial must be 1 character or less.");
                }
                _MiddleInitial = value;
            }
        }

        private string _LastName = string.Empty;
        [XmlElement]
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (value != null && value.Length > 20)
                {
                    throw new InvalidInputException("LastName must be 20 characters or less.");
                }
                _LastName = value;
            }
        }

        private string _Suffix = string.Empty;
        [XmlElement]
        public string Suffix
        {
            get { return _Suffix; }
            set
            {
                if (value != null && value.Length > 15)
                {
                    throw new InvalidInputException("Suffix must be 15 characters or less.");
                }
                _Suffix = value;
            }
        }

        private string _ProfessionalTitle = string.Empty;
        [XmlElement]
        public string ProfessionalTitle
        {
            get { return _ProfessionalTitle; }
            set
            {
                if (value != null && value.Length > 40)
                {
                    throw new InvalidInputException("ProfessionalTitle must be 40 characters or less.");
                }
                _ProfessionalTitle = value;
            }
        }

        [XmlElement]
        public Address Address = new Address();

        [XmlElement]
        public string StatusFlag;

        [XmlElement]
        public List<Subscription> Subscriptions = new List<Subscription>();

        [XmlElement]
        public string StatusDesc
        {
            get
            {
                switch (StatusFlag.ToString().ToLower())
                {
                    case "":
                        return "Active";
                    case "b":
                        return "Bad Address";
                    case "d":
                        return "Deceased/Inactive";
                    case "f":
                        return "Fraud";
                    case "h":
                        return "Hold";
                    case "i":
                        return "Inactive";
                    case "n":
                        return "NSF";
                    case "s":
                        return "Suspended(USPS)";
                    default:
                        return "Unknown";
                }
            }
        }

        [XmlElement]
        public bool OptIn;
        
        #endregion

        public Member() { }
        public Member(int cusid)
        {
            try
            {
                tbl_Customer member = new tbl_Customer(cusid, false);
                this.CusId = member.cusID.ToString();
                this.OptIn = false;
                this.CusCustNum = member.cusCustNum;
                this.ScreenName = member.cusDisplayName;
                this.UserName = member.cusUserName;
                this.MemberId = member.SfgId.ToString();
                this.FirstName = member.cusFirstName;
                this.LastName = member.cusLastName;
                this.Address = new Address(member.addID);
            }
            catch { }
        }
        public void LoadHarperMemberData()
        {
            SqlDataReader reader = null;
            string sql = string.Empty;
            try
            {
                sql = string.Format(@"
                select a.cusid, a.cuscustnum, a.cusdisplayname from tbl_customer a
                join sfg_customernumbers b
                on a.cusid = b.cusid
                where b.sfgcustnum = '{0}'", MemberId);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        CusId = reader["cusid"].ToString();
                        CusCustNum = reader["cuscustnum"].ToString();
                        ScreenName = reader["cusdisplayname"].ToString();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                throw new Exception(string.Format(@"Unable to load harper member data\r\n{0}\r\n{1}", new object[] {sql, ex.Message}));
            }
        }
        public static bool SavePassword()
        {
            return true;
        }
        
        /// <summary>
        /// get cusid, cusdisplayname from tbl_customer by cususername 
        /// if this username exists at AH:
        /// 1 - the cusids must match and 2 - screenname (if one was sent) must match
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cusid"></param>
        /// <param name="screenname"></param>
        /// <returns></returns>
        public static bool CheckDrupalHarperSync(string username, string cusid, string screenname)
        {
            bool isSynced = true;
            Member mbr = new Member();
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("Empty username sent to CheckDrupalHarperSync");
            }
            else
            {
                mbr.UserName = username;
                string sql = string.Format("select cusid, cusdisplayname from tbl_customer where cususername = '{0}'", mbr.UserName);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.HasRows)                                     //do we have this username at AH?
                    {
                        while (reader.Read())
                        {
                            if (reader["cusid"].GetType().Name != "DBNull")
                            {
                                mbr.CusId = reader["cusid"].ToString();
                            }
                            if (reader["cusdisplayname"].GetType().Name != "DBNull")
                            {
                                mbr.ScreenName = reader["cusdisplayname"].ToString();
                            }
                            isSynced = mbr.CusId == cusid;                      //if so, the cusid must be the same as the one passed in
                            if (isSynced && !string.IsNullOrEmpty(screenname))
                            {
                                isSynced = mbr.ScreenName == screenname;        //if screenname passed in it must match
                            }
                        }
                    }
                    else
                    {
                        isSynced = true;                                    //we don't have that username - nothing to sync
                    }
                    reader.Close();
                }
            }
            return isSynced;
        }

        /// <summary>
        /// Get membership that hasn't expired.
        /// Returns false if cusid is not specified. FYI - The user might still be a valid SFG-authenticated member.
        /// Throws an exception if multiple memberships are found.
        /// </summary>
        /// <param name="cusid"></param>
        /// <returns></returns>
        public static bool IsAuthenticatedByHarper(string cusid)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(cusid))
            {
                string sql = string.Format(@"select count(a.mtycode),  as 'memberships' from tbl_netmembership a
                join tbl_membershiptype b on a.mtycode = b.mtycode
                where a.cusid = {0} 
                and getdate() between nmbdatestart and nmbdateend", cusid);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if ((int)reader["memberships"] > 1)
                            {
                                reader.Close();
                                throw new Exception("Unable to determine authentication method - multiple active subscriptions found");
                            }
                            if ((string)reader["mtyauthenticatedbyharper"] == "1")
                            {
                                result = true;
                            }
                        }
                    }
                    reader.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="cusid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool HarperLogin(string username, string password)
        {
            bool match = false;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                string encpassword = "";
                string passwordsalt = "";//D893E71F3F3240B48250BB8198B3F52A

                string sql = string.Format("select cusencryptedpassword, cuspasswordsalt from tbl_customer where cususername = '{0}'", username);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["cusencryptedpassword"].GetType().Name != "DBNull"
                                && reader["cuspasswordsalt"].GetType().Name != "DBNull")
                            {
                                encpassword = reader["cusencryptedpassword"].ToString();
                                passwordsalt = reader["cuspasswordsalt"].ToString();
                            }                            
                        }
                    }
                    reader.Close();
                }
                if (!string.IsNullOrEmpty(encpassword)
                    && !string.IsNullOrEmpty(passwordsalt)
                    && !string.IsNullOrEmpty(password))
                {
                    match = Cryptography.IsMatch(password, encpassword);
                }
            }
            return match;
        }
        
        /// <summary>
        /// The cusids and usernames must match if this sfgid exists at AH.
        /// If screenname isn't empty then it must also match
        /// If no cusid passed in, checks to see that username is available.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cusid"></param>
        /// <param name="screenname"></param>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static bool CheckSFGHarperSync(string username, string cusid, string screenname, string memberid)
        {
            bool isSynced = false;
            Member mbr = new Member();
            if (string.IsNullOrEmpty(cusid))
            {
                string sql = string.Format("select cusid from tbl_customer where cususername = '{0}' or cusdisplayname = {1}", new object[]{username,screenname});
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        isSynced = false;                                  //username and screenname is not available so this account cannot be created
                    }
                    else
                    {
                        isSynced = true;                                   //username and screenname is available so this account can be created
                    }
                    reader.Close();
                }
            }
            else
            {
                mbr.UserName = username;
                string sql = string.Format("select a.cusid, b.cususername, b.cusdisplayname from sfg_customernumbers a join tbl_customer b on a.cusid = b.cusid where a.sfgcustnum = '{0}'", memberid);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["cusid"].GetType().Name != "DBNull")
                            {
                                mbr.CusId = reader["cusid"].ToString();
                            }
                            if (reader["cususername"].GetType().Name != "DBNull")
                            {
                                mbr.UserName = reader["cususername"].ToString();
                            }
                            if (reader["cusdisplayname"].GetType().Name != "DBNull")
                            {
                                mbr.ScreenName = reader["cusdisplayname"].ToString();
                            }
                            isSynced = mbr.CusId == cusid                       //the cusid must match the input 
                                && mbr.UserName == username;                    //the username must match the input
                            if (isSynced && !string.IsNullOrEmpty(screenname))
                            {
                                isSynced = mbr.ScreenName == screenname;        //if screenname passed in it must match
                            }
                        }
                    }
                    else
                    {
                        isSynced = false;                                   //no data at AH so nothing to sync                                   
                    }
                    reader.Close();
                }
            }
            return isSynced;
        }

        /// <summary>
        /// Creates member at AH from data returned by SFG, returns true if member already exists
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool CreateMember(Member member) 
        {
            using (SupportDataDataContext context = new SupportDataDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
            {
                var customer = (from a in context.Customers where a.cusUserName == member.UserName select a).SingleOrDefault();
                var sfglink = (from a in context.SFG_CustomerNumbers where a.SFGCustNum == member.MemberId select a).SingleOrDefault();

                if (customer == null && sfglink != null)
                {
                    //someone else already has that sfgid
                    return false;
                }
                try
                {
                    if (customer == null)
                    {
                        AddressCustomer address = new AddressCustomer();
                        address.addAddress1 = member.Address.Address1;
                        address.addAddress2 = member.Address.Address2;
                        address.addAddress3 = member.Address.Address3;
                        address.addCity = member.Address.City;
                        address.addCountry = member.Address.Country;
                        address.addDateCreated = DateTime.Now;
                        address.addDateUpdated = DateTime.Now;
                        address.addPostalCode = member.Address.PostalCode;
                        address.addRegion = member.Address.State;
                        context.AddressCustomers.InsertOnSubmit(address);
                        context.SubmitChanges();

                        var custnum = (from a in context.Customers select a.cusCustNum).Max();
                        int nextcustnum = int.Parse(custnum) + 1;

                        customer = new Customer();
                        customer.addID = address.addID;
                        customer.cusCustNum = nextcustnum.ToString();
                        customer.cusFirstName = member.FirstName;
                        customer.cusLastName = member.LastName;
                        customer.cusCustType = "UNKNOWN";
                        customer.cusPriFirstName = member.FirstName;
                        customer.cusPriLastName = member.LastName;
                        customer.cusPrefix = member.Salutation;
                        customer.cusSuffix = member.Suffix;
                        customer.cusEmail = member.Email;
                        customer.cusUserName = member.UserName;
                        customer.cusIsCharterMem = false;
                        customer.cusIsDeleted = false;
                        customer.cusSex = 'U';
                        customer.cusHasDisplayName = true;
                        customer.cusDisplayName = member.UserName;
                        customer.cusGUID = Guid.NewGuid();
                        context.Customers.InsertOnSubmit(customer);
                        context.SubmitChanges();
                    }
                    if (sfglink == null)
                    {
                        sfglink = new SFG_CustomerNumber();
                        sfglink.SFGCustNum = member.MemberId;
                        sfglink.cusID = customer.cusID;
                        context.SFG_CustomerNumbers.InsertOnSubmit(sfglink);
                        context.SubmitChanges();
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Looks for a SFG-AH link in the AH database
        /// </summary>
        /// <param name="cusid"></param>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static bool IsCurrentSFGMember(string cusid, string memberid) 
        {
            bool iscurrentmember = false;
            string sql = "svcs_IsCurrentMember";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
            using (SqlCommand comm = new SqlCommand(sql, conn))
            {
                conn.Open();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@cusid", cusid));
                comm.Parameters.Add(new SqlParameter("@sfgid", memberid));
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        iscurrentmember = reader["iscurrentmember"].ToString() == "1" ? true : false;
                    }
                }
                reader.Close();
            }
            return iscurrentmember; 
        }

        /// <summary>
        /// Deletes all roles for the member then inserts a role for the most recent, active, non-donor subscription
        /// </summary>
        /// <param name="cusid"></param>
        /// <param name="subscriptions"></param>
        /// <returns></returns>
        public static SubscriptionValidation UpdateMemberRole(string username, List<Subscription> subscriptions) 
        {
            SubscriptionValidation validation = new SubscriptionValidation();
            validation.Access = false;
            validation.StatusFlag = "";
            Subscription current = null;
            subscriptions.Sort(delegate (Subscription s1, Subscription s2)
                {
                    return s2.ExpireDate.CompareTo(s1.ExpireDate);
                });
            foreach (Subscription subscription in subscriptions)
            {
                if (!subscription.IsDonor && (subscription.StatusFlag == "P" || subscription.StatusFlag == "0"))
                {
                    validation.StatusFlag = subscription.StatusFlag;
                    current = subscription;
                    break;
                }
            }
            if (current != null)
            {
                using (SupportDataDataContext context = new SupportDataDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                {
                    SupportClasses.Customer customer = null;
                    SupportClasses.MembershipType membershiptype = null;
                    SupportClasses.NetMembership netmembership = null;
                    try
                    {
                        customer = (from a in context.Customers where a.cusUserName == username select a).SingleOrDefault();
                    }
                    catch
                    {
                        throw new MembershipUsernameNotUniqueException();
                    }
                    if (customer == null)
                    {
                        throw new MembershipUnknownUsernameException();
                    }
                    try
                    {
                        membershiptype = (from a in context.MembershipTypes
                                          join b in context.SFG_ProdCodes
                                          on a.mtyCode equals b.IntCode
                                          where b.ExtCode == current.PublicationCode
                                          select a).Single();
                    }
                    catch
                    {
                        throw new Exception(string.Format("Unable to update member's membership type - could not find membership type code {0}.", current.PublicationCode));
                    }

                    try
                    {
                        netmembership = (from a in context.NetMemberships where a.cusID == customer.cusID select a).SingleOrDefault();
                        if (netmembership != null)
                        {
                            context.NetMemberships.DeleteOnSubmit(netmembership);
                            context.SubmitChanges();
                        }
                        netmembership = new NetMembership();
                        netmembership.cusID = customer.cusID;
                        netmembership.mtyCode = membershiptype.mtyCode;
                        netmembership.nmbDateCreated = DateTime.Now;
                        DateTime created;
                        if (!DateTime.TryParse(current.DateEntered, out created))
                        {
                            created = DateTime.Now;
                        }
                        netmembership.nmbDateStart = created;
                        DateTime exp;
                        if (!DateTime.TryParse(current.ExpireDate, out exp))
                        {
                            exp = DateTime.Now.AddMonths(12);
                        }
                        netmembership.nmbDateEnd = exp;
                        context.NetMemberships.InsertOnSubmit(netmembership);
                        context.SubmitChanges();
                    }
                    catch (Exception ex1)
                    {
                        throw new Exception(string.Format("Unable to update member's membership type - error creating netmembership {0}.", ex1.Message));
                    }
                    validation.Access = true;
                }
            }
            return validation; 
        }
        
        /// <summary>
        /// Gets cusid and screen name for comp member
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static Member GetCompMember(string username)
        {
            Member compmember = new Member();
            compmember.UserName = username;
            string sql = "svcs_GetCompMember";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
            using (SqlCommand comm = new SqlCommand(sql, conn))
            {
                conn.Open();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@username", username));
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["cusid"].ToString().GetType().Name == "DBNull")
                        {
                            reader.Close();
                            throw new Exception("No cusid for member");
                        }
                        compmember.CusId = Utilities.GetStringFromDB(reader["cusid"]);
                        compmember.FirstName = Utilities.GetStringFromDB(reader["cusfirstname"]);
                        compmember.LastName = Utilities.GetStringFromDB(reader["cuslastname"]);
                        compmember.Email = Utilities.GetStringFromDB(reader["cusemail"]);
                        compmember.Salutation = Utilities.GetStringFromDB(reader["cusprefix"]);
                        compmember.ProfessionalTitle = Utilities.GetStringFromDB(reader["custitle"]);
                        compmember.ScreenName = Utilities.GetStringFromDB(reader["cusdisplayname"]);
                        compmember.Suffix = Utilities.GetStringFromDB(reader["cussuffix"]);
                        compmember.Address.Address1 = Utilities.GetStringFromDB(reader["addaddress1"]);
                        compmember.Address.Address2 = Utilities.GetStringFromDB(reader["addaddress2"]);
                        compmember.Address.Address3 = Utilities.GetStringFromDB(reader["addaddress3"]);
                        compmember.Address.BusinessName = Utilities.GetStringFromDB(reader["cuscompany"]);
                        compmember.Address.City = Utilities.GetStringFromDB(reader["addcity"]);
                        compmember.Address.Country = Utilities.GetStringFromDB(reader["addcountry"]);
                        compmember.Address.Fax = Utilities.GetStringFromDB(reader["cusfax"]);
                        compmember.Address.Phone = Utilities.GetStringFromDB(reader["cusphone1"]);
                        compmember.Address.PostalCode = Utilities.GetStringFromDB(reader["addpostalcode"]);
                        compmember.Address.State = Utilities.GetStringFromDB(reader["addregion"]);
                    }
                }
                reader.Close();
            }
            return compmember; 
        }

        public static Subscription GetCompSubscription(Member member)
        {
            Subscription compsubscription = new Subscription();
            compsubscription.Category = SubscriptionCategory.Publication;
            compsubscription.SubscribingMember = member.MemberId;
            compsubscription.StatusFlag = "p";
            string sql = "svcs_GetCompMember";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
            using (SqlCommand comm = new SqlCommand(sql, conn))
            {
                conn.Open();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@cusid", member.CusId));
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        compsubscription.PublicationCode = Utilities.GetStringFromDB(reader["mtycode"]);
                        compsubscription.DateEntered = Utilities.GetStringFromDB(reader["nmbdatestart"]);
                        compsubscription.ExpireDate = Utilities.GetStringFromDB(reader["nmbdateend"]);
                    }
                }
                reader.Close();
            }            
            return compsubscription;
        }
	}
    public class MembershipUnknownUsernameException : Exception { }
    public class MembershipUsernameNotUniqueException : Exception { }
}
