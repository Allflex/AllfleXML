using System.Collections.Generic;
using System.IO;
using System.Linq;
using AllfleXML.FlexOrder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllfleXML.Test
{
    [TestClass]
    public class FlexOrder
    {
        [TestMethod]
        public void ImportFlexOrder1()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample1.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrder2()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample2.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrder3()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample3.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrder4()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample4.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrder5()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample5.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrder6()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample6.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrder7()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample7.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrder8()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample8.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrder9()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample9.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrder0()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample0.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrderEmptyUserDefined()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample11.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.OrderHeaders.Any());
            Assert.IsTrue(order.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ImportFlexOrderMini()
        {
            // TODO: Restrict this possiblity in the future
            // We will continue to support the missing <Document> node for the time being.
            var document = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sampleMini.xml");
            Assert.IsNotNull(document);
            Assert.IsTrue(document.OrderHeaders.Any());

            foreach (var order in document.OrderHeaders)
            {
                Assert.IsTrue(order.OrderLineHeaders.Any());
            }
        }

        [TestMethod]
        public void ImportFlexOrderMulti()
        {
            var document = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sampleMulti.xml");
            Assert.IsNotNull(document);
            Assert.IsTrue(document.OrderHeaders.Any());
            Assert.IsTrue(document.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));
        }

        [TestMethod]
        public void ExportFlexOrder()
        {
            var order = new AllfleXML.FlexOrder.OrderHeader
            {
                CustomerNumber = "testing",
                PremiseID = "ABC1234",
                PO = "123456",
                ShipToName = "Jane Doe",
                ShipToContact = "Jane Doe",
                ShipToPhone = "5551234567",
                ShipToAddress1 = "Rev. Calvin & Thelma Alcorn",
                ShipToCity = "Dallas",
                ShipToState = "TX",
                ShipToPostalCode = "76021",
                ShipToCountry = "US",
                ShipMethod = "UPS",
                OrderLineHeaders = new List<AllfleXML.FlexOrder.OrderLineHeader>
                {
                    new AllfleXML.FlexOrder.OrderLineHeader
                    {
                        SkuName = "ANTXLSet3306LA",
                        Quantity = 17,
                    }
                }
            };

            var doc = AllfleXML.FlexOrder.Parser.Export(order);
            Assert.IsNotNull(doc);

            var result = AllfleXML.FlexOrder.Parser.Validate(doc);
            Assert.IsTrue(result.Item1);
        }

        [TestMethod]
        public void SaveFlexOrder()
        {
            var order = new AllfleXML.FlexOrder.OrderHeader
            {
                CustomerNumber = "testing",
                PremiseID = "ABC1234",
                PO = "123456",
                ShipToName = "Jane Doe",
                ShipToContact = "Jane Doe",
                ShipToPhone = "5551234567",
                ShipToAddress1 = "Rev. Calvin & Thelma Alcorn",
                ShipToCity = "Dallas",
                ShipToState = "TX",
                ShipToPostalCode = "76021",
                ShipToCountry = "US",
                ShipMethod = "UPS",
                OrderLineHeaders = new List<AllfleXML.FlexOrder.OrderLineHeader>
                {
                    new AllfleXML.FlexOrder.OrderLineHeader
                    {
                        SkuName = "ANTXLSet3306LA",
                        Quantity = 17,
                    }
                }
            };

            var doc = AllfleXML.FlexOrder.Parser.Export(order);
            Assert.IsNotNull(doc);

            var isValid1 = AllfleXML.FlexOrder.Parser.Validate(doc);
            Assert.IsTrue(isValid1.Item1);

            const string fileName = "testFlexOrder.xml";

            order.Save(fileName);
            Assert.IsTrue(File.Exists(fileName));

            var isValid2 = AllfleXML.FlexOrder.Parser.Validate(fileName);
            Assert.IsTrue(isValid2.Item1);

            var document = AllfleXML.FlexOrder.Parser.Import(fileName);
            Assert.IsNotNull(document);
            Assert.IsTrue(document.OrderHeaders.Any());
            Assert.IsTrue(document.OrderHeaders.Select(o => o.OrderLineHeaders.Any()).All(o => o));

            File.Delete(fileName);
            Assert.IsFalse(File.Exists(fileName));
        }

        [TestMethod]
        public void FailedFlexOrderValidation()
        {
            var result = AllfleXML.FlexOrder.Parser.Validate(@"TestData\ID1Order\sample0.xml");
            Assert.IsFalse(result.Item1);
        }

        [TestMethod]
        public void PassedFlexOrderValidation()
        {
            var result = AllfleXML.FlexOrder.Parser.Validate(@"TestData\FlexOrder\sample0.xml");
            Assert.IsTrue(result.Item1);
        }
    }
}
