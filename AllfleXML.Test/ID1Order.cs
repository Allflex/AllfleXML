using System;
using System.Linq;
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
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportID1Order1()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample1.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportID1Order2()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample2.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportID1Order3()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample3.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportID1Order4()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample4.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportID1Order5()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample5.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportID1Order6()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample6.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportID1Order7()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample7.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportID1Order8()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample8.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportID1Order9()
        {
            var order = AllfleXML.ID1Order.Parser.Import(@"TestData\ID1Order\sample9.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ExportID1Order()
        {
            var order = new AllfleXML.ID1Order.ID1Order();

            var doc = AllfleXML.ID1Order.Parser.Export(order);
            Assert.IsNotNull(doc);
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
