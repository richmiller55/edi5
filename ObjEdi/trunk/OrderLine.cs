using System;
using System.Collections.Generic;
using System.Text;

namespace ObjEdi
{
    public class OrderLine
    {
        int lineNumber;
        string customerPart;
        string ourPart;
        string upc;
        int rev;
        decimal orderQty;
        decimal unitPrice;
        decimal factor;
        string lineRef;

        public OrderLine()
        {
            //
            // TODO: Add constructor logic here
            factor = 1;
            //
        }
        public void setCustomerPart(string part) { customerPart = part; }
        public void setOurPart(string part) { ourPart = part; }
        public void setUpc(string part) { upc = part; }
        public void setLineRef(string oldStyle) { lineRef = oldStyle; }
        
        public void setLineNo(string lineNo)
        {
            lineNumber = Convert.ToInt32(lineNo);
        }
        public void setRev(string revNo)
        {
            rev = Convert.ToInt32(revNo);
        }

        public void setFactor(string factorIn)
        {
            factor = Convert.ToDecimal(factorIn); 
        }
        public void setQty(string qty)
        {
            orderQty = Convert.ToDecimal(qty);
        }
        public void setUnitPrice(string price)
        {
            unitPrice = Convert.ToDecimal(price);
        }
        public int getLineNo() { return lineNumber; }
        public string getUpc() { return upc; }
        public int getRev() { return rev; }
        public decimal getQty() { return orderQty; }
        public decimal getUnitPrice() { return unitPrice; }
        public string getLineRef() { return lineRef; }
        public decimal getFactor() { return factor; }
    }
}
