namespace FastLoad
{
    public class OrderLine
    {
        int lineNumber;
        string customerPart;
        string ourPart;
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
       public void setCustomerPart(string part){customerPart = part;}
        public void setOurPart(string part){ourPart = part;}
        public void setUpc(string part){upc = part;}

        public void setLineNo(string lineNo) 
        { 
            lineNumber = Convert.ToInt32(lineNo); 
        }
        public void setQty(string qty) { 
            orderQty = Convert.ToDecimal(qty); 
        }
        public void setUnitPrice(string price) { 
            unitPrice = Convert.ToDecimal(price); 
        }
        public int getLineNo() { return lineNumber; }
        public string getUpc() { return upc; }
        public decimal getQty() {return orderQty;}
        public decimal getUnitPrice(){return unitPrice;}
    }
}
