package com.allflex.api;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBElement;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

public class FlexSpec {
    public static class Parser
    {
        public static com.allflex.api.flexspec.Document Import(String filePath) throws FileNotFoundException {
            com.allflex.api.flexspec.ObjectFactory tmp = new com.allflex.api.flexspec.ObjectFactory();
            com.allflex.api.flexspec.Document result = tmp.createDocument();
            List<com.allflex.api.flexspec.Specification> sp = result.getSpecifications();
            
            List<com.allflex.api.flexspec.Specification> docs = ImportDocument(filePath);
            if(docs != null) {
                sp.addAll(docs);
            }
            
            com.allflex.api.flexspec.Specification spec = ImportSingle(filePath);
            if(spec != null) {
                sp.add(spec);
            }
            
            return result;
        }
        
        private static List<com.allflex.api.flexspec.Specification> ImportDocument(String filePath){
            try {
                JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexspec.ObjectFactory.class);
                Unmarshaller um = context.createUnmarshaller();
                FileInputStream fr = new FileInputStream(filePath);
                com.allflex.api.flexspec.Document doc = (com.allflex.api.flexspec.Document) um.unmarshal(fr);
                return doc.getSpecifications();
            } catch (Exception ex) {
                Logger.getLogger(FlexSpec.class.getName()).log(Level.SEVERE, null, ex);
            }
            
            return null;
        }
        
        private static com.allflex.api.flexspec.Specification ImportSingle(String filePath){
            try {
                JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexspec.ObjectFactory.class);
                Unmarshaller um = context.createUnmarshaller();
                FileInputStream fr = new FileInputStream(filePath);
                JAXBElement<com.allflex.api.flexspec.Specification> doc = (JAXBElement<com.allflex.api.flexspec.Specification>) um.unmarshal(fr);
                return doc.getValue();
            } catch (Exception ex) {
                Logger.getLogger(FlexSpec.class.getName()).log(Level.SEVERE, null, ex);
            }
            
            return null;
        }
        
        public static String Export(com.allflex.api.flexspec.Document flexSpec) throws JAXBException {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexspec.ObjectFactory.class);
            return Common.asString(context, flexSpec);
        }
        
        public static String Export(com.allflex.api.flexspec.Specification flexSpec) throws JAXBException {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexspec.ObjectFactory.class);
            return Common.asString(context, flexSpec);
        }
        
        public static void Save(com.allflex.api.flexspec.Document flexSpec, String filePath) throws JAXBException {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexspec.ObjectFactory.class);
            Common.toFile(context, flexSpec, filePath);
        }
        
        public static void Save(com.allflex.api.flexspec.Specification flexSpec, String filePath) throws JAXBException {
            com.allflex.api.flexspec.ObjectFactory tmp = new com.allflex.api.flexspec.ObjectFactory();
            com.allflex.api.flexspec.Document doc = tmp.createDocument();
            doc.getSpecifications().add(flexSpec);
            Save(doc, filePath);
        }
        
        public static boolean ValidateContent(String content) {
            List<Exception> exceptions = Common.ValidateContent(content, Config.FlexSpecXSD.get());
            return (exceptions != null && exceptions.isEmpty());
        }
        
        public static boolean Validate(String filePath) {
            List<Exception> exceptions = Common.ValidateOnline(filePath, Config.FlexSpecXSD.get());
            return (exceptions != null && exceptions.isEmpty());
        }
    }
}
