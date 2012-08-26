namespace Aliyun.OpenServices.OpenTableService.Model
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PrimaryKey
    {
        public string Name { get; set; }

        [XmlElement(ElementName="Type")]
        public string PrimaryKeyType { get; set; }
    }
}

