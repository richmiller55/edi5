using System;
// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace ObjEdi
{
    public class Order
    {
        private string m_poNumber;
        // private string m_aicOrderNo;
        private string m_customerId;
        private string m_shipToId;
        private string m_shipVia;
        private string m_termsCode;
        private string m_glnLocation;
        private int m_validLines;        
        private System.DateTime m_orderDate;
        private System.DateTime m_requestDate;
        private System.DateTime m_needByDate;
        public ArrayList lines;
        private OrderLine m_currentLine;

        public Order()
        {
            //
            this.m_shipToId = "";
            this.m_validLines = 0;
            this.lines = new ArrayList();
            this.CurrentLine = new OrderLine();
        }
        public void postLineAfter860()
        {
            string canceledItem1 = "23473952";
            string canceledItem2 = "23474174";
            if (this.CurrentLine.CustomerPart == canceledItem1)
            {
                this.CurrentLine = new OrderLine();
            }
            else if (this.CurrentLine.CustomerPart == canceledItem2)
            {
                this.CurrentLine = new OrderLine();
            }
            else
            {
                lines.Add(this.CurrentLine);
                this.CurrentLine = new OrderLine();
                this.ValidLines += 1;
            }
        }
        public void postLine()
        {
            lines.Add(this.CurrentLine);
            this.CurrentLine = new OrderLine();
            this.ValidLines += 1;
        }
        public OrderLine CurrentLine
        {
            get
            {
                return m_currentLine;
            }
            set
            {
                m_currentLine = value;
            }
        }

        public string PONumber
        {
            get
            {
                return m_poNumber;
            }
            set
            {
                m_poNumber = value;
            }
        }
        public string CustomerID
        {
            get
            {
                return m_customerId;
            }
            set
            {
                m_customerId = value;
            }
        }
        public string CustomerPart
        {
            get
            {
                return this.CurrentLine.CustomerPart;
            }
            set
            {
                this.CurrentLine.CustomerPart = value;
            }
        }
        public int ValidLines
        {
            get
            {
                return m_validLines;
            }
            set
            {
                m_validLines = value;
            }
        }
        public string ShipTo
        {
            get
            {
                return m_shipToId;
            }
            set
            {
                m_shipToId = value;
            }
        }
        public string GLN
        {
            get
            {
                return m_glnLocation;
            }
            set
            {
                m_glnLocation = value;
            }
        }
        public string ShipVia
        {
            get
            {
                return m_shipVia;
            }
            set
            {
                m_shipVia = value;
            }
        }
        public string TermsCode
        {
            get
            {
                return m_termsCode;
            }
            set
            {
                m_termsCode = value;
            }
        }
        public System.DateTime OrderDate
        {
            get
            {
                return m_orderDate;
            }
            set
            {
                m_orderDate = value;
            }
        }
        public System.DateTime RequestDate
        {
            get
            {
                return m_requestDate;
            }
            set
            {
                m_requestDate = value;
            }
        }
        public System.DateTime NeedByDate
        {
            get
            {
                return m_needByDate;
            }
            set
            {
                m_needByDate = value;
            }
        }
        public string OrderDateStr
        {
            get
            {
                return m_orderDate.ToString();
            }
            set
            {
                m_orderDate = this.convertStrToDate2(value);
            }
        }
        public string RequestDateStr
        {
            get
            {
                return m_requestDate.ToString();
            }
            set
            {
                m_requestDate = this.convertStrToDate2(value);
            }
        }
        public string NeedByDateStr
        {
            get
            {
                return m_needByDate.ToString();
            }
            set
            {
                m_needByDate = this.convertStrToDate2(value);
            }
        }
        public string OrderLineNo
        {
            get
            {
                return this.CurrentLine.LineNo.ToString();
            }
            set
            {
                this.CurrentLine.LineNo = Convert.ToInt32(value);
            }
        }
        public string UPC
        {
            get
            {
                return this.CurrentLine.UPC;
            }
            set
            {
                this.CurrentLine.UPC = value;
            }
        }
        public int Rev
        {
            get
            {
                return this.CurrentLine.Rev;
            }
            set
            {
                this.CurrentLine.Rev = value;
            }
        }
        public decimal OrderQty
        {
            get
            {
                return this.CurrentLine.Qty;
            }
            set
            {
                this.CurrentLine.Qty = value;
            }
        }
        public decimal UnitPrice
        {
            get
            {
                return this.CurrentLine.UnitPrice;
            }
            set
            {
                this.CurrentLine.UnitPrice = value;
            }
        }
        public int Factor
        {
            get
            {
                return this.CurrentLine.Factor;
            }
            set
            {
                this.CurrentLine.Factor = value;
            }
        }
        public string LineRef
        {
            get
            {
                return this.CurrentLine.LineRef;
            }
            set
            {
                this.CurrentLine.LineRef = value;
            }
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