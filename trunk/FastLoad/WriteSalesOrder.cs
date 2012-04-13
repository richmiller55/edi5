using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace FastLoad
{
    class WriteSalesOrder
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
        public WriteSalesOrder()
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
            hedRow.PONum = ord.PoNo;
            hedRow.OrderDate = ord.OrderDate;
            hedRow.NeedByDate = ord.NeedByDate;
            hedRow.RequestDate = ord.RequestDate;
            hedRow.Date01 = ord.ShipNoLaterDate;
            hedRow.ShipToNum = ord.ShipToNum;
            hedRow.ShipViaCode = ord.ShipVia;
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
                MessageBox.Show(message.ToString() + " ship to: " + ord.ShipToNum.ToString(),
                    "Sales Order Header Did not Post.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                result = false;
            }
            if (result) {

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
                    dtlRow.RowMod = "A";
                    dtlRow.UnitPrice = line.UnitPrice;
                    dtlRow.DocUnitPrice = line.UnitPrice;

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
    class WriteShipToOrder : WriteSalesOrder
    {
        new public void ProcessOrder(SalesOrder ord)
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
            hedRow.BTCustNum = custNum;
            hedRow.TermsCode = "N30";
            hedRow.Company = "CA";
            hedRow.ShortChar01 = "EDI";

            hedRow.PONum = ord.PoNo;
            // ord has to ensure that these dates are valid 
            hedRow.OrderDate   = ord.OrderDate;
            hedRow.NeedByDate  = ord.NeedByDate;
            hedRow.RequestDate = ord.RequestDate;
            hedRow.Date01      = ord.ShipNoLaterDate;
            hedRow.ShipToNum   = ord.ShipToNum;
            hedRow.ShipViaCode = ord.ShipVia;
   
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
              MessageBox.Show(message.ToString() + " ship to: " + ord.ShipToNum.ToString(),
                    "Sales Order Header Did not Post.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
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
                    
                    string partNumber = line.Upc;
                    dtlRow.PartNum = partNumber;
                    
                    string partDescr = getPartDescr(partNumber);
                    dtlRow.LineDesc = partDescr;
                    dtlRow.RowMod = "A";
                    dtlRow.SellingFactor = line.SellingFactor;
                    dtlRow.SellingFactorDirection = line.SellingFactorDirection;
                    dtlRow.SellingQuantity = line.SellingQty;
                    dtlRow.OrderQty = line.OrderQty;
                    dtlRow.UnitPrice = line.UnitPrice;
                    dtlRow.DocUnitPrice = line.UnitPrice;

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
