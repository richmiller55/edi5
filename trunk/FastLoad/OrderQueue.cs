using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.UI;

namespace FastLoad
{
    public enum fedEx
    {
        trackingNo,
        packSlipNo,
        shipDate,
        serviceClass,
        orderNo,
        weight,
        charge,
        tranType
    }
    public enum ups
    {
        trackingNo,
        packSlipNo,
        unknown,
        shipDate,
        serviceClass,
        weight,
        charge,
        tranType,
        xnumber,
        funnyInt,
        frtBilledTo
    }
    class OrderQueue
    {
        string nowPath = @"I:\users\edi\FastLoad\now";
        string dumpPath = @"D:\users\edi\FastLoad\dump";
        StreamReader tr;
        DeferEntry deferEntry;  // need to write this
        CouchDBLogger cdbLog;   // what the hell did you do

        Epicor.Mfg.Core.Session session;

        public OrderQueue(Epicor.Mfg.Core.Session vanSession)
        {
            this.session = vanSession;
            /* not lots of exta sessions, just one,
            // should be able to log itself, the logger
            // maybe should hang around, retry tear down
            // sounds like a more dependable method, always
            // retry. 
            // you need a new order every time for sure, 
            // 
            */

            string[] filePaths = Directory.GetFiles(this.fullPath);
            bool AllOk = true;
            foreach (string fileName in filePaths)
            {
                try
                {
                    tr = new StreamReader(fileName);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                    report.AddMessage(GetNextMessageKey(), message);
                    AllOk = false;
                }
                if (AllOk)
                {
                    PickCarrier();
                    tr.Close();
                }
                MoveFile(fileName);
                if (AllOk)
                {
                    InvoiceShipment();
                }
            }
        }
        private void MoveFile(string fullName)
        {
            string fileName = Path.GetFileName(fullName);
            string prefix = Path.GetFileNameWithoutExtension(fullName);

            DateTime now = DateTime.Now;
            string date = now.Year.ToString("0000") + 
                          now.Month.ToString("00") + 
                          now.Day.ToString("00");
            string time = now.Hour.ToString("00") + 
                          now.Minute.ToString("00") + 
                          now.Second.ToString("00");
            string newFileName = prefix + "_" + date + "_" + time + ".txt";
            File.Move(fullName, dumpPath + "\\" + newFileName);
            string message = "New File Name " + newFileName;
            report.AddMessage(GetNextMessageKey(), message);
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
        public string GetNextMessageKey()
        {
            string messKey = this.messPrefix + this.messCount.ToString();
            this.messCount += 1;
            return messKey;
        }
    }
}



