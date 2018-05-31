using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AllfleXML.FlexOrderStatus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllfleXML.Test
{
    [TestClass]
    public class FlexOrderStatus
    {
        [TestMethod]
        public void ImportFlexOrderStatus1()
        {
            var order = AllfleXML.FlexOrderStatus.Parser.Import(@"TestData\FlexOrderStatus\sample1.xml");
            Assert.IsNotNull(order);
            Assert.IsNotNull(order.WSOrderId);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(order.WSOrderId));
        }

        [TestMethod]
        public void ImportFlexOrderStatus2()
        {
            var order = AllfleXML.FlexOrderStatus.Parser.Import(@"TestData\FlexOrderStatus\sample2.xml");
            Assert.IsNotNull(order);
            Assert.IsNotNull(order.WSOrderId);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(order.WSOrderId));
        }

        [TestMethod]
        public void ImportFlexOrderStatus1Bad()
        {
            AllfleXML.FlexOrderStatus.OrderStatus order = null;
            Exception err = null;
            try
            {
                order = AllfleXML.FlexOrderStatus.Parser.Import(@"TestData\FlexOrderStatus\sample1bad.xml");
            }
            catch (Exception ex)
            {
                err = ex;
            }
            Assert.IsNull(order);
            Assert.IsNotNull(err);
        }

        [TestMethod]
        public void ImportFlexOrderStatus2Bad()
        {
            AllfleXML.FlexOrderStatus.OrderStatus order = null;
            Exception err = null;
            try
            {
                order = AllfleXML.FlexOrderStatus.Parser.Import(@"TestData\FlexOrderStatus\sample2bad.xml");
            }
            catch (Exception ex)
            {
                err = ex;
            }
            Assert.IsNull(order);
            Assert.IsNotNull(err);
        }

        [TestMethod]
        public void ExportFlexOrder()
        {
            var order = new AllfleXML.FlexOrderStatus.OrderStatus
            {
                WSOrderId = "8432fe3a-ef38-45bf-af81-f73a5ae7eb8c",
                PO = "3432564",
                MasterId = 123456,
                OrderId = "CC123456",
                Status = "Confirmed",
                CustomerNumber = "12345",
                Progress = 67,
                Shipment = new List<Shipment>()
                {
                    new Shipment()
                    {
                        ShipMethod = "UPS",
                        ShippingAccountNumber = "98765432",
                        FreightAmount = 14.56,
                        TrackingNumber = "Z3242FFSD324326SA",
                        ShippingDate = DateTime.Now,
                        Address = new Address()
                        {
                            Name = "John H. Smith",
                            Address1 = "123 Main Street",
                            Address2 = "",
                            Address3 = "",
                            City = "Everytown",
                            State = "TX",
                            PostalCode = "12345",
                            Country = "USA"
                        },

                        OrderLines = new List<OrderLine>()
                        {
                            new OrderLine()
                            {
                                LineNumber = 123456,
                                ItemNumber = "Test",
                                Quanitity = 3,
                                Progress = 0,
                                Status = "Confirmed",
                                TagManifest = new TagManifest()
                                {
                                    Containers = new List<Container>()
                                    {
                                        new Container()
                                        {
                                            ChildContainers = null,
                                            ID = "",
                                            Tags = null,
                                            Type = "Bag"
                                        }
                                    }
                                },
                                Template = ""
                            }
                        }


                    }
                },
                Messages = new AllfleXML.FlexOrderStatus.Messages()
                {
                    Message = new List<string> { "This is 1 test message", "This is 2 test message" }
                }
            };

            var doc = AllfleXML.FlexOrderStatus.Parser.Export(order);
            Assert.IsNotNull(doc);

            var result = AllfleXML.FlexOrderStatus.Parser.Validate(doc);
            Assert.IsTrue(result.Item1);
        }

        [TestMethod]
        public void SaveFlexOrder()
        {
            var order = new AllfleXML.FlexOrderStatus.OrderStatus
            {
                WSOrderId = "8432fe3a-ef38-45bf-af81-f73a5ae7eb8c",
                PO = "3432564",
                MasterId = 123456,
                OrderId = "CC123456",
                Status = "Confirmed",
                CustomerNumber = "12345",
                Progress = 67,
                Shipment = new List<Shipment>()
                {
                    new Shipment()
                    {
                        ShipMethod = "UPS",
                        ShippingAccountNumber = "98765432",
                        FreightAmount = 14.56,
                        TrackingNumber = "Z3242FFSD324326SA",
                        ShippingDate = DateTime.Now,
                        Address = new Address()
                        {
                            Name = "John H. Smith",
                            Address1 = "123 Main Street",
                            Address2 = "",
                            Address3 = "",
                            City = "Everytown",
                            State = "TX",
                            PostalCode = "12345",
                            Country = "USA"
                        },
                        OrderLines = new List<OrderLine>()
                        {
                            new OrderLine()
                            {
                                LineNumber = 123456,
                                ItemNumber = "Test",
                                Quanitity = 3,
                                Progress = 0,
                                Status = "Confirmed",
                                TagManifest = new TagManifest()
                                {
                                    Containers = new List<Container>()
                                    {
                                        new Container()
                                        {
                                            ChildContainers = null,
                                            ID = "",
                                            Tags = null,
                                            Type = "Bag"
                                        }
                                    }
                                },
                                Template = ""
                            }
                        }
                    }
                },
                Messages = new AllfleXML.FlexOrderStatus.Messages()
                {
                    Message = new List<string> { "This is a test message" }
                }
            };

            var doc = AllfleXML.FlexOrderStatus.Parser.Export(order);
            Assert.IsNotNull(doc);

            var isValid1 = AllfleXML.FlexOrderStatus.Parser.Validate(doc);
            Assert.IsTrue(isValid1.Item1);

            const string fileName = "testFlexOrderStatus.xml";

            order.Save(fileName);
            Assert.IsTrue(File.Exists(fileName));

            var isValid2 = AllfleXML.FlexOrderStatus.Parser.Validate(fileName);
            Assert.IsTrue(isValid2.Item1);

            var document = AllfleXML.FlexOrderStatus.Parser.Import(fileName);
            Assert.IsNotNull(document);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(document.WSOrderId));

            File.Delete(fileName);
            Assert.IsFalse(File.Exists(fileName));
        }

        [TestMethod]
        public void FailedFlexOrderValidation()
        {
            var result = AllfleXML.FlexOrderStatus.Parser.Validate(@"TestData\ID1Order\sample0.xml");
            Assert.IsFalse(result.Item1);
        }

        [TestMethod]
        public void PassedFlexOrderValidation()
        {
            var result = AllfleXML.FlexOrderStatus.Parser.Validate(@"TestData\FlexOrderStatus\sample1.xml");
            Assert.IsTrue(result.Item1);
        }
    }
}
