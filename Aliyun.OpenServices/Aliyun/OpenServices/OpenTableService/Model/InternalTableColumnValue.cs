namespace Aliyun.OpenServices.OpenTableService.Model
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot("Value"), DebuggerDisplay("{ColumnType}: {Value}")]
    public class InternalTableColumnValue
    {
        [XmlAttribute("type")]
        public string ColumnType { get; set; }

        [XmlAttribute("encoding")]
        public string Encoding { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}

