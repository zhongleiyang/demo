namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Handlers;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.OpenTableService.Model;
    using System;
    using System.IO;

    internal class OtsExceptionHandler : ResponseHandler
    {
        public override void Handle(ServiceResponse response)
        {
            base.Handle(response);
            if (!response.IsSuccessful())
            {
                ErrorResult errorResult = null;
                try
                {
                    string contentType = response.Headers.ContainsKey("Content-Type") ? response.Headers["Content-Type"] : null;
                    IDeserializer<Stream, ErrorResult> d = new DeserializerFactory().CreateDeserializer<ErrorResult>(contentType);
                    if (d == null)
                    {
                        response.EnsureSuccessful();
                    }
                    errorResult = d.Deserialize(response.Content);
                }
                catch (ResponseDeserializationException)
                {
                    response.EnsureSuccessful();
                }
                throw ExceptionFactory.CreateException(errorResult, null);
            }
        }
    }
}

