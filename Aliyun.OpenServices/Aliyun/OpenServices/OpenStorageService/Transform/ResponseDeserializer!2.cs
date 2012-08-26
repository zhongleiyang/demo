namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal abstract class ResponseDeserializer<TResult, TModel> : IDeserializer<ServiceResponse, TResult>
    {
        public ResponseDeserializer(IDeserializer<Stream, TModel> contentDeserializer)
        {
            this.ContentDeserializer = contentDeserializer;
        }

        public abstract TResult Deserialize(ServiceResponse response);
       public IDeserializer<Stream, TModel> ContentDeserializer;
    }
}

