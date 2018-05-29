package com.allflex.api;

import java.io.File;
import java.io.IOException;
import java.io.StringWriter;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.LinkedList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.XMLConstants;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Marshaller;
import javax.xml.transform.Source;
import javax.xml.transform.stream.StreamSource;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;
import javax.xml.validation.Validator;
import org.xml.sax.ErrorHandler;
import org.xml.sax.SAXException;

public class Common {
    private static List<Exception> Validate(Source xml, Source xsd){
        List<Exception> result = new LinkedList<>();
        try {
            SchemaFactory factory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
            Schema schema = factory.newSchema(xsd);
            result = Validate(xml, schema);
        } catch (SAXException ex) {
            Logger.getLogger(Common.class.getName()).log(Level.SEVERE, null, ex);
            result.add(ex);
        }
        return result;
    }
    
    private static List<Exception> Validate(Source xml, URL xsd){
        List<Exception> result = new LinkedList<>();
        try {
            SchemaFactory factory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
            Schema schema = factory.newSchema(xsd);
            result = Validate(xml, schema);
        } catch (SAXException ex) {
            Logger.getLogger(Common.class.getName()).log(Level.SEVERE, null, ex);
            result.add(ex);
        }
        return result;
    }
    
    private static List<Exception> Validate(Source xml, Schema xsd){
        final List<Exception> exceptions = new LinkedList<>();

        try {
            Validator validator = xsd.newValidator();

            validator.setErrorHandler(new ErrorHandler() {
                @Override
                public void warning(org.xml.sax.SAXParseException ex) throws SAXException {
                    exceptions.add(ex);
                    Logger.getLogger(Common.class.getName()).log(Level.SEVERE, null, ex);
                }

                @Override
                public void error(org.xml.sax.SAXParseException ex) throws SAXException {
                    exceptions.add(ex);
                    Logger.getLogger(Common.class.getName()).log(Level.SEVERE, null, ex);
                }

                @Override
                public void fatalError(org.xml.sax.SAXParseException ex) throws SAXException {
                    exceptions.add(ex);
                    Logger.getLogger(Common.class.getName()).log(Level.SEVERE, null, ex);
                }
            });

            validator.validate(xml);
        } catch (SAXException | IOException ex) {
            exceptions.add(ex);
            Logger.getLogger(Common.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        return exceptions;
    }
    
    public static List<Exception> Validate(File xmlFile, URL xsdLink) {
        StreamSource xmlValue = new StreamSource(xmlFile);
        return Validate(xmlValue, xsdLink);
    }
    
    public static List<Exception> Validate(File xmlFile, File xsdFile) {
        StreamSource xml = new StreamSource(xmlFile);
        StreamSource xsd = new StreamSource(xsdFile);
        return Validate(xml, xsd);
    }
    
    public static List<Exception> ValidateOnline(String xml, String xsd){
        // TODO: Determine if xsd is URL or file path
        Schema schema = null;
        
        try {
            URL schemaFile = new URL("http://host:port/filename.xsd");
            // webapp example xsd:
            // URL schemaFile = new URL("http://java.sun.com/xml/ns/j2ee/web-app_2_4.xsd");
            // local file example:
            // File schemaFile = new File("/location/to/localfile.xsd"); // etc.
            
            SchemaFactory schemaFactory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
            schema = schemaFactory.newSchema(schemaFile);
        } catch (MalformedURLException ex) {
            Logger.getLogger(Common.class.getName()).log(Level.SEVERE, null, ex);
        } catch (SAXException ex) {
            Logger.getLogger(Common.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        Source xmlFile = new StreamSource(new File(xml));
        return Validate(xmlFile, schema);
    }
    
    public static void toFile(JAXBContext pContext, Object pObject, String output) throws JAXBException {
        toFile(pContext, pObject, new File(output));
    }
    
    public static void toFile(JAXBContext pContext, Object pObject, File output) throws JAXBException {
        Marshaller m = pContext.createMarshaller();
        m.setProperty(Marshaller.JAXB_ENCODING, "UTF-8");
        m.setProperty(Marshaller.JAXB_FORMATTED_OUTPUT, Boolean.TRUE);
        m.marshal(pObject, output);
    }
    
    public static String asString(JAXBContext pContext, Object pObject) throws JAXBException {
        StringWriter sw = new StringWriter();

        Marshaller m = pContext.createMarshaller();
        m.setProperty(Marshaller.JAXB_ENCODING, "UTF-8");
        m.setProperty(Marshaller.JAXB_FORMATTED_OUTPUT, Boolean.TRUE);
        m.marshal(pObject, sw);

        return sw.toString();
    }
}
