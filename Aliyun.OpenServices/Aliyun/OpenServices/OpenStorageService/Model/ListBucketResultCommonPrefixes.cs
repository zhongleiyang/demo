namespace Aliyun.OpenServices.OpenStorageService.Model
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, DesignerCategory("code"), XmlRoot("CommonPrefixes"), XmlType(AnonymousType=true), DebuggerStepThrough, GeneratedCode("xsd", "4.0.30319.1")]
    public class ListBucketResultCommonPrefixes
    {
        private string[] prefixField;

        [XmlElement("Prefix")]
        public string[] Prefix
        {
            get
            {
                return this.prefixField;
            }
            set
            {
                this.prefixField = value;
            }
        }
    }
}

