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
            using (var stream = assembly.GetManifestResourceStream("AllfleXML.FlexOrder.FlexOrderStatus.xsd"))
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
        [XmlElement("Guid")]
        public string Guid { get; set; }

        [XmlElement("PO")]
        public string PO { get; set; }

        [XmlElement("MasterId")]
        public int MasterId { get; set; }

        [XmlElement("AllflexOrderId")]
        public string AllflexOrderId { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }

        [XmlElement("ProductionProgress")]
        public int ProductionProgress { get; set; }
        
        [XmlElement("ErrorMessage")]
        public List<ErrorMessage> ErrorMessages { get; set; }

        [XmlElement("Shipping")]
        public Shipping Shipment { get; set; }
    }

    [Serializable]
    public class ErrorMessage
    {
        [XmlElement("Message")]
        public string Message { get; set; }
    }

    [Serializable]
    public class Shipping
    {
        [XmlElement("ShipMethod")]
        public string ShipMethod { get; set; }

        [XmlElement("ShippingAccountNumber")]
        public string ShippingAccountNumber { get; set; }

        [XmlElement("FreightAmount")]
        public decimal FreightAmount { get; set; }

        [XmlElement("TrackingNumber")]
        public string TrackingNumber { get; set; }

        [XmlElement("ShippingDate")]
        public string ShippingDate { get; set; }

        [XmlElement("Address")]
        public ShippingAddress Address { get; set; }
    }

    [Serializable]
    public class ShippingAddress
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Address1")]
        public string Address1 { get; set; }

        [XmlElement("Address2")]
        public decimal Address2 { get; set; }

        [XmlElement("Address3")]
        public string Address3 { get; set; }

        [XmlElement("City")]
        public string City { get; set; }

        [XmlElement("State")]
        public string State { get; set; }

        [XmlElement("PostalCode")]
        public string PostalCode { get; set; }

        [XmlElement("Country")]
        public string Country { get; set; }
    }
}
