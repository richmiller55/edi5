using System;
using System.Collections.Generic;
using System.Text;


namespace FastLoad
{
    public class ShipToXman
    {
        public string dataSet;
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.Customer customerObj;
        Epicor.Mfg.BO.CustomerDataSet ds;
        int newShipto = 0;

        public ShipToXman()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8301", Epicor.Mfg.Core.Session.LicenseType.Default);
            customerObj = new Epicor.Mfg.BO.Customer(objSess.ConnectionPool);

            //
            //
        }
        public bool ShipToExists(ShipTo st)
        {
            bool result = true;
            string message = "does exception throw";
            try
            {
                Epicor.Mfg.BO.CustomerDataSet custDs;
                custDs = customerObj.GetShipTo(st.CustId, st.ShipToId);
                Epicor.Mfg.BO.CustomerDataSet.ShipToRow shipToRow =
                    (Epicor.Mfg.BO.CustomerDataSet.ShipToRow)custDs.ShipTo.Rows[0];
                string shipTo = shipToRow.ShipToNum;
            }
            catch (Exception e)
            {
                this.NewShipTo += 1;
                message = e.Message;
                result = false;
            }
            return result;
        }
        public int getCustNoFromID(Epicor.Mfg.BO.Customer custObj, string custID)
        {
            Epicor.Mfg.BO.CustomerDataSet ds = new Epicor.Mfg.BO.CustomerDataSet();
            ds = custObj.GetByCustID(custID);
            Epicor.Mfg.BO.CustomerDataSet.CustomerRow row = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)ds.Customer.Rows[0];
            return (int)row.CustNum;
        }
        private Boolean AddressUpdateNeeded(ShipTo st,Epicor.Mfg.BO.CustomerDataSet.ShipToRow shipToRow)
        {
            Boolean result = false;
            if (st.Address1.CompareTo(shipToRow.Address1) != 0)
            {
                result = true;
                return result;
            }
            if (st.Address2.CompareTo(shipToRow.Address2) != 0)
            {
                result = true;
                return result;
            }
            if (st.Address3.CompareTo(shipToRow.Address3) != 0)
            {
                result = true;
                return result;
            }
            if (st.City.CompareTo(shipToRow.City) != 0)
            {
                result = true;
                return result;
            }
            if (st.State.CompareTo(shipToRow.State) != 0)
            {
                result = true;
                return result;
            }
            if (st.Zip.CompareTo(shipToRow.ZIP) != 0)
            {
                result = true;
                return result;
            }
            return result;
        }
        public void UpdateShipTo(ShipTo st)
        {
            try
            {
                Epicor.Mfg.BO.CustomerDataSet custDs;
                custDs = customerObj.GetShipTo(st.CustId, st.ShipToId);
                Epicor.Mfg.BO.CustomerDataSet.ShipToDataTable shipToTable = custDs.ShipTo;

                Epicor.Mfg.BO.CustomerDataSet.ShipToRow shipToRow =
                    (Epicor.Mfg.BO.CustomerDataSet.ShipToRow)shipToTable.Rows[0];
                if (this.AddressUpdateNeeded(st, shipToRow))
                {
                    shipToRow.Name = st.Name;
                    shipToRow.Address1 = st.Address1;
                    shipToRow.Address2 = st.Address2;
                    shipToRow.Address3 = st.Address3;
                    shipToRow.City = st.City;
                    shipToRow.State = st.State;
                    shipToRow.ZIP = st.Zip;
                    shipToRow.Country = st.Country;
                    shipToRow.CountryNum = st.CountryNo;
                    shipToRow.TerritoryID = "10";
                    shipToRow.ShipViaCode = st.ShipVia;
                    shipToRow.Company = "CA";
                    shipToRow.Country = "US";

                    customerObj.Update(custDs);
                }
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
        public void addShipTo(ShipTo st)
        {
            string message = "Customer found";
            try
            {
                ds = customerObj.GetByCustID(st.CustId);
                Epicor.Mfg.BO.CustomerDataSet.CustomerRow CustRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)ds.Customer.Rows[0];
                int custNum = CustRow.CustNum;
                customerObj.GetNewShipTo(ds, custNum);
                        
                int count = ds.ShipTo.Rows.Count;
                int index = count - 1;
                Epicor.Mfg.BO.CustomerDataSet.ShipToRow ShipToRow = (Epicor.Mfg.BO.CustomerDataSet.ShipToRow)ds.ShipTo.Rows[index];
                ShipToRow.ShipToNum = st.ShipToId;

                ShipToRow.Address1 = st.Address1;
                ShipToRow.Address2 = st.Address2;
                ShipToRow.Address3 = st.Address3;

                ShipToRow.City     = st.City;
                ShipToRow.State    = st.State;
                ShipToRow.ZIP      = st.Zip;
                ShipToRow.Country = st.Country;
                ShipToRow.CountryNum = st.CountryNo;
                ShipToRow.TerritoryID = "10";
                ShipToRow.Name = st.Name;
                ShipToRow.ShipViaCode = st.ShipVia;

                ShipToRow.PhoneNum = st.Phone;
                ShipToRow.Company = "CA";
                message = "Shipto Added OK";
                customerObj.Update(ds);
                ds.Clear();
            }
                catch (Exception e)
            {
                message = e.Message;
            }
        }
        public int NewShipTo
        {
            get
            {
                return newShipto;
            }
            set
            {
                newShipto = value;
            }
        }
 
     }
}
