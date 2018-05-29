//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, v2.2.8-b130911.1802 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2018.05.29 at 04:37:44 PM CDT 
//


package com.allflex.api.flexspec;

import java.util.ArrayList;
import java.util.List;
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
 *         &lt;element name="Variable" maxOccurs="unbounded">
 *           &lt;complexType>
 *             &lt;complexContent>
 *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *                 &lt;sequence>
 *                   &lt;element name="Index" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *                   &lt;element name="Name" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *                   &lt;element name="Description" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                   &lt;element name="Role" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *                   &lt;element name="Type" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *                   &lt;element name="Width" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *                   &lt;element name="Height" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *                   &lt;element name="PositionX" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *                   &lt;element name="PositionY" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *                   &lt;element name="DefaultValue" type="{http://www.w3.org/2001/XMLSchema}anyType" minOccurs="0"/>
 *                   &lt;element name="ValueFormat" type="{http://www.w3.org/2001/XMLSchema}anyType" minOccurs="0"/>
 *                   &lt;element name="MaxLength" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *                   &lt;element name="FontSize" type="{http://www.w3.org/2001/XMLSchema}anyType"/>
 *                   &lt;element name="IsFixed" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
 *                   &lt;element name="IsLaser" type="{http://www.w3.org/2001/XMLSchema}boolean" minOccurs="0"/>
 *                   &lt;element name="IsInk" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
 *                   &lt;element name="LogoImageLocation" type="{http://www.w3.org/2001/XMLSchema}anyType" minOccurs="0"/>
 *                   &lt;element name="Radius" type="{http://www.w3.org/2001/XMLSchema}int" minOccurs="0"/>
 *                   &lt;element name="CurveTextAttachTo" type="{http://www.w3.org/2001/XMLSchema}anyType" minOccurs="0"/>
 *                   &lt;element name="CopyValueFrom" type="{http://www.w3.org/2001/XMLSchema}anyType" minOccurs="0"/>
 *                 &lt;/sequence>
 *               &lt;/restriction>
 *             &lt;/complexContent>
 *           &lt;/complexType>
 *         &lt;/element>
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
    "variable"
})
public class Variables {

    @XmlElement(name = "Variable", required = true)
    protected List<Variable> variable;

    /**
     * Gets the value of the variable property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the variable property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getVariable().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link Variable }
     * 
     * 
     */
    public List<Variable> getVariable() {
        if (variable == null) {
            variable = new ArrayList<Variable>();
        }
        return this.variable;
    }

}
