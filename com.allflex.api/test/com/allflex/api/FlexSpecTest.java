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
    
    private static com.allflex.api.flexspec.Document getSpecification() {
        
        com.allflex.api.flexspec.ObjectFactory tmp = new com.allflex.api.flexspec.ObjectFactory();
        
        com.allflex.api.flexspec.Variable f1v1 = tmp.createVariable();
        f1v1.setIndex(1);
        f1v1.setName("var1");
        f1v1.setDescription("var1");
        f1v1.setRole("Text");
        f1v1.setType("Text");
        f1v1.setWidth(56);
        f1v1.setHeight(12);
        f1v1.setPositionX(8);
        f1v1.setPositionY(7);
        f1v1.setDefaultValue("var1");
        f1v1.setValueFormat("");
        f1v1.setFontSize("20");
        f1v1.setIsFixed(false);
        f1v1.setIsLaser(true);
        f1v1.setIsInk(false);
        f1v1.setRadius(0);
        f1v1.setCurveTextAttachTo(null);
        
        com.allflex.api.flexspec.Variable f1v2 = tmp.createVariable();
        f1v2.setIndex(2);
        f1v2.setName("var2");
        f1v2.setDescription("var2");
        f1v2.setRole("Text");
        f1v2.setType("Text");
        f1v2.setWidth(56);
        f1v2.setHeight(12);
        f1v2.setPositionX(8);
        f1v2.setPositionY(7);
        f1v2.setDefaultValue("var2");
        f1v2.setValueFormat("");
        f1v2.setFontSize("20");
        f1v2.setIsFixed(false);
        f1v2.setIsLaser(true);
        f1v2.setIsInk(false);
        f1v2.setRadius(0);
        f1v2.setCurveTextAttachTo(null);
        
        com.allflex.api.flexspec.Variables f1variables = tmp.createVariables();
        f1variables.getVariable().add(f1v1);
        f1variables.getVariable().add(f1v2);
                
        com.allflex.api.flexspec.Face face1 = tmp.createFace();
        face1.setName("Front");
        face1.setVariables(f1variables);
        
        com.allflex.api.flexspec.Variable f2v1 = tmp.createVariable();
        f2v1.setIndex(3);
        f2v1.setName("var3");
        f2v1.setDescription("var3");
        f2v1.setRole("Text");
        f2v1.setType("Text");
        f2v1.setWidth(56);
        f2v1.setHeight(12);
        f2v1.setPositionX(8);
        f2v1.setPositionY(7);
        f2v1.setDefaultValue("var3");
        f2v1.setValueFormat("");
        f2v1.setFontSize("20");
        f2v1.setIsFixed(false);
        f2v1.setIsLaser(true);
        f2v1.setIsInk(false);
        f2v1.setRadius(0);
        f2v1.setCurveTextAttachTo(null);
        
        com.allflex.api.flexspec.Variable f2v2 = tmp.createVariable();
        f2v2.setIndex(4);
        f2v2.setName("var4");
        f2v2.setDescription("var4");
        f2v2.setRole("Text");
        f2v2.setType("Text");
        f2v2.setWidth(56);
        f2v2.setHeight(12);
        f2v2.setPositionX(8);
        f2v2.setPositionY(7);
        f2v2.setDefaultValue("var4");
        f2v2.setValueFormat("");
        f2v2.setFontSize("20");
        f2v2.setIsFixed(false);
        f2v2.setIsLaser(true);
        f2v2.setIsInk(false);
        f2v2.setRadius(0);
        f2v2.setCurveTextAttachTo(null);
        
        com.allflex.api.flexspec.Variables f2variables = tmp.createVariables();
        f1variables.getVariable().add(f2v1);
        f1variables.getVariable().add(f2v2);
        
        com.allflex.api.flexspec.Face face2 = tmp.createFace();
        face2.setName("Back");
        face2.setVariables(f2variables);
        
        com.allflex.api.flexspec.Faces faces = tmp.createFaces();
        faces.getFace().add(face1);
        faces.getFace().add(face2);
        
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
        
        return doc;
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
        com.allflex.api.flexspec.Document spec = getSpecification();
        String content = com.allflex.api.FlexSpec.Parser.Export(spec);
        
        boolean result = com.allflex.api.FlexSpec.Parser.ValidateContent(content);
        assertTrue(result);
    }
    
    @Test
    public void SaveFlexSpec() throws JAXBException, FileNotFoundException{
        String fileName = "testFlexSpec.xml";
        com.allflex.api.flexspec.Document spec = getSpecification();
        
        com.allflex.api.FlexSpec.Parser.Save(spec, fileName);
        
        com.allflex.api.flexspec.Document spec2 = com.allflex.api.FlexSpec.Parser.Import(fileName);
        assertNotNull(spec2);
        assertFalse(spec2.getSpecification().isEmpty());
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
