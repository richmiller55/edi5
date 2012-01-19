using System;
using System.Collections;
using System.Collections.Generic;

namespace FastLoad
{
    public class ShipToOrder : Order
    {
        public ShipToOrder()
        {
            lines = new ArrayList();
            currentLine = new ShipToOrderLine();
        }
        public void setShipToLine(string shipTo)
        {
            currentLine.setShipTo(shipTo);
        }
        public string getPoNum() { return PoNo + ":" + ShipToId; }
        public new void postLine()
        {
            lines.Add(currentLine);
            currentLine = new ShipToOrderLine();
        }
    }
    public class ShipToOrderLine : OrderLine
    {
        private string shipToId;
        public string getShipTo() { return shipToId; }
        public void setShipTo(string shipTo) { shipToId = shipTo; }
    }
}
