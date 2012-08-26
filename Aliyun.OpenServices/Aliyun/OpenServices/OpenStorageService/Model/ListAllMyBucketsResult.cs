namespace Aliyun.OpenServices.OpenStorageService.Model
{
    using Aliyun.OpenServices.OpenStorageService;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot("ListAllMyBucketsResult")]
    public class ListAllMyBucketsResult
    {
        [XmlArrayItem("Bucket")]
        public List<BucketModel> Buckets { get; set; }

        [XmlElement("Owner")]
        public Aliyun.OpenServices.OpenStorageService.Owner Owner { get; set; }
    }
}

