package com.allflex.api;

import java.io.FileReader;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

public class FlexOrderStatus {
    public static class Parser
    {
        public static com.allflex.api.flexorderstatus.OrderStatus Import(String filePath)
        {
            try {
                JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
                Unmarshaller um = context.createUnmarshaller();
                return (com.allflex.api.flexorderstatus.OrderStatus) um.unmarshal(new FileReader(filePath));
            } catch (Exception ex) {
                Logger.getLogger(FlexOrder.class.getName()).log(Level.SEVERE, null, ex);
            }
            return null;
        }
        
        public static String Export(com.allflex.api.flexorderstatus.OrderStatus flexOrderStatus) throws JAXBException
        {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorderstatus.OrderStatus.class);
            return Common.asString(context, flexOrderStatus);
        }
        
        public static void Save(com.allflex.api.flexorderstatus.OrderStatus flexOrderStatus, String filePath) throws JAXBException
        {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
            Common.toFile(context, flexOrderStatus, filePath);
        }
        
        public static void Validate(String filePath)
        {
            
        }
    }
}
