using System;
// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace ShipToLoad
{
    public class ShipTo
    {
        string custId;
        string shipToId;
        string name;
        string address1;
        string address2 = "";
        string address3 = "";
        string city = "";
        string state;
        string zip;
        string country;
        string phone;
        int    shipOrder;
        int    countryNo;
        string shipVia;

        public ShipTo()
        {
            //
        }

        public string CustId
        {
            get
            {
                return custId;
            }
            set
            {
                custId = value;
            }
        }
        public string ShipToId
        {
            get
            {
                return shipToId;
            }
            set
            {
                shipToId = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Address1
        {
            get
            {
                return address1;
            }
            set
            {
                address1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return address2;
            }
            set
            {
                address2 = value;
            }
        }
        public string Address3
        {
            get
            {
                return address3;
            }
            set
            {
                address3 = value;
            }
        }
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        public string Zip
        {
            get
            {
                return zip;
            }
            set
            {
                zip = value;
            }
        }
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }
        public int ShipOrder
        {
            get
            {
                return shipOrder;
            }
            set
            {
                shipOrder = value;
            }
        }
        public int CountryNo
        {
            get
            {
                return countryNo;
            }
            set
            {
                countryNo = value;
            }
        }
        public string ShipVia
        {
            get
            {
                return shipVia;
            }
            set
            {
                shipVia = value;
            }
        }
        public void setCountryNo(string country)
        {
            this.CountryNo = 1;
            this.Country = "USA";
            if (country.ToUpper().Equals("CANADA"))
            {
                this.CountryNo = 36;
                this.Country = "Canada";
            }
            if (country.ToUpper().Equals("PR"))
            {
                this.CountryNo = 149;
                this.Country = "Puerto Rico";
            }
        }
   }
}
