package com.allflex.api;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

public class FlexOrderStatus {
    public static class Parser
    {
        public static com.allflex.api.flexorderstatus.Document Import(String filePath) throws FileNotFoundException {
            com.allflex.api.flexorderstatus.ObjectFactory tmp = new com.allflex.api.flexorderstatus.ObjectFactory();
            com.allflex.api.flexorderstatus.Document result = tmp.createDocument();
            List<com.allflex.api.flexorderstatus.OrderStatus> sp = result.getOrderStatuses();
            
            List<com.allflex.api.flexorderstatus.OrderStatus> docs = ImportDocument(filePath);
            if(docs != null) {
                sp.addAll(docs);
            }
            
            com.allflex.api.flexorderstatus.OrderStatus status = ImportSingle(filePath);
            if(status != null) {
                sp.add(status);
            }
            
            return result;
        }
        
        private static List<com.allflex.api.flexorderstatus.OrderStatus> ImportDocument(String filePath) {
            try {
                JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorderstatus.ObjectFactory.class);
                Unmarshaller um = context.createUnmarshaller();
                FileInputStream fr = new FileInputStream(filePath);
                com.allflex.api.flexorderstatus.Document doc = (com.allflex.api.flexorderstatus.Document) um.unmarshal(fr);
                return doc.getOrderStatuses();
            } catch (Exception ex) {
                Logger.getLogger(FlexOrderStatus.class.getName()).log(Level.SEVERE, null, ex);
            }
            return null;
        }
        
        private static com.allflex.api.flexorderstatus.OrderStatus ImportSingle(String filePath) {
            try {
                JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorderstatus.ObjectFactory.class);
                Unmarshaller um = context.createUnmarshaller();
                FileInputStream fr = new FileInputStream(filePath);
                return (com.allflex.api.flexorderstatus.OrderStatus) um.unmarshal(fr);
            } catch (Exception ex) {
                Logger.getLogger(FlexOrderStatus.class.getName()).log(Level.SEVERE, null, ex);
            }
            return null;
        }
        
        public static String Export(com.allflex.api.flexorderstatus.OrderStatusNode flexOrderStatus) throws JAXBException {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorderstatus.ObjectFactory.class);
            return Common.asString(context, flexOrderStatus);
        }
        
        public static void Save(com.allflex.api.flexorderstatus.OrderStatusNode flexOrderStatus, String filePath) throws JAXBException {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorderstatus.ObjectFactory.class);
            Common.toFile(context, flexOrderStatus, filePath);
        }
        
        public static boolean ValidateContent(String content) {
            List<Exception> exceptions = Common.ValidateContent(content, Config.FlexOrderStatusXSD.get());
            return (exceptions != null && exceptions.isEmpty());
        }
        
        public static boolean Validate(String filePath) {
            List<Exception> exceptions = Common.ValidateOnline(filePath, Config.FlexOrderStatusXSD.get());
            return (exceptions != null && exceptions.isEmpty());
        }
    }
}
