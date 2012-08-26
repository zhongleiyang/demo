namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using System;
    using System.IO;

    internal class SimpleResponseDeserializer<T> : ResponseDeserializer<T, T>
    {
        public SimpleResponseDeserializer(IDeserializer<Stream, T> contentDeserializer) : base(contentDeserializer)
        {
        }

        public override T Deserialize(ServiceResponse response)
        {
            using (response.Content)
            {
                return base.ContentDeserializer.Deserialize(response.Content);
            }
        }
    }
}

