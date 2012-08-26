namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    using Aliyun.OpenServices.Common.Transform;
    using System.IO;

    internal class XmlDeserializerFactory : DeserializerFactory
    {
        protected override IDeserializer<Stream, T> CreateContentDeserializer<T>()
        {
            return new XmlStreamDeserializer<T>();
        }
    }
}

