using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using HarperLINQ;

namespace SupportClasses
{
	public class Address
	{
        public Address() { }
        public Address(int addid)
        {
            tbl_AddressCustomer add = new tbl_AddressCustomer(addid);
            this.Address1 = add.addAddress1;
            this.Address2 = add.addAddress2;
            this.City = add.addCity;
            this.State = add.addRegion;
            this.StateCode = add.addRegion;
            this.PostalCode = add.addPostalCode;
            this.Country = add.addCountry;
        }
        private string _BusinessName = string.Empty;
        [XmlElement]
        public string BusinessName
        {
            get { return _BusinessName; }
            set
            {
                if (value != null && value.Length > 40)
                {
                    throw new InvalidInputException("BusinessName must be 40 characters or less.");
                }
                _BusinessName = value;
            }
        }

        private string _Address1 = string.Empty;
        [XmlElement]
        public string Address1
        {
            get { return _Address1; }
            set
            {
                if (value != null && value.Length > 40)
                {
                    throw new InvalidInputException("Address1 must be 40 characters or less.");
                }
                _Address1 = value;
            }
        }

        private string _Address2 = string.Empty;
        [XmlElement]
        public string Address2
        {
            get { return _Address2; }
            set
            {
                if (value != null && value.Length > 40)
                {
                    throw new InvalidInputException("Address2 must be 40 characters or less.");
                }
                _Address2 = value;
            }
        }

        private string _Address3 = string.Empty;
        [XmlElement]
        public string Address3
        {
            get { return _Address3; }
            set
            {
                if (value != null && value.Length > 40)
                {
                    throw new InvalidInputException("Address3 must be 40 characters or less.");
                }
                _Address3 = value;
            }
        }

        private string _City = string.Empty;
        [XmlElement]
        public string City
        {
            get { return _City; }
            set
            {
                if (value != null && value.Length > 30)
                {
                    throw new InvalidInputException("City must be 30 characters or less.");
                }
                _City = value;
            }
        }
        
        [XmlElement]
		public string State;
        
        [XmlElement]
		public string StateCode;

        private string _PostalCode = string.Empty;
        [XmlElement]
        public string PostalCode
        {
            get { return _PostalCode; }
            set
            {
                if (value != null && value.Length > 10)
                {
                    throw new InvalidInputException("PostalCode must be 10 characters or less.");
                }
                _PostalCode = value;
            }
        }

        private string _Country = string.Empty;
        [XmlElement]
        public string Country
        {
            get { return _Country; }
            set
            {
                if (value != null && value.Length > 30)
                {
                    throw new InvalidInputException("Country must be 30 characters or less.");
                }
                _Country = value;
            }
        }

        private string _Phone = string.Empty;
        [XmlElement]
        public string Phone
        {
            get { return _Phone; }
            set
            {
                if (value != null && value.Length > 15)
                {
                    throw new InvalidInputException("Phone must be 15 characters or less.");
                }
                _Phone = value;
            }
        }

        private string _Fax = string.Empty;
        [XmlElement]
        public string Fax
        {
            get { return _Fax; }
            set
            {
                if (value != null && value.Length > 15)
                {
                    throw new InvalidInputException("Fax must be 15 characters or less.");
                }
                _Fax = value;
            }
        }

        private string _AltCity = string.Empty;
        [XmlElement]
        public string AltCity
        {
            get { return _AltCity; }
            set
            {
                if (value != null && value.Length > 30)
                {
                    throw new InvalidInputException("AltCity must be 30 characters or less.");
                }
                _AltCity = value;
            }
        }
	}
}
