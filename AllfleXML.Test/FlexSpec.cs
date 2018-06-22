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
        //GXF72_DL
        [TestMethod]
        public void ImportFlexSpec0()
        {
            var specification = Parser.Import(@"TestData\FlexSpec\GXF72_DL.xml");
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }

        //GNXUS840TXF2LM_TSU
        [TestMethod]
        public void ImportFlexSpec1()
        {
            var specification = Parser.Import(@"TestData\FlexSpec\GNXUS840TXF2LM_TSU.xml");
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }

        //GTLF2_GSM2
        [TestMethod]
        public void ImportFlexSpec2()
        {
            var specification = Parser.Import(@"TestData\FlexSpec\GTLF2_GSM2.xml");
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }

        //840HDXLFS3SM
        [TestMethod]
        public void ImportFlexSpec6()
        {
            var specification = Parser.Import(@"TestData\FlexSpec\840HDXLFS3SM.xml");
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }

        /*
        //US8OFPGESM1
        [TestMethod]
        public void ImportFlexSpec3()
        {
            var specification = Parser.Import(@"TestData\FlexSpec\US8OFPGESM1.xml");
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }

        //US840OHGESM1
        [TestMethod]
        public void ImportFlexSpec4()
        {
            var specification = Parser.Import(@"TestData\FlexSpec\US840OHGESM1.xml");
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }

        //840OH_GESM
        [TestMethod]
        public void ImportFlexSpec5()
        {
            var specification = Parser.Import(@"TestData\FlexSpec\840OH_GESM.xml");
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }
        
        //NF_US840STF1SM
        [TestMethod]
        public void ImportFlexSpec7()
        {
            var specification = Parser.Import(@"TestData\FlexSpec\NF_US840STF1SM.xml");
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }

        //JYXC2_GSM
        [TestMethod]
        public void ImportFlexSpec8()
        {
            var specification = Parser.Import(@"TestData\FlexSpec\JYXC2_GSM.xml");
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }

        //IMI_840OF_GESM
        [TestMethod]
        public void ImportFlexSpec9()
        {
            var specification = Parser.Import(@"TestData\FlexSpec\IMI_840OF_GESM.xml");
            Assert.IsNotNull(specification);
            Assert.IsTrue(specification.Specifications.Any());
            Assert.IsTrue(specification.Specifications.Select(o => o.Components.Any()).All(o => o));
        }
        */

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
                        Silhouette = "TestSilhouette",
                        Outline = "TestOutline",
                        Color = "Y",
                        Colors = new List<Color>
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
                        },

                        Faces = new List<Face>
                        {
                            new Face
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
                                        DefaultValue = "1234567890",
                                        ValueFormat = string.Empty,
                                        FontSize = "20",
                                        IsInk = true,
                                        IsLaser = true,
                                        LogoImageLocation = string.Empty,
                                        Radius = 51,
                                        CurveTextAttachTo = string.Empty
                                    },
                                    new Variable
                                    {
                                        Index = 0,
                                        Name = "var1",
                                        Role = "registration",
                                        Type = "Text",
                                        Width = 56,
                                        Height = 12,
                                        PositionX = 8,
                                        PositionY = 7,
                                        DefaultValue = "1234567890",
                                        FontSize = "20",
                                        IsInk = true,
                                        IsLaser = true
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
                Components = new List<Component>
                {
                    new Component
                    {
                        Index = 1,
                        Name = "AS",
                        ProductLine = "Allflex",
                        Silhouette = "TestSilhouette",
                        Outline = "TestOutline",
                        Color = "Y",
                        Colors = new List<Color>
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
                        },

                        Faces = new List<Face>
                        {
                            new Face
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
                                        DefaultValue = "1234567890",
                                        ValueFormat = string.Empty,
                                        FontSize = "20",
                                        IsFixed = false,
                                        IsLaser = true,
                                        IsInk = false,
                                        Radius = 51,
                                        CurveTextAttachTo = "outside"
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

        [TestMethod]
        public void FailedFlexSpecValidation1()
        {
            var result = AllfleXML.FlexSpec.Parser.Validate(@"TestData\FlexOrder\sample0.xml");
            Assert.IsFalse(result.Item1);
        }

        [TestMethod]
        public void FailedFlexSpecValidation2()
        {
            var result = AllfleXML.FlexSpec.Parser.Validate(@"TestData\FlexSpec\FeedlotSpecifications-fail.xml");
            Assert.IsFalse(result.Item1);
        }

        [TestMethod]
        public void PassedFlexSpecValidation()
        {
            var result = AllfleXML.FlexSpec.Parser.Validate(@"TestData\FlexSpec\GTLF2_GSM2.xml");
            Assert.IsTrue(result.Item1);
        }
    }
}
