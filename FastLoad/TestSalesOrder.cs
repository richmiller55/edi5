using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace FastLoad
{
    class TestSalesOrder
    {
        /*
         * Training   8311
         * Pilot      8331
         * production 8301
         * test       8321
         */
        protected Epicor.Mfg.Core.Session objSess;
        protected Epicor.Mfg.BO.Customer customerObj;
        protected Epicor.Mfg.BO.CustomerDataSet ds;
        AppVarsMgr v;
        public TestSalesOrder()
        {
            v = new AppVarsMgr();

            objSess = new Epicor.Mfg.Core.Session(v.User, v.Password,
                v.ServerUrl + ":" + v.DataPort,
                Epicor.Mfg.Core.Session.LicenseType.Default);

        }
        protected string getPartDescr(string partNumber)
        {
            Epicor.Mfg.BO.PartDataSet partDs;
            Epicor.Mfg.BO.Part partObj = new Epicor.Mfg.BO.Part(objSess.ConnectionPool);
            string descr = "null";
            if (partObj.PartExists(partNumber))
            {
                partDs = new Epicor.Mfg.BO.PartDataSet();
                partDs = partObj.GetByID(partNumber);
                Epicor.Mfg.BO.PartDataSet.PartRow row =
                       (Epicor.Mfg.BO.PartDataSet.PartRow)partDs.Part.Rows[0];
                descr = row.PartDescription;
            }
            return descr;
        }
        public void ProcessOrder(SalesOrder ord)
        {
            customerObj = new Epicor.Mfg.BO.Customer(objSess.ConnectionPool);
            string customerId = ord.CustomerID;
            ds = customerObj.GetCustomer(customerId);
            Epicor.Mfg.BO.CustomerDataSet.CustomerRow row = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)ds.Customer.Rows[0];
            int custNum = (int)row.CustNum;

            Epicor.Mfg.BO.SalesOrder salesOrderObj;
            salesOrderObj = new Epicor.Mfg.BO.SalesOrder(objSess.ConnectionPool);
            Epicor.Mfg.BO.SalesOrderDataSet soDs;
            soDs = new Epicor.Mfg.BO.SalesOrderDataSet();
            salesOrderObj.GetNewOrderHed(soDs);
            Epicor.Mfg.BO.SalesOrderDataSet.OrderHedRow hedRow = (Epicor.Mfg.BO.SalesOrderDataSet.OrderHedRow)soDs.OrderHed.Rows[0];
            hedRow.CustomerCustID = customerId;
            hedRow.CustNum = custNum;
            string shipTo = ord.ShipToNum;
            // my expectation here is that ord should return 0
            // unless it is set. ord is so primitive bring the 
            // xml code over, of course
            hedRow.ShipToNum = shipTo;
            hedRow.BTCustNum = custNum;
            hedRow.TermsCode = "N30";
            hedRow.Company = "CA";
            hedRow.ShortChar01 = "EDI";
            hedRow.OrderDate = ord.OrderDate;
            hedRow.ShortChar02 = ord.ShortChar02;
            hedRow.ShortChar03 = ord.ShortChar03;
            hedRow.ShortChar04 = ord.ShortChar04;
            hedRow.ShortChar05 = ord.ShortChar05;
            hedRow.ShortChar06 = ord.ShortChar06;
            hedRow.ShortChar07 = ord.ShortChar07;
            hedRow.ShortChar08 = ord.ShortChar08;
            hedRow.ShortChar09 = ord.ShortChar09;
            hedRow.ShortChar10 = ord.ShortChar10;

            hedRow.PONum = ord.PoNo;
            hedRow.OrderDate = ord.OrderDate;
            hedRow.NeedByDate = ord.NeedByDate;
            hedRow.RequestDate = ord.RequestDate;

            hedRow.Date01 = ord.ShipNoLaterDate;
            hedRow.ShipToNum = ord.ShipToNum;
            hedRow.ShipViaCode = ord.ShipVia;
            string outMessage = "PO= " + ord.PoNo.ToString();
            outMessage += "  Order Date " + ord.OrderDate.ToString();
            Console.WriteLine(outMessage);
            // hedRow.ShipViaCode = "UGND";
            bool result = true;

            string message;
            try
            {
                salesOrderObj.Update(soDs);
            }
            catch (Exception e)
            {
                // header did not post
                message = e.Message;
                Console.WriteLine(message.ToString() + " ship to: " + ord.ShipToNum.ToString());
                result = false;
            }
            if (result)
            {
                int orderNum = hedRow.OrderNum;
                int rowNumber = 0;

                foreach (OrderLine line in ord.lines)
                {
                    salesOrderObj.GetNewOrderDtl(soDs, orderNum);
                    Epicor.Mfg.BO.SalesOrderDataSet.OrderDtlRow dtlRow =
                        (Epicor.Mfg.BO.SalesOrderDataSet.OrderDtlRow)soDs.OrderDtl.Rows[rowNumber];
                    rowNumber++;

                    dtlRow.OrderQty = line.OrderQty;
                    string partNumber = line.Upc;
                    dtlRow.PartNum = partNumber;
                    // dtlRow.RevisionNum = "1";
                    string partDescr = getPartDescr(partNumber);
                    dtlRow.LineDesc = partDescr;
                    string custPart = line.CustomerPart;
                    if (!line.CustomerPart.Equals(""))
                    {
                        dtlRow.XPartNum = line.CustomerPart;
                    }
                    dtlRow.ShortChar02 = line.ShortChar02;
                    dtlRow.ShortChar03 = line.ShortChar03;
                    dtlRow.ShortChar04 = line.ShortChar04;
                    dtlRow.ShortChar05 = line.ShortChar05;
                    dtlRow.ShortChar06 = line.ShortChar06;
                    dtlRow.ShortChar07 = line.ShortChar07;
                    dtlRow.Character01 = line.Character01;

                    dtlRow.RowMod = "A";
                    dtlRow.UnitPrice = line.UnitPrice;
                    dtlRow.DocUnitPrice = line.UnitPrice;
                    string lineMessage = " line " + rowNumber.ToString();
                    lineMessage += "  " + partDescr;
                    lineMessage += "  " + line.OrderQty.ToString();
                    lineMessage += "  " + line.UnitPrice.ToString();
                    Decimal lineTotal = line.OrderQty * line.UnitPrice;
                    lineMessage += "  " + lineTotal.ToString();
                    Console.WriteLine(lineMessage);

                    message = "OK Line";
                    try
                    {
                        salesOrderObj.Update(soDs);
                    }
                    catch (Exception e)
                    {
                        message = e.Message;
                        MessageBox.Show(message.ToString(),
                            "Sales Order Line Did not Post.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }

                }
            }
        }
    }
}