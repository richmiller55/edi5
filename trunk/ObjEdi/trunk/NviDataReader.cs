using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace ObjEdi
{
    public enum col
    {
        customerId,
        upc,
        rev,
        shipTo,
        units,
        poNo,
        tranDate,
        startDate,
        price,
        factor,
        filler
    }
    class NviDataReader
    {
        string dir = "D:/users/rich/data/NVI/";
        string file = "xmasOrder20081217.txt";
        StreamReader tr;
        Order ord;
	    string customerId = "1124";
	    string shipVia = "F2DP";
	    string crTerms = "N30";

        public NviDataReader()
        {
            ord = new Order();
            tr = new StreamReader(dir + file);
            processFile();
        }
//                int result = orderNo.CompareTo("543367");
//                if (result < 0) continue;

        void processFile()
        {
            string line = "";
            string lastStoreNo = "first";
            bool notFirstTime = false;
            WriteSalesOrder writer = new WriteSalesOrder();
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                
                string storeNo = split[(int)col.shipTo];
                // int result = storeNo.CompareTo("391");
                // if (result == 0) continue;
                if (lastStoreNo != storeNo && notFirstTime) 
                {
                    writer.ProcessOrder(ord);
                    ord = new Order();
                    notFirstTime = true;
                }
                ord.setCustomerId(customerId);
                ord.setShipTo(split[(int)col.shipTo]);
                ord.setRequestDate(split[(int)col.startDate]);
                ord.setOrderDate(split[(int)col.tranDate]);
        		string poNo = split[(int)col.poNo];
                ord.setPoNum(poNo);
                ord.setShipVia(shipVia);
                ord.setTerms(crTerms);
                
                ord.setUpc(split[(int)col.upc]);
                ord.setOrderQty(split[(int)col.units]);
                ord.setUnitPrice(split[(int)col.price]);
                ord.setFactor(split[(int)col.factor]);
                ord.postLine();
                lastStoreNo = storeNo;
                notFirstTime = true;
            }
        }
    }
}



