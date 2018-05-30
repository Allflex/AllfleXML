package com.allflex.api;

import java.io.IOException;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

public class FlexOrderStatusTest {
    
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
    public void ExportFlexOrderStatus() {
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    @Test
    public void SaveFlexOrderStatus() {
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
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
