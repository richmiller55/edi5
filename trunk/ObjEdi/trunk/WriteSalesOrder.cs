using System;
// using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;

namespace ObjEdi
{
    class WriteSalesOrder
    {
        /*
         * Training   8311
         * Pilot      8331
         * production 8301
         * test       8321
         */
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.Customer customerObj;
        Epicor.Mfg.BO.CustomerDataSet ds;
   
        public WriteSalesOrder()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8321", Epicor.Mfg.Core.Session.LicenseType.Default);
        }
        public string getPartDescr(string partNumber)
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
        public void ProcessOrder(Order ord)
        {
            bool processOK = true;
            if (ord.ValidLines.Equals(0))
            {
                processOK = false;
            }
            if (processOK)
            {
                customerObj = new Epicor.Mfg.BO.Customer(objSess.ConnectionPool);

                ds = customerObj.GetCustomer(ord.CustomerID);
                Epicor.Mfg.BO.CustomerDataSet.CustomerRow row = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)ds.Customer.Rows[0];
                int custNum = (int)row.CustNum;
                Epicor.Mfg.BO.SalesOrder salesOrderObj;
                salesOrderObj = new Epicor.Mfg.BO.SalesOrder(objSess.ConnectionPool);
                Epicor.Mfg.BO.SalesOrderDataSet soDs = new Epicor.Mfg.BO.SalesOrderDataSet();
                salesOrderObj.GetNewOrderHed(soDs);
                Epicor.Mfg.BO.SalesOrderDataSet.OrderHedRow hedRow = (Epicor.Mfg.BO.SalesOrderDataSet.OrderHedRow)soDs.OrderHed.Rows[0];

                hedRow.CustomerCustID = ord.CustomerID;
                hedRow.CustNum = custNum;
                hedRow.BTCustNum = custNum;
                hedRow.Company = "CA";

                hedRow.PONum = ord.PONumber;
                hedRow.OrderDate = ord.OrderDate;
                hedRow.RequestDate = ord.RequestDate;
                hedRow.NeedByDate = ord.NeedByDate;
                hedRow.ShipToNum = ord.ShipTo;
                hedRow.TermsCode = ord.TermsCode;
                hedRow.ShipViaCode = ord.ShipVia;
                hedRow.ShortChar01 = "EDI";
                string message = "customerOK";
                bool orderHedOK = true;
                try
                {
                    salesOrderObj.Update(soDs);
                }
                catch (Exception e)
                {
                    message = e.Message;
                    orderHedOK = false;
                }
                int orderNum = hedRow.OrderNum;
                int rowNumber = 0;
                if (orderHedOK)
                {
                    foreach (OrderLine line in ord.lines)
                    {
                        salesOrderObj.GetNewOrderDtl(soDs, orderNum);
                        Epicor.Mfg.BO.SalesOrderDataSet.OrderDtlRow dtlRow =
                            (Epicor.Mfg.BO.SalesOrderDataSet.OrderDtlRow)soDs.OrderDtl.Rows[rowNumber];
                        rowNumber++;

                        dtlRow.PartNum = line.UPC;
                        int rev = line.Rev;
                        if (rev > 0)
                        {
                            dtlRow.RevisionNum = rev.ToString();
                        }
                        // just NVI
                        dtlRow.SalesUM = "CA";
                        dtlRow.SellingFactor = line.Factor;
                        dtlRow.SellingQuantity = line.Qty;
                        dtlRow.OrderQty = line.Qty * line.Factor;
                        // end Just NVI
                        dtlRow.LineDesc = getPartDescr(line.UPC);
                        dtlRow.RowMod = "A";
                        dtlRow.UnitPrice = line.UnitPrice;
                        dtlRow.DocUnitPrice = line.UnitPrice;
                        try
                        {
                            salesOrderObj.Update(soDs);
                        }
                        catch (Exception e)
                        {
                            message = e.Message;
                            orderHedOK = false;
                        }

                    }
                }
            }
        }
    }
 }