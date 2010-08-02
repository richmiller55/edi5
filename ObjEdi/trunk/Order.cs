using System;
// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace ObjEdi
{
    public class Order
    {
        string aicOrderNo;
        string poNumber;
        string customerId;
        string shipToId;
        string shipVia;
        string termsCode;
        
        System.DateTime orderDate;
        System.DateTime requestDate;
        System.DateTime needByDate;
        public ArrayList lines;
        OrderLine currentLine;

        public Order()
        {
            //
            shipToId = "";
            lines = new ArrayList();
            currentLine = new OrderLine();
        }
        public void postLine()
        {
            if 
            lines.Add(currentLine);
            currentLine = new OrderLine();
        }
        // set functions
        public void setPoNum(string poNo) { poNumber = poNo; }
        public void setCustomerId(string custId) { customerId = custId; }
        public void setShipTo(string shipTo) { shipToId = shipTo; }
        public void setLocation(string gln) { shipToId = gln; }
        public void setAicOrderNo(string aicOrd) { aicOrderNo = aicOrd; }
        public void setShipVia(string via) { shipVia = via; }
        public void setTerms(string terms) { termsCode = terms; }
        
        // get functions
        public string getSoldTo() { return customerId; }	
        public string getCustomerId() { return customerId; }	
        public string getShipTo() { return shipToId; }
        public string getPoNum() { return poNumber; }
        public string getShipVia() { return shipVia; }
        public string getTermsCode() { return termsCode; }
        public string getAicOrderNo() { return aicOrderNo; }
        
        public System.DateTime getOrderDate() { return orderDate; }
        public System.DateTime getRequestDate() { return requestDate; }
        public System.DateTime getNeedByDate() { return needByDate; }

        public void setRequestDate(string date)
        {
            requestDate = convertStrToDate2(date);
        }
        public void setOrderDate(string date)
        {
            orderDate = convertStrToDate2(date);
        }
        public void setNeedByDate(string date)
        {
            needByDate = convertStrToDate2(date);
        }

        public void setOrderLineNo(string lineno)
        {
            currentLine.setLineNo(lineno);
        }
        public void setUpc(string part)
        {
            currentLine.setUpc(part);
        }
        public void setRev(string rev)
        {
            currentLine.setRev(rev);
        }
        public void setOrderQty(string qty)
        {
            currentLine.setQty(qty);
        }
        public void setUnitPrice(string price)
        {
            currentLine.setUnitPrice(price);
        }
        public void setFactor(string factor)
        {
            currentLine.setFactor(factor);
        }
        public void setLineRef(string lineRef)
        {
            currentLine.setLineRef(lineRef);
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
        public System.DateTime convertStrToDate2(string dateStr)
        {
            string year = dateStr.Substring(6, 4);
            string month = dateStr.Substring(0, 2);
            string day = dateStr.Substring(3, 2);

            System.DateTime dateObj = new DateTime(Convert.ToInt32(year),
                Convert.ToInt32(month), Convert.ToInt32(day));
            return dateObj;
        }
    }
}