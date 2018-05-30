package com.allflex.api;

import java.io.FileNotFoundException;
import java.io.IOException;
import javax.xml.bind.JAXBException;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

public class FlexOrderTest {
    
    private static com.allflex.api.flexorder.Document getOrder() {
        com.allflex.api.flexorder.ObjectFactory tmp = new com.allflex.api.flexorder.ObjectFactory();
        
        com.allflex.api.flexorder.OrderLineHeader orderLine = tmp.createOrderLineHeader();
        orderLine.setSkuName("ANTXLSET3306LA");
        orderLine.setQuantity(17);
        
        com.allflex.api.flexorder.OrderHeader orderHeader = tmp.createOrderHeader();
        orderHeader.setCustomerNumber("testing");
        // TODO: Set premise
        orderHeader.setPO("123456");
        orderHeader.setShipToName("Jane Doe");
        orderHeader.setShipToPhone("5551234567");
        orderHeader.setShipToAddress1("Rev. Calvin & Thelma Alcorn");
        orderHeader.setShipToCity("Dallas");
        orderHeader.setShipToState("TX");
        orderHeader.setShipToPostalCode("76021");
        orderHeader.setShipToCountry("US");
        orderHeader.setShipMethod("UPS");
        orderHeader.getOrderLineHeaders().add(orderLine);
        
        com.allflex.api.flexorder.Document doc = tmp.createDocument();
        doc.getOrderHeaders().add(orderHeader);
        
        return doc;
    }
    
    public FlexOrderTest() {
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
    }
    
    @After
    public void tearDown() {
    }

    @Test
    public void ImportFlexOrder1() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample1.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder2() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample2.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder3() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample3.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder4() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample4.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder5() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample5.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder6() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample6.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder7() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample7.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder8() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample8.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder9() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample9.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder0() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample0.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
        
    @Test
    public void ImportFlexOrderEmptyUserDefined() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample11.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
    
    @Test
    public void ImportFlexOrderMini() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sampleMini.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
    
    @Test
    public void ImportFlexOrderMulti() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sampleMulti.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeaders().isEmpty());
    }
    
    @Test
    public void ExportFlexOrder() throws JAXBException {
        com.allflex.api.flexorder.Document doc = getOrder();
        String content = com.allflex.api.FlexOrder.Parser.Export(doc);
        
        boolean result = com.allflex.api.FlexOrder.Parser.ValidateContent(content);
        assertTrue(result);
    }
    
    @Test
    public void SaveFlexOrder() throws JAXBException, FileNotFoundException{
        String fileName = "testFlexOrder.xml";
        com.allflex.api.flexorder.Document doc1 = getOrder();
        
        com.allflex.api.FlexOrder.Parser.Save(doc1, fileName);
        
        com.allflex.api.flexorder.Document doc2 = com.allflex.api.FlexOrder.Parser.Import(fileName);
        assertNotNull(doc2);
        assertFalse(doc2.getOrderHeaders().isEmpty());
    }

    @Test
    public void FailedFlexOrderValidation() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\ID1Order\\sample0.xml";
        path = new java.io.File(path).getCanonicalPath();
        boolean result = com.allflex.api.FlexOrder.Parser.Validate(path);
        assertFalse(result);
    }

    @Test
    public void PassedFlexOrderValidation() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample1.xml";
        path = new java.io.File(path).getCanonicalPath();
        boolean result = com.allflex.api.FlexOrder.Parser.Validate(path);
        assertTrue(result);
    }
}
