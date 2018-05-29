package com.allflex.api;

import java.io.FileReader;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

public class FlexSpec {
    public static class Parser
    {
        public static com.allflex.api.flexspec.Document Import(String filePath)
        {
            try {
                JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
                Unmarshaller um = context.createUnmarshaller();
                return (com.allflex.api.flexspec.Document) um.unmarshal(new FileReader(filePath));
            } catch (Exception ex) {
                Logger.getLogger(FlexOrder.class.getName()).log(Level.SEVERE, null, ex);
            }
            return null;
        }
        
        public static String Export(com.allflex.api.flexspec.Document flexSpec) throws JAXBException
        {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
            return Common.asString(context, flexSpec);
        }
        
        public static String Export(com.allflex.api.flexspec.SpecificationNode flexSpec) throws JAXBException
        {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
            return Common.asString(context, flexSpec);
        }
        
        public static void Save(com.allflex.api.flexspec.Document flexSpec, String filePath) throws JAXBException
        {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
            Common.toFile(context, flexSpec, filePath);
        }
        
        public static void Save(com.allflex.api.flexspec.SpecificationNode flexSpec, String filePath) throws JAXBException
        {
            com.allflex.api.flexspec.Document doc = new com.allflex.api.flexspec.Document();
            doc.getSpecification().add(flexSpec);
            Save(doc, filePath);
        }
        
        public static void Validate(String filePath)
        {
            
        }
    }
}
