using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using System.IO;

namespace ObjEdi
{
    public enum pvcol
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
    class PearlBlanketReader
    {
        string dir = "D:/users/rich/data/sn/";
        string file = "pearleOrder_20090713.txt";
        StreamReader tr;
        Order ord;
	    
	    string shipVia = "FGRB";
	    string crTerms = "N30";

        public PearlBlanketReader()
        {
            ord = new Order();
            tr = new StreamReader(dir + file);
            processFile();
        }
        void processFile()
        {
            string line = "";
            string lastStoreNo = "first";

            bool notFirstTime = false;
            WriteSalesOrder writer = new WriteSalesOrder();
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string customerId = split[(int)pvcol.customerId];
                string storeNo = split[(int)pvcol.shipTo];
                if (lastStoreNo != storeNo && notFirstTime) 
                {
                    writer.ProcessOrder(ord);
                    ord = new Order();
                    notFirstTime = true;
                }
                ord.setCustomerId(customerId);
                ord.setShipTo(split[(int)pvcol.shipTo]);
                ord.setRequestDate(split[(int)pvcol.startDate]);
                ord.setNeedByDate(split[(int)pvcol.startDate]);
                ord.setOrderDate(split[(int)pvcol.tranDate]);
        		string poNo = split[(int)pvcol.poNo];

                ord.setPoNum(poNo);
                ord.setShipVia(shipVia);
                ord.setTerms(crTerms);
                
                ord.setUpc(split[(int)pvcol.upc]);
                ord.setRev(split[(int)pvcol.rev]);
                ord.setOrderQty(split[(int)pvcol.units]);
                ord.setUnitPrice(split[(int)pvcol.price]);

                ord.postLine();
                lastStoreNo = storeNo;
                notFirstTime = true;
            }
            writer.ProcessOrder(ord);
        }
    }
}
