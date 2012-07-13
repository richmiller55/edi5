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
                string customerId = this.custId;
                string storeNoPre = split[(int)dicFmt.MarkForLocation];
                int storeInt = Convert.ToInt32(storeNoPre);
                string storeNo = storeInt.ToString();   
                
                if (lastStoreNo != storeNo && notFirstTime) 
                {
                    writer.ProcessOrder(ord);
                    ord = new Order();
                    notFirstTime = true;
                }
                ord.CustomerID = customerId;
                ord.ShipTo = storeNo;
                ord.RequestDateStr = split[(int)dicFmt.ShipDate];
                ord.NeedByDateStr  = split[(int)dicFmt.CancelAfter];
                ord.OrderDateStr   = split[(int)dicFmt.PODate];
                ord.PONumber = split[(int)dicFmt.PONumber];
                ord.ShipVia = this.getShipVia(storeNo);
                ord.TermsCode = crTerms;
                bool processLine = true;
                string smPart = split[(int)dicFmt.SKUNumber];
                ord.CustomerPart = smPart;
                try
                {
                    ord.UPC = partXref[smPart].ToString();
                }
                catch
                {
                    MessageBox.Show("This Steinmart UPC does not match "  + smPart);
                    processLine = false;
                }
                ord.Rev = 0;
                ord.OrderQty = Convert.ToDecimal(split[(int)dicFmt.Qty]);
                ord.UnitPrice = Convert.ToDecimal(split[(int)dicFmt.UnitPrice]);
                if (processLine)
                {
                    ord.postLine();
                }
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
            partXref.Add("23457245", "757026198555");
            partXref.Add("23457252", "757026198586");
            partXref.Add("23457260", "757026198616");
            partXref.Add("23457278", "757026198579");
            partXref.Add("41828526", "757026199330");
            partXref.Add("41828666", "757026199132");
            partXref.Add("41828690", "757026199293");
            partXref.Add("41828773", "757026199286");
            partXref.Add("41829193", "757026199248");
            partXref.Add("41829235", "757026166059");
            partXref.Add("41829755", "757026199224");
            partXref.Add("41830399", "757026197923");
            partXref.Add("41830449", "757026197930");
            partXref.Add("42077719", "757026200715");
            partXref.Add("42078071", "757026201941");
            partXref.Add("42078329", "757026200739");
            partXref.Add("42079392", "757026201873");
            partXref.Add("42079673", "757026200708");
            partXref.Add("42079707", "757026200746");
            partXref.Add("42070367", "757026202009");
            partXref.Add("42671750", "757026204799");
            partXref.Add("42671792", "757026197862");
            partXref.Add("42671859", "757026202467");
            partXref.Add("42671909", "757026194700");
            partXref.Add("42671982", "757026182264");
            partXref.Add("42672030", "757026166325");
            partXref.Add("42672113", "757026203174");
            partXref.Add("42672295", "757026199361");
            partXref.Add("42672360", "757026191587");
            partXref.Add("42672527", "757026194564");
            partXref.Add("42672550", "757026189522");
            partXref.Add("43181320", "757026201866");
            partXref.Add("43181114", "757026202979");
            partXref.Add("43181387", "757026208292");
            partXref.Add("43180850", "757026196322");
            partXref.Add("43180884", "757026194496");
            partXref.Add("43180900", "757026204799");
            partXref.Add("43180926", "757026197862");
            partXref.Add("43181072", "757026204713");
            partXref.Add("43181098", "757026202467");
            partXref.Add("43181346", "757026208308");
            partXref.Add("43398619", "757026208483");
            partXref.Add("43398882", "757026208490");
            partXref.Add("43399302", "757026208506");
            partXref.Add("43399401", "757026208513");
            partXref.Add("43407709", "757026208544");
            partXref.Add("43407725", "757026208551");
            partXref.Add("43407741", "757026208568");
            partXref.Add("43407766", "757026208599");
            partXref.Add("43407782", "757026208605");
            partXref.Add("43407808", "757026208612");
            partXref.Add("43407824", "757026208629");
            partXref.Add("43407840", "757026208636");
            partXref.Add("43407865", "757026208643");
            partXref.Add("43407881", "757026208650");
            partXref.Add("43408046", "757026208667");
            partXref.Add("43408186", "757026208674");
            partXref.Add("43408202", "757026208681");
            partXref.Add("43408236", "757026208698");
            partXref.Add("43408277", "757026208704");
            partXref.Add("43408400", "757026208711");
            partXref.Add("43408558", "757026208728");
            partXref.Add("43408632", "757026208735");
            partXref.Add("43408889", "757026208742");
            partXref.Add("43408962", "757026208834");
            partXref.Add("43409135", "757026208841");
            partXref.Add("43409218", "757026208858");
            partXref.Add("43409283", "757026208865");
            partXref.Add("43409879", "757026208889");
            partXref.Add("43410034", "757026208896");
            partXref.Add("43410166", "757026208902");
            partXref.Add("43410620", "757026197398");
            partXref.Add("43410828", "757026197718");
            partXref.Add("43410927", "757026197725");
            partXref.Add("43410984", "757026197749");
            partXref.Add("43411255", "757026197756");
            partXref.Add("43411685", "757026197763");
            partXref.Add("43411891", "757026201552");
            partXref.Add("43412030", "757026201576");
            partXref.Add("43412139", "757026201590");
            partXref.Add("43412295", "757026201613");
            partXref.Add("43412519", "757026201507");
            partXref.Add("43412691", "757026201521");
            partXref.Add("43413384", "757026155633");
            partXref.Add("43414374", "757026155640");
            partXref.Add("43414465", "757026155657");
            partXref.Add("43414549", "757026155664");
            partXref.Add("43414606", "757026155602");
            partXref.Add("43414697", "757026155626");
            partXref.Add("43414762", "757026155725");
            partXref.Add("43414887", "757026155732");
            partXref.Add("43414978", "757026155749");
            partXref.Add("43415033", "757026155671");
            partXref.Add("43415090", "757026155688");
            partXref.Add("43415124", "757026155695");
            partXref.Add("43415173", "757026211681");
            partXref.Add("43415215", "757026133631");
            partXref.Add("43415249", "757026121409");
            partXref.Add("44007391", "757026211117");
            partXref.Add("44007508", "757026211100");
            partXref.Add("44006773", "757026208308");
            partXref.Add("44006971", "757026208292");
            partXref.Add("44007045", "757026191587");
            partXref.Add("44007565", "757026209442");
            partXref.Add("44007631", "757026209459");
            partXref.Add("44850667", "757026161177");
            partXref.Add("44850634", "757026161153");
            partXref.Add("44850550", "757026174467");
            partXref.Add("44850899", "757026211049");
            partXref.Add("44850949", "757026211971");
            partXref.Add("44850576", "757026220379");
            partXref.Add("44850600", "757026194670");
            partXref.Add("44850691", "757026211063");
            partXref.Add("44851020", "757026216198");
            partXref.Add("44850923", "757026209459");
            partXref.Add("44851111", "757026211117");
            partXref.Add("44851145", "757026220447");
            partXref.Add("44850493", "757026211001");

            partXref.Add("45922796", "757026221932");
            partXref.Add("45922846", "757026216785");
            partXref.Add("45922937", "757026201866");
            partXref.Add("45923158", "757026208308");
            partXref.Add("45923240", "757026220447");
            partXref.Add("45923299", "757026189386");
            partXref.Add("45923489", "757026198388");
            partXref.Add("45923737", "757026220430");
            partXref.Add("45923836", "757026221871");
            partXref.Add("45923869", "757026221925");

            partXref.Add("46668463", "757026221949");
            partXref.Add("46668505", "757026225275");
            partXref.Add("46668653", "757026221857");
            partXref.Add("46668679", "757026225688");
            partXref.Add("46668737", "757026198388");
            partXref.Add("46672630", "757026216884");
            partXref.Add("46672820", "757026217133");
            partXref.Add("46672846", "757026216938");
            partXref.Add("46672903", "757026217188");
            partXref.Add("46672937", "757026216952");
            partXref.Add("46672994", "757026217201");
            partXref.Add("46673026", "757026217102");
            partXref.Add("46673083", "757026217355");
            partXref.Add("46673166", "757026216990");
            partXref.Add("46673182", "757026217249");
            partXref.Add("46673240", "757026217096");
            partXref.Add("46673265", "757026217348");
            partXref.Add("46673281", "757026217041");
            partXref.Add("46673463", "757026217294");
            partXref.Add("46673505", "757026217058");
            partXref.Add("46673596", "757026217300");
            partXref.Add("46673703", "757026217072");
            partXref.Add("46673869", "757026217324");

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
            shipViaHash.Add("349", "DOAK");
            shipViaHash.Add("351", "DOAK");
            shipViaHash.Add("354", "DOAK");
            shipViaHash.Add("357", "DOAK");
            shipViaHash.Add("359", "DOAK");
            shipViaHash.Add("360", "DOAK");
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
            shipViaHash.Add("345", "AOAK");
            shipViaHash.Add("346", "AOAK");
            shipViaHash.Add("348", "AOAK");
            shipViaHash.Add("352", "AOAK");
            shipViaHash.Add("353", "AOAK");
            shipViaHash.Add("356", "AOAK");
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
            shipViaHash.Add("347", "COAK");
            shipViaHash.Add("350", "COAK");
            shipViaHash.Add("355", "COAK");
            shipViaHash.Add("358", "COAK");
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



