using System;
using System.Linq;
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
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportFlexOrder2()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample2.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportFlexOrder3()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample3.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportFlexOrder4()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample4.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportFlexOrder5()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample5.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportFlexOrder6()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample6.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportFlexOrder7()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample7.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportFlexOrder8()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample8.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportFlexOrder9()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample9.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ImportFlexOrder0()
        {
            var order = AllfleXML.FlexOrder.Parser.Import(@"TestData\FlexOrder\sample0.xml");
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Any());
        }

        [TestMethod]
        public void ExportFlexOrder()
        {
            var order = new AllfleXML.FlexOrder.OrderHeader() {CustomerNumber = "testing"};

            var doc = AllfleXML.FlexOrder.Parser.Export(order);
            Assert.IsNotNull(doc);

            var isValid = AllfleXML.FlexOrder.Parser.Validate(doc);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void FailedFlexOrderValidation()
        {
            var isValid = AllfleXML.FlexOrder.Parser.Validate(@"TestData\ID1Order\sample0.xml");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void PassedFlexOrderValidation()
        {
            var isValid = AllfleXML.FlexOrder.Parser.Validate(@"TestData\FlexOrder\sample0.xml");
            Assert.IsTrue(isValid);
        }
    }
}
