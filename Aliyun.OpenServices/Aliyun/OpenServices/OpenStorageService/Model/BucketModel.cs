namespace Aliyun.OpenServices.OpenStorageService.Model
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot("Bucket")]
    public class BucketModel
    {
        [XmlElement("CreationDate")]
        public DateTime CreationDate { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }
    }
}

