using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AllfleXML.FlexOrderStatus
{
    public static class Parser
    {
        public static OrderStatus Import(string xmlFilePath)
        {
            return Import(XDocument.Load(xmlFilePath));
        }

        public static OrderStatus Import(XDocument document)
        {
            var validation = Validate(document);
            if (!validation.Item1)
            {
                throw new XmlSchemaValidationException(validation.Item2);
            }

            OrderStatus result;
            
            var serializer = new XmlSerializer(typeof(OrderStatus));
            using (var reader = new StringReader(document.ToString()))
            {
                result = (OrderStatus)serializer.Deserialize(reader);
            }

            return result;
        }
        
        public static XDocument Export(this OrderStatus orderStatus)
        {
            var result = new XDocument();
            using (var writer = result.CreateWriter())
            {
                var serializer = new XmlSerializer(orderStatus.GetType());
                serializer.Serialize(writer, orderStatus);
            }
            return result;
        }

        public static void Save(this OrderStatus order, string xmlFilePath)
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
            using (var stream = assembly.GetManifestResourceStream("AllfleXML.FlexOrderStatus.FlexOrderStatus.xsd"))
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
    public class OrderStatus
    {
        /// <summary>
        /// Unique identifier of an order.
        /// </summary>
        /// <value>
        /// The API Global Unique Identifier
        /// </value>
        [XmlElement("WSOrderId", IsNullable = false)]
        public string WSOrderId { get; set; }

        /// <summary>
        /// Gets or sets the Purchase Order number.
        /// </summary>
        /// <value>
        /// The po.
        /// </value>
        [XmlElement("PO", IsNullable = false)]
        public string PO { get; set; }

        /// <summary>
        /// Gets or sets the Master Identification number. An intenal Reference number
        /// </summary>
        /// <value>
        /// The master id (internal to Allflex)
        /// </value>
        [XmlElement("MasterId")]
        public int MasterId { get; set; } = -1;
        
        [XmlIgnore]
        public bool MasterIdSpecified => MasterId >= 0;

        /// <summary>
        /// Gets or sets the Order Idenfication number. An internal Reference number
        /// </summary>
        /// <value>
        /// The allflex order identifier.
        /// </value>
        [XmlElement("OrderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [XmlElement("Status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the Customer Number of the order.
        /// </summary>
        /// <value>
        /// The customer number.
        /// </value>
        [XmlElement("CustomerNumber")]
        public string CustomerNumber { get; set; }

        /// <summary>
        /// Gets or sets the progress percentage of the order.
        /// </summary>
        /// <value>
        /// The progress percentage (in Int form)
        /// </value>
        [XmlElement("Progress")]
        public int Progress { get; set; } = -1;

        [XmlIgnore]
        public bool ProgressSpecified => Progress >= 0;

        /// <summary>
        /// Gets or sets a list of Shipments.
        /// </summary>
        /// <value>
        /// List of Shipments for the order.
        /// </value>
        [XmlElement("Shipment")]
        public List<Shipment> Shipment { get; set; }

        /// <summary>
        /// Gets or sets the messages for an order.
        /// </summary>
        /// <value>
        /// Order Messages.  
        /// </value>
        [XmlElement("Messages")]
        public Messages Messages { get; set; }
    }
    
    /// <summary>
    /// Messages for this order
    /// </summary>
    [Serializable]
    public class Messages
    {
        /// <summary>
        /// Gets or sets the actual error message.
        /// </summary>
        /// <value>
        /// The particular error message set by the system. 
        /// </value>
        [XmlElement("ErrorMessage")]
        public List<string> ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The particular message set by the system. 
        /// </value>
        [XmlElement("Message")]
        public List<string> Message { get; set; }
    }

    /// <summary>
    /// The Shipment information for the order
    /// </summary>
    /// <remarks>
    /// Shipment info will be set at order entry, but fields like Tracking Number and Freight Amount will be applied at Dispatch and Invoicing, respectively. 
    /// </remarks>
    [Serializable]
    public class Shipment
    {
        /// <summary>
        /// Gets or sets the ship method.
        /// </summary>
        /// <value>
        /// The shipping method for the given order. 
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
        /// Gets or sets the freight amount.
        /// </summary>
        /// <value>
        /// The freight amount for the order. 
        /// </value>
        [XmlElement("FreightAmount")]
        public double FreightAmount { get; set; } = -1;

        [XmlIgnore]
        public bool FreightAmountSpecified => FreightAmount >= 0;

        /// <summary>
        /// Gets or sets the tracking number.
        /// </summary>
        /// <value>
        /// The tracking number give by the shipping company. 
        /// </value>
        [XmlElement("TrackingNumber")]
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Gets or sets the shipping date.
        /// </summary>
        /// <value>
        /// The shipping date for the order. 
        /// </value>
        [XmlElement("ShippingDate")]
        public DateTime? ShippingDate { get; set; }

        [XmlIgnore]
        public bool ShippingDateSpecified => ShippingDate != null;

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The shipping address provided by the customer. 
        /// </value>
        [XmlElement("Address")]
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the order lines.
        /// </summary>
        /// <value>
        /// The order lines for the given order.
        /// </value>
        [XmlElement("OrderLine")]
        public List<OrderLine> OrderLines { get; set; }
    }

    [Serializable]
    public class Address
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name to the person or business that the order is being shipped to. 
        /// </value>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Name of the recipient of the order. Unused, please use Name.
        /// </summary>
        [XmlElement("Contact")]
        [Obsolete("This field is deprecated. It will be removed in the next version.")]
        public string Contact { get; set; }

        /// <summary>
        /// Telephone number where the addressed can be reached.
        /// </summary>
        [XmlElement("Phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the first address line.
        /// </summary>
        /// <value>
        /// The first address line for the order. 
        /// </value>
        [XmlElement("Address1")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the second address line.
        /// </summary>
        /// <value>
        /// The second address line for the order (if needed)
        /// </value>
        [XmlElement("Address2")]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the third address line.
        /// </summary>
        /// <value>
        /// The third adress line for the order (if needed).
        /// </value>
        [XmlElement("Address3")]
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The the city/town. 
        /// </value>
        [XmlElement("City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state/province.
        /// </value>
        [XmlElement("State")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        [XmlElement("PostalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country
        /// </value>
        [XmlElement("Country")]
        public string Country { get; set; }
    }

    [Serializable]
    public class OrderLine
    {
        /// <summary>
        /// Gets or sets the order line number.
        /// </summary>
        /// <value>
        /// The line number for the given order line. 
        /// </value>
        [XmlElement("LineNumber")]
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the item number.
        /// </summary>
        /// <value>
        /// The item number or sku for the order line. 
        /// </value>
        [XmlElement("ItemNumber")]
        public string ItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the quanitity.
        /// </summary>
        /// <value>
        /// The amount of items being ordered. 
        /// </value>
        [XmlElement("Quantity")]
        public int Quanitity { get; set; }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        [XmlElement("Template")]
        public string Template { get; set; }

        /// <summary>
        /// Gets or sets the status of the order line.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [XmlElement("Status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the progress percentage of the order line.
        /// </summary>
        /// <value>
        /// The progress percentage (in Int form)
        /// </value>
        [XmlElement("Progress")]
        public int Progress { get; set; } = -1;

        [XmlIgnore]
        public bool ProgressSpecified => Progress >= 0;

        /// <summary>
        /// Gets or sets the tag manifest.
        /// </summary>
        /// <value>
        /// The tag manifest (if applicable). 
        /// </value>
        [XmlElement("TagManifest")]
        public TagManifest TagManifest { get; set; }
    }
    
    [Serializable]
    public class TagManifest
    {
        /// <summary>
        /// Gets or sets the packaging containers.
        /// </summary>
        /// <value>
        /// The packaging containers.
        /// </value>
        [XmlElement("Container")]
        public List<Container> Containers { get; set; }
    }

    [Serializable]
    public class Container
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [XmlElement("ID")]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Tags")]
        public Tags Tags { get; set; }

        /// <summary>
        /// Gets or sets the child container.
        /// </summary>
        /// <value>
        /// The child container.
        /// </value>
        [XmlElement("Container")]
        public List<Container> ChildContainers { get; set; }
    }

    [Serializable]
    public class Tags
    {
        /// <summary>
        /// Gets or sets the tag sets.
        /// </summary>
        /// <value>
        /// The tag sets (of variables)
        /// </value>
        [XmlElement("TagSet")]
        public List<TagSet> TagSets { get; set; }
    }
    
    [Serializable]
    public class TagSet
    {
        /// <summary>
        /// Gets or sets the tag variables
        /// </summary>
        /// <value>
        /// The variables for a given tag. 
        /// </value>
        [XmlElement("Variable")]
        public List<Variable> Variables { get; set; }
    }

    [Serializable]
    public class Variable
    {
        /// <summary>
        /// Gets or sets the variable name.
        /// </summary>
        /// <value>
        /// The variable name.  Could be "EID", "Management", "Serial", etc. 
        /// </value>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the variable value
        /// </summary>
        /// <value>
        /// The value for the variable named with the "Name" field.  For instance, a variable named "EID" might have a value of "9821234567890".
        /// </value>
        [XmlElement("Value")]
        public string Value { get; set; }
    }
    
    public enum ContainerType
    {
        Pallet,
        Case,
        Bag
    }

    public enum ManifestType
    {
        Serial,
        EID,
        Management
    }
}
