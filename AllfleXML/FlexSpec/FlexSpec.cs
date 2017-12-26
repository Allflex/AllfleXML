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

namespace AllfleXML.FlexSpec
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
            if (rootElement == "Specification")
            {
                // TODO: Deprecate this.
                Trace.WriteLine("Document users Specification as root node. This feature is obsolete, please wrap the Specification node in a Document node.");
                result = new Document { Specifications = new List<Specification>() };
                var serializer = new XmlSerializer(typeof(Specification));
                using (var reader = new StringReader(document.ToString()))
                {
                    result.Specifications.Add((Specification)serializer.Deserialize(reader));
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


        public static XDocument Export(this Specification specification)
        {
            return Export(new Document { Specifications = new List<Specification> { specification } });
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

        public static void Save(this Specification specification, string xmlFilePath)
        {
            var doc = Export(specification);
            doc.Save(xmlFilePath);
        }

        public static void Save(this Document specification, string xmlFilePath)
        {
            var doc = Export(specification);
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
            using (var stream = assembly.GetManifestResourceStream("AllfleXML.FlexSpec.FlexSpec.xsd"))
            using (var reader = new StreamReader(stream))
            {
                xsDocument.Add(null, XmlReader.Create(reader));
            }

            var errors = new List<Tuple<int, string, Exception>>();
            var isValid = true;
            xml.Validate(xsDocument, (o, e) =>
            {
                if (errors.SingleOrDefault(x => x.Item2 == e.Message) == null)
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

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot]
    public class Document
    {
        /// <summary>
        /// Gets or sets the Specification
        /// </summary>
        /// <value>
        /// The Specification
        /// </value>
        [XmlElement("Specification")]
        public List<Specification> Specifications { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Specification
    {
        /// <summary>
        /// Gets or sets the ID of the specification.
        /// </summary>
        /// <value>
        /// The ID of the specification.
        /// </value>
        [XmlElement("Id", IsNullable = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the specification.
        /// </summary>
        /// <value>
        /// The Name of the specification.
        /// </value>
        [XmlElement("Name", IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the components.
        /// </summary>
        /// <value>
        /// The Components in the specification
        /// </value>
        public List<Component> Components { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Component
    {
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        [XmlElement("Index", IsNullable = false)]
        public int Index { get; set; }
        
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [XmlElement("Name", IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product line.
        /// </summary>
        [XmlElement("ProductLine", IsNullable = false)]
        public string ProductLine { get; set; }
        
        /// <summary>
        /// Gets or sets the Shilouette.
        /// </summary>
        [XmlElement("Shilouette", IsNullable = false)]
        public string Shilouette { get; set; }

        /// <summary>
        /// Gets or sets the Outline.
        /// </summary>
        [XmlElement("Outline", IsNullable = false)]
        public string Outline { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        [XmlElement("Color", IsNullable = false)]
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Colors Colors { get; set; }

        /// <summary>
        /// Gets or sets the Faces.
        /// </summary>
        public List<ComponentFace> Faces { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Faces
    {
        /// <summary>
        /// Gets or sets the name of the specification.
        /// </summary>
        [XmlElement("ComponentFace", IsNullable = false)]
        public List<ComponentFace> ComponentFace { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Colors
    {
        /// <summary>
        /// Gets or sets the name of the specification.
        /// </summary>
        [XmlElement("Color", IsNullable = false)]
        public List<Color> Color { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Color
    {
        /// <summary>
        /// Gets or sets the ColorCode.
        /// </summary>
        [XmlElement("ColorCode", IsNullable = false)]
        public string ColorCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the color.
        /// </summary>
        [XmlElement("Name", IsNullable = false)]
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the HexCode.
        /// </summary>
        [XmlElement("HexCode", IsNullable = false)]
        public string HexCode { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ComponentFace
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [XmlElement("Name", IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the components.
        /// </summary>
        public List<Variable> Variables { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Variable
    {
        /// <summary>
        /// Gets or sets the Index.
        /// </summary>
        [XmlElement("Index", IsNullable = false)]
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [XmlElement("Name", IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        [XmlElement("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Role.
        /// </summary>
        [XmlElement("Role", IsNullable = false)]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        [XmlElement("Type", IsNullable = false)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Width.
        /// </summary>
        [XmlElement("Width", IsNullable = false)]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the Height.
        /// </summary>
        [XmlElement("Height", IsNullable = false)]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the PositionX.
        /// </summary>
        [XmlElement("PositionX", IsNullable = false)]
        public int PositionX { get; set; }

        /// <summary>
        /// Gets or sets the PositionY.
        /// </summary>
        [XmlElement("PositionY", IsNullable = false)]
        public int PositionY { get; set; }

        /// <summary>
        /// Gets or sets the DefaultValue.
        /// </summary>
        [XmlElement("DefaultValue")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the TextFormat.
        /// </summary>
        [XmlElement("ValueFormat", IsNullable = true)]
        public string ValueFormat { get; set; }

        /// <summary>
        /// Gets or sets the MaxLength
        /// </summary>
        [XmlElement("MaxLength", IsNullable = false)]
        public int MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the FontSize.
        /// </summary>
        [XmlElement("FontSize")]
        public string FontSize { get; set; }

        /// <summary>
        /// Gets or sets the IsFixed.
        /// </summary>
        [XmlElement("IsFixed")]
        public bool IsFixed { get; set; }

        /// <summary>
        /// Gets or sets the IsLaser.
        /// </summary>
        [XmlElement("IsLaser")]
        public bool IsLaser { get; set; } = true;

        /// <summary>
        /// Gets or sets the IsInk.
        /// </summary>
        [XmlElement("IsInk")]
        public bool IsInk { get; set; }

        /// <summary>
        /// Gets or sets the ImageLocation.
        /// </summary>
        [XmlElement("LogoImageLocation", IsNullable = true)]
        public string LogoImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the Radius.
        /// </summary>
        [XmlElement("Radius", IsNullable = true)]
        public int? Radius { get; set; }

        /// <summary>
        /// Gets or sets the CurveTextAttachTo.
        /// </summary>
        [XmlElement("CurveTextAttachTo", IsNullable = true)]
        public string CurveTextAttachTo { get; set; }
    }
}
