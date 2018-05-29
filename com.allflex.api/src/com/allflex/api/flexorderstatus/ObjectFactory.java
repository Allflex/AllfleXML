//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, v2.2.8-b130911.1802 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2018.05.29 at 10:44:23 AM CDT 
//


package com.allflex.api.flexorderstatus;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlElementDecl;
import javax.xml.bind.annotation.XmlRegistry;
import javax.xml.namespace.QName;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the com.allflex.api.flexorderstatus package. 
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

    private final static QName _OrderStatus_QNAME = new QName("", "OrderStatus");

    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: com.allflex.api.flexorderstatus
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link OrderStatusNode }
     * 
     */
    public OrderStatusNode createOrderStatusNode() {
        return new OrderStatusNode();
    }

    /**
     * Create an instance of {@link Document }
     * 
     */
    public Document createDocument() {
        return new Document();
    }

    /**
     * Create an instance of {@link ContainerNode }
     * 
     */
    public ContainerNode createContainerNode() {
        return new ContainerNode();
    }

    /**
     * Create an instance of {@link Tags }
     * 
     */
    public Tags createTags() {
        return new Tags();
    }

    /**
     * Create an instance of {@link TagSet }
     * 
     */
    public TagSet createTagSet() {
        return new TagSet();
    }

    /**
     * Create an instance of {@link Variable }
     * 
     */
    public Variable createVariable() {
        return new Variable();
    }

    /**
     * Create an instance of {@link Shipment }
     * 
     */
    public Shipment createShipment() {
        return new Shipment();
    }

    /**
     * Create an instance of {@link Messages }
     * 
     */
    public Messages createMessages() {
        return new Messages();
    }

    /**
     * Create an instance of {@link Address }
     * 
     */
    public Address createAddress() {
        return new Address();
    }

    /**
     * Create an instance of {@link OrderLine }
     * 
     */
    public OrderLine createOrderLine() {
        return new OrderLine();
    }

    /**
     * Create an instance of {@link TagManifest }
     * 
     */
    public TagManifest createTagManifest() {
        return new TagManifest();
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link OrderStatusNode }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "", name = "OrderStatus")
    public JAXBElement<OrderStatusNode> createOrderStatus(OrderStatusNode value) {
        return new JAXBElement<OrderStatusNode>(_OrderStatus_QNAME, OrderStatusNode.class, null, value);
    }

}
