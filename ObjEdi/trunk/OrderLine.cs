using System;
using System.Collections.Generic;
using System.Text;

namespace ObjEdi
{
    public class OrderLine
    {
        private int     m_lineNumber;
        private string  m_customerPart;
        private string  m_ourPart;
        private string  m_upc;
        private int     m_rev;
        private decimal m_orderQty;
        private decimal m_unitPrice;
        private int m_factor;
        private string  m_lineRef;

        public OrderLine()
        {
            this.m_factor = 1;
            this.UPC = "";
            this.OurPart = "";
            this.Qty = 0.0M;
            this.UnitPrice = 0.0M;
        }
        public string CustomerPart
        {
            get
            {
                return m_customerPart;
            }
            set
            {
                m_customerPart = value;
            }
        }
        public string OurPart
        {
            get
            {
                return m_ourPart;
            }
            set
            {
                m_ourPart = value;
            }
        }
        public string UPC
        {
            get
            {
                return m_upc;
            }
            set
            {
                m_upc = value;
            }
        }
        public int LineNo
        {
            get
            {
                return m_lineNumber;
            }
            set
            {
                m_lineNumber = value;
            }
        }
        public int Rev
        {
            get
            {
                return m_rev;
            }
            set
            {
                m_rev = value;
            }
        }
        public int Factor
        {
            get
            {
                return m_factor;
            }
            set
            {
                m_factor = value;
            }
        }
        public decimal Qty
        {
            get
            {
                return m_orderQty;
            }
            set
            {
                m_orderQty = value;
            }
        }
        public decimal UnitPrice
        {
            get
            {
                return m_unitPrice;
            }
            set
            {
                m_unitPrice = value;
            }
        }
        public string LineRef
        {
            get
            {
                return m_lineRef;
            }
            set
            {
                m_lineRef = value;
            }
        }
        // Convert.ToDecimal(qty);

    }
}
