package com.allflex.api;

import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.util.Properties;
import java.util.logging.Level;
import java.util.logging.Logger;

public class Configuration {
    private URL configFile;
    
    private String flexOrderXSD;
    private String flexOrderStatusXSD;
    private String flexSpecXSD;
    
    public Configuration(){
        configFile = Configuration.class.getClass().getResource("/Configuration.config");
        
        try {
            InputStream configFileStream = configFile.openStream();
            Properties p = new Properties();
            p.load(configFileStream);
            configFileStream.close();
            
            this.flexOrderXSD = (String)p.get("FlexOrderXSD");
            this.flexOrderStatusXSD = (String)p.get("FlexOrderStatusXSD");
            this.flexSpecXSD = (String)p.get("FlexSpecXSD");
             
        } catch (IOException ex) {  //IO or NullPointer exceptions possible in block above
            Logger.getLogger(Common.class.getName()).log(Level.SEVERE, null, ex);
            System.exit(1);
        }
    }
    
    public String getFlexOrderXSD(){
        return this.flexOrderXSD;
    }
    
    public String getFlexOrderStatusXSD(){
        return this.flexOrderStatusXSD;
    }
    
    public String getFlexSpecXSD(){
        return this.flexSpecXSD;
    }
}