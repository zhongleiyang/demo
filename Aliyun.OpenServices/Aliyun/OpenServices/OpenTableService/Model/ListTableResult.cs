namespace Aliyun.OpenServices.OpenTableService.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ListTableResult : OpenTableServiceResult
    {
        [XmlArrayItem(ElementName="TableName"), XmlArray]
        public List<string> TableNames { get; set; }
    }
}

