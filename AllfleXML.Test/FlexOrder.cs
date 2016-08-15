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
            throw new NotImplementedException();
        }

        [TestMethod]
        public void FailedFlexOrderValidation()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PassedFlexOrderValidation()
        {
            throw new NotImplementedException();
        }
    }
}
