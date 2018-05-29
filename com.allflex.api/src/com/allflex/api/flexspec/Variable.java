//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, v2.2.8-b130911.1802 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2018.05.29 at 10:44:23 AM CDT 
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
 *         &lt;element name="Index" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="Name" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element name="Description" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="Role" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element name="Type" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element name="Width" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="Height" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="PositionX" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="PositionY" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="DefaultValue" type="{http://www.w3.org/2001/XMLSchema}anyType" minOccurs="0"/>
 *         &lt;element name="ValueFormat" type="{http://www.w3.org/2001/XMLSchema}anyType" minOccurs="0"/>
 *         &lt;element name="MaxLength" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="FontSize" type="{http://www.w3.org/2001/XMLSchema}anyType"/>
 *         &lt;element name="IsFixed" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
 *         &lt;element name="IsLaser" type="{http://www.w3.org/2001/XMLSchema}boolean" minOccurs="0"/>
 *         &lt;element name="IsInk" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
 *         &lt;element name="LogoImageLocation" type="{http://www.w3.org/2001/XMLSchema}anyType" minOccurs="0"/>
 *         &lt;element name="Radius" type="{http://www.w3.org/2001/XMLSchema}int" minOccurs="0"/>
 *         &lt;element name="CurveTextAttachTo" type="{http://www.w3.org/2001/XMLSchema}anyType" minOccurs="0"/>
 *         &lt;element name="CopyValueFrom" type="{http://www.w3.org/2001/XMLSchema}anyType" minOccurs="0"/>
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
    "index",
    "name",
    "description",
    "role",
    "type",
    "width",
    "height",
    "positionX",
    "positionY",
    "defaultValue",
    "valueFormat",
    "maxLength",
    "fontSize",
    "isFixed",
    "isLaser",
    "isInk",
    "logoImageLocation",
    "radius",
    "curveTextAttachTo",
    "copyValueFrom"
})
public class Variable {

    @XmlElement(name = "Index")
    protected int index;
    @XmlElement(name = "Name", required = true)
    protected String name;
    @XmlElement(name = "Description")
    protected String description;
    @XmlElement(name = "Role", required = true)
    protected String role;
    @XmlElement(name = "Type", required = true)
    protected String type;
    @XmlElement(name = "Width")
    protected int width;
    @XmlElement(name = "Height")
    protected int height;
    @XmlElement(name = "PositionX")
    protected int positionX;
    @XmlElement(name = "PositionY")
    protected int positionY;
    @XmlElement(name = "DefaultValue")
    protected Object defaultValue;
    @XmlElement(name = "ValueFormat")
    protected Object valueFormat;
    @XmlElement(name = "MaxLength")
    protected int maxLength;
    @XmlElement(name = "FontSize", required = true)
    protected Object fontSize;
    @XmlElement(name = "IsFixed")
    protected boolean isFixed;
    @XmlElement(name = "IsLaser")
    protected Boolean isLaser;
    @XmlElement(name = "IsInk")
    protected boolean isInk;
    @XmlElement(name = "LogoImageLocation")
    protected Object logoImageLocation;
    @XmlElement(name = "Radius")
    protected Integer radius;
    @XmlElement(name = "CurveTextAttachTo")
    protected Object curveTextAttachTo;
    @XmlElement(name = "CopyValueFrom")
    protected Object copyValueFrom;

    /**
     * Gets the value of the index property.
     * 
     */
    public int getIndex() {
        return index;
    }

    /**
     * Sets the value of the index property.
     * 
     */
    public void setIndex(int value) {
        this.index = value;
    }

    /**
     * Gets the value of the name property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getName() {
        return name;
    }

    /**
     * Sets the value of the name property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setName(String value) {
        this.name = value;
    }

    /**
     * Gets the value of the description property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getDescription() {
        return description;
    }

    /**
     * Sets the value of the description property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setDescription(String value) {
        this.description = value;
    }

    /**
     * Gets the value of the role property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getRole() {
        return role;
    }

    /**
     * Sets the value of the role property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setRole(String value) {
        this.role = value;
    }

    /**
     * Gets the value of the type property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getType() {
        return type;
    }

    /**
     * Sets the value of the type property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setType(String value) {
        this.type = value;
    }

    /**
     * Gets the value of the width property.
     * 
     */
    public int getWidth() {
        return width;
    }

    /**
     * Sets the value of the width property.
     * 
     */
    public void setWidth(int value) {
        this.width = value;
    }

    /**
     * Gets the value of the height property.
     * 
     */
    public int getHeight() {
        return height;
    }

    /**
     * Sets the value of the height property.
     * 
     */
    public void setHeight(int value) {
        this.height = value;
    }

    /**
     * Gets the value of the positionX property.
     * 
     */
    public int getPositionX() {
        return positionX;
    }

