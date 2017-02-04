
using System.Xml.Serialization;

[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class dataroot
{

    private xml[] xmlField;

    private System.DateTime generatedField;

    private bool generatedFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("xml")]
    public xml[] xml
    {
        get
        {
            return this.xmlField;
        }
        set
        {
            this.xmlField = value;
        }
    }

    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime generated
    {
        get
        {
            return this.generatedField;
        }
        set
        {
            this.generatedField = value;
        }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool generatedSpecified
    {
        get
        {
            return this.generatedFieldSpecified;
        }
        set
        {
            this.generatedFieldSpecified = value;
        }
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]

public partial class xml
{

    private int serviceField;

    private short factor1Field;

    private short factor2Field;

    private short factor3Field;

    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public int service
    {
        get
        {
            return this.serviceField;
        }
        set
        {
            this.serviceField = value;
        }
    }

    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public short factor1
    {
        get
        {
            return this.factor1Field;
        }
        set
        {
            this.factor1Field = value;
        }
    }

    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public short factor2
    {
        get
        {
            return this.factor2Field;
        }
        set
        {
            this.factor2Field = value;
        }
    }

    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public short factor3
    {
        get
        {
            return this.factor3Field;
        }
        set
        {
            this.factor3Field = value;
        }
    }
}
