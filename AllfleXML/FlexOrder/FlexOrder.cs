using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                // TODO: Deprecate this.
                Trace.WriteLine("Document users OrderHeader as root node. This feature is obsolete, please wrap the OrderHeader node in a Document node.");
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

        public static XDocument Export(this OrderHeader order)
        {
            return Export(new Document { OrderHeaders = new List<OrderHeader> { order } });
        }

        public static XDocument Export(this Document document)
        {
            var result = new XDocument();
            using (var writer = result.CreateWriter())
            {
                var serializer = new XmlSerializer(document.GetType());
                serializer.Serialize(writer, document);
            }
            return result;
        }

        public static void Save(this OrderHeader order, string xmlFilePath)
        {
            var doc = Export(order);
            doc.Save(xmlFilePath);
        }

        public static void Save(this Document order, string xmlFilePath)
        {
            var doc = Export(order);
            doc.Save(xmlFilePath);
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
                if (errors.SingleOrDefault(x => x.Item2 == e.Message) == null)
                    errors.Add(new Tuple<int, string, Exception>((int) e.Severity, e.Message, e.Exception));
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
        [XmlElement("CustomerNumber", IsNullable = false)]
        public string CustomerNumber { get; set; }
        [XmlElement("PremiseID")]
        public string PremiseID { get; set; }
        [XmlElement("Comments")]
        public string Comments { get; set; }
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
        public UserDefinedFields UserDefinedFields { get; set; }
        [XmlElement("WSOrderId")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string WSOrderId { get; set; } // TODO: Remove from specification
        [XmlElement("Hold")]
        public bool OrderHold { get; set; }
        [XmlElement("OrderLineHeader")]
        public List<OrderLineHeader> OrderLineHeaders { get; set; }
    }

    [Serializable]
    public class UserDefinedFields
    {
        [XmlElement("Field")]
        public List<UserDefinedField> Fields { get; set; }
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
        public UserDefinedFields UserDefinedFields { get; set; }
        [XmlElement("OrderLineCustom")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public List<OrderLineCustom> OrderLineCustom { get; set; } // TODO: Remove from specification
        [XmlElement("OrderLineMarkingDetail")]
        public List<OrderLineMarkingDetail> OrderLineMarkingDetail { get; set; }
    }

    [Serializable]
    public class OrderLineMarkingDetail
    {
        [XmlElement("Comment")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Comment { get; set; } // TODO: Remove from specification
        [XmlElement("Variable")]
        public List<Variable> Variables { get; set; }
    }

    [Serializable]
    [Obsolete("This class is deprecated. It will be removed in the next version.")]
    public class OrderLineCustom // TODO: Remove from specification
    {
        [XmlElement("Comment")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Comment { get; set; } // TODO: Remove from specification
        [XmlElement("Variable")]
        public List<Variable> Variables { get; set; }
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