    /**
     * Sets the value of the positionX property.
     * 
     */
    public void setPositionX(int value) {
        this.positionX = value;
    }

    /**
     * Gets the value of the positionY property.
     * 
     */
    public int getPositionY() {
        return positionY;
    }

    /**
     * Sets the value of the positionY property.
     * 
     */
    public void setPositionY(int value) {
        this.positionY = value;
    }

    /**
     * Gets the value of the defaultValue property.
     * 
     * @return
     *     possible object is
     *     {@link Object }
     *     
     */
    public Object getDefaultValue() {
        return defaultValue;
    }

    /**
     * Sets the value of the defaultValue property.
     * 
     * @param value
     *     allowed object is
     *     {@link Object }
     *     
     */
    public void setDefaultValue(Object value) {
        this.defaultValue = value;
    }

    /**
     * Gets the value of the valueFormat property.
     * 
     * @return
     *     possible object is
     *     {@link Object }
     *     
     */
    public Object getValueFormat() {
        return valueFormat;
    }

    /**
     * Sets the value of the valueFormat property.
     * 
     * @param value
     *     allowed object is
     *     {@link Object }
     *     
     */
    public void setValueFormat(Object value) {
        this.valueFormat = value;
    }

    /**
     * Gets the value of the maxLength property.
     * 
     */
    public int getMaxLength() {
        return maxLength;
    }

    /**
     * Sets the value of the maxLength property.
     * 
     */
    public void setMaxLength(int value) {
        this.maxLength = value;
    }

    /**
     * Gets the value of the fontSize property.
     * 
     * @return
     *     possible object is
     *     {@link Object }
     *     
     */
    public Object getFontSize() {
        return fontSize;
    }

    /**
     * Sets the value of the fontSize property.
     * 
     * @param value
     *     allowed object is
     *     {@link Object }
     *     
     */
    public void setFontSize(Object value) {
        this.fontSize = value;
    }

    /**
     * Gets the value of the isFixed property.
     * 
     */
    public boolean isIsFixed() {
        return isFixed;
    }

    /**
     * Sets the value of the isFixed property.
     * 
     */
    public void setIsFixed(boolean value) {
        this.isFixed = value;
    }

    /**
     * Gets the value of the isLaser property.
     * 
     * @return
     *     possible object is
     *     {@link Boolean }
     *     
     */
    public Boolean isIsLaser() {
        return isLaser;
    }

    /**
     * Sets the value of the isLaser property.
     * 
     * @param value
     *     allowed object is
     *     {@link Boolean }
     *     
     */
    public void setIsLaser(Boolean value) {
        this.isLaser = value;
    }

    /**
     * Gets the value of the isInk property.
     * 
     */
    public boolean isIsInk() {
        return isInk;
    }

    /**
     * Sets the value of the isInk property.
     * 
     */
    public void setIsInk(boolean value) {
        this.isInk = value;
    }

    /**
     * Gets the value of the logoImageLocation property.
     * 
     * @return
     *     possible object is
     *     {@link Object }
     *     
     */
    public Object getLogoImageLocation() {
        return logoImageLocation;
    }

    /**
     * Sets the value of the logoImageLocation property.
     * 
     * @param value
     *     allowed object is
     *     {@link Object }
     *     
     */
    public void setLogoImageLocation(Object value) {
        this.logoImageLocation = value;
    }

    /**
     * Gets the value of the radius property.
     * 
     * @return
     *     possible object is
     *     {@link Integer }
     *     
     */
    public Integer getRadius() {
        return radius;
    }

    /**
     * Sets the value of the radius property.
     * 
     * @param value
     *     allowed object is
     *     {@link Integer }
     *     
     */
    public void setRadius(Integer value) {
        this.radius = value;
    }

    /**
     * Gets the value of the curveTextAttachTo property.
     * 
     * @return
     *     possible object is
     *     {@link Object }
     *     
     */
    public Object getCurveTextAttachTo() {
        return curveTextAttachTo;
    }

    /**
     * Sets the value of the curveTextAttachTo property.
     * 
     * @param value
     *     allowed object is
     *     {@link Object }
     *     
     */
    public void setCurveTextAttachTo(Object value) {
        this.curveTextAttachTo = value;
    }

    /**
     * Gets the value of the copyValueFrom property.
     * 
     * @return
     *     possible object is
     *     {@link Object }
     *     
     */
    public Object getCopyValueFrom() {
        return copyValueFrom;
    }

    /**
     * Sets the value of the copyValueFrom property.
     * 
     * @param value
     *     allowed object is
     *     {@link Object }
     *     
     */
    public void setCopyValueFrom(Object value) {
        this.copyValueFrom = value;
    }

}
