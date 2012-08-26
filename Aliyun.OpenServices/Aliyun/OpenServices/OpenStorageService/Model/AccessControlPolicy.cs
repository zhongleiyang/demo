namespace Aliyun.OpenServices.OpenStorageService.Model
{
    using Aliyun.OpenServices.OpenStorageService;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot("AccessControlPolicy")]
    public class AccessControlPolicy
    {
        [XmlArray("AccessControlList"), XmlArrayItem("Grant")]
        public List<string> Grants { get; set; }

        [XmlElement("Owner")]
        public Aliyun.OpenServices.OpenStorageService.Owner Owner { get; set; }
    }
}

