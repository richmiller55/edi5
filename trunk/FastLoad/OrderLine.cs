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
    }
}
