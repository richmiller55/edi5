using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OrderEDI
{
    class XmlReader
    {
        public string fileName;
        string currentElement;
        ShipToOrder order;
        public XmlReader(string dir, string file)
        {
            fileName = dir + file;
            order = new ShipToOrder();
        }
        public ShipToOrder getOrder()
        {
            return order;
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
        public void runIt()
        {
            XmlTextReader reader = new XmlTextReader(fileName);
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
                                order.ShipToId = reader.Value;
                                order.setShipToLine(reader.Value);
                                break;
                            case "ShipTo":
                                order.ShipToId = reader.Value;
                                order.setShipToLine(reader.Value);
                                break;
                            case "ShipViaCode":
                                order.setShipVia(reader.Value);
                                break;
                            case "Character01":
                                order.setLocation(reader.Value);
                                break;
                            case "BTCustID":
                                order.CustomerID = reader.Value;
                                break;
                            case "RequestDate":
                                order.setRequestDate(reader.Value);
                                break;
                            case "NeedByDate":
                                order.NeedByDate = convertStrToDate(reader.Value);
                                break;
                            case "OrderDate":
                                order.setOrderDate(reader.Value);
                                break;
                            case "OrderLine":
                                order.setOrderLineNo(reader.Value);
                                break;
                            case "PartNum":
                                order.setUpc(reader.Value);
                                break;
                            case "OrderQty":
                                order.setOrderQty(reader.Value);
                                break;
                            case "UnitPrice":
                                order.setUnitPrice(reader.Value);
                                order.postLine();
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
            Console.ReadLine();
        }
    }
}
