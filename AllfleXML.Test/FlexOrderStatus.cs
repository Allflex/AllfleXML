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
            Assert.IsNotNull(order.Guid);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(order.Guid));
        }
        
        [TestMethod]
        public void ExportFlexOrder()
        {
            var order = new AllfleXML.FlexOrderStatus.OrderStatus
            {
                  Guid = "8432fe3a-ef38-45bf-af81-f73a5ae7eb8c",
                  PurchaseOrderNumber = "3432564",
                  MasterId = 123456,
                  AllflexOrderId = "CC123456",
                  Status = "Confirmed",
                  ProductionProgress = 67,
                  Shipping = new Shipping
                   {
                        ShipMethod = "UPS",
                        ShippingAccountNumber = "98765432",
                        FreightAmount = 14.56,
                        TrackingNumber = "Z3242FFSD324326SA",
                        ShippingDate = DateTime.Now,
                        Address = new ShippingAddress
                        {
                            Name = "John H. Smith",
                            Address1 = "123 Main Street",
                            Address2 = "",
                            Address3 = "",
                            City = "Everytown",
                            State = "TX",
                            PostalCode = "12345",
                            Country = "USA"
                        }
                   },
                ErrorMessages = new List<ErrorMessage>
                    {
                        new AllfleXML.FlexOrderStatus.ErrorMessage
                        {
                            Message = "This is a test message"
                        }
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
                Guid = "8432fe3a-ef38-45bf-af81-f73a5ae7eb8c",
                PurchaseOrderNumber = "3432564",
                MasterId = 123456,
                AllflexOrderId = "CC123456",
                Status = "Confirmed",
                ProductionProgress = 67,
                Shipping = new Shipping
                {
                    ShipMethod = "UPS",
                    ShippingAccountNumber = "98765432",
                    FreightAmount = 14.56,
                    TrackingNumber = "Z3242FFSD324326SA",
                    ShippingDate = DateTime.Now,
                    Address = new ShippingAddress
                    {
                        Name = "John H. Smith",
                        Address1 = "123 Main Street",
                        Address2 = "",
                        Address3 = "",
                        City = "Everytown",
                        State = "TX",
                        PostalCode = "12345",
                        Country = "USA"
                    }
                },
                ErrorMessages = new List<ErrorMessage>
                    {
                        new AllfleXML.FlexOrderStatus.ErrorMessage
                        {
                            Message = "This is a test message"
                        }
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
            Assert.IsTrue(!string.IsNullOrWhiteSpace(document.Guid));

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
