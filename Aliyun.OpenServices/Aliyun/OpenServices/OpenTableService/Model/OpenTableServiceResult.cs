namespace Aliyun.OpenServices.OpenTableService.Model
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public abstract class OpenTableServiceResult
    {
        protected OpenTableServiceResult()
        {
        }

        [XmlElement("HostID")]
        public string HostId { get; set; }

        [XmlElement("RequestID")]
        public string RequestId { get; set; }
    }
}

