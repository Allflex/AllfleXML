using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AllfleXML.FlexOrder
{
    public static class Parser
    {
        public static List<OrderHeader> Import(string xmlFilePath)
        {
            return Import(XDocument.Load(xmlFilePath));
        }

        public static List<OrderHeader> Import(XDocument document)
        {
            if (!Validate(document))
            {
                throw new XmlSchemaValidationException("XML Document is invalid");
            }

            Document result;

            var serializer = new XmlSerializer(typeof(Document));
            using (var reader = new StringReader(document.ToString()))
            {
                result = (Document)serializer.Deserialize(reader);
            }

            return result.OrderHeaders;
        }

        public static XDocument Export(OrderHeader order)
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
            using (var stream = assembly.GetManifestResourceStream("AllfleXML.FlexOrder.FlexOrder.xsd"))
            using (var reader = new StreamReader(stream))
            {
                xsDocument.Add(null, XmlReader.Create(reader));
            }

            var msg = string.Empty;
            var exc = new Exception();
            var severity = 0;
            var isValid = true;
            xml.Validate(xsDocument, (o, e) =>
            {
                isValid = false;
                msg = e.Message;
                exc = e.Exception;
                severity = (int)e.Severity;
            });

            Debug.Assert(isValid, $"{severity} - {msg}");
            //if (!isValid) throw new Exception($"{severity} - {msg}", exc);

            return isValid;
        }
    }

    [Serializable]
    [XmlRoot]
    //[XmlRoot(Namespace = "http://localhost/", IsNullable = false)]
    public class Document
    {
        [XmlElement("OrderHeader")]
        public List<OrderHeader> OrderHeaders { get; set; }
    }

    [Serializable]
    public class OrderHeader
    {
        [XmlElement("CustomerNumber")]
        public string CustomerNumber { get; set; }
        [XmlElement("Comments")]
        public string Comments { get; set; }
        [XmlElement("PremiseID")]
        public string PremiseID { get; set; }
        [XmlElement("PO")]
        public string PO { get; set; }
        [XmlElement("ShipToName")]
        public string ShipToName { get; set; }
        [XmlElement("ShipToContact")]
        public string ShipToContact { get; set; }
        [XmlElement("ShipToPhone")]
        public string ShipToPhone { get; set; }
        [XmlElement("ShipToAddress1")]
        public string ShipToAddress1 { get; set; }
        [XmlElement("ShipToAddress2")]
        public string ShipToAddress2 { get; set; }
        [XmlElement("ShipToAddress3")]
        public string ShipToAddress3 { get; set; }
        [XmlElement("ShipToCity")]
        public string ShipToCity { get; set; }
        [XmlElement("ShipToState")]
        public string ShipToState { get; set; }
        [XmlElement("ShipToPostalCode")]
        public string ShipToPostalCode { get; set; }
        [XmlElement("ShipToCountry")]
        public string ShipToCountry { get; set; }
        [XmlElement("IsRush")]
        public bool IsRush { get; set; }
        [XmlElement("ShipMethod")]
        public string ShipMethod { get; set; }
        [XmlElement("ShippingAccountNumber")]
        public string ShippingAccountNumber { get; set; }
        [XmlElement("EmailListError")]
        public string EmailListError { get; set; }
        [XmlElement("EmailListTrackingInfo")]
        public string EmailListTrackingInfo { get; set; }
        [XmlElement("EmailListEIDInfo")]
        public string EmailListEIDInfo { get; set; }
        [XmlElement("UserDefined")]
        public UserDefinedFields UserDefinedFields { get; set; } = new UserDefinedFields();
        [XmlElement("WSOrderId")]
        public string WSOrderId { get; set; }
        [XmlElement("Hold")]
        public bool OrderHold { get; set; }
        [XmlElement("OrderLineHeader")]
        public List<OrderLineHeader> OrderLineHeaders = new List<OrderLineHeader>();
    }

    [Serializable]
    public class UserDefinedFields
    {
        [XmlElement("Field")]
        public List<UserDefinedField> Fields { get; set; } = new List<UserDefinedField>();
    }

    [Serializable]
    public class UserDefinedField
    {
        [XmlAttribute("Key")]
        public string Key { get; set; }
        [XmlText]
        public string Value { get; set; }
    }

    [Serializable]
    public class OrderLineHeader
    {
        [XmlElement("SkuName")]
        public string SkuName { get; set; }
        [XmlElement("PremiseID")]
        public string PremiseID { get; set; } // TODO: Remove from specification
        [XmlElement("Quantity")]
        public int Quantity { get; set; }
        [XmlElement("Template")]
        public string Template { get; set; }
        [XmlElement("Comment")]
        public string Comment { get; set; }
        [XmlElement("UserDefined")]
        public UserDefinedFields UserDefinedFields { get; set; } = new UserDefinedFields();
        [XmlElement("OrderLineMarkingDetail")]
        public List<OrderLineMarkingDetail> OrderLineMarkingDetail = new List<OrderLineMarkingDetail>();
        [XmlElement("OrderLineCustom")]
        public List<OrderLineCustom> OrderLineCustom = new List<OrderLineCustom>(); // TODO: Remove from specification
    }

    [Serializable]
    public class OrderLineMarkingDetail
    {
        [XmlElement("Variable")]
        public List<Variable> Variables = new List<Variable>();
        [XmlElement("Comment")]
        public string Comment { get; set; }
    }

    [Serializable]
    public class OrderLineCustom // TODO: Remove from specification
    {
        [XmlElement("Variable")]
        public List<Variable> Variables = new List<Variable>();
        [XmlElement("Comment")]
        public string Comment { get; set; }
    }

    [Serializable]
    public class Variable
    {
        [XmlElement("VariableName")]
        public string Name { get; set; }
        [XmlElement("VariableValue")]
        public string Value { get; set; }
    }
}
