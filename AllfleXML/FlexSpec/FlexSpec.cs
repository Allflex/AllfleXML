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
    /// Document Container
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
    /// Marking specification for a product offering.
    /// </summary>
    [Serializable]
    public class Specification
    {
        /// <summary>
        /// Gets or sets the Unique identifier for the specification.
        /// </summary>
        /// <value>
        /// The Unique identifier for the specification.
        /// </value>
        [XmlElement("Id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the specification.
        /// </summary>
        /// <value>
        /// The Name of the specification.
        /// </value>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the components.
        /// </summary>
        /// <value>
        /// The Components in the specification
        /// </value>
        public List<Component> Components { get; set; }
    }

    /// <summary>
    /// Raw component definition
    /// </summary>
    [Serializable]
    public class Component
    {
        /// <summary>
        /// Gets or sets the unique ID defining the order of the component.
        /// </summary>
        [XmlElement("Index")]
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [XmlElement("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the product line, or brand.
        /// </summary>
        [XmlElement("ProductLine")]
        public string ProductLine { get; set; }

        /// <summary>
        /// Gets or sets the Silhouette.
        /// Optional: For use by the renderer. The silhouette is the filled component outline to be entirely recolored by the selected color with the outline placed on top.
        /// </summary>
        [XmlElement("Silhouette")]
        public string Silhouette { get; set; }

        /// <summary>
        /// Gets or sets the Outline.
        /// Optional: For use by the renderer. The outline is the component shadows and definition lines to be placed on top of the silhouette to give it a raised, 3D effect.
        /// </summary>
        [XmlElement("Outline")]
        public string Outline { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        [XmlElement("Color")]
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the Faces.
        /// </summary>
        public List<Face> Faces { get; set; }

        /// <summary>
        /// Valid colors of the component.
        /// </summary>
        public List<Color> Colors { get; set; }
    }


    /// <summary>
    /// Material Color
    /// </summary>
    [Serializable]
    public class Color
    {
        /// <summary>
        /// Gets or sets the ColorCode.
        /// Identifying code of the Color
        /// </summary>
        [XmlElement("ColorCode")]
        public string ColorCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the color.
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the HexCode.
        /// Hexidecimal Value of the color.
        /// </summary>
        [XmlElement("HexCode")]
        public string HexCode { get; set; }
    }

    /// <summary>
    /// The side of the component. Physical or otherwise (as in the RFID field of a component).
    /// </summary>
    [Serializable]
    public class Face
    {
        /// <summary>
        /// Gets or sets the Name.
        /// Indicates weather the variables are marked on the front or back of a component, or in the RFID field of the component. Values can be 'Front', 'Back', or 'RFID'
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the components.
        /// </summary>
        public List<Variable> Variables { get; set; }
    }

    /// <summary>
    /// Parameters of a single marked value.
    /// </summary>
    [Serializable]
    public class Variable
    {
        /// <summary>
        /// Gets or sets the Index.
        /// Index order of the variable. Unique for all variables in a specification.
        /// </summary>
        [XmlElement("Index")]
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// The name of the variable. Unique for all variables in a specification.
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// Optional description of the variable.
        /// </summary>
        [XmlElement("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Role.
        /// Significance of the variable (Management number, Registration Number, Serial Number, One Time Programmable/Electronic Identification, Animal Identification Number, etc)
        /// </summary>
        [XmlElement("Role")]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// Required: Values can be 'text', 'curved-text' or 'image'
        /// </summary>
        [XmlElement("Type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Width.
        /// Width of the variable's bounding box.
        /// </summary>
        [XmlElement("Width")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the Height.
        /// Height of the variable's bounding box.
        /// </summary>
        [XmlElement("Height")]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the PositionX.
        /// X coordinate of the top left corner of the variable's bounding box.
        /// </summary>
        [XmlElement("PositionX")]
        public int PositionX { get; set; }

        /// <summary>
        /// Gets or sets the PositionY.
        /// Y coordinate of the top left corner of the variable's bounding box.
        /// </summary>
        [XmlElement("PositionY")]
        public int PositionY { get; set; }

        /// <summary>
        /// Gets or sets the DefaultValue.
        /// The default value of the variable.
        /// </summary>
        [XmlElement("DefaultValue")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the TextFormat.
        /// Optional: Only used when `Type` = "text" or "curved-text". Value indicates formatting of the `Text` value '#' = character, '_' = space insert
        /// </summary>
        [XmlElement("ValueFormat")]
        public string ValueFormat { get; set; }

        /// <summary>
        /// Gets or sets the MaxLength
        /// Optional: Only used when `Type` = "text" or "curved-text". Value indicates the maximum length of `Text`
        /// </summary>
        [XmlElement("MaxLength")]
        public int MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the FontSize.
        /// Optional: Only used when `Type` = "text" or "curved-text". Value indicates the size of the font
        /// </summary>
        [XmlElement("FontSize")]
        public string FontSize { get; set; }

        /// <summary>
        /// Gets or sets the IsFixed.
        /// Indicates if the variable value is static for the specification. The value can not be changed by instance.
        /// </summary>
        [XmlElement("IsFixed")]
        public bool IsFixed { get; set; }

        /// <summary>
        /// Gets or sets the IsLaser.
        /// Indicates if the value is laser etched into the product.
        /// </summary>
        [XmlElement("IsLaser")]
        public bool IsLaser { get; set; } = true;

        /// <summary>
        /// Gets or sets the IsInk.
        /// Required: Determines if a variable value will be inked onto the product (It is possible to ink both TEXT and LOGO)
        /// </summary>
        [XmlElement("IsInk")]
        public bool IsInk { get; set; }

        /// <summary>
        /// Gets or sets the ImageLocation.
        /// Optional: For use by the renderer. Only used when `Type` = "image". Location of the logo image file. the Logo Inditification number should be populated as the variable value.
        /// </summary>
        [XmlElement("LogoImageLocation")]
        public string LogoImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the Radius.
        /// Optional: Only used when `Type` = "curved-text". Value indicates wideness of curved text
        /// </summary>
        [XmlElement("Radius")]
        public int? Radius { get; set; }

        /// <summary>
        /// Gets or sets the CurveTextAttachTo.
        /// Optional: Only used when `Type` = "curved-text". Values can be 'inside' or 'outside'
        /// </summary>
        [XmlElement("CurveTextAttachTo")]
        public string CurveTextAttachTo { get; set; }

        /// <summary>
        /// Name of the variable to copy value from. isFixed must be set to true if this property has a value set.
        /// Optional: Accepts the variable name of an existing varialble on this specification. It will set IsFixed to true and makes DefaultValue the same as the provided variable name.
        /// </summary>
        [XmlElement("CopyValueFrom")]
        public string CopyValueFrom { get; set; }
    }
}