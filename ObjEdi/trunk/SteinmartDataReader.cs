using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace ObjEdi
{
    public enum dicFmt
    {
        TransControlNo,
        POType,
        PONumber,
        PODate,
        DepartmentNo,
        VendorNo,
        BuyerNo,
        ShipDate,
        CancelAfter,
        WarehouseDescription,
        WarehouseNumber,
        Qty,
        QtyUnit,
        UnitPrice,
        BasisofUnitPrice,
        SKUNumber,
        UP_VA_UKCode1,
        UP_VA_UKNo1,
        UP_VA_UKCode2,
        UP_VA_UKNo2,
        ExtendedLineAmountt,
        StoreNoNotThere,
        StoreQuantity,
        TotalPOAmountt,
        NoofLineItems,
        TotalOrderQuantityy,
        ShipToStoreName,
        ShipToStoreAddress1,
        ShipToStoreAddress2,
        ShipToStoreCity,
        ShipToStoreState,
        ShipToStoreZipcode,
        ShipToLocation,
        MarkForLocation,
        SKU_UPNumber,
        CrossVendorItem,
        CrossColor,
        CrossSize,
        CrossDescr,
        SalesRequirementCode1,
        SalesRequirementCode2,
        PromotionStart,
        SpecialOrderType,
        TermsDescription,
        Instructions,
        Message,
        Routing,
        BuyerContactName,
        BuyerContactPhone,
        DeliveryContactName,
        DeliveryContactPhone,
        RetailPrice,
        Hanger_PackingTypeCode,
        Hanger_PackingTypeDescr,
        BlanketOrderNo,
        Priority,
        VendorStyle,
        TransType,
        InstructionsID,
        ManufacturersSuggestedRetailPrice,
        A_CIndicator,
        AgencyQualifierType
    }
    class SteinmartDataReader
    {
        string dir = "D:/users/EDI/SteinMart/InboxOrders/";
        
        StreamReader tr;
        Order ord;
	    Hashtable partXref;
        Hashtable shipViaHash;
	    string crTerms = "N30";
        string custId = "75070";
        public SteinmartDataReader()
        {
            initLookupStyle();
            initShipVia();
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            FileInfo[] fileListInfo = dirInfo.GetFiles();

            foreach (FileSystemInfo fsi in fileListInfo)
            {
                string file = fsi.FullName;
                tr = new StreamReader(file);
                processFile();
            }
        }
        void processFile()
        {
            string line = "";
            string lastStoreNo = "first";
            ord = new Order();
            bool notFirstTime = false;
            WriteSalesOrder writer = new WriteSalesOrder();
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string customerId = custId;
                string storeNoPre = split[(int)dicFmt.MarkForLocation];
                int storeInt = Convert.ToInt32(storeNoPre);
                string storeNo = storeInt.ToString();   
                
                // int result = storeNo.CompareTo("391");
                // if (result == 0) continue;
                if (lastStoreNo != storeNo && notFirstTime) 
                {
                    writer.ProcessOrder(ord);
                    ord = new Order();
                    notFirstTime = true;
                }
                ord.setCustomerId(customerId);
                ord.setShipTo(storeNo);
                ord.setRequestDate(split[(int)dicFmt.ShipDate]);
                ord.setNeedByDate(split[(int)dicFmt.CancelAfter]);
                ord.setOrderDate(split[(int)dicFmt.PODate]);
                string poNo = split[(int)dicFmt.PONumber];
                ord.setPoNum(poNo);
                string shipVia = getShipVia(storeNo);
                ord.setShipVia(shipVia);
                ord.setTerms(crTerms);
                string upc = "";
                string smPart = split[(int)dicFmt.SKUNumber];
                try
                {
                    upc = partXref[smPart].ToString();
                }
                catch
                {
                    MessageBox.Show("This Steinmart UPC does not match "  + smPart);
                }
                ord.setUpc(upc); 
                ord.setRev("0");  // 0
                ord.setOrderQty(split[(int)dicFmt.Qty]);
                ord.setUnitPrice(split[(int)dicFmt.UnitPrice]);
                ord.postLine();
                lastStoreNo = storeNo;
                notFirstTime = true;
            }
            writer.ProcessOrder(ord);
            ord = new Order();
            notFirstTime = true;
        }
        void initLookupStyle()
        {
            partXref = new Hashtable();
            partXref.Add("15908403", "757026153448");
            partXref.Add("16694127", "757026136601");
            partXref.Add("16694135", "757026131477");
            partXref.Add("16694101", "757026161313"); 
            partXref.Add("21230842", "757026176102");
            partXref.Add("21230859", "757026167049");
            partXref.Add("21230867", "757026177062");
            partXref.Add("21230875", "757026167209");
            partXref.Add("21230883", "757026176614");
            partXref.Add("21230891", "757026176546");
            partXref.Add("21230909", "757026182592");
            partXref.Add("21230917", "757026183742");
            partXref.Add("21230925", "757026183766");
            partXref.Add("16694119", "757026161320");
            partXref.Add("21327549", "757026192676");
            partXref.Add("21327614", "757026192683");
            partXref.Add("21144076", "757026191952");
            partXref.Add("21144084", "757026191969");
            partXref.Add("21144092", "757026191976");
            partXref.Add("21144100", "757026192003");
            partXref.Add("21144118", "757026191945");
            partXref.Add("21144126", "757026185395");
            partXref.Add("21144134", "757026185388");
            partXref.Add("20771663", "757026186590");
            partXref.Add("20865952", "757026189683");
            partXref.Add("20865960", "757026189546");
            partXref.Add("21472568", "757026182356");
            partXref.Add("21144191", "757026191938");
            partXref.Add("21144423", "757026191983");
            partXref.Add("21144431", "757026191990");
            partXref.Add("21144449", "757026192010");
            partXref.Add("22927701", "757026170544");
            partXref.Add("22927719", "757026192515");
            partXref.Add("22927727", "757026192881");
            partXref.Add("22927735", "757026191594");
            partXref.Add("22927743", "757026194410");
            partXref.Add("22927750", "757026192669");
            partXref.Add("22927768", "757026191587");
            partXref.Add("22927776", "757026194588");
            partXref.Add("22927628", "757026117228");
            partXref.Add("22927610", "757026134508");
            partXref.Add("22927602", "757026192799");
            partXref.Add("22927594", "757026166554");
            partXref.Add("23473713", "757026198654");
            partXref.Add("23473820", "757026198623");
            partXref.Add("23473945", "757026198562");
            partXref.Add("23473952", "757026198487");
            partXref.Add("23474174", "757026198494");
            partXref.Add("23474299", "757026198500");
            partXref.Add("23474307", "757026198630");
        }
        void initShipVia()
        {
            shipViaHash = new Hashtable();
            shipViaHash.Add("5","DOAK");
            shipViaHash.Add("13","DOAK");
            shipViaHash.Add("14","DOAK");
            shipViaHash.Add("15","DOAK");
            shipViaHash.Add("22","DOAK");
            shipViaHash.Add("23","DOAK");
            shipViaHash.Add("24","DOAK");
            shipViaHash.Add("27","DOAK");
            shipViaHash.Add("30","DOAK");
            shipViaHash.Add("40","DOAK");
            shipViaHash.Add("44","DOAK");
            shipViaHash.Add("46","DOAK");
            shipViaHash.Add("50","DOAK");
            shipViaHash.Add("55","DOAK");
            shipViaHash.Add("56","DOAK");
            shipViaHash.Add("61","DOAK");
            shipViaHash.Add("65","DOAK");
            shipViaHash.Add("66","DOAK");
            shipViaHash.Add("68","DOAK");
            shipViaHash.Add("70","DOAK");
            shipViaHash.Add("72","DOAK");
            shipViaHash.Add("78","DOAK");
            shipViaHash.Add("79","DOAK");
            shipViaHash.Add("82","DOAK");
            shipViaHash.Add("84","DOAK");
            shipViaHash.Add("86","DOAK");
            shipViaHash.Add("90","DOAK");
            shipViaHash.Add("91","DOAK");
            shipViaHash.Add("98","DOAK");
            shipViaHash.Add("99","DOAK");
            shipViaHash.Add("104","DOAK");
            shipViaHash.Add("107","DOAK");
            shipViaHash.Add("112","DOAK");
            shipViaHash.Add("113","DOAK");
            shipViaHash.Add("117","DOAK");
            shipViaHash.Add("118","DOAK");
            shipViaHash.Add("122","DOAK");
            shipViaHash.Add("123","DOAK");
            shipViaHash.Add("124","DOAK");
            shipViaHash.Add("126","DOAK");
            shipViaHash.Add("131","DOAK");
            shipViaHash.Add("132","DOAK");
            shipViaHash.Add("138","DOAK");
            shipViaHash.Add("140","DOAK");
            shipViaHash.Add("146","DOAK");
            shipViaHash.Add("161","DOAK");
            shipViaHash.Add("177","DOAK");
            shipViaHash.Add("181","DOAK");
            shipViaHash.Add("192","DOAK");
            shipViaHash.Add("193","DOAK");
            shipViaHash.Add("206","DOAK");
            shipViaHash.Add("208","DOAK");
            shipViaHash.Add("217","DOAK");
            shipViaHash.Add("222","DOAK");
            shipViaHash.Add("232","DOAK");
            shipViaHash.Add("233","DOAK");
            shipViaHash.Add("237","DOAK");
            shipViaHash.Add("252","DOAK");
            shipViaHash.Add("261","DOAK");
            shipViaHash.Add("276","DOAK");
            shipViaHash.Add("314","DOAK");
            shipViaHash.Add("315","DOAK");
            shipViaHash.Add("319","DOAK");
            shipViaHash.Add("323","DOAK");
            shipViaHash.Add("327","DOAK");
            shipViaHash.Add("339","DOAK");
            shipViaHash.Add("342", "DOAK");
            shipViaHash.Add("343", "DOAK");
            shipViaHash.Add("344", "DOAK");
            shipViaHash.Add("1", "AOAK");
            shipViaHash.Add("3", "AOAK");
            shipViaHash.Add("4", "AOAK");
            shipViaHash.Add("6", "AOAK");
            shipViaHash.Add("7", "AOAK");
            shipViaHash.Add("8", "AOAK");
            shipViaHash.Add("10", "AOAK");
            shipViaHash.Add("11", "AOAK");
            shipViaHash.Add("12", "AOAK");
            shipViaHash.Add("16", "AOAK");
            shipViaHash.Add("18", "AOAK");
            shipViaHash.Add("19", "AOAK");
            shipViaHash.Add("25", "AOAK");
            shipViaHash.Add("26", "AOAK");
            shipViaHash.Add("28", "AOAK");
            shipViaHash.Add("31", "AOAK");
            shipViaHash.Add("32", "AOAK");
            shipViaHash.Add("33", "AOAK");
            shipViaHash.Add("36", "AOAK");
            shipViaHash.Add("37", "AOAK");
            shipViaHash.Add("38", "AOAK");
            shipViaHash.Add("39", "AOAK");
            shipViaHash.Add("41", "AOAK");
            shipViaHash.Add("42", "AOAK");
            shipViaHash.Add("43", "AOAK");
            shipViaHash.Add("45", "AOAK");
            shipViaHash.Add("47", "AOAK");
            shipViaHash.Add("48", "AOAK");
            shipViaHash.Add("49", "AOAK");
            shipViaHash.Add("51", "AOAK");
            shipViaHash.Add("52", "AOAK");
            shipViaHash.Add("53", "AOAK");
            shipViaHash.Add("54", "AOAK");
            shipViaHash.Add("57", "AOAK");
            shipViaHash.Add("58", "AOAK");
            shipViaHash.Add("60", "AOAK");
            shipViaHash.Add("62", "AOAK");
            shipViaHash.Add("63", "AOAK");
            shipViaHash.Add("67", "AOAK");
            shipViaHash.Add("71", "AOAK");
            shipViaHash.Add("73", "AOAK");
            shipViaHash.Add("74", "AOAK");
            shipViaHash.Add("75", "AOAK");
            shipViaHash.Add("76", "AOAK");
            shipViaHash.Add("81", "AOAK");
            shipViaHash.Add("87", "AOAK");
            shipViaHash.Add("89", "AOAK");
            shipViaHash.Add("92", "AOAK");
            shipViaHash.Add("94", "AOAK");
            shipViaHash.Add("96", "AOAK");
            shipViaHash.Add("97", "AOAK");
            shipViaHash.Add("100", "AOAK");
            shipViaHash.Add("102", "AOAK");
            shipViaHash.Add("103", "AOAK");
            shipViaHash.Add("105", "AOAK");
            shipViaHash.Add("108", "AOAK");
            shipViaHash.Add("116", "AOAK");
            shipViaHash.Add("119", "AOAK");
            shipViaHash.Add("120", "AOAK");
            shipViaHash.Add("125", "AOAK");
            shipViaHash.Add("127", "AOAK");
            shipViaHash.Add("130", "AOAK");
            shipViaHash.Add("134", "AOAK");
            shipViaHash.Add("135", "AOAK");
            shipViaHash.Add("136", "AOAK");
            shipViaHash.Add("137", "AOAK");
            shipViaHash.Add("139", "AOAK");
            shipViaHash.Add("141", "AOAK");
            shipViaHash.Add("149", "AOAK");
            shipViaHash.Add("153", "AOAK");
            shipViaHash.Add("155", "AOAK");
            shipViaHash.Add("158", "AOAK");
            shipViaHash.Add("160", "AOAK");
            shipViaHash.Add("167", "AOAK");
            shipViaHash.Add("174", "AOAK");
            shipViaHash.Add("175", "AOAK");
            shipViaHash.Add("180", "AOAK");
            shipViaHash.Add("182", "AOAK");
            shipViaHash.Add("183", "AOAK");
            shipViaHash.Add("184", "AOAK");
            shipViaHash.Add("185", "AOAK");
            shipViaHash.Add("186", "AOAK");
            shipViaHash.Add("188", "AOAK");
            shipViaHash.Add("190", "AOAK");
            shipViaHash.Add("194", "AOAK");
            shipViaHash.Add("201", "AOAK");
            shipViaHash.Add("202", "AOAK");
            shipViaHash.Add("203", "AOAK");
            shipViaHash.Add("204", "AOAK");
            shipViaHash.Add("205", "AOAK");
            shipViaHash.Add("207", "AOAK");
            shipViaHash.Add("212", "AOAK");
            shipViaHash.Add("213", "AOAK");
            shipViaHash.Add("215", "AOAK");
            shipViaHash.Add("216", "AOAK");
            shipViaHash.Add("218", "AOAK");
            shipViaHash.Add("219", "AOAK");
            shipViaHash.Add("221", "AOAK");
            shipViaHash.Add("223", "AOAK");
            shipViaHash.Add("224", "AOAK");
            shipViaHash.Add("225", "AOAK");
            shipViaHash.Add("226", "AOAK");
            shipViaHash.Add("228", "AOAK");
            shipViaHash.Add("230", "AOAK");
            shipViaHash.Add("236", "AOAK");
            shipViaHash.Add("238", "AOAK");
            shipViaHash.Add("239", "AOAK");
            shipViaHash.Add("240", "AOAK");
            shipViaHash.Add("243", "AOAK");
            shipViaHash.Add("247", "AOAK");
            shipViaHash.Add("249", "AOAK");
            shipViaHash.Add("250", "AOAK");
            shipViaHash.Add("253", "AOAK");
            shipViaHash.Add("255", "AOAK");
            shipViaHash.Add("262", "AOAK");
            shipViaHash.Add("265", "AOAK");
            shipViaHash.Add("266", "AOAK");
            shipViaHash.Add("267", "AOAK");
            shipViaHash.Add("268", "AOAK");
            shipViaHash.Add("269", "AOAK");
            shipViaHash.Add("271", "AOAK");
            shipViaHash.Add("272", "AOAK");
            shipViaHash.Add("275", "AOAK");
            shipViaHash.Add("277", "AOAK");
            shipViaHash.Add("278", "AOAK");
            shipViaHash.Add("279", "AOAK");
            shipViaHash.Add("280", "AOAK");
            shipViaHash.Add("281", "AOAK");
            shipViaHash.Add("283", "AOAK");
            shipViaHash.Add("284", "AOAK");
            shipViaHash.Add("286", "AOAK");
            shipViaHash.Add("287", "AOAK");
            shipViaHash.Add("288", "AOAK");
            shipViaHash.Add("289", "AOAK");
            shipViaHash.Add("290", "AOAK");
            shipViaHash.Add("291", "AOAK");
            shipViaHash.Add("293", "AOAK");
            shipViaHash.Add("295", "AOAK");
            shipViaHash.Add("296", "AOAK");
            shipViaHash.Add("297", "AOAK");
            shipViaHash.Add("298", "AOAK");
            shipViaHash.Add("299", "AOAK");
            shipViaHash.Add("301", "AOAK");
            shipViaHash.Add("302", "AOAK");
            shipViaHash.Add("303", "AOAK");
            shipViaHash.Add("305", "AOAK");
            shipViaHash.Add("307", "AOAK");
            shipViaHash.Add("308", "AOAK");
            shipViaHash.Add("309", "AOAK");
            shipViaHash.Add("310", "AOAK");
            shipViaHash.Add("312", "AOAK");
            shipViaHash.Add("313", "AOAK");
            shipViaHash.Add("316", "AOAK");
            shipViaHash.Add("317", "AOAK");
            shipViaHash.Add("320", "AOAK");
            shipViaHash.Add("321", "AOAK");
            shipViaHash.Add("322", "AOAK");
            shipViaHash.Add("324", "AOAK");
            shipViaHash.Add("325", "AOAK");
            shipViaHash.Add("326", "AOAK");
            shipViaHash.Add("329", "AOAK");
            shipViaHash.Add("330", "AOAK");
            shipViaHash.Add("331", "AOAK");
            shipViaHash.Add("332", "AOAK");
            shipViaHash.Add("333", "AOAK");
            shipViaHash.Add("334", "AOAK");
            shipViaHash.Add("335", "AOAK");
            shipViaHash.Add("337", "AOAK");
            shipViaHash.Add("338", "AOAK");
            shipViaHash.Add("341", "AOAK");
            shipViaHash.Add("703", "AOAK");
            shipViaHash.Add("704", "AOAK");
            shipViaHash.Add("80", "COAK");
            shipViaHash.Add("85", "COAK");
            shipViaHash.Add("88", "COAK");
            shipViaHash.Add("93", "COAK");
            shipViaHash.Add("106", "COAK");
            shipViaHash.Add("121", "COAK");
            shipViaHash.Add("143", "COAK");
            shipViaHash.Add("148", "COAK");
            shipViaHash.Add("154", "COAK");
            shipViaHash.Add("173", "COAK");
            shipViaHash.Add("187", "COAK");
            shipViaHash.Add("198", "COAK");
            shipViaHash.Add("199", "COAK");
            shipViaHash.Add("210", "COAK");
            shipViaHash.Add("214", "COAK");
            shipViaHash.Add("220", "COAK");
            shipViaHash.Add("229", "COAK");
            shipViaHash.Add("234", "COAK");
            shipViaHash.Add("245", "COAK");
            shipViaHash.Add("246", "COAK");
            shipViaHash.Add("248", "COAK");
            shipViaHash.Add("251", "COAK");
            shipViaHash.Add("270", "COAK");
            shipViaHash.Add("273", "COAK");
            shipViaHash.Add("274", "COAK");
            shipViaHash.Add("294", "COAK");
            shipViaHash.Add("304", "COAK");
            shipViaHash.Add("311", "COAK");
            shipViaHash.Add("318", "COAK");
            shipViaHash.Add("328", "COAK");
            shipViaHash.Add("336", "COAK");
            shipViaHash.Add("340", "COAK");
            shipViaHash.Add("701", "COAK");
        }
        string getShipVia(string store)
        {
            string shipVia = "UGND";
            try
            {
                shipVia = shipViaHash[store].ToString();
            }
            catch
            {
                shipVia = "UGND";
            }
            return shipVia;
        }
        string getCaUpc(string smPart)
        {
            string upc = partXref[smPart].ToString();
            return upc;
        }
    }
}



