namespace Aliyun.OpenServices.OpenTableService.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ListTableGroupResult : OpenTableServiceResult
    {
        [XmlArray, XmlArrayItem(ElementName="TableGroupName")]
        public List<string> TableGroupNames { get; set; }
    }
}

