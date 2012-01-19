using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.UI;

namespace InvBox
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
    class UPSReader
    {
        string fullPath = @"D:\users\UPS";
        string dumpPath = @"D:\users\rich\ProcessedFrt";
        StreamReader tr;
        ShipMgr m_shipMgr;
        string packSlipStr;
        ExReport report;
        int messCount = 100;
        string messPrefix = "UpsReader_";

        Epicor.Mfg.Core.Session session;
        public UPSReader(Epicor.Mfg.Core.Session vanSession, ExReport report)
        {
            this.session = vanSession;
            this.report = report;
            this.m_shipMgr = new ShipMgr(this.report);
            // insert try catch block here
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
        public ShipMgr GetShipMgr() { return m_shipMgr; }

        private void MoveFile(string fullName)
        {
            string fileName = Path.GetFileName(fullName);
            string prefix = Path.GetFileNameWithoutExtension(fullName);

            DateTime now = DateTime.Now;
            string date = now.Year.ToString("0000") + now.Month.ToString("00") + now.Day.ToString("00");
            string time = now.Hour.ToString("00") + now.Minute.ToString("00") + now.Second.ToString("00");
            string newFileName = prefix + "_" + date + "_" + time + ".txt";
            File.Move(fullName, dumpPath + "\\" + newFileName);
            string message = "New File Name " + newFileName;
            report.AddMessage(GetNextMessageKey(), message);
        }
        private void InvoiceShipment()
        {
            CAInvoice cainv = new CAInvoice(this.session,this.report, "RLM85", this.packSlipStr, GetShipMgr());
        }
        private void PickCarrier()
        {
            string linePre = "";
            
            while ((linePre = tr.ReadLine()) != null)
            {
                string line = linePre.Replace("\"", "");
                string[] split = line.Split(new Char[] { ',' });

                if (split[0].CompareTo("") == 0) continue;
                string trackingNumber = split[0];
                if (trackingNumber.Substring(0, 1).CompareTo("4") == 0)
                {
                    ProcessFedEx(split);
                }
                else if (trackingNumber.Substring(0, 2).CompareTo("1Z") == 0)
                {
                    ProcessUPS(split);
                }
            }
        }
        private void ProcessUPS(string[] split)
        {
            this.packSlipStr = split[(int)ups.packSlipNo];
            string trackingNo = split[(int)ups.trackingNo];

            int packSlip = Convert.ToInt32(packSlipStr);
            string shipDateStr = split[(int)ups.shipDate];
            System.DateTime shipDate = new DateTime();

            try
            {
                shipDate = convertStrToDate(shipDateStr);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                report.AddMessage(GetNextMessageKey(), ex.Message);
            }
            string serviceClass = split[(int)ups.serviceClass];

            int orderNo = 656565;  // fix this thing
            string weightStr = split[(int)ups.weight];
            decimal weight = Convert.ToDecimal(weightStr);

            string chargeStr = split[(int)ups.charge];
            decimal charge = Convert.ToDecimal(chargeStr);
            decimal zero = 0.0M;
            int result = charge.CompareTo(zero);
            if (result == 0)
            {
                string message = "Zero Charge Freight ";
                report.AddMessage(GetNextMessageKey(), message);
            }
            string tranType = split[(int)ups.tranType];
            if (tranType.CompareTo("N") == 0)
            {
                m_shipMgr.AddShipmentLine(packSlip, trackingNo, shipDate,
                            serviceClass, orderNo, weight, charge);
                string message =  "Pack added " + packSlip.ToString();
                report.AddMessage(GetNextMessageKey(),message);
            }
            else
            {
                m_shipMgr.RemoveShipmentLine(packSlip, trackingNo);
                string message = "Void Transaction Line - PackSlip " + packSlip.ToString(); 
                report.AddMessage(GetNextMessageKey(),message);
            }
        }
        private void ProcessFedEx(string[] split)
        {
            this.packSlipStr = split[(int)fedEx.packSlipNo];
            string trackingNo = split[(int)fedEx.trackingNo];
            int result = packSlipStr.CompareTo("po"); // to do modifiy for ups heading

            int packSlip = Convert.ToInt32(packSlipStr);
            string shipDateStr = split[(int)fedEx.shipDate];

            System.DateTime shipDate = convertStrToDate(shipDateStr);
            string serviceClass = split[(int)fedEx.serviceClass];

            // string orderStr = split[(int)ups.orderNo];
            // int orderNo = 0;
            // if (orderStr.Length > 0)
            //{
            //  orderNo = Convert.ToInt32(orderStr);
            //}
            int orderNo = 656565;  // fix this thing
            string weightStr = split[(int)fedEx.weight];
            decimal weight = Convert.ToDecimal(weightStr);

            string chargeStr = split[(int)fedEx.charge];
            decimal charge = Convert.ToDecimal(chargeStr);
            decimal zero = 0.0M;
            result = charge.CompareTo(zero);
            // if (result == 0) continue;  // if collect then do not process

            string tranType = split[(int)fedEx.tranType];
            if (tranType.CompareTo("N") == 0)
            {
                m_shipMgr.AddShipmentLine(packSlip, trackingNo, shipDate,
                            serviceClass, orderNo, weight, charge);
            }
            else
            {
                m_shipMgr.RemoveShipmentLine(packSlip, trackingNo);
            }
            //shipMgr.ShipmentComplete(); // is this step needed?
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



