﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações a este ficheiro poderão provocar um comportamento incorrecto e perder-se-ão se
//     o código for regenerado.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 


/// <observações/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class dataroot {
    
    private xml[] xmlField;
    
    private System.DateTime generatedField;
    
    private bool generatedFieldSpecified;
    
    /// <observações/>
    [System.Xml.Serialization.XmlElementAttribute("xml")]
    public xml[] xml {
        get {
            return this.xmlField;
        }
        set {
            this.xmlField = value;
        }
    }
    
    /// <observações/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime generated {
        get {
            return this.generatedField;
        }
        set {
            this.generatedField = value;
        }
    }
    
    /// <observações/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool generatedSpecified {
        get {
            return this.generatedFieldSpecified;
        }
        set {
            this.generatedFieldSpecified = value;
        }
    }
}

/// <observações/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class xml {
    
    private int serviceField;
    
    private short factor1Field;
    
    private short factor2Field;
    
    private short factor3Field;
    
    /// <observações/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public int service {
        get {
            return this.serviceField;
        }
        set {
            this.serviceField = value;
        }
    }
    
    /// <observações/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public short factor1 {
        get {
            return this.factor1Field;
        }
        set {
            this.factor1Field = value;
        }
    }
    
    /// <observações/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public short factor2 {
        get {
            return this.factor2Field;
        }
        set {
            this.factor2Field = value;
        }
    }
    
    /// <observações/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public short factor3 {
        get {
            return this.factor3Field;
        }
        set {
            this.factor3Field = value;
        }
    }
}
