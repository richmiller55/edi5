using System;

// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace FastLoad
{
    public class Order
    {
        string poNumber;
        string customerId;
        string shipToNum;
        string shipViaCode;

        System.DateTime orderDate;
        System.DateTime needByDate;
        System.DateTime requestDate;
        System.DateTime shipNoLaterDate;
        public ArrayList lines;
        ShipToOrderLine currentLine;

        public Order()
        {
            lines = new ArrayList();
            currentLine = new ShipToOrderLine();
        }
        public void postLine()
        {
            lines.Add(currentLine);
            currentLine = new ShipToOrderLine();
        }
        public string ShipToNum
        {
            get
            {
                return shipToNum;
            }
            set
            {
                shipToNum = value;
            }
        }
        public void setLocation(string gln){shipToId = gln;}
        public void setShipVia(string shipVia) { shipViaCode = shipVia; }
        
        public string getShipVia() { return shipViaCode; }
        public System.DateTime getOrderDate() { return orderDate; }
        public System.DateTime getRequestDate() { return requestDate; }
        public string CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public string PoNo
        {
            get { return poNumber; }
            set { poNumber = value; }
        }
        public string ShipToId
        {
            get { return shipToId; }
            set { shipToId = value; }
        }
        public System.DateTime NeedByDate
        {
            get { return needByDate; }
            set { needByDate = value; }
        }
        public System.DateTime ShipNoLaterDate
        {
            get { return shipNoLaterDate; }
            set { shipNoLaterDate = value; }
        }
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
}
