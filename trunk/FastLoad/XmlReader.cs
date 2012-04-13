using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FastLoad
{
    class XmlReader
    {
        public string fileName;
        string currentElement;
        SalesOrder order;
        public XmlReader(string fileName)
        {
            this.fileName = fileName;
            this.order = new SalesOrder();
            ReadTheXml();
        }
        public SalesOrder GetSalesOrder()
        {
            return this.order;
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
        public void ReadTheXml()
        {
            XmlTextReader reader = new XmlTextReader(this.fileName);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        Console.Write("<" + reader.Name);
                        currentElement = reader.Name;
                        Console.WriteLine(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        switch (currentElement)
                        {
                            case "PONum":
                                order.PoNo = reader.Value;
                                break;
                            case "ShipToNum":
                                order.ShipToNum = reader.Value;
                                break;
                            case "ShipTo":
                                order.ShipToNum = reader.Value;
                                break;
                            case "ShipViaCode":
                                order.ShipVia = reader.Value;
                                break;
                            case "Character01":
                                order.GLN = reader.Value;
                                break;
                            case "BTCustID":
                                order.CustomerID = reader.Value;
                                break;
                            case "RequestDate":
                                order.RequestDate = convertStrToDate(reader.Value);
                                break;
                            case "ShipNoLater":
                                order.ShipNoLaterDate = convertStrToDate(reader.Value);
                                break;
                            case "NeedByDate":
                                order.NeedByDate = convertStrToDate(reader.Value);
                                break;
                            case "OrderDate":
                                order.OrderDate = convertStrToDate(reader.Value);
                                break;
                            case "OrderLine":
                                order.OrderLineNum = System.Convert.ToInt32(reader.Value);
                                break;
                            case "PartNum":
                                order.Upc = reader.Value;
                                break;
                            case "OrderQty":
                                order.OrderQty = System.Convert.ToDecimal(reader.Value);
                                break;
                            case "SellingFactor":
                                order.SellingFactor = System.Convert.ToDecimal(reader.Value);
                                break;
                            case "SellingFactorDirection":
                                order.SellingFactorDirection = reader.Value;
                                break;
                            case "SellingFactorQty":
                                order.SellingQty = System.Convert.ToDecimal(reader.Value);
                                break;
                            case "UnitPrice":
                                order.UnitPrice = System.Convert.ToDecimal(reader.Value);
                                order.postLine();  // are we sure this is the best way 
                                break;
                        }
                        break;
                        //Console.WriteLine(reader.Value);
                        
                    case XmlNodeType.EndElement: //Display the end of the element.
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;
                }
            }
            // Console.ReadLine();
        }
    }
}
