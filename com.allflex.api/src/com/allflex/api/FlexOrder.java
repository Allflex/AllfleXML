package com.allflex.api;

import java.io.FileReader;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

public class FlexOrder {
    public static class Parser
    {
        public static com.allflex.api.flexorder.Document Import(String filePath)
        {
            try {
                JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
                Unmarshaller um = context.createUnmarshaller();
                return (com.allflex.api.flexorder.Document) um.unmarshal(new FileReader(filePath));
            } catch (Exception ex) {
                Logger.getLogger(FlexOrder.class.getName()).log(Level.SEVERE, null, ex);
            }
            return null;
        }
        
        public static String Export(com.allflex.api.flexorder.Document flexOrder) throws JAXBException
        {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
            return Common.asString(context, flexOrder);
        }
        
        public static String Export(com.allflex.api.flexorder.OrderHeaderNode flexOrder) throws JAXBException
        {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
            return Common.asString(context, flexOrder);
        }
        
        public static void Save(com.allflex.api.flexorder.Document flexOrder, String filePath) throws JAXBException
        {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
            Common.toFile(context, flexOrder, filePath);
        }
        
        public static void Save(com.allflex.api.flexorder.OrderHeaderNode flexOrder, String filePath) throws JAXBException
        {
            com.allflex.api.flexorder.Document doc = new com.allflex.api.flexorder.Document();
            doc.getOrderHeader().add(flexOrder);
            Save(doc, filePath);
        }
        
        public static boolean Validate(String filePath)
        {
            List<Exception> exceptions = Common.ValidateOnline(filePath, Config.FlexOrderXSD.get());
            return (exceptions != null && exceptions.isEmpty());
        }
    }
}
