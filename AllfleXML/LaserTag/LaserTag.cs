﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AllfleXML.LaserTag
{
    [Obsolete("LaserTag.Parser is deprecated, please use FlexOrder.Parser instead.")]
    public class Parser
    {
        private const string OrderHeaderLineType = "SOH";
        private const string OrderLineHeaderLineType = "SOD";
        private const string OrderLineItemLineType = "LTD";

        public static List<OrderHeader> Import(string xmlFilePath)
        {
            var document = XDocument.Load(xmlFilePath);
            if (!Validate(document))
            {
                throw new XmlSchemaValidationException("XML Document is invalid");
            }

            List<OrderHeader> result;

            var serializer = new XmlSerializer(typeof(List<OrderHeader>));
            using (var reader = XmlReader.Create(xmlFilePath))
            {
                result = (List<OrderHeader>)serializer.Deserialize(reader);
            }

            return result;
        }

        public static string Export(List<OrderHeader> orders)
        {
            throw new NotImplementedException();
        }

        public static bool Validate(string xmlFilePath)
        {
            return Validate(XDocument.Load(xmlFilePath));
        }

        public static bool Validate(XDocument xml)
        {
            var xsDocument = new XmlSchemaSet();
            var assembly = Assembly.Load("AllfleXML");
            using (var stream = assembly.GetManifestResourceStream("AllfleXML.LaserTag.LaserTag.xsd"))
            using (var reader = new StreamReader(stream))
            {
                xsDocument.Add(null, XmlReader.Create(reader));
            }

            var isValid = true;
            xml.Validate(xsDocument, (o, e) => { isValid = false; });
            return isValid;
        }

        public static List<OrderHeader> ImportDAT(string fileName)
        {
            var orders = new List<OrderHeader>();
            
            string line;
            OrderHeader orderHeader = null;
            OrderLineHeader orderLineHeader = null;

            // Read the file and parse it line by line.
            var enc = Encoding.GetEncoding(1252);
            using (var file = new StreamReader(fileName, enc))
                while ((line = file.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.Length < 3) continue;
                    var lineType = line.Substring(0, 3);

                    // If the line starts with SOH
                    if (lineType.Equals(OrderHeaderLineType))
                    {
                        orderHeader = ParseHeaderString(line);
                        orders.Add(orderHeader);
                        continue;
                    }

                    if (orderHeader == null)
                    {
                        throw new Exception("The file is in the incorrect format.", new Exception("SOH Header line not included before SOD or LTD lines."));
                    }

                    // If the line starts with SOD
                    if (lineType.Equals(OrderLineHeaderLineType))
                    {
                        orderLineHeader = ParseOrderLineHeaderString(line);
                        orderHeader.OrderLineHeaders.Add(orderLineHeader);

                        continue;
                    }

                    if (orderLineHeader == null)
                    {
                        throw new Exception("The file is in the incorrect format.", new Exception("SOD Header line not included before LTD lines."));
                    }

                    // If the line starts with LTD
                    if (lineType.Equals(OrderLineItemLineType))
                    {
                        var templateDetail = ParseOrderLineString(line);
                        orderLineHeader.OrderLineTemplateDetails.Add(templateDetail);
                        continue;
                    }
                }

            return orders;
        }

        private static OrderHeader ParseHeaderString(string orderString)
        {
            var fields = orderString.Split('þ'); // "https://en.wikipedia.org/wiki/Thorn_(letter)"

            var orderLineType = fields[0]; // should be SOH;

            if (!orderLineType.Equals(OrderHeaderLineType)) throw new Exception("That is not the header string!");
            var result = new OrderHeader();

            if (!fields.Any()) return result;

            result.OrderLineHeaders = new List<OrderLineHeader>();

            if (fields.Length > 1)
                result.CustomerNumber = fields[1];

            if (fields.Length > 2)
                result.OrderDate = fields[2];

            if (fields.Length > 3)
                result.OrderBy = fields[3];

            if (fields.Length > 4)
            {
                result.Comments = fields[4];

                // TODO: IMPROVE
                var premiseId = result.Comments.Replace("PIN:", "");
                if (premiseId.Contains("NEED PREMISE ID FOR NEXT ORDER") || premiseId.Contains("Replacement Tag Order for PO:") || premiseId.Contains("REPLACEMENT TAGS"))
                {
                    premiseId = string.Empty;
                }
                premiseId = premiseId.Replace("PIN-", "").Replace(" ", "").Trim();

                result.PremiseId = premiseId;
            }
            if (fields.Length > 5)
                result.ShipVia = fields[5];

            if (fields.Length > 6)
                result.ShipComp = fields[6];

            if (fields.Length > 7)
                result.POBPoint = fields[7];

            if (fields.Length > 8)
                result.TermsCode = fields[8];

            if (fields.Length > 9)
                result.PO = fields[9];

            if (fields.Length > 10)
                result.PickDate = fields[10];

            if (fields.Length > 11)
                result.ExtraChargeCode = fields[11];

            if (fields.Length > 12)
                result.ExtraChargeAccount = fields[12];

            if (fields.Length > 13)
                result.TaxCode = fields[13];

            if (fields.Length > 14)
                result.SalesRepCode = fields[14];

            if (fields.Length > 15)
                result.OrderType = fields[15];

            if (fields.Length > 16)
                result.OrderClass = fields[16];

            if (fields.Length > 17)
                result.SalesRepCommission = fields[17];

            if (fields.Length > 18)
                result.CommissionCode = fields[18];

            if (fields.Length > 19)
                result.PickPriorityCode = fields[19];

            if (fields.Length > 20)
                result.DueDate = fields[20];

            if (fields.Length > 21)
                result.DivisionCode = fields[21];

            if (fields.Length > 22)
                result.UnknownFeild01 = fields[22];

            if (fields.Length > 23)
                result.UnknownFeild02 = fields[23];

            if (fields.Length > 24)
                result.UnknownFeild03 = fields[24];

            if (fields.Length > 25)
                result.UnknownFeild04 = fields[25];

            if (fields.Length > 26)
                result.UnknownFeild05 = fields[26];
            if (fields.Length > 27)
                result.UnknownFeild06 = fields[27];

            if (fields.Length > 28)
                result.UnknownFeild07 = fields[28];

            if (fields.Length > 29)
                result.UnknownFeild08 = fields[29];

            if (fields.Length > 30)
                result.UnknownFeild09 = fields[30];

            if (fields.Length > 31)
                result.UnknownFeild10 = fields[31];

            if (fields.Length > 32)
                result.UnknownFeild11 = fields[32];

            if (fields.Length > 33)
                result.UnknownFeild12 = fields[33];

            if (fields.Length > 34)
                result.BookFacCode = fields[34];

            if (fields.Length > 35)
                result.DirShipFlag = fields[35];

            if (fields.Length > 36)
                result.ShipToName = fields[36];

            if (fields.Length > 37)
                result.ShipToAddress1 = fields[37];

            if (fields.Length > 38)
                result.ShipToAddress2 = fields[38];

            if (fields.Length > 39)
                result.ShipToCity = fields[39];

            if (fields.Length > 40)
                result.ShipToState = fields[40];

            if (fields.Length > 41)
                result.ShipToZipCode = fields[41];

            if (fields.Length > 42)
                result.ShipToCountry = fields[42];

            if (fields.Length > 43)
                result.ShipToCode = fields[43];

            if (fields.Length > 44)
                result.RushOrder = fields[44];
            
            return result;
        }

        private static OrderLineHeader ParseOrderLineHeaderString(string orderString)
        {
            var fields = orderString.Split('þ');

            var orderLineType = fields[0]; // should be SOD;

            if (!orderLineType.Equals(OrderLineHeaderLineType)) throw new Exception("That is not an order line header string!");

            var result = new OrderLineHeader();

            if (!fields.Any()) return result;

            result.OrderLineTemplateDetails = new List<LineTemplateDetail>();

            if (fields.Length > 1)
                result.RequiredDeliveryDate = fields[1];

            if (fields.Length > 2)
                result.QTY = fields[2];

            if (fields.Length > 3)
                result.SellingPrice = fields[3];

            if (fields.Length > 4)
                result.Description = fields[4];

            if (fields.Length > 5)
                result.SKU = fields[5];

            if (fields.Length > 6)
                result.SODShipComp = fields[6];

            if (fields.Length > 7)
                result.CatalogCode = fields[7];

            if (fields.Length > 8)
                result.DeliveryRemarks = fields[8];

            if (fields.Length > 9)
                result.TaxFlag = fields[9];

            if (fields.Length > 10)
                result.SkipAllocationFlag = fields[10];

            if (fields.Length > 11)
                result.ListPrice = fields[11];

            if (fields.Length > 12)
                result.SODCommissionFlag = fields[12];

            if (fields.Length > 13)
                result.SODCommissionCode = fields[13];

            if (fields.Length > 14)
                result.ItemsTerms = fields[14];

            if (fields.Length > 15)
                result.MgtNumber = fields[15];

            if (fields.Length > 16)
                result.OptionOne = fields[16];

            if (fields.Length > 17)
                result.OptionTwo = fields[17];

            if (fields.Length > 18)
                result.OptionThree = fields[18];

            if (fields.Length > 19)
                result.OptionFour = fields[19];

            if (fields.Length > 20)
                result.OptionFive = fields[20];
            
            return result;
        }

        private static LineTemplateDetail ParseOrderLineString(string orderString)
        {
            var fields = orderString.Split(',');

            var orderLineType = fields[0]; // should be LTD;

            if (!orderLineType.Equals(OrderLineItemLineType)) throw new Exception("That is not an order line string!");

            var result = new LineTemplateDetail();

            if (!fields.Any()) return result;

            if (fields.Length > 1)
                result.SKU = fields[1];
            
            if (fields.Length > 2)
            {
                var vars = fields.ToList();
                vars.RemoveAt(0);

                result.Variables = vars.ToArray();
            }

            return result;
        }
    }
    
    [Serializable]
    [XmlRoot]
    //[XmlRoot(Namespace = "http://localhost/", IsNullable = false)]
    public class Document
    {
        public List<OrderHeader> OrderHeader { get; set; }
    }

    [Serializable]
    [Obsolete("LaserTag.OrderHeader is deprecated, please use FlexOrder.OrderHeader instead.")]
    public class OrderHeader
    {
        public List<OrderLineHeader> OrderLineHeaders = new List<OrderLineHeader>();
        public string CustomerNumber { get; set; }
        public string OrderDate { get; set; }
        public string OrderBy { get; set; }
        public string PremiseId { get; set; }
        public string Comments { get; set; }
        public string ShipVia { get; set; }
        public string ShipComp { get; set; }
        public string POBPoint { get; set; }
        public string TermsCode { get; set; }
        public string PO { get; set; }
        public string PickDate { get; set; }
        public string ExtraChargeCode { get; set; }
        public string ExtraChargeAccount { get; set; }
        public string TaxCode { get; set; }
        public string SalesRepCode { get; set; }
        public string OrderType { get; set; }
        public string OrderClass { get; set; }
        public string SalesRepCommission { get; set; }
        public string CommissionCode { get; set; }
        public string PickPriorityCode { get; set; }
        public string DueDate { get; set; }
        public string DivisionCode { get; set; }
        public string UnknownFeild01 { get; set; }
        public string UnknownFeild02 { get; set; }
        public string UnknownFeild03 { get; set; }
        public string UnknownFeild04 { get; set; }
        public string UnknownFeild05 { get; set; }
        public string UnknownFeild06 { get; set; }
        public string UnknownFeild07 { get; set; }
        public string UnknownFeild08 { get; set; }
        public string UnknownFeild09 { get; set; }
        public string UnknownFeild10 { get; set; }
        public string UnknownFeild11 { get; set; }
        public string UnknownFeild12 { get; set; }
        public string BookFacCode { get; set; }
        public string DirShipFlag { get; set; }
        public string ShipToName { get; set; }
        public string ShipToAddress1 { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToZipCode { get; set; }
        public string ShipToCountry { get; set; }
        public string ShipToCode { get; set; }
        public string RushOrder { get; set; }
    }

    [Serializable]
    [Obsolete("LaserTag.OrderLineHeader is deprecated, please use FlexOrder.OrderLineHeader instead.")]
    public class OrderLineHeader
    {
        public List<LineTemplateDetail> OrderLineTemplateDetails = new List<LineTemplateDetail>();
        public string RequiredDeliveryDate { get; set; }
        public string QTY { get; set; }
        public int Quantity { get; set; }
        public string SellingPrice { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string SODShipComp { get; set; }
        public string CatalogCode { get; set; }
        public string DeliveryRemarks { get; set; }
        public string TaxFlag { get; set; }
        public string SkipAllocationFlag { get; set; }
        public string ListPrice { get; set; }
        public string SODCommissionFlag { get; set; }
        public string SODCommissionCode { get; set; }
        public string ItemsTerms { get; set; }
        public string MgtNumber { get; set; }
        public string OptionOne { get; set; }
        public string OptionTwo { get; set; }
        public string OptionThree { get; set; }
        public string OptionFour { get; set; }
        public string OptionFive { get; set; }
    }

    [Serializable]
    [Obsolete("LaserTag.LineTemplateDetail is deprecated, please use FlexOrder.LineTemplateDetail instead.")]
    public class LineTemplateDetail
    {
        public string SKU { get; set; }
        public string[] Variables { get; set; }
    }
}