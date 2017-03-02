using System.Collections.Generic;
using System.IO;
using System.Linq;
using AllfleXML.ID1Order;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllfleXML.Test
{
    [TestClass]
    public class ID1Order
    {
        [TestMethod]
        public void ImportID1Order0()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample0.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.ID1Order.Any());
        }

        [TestMethod]
        public void ImportID1Order1()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample1.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.ID1Order.Any());
        }

        [TestMethod]
        public void ImportID1Order2()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample2.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.ID1Order.Any());
        }

        [TestMethod]
        public void ImportID1Order3()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample3.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.ID1Order.Any());
        }

        [TestMethod]
        public void ImportID1Order4()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample4.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.ID1Order.Any());
        }

        [TestMethod]
        public void ImportID1Order5()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample5.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.ID1Order.Any());
        }

        [TestMethod]
        public void ImportID1Order6()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample6.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.ID1Order.Any());
        }

        [TestMethod]
        public void ImportID1Order7()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample7.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.ID1Order.Any());
        }

        [TestMethod]
        public void ImportID1Order8()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample8.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.ID1Order.Any());
        }

        [TestMethod]
        public void ImportID1Order9()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample9.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.ID1Order.Any());
        }

        [TestMethod]
        public void ExportID1Order()
        {
            var order = new AllfleXML.ID1Order.ID1Order
            {
                CUSTNMBR = "testing",
                CSTPONBR = "test1234",
                OrderDelivery = new OrderDelivery
                {
                    ShipToName = "Customer",
                    ADDRESS1 = "123 ABC St.",
                    ADDRESS2 = "SUITE 100",
                    CITY = "Dallas",
                    STATE = "TX",
                    ZIPCODE = "76021"
                },
                OrderLines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        ITEMNMBR = "APP-UTT",
                        QTYORDER = 60
                    }
                }
            };

            var doc = AllfleXML.ID1Order.Parser.Export(order);
            Assert.IsNotNull(doc);

            var isValid = AllfleXML.ID1Order.Parser.Validate(doc);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void SaveID1Order()
        {
            var order = new AllfleXML.ID1Order.ID1Order
            {
                CUSTNMBR = "testing",
                CSTPONBR = "test1234",
                OrderDelivery = new OrderDelivery
                {
                    ShipToName = "Customer",
                    ADDRESS1 = "123 ABC St.",
                    ADDRESS2 = "SUITE 100",
                    CITY = "Dallas",
                    STATE = "TX",
                    ZIPCODE = "76021"
                },
                OrderLines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        ITEMNMBR = "APP-UTT",
                        QTYORDER = 60
                    }
                }
            };

            var doc = AllfleXML.ID1Order.Parser.Export(order);
            Assert.IsNotNull(doc);

            var isValid1 = AllfleXML.ID1Order.Parser.Validate(doc);
            Assert.IsTrue(isValid1);
            
            const string fileName = "testid1Order.xml";

            order.Save(fileName);
            Assert.IsTrue(File.Exists(fileName));

            var tmp = AllfleXML.ID1Order.Parser.Import(fileName);
            Assert.IsNotNull(tmp);

            var isValid2 = AllfleXML.ID1Order.Parser.Validate(fileName);
            Assert.IsTrue(isValid2);

            File.Delete(fileName);
            Assert.IsFalse(File.Exists(fileName));
        }

        [TestMethod]
        public void FailedID1OrderValidation()
        {
            var isValid = AllfleXML.ID1Order.Parser.Validate(@"TestData\FlexOrder\sample0.xml");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void PassedID1OrderValidation()
        {
            var isValid = AllfleXML.ID1Order.Parser.Validate(@"TestData\ID1Order\sample2.xml");
            Assert.IsTrue(isValid);
        }
    }
}
