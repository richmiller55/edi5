namespace FastLoad
{
    public class OrderLine
    {
        int lineNum;
        string customerPart;
        string upc;
        decimal orderQty;
        decimal unitPrice;
        string shipToNum;

        public OrderLine()
        {
            //
            // TODO: Add constructor logic here
            //
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
    }
}
