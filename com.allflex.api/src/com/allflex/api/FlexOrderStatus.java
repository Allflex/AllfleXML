package com.allflex.api;

import java.io.FileReader;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

public class FlexOrderStatus {
    public static class Parser
    {
        public static com.allflex.api.flexorderstatus.OrderStatusNode Import(String filePath)
        {
            try {
                JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
                Unmarshaller um = context.createUnmarshaller();
                return (com.allflex.api.flexorderstatus.OrderStatusNode) um.unmarshal(new FileReader(filePath));
            } catch (Exception ex) {
                Logger.getLogger(FlexOrder.class.getName()).log(Level.SEVERE, null, ex);
            }
            return null;
        }
        
        public static String Export(com.allflex.api.flexorderstatus.OrderStatusNode flexOrderStatus) throws JAXBException
        {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorderstatus.OrderStatusNode.class);
            return Common.asString(context, flexOrderStatus);
        }
        
        public static void Save(com.allflex.api.flexorderstatus.OrderStatusNode flexOrderStatus, String filePath) throws JAXBException
        {
            JAXBContext context = JAXBContext.newInstance(com.allflex.api.flexorder.Document.class);
            Common.toFile(context, flexOrderStatus, filePath);
        }
        
        public static boolean Validate(String filePath)
        {
            List<Exception> exceptions = Common.ValidateOnline(filePath, Config.FlexOrderStatusXSD.get());
            return (exceptions != null && exceptions.isEmpty());
        }
    }
}
