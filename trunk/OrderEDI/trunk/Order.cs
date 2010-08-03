using System;

// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace OrderEDI
{
    public class Order
    {
        protected string poNumber;
        string customerId;
        protected string shipToId;
        string shipViaCode;
        System.DateTime orderDate;
        System.DateTime requestDate;
        public ArrayList lines;
        protected ShipToOrderLine currentLine;

        public Order()
        {
            //
            lines = new ArrayList();
            currentLine = new ShipToOrderLine();
            // TODO: Add constructor logic here
            //
        }
        public void postLine()
        {
            lines.Add(currentLine);
            currentLine = new ShipToOrderLine();
        }
        public void setPoNum(string poNo){poNumber = poNo;}
        
        public void setCustomerId(string custId){customerId = custId;}
        public void setShipTo(string shipTo){shipToId = shipTo;}
        public void setLocation(string gln){shipToId = gln;}
        public void setShipVia(string shipVia) { shipViaCode = shipVia; }
        public string getSoldTo() { return customerId; }
        public string getShipTo() { return shipToId; }
        public string getPoNum() { return poNumber; }
        public string getShipVia() { return shipViaCode; }
        public System.DateTime getOrderDate() { return orderDate; }
        public System.DateTime getRequestDate() { return requestDate; }
        
        public void setRequestDate(string date)
        {
            requestDate = convertStrToDate(date);
        }
        public void setOrderDate(string date)
        {
            orderDate = convertStrToDate(date);
        }
        public void setOrderLineNo(string lineno)
        {
            currentLine.setLineNo(lineno);
        }
        public void setUpc(string part)
        {
            currentLine.setUpc(part);
        }
        public void setOrderQty(string qty)
        {
            currentLine.setQty(qty);
        }
        public void setUnitPrice(string price)
        {
            currentLine.setUnitPrice(price);
        }
        public System.DateTime convertStrToDate(string dateStr)
        {
            string year = dateStr.Substring(0, 4);
            string month = dateStr.Substring(4, 2);
            string day = dateStr.Substring(6, 2);

            System.DateTime dateObj = new DateTime(Convert.ToInt32(year),
                Convert.ToInt32(month), Convert.ToInt32(day));
            return dateObj;
        }

    }
    public class OrderLine
    {
        protected int lineNumber;
        protected string customerPart;
        protected string ourPart;
        protected string upc;
        protected decimal orderQty;
        protected decimal unitPrice;

        public OrderLine()
        {
            //
            // TODO: Add constructor logic here
            //
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