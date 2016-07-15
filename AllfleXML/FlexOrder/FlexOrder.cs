using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AllfleXML
{
    public static class FlexOrder
    {
        public static OrderHeader Import(string xmlFilePath)
        {
            var document = XDocument.Load(xmlFilePath);
            if (!Validate(document))
            {
                throw new XmlSchemaValidationException("XML Document is invalid");
            }

            OrderHeader result;

            var serializer = new XmlSerializer(typeof (OrderHeader));
            using (var reader = XmlReader.Create(xmlFilePath))
            {
                result = (OrderHeader) serializer.Deserialize(reader);
            }

            return result;
        }

        public static string Export(OrderHeader order)
        {
            throw new NotImplementedException();
        }

        public static bool Validate(string xmlFilePath)
        {
            return Validate(XDocument.Load(xmlFilePath));
        }

        public static bool Validate(XDocument xml)
        {
            var xsDocument = new XmlSchemaSet();
            var assembly = Assembly.Load("AllfleXML");
            using (var stream = assembly.GetManifestResourceStream("AllfleXML.FlexOrder.xsd"))
            using (var reader = new StreamReader(stream))
            {
                xsDocument.Add(null, XmlReader.Create(reader));
            }

            var isValid = true;
            xml.Validate(xsDocument, (o, e) => { isValid = false; });
            return isValid;
        }
    }

    public class OrderHeader
    {
        public string CustomerNumber { get; set; }
        public string Comments { get; set; }
        public string PremiseID { get; set; }
        public string PO { get; set; }
        public string ShipToName { get; set; }
        public string ShipToContact { get; set; }
        public string ShipToPhone { get; set; }
        public string ShipToAddress1 { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToAddress3 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToPostalCode { get; set; }
        public string ShipToCountry { get; set; }
        public bool IsRush { get; set; }
        public string ShipMethod { get; set; }
        public string ShippingAccountNumber { get; set; }
        public string EmailListError { get; set; }
        public string EmailListTrackingInfo { get; set; }
        public string EmailListEIDInfo { get; set; }
        public string WSOrderId { get; set; }
        public bool OrderHold { get; set; }
        public List<OrderLineHeader> OrderLineHeaders = new List<OrderLineHeader>();
    }

    public class OrderLineHeader
    {
        public string SkuName { get; set; }
        public string PremiseID { get; set; }
        public int Quantity { get; set; }
        public string Template { get; set; }
        public string Comment { get; set; }
        public List<OrderLineMarkingDetail> OrderLineMarkingDetail = new List<OrderLineMarkingDetail>();
        public List<OrderLineCustom> OrderLineCustom = new List<OrderLineCustom>();
    }

    public class OrderLineMarkingDetail
    {
        public List<Variable> Variables = new List<Variable>();
        public string Comment { get; set; }
    }

    public class OrderLineCustom
    {
        public List<Variable> Variables = new List<Variable>();
        public string Comment { get; set; }
    }

    public class Variable
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
