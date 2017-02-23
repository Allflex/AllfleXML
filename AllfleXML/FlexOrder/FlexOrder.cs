using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AllfleXML.FlexOrder
{
    public static class Parser
    {
        public static Document Import(string xmlFilePath)
        {
            return Import(XDocument.Load(xmlFilePath));
        }

        public static Document Import(XDocument document)
        {
            var validation = Validate(document);
            if (!validation.Item1)
            {
                throw new XmlSchemaValidationException(validation.Item2);
            }

            Document result;

            var rootElement = document.Document?.Root?.Name.ToString();
            if (rootElement == "OrderHeader")
            {
                result = new Document {OrderHeaders = new List<OrderHeader>()};
                var serializer = new XmlSerializer(typeof(OrderHeader));
                using (var reader = new StringReader(document.ToString()))
                {
                    result.OrderHeaders.Add((OrderHeader)serializer.Deserialize(reader));
                }
            }
            else
            {
                var serializer = new XmlSerializer(typeof(Document));
                using (var reader = new StringReader(document.ToString()))
                {
                    result = (Document)serializer.Deserialize(reader);
                }
            }

            return result;
        }

        public static XDocument Export(OrderHeader order)
        {
            return Export(new Document { OrderHeaders = new List<OrderHeader> { order } });
        }

        public static XDocument Export(Document document)
        {
            var result = new XDocument();
            using (var writer = result.CreateWriter())
            {
                var serializer = new XmlSerializer(document.GetType());
                serializer.Serialize(writer, document);
            }
            return result;
        }

        public static Tuple<bool, string> Validate(string xmlFilePath)
        {
            return Validate(XDocument.Load(xmlFilePath));
        }

        public static Tuple<bool, string> Validate(XDocument xml)
        {
            var xsDocument = new XmlSchemaSet();
            var assembly = Assembly.Load("AllfleXML");
            using (var stream = assembly.GetManifestResourceStream("AllfleXML.FlexOrder.FlexOrder.xsd"))
            using (var reader = new StreamReader(stream))
            {
                xsDocument.Add(null, XmlReader.Create(reader));
            }

            var errors = new List<Tuple<int, string, Exception>>();
            var isValid = true;
            xml.Validate(xsDocument, (o, e) =>
            {
                errors.Add(new Tuple<int, string, Exception>((int)e.Severity, e.Message, e.Exception));
                isValid = false;
            });

            var errs =
                errors.Select(o => $"Error (Severity: {o.Item1}) - {o.Item2} {o.Item3.Message}")
                    .Aggregate(string.Empty, (c, e) => $"{c}{e}\n");

            var message = isValid ? string.Empty : errs;
            var result = new Tuple<bool, string>(isValid, message);
            return result;
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
        [XmlElement("CustomerNumber", IsNullable = false, Order = 1)]
        public string CustomerNumber { get; set; }
        [XmlElement("PremiseID", Order = 2)]
        public string PremiseID { get; set; }
        [XmlElement("Comments", Order = 3)]
        public string Comments { get; set; }
        [XmlElement("PO", IsNullable = false, Order = 4)]
        public string PO { get; set; }
        [XmlElement("ShipToName", IsNullable = false, Order = 5)]
        public string ShipToName { get; set; }
        [XmlElement("ShipToContact", Order = 6)]
        [Obsolete("This field is deprecated. Please use ShipToName instead.")]
        public string ShipToContact { get; set; } // TODO: Remove from specification
        [XmlElement("ShipToPhone", Order = 7)]
        public string ShipToPhone { get; set; }
        [XmlElement("ShipToAddress1", IsNullable = false, Order = 8)]
        public string ShipToAddress1 { get; set; }
        [XmlElement("ShipToAddress2", Order = 9)]
        public string ShipToAddress2 { get; set; }
        [XmlElement("ShipToAddress3", Order = 10)]
        public string ShipToAddress3 { get; set; }
        [XmlElement("ShipToCity", IsNullable = false, Order = 11)]
        public string ShipToCity { get; set; }
        [XmlElement("ShipToState", IsNullable = false, Order = 12)]
        public string ShipToState { get; set; }
        [XmlElement("ShipToPostalCode", IsNullable = false, Order = 13)]
        public string ShipToPostalCode { get; set; }
        [XmlElement("ShipToCountry", Order = 14)]
        public string ShipToCountry { get; set; }
        [XmlElement("IsRush", Order = 15)]
        public bool IsRush { get; set; }
        [XmlElement("ShipMethod", Order = 16)]
        public string ShipMethod { get; set; }
        [XmlElement("ShippingAccountNumber", Order = 17)]
        public string ShippingAccountNumber { get; set; }
        [XmlElement("EmailListError", Order = 18)]
        public string EmailListError { get; set; }
        [XmlElement("EmailListTrackingInfo", Order = 19)]
        public string EmailListTrackingInfo { get; set; }
        [XmlElement("EmailListEIDInfo", Order = 20)]
        public string EmailListEIDInfo { get; set; }
        [XmlElement("UserDefined", Order = 21)]
        public UserDefinedFields UserDefinedFields { get; set; }
        [XmlElement("WSOrderId", Order = 22)]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string WSOrderId { get; set; } // TODO: Remove from specification
        [XmlElement("Hold", Order = 23)]
        public bool OrderHold { get; set; }
        [XmlElement("OrderLineHeader", Order = 24)]
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
        [XmlElement("SkuName", IsNullable = false, Order = 1)]
        public string SkuName { get; set; }
        [XmlElement("PremiseID", Order = 2)]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string PremiseID { get; set; } // TODO: Remove from specification
        [XmlElement("Quantity", IsNullable = false, Order = 3)]
        public int Quantity { get; set; }
        [XmlElement("Template", Order = 4)]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Template { get; set; } // TODO: Remove from specification
        [XmlElement("Comment", Order = 5)]
        public string Comment { get; set; }
        [XmlElement("UserDefined", Order = 6)]
        public UserDefinedFields UserDefinedFields { get; set; }
        [XmlElement("OrderLineCustom", Order = 7)]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public List<OrderLineCustom> OrderLineCustom = new List<OrderLineCustom>(); // TODO: Remove from specification
        [XmlElement("OrderLineMarkingDetail", Order = 8)]
        public List<OrderLineMarkingDetail> OrderLineMarkingDetail = new List<OrderLineMarkingDetail>();
    }

    [Serializable]
    public class OrderLineMarkingDetail
    {
        [XmlElement("Comment", Order = 1)]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Comment { get; set; } // TODO: Remove from specification
        [XmlElement("Variable", Order = 2)]
        public List<Variable> Variables = new List<Variable>();
    }

    [Serializable]
    [Obsolete("This class is deprecated. It will be removed in the next version.")]
    public class OrderLineCustom // TODO: Remove from specification
    {
        [XmlElement("Comment", Order = 1)]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Comment { get; set; } // TODO: Remove from specification
        [XmlElement("Variable", Order = 2)]
        public List<Variable> Variables = new List<Variable>();
    }

    [Serializable]
    public class Variable
    {
        [XmlElement("VariableName", Order = 1)]
        public string Name { get; set; }
        [XmlElement("VariableValue", Order = 2)]
        public string Value { get; set; }
    }
}
