using System;

// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace FastLoad
{
    public class SalesOrder
    {
        string poNumber;
        string customerId;
        string shipToNum;
        string gln;
        string shipViaCode;
        string termsCode;
        string company;
        string shortChar01 = "EDI";
        string shortChar02 = "";
        string shortChar03 = "";
        string shortChar04 = "";
        string shortChar05 = "";
        string shortChar06 = "";
        string shortChar07 = "";
        string shortChar08 = "";
        string shortChar09 = "";
        string shortChar10 = "";
        string character01 = "";
        System.DateTime date05;
        System.DateTime orderDate;
        System.DateTime needByDate;
        System.DateTime requestDate;
        System.DateTime shipNoLaterDate;
        public ArrayList lines;
        OrderLine currentLine;

        public SalesOrder()
        {
            lines = new ArrayList();
            currentLine = new OrderLine();
        }
        public void postLine()
        {
            lines.Add(currentLine);
            currentLine = new OrderLine();
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
        public string GLN
        {
            get
            {
                return gln;
            }
            set
            {
                gln = value;
            }
        }            
        public string ShipVia
        {
            get
            {
                return shipViaCode;
            }
            set
            {
                shipViaCode = value;
            }
        }            
        public string CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public string OrderSource
        {
            get { return shortChar01; }
            set { shortChar01 = value; }
        }

        public string PoNo
        {
            get { return poNumber; }
            set { poNumber = value; }
        }
        public string TermsCode
        {
            get { return termsCode; }
            set { termsCode = value; }
        }
        public string Company
        {
            get { return company; }
            set { company = value; }
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
        public System.DateTime RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; }
        }
        public System.DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }
        public System.DateTime Date05
        {
            get { return date05; }
            set { date05 = value; }
        }
        public decimal OrderQty
        {
            get { return currentLine.OrderQty; }
            set { currentLine.OrderQty = value; }
        }
        public int OrderLineNum
        {
            get { return currentLine.LineNum; }
            set { currentLine.LineNum = value; }
        }
        public decimal UnitPrice
        {
            get { return currentLine.UnitPrice; }
            set { currentLine.UnitPrice = value; }
        }
        public decimal SellingFactor
        {
            get { return currentLine.SellingFactor; }
            set { currentLine.SellingFactor = value; }
        }
        public string CustomerPart
        {
            get { return currentLine.CustomerPart; }
            set { currentLine.CustomerPart = value; }
        }
        public decimal SellingQty
        {
            get { return currentLine.SellingQty; }
            set { currentLine.SellingQty = value; }
        }
        public string SellingFactorDirection
        {
            get { return currentLine.SellingFactorDirection; }
            set { currentLine.SellingFactorDirection = value; }
        }
        public string Upc
        {
            get { return currentLine.Upc; }
            set { currentLine.Upc = value; }
        }
        public string ShortChar02
        {
            get { return shortChar02; }
            set { shortChar02 = value; }
        }
        public string LineShortChar02
        {
            get { return currentLine.ShortChar02; }
            set { currentLine.ShortChar02 = value; }
        }
        public string LineShortChar03
        {
            get { return currentLine.ShortChar03; }
            set { currentLine.ShortChar03 = value; }
        }
        public string LineShortChar04
        {
            get { return currentLine.ShortChar04; }
            set { currentLine.ShortChar04 = value; }
        }
        public string LineShortChar05
        {
            get { return currentLine.ShortChar05; }
            set { currentLine.ShortChar05 = value; }
        }
        public string LineShortChar06
        {
            get { return currentLine.ShortChar06; }
            set { currentLine.ShortChar06 = value; }
        }
        public string LineShortChar07
        {
            get { return currentLine.ShortChar07; }
            set { currentLine.ShortChar07 = value; }
        }
        public string LineCharacter01
        {
            get { return currentLine.Character01; }
            set { currentLine.Character01 = value; }
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
        public string ShortChar08
        {
            get { return shortChar08; }
            set { shortChar08 = value; }
        }
        public string ShortChar09
        {
            get { return shortChar09; }
            set { shortChar09 = value; }
        }
        public string ShortChar10
        {
            get { return shortChar10; }
            set { shortChar10 = value; }
        }
        public string Character01
        {
            get { return character01; }
            set { character01 = value; }
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
        // public string getPoNum() { return PoNo + ":" + ShipToId; }
    }
}
