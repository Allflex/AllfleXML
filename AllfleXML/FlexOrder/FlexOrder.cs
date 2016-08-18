using System;
using System.Collections.Generic;
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
            
            return isValid;
        }
    }

    [Serializable]
    [XmlRoot]
    public class Document
    {
        [XmlElement("OrderHeader")]
        public List<OrderHeader> OrderHeaders { get; set; }
    }

    [Serializable]
    public class OrderHeader
    {
        [XmlElement("CustomerNumber", IsNullable = false)]
        public string CustomerNumber { get; set; }
        [XmlElement("Comments")]
        public string Comments { get; set; }
        [XmlElement("PremiseID")]
        public string PremiseID { get; set; }
        [XmlElement("PO", IsNullable = false)]
        public string PO { get; set; }
        [XmlElement("ShipToName", IsNullable = false)]
        public string ShipToName { get; set; }
        [XmlElement("ShipToContact")]
        [Obsolete("This field is deprecated. Please use ShipToName instead.")]
        public string ShipToContact { get; set; } // TODO: Remove from specification
        [XmlElement("ShipToPhone")]
        public string ShipToPhone { get; set; }
        [XmlElement("ShipToAddress1", IsNullable = false)]
        public string ShipToAddress1 { get; set; }
        [XmlElement("ShipToAddress2")]
        public string ShipToAddress2 { get; set; }
        [XmlElement("ShipToAddress3")]
        public string ShipToAddress3 { get; set; }
        [XmlElement("ShipToCity", IsNullable = false)]
        public string ShipToCity { get; set; }
        [XmlElement("ShipToState", IsNullable = false)]
        public string ShipToState { get; set; }
        [XmlElement("ShipToPostalCode", IsNullable = false)]
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
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string WSOrderId { get; set; } // TODO: Remove from specification
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
        [XmlElement("SkuName", IsNullable = false)]
        public string SkuName { get; set; }
        [XmlElement("PremiseID")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string PremiseID { get; set; } // TODO: Remove from specification
        [XmlElement("Quantity", IsNullable = false)]
        public int Quantity { get; set; }
        [XmlElement("Template")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Template { get; set; } // TODO: Remove from specification
        [XmlElement("Comment")]
        public string Comment { get; set; }
        [XmlElement("UserDefined")]
        public UserDefinedFields UserDefinedFields { get; set; } = new UserDefinedFields();
        [XmlElement("OrderLineMarkingDetail")]
        public List<OrderLineMarkingDetail> OrderLineMarkingDetail = new List<OrderLineMarkingDetail>();
        [XmlElement("OrderLineCustom")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public List<OrderLineCustom> OrderLineCustom = new List<OrderLineCustom>(); // TODO: Remove from specification
    }

    [Serializable]
    public class OrderLineMarkingDetail
    {
        [XmlElement("Variable")]
        public List<Variable> Variables = new List<Variable>();
        [XmlElement("Comment")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Comment { get; set; } // TODO: Remove from specification
    }

    [Serializable]
    [Obsolete("This class is deprecated. It will be removed in the next version.")]
    public class OrderLineCustom // TODO: Remove from specification
    {
        [XmlElement("Variable")]
        public List<Variable> Variables = new List<Variable>();
        [XmlElement("Comment")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Comment { get; set; } // TODO: Remove from specification
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
