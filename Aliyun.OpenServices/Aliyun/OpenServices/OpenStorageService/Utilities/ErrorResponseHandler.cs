namespace Aliyun.OpenServices.OpenStorageService.Utilities
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Handlers;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.OpenStorageService.Model;
    using Aliyun.OpenServices.OpenStorageService.Transform;
    using System;
    using System.Xml;

    internal class ErrorResponseHandler : ResponseHandler
    {
        public override void Handle(ServiceResponse response)
        {
            base.Handle(response);
            if (!response.IsSuccessful())
            {
                ErrorResult errorResult = null;
                try
                {
                    IDeserializer<ServiceResponse, ErrorResult> d = DeserializerFactory.GetFactory().CreateErrorResultDeserializer();
                    if (d == null)
                    {
                        response.EnsureSuccessful();
                    }
                    errorResult = d.Deserialize(response);
                }
                catch (XmlException)
                {
                    response.EnsureSuccessful();
                }
                catch (InvalidOperationException)
                {
                    response.EnsureSuccessful();
                }
                throw ExceptionFactory.CreateException(errorResult.Code, errorResult.Message, errorResult.RequestId, errorResult.HostId);
            }
        }
    }
}

