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
    }

    [Serializable]
    public class ErrorMessage
    {
        [XmlElement("Message")]
        public string Message { get; set; }
    }
}
