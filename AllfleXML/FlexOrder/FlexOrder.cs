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
    /// <summary>
    /// 
    /// </summary>
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
            var result = new XDocument();
            using (var writer = result.CreateWriter())
            {
                var serializer = new XmlSerializer(order.GetType());
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(writer, order, ns);
            }
            return result;
        }

        public static XDocument Export(this Document document)
        {
            var result = new XDocument();
            using (var writer = result.CreateWriter())
            {
                var serializer = new XmlSerializer(document.GetType());
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(writer, document, ns);
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

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot]
    public class Document
    {
        /// <summary>
        /// Gets or sets the Order Headers
        /// </summary>
        /// <value>
        /// The Order Headers
        /// </value>
        [XmlElement("OrderHeader")]
        public List<OrderHeader> OrderHeaders { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class OrderHeader
    {
        /// <summary>
        /// Gets or sets the Customer account number.
        /// </summary>
        /// <value>
        /// The customer number.
        /// </value>
        [XmlElement("CustomerNumber", IsNullable = false)]
        public string CustomerNumber { get; set; }

        /// <summary>
        /// Gets or sets the Premise Identification Code or the LID number.
        /// </summary>
        /// <value>
        /// The Premise Idenfication Code (PIC) or the Location Identification Number (LID).
        /// </value>
        [XmlElement("PremiseID")]
        public string PremiseID { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        [XmlElement("Comments")]
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the Purchase Order number.
        /// </summary>
        /// <value>
        /// The po.
        /// </value>
        [XmlElement("PO", IsNullable = false)]
        public string PO { get; set; }

        /// <summary>
        /// Gets or sets the name of the shipping address.
        /// </summary>
        /// <value>
        /// The name of the ship to.
        /// </value>
        [XmlElement("ShipToName", IsNullable = false)]
        public string ShipToName { get; set; } // TODO: Remove "ShipTo" from name of fields in the specification

        /// <summary>
        /// Gets or sets the contact at the shipping address.
        /// </summary>
        /// <value>
        /// The ship to contact.
        /// </value>
        [XmlElement("ShipToContact")]
        [Obsolete("This field is deprecated. Please use ShipToName instead.")]
        public string ShipToContact { get; set; } // TODO: Remove from specification  // TODO: Remove "ShipTo" from name of fields in the specification

        /// <summary>
        /// Gets or sets the phone number of the shipping address.
        /// </summary>
        /// <value>
        /// The ship to phone.
        /// </value>
        [XmlElement("ShipToPhone")]
        public string ShipToPhone { get; set; } // TODO: Remove "ShipTo" from name of fields in the specification

        /// <summary>
        /// Gets or sets the first address line of the shipping address.
        /// </summary>
        /// <value>
        /// The ship to address1.
        /// </value>
        [XmlElement("ShipToAddress1", IsNullable = false)]
        public string ShipToAddress1 { get; set; } // TODO: Remove "ShipTo" from name of fields in the specification

        /// <summary>
        /// Gets or sets the second address line of the shipping address.
        /// </summary>
        /// <value>
        /// The ship to address2.
        /// </value>
        [XmlElement("ShipToAddress2")]
        public string ShipToAddress2 { get; set; } // TODO: Remove "ShipTo" from name of fields in the specification

        /// <summary>
        /// Gets or sets the third address line of the shipping address
        /// </summary>
        /// <value>
        /// The ship to address3.
        /// </value>
        [XmlElement("ShipToAddress3")]
        public string ShipToAddress3 { get; set; } // TODO: Remove "ShipTo" from name of fields in the specification

        /// <summary>
        /// Gets or sets the city of the shipping address.
        /// </summary>
        /// <value>
        /// The ship to city.
        /// </value>
        [XmlElement("ShipToCity", IsNullable = false)]
        public string ShipToCity { get; set; } // TODO: Remove "ShipTo" from name of fields in the specification

        /// <summary>
        /// Gets or sets the state of the shipping address.
        /// </summary>
        /// <value>
        /// The state of the ship to.
        /// </value>
        [XmlElement("ShipToState", IsNullable = false)]
        public string ShipToState { get; set; } // TODO: Remove "ShipTo" from name of fields in the specification

        /// <summary>
        /// Gets or sets the postal code of the shipping address.
        /// </summary>
        /// <value>
        /// The ship to postal code.
        /// </value>
        [XmlElement("ShipToPostalCode", IsNullable = false)]
        public string ShipToPostalCode { get; set; } // TODO: Remove "ShipTo" from name of fields in the specification

        /// <summary>
        /// Gets or sets the country of the shipping address.
        /// </summary>
        /// <value>
        /// The ship to country.
        /// </value>
        [XmlElement("ShipToCountry")]
        public string ShipToCountry { get; set; } // TODO: Remove "ShipTo" from name of fields in the specification

        /// <summary>
        /// Gets or sets a value indicating whether this order is a rush.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this order is a rush; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("IsRush")]
        public bool IsRush { get; set; }

        /// <summary>
        /// Gets or sets the shipping method.
        /// </summary>
        /// <value>
        /// The shipping method.
        /// </value>
        [XmlElement("ShipMethod")]
        public string ShipMethod { get; set; }

        /// <summary>
        /// Gets or sets the shipping account number.
        /// </summary>
        /// <value>
        /// The shipping account number.
        /// </value>
        [XmlElement("ShippingAccountNumber")]
        public string ShippingAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the email list error.
        /// </summary>
        /// <value>
        /// The email list error.
        /// </value>
        [XmlElement("EmailListError")]
        public string EmailListError { get; set; }

        /// <summary>
        /// Gets or sets the email list tracking information.
        /// </summary>
        /// <value>
        /// The email list tracking information.
        /// </value>
        [XmlElement("EmailListTrackingInfo")]
        public string EmailListTrackingInfo { get; set; }

        /// <summary>
        /// Gets or sets the email list eid information.
        /// </summary>
        /// <value>
        /// The email list eid information.
        /// </value>
        [XmlElement("EmailListEIDInfo")]
        public string EmailListEIDInfo { get; set; }

        /// <summary>
        /// Gets or sets the user defined fields.
        /// </summary>
        /// <value>
        /// The user defined fields.
        /// </value>
        [XmlElement("UserDefined")]
        public UserDefinedFields UserDefinedFields { get; set; }

        /// <summary>
        /// Gets or sets the ws order identifier.
        /// </summary>
        /// <value>
        /// The ws order identifier.
        /// </value>
        [XmlElement("WSOrderId")]
        public string WSOrderId { get; set; } // TODO: Remove from specification

        /// <summary>
        /// Gets or sets a value indicating whether [order hold].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [order hold]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("Hold")]
        public bool OrderHold { get; set; }

        /// <summary>
        /// Gets or sets the order line headers.
        /// </summary>
        /// <value>
        /// The order line headers.
        /// </value>
        [XmlElement("OrderLineHeader")]
        public List<OrderLineHeader> OrderLineHeaders { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class UserDefinedFields
    {
        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        [XmlElement("Field")]
        public List<UserDefinedField> Fields { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class UserDefinedField
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [XmlAttribute("Key")]
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [XmlText]
        public string Value { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class OrderLineHeader
    {
        /// <summary>
        /// Gets or sets the name of the sku.
        /// </summary>
        /// <value>
        /// The name of the sku.
        /// </value>
        [XmlElement("SkuName", IsNullable = false)]
        public string SkuName { get; set; }
        /// <summary>
        /// Gets or sets the premise identifier.
        /// </summary>
        /// <value>
        /// The premise identifier.
        /// </value>
        [XmlElement("PremiseID")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string PremiseID { get; set; } // TODO: Remove from specification
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [XmlElement("Quantity", IsNullable = false)]
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        /// <value>
        /// The unit of measure.
        /// </value>
        [XmlElement("UnitOfMeasure")]
        public int UnitOfMeasure { get; set; }
        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        [XmlElement("Template")]
        public string Template { get; set; } // TODO: Remove from specification
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        [XmlElement("Comment")]
        public string Comment { get; set; }
        /// <summary>
        /// Gets or sets the user defined fields.
        /// </summary>
        /// <value>
        /// The user defined fields.
        /// </value>
        [XmlElement("UserDefined")]
        public UserDefinedFields UserDefinedFields { get; set; }
        /// <summary>
        /// Gets or sets the order line custom.
        /// </summary>
        /// <value>
        /// The order line custom.
        /// </value>
        [XmlElement("OrderLineCustom")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public List<OrderLineCustom> OrderLineCustom { get; set; } // TODO: Remove from specification
        /// <summary>
        /// Gets or sets the order line marking detail.
        /// </summary>
        /// <value>
        /// The order line marking detail.
        /// </value>
        [XmlElement("OrderLineMarkingDetail")]
        public List<OrderLineMarkingDetail> OrderLineMarkingDetail { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class OrderLineMarkingDetail
    {
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        [XmlElement("Comment")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Comment { get; set; } // TODO: Remove from specification
        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        /// <value>
        /// The variables.
        /// </value>
        [XmlElement("Variable")]
        public List<Variable> Variables { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [Obsolete("This class is deprecated. It will be removed in the next version.")]
    public class OrderLineCustom // TODO: Remove from specification
    {
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        [XmlElement("Comment")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Comment { get; set; } // TODO: Remove from specification
        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        /// <value>
        /// The variables.
        /// </value>
        [XmlElement("Variable")]
        public List<Variable> Variables { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Variable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlElement("VariableName")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [XmlElement("VariableValue")]
        public string Value { get; set; }
    }
}
