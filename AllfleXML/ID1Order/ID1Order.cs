using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AllfleXML.ID1Order
{
    [Obsolete("ID1Order.Parser is deprecated, please use FlexOrder.Parser instead.")]
    public class Parser
    {
        public static Document Import(string xmlFilePath)
        {
            return Import(XDocument.Load(xmlFilePath));
        }

        public static Document Import(XDocument document)
        {
            if (!Validate(document))
            {
                throw new XmlSchemaValidationException("XML Document is invalid");
            }

            ID1Order result;

            var serializer = new XmlSerializer(typeof(ID1Order));
            //using (var reader = document.CreateReader())
            using (var reader = new StringReader(document.ToString()))
            {
                result = (ID1Order)serializer.Deserialize(reader);
            }

            return new Document {ID1Order = new List<ID1Order> {result}};
        }

        public static XDocument Export(ID1Order order)
        {
            var result = new XDocument();
            using (var writer = result.CreateWriter())
            {
                var serializer = new XmlSerializer(order.GetType());
                serializer.Serialize(writer, order);
            }
            return result;
        }

        public static bool Validate(string xmlFilePath)
        {
            return Validate(XDocument.Load(xmlFilePath));
        }

        public static bool Validate(XDocument xml)
        {
            var xsDocument = new XmlSchemaSet();
            var assembly = Assembly.Load("AllfleXML");
            using (var stream = assembly.GetManifestResourceStream("AllfleXML.ID1Order.ID1Order.xsd"))
            using (var reader = new StreamReader(stream))
            {
                xsDocument.Add(null, XmlReader.Create(reader));
            }

            var isValid = true;
            xml.Validate(xsDocument, (o, e) => { isValid = false; });
            return isValid;
        }
    }

    [Serializable]
    [XmlRoot]
    [Obsolete("ID1Order.Document is deprecated, please use FlexOrder.Document instead.")]
    public class Document
    {
        public List<ID1Order> ID1Order { get; set; }
    }

    [Serializable]
    [Obsolete("ID1Order.ID1Order is deprecated, please use FlexOrder.OrderHeader instead.")]
    public class ID1Order
    {
        public string SOPNUMBE { get; set; }

        public string MASTERID { get; set; }

        public bool HasDeliveryCharge { get; set; }
        
        [XmlIgnore]
        public bool HasDeliveryChargeSpecified { get; set; }
        
        public string DOCDATE { get; set; }
        
        public string ReqShipDate { get; set; }
        
        public int PriorityStatus { get; set; }

        public bool NotBeforeReqShipDate { get; set; }

        [XmlIgnore]
        public bool NotBeforeReqShipDateSpecified { get; set; }

        public string FREIGHTID { get; set; }

        public string CSTPONBR { get; set; }
        
        public string CUSTNMBR { get; set; }
        
        public string ADRSCODE { get; set; }
        
        public OrderDelivery OrderDelivery { get; set; } = new OrderDelivery();
        
        [XmlArrayItem("OrderNotes", IsNullable = true)]
        public List<OrderNote> OrderNotes { get; set; } = new List<OrderNote>();

        [XmlArrayItem("OrderLine", IsNullable = false)]
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
    
    [Serializable]
    [Obsolete("ID1Order.OrderDelivery is deprecated, please use FlexOrder.OrderHeader instead.")]
    public class OrderDelivery
    {
        public string ShipToName { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIPCODE { get; set; }
        public string COUNTRY { get; set; }
        public string PHONE1 { get; set; }
        public string PHONE2 { get; set; }
        public string FAXNUMBR { get; set; }
        public string Email { get; set; }
        public bool IsRush { get; set; }
        [XmlIgnore]
        public bool IsRushSpecified { get; set; }
        public string SHIPMTHD { get; set; }
        public string PREMISEID { get; set; }
        public decimal FREIGHT { get; set; }
    }

    [Serializable]
    [Obsolete("ID1Order.OrderNote is deprecated, Please use FlexOrder instead.")]
    public class OrderNote
    {
        public string Note { get; set; }
    }

    [Serializable]
    [Obsolete("ID1Order.OrderLine is deprecated, please use FlexOrder.OrderLineHeader instead.")]
    public class OrderLine
    {
        public int LINESEQ { get; set; }
        public string ITEMNMBR { get; set; }
        public string ITEMDESC { get; set; }
        public decimal QTYORDER { get; set; }
        public string UOM { get; set; }
        public string PACKINGID { get; set; }
        public string MARKINGPRG { get; set; }
        public string ITEMNUMBER { get; set; }
        public string QTYBKORD { get; set; }
        public string UNITPRCE { get; set; }
        public string CDCNAME { get; set; }
        [XmlArrayItem("OrderNotes", IsNullable = true)]
        public List<OrderNote> OrderNotes { get; set; } = new List<OrderNote>();
        [XmlArrayItem("OrderLineDetail", IsNullable = false)]
        public List<OrderLineDetail> OrderLineDetails { get; set; }
    }
    
    [Serializable]
    [Obsolete("ID1Order.OrderLineDetail is deprecated, please use FlexOrder.OrderLineHeader instead.")]
    public class OrderLineDetail
    {
        public ushort QUANTITY { get; set; }
        
        [XmlArrayItem("OrderLineMarking", IsNullable = false)]
        public List<Marking> Markings { get; set; }
    }

    [Serializable]
    [Obsolete("ID1Order.Marking is deprecated, please use FlexOrder.OrderLineMarkingDetail instead.")]
    public class Marking
    {
        public bool ISINK { get; set; }
        public int VARLINENRO { get; set; }
        public string FORMAT { get; set; }
        public string VARKITTYPE { get; set; }
        public string VARKITVALUE { get; set; }
    }
}