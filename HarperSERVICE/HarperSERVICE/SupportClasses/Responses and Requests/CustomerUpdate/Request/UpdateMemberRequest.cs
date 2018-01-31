using System;

namespace SupportClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateMemberRequest : CustomerUpdateServiceRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberToUpdate"></param>
        /// <param name="optIn"></param>
        /// <param name="debug"></param>
        public UpdateMemberRequest(Member memberToUpdate, bool debug)
        {
            base.Debug = debug;
            this.MemberToUpdate = memberToUpdate;
        }

        public UpdateMemberRequest(string sfgid, string prefix, string firstname, string lastname, string suffix, string title, string email, bool optin,
                                   string company, string address1, string address2, string address3, string city, string region, string postalcode, string country, string phone)
        {
            MemberToUpdate = new Member();
            MemberToUpdate.MemberId = sfgid;
            MemberToUpdate.Salutation = prefix;
            MemberToUpdate.FirstName = firstname;
            MemberToUpdate.LastName = lastname;
            MemberToUpdate.Suffix = suffix;
            MemberToUpdate.ProfessionalTitle = title;
            MemberToUpdate.OptIn = optin;                          
            MemberToUpdate.Email = email;

            MemberToUpdate.Address = new Address();
            MemberToUpdate.Address.BusinessName = company;
            MemberToUpdate.Address.Address1 = address1;
            MemberToUpdate.Address.Address2 = address2;
            MemberToUpdate.Address.Address3 = address3;
            MemberToUpdate.Address.City = city;
            MemberToUpdate.Address.State = region;
            MemberToUpdate.Address.PostalCode = postalcode;
            MemberToUpdate.Address.Country = country;
            MemberToUpdate.Address.Phone = phone;
        }
    }
}

