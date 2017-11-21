using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AllfleXML.FlexSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllfleXML.Test
{
    [TestClass]
    public class FlexSpec
    {
        public const string testPath = @"C:\ScaliseProjects\AllfleXML\AllfleXML\FlexSpec\FlexSpec.xml";

        [TestMethod]
        public void ImportFlexSpec()
        {
            var specification = Parser.Import(testPath);
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }

        [TestMethod]
        public void SaveFlexSpec()
        {
            var order = new Specification
            {
                Id = "Testing",
                Name = "Testing_Elements",
                Components = new List<Component>
                {
                    new Component
                    {
                        Index = 1,
                        Name = "AS",
                        ProductLine = "Allflex",
                        Shilouette = "TestShiloutte",
                        Outline = "TestOutline",
                        Features = String.Empty,
                        Color = "Y",
                        Colors = new Colors
                        {
                            Color = new List<Color>
                            {
                                new Color
                                {
                                    ColorCode = "Y",
                                    Name = "yellow",
                                    HexCode = "e4d84d"
                                },
                                new Color
                                {
                                    ColorCode = "B",
                                    Name = "Blue",
                                    HexCode = "71cfeb"
                                }
                            }
                        },

                        Faces = new List<ComponentFace>
                        {
                            new ComponentFace
                            {
                                Name = "Front",
                                Variables = new List<Variable>
                                {
                                    new Variable
                                    {
                                        Index = 0,
                                        Name = "var1",
                                        Description = string.Empty,
                                        Role = "registration",
                                        Type = "curved-text",
                                        Width = 56,
                                        Height = 12,
                                        PositionX = 8,
                                        PositionY = 7,
                                        Text = "1234567890",
                                        TextFormat = string.Empty,
                                        MaxLength = string.Empty,
                                        FontSize = "20",
                                        IsFixed = string.Empty,
                                        IsInk = "True",
                                        LogoImageLocation = String.Empty,
                                        Radius = 51,
                                        CurveTextAttachTo = string.Empty
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var doc = order.Export();
            Assert.IsNotNull(doc);

            var isValid1 = Parser.Validate(doc);
            Assert.IsTrue(isValid1.Item1);

            const string fileName = "testFlexOrder.xml";

            order.Save(fileName);
            Assert.IsTrue(File.Exists(fileName));

            var isValid2 = Parser.Validate(fileName);
            Assert.IsTrue(isValid2.Item1);

            var document = Parser.Import(fileName);
            Assert.IsNotNull(document);
            Assert.IsTrue(document.Specifications.Any());
            Assert.IsTrue(document.Specifications.Select(o => o.Components.Any()).All(o => o));

            File.Delete(fileName);
            Assert.IsFalse(File.Exists(fileName));
        }

        [TestMethod]
        public void ExportFlexSpec()
        {
            var order = new Specification
            {
                Id = "Testing",
                Name = "Testing_Elements",
                Components =  new List<Component>
                {
                    new Component
                    {
                        Index = 1,
                        Name = "AS",
                        ProductLine = "Allflex",
                        Shilouette = "TestShiloutte",
                        Outline = "TestOutline",
                        Features = String.Empty,
                        Color = "Y",
                        Colors = new Colors
                        {
                            Color = new List<Color>
                            {
                                new Color
                                {
                                    ColorCode = "Y",
                                    Name = "yellow",
                                    HexCode = "e4d84d"
                                },
                                new Color
                                {
                                    ColorCode = "B",
                                    Name = "Blue",
                                    HexCode = "71cfeb"
                                }
                            }
                        },

                        Faces = new List<ComponentFace>
                        {
                            new ComponentFace
                            {
                                Name = "Front",
                                Variables = new List<Variable>
                                {
                                    new Variable
                                    {
                                        Index = 0,
                                        Name = "var1",
                                        Description = string.Empty,
                                        Role = "registration",
                                        Type = "curved-text",
                                        Width = 56,
                                        Height = 12,
                                        PositionX = 8,
                                        PositionY = 7,
                                        Text = "1234567890",
                                        TextFormat = string.Empty,
                                        MaxLength = string.Empty,
                                        FontSize = "20",
                                        IsFixed = string.Empty,
                                        IsInk = string.Empty,
                                        LogoImageLocation = String.Empty,
                                        Radius = 51,
                                        CurveTextAttachTo = string.Empty
                                    }
                                }
                            }
                        }
                    }
                }
            };
        

            var doc = order.Export();
            Assert.IsNotNull(doc);

            var result = Parser.Validate(doc);
            Assert.IsTrue(result.Item1);
        }


    }
}
