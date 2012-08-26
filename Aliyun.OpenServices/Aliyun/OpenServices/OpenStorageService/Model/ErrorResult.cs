namespace Aliyun.OpenServices.OpenStorageService.Model
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot("Error")]
    public class ErrorResult
    {
        [XmlElement("Code")]
        public string Code { get; set; }

        [XmlElement("HostID")]
        public string HostId { get; set; }

        [XmlElement("Message")]
        public string Message { get; set; }

        [XmlElement("RequestID")]
        public string RequestId { get; set; }
    }
}

