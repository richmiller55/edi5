using System;
// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace FastLoad
{
    public class ExShipTo : ShipTo
    {
        string Fax;
        string StoreRegion;
        string StoreArea;
        string StoreGM;
        string RDO;
        string LPM;
        string BusinessUnit;

        public ExShipTo()
        {
            //
        }
        public void FixNameCorp()
        {
            if (BusinessUnit.Equals("PRL-C"))
            {
                String baseName = "Pearle Vision " + this.getShipTo().ToString();
                setName(baseName);
            }
            if (BusinessUnit.Equals("PRL-F") & getName().ToUpper().Substring(0,6).Equals("PEARLE"))
            {
                String baseName = "Pearle Franchise " + this.getShipTo().ToString();
                setName(baseName);
            }
        }
        public string fax
        {
            get
            {
                return Fax;
            }
            set
            {
                Fax = value;
            }
        }
        public string storeRegion
        {
            get
            {
                return StoreRegion;
            }
            set
            {
                StoreRegion = value;
            }
        }
        public string storeArea
        {
            get
            {
                return StoreArea;
            }
            set
            {
                StoreArea = value;
            }
        }
        public string storeGM
        {
            get
            {
                return StoreGM;
            }
            set
            {
                StoreGM = value;
            }
        }
        public string rdo
        {
            get
            {
                return RDO;
            }
            set
            {
                RDO = value;
            }
        }
        public string lpm
        {
            get
            {
                return LPM;
            }
            set
            {
                LPM = value;
            }
        }
        public string businessUnit
        {
            get
            {
                return BusinessUnit;
            }
            set
            {
                BusinessUnit = value;
            }
        }
   }
}