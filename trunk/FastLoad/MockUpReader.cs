using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FastLoad
{
    class MockUpReader
    {
        SalesOrder order;
        public MockUpReader()
        {
            // nothing
          
        }
        public SalesOrder GetSalesOrder()
        {
            return this.order;
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
        public SalesOrder FillInOrder()
        {
            order = new SalesOrder();
            order.PoNo = "1924576596";
            order.OrderDate = convertStrToDate("20150120");
            order.NeedByDate = convertStrToDate("20150121");
            order.RequestDate = convertStrToDate("20150121");
            order.ShipNoLaterDate = convertStrToDate("20150121");
            order.ShortChar02 = "This shipment completes your order.";
            order.ShortChar03 = "";
            order.ShortChar04 = "Adam Talle";
            order.ShortChar05 = "7000 Target Parkway, NCC-03622";
            order.ShortChar06 = "Brooklyn Park" + "\t" + "MN" + "\t" + "55445" + "\t" + "US";
            order.ShortChar07 = "Adam Talle";
            order.ShortChar08 = "7000 Target Parkway, NCC-03622";
            order.ShortChar09 = "Brooklyn Park" + "\t" + "MN" + "\t" + "55445" + "\t" + "US";
            order.ShortChar10 = "46532590";
            order.Character01 = "1924576596";
            order.Date05 = convertStrToDate("20150120");
            order.ShipVia = "MGPP";
            order.CustomerID = "15220";
            order.ShipToNum = "";
            order.OrderLineNum = 1;
            order.OrderQty = 8;
            order.UnitPrice = 1.11M;
            order.Upc = "123456789123";
            order.CustomerPart = "12345678";
            order.LineShortChar02 = "12345678";
            order.LineShortChar03 = "123";
            order.LineShortChar04 = "111-22-3333";
            order.LineShortChar05 = "TEST ITEM 1";
            order.LineShortChar06 = "Mail In or Store";
            order.postLine();

            order.OrderLineNum = 2;
            order.OrderQty = 4;
            order.UnitPrice = 2.22M;
            order.Upc = "456789123456";
            order.CustomerPart = "90123456";
            order.LineShortChar02 = "90123456";
            order.LineShortChar03 = "456";
            order.LineShortChar04 = "444-55-6666";
            order.LineShortChar05 = "TEST ITEM 2";
            order.LineShortChar06 = "Mail In or Store";
            order.postLine();
            return order;
        }
    }
}