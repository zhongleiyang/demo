namespace Aliyun.OpenServices.OpenTableService.Model
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class StartTransactionResult : OpenTableServiceResult
    {
        [XmlElement("TransactionID")]
        public string TransactionId { get; set; }
    }
}

