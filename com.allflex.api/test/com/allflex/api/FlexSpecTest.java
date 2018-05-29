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
    public void ExportFlexSpec() throws JAXBException {
        com.allflex.api.flexspec.ObjectFactory tmp = new com.allflex.api.flexspec.ObjectFactory();
        
        com.allflex.api.flexspec.Faces faces = tmp.createFaces();
        faces.getFace().
        
        com.allflex.api.flexspec.Color color1 = tmp.createColor();
        color1.setColorCode("Y");
        color1.setName("Yellow");
        color1.setHexCode("e4d84d");
        
        com.allflex.api.flexspec.Color color2 = tmp.createColor();
        color2.setColorCode("B");
        color2.setName("Blue");
        color2.setHexCode("71cfeb");
        
        com.allflex.api.flexspec.Colors colors = tmp.createColors();
        colors.getColor().add(color1);
        colors.getColor().add(color2);
        
        com.allflex.api.flexspec.Component comp = tmp.createComponent();
        comp.setIndex(1);
        comp.setName("AS");
        comp.setProductLine("Allflex");
        comp.setSilhouette("TestSilhouette");
        comp.setOutline("TestOutline");
        comp.setColor("Y");
        comp.setColors(colors);
        comp.setFaces(faces);
        
        com.allflex.api.flexspec.Components comps = tmp.createComponents();
        comps.getComponent().add(comp);
        
        com.allflex.api.flexspec.SpecificationNode spec = tmp.createSpecificationNode();
        spec.setId("Testing");
        spec.setName("Testing_Elements");
        spec.setComponents(comps);
        
        com.allflex.api.flexspec.Document doc = tmp.createDocument();
        doc.getSpecification().add(spec);
        
        String content = com.allflex.api.FlexSpec.Parser.Export(doc);
        
        boolean result = com.allflex.api.FlexSpec.Parser.ValidateContent(content);
        assertTrue(result);
    }
    
    @Test
    public void SaveFlexSpec() throws JAXBException, FileNotFoundException{
        com.allflex.api.flexspec.ObjectFactory tmp = new com.allflex.api.flexspec.ObjectFactory();
        
        com.allflex.api.flexspec.OrderLineHeader orderLine = tmp.createOrderLineHeader();
        orderLine.setSkuName("ANTXLSET3306LA");
        orderLine.setQuantity(17);
        
        com.allflex.api.flexorder.OrderHeaderNode orderHeader = tmp.createOrderHeaderNode();
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
        orderHeader.getOrderLineHeader().add(orderLine);
        
        com.allflex.api.flexspec.Document doc1 = tmp.createDocument();
        doc1.getOrderHeader().add(orderHeader);
        
        String fileName = "testFlexSpec.xml";
        
        com.allflex.api.FlexSpec.Parser.Save(doc1, fileName);
        
        com.allflex.api.flexspec.Document doc2 = com.allflex.api.flexspec.Parser.Import(fileName);
        assertNotNull(doc2);
        assertFalse(doc2.getOrderHeader().isEmpty());
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
