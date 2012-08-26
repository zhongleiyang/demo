namespace Aliyun.OpenServices.OpenTableService.Model
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [DebuggerDisplay("Column: {Name}"), XmlRoot("Column")]
    public class InternalTableColumn
    {
        [XmlAttribute("PK")]
        public bool IsPrimaryKey { get; set; }

        public string Name { get; set; }

        public InternalTableColumnValue Value { get; set; }
    }
}

