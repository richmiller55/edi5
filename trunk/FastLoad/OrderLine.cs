namespace FastLoad
{
    public class OrderLine
    {
        int lineNum;
        string customerPart;
        string upc;
        decimal orderQty;
        decimal unitPrice;

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
        public decimal LineNum
        {
            get { return lineNum;  }
            set { lineNum = value; }
        }

        public void setLineNo(string lineNo) 
        { 
            LineNum = Convert.ToInt32(lineNo); 
        }
        public void setQty(string qty) { 
            OrderQty = Convert.ToDecimal(qty); 
        }
        public void setUnitPrice(string price) { 
            UnitPrice = Convert.ToDecimal(price); 
        }
    }
}
