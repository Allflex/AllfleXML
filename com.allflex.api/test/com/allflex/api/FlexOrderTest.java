package com.allflex.api;

import java.io.IOException;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

public class FlexOrderTest {
    
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
        assertFalse(order.getOrderHeader().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder2() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample2.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder3() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample3.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder4() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample4.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder5() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample5.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder6() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample6.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder7() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample7.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder8() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample8.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder9() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample9.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
        
    @Test
    public void ImportFlexOrder0() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample0.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
        
    @Test
    public void ImportFlexOrderEmptyUserDefined() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample11.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
    
    @Test
    public void ImportFlexOrderMini() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sampleMini.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
    
    @Test
    public void ImportFlexOrderMulti() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sampleMulti.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexorder.Document order = com.allflex.api.FlexOrder.Parser.Import(path);
        assertNotNull(order);
        assertFalse(order.getOrderHeader().isEmpty());
    }
    
    @Test
    public void ExportFlexOrder(){
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }
    
    @Test
    public void SaveFlexOrder(){
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
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
