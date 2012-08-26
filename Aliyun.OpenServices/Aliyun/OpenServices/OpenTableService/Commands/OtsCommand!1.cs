namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.IO;

    internal abstract class OtsCommand<T> : OtsCommand
    {
        protected OtsCommand(IServiceClient client, Uri endpoint, ExecutionContext context) : base(client, endpoint, context)
        {
        }

        public static T EndExecute(IServiceClient client, IAsyncResult asyncResult)
        {
            if (asyncResult == null)
            {
                throw new ArgumentNullException("asyncResult");
            }
            using (ServiceResponse response = client.EndSend(asyncResult))
            {
                return OtsCommand<T>.GetResultFromResponse(response);
            }
        }

        public T Execute()
        {
            using (ServiceResponse response = base.Client.Send(base.CreateRequest(), base.Context))
            {
                return OtsCommand<T>.GetResultFromResponse(response);
            }
        }

        private static T GetResultFromResponse(ServiceResponse response)
        {
            T temp;
            try
            {
                string contentType = response.Headers.ContainsKey("Content-Type") ? response.Headers["Content-Type"] : null;
                IDeserializer<Stream, T> d = new DeserializerFactory().CreateDeserializer<T>(contentType);
                if (d == null)
                {
                    throw ExceptionFactory.CreateInvalidResponseException(response, OtsExceptions.ResponseDataIncomplete, null);
                }
                temp = d.Deserialize(response.Content);
            }
            catch (ResponseDeserializationException ex)
            {
                throw ExceptionFactory.CreateInvalidResponseException(response, OtsExceptions.ResponseDataIncomplete, ex);
            }
            return temp;
        }
    }
}

