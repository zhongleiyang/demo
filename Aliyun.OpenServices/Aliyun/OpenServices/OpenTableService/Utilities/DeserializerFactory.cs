namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.Common.Transform;
    using System;
    using System.IO;

    internal class DeserializerFactory
    {
        public IDeserializer<Stream, T> CreateDeserializer<T>(string contentType)
        {
            if (contentType == null)
            {
                contentType = "text/xml";
            }
            if (contentType.Contains("xml"))
            {
                return new XmlStreamDeserializer<T>();
            }
            return null;
        }
    }
}

