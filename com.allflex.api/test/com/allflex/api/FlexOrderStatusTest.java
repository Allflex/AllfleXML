package com.allflex.api;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.GregorianCalendar;
import java.util.Locale;
import javax.xml.bind.JAXBException;
import javax.xml.datatype.DatatypeConfigurationException;
import javax.xml.datatype.DatatypeFactory;
import javax.xml.datatype.XMLGregorianCalendar;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

public class FlexOrderStatusTest {
    private static com.allflex.api.flexorderstatus.Document getOrderStatus() throws DatatypeConfigurationException, ParseException {
        com.allflex.api.flexorderstatus.ObjectFactory tmp = new com.allflex.api.flexorderstatus.ObjectFactory();
        
        com.allflex.api.flexorderstatus.ContainerNode container1 = tmp.createContainerNode();
        container1.setID("B123");
        container1.setType("Bag");
        
        com.allflex.api.flexorderstatus.TagManifest manifest1 = tmp.createTagManifest();
        manifest1.getContainers().add(container1);
        
        com.allflex.api.flexorderstatus.OrderLine orderLine1 = tmp.createOrderLine();
        orderLine1.setLineNumber(123456);
        orderLine1.setItemNumber("Test");
        orderLine1.setQuantity(3);
        orderLine1.setProgress(0);
        orderLine1.setStatus("Confirmed");
        orderLine1.setTagManifest(manifest1);
        
        //com.allflex.api.flexorderstatus.OrderLine orderLine2 = tmp.createOrderLine();
        
        com.allflex.api.flexorderstatus.Address address1 = tmp.createAddress();
        address1.setName("John H. Smith");
        address1.setAddress1("123 Main Street");
        address1.setCity("Everytown");
        address1.setState("TX");
        address1.setPostalCode("12345");
        address1.setCountry("USA");
        
        com.allflex.api.flexorderstatus.Shipment shipment1 = tmp.createShipment();
        shipment1.setShipMethod("UPS");
        shipment1.setShippingAccountNumber("98765432");
        shipment1.setFreightAmount(14.56);
        shipment1.setTrackingNumber("Z3242FFSD324326SA");
        GregorianCalendar c = new GregorianCalendar();
        c.setTime(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.ENGLISH).parse("1992-01-02 07:05:45"));
        XMLGregorianCalendar date2 = DatatypeFactory.newInstance().newXMLGregorianCalendar(c);
        shipment1.setShippingDate(date2);
        shipment1.setAddress(address1);
        shipment1.getOrderLines().add(orderLine1);
        //shipment1.getOrderLines().add(orderLine2);
        
        com.allflex.api.flexorderstatus.Messages messages = tmp.createMessages();
        messages.getErrorMessages().add("This is a test message");
        
        com.allflex.api.flexorderstatus.OrderStatus status = tmp.createOrderStatus();
        status.setWSOrderId("8432fe3a-ef38-45bf-af81-f73a5ae7eb8c");
        status.setPO("3432564");
        status.setMasterId(123456);
        status.setOrderId("CC123456");
        status.setStatus("Confirmed");
        status.setCustomerNumber("12345");
        status.setProgress(67);
        status.getShipments().add(shipment1);
        status.setMessages(messages);
        
        com.allflex.api.flexorderstatus.Document doc = tmp.createDocument();
        doc.getOrderStatuses().add(status);
        
        return doc;
    }
    
    public FlexOrderStatusTest() {
    }
    
    @BeforeClass // runs once before any test cases
    public static void setUpClass() {
    }
    
    @AfterClass // runs once after all test cases
    public static void tearDownClass() {
    }
    
    @Before // runs before each test case
    public void setUp() {
    }
    
    @After // runs after each test case
    public void tearDown() {
    }

    @Test
    public void ImportFlexOrderStatus1() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrderStatus\\sample1.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorderstatus.Document orderStatus = com.allflex.api.FlexOrderStatus.Parser.Import(path);
        assertNotNull(orderStatus);
        assertFalse(orderStatus.getOrderStatuses().isEmpty());
    }
    
    @Test
    public void ImportFlexOrderStatus2() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrderStatus\\sample2.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorderstatus.Document orderStatus = com.allflex.api.FlexOrderStatus.Parser.Import(path);
        assertNotNull(orderStatus);
        assertFalse(orderStatus.getOrderStatuses().isEmpty());
    }

    @Test
    public void ExportFlexOrderStatus() throws JAXBException, DatatypeConfigurationException, ParseException {
        com.allflex.api.flexorderstatus.Document doc = getOrderStatus();
        String content = com.allflex.api.FlexOrderStatus.Parser.Export(doc);
        
        boolean result = com.allflex.api.FlexOrderStatus.Parser.ValidateContent(content);
        assertTrue(result);
    }

    @Test
    public void SaveFlexOrderStatus() throws JAXBException, FileNotFoundException, DatatypeConfigurationException, ParseException {
        String fileName = "testFlexOrderStatus.xml";
        com.allflex.api.flexorderstatus.Document doc1 = getOrderStatus();
        
        com.allflex.api.FlexOrderStatus.Parser.Save(doc1, fileName);
        
        com.allflex.api.flexorderstatus.Document doc2 = com.allflex.api.FlexOrderStatus.Parser.Import(fileName);
        assertNotNull(doc2);
        assertFalse(doc2.getOrderStatuses().isEmpty());
    }

    @Test
    public void FailedFlexOrderStatusValidation() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\ID1Order\\sample0.xml";
        path = new java.io.File(path).getCanonicalPath();
        boolean result = com.allflex.api.FlexOrderStatus.Parser.Validate(path);
        assertFalse(result);
    }

    @Test
    public void PassedFlexOrderStatusValidation() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrderStatus\\sample1.xml";
        path = new java.io.File(path).getCanonicalPath();
        boolean result = com.allflex.api.FlexOrderStatus.Parser.Validate(path);
        assertTrue(result);
    }
}
