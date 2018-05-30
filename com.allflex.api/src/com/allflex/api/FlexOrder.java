package com.allflex.api;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

public class FlexOrder {
    public static class Parser
    {
        public static com.allflex.api.flexorder.Document Import(String filePath) throws FileNotFoundException {
            com.allflex.api.flexorder.ObjectFactory tmp = new com.allflex.api.flexorder.ObjectFactory();
            com.allflex.api.flexorder.Document result = tmp.createDocument();
            List<com.allflex.api.flexorder.OrderHeader> ord = result.getOrderHeaders();
            
            List<com.allflex.api.flexorder.OrderHeader> docs = ImportDocument(filePath);
            if(docs != null) {
                ord.addAll(docs);
            }
            
            com.allflex.api.flexorder.OrderHeader order = ImportSingle(filePath);
            if(order != null) {
                ord.add(order);
            }
            
            return result; 
        }
        
        private static List<com.allflex.api.flexorder.OrderHeader> ImportDocument(String filePath) {
            try {
                JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.ObjectFactory.class);
                Unmarshaller um = context.createUnmarshaller();
                FileInputStream fr = new FileInputStream(filePath);
                com.allflex.api.flexorder.Document doc = (com.allflex.api.flexorder.Document) um.unmarshal(fr);
                return doc.getOrderHeaders();
            } catch (Exception ex) {
                Logger.getLogger(FlexOrder.class.getName()).log(Level.SEVERE, null, ex);
            }
            return null;
        }
        
        private static com.allflex.api.flexorder.OrderHeader ImportSingle(String filePath) {
            try {
                JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.ObjectFactory.class);
                Unmarshaller um = context.createUnmarshaller();
                FileInputStream fr = new FileInputStream(filePath);
                return (com.allflex.api.flexorder.OrderHeader) um.unmarshal(fr);
            } catch (Exception ex) {
                Logger.getLogger(FlexOrder.class.getName()).log(Level.SEVERE, null, ex);
            }
            return null;
        }
        
        public static String Export(com.allflex.api.flexorder.Document flexOrder) throws JAXBException {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.ObjectFactory.class);
            return Common.asString(context, flexOrder);
        }
        
        public static String Export(com.allflex.api.flexorder.OrderHeader flexOrder) throws JAXBException {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.ObjectFactory.class);
            return Common.asString(context, flexOrder);
        }
        
        public static void Save(com.allflex.api.flexorder.Document flexOrder, String filePath) throws JAXBException {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.ObjectFactory.class);
            Common.toFile(context, flexOrder, filePath);
        }
        
        public static void Save(com.allflex.api.flexorder.OrderHeader flexOrder, String filePath) throws JAXBException {
            com.allflex.api.flexorder.ObjectFactory tmp = new com.allflex.api.flexorder.ObjectFactory();
            com.allflex.api.flexorder.Document doc = tmp.createDocument();
            doc.getOrderHeaders().add(flexOrder);
            Save(doc, filePath);
        }
        
        public static boolean ValidateContent(String content) {
            List<Exception> exceptions = Common.ValidateContent(content, Config.FlexOrderXSD.get());
            return (exceptions != null && exceptions.isEmpty());
        }
        
        public static boolean Validate(String filePath) {
            List<Exception> exceptions = Common.ValidateOnline(filePath, Config.FlexOrderXSD.get());
            return (exceptions != null && exceptions.isEmpty());
        }
    }
}
