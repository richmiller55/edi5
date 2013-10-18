using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace ObjEdi
{
    class AcademyReader
    {
      string dir = "D:/users/EDI/SteinMart/InboxOrders/";
      StreamReader tr;
        Order ord;
        // Hashtable partXref;
        // Hashtable shipViaHash;
        string crTerms = "N30";
        string shipVia = "FGRB";
        string custId = "269715";
        public AcademyReader()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            FileInfo[] fileListInfo = dirInfo.GetFiles();
            foreach (FileSystemInfo fsi in fileListInfo)
            {
                string file = fsi.FullName;
                tr = new StreamReader(file);
                processFile();
            }
        }
        void processFile()
        {
            int currentTransactionNo = 0;
            ord = new Order();
            WriteSalesOrder writer = new WriteSalesOrder();
            string line;
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string customerId = this.custId;
                int TransactionControlNo  = System.Convert.ToInt32( split[(int)acad.TransControlNo]);
                
                // we will worry about store in a second, 
                // here this transaction thing will control the action
                // int storeInt = Convert.ToInt32(storeNoPre);
                // string storeNo = storeInt.ToString();   

                if (currentTransactionNo != TransactionControlNo && currentTransactionNo != 0) 
                {
                    writer.ProcessOrder(ord);
                    ord = new Order();
                }
                currentTransactionNo = TransactionControlNo;
                string zipCode = split[(int)acad.ShipToZipcode];
               
                if (zipCode.Equals("77449"))
                {
                    ord.ShipTo = "1";
                    // verify.state = TX
                    // verify.city = Katy

                }
                else if (zipCode.Equals("31044"))
                {
                    ord.ShipTo = "2";
                    // verify.state = GA
                    // verify.city = Jeffersonville
                }

                ord.CustomerID = customerId;  // hardcoded from above
                ord.RequestDateStr = split[(int)acad.RequestShipDate];
                ord.NeedByDateStr  = split[(int)acad.CancelAfter];
                ord.OrderDateStr   = split[(int)acad.PODate];
                ord.PONumber = split[(int)acad.PONumber];
                string shipInstruc = split[(int)acad.ShippingInstr];
                ord.ShipVia = shipVia;
                ord.TermsCode = crTerms;
                bool processLine = true;
                string leadingZeroUPC = split[(int)acad.UPCCode];
                ord.UPC = leadingZeroUPC.Substring(2);
                ord.OrderQty = Convert.ToDecimal(split[(int)acad.QtyOrdered]);
                ord.UnitPrice = Convert.ToDecimal(split[(int)acad.UnitPrice]);
                if (processLine)
                {
                    ord.postLine();
                }
            }
            writer.ProcessOrder(ord);
            ord = new Order();
        }
        string getShipVia(string store)
        {
            string shipVia = "UGND";
            return shipVia;
        }
    }
}



