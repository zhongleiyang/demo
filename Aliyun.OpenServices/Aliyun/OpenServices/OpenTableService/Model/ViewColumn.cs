namespace Aliyun.OpenServices.OpenTableService.Model
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ViewColumn
    {
        [XmlElement(ElementName="Type")]
        public string ColumnType { get; set; }

        public string Name { get; set; }
    }
}

