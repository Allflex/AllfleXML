package com.allflex.api;

import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.util.Properties;
import java.util.logging.Level;
import java.util.logging.Logger;

public enum Config {
    FlexOrderXSD,
    FlexOrderStatusXSD,
    FlexSpecXSD;
    
    String get(){
        try{
            Properties p = getProperties();
            switch(this){
                case FlexOrderXSD:
                    return (String)p.get("FlexOrderXSD");
                case FlexOrderStatusXSD:
                    return (String)p.get("FlexOrderStatusXSD");
                case FlexSpecXSD:
                    return (String)p.get("FlexSpecXSD");
            }
        } catch (IOException ex) {  //IO or NullPointer exceptions possible in block above
            Logger.getLogger(Common.class.getName()).log(Level.SEVERE, null, ex);
            System.exit(1);
        }
        return null;
    }
    
    private static Properties getProperties() throws IOException {
        URL configFile = Config.class.getClass().getResource("/Configuration.config");;
        InputStream configFileStream = configFile.openStream();
        Properties p = new Properties();
        p.load(configFileStream);
        configFileStream.close();
        
        return p;
    }
}
