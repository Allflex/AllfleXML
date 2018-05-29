//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, v2.2.8-b130911.1802 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2018.05.29 at 04:37:42 PM CDT 
//


package com.allflex.api.flexorder;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlElementDecl;
import javax.xml.bind.annotation.XmlRegistry;
import javax.xml.namespace.QName;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the com.allflex.api.flexorder package. 
 * <p>An ObjectFactory allows you to programatically 
 * construct new instances of the Java representation 
 * for XML content. The Java representation of XML 
 * content can consist of schema derived interfaces 
 * and classes representing the binding of schema 
 * type definitions, element declarations and model 
 * groups.  Factory methods for each of these are 
 * provided in this class.
 * 
 */
@XmlRegistry
public class ObjectFactory {

    private final static QName _OrderHeader_QNAME = new QName("", "OrderHeader");
    private final static QName _OrderHeaderNodeEmailListEIDInfo_QNAME = new QName("", "EmailListEIDInfo");
    private final static QName _OrderHeaderNodeComments_QNAME = new QName("", "Comments");
    private final static QName _OrderHeaderNodeEmailListTrackingInfo_QNAME = new QName("", "EmailListTrackingInfo");
    private final static QName _OrderHeaderNodePremiseID_QNAME = new QName("", "PremiseID");
    private final static QName _OrderHeaderNodeEmailListError_QNAME = new QName("", "EmailListError");

    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: com.allflex.api.flexorder
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link OrderHeaderNode }
     * 
     */
    public OrderHeaderNode createOrderHeaderNode() {
        return new OrderHeaderNode();
    }

    /**
     * Create an instance of {@link Document }
     * 
     */
    public Document createDocument() {
        return new Document();
    }

    /**
     * Create an instance of {@link VariablesNode }
     * 
     */
    public VariablesNode createVariablesNode() {
        return new VariablesNode();
    }

    /**
     * Create an instance of {@link UserDefinedNode }
     * 
     */
    public UserDefinedNode createUserDefinedNode() {
        return new UserDefinedNode();
    }

    /**
     * Create an instance of {@link Field }
     * 
     */
    public Field createField() {
        return new Field();
    }

    /**
     * Create an instance of {@link Variable }
     * 
     */
    public Variable createVariable() {
        return new Variable();
    }

    /**
     * Create an instance of {@link OrderLineHeader }
     * 
     */
    public OrderLineHeader createOrderLineHeader() {
        return new OrderLineHeader();
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link OrderHeaderNode }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "", name = "OrderHeader")
    public JAXBElement<OrderHeaderNode> createOrderHeader(OrderHeaderNode value) {
        return new JAXBElement<OrderHeaderNode>(_OrderHeader_QNAME, OrderHeaderNode.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "", name = "EmailListEIDInfo", scope = OrderHeaderNode.class)
    public JAXBElement<String> createOrderHeaderNodeEmailListEIDInfo(String value) {
        return new JAXBElement<String>(_OrderHeaderNodeEmailListEIDInfo_QNAME, String.class, OrderHeaderNode.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "", name = "Comments", scope = OrderHeaderNode.class)
    public JAXBElement<String> createOrderHeaderNodeComments(String value) {
        return new JAXBElement<String>(_OrderHeaderNodeComments_QNAME, String.class, OrderHeaderNode.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "", name = "EmailListTrackingInfo", scope = OrderHeaderNode.class)
    public JAXBElement<String> createOrderHeaderNodeEmailListTrackingInfo(String value) {
        return new JAXBElement<String>(_OrderHeaderNodeEmailListTrackingInfo_QNAME, String.class, OrderHeaderNode.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "", name = "PremiseID", scope = OrderHeaderNode.class)
    public JAXBElement<String> createOrderHeaderNodePremiseID(String value) {
        return new JAXBElement<String>(_OrderHeaderNodePremiseID_QNAME, String.class, OrderHeaderNode.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "", name = "EmailListError", scope = OrderHeaderNode.class)
    public JAXBElement<String> createOrderHeaderNodeEmailListError(String value) {
        return new JAXBElement<String>(_OrderHeaderNodeEmailListError_QNAME, String.class, OrderHeaderNode.class, value);
    }

}
