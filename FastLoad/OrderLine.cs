namespace FastLoad
{
    public class OrderLine
    {
        int lineNum;
        string customerPart;
        string upc;
        decimal orderQty;
        decimal unitPrice;
        decimal sellingFactor;
        string sellingFactorDirection;
        decimal sellingQty;

        string shipToNum;
        string shortChar02 = "";
        string shortChar03 = "";
        string shortChar04 = "";
        string shortChar05 = "";
        string shortChar06 = "";
        string shortChar07 = "";
        string character01 = "";

        public OrderLine()
        {
            this.CustomerPart = "";
        }
        public string CustomerPart
        {
            get
            {
                return customerPart;
            }
            set
            {
                customerPart = value;
            }
        }
        public string Upc
        {
            get { return upc;  }
            set { upc = value; }
        }
        public decimal OrderQty
        {
            get { return orderQty;  }
            set { orderQty = value; }
        }
        public decimal UnitPrice
        {
            get { return unitPrice;  }
            set { unitPrice = value; }
        }
        public decimal SellingFactor
        {
            get { return sellingFactor; }
            set { sellingFactor = value; }
        }
        public decimal SellingQty
        {
            get { return sellingQty; }
            set { sellingQty = value; }
        }
        public int LineNum
        {
            get { return lineNum;  }
            set { lineNum = value; }
        }
        public string ShipToNum
        {
            get { return shipToNum;  }
            set { shipToNum = value; }
        }
        public string SellingFactorDirection
        {
            get { return sellingFactorDirection; }
            set { sellingFactorDirection = value; }
        }
        public string ShortChar02
        {
            get { return shortChar02; }
            set { shortChar02 = value; }
        }
        public string ShortChar03
        {
            get { return shortChar03; }
            set { shortChar03 = value; }
        }
        public string ShortChar04
        {
            get { return shortChar04; }
            set { shortChar04 = value; }
        }
        public string ShortChar05 
        {
            get { return shortChar05; }
            set { shortChar05 = value; }
        }
        public string ShortChar06
        {
            get { return shortChar06; }
            set { shortChar06 = value; }
        }
        public string ShortChar07
        {
            get { return shortChar07; }
            set { shortChar07 = value; }
        }
        public string Character01
        {
            get { return character01; }
            set { character01 = value; }
        }

    }
}
