//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, v2.2.8-b130911.1802 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2018.05.29 at 04:37:44 PM CDT 
//


package com.allflex.api.flexspec;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for anonymous complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="ColorCode" type="{http://www.w3.org/2001/XMLSchema}anyType"/>
 *         &lt;element name="Name" type="{http://www.w3.org/2001/XMLSchema}anyType"/>
 *         &lt;element name="HexCode" type="{http://www.w3.org/2001/XMLSchema}anyType"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "colorCode",
    "name",
    "hexCode"
})
public class Color {

    @XmlElement(name = "ColorCode", required = true)
    protected Object colorCode;
    @XmlElement(name = "Name", required = true)
    protected Object name;
    @XmlElement(name = "HexCode", required = true)
    protected Object hexCode;

    /**
     * Gets the value of the colorCode property.
     * 
     * @return
     *     possible object is
     *     {@link Object }
     *     
     */
    public Object getColorCode() {
        return colorCode;
    }

    /**
     * Sets the value of the colorCode property.
     * 
     * @param value
     *     allowed object is
     *     {@link Object }
     *     
     */
    public void setColorCode(Object value) {
        this.colorCode = value;
    }

    /**
     * Gets the value of the name property.
     * 
     * @return
     *     possible object is
     *     {@link Object }
     *     
     */
    public Object getName() {
        return name;
    }

    /**
     * Sets the value of the name property.
     * 
     * @param value
     *     allowed object is
     *     {@link Object }
     *     
     */
    public void setName(Object value) {
        this.name = value;
    }

    /**
     * Gets the value of the hexCode property.
     * 
     * @return
     *     possible object is
     *     {@link Object }
     *     
     */
    public Object getHexCode() {
        return hexCode;
    }

    /**
     * Sets the value of the hexCode property.
     * 
     * @param value
     *     allowed object is
     *     {@link Object }
     *     
     */
    public void setHexCode(Object value) {
        this.hexCode = value;
    }

}
