package com.allflex.api;

import java.io.IOException;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

public class FlexSpecTest {
    
    public FlexSpecTest() {
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
    public void ImportFlexSpec2() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexSpec\\GTLF2_GSM2.xml";
        path = new java.io.File(path).getCanonicalPath();
        com.allflex.api.flexspec.Document specifications = com.allflex.api.FlexSpec.Parser.Import(path);
        assertNotNull(specifications);
        assertFalse(specifications.getSpecification().isEmpty());
    }

    @Test
    public void SaveFlexSpec() {
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    @Test
    public void ExportFlexSpec() {
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    @Test
    public void FailedFlexSpecValidation() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexOrder\\sample0.xml";
        path = new java.io.File(path).getCanonicalPath();
        boolean result = com.allflex.api.FlexSpec.Parser.Validate(path);
        assertFalse(result);
    }

    @Test
    public void PassedFlexSpecValidation() throws IOException {
        String path = "..\\AllfleXML.Test\\TestData\\FlexSpec\\GTLF2_GSM2.xml";
        path = new java.io.File(path).getCanonicalPath();
        boolean result = com.allflex.api.FlexSpec.Parser.Validate(path);
        assertTrue(result);
    }
}
