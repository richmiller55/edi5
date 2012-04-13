using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
// using System.Web.UI;

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
        // DeferEntry deferEntry;  // need to write this
        // CouchDBLogger cdbLog;   // what the hell did you do

        Epicor.Mfg.Core.Session session;
        AppVarsMgr vars;
        public OrderQueue(Epicor.Mfg.Core.Session vanSession)
        {
            this.session = vanSession;
            vars = new AppVarsMgr();
            string watchPath = vars.WatchDirectory;
            
            /* not lots of exta sessions, just one,
            // should be able to log itself, the logger
            // maybe should hang around, retry tear down
            // sounds like a more dependable method, always
            // retry. 
            // you need a new order every time for sure, 
            // 
            */

            string[] filePaths = Directory.GetFiles(watchPath);
            bool AllOk = true;
            WriteSalesOrder writer = new WriteSalesOrder();
            foreach (string fileName in filePaths)
            {
                try
                {
                    XmlReader reader = new XmlReader(fileName);
                    SalesOrder salesOrder = reader.GetSalesOrder();
                    writer.ProcessOrder(salesOrder);
                
                }
                catch (Exception e)
                {
                    string message = e.Message;
                    AllOk = false;
                }

                if (AllOk)
                {
                    MoveFile(fileName, AllOk);
                }
                else
                {
                    MoveFile(fileName, false);
                }
            }
        }
        private void MoveFile(string fullName,bool AllOk)
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
            string dumpPath = vars.DeferDirectory;
            if (AllOk)
            {
                dumpPath = vars.ProcessedDirectory;
            }
            File.Move(fullName, dumpPath + "\\" + newFileName);
            string message = "New File Name " + newFileName;

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



