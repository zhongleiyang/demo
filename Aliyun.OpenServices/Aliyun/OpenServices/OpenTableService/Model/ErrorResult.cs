namespace Aliyun.OpenServices.OpenTableService.Model
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot("Error")]
    public class ErrorResult : OpenTableServiceResult
    {
        public string Code { get; set; }

        public string Message { get; set; }
    }
}

